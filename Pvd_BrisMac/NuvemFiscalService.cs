using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pvd_BrisMac
{
    public class ResultadoNfce
    {
        public bool Sucesso { get; set; }
        public string Status { get; set; }
        public string Mensagem { get; set; }
        public string IdNuvemFiscal { get; set; }
        public string ChaveAcesso { get; set; }
        public string Protocolo { get; set; }
        public int Numero { get; set; }
        public int Serie { get; set; }
    }

    public static class NuvemFiscalService
    {
        private static readonly string connectionString =
            @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        private const string DefaultClientId = "1oYcKDfklFhvzEv1UqL0";
        private const string DefaultClientSecret = "UTBzrZwzPbYx7kFEp9Asi2szL7BdUIt5PvWnqjkAFWRlySA0u5YH2Bx4EYgqjjKd1UfqmzNItBSQ06qyTUQjvX";

        private static readonly HttpClient httpClient = new HttpClient();

        static NuvemFiscalService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public static async Task<ResultadoNfce> EmitirNfceAsync(int vendaId)
        {
            var resultado = new ResultadoNfce();

            try
            {
                var empresa = CarregarEmpresa();
                if (empresa == null)
                {
                    resultado.Mensagem = "Dados fiscais da empresa não cadastrados (tbempresa está vazia).";
                    return resultado;
                }

                if (string.IsNullOrWhiteSpace(empresa.ApiClientId) || string.IsNullOrWhiteSpace(empresa.ApiClientSecret))
                {
                    resultado.Mensagem = "Credenciais da ACBr API (ClientId / ClientSecret) não configuradas.";
                    return resultado;
                }

                var venda = CarregarVenda(vendaId);
                if (venda == null || venda.Itens.Count == 0)
                {
                    resultado.Mensagem = "Venda ou itens da venda não encontrados no banco.";
                    return resultado;
                }

                // Trava contra duplicidade: evita emitir uma 2ª NFC-e para uma venda já autorizada
                if (venda.StatusFiscal.Equals("Autorizada", StringComparison.OrdinalIgnoreCase))
                {
                    resultado.Sucesso = true;
                    resultado.Status = "Autorizada";
                    resultado.Numero = venda.NumeroNFCe;
                    resultado.Serie = venda.SerieNFCe;
                    resultado.ChaveAcesso = venda.ChaveAcesso;
                    resultado.Protocolo = venda.ProtocoloNFCe;
                    resultado.IdNuvemFiscal = venda.IdNuvemFiscal;
                    resultado.Mensagem = $"Esta venda já possui a NFC-e nº {venda.NumeroNFCe} autorizada.";
                    return resultado;
                }

                // 1. Autentica na ACBr API (OAuth2)
                string token = await ObterTokenAsync(empresa.ApiClientId, empresa.ApiClientSecret);

                // 2. Define URL base e ambiente (ACBr API: prod para Produção, hom para Homologação/Sandbox)
                bool ehProducao = empresa.Ambiente.Equals("Producao", StringComparison.OrdinalIgnoreCase);

                string baseUrl = ehProducao
                    ? "https://prod.acbr.api.br"
                    : "https://hom.acbr.api.br";

                string ambientePayload = ehProducao ? "producao" : "homologacao";
                int proximoNumero = empresa.UltimoNumeroNFCe + 1;

                // 3. Monta o JSON da NFC-e (modelo 65)
                var payload = MontarPayloadNfce(empresa, venda, proximoNumero, ambientePayload);
                string json = JsonConvert.SerializeObject(payload);

                using (var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/nfce"))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    string respostaTexto = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        resultado.Status = "Rejeitada";
                        resultado.Mensagem = $"Erro HTTP {(int)response.StatusCode}: {ExtrairMensagemErro(respostaTexto)}";
                        AtualizarVendaFiscal(vendaId, resultado);
                        return resultado;
                    }

                    JObject dados = JObject.Parse(respostaTexto);
                    string statusApi = dados["status"]?.ToString() ?? "";
                    string idApi = dados["id"]?.ToString() ?? "";
                    string chave = dados["chave"]?.ToString() ?? "";
                    string protocolo = dados["autorizacao"]?["numero_protocolo"]?.ToString() ?? "";
                    string motivo = dados["autorizacao"]?["motivo_status"]?.ToString()
                                    ?? dados["motivo_status"]?.ToString()
                                    ?? "";

                    resultado.IdNuvemFiscal = idApi;
                    resultado.ChaveAcesso = chave;
                    resultado.Protocolo = protocolo;
                    resultado.Numero = proximoNumero;
                    resultado.Serie = empresa.SerieNFCe;

                    if (statusApi.Equals("autorizado", StringComparison.OrdinalIgnoreCase))
                    {
                        resultado.Sucesso = true;
                        resultado.Status = "Autorizada";
                        resultado.Mensagem = "NFC-e autorizada com sucesso!";

                        AtualizarUltimoNumeroNfce(empresa.EmpresaID, proximoNumero);
                        AtualizarVendaFiscal(vendaId, resultado);
                    }
                    else
                    {
                        resultado.Sucesso = false;
                        resultado.Status = statusApi.Equals("rejeitado", StringComparison.OrdinalIgnoreCase) ? "Rejeitada" : statusApi;
                        resultado.Mensagem = string.IsNullOrWhiteSpace(motivo)
                            ? $"Status retornado pela SEFAZ: {statusApi}"
                            : $"SEFAZ ({statusApi}): {motivo}";

                        AtualizarUltimoNumeroNfce(empresa.EmpresaID, proximoNumero);
                        AtualizarVendaFiscal(vendaId, resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Sucesso = false;
                resultado.Status = "Erro";
                resultado.Mensagem = "Falha na comunicação fiscal: " + ex.Message;
            }

            return resultado;
        }

        public static async Task AbrirPdfNfceAsync(string idNuvemFiscal)
        {
            var empresa = CarregarEmpresa();
            if (empresa == null || string.IsNullOrWhiteSpace(idNuvemFiscal)) return;

            string token = await ObterTokenAsync(empresa.ApiClientId, empresa.ApiClientSecret);
            string baseUrl = empresa.Ambiente.Equals("Producao", StringComparison.OrdinalIgnoreCase)
                ? "https://prod.acbr.api.br"
                : "https://hom.acbr.api.br";

            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/nfce/{idNuvemFiscal}/pdf?nome_fantasia=true"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    byte[] pdfBytes = await response.Content.ReadAsByteArrayAsync();
                    string caminhoArquivo = Path.Combine(Path.GetTempPath(), $"NFCe_{idNuvemFiscal}.pdf");
                    File.WriteAllBytes(caminhoArquivo, pdfBytes);

                    Process.Start(new ProcessStartInfo
                    {
                        FileName = caminhoArquivo,
                        UseShellExecute = true
                    });
                }
                else
                {
                    string erro = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Não foi possível baixar o PDF do cupom: {erro}");
                }
            }
        }

        private static async Task<string> ObterTokenAsync(string clientId, string clientSecret)
        {
            var form = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", clientId.Trim() },
                { "client_secret", clientSecret.Trim() },
                { "scope", "nfce empresa" }
            };

            using (var content = new FormUrlEncodedContent(form))
            {
                HttpResponseMessage response = await httpClient.PostAsync(
                    "https://auth.acbr.api.br/realms/ACBrAPI/protocol/openid-connect/token", content);
                string resposta = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Falha ao autenticar na ACBr API: " + resposta);

                JObject obj = JObject.Parse(resposta);
                return obj["access_token"].ToString();
            }
        }

        private static object MontarPayloadNfce(EmpresaFiscal empresa, VendaFiscal venda, int numeroNfce, string ambientePayload)
        {
            bool homologacao = ambientePayload == "homologacao";
            string ibge = SomenteNumeros(empresa.CodigoMunicipioIBGE);
            int cUF = ibge.Length >= 2 ? int.Parse(ibge.Substring(0, 2)) : 35; // 35 = SP

            var itensJson = new List<object>();
            int nItem = 1;

            foreach (var item in venda.Itens)
            {
                string descricaoItem = (homologacao && nItem == 1)
                    ? "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"
                    : item.Descricao;

                string ean = EhGtinValido(item.CodigoBarras) ? item.CodigoBarras : "SEM GTIN";
                string ncm = SomenteNumeros(item.NCM);
                if (ncm.Length != 8) ncm = "87141000"; // NCM genérico de peças de motocicleta caso esteja vazio

                string cfop = SomenteNumeros(item.CFOP);
                if (cfop.Length != 4) cfop = "5102";

                string csosn = SomenteNumeros(item.CSOSN);
                if (string.IsNullOrEmpty(csosn)) csosn = "102";

                int origem = int.TryParse(item.Origem, out int o) ? o : 0;

                var icms = new Dictionary<string, object>();
                if (csosn == "500")
                {
                    icms["ICMSSN500"] = new { orig = origem, CSOSN = "500" };
                }
                else
                {
                    icms["ICMSSN102"] = new { orig = origem, CSOSN = csosn };
                }

                itensJson.Add(new
                {
                    nItem = nItem,
                    prod = new
                    {
                        cProd = item.ProdutoID.ToString(),
                        cEAN = ean,
                        xProd = descricaoItem,
                        NCM = ncm,
                        CFOP = cfop,
                        uCom = string.IsNullOrWhiteSpace(item.Unidade) ? "UN" : item.Unidade.Trim(),
                        qCom = item.Quantidade,
                        vUnCom = item.PrecoUnitario,
                        vProd = item.Subtotal,
                        cEANTrib = ean,
                        uTrib = string.IsNullOrWhiteSpace(item.Unidade) ? "UN" : item.Unidade.Trim(),
                        qTrib = item.Quantidade,
                        vUnTrib = item.PrecoUnitario,
                        indTot = 1
                    },
                    imposto = new
                    {
                        ICMS = icms
                    }
                });

                nItem++;
            }

            string tPag = MapearFormaPagamento(venda.FormaPagamento);
            var detalhePagamento = new Dictionary<string, object>
            {
                ["tPag"] = tPag,
                ["vPag"] = venda.ValorTotal
            };

            // Cartão Crédito (03), Débito (04) e PIX (17) exigem grupo card com tpIntegra = 2 (não integrado ao TEF)
            if (tPag == "03" || tPag == "04" || tPag == "17")
            {
                detalhePagamento["card"] = new { tpIntegra = 2 };
            }

            var infNFe = new Dictionary<string, object>
            {
                ["versao"] = "4.00",
                ["ide"] = new
                {
                    cUF = cUF,
                    natOp = "VENDA DE MERCADORIA",
                    mod = 65,
                    serie = empresa.SerieNFCe,
                    nNF = numeroNfce,
                    dhEmi = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    tpNF = 1,
                    idDest = 1,
                    cMunFG = ibge,
                    tpImp = 4,
                    tpEmis = 1,
                    finNFe = 1,
                    indFinal = 1,
                    indPres = 1,
                    procEmi = 0,
                    verProc = "Pvd_BrisMac 1.0"
                },
                ["emit"] = new
                {
                    CNPJ = SomenteNumeros(empresa.CNPJ),
                    xNome = empresa.RazaoSocial,
                    xFant = string.IsNullOrWhiteSpace(empresa.NomeFantasia) ? empresa.RazaoSocial : empresa.NomeFantasia,
                    enderEmit = new
                    {
                        xLgr = empresa.Logradouro,
                        nro = empresa.Numero,
                        xBairro = empresa.Bairro,
                        cMun = ibge,
                        xMun = empresa.Municipio,
                        UF = empresa.UF.Trim().ToUpper(),
                        CEP = SomenteNumeros(empresa.CEP),
                        cPais = "1058",
                        xPais = "BRASIL"
                    },
                    IE = SomenteNumeros(empresa.InscricaoEstadual),
                    CRT = empresa.CRT // 4 = MEI
                },
                ["det"] = itensJson,
                ["total"] = new
                {
                    ICMSTot = new
                    {
                        vBC = 0m,
                        vICMS = 0m,
                        vICMSDeson = 0m,
                        vFCP = 0m,
                        vBCST = 0m,
                        vST = 0m,
                        vFCPST = 0m,
                        vFCPSTRet = 0m,
                        vProd = venda.ValorTotal,
                        vFrete = 0m,
                        vSeg = 0m,
                        vDesc = 0m,
                        vII = 0m,
                        vIPI = 0m,
                        vIPIDevol = 0m,
                        vPIS = 0m,
                        vCOFINS = 0m,
                        vOutro = 0m,
                        vNF = venda.ValorTotal
                    }
                },
                ["transp"] = new { modFrete = 9 },
                ["pag"] = new
                {
                    detPag = new[] { detalhePagamento }
                }
            };

            // Inclui CPF/CNPJ do consumidor apenas se informado
            string docCliente = SomenteNumeros(venda.CPFCliente);
            if (docCliente.Length == 11 || docCliente.Length == 14)
            {
                var dest = new Dictionary<string, object>
                {
                    ["indIEDest"] = 9
                };

                if (docCliente.Length == 11) dest["CPF"] = docCliente;
                else dest["CNPJ"] = docCliente;

                if (homologacao)
                    dest["xNome"] = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

                infNFe["dest"] = dest;
            }

            return new
            {
                ambiente = ambientePayload,
                referencia = $"VENDA-{venda.VendaID}-{DateTime.Now:yyyyMMddHHmmss}",
                infNFe = infNFe
            };
        }

        private static string MapearFormaPagamento(string forma)
        {
            if (string.IsNullOrWhiteSpace(forma)) return "99";
            string f = forma.ToLower();

            if (f.Contains("dinheiro")) return "01";
            if (f.Contains("crédito") || f.Contains("credito")) return "03";
            if (f.Contains("débito") || f.Contains("debito")) return "04";
            if (f.Contains("pix")) return "17";
            return "99";
        }

        private static string SomenteNumeros(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        private static bool EhGtinValido(string codigo)
        {
            string num = SomenteNumeros(codigo);
            if (num.Length != 8 && num.Length != 12 && num.Length != 13 && num.Length != 14)
                return false;

            int soma = 0;
            bool multiplicarPor3 = true;
            for (int i = num.Length - 2; i >= 0; i--)
            {
                int digito = num[i] - '0';
                soma += multiplicarPor3 ? digito * 3 : digito;
                multiplicarPor3 = !multiplicarPor3;
            }

            int dvCalculado = (10 - (soma % 10)) % 10;
            return dvCalculado == (num[num.Length - 1] - '0');
        }

        private static string ExtrairMensagemErro(string json)
        {
            try
            {
                JObject obj = JObject.Parse(json);
                return obj["error"]?["message"]?.ToString()
                       ?? obj["message"]?.ToString()
                       ?? json;
            }
            catch
            {
                return json;
            }
        }

        private static EmpresaFiscal CarregarEmpresa()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TOP 1 * FROM tbempresa";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        string dbClientId = r["ApiClientId"]?.ToString();
                        string dbClientSecret = r["ApiClientSecret"]?.ToString();

                        return new EmpresaFiscal
                        {
                            EmpresaID = Convert.ToInt32(r["EmpresaID"]),
                            RazaoSocial = r["RazaoSocial"].ToString(),
                            NomeFantasia = r["NomeFantasia"]?.ToString(),
                            CNPJ = r["CNPJ"].ToString(),
                            InscricaoEstadual = r["InscricaoEstadual"].ToString(),
                            CRT = Convert.ToInt32(r["CRT"]),
                            Logradouro = r["Logradouro"].ToString(),
                            Numero = r["Numero"].ToString(),
                            Bairro = r["Bairro"].ToString(),
                            Municipio = r["Municipio"].ToString(),
                            CodigoMunicipioIBGE = r["CodigoMunicipioIBGE"].ToString(),
                            UF = r["UF"].ToString(),
                            CEP = r["CEP"].ToString(),
                            Ambiente = r["Ambiente"].ToString(),
                            SerieNFCe = Convert.ToInt32(r["SerieNFCe"]),
                            UltimoNumeroNFCe = Convert.ToInt32(r["UltimoNumeroNFCe"]),
                            ApiClientId = string.IsNullOrWhiteSpace(dbClientId) ? DefaultClientId : dbClientId,
                            ApiClientSecret = string.IsNullOrWhiteSpace(dbClientSecret) ? DefaultClientSecret : dbClientSecret
                        };
                    }
                }
            }

            // Fallback automático com os dados da BrisMac Moto Peças caso tbempresa ainda esteja vazia
            return new EmpresaFiscal
            {
                EmpresaID = 1,
                RazaoSocial = "69.433.195 DANIELA FERNANDA BOTELHO DA SILVA",
                NomeFantasia = "BrisMac Moto Peças",
                CNPJ = "69433195000160",
                InscricaoEstadual = "371502632112",
                CRT = 4,
                Logradouro = "RUA MARIA LEONEL MONTEIRO",
                Numero = "96",
                Bairro = "VILA VISALTINO GOMES",
                Municipio = "Itapetininga",
                CodigoMunicipioIBGE = "3522307",
                UF = "SP",
                CEP = "18214028",
                Ambiente = "Producao",
                SerieNFCe = 1,
                UltimoNumeroNFCe = 0,
                ApiClientId = DefaultClientId,
                ApiClientSecret = DefaultClientSecret
            };
        }

        private static VendaFiscal CarregarVenda(int vendaId)
        {
            VendaFiscal venda = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string queryVenda = @"SELECT VendaID, ValorTotal, FormaPagamento, CPFCliente,
                                             StatusFiscal, NumeroNFCe, SerieNFCe, ChaveAcesso, ProtocoloNFCe, IdNuvemFiscal
                                      FROM tbvendas
                                      WHERE VendaID = @VendaID";

                using (SqlCommand cmd = new SqlCommand(queryVenda, conn))
                {
                    cmd.Parameters.AddWithValue("@VendaID", vendaId);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            venda = new VendaFiscal
                            {
                                VendaID = Convert.ToInt32(r["VendaID"]),
                                ValorTotal = Convert.ToDecimal(r["ValorTotal"]),
                                FormaPagamento = r["FormaPagamento"]?.ToString() ?? "",
                                CPFCliente = r["CPFCliente"]?.ToString() ?? "",
                                StatusFiscal = r["StatusFiscal"]?.ToString() ?? "",
                                NumeroNFCe = r["NumeroNFCe"] != DBNull.Value ? Convert.ToInt32(r["NumeroNFCe"]) : 0,
                                SerieNFCe = r["SerieNFCe"] != DBNull.Value ? Convert.ToInt32(r["SerieNFCe"]) : 0,
                                ChaveAcesso = r["ChaveAcesso"]?.ToString() ?? "",
                                ProtocoloNFCe = r["ProtocoloNFCe"]?.ToString() ?? "",
                                IdNuvemFiscal = r["IdNuvemFiscal"]?.ToString() ?? ""
                            };
                        }
                    }
                }

                if (venda == null) return null;

                string queryItens = @"SELECT i.ProdutoID, p.CodigoBarras, p.Descricao, p.Unidade, p.NCM, p.CFOP, p.CSOSN, p.Origem,
                                             i.Quantidade, i.PrecoUnitario, i.Subtotal
                                      FROM tbitensvenda i
                                      INNER JOIN tbprodutos p ON p.ProdutoID = i.ProdutoID
                                      WHERE i.VendaID = @VendaID";

                using (SqlCommand cmd = new SqlCommand(queryItens, conn))
                {
                    cmd.Parameters.AddWithValue("@VendaID", vendaId);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            venda.Itens.Add(new ItemVendaFiscal
                            {
                                ProdutoID = Convert.ToInt32(r["ProdutoID"]),
                                CodigoBarras = r["CodigoBarras"]?.ToString() ?? "",
                                Descricao = r["Descricao"].ToString(),
                                Unidade = r["Unidade"]?.ToString() ?? "UN",
                                NCM = r["NCM"]?.ToString() ?? "",
                                CFOP = r["CFOP"]?.ToString() ?? "5102",
                                CSOSN = r["CSOSN"]?.ToString() ?? "102",
                                Origem = r["Origem"]?.ToString() ?? "0",
                                Quantidade = Convert.ToDecimal(r["Quantidade"]),
                                PrecoUnitario = Convert.ToDecimal(r["PrecoUnitario"]),
                                Subtotal = Convert.ToDecimal(r["Subtotal"])
                            });
                        }
                    }
                }
            }

            return venda;
        }

        private static void AtualizarUltimoNumeroNfce(int empresaId, int novoNumero)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tbempresa SET UltimoNumeroNFCe = @Numero WHERE EmpresaID = @EmpresaID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Numero", novoNumero);
                    cmd.Parameters.AddWithValue("@EmpresaID", empresaId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private static void AtualizarVendaFiscal(int vendaId, ResultadoNfce res)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE tbvendas
                                 SET StatusFiscal = @Status,
                                     NumeroNFCe = @Numero,
                                     SerieNFCe = @Serie,
                                     ChaveAcesso = @Chave,
                                     ProtocoloNFCe = @Protocolo,
                                     IdNuvemFiscal = @IdApi
                                 WHERE VendaID = @VendaID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", (object)res.Status ?? "Erro");
                    cmd.Parameters.AddWithValue("@Numero", res.Numero > 0 ? (object)res.Numero : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Serie", res.Serie > 0 ? (object)res.Serie : DBNull.Value);
                    cmd.Parameters.AddWithValue("@Chave", string.IsNullOrEmpty(res.ChaveAcesso) ? (object)DBNull.Value : res.ChaveAcesso);
                    cmd.Parameters.AddWithValue("@Protocolo", string.IsNullOrEmpty(res.Protocolo) ? (object)DBNull.Value : res.Protocolo);
                    cmd.Parameters.AddWithValue("@IdApi", string.IsNullOrEmpty(res.IdNuvemFiscal) ? (object)DBNull.Value : res.IdNuvemFiscal);
                    cmd.Parameters.AddWithValue("@VendaID", vendaId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private class EmpresaFiscal
        {
            public int EmpresaID { get; set; }
            public string RazaoSocial { get; set; }
            public string NomeFantasia { get; set; }
            public string CNPJ { get; set; }
            public string InscricaoEstadual { get; set; }
            public int CRT { get; set; }
            public string Logradouro { get; set; }
            public string Numero { get; set; }
            public string Bairro { get; set; }
            public string Municipio { get; set; }
            public string CodigoMunicipioIBGE { get; set; }
            public string UF { get; set; }
            public string CEP { get; set; }
            public string Ambiente { get; set; }
            public int SerieNFCe { get; set; }
            public int UltimoNumeroNFCe { get; set; }
            public string ApiClientId { get; set; }
            public string ApiClientSecret { get; set; }
        }

        private class VendaFiscal
        {
            public int VendaID { get; set; }
            public decimal ValorTotal { get; set; }
            public string FormaPagamento { get; set; }
            public string CPFCliente { get; set; }
            public string StatusFiscal { get; set; }
            public int NumeroNFCe { get; set; }
            public int SerieNFCe { get; set; }
            public string ChaveAcesso { get; set; }
            public string ProtocoloNFCe { get; set; }
            public string IdNuvemFiscal { get; set; }
            public List<ItemVendaFiscal> Itens { get; set; } = new List<ItemVendaFiscal>();
        }

        private class ItemVendaFiscal
        {
            public int ProdutoID { get; set; }
            public string CodigoBarras { get; set; }
            public string Descricao { get; set; }
            public string Unidade { get; set; }
            public string NCM { get; set; }
            public string CFOP { get; set; }
            public string CSOSN { get; set; }
            public string Origem { get; set; }
            public decimal Quantidade { get; set; }
            public decimal PrecoUnitario { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}