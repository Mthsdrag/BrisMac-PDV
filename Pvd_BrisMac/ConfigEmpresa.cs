using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class ConfigEmpresa : Form
    {
        private readonly string connectionString =
            @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        private int empresaIdAtual = 0;

        public ConfigEmpresa()
        {
            InitializeComponent();
            ConfigurarCombos();
            CarregarDadosEmpresa();
        }

        private void ConfigurarCombos()
        {
            cbCrt.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCrt.Items.Clear();
            cbCrt.Items.Add("4 - MEI");
            cbCrt.Items.Add("1 - Simples Nacional");
            cbCrt.Items.Add("3 - Regime Normal");
            cbCrt.SelectedIndex = 0;

            cbAmbiente.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAmbiente.Items.Clear();
            cbAmbiente.Items.Add("Sandbox");
            cbAmbiente.Items.Add("Homologacao");
            cbAmbiente.Items.Add("Producao");
            cbAmbiente.SelectedIndex = 0;
        }

        private void CarregarDadosEmpresa()
        {
            try
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
                            empresaIdAtual = Convert.ToInt32(r["EmpresaID"]);
                            txtRazaoSocial.Text = r["RazaoSocial"].ToString();
                            txtNomeFantasia.Text = r["NomeFantasia"]?.ToString() ?? "";
                            txtCnpj.Text = r["CNPJ"].ToString();
                            txtIE.Text = r["InscricaoEstadual"].ToString();

                            int crt = Convert.ToInt32(r["CRT"]);
                            if (crt == 4) cbCrt.SelectedIndex = 0;
                            else if (crt == 1) cbCrt.SelectedIndex = 1;
                            else if (crt == 3) cbCrt.SelectedIndex = 2;

                            txtLogradouro.Text = r["Logradouro"].ToString();
                            txtNumero.Text = r["Numero"].ToString();
                            txtBairro.Text = r["Bairro"].ToString();
                            txtMunicipio.Text = r["Municipio"].ToString();
                            txtCodigoIBGE.Text = r["CodigoMunicipioIBGE"].ToString();
                            txtUF.Text = r["UF"].ToString();
                            txtCep.Text = r["CEP"].ToString();

                            string ambiente = r["Ambiente"].ToString();
                            int idxAmbiente = cbAmbiente.FindStringExact(ambiente);
                            cbAmbiente.SelectedIndex = idxAmbiente >= 0 ? idxAmbiente : 0;

                            txtSerie.Text = r["SerieNFCe"].ToString();
                            txtUltimoNumero.Text = r["UltimoNumeroNFCe"].ToString();
                            txtClientId.Text = r["ApiClientId"]?.ToString() ?? "";
                            txtClientSecret.Text = r["ApiClientSecret"]?.ToString() ?? "";
                        }
                        else
                        {
                            // Valores padrão para primeiro cadastro (MEI em São Paulo)
                            txtUF.Text = "SP";
                            txtSerie.Text = "1";
                            txtUltimoNumero.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar configurações da empresa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string SomenteNumeros(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return "";
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string cnpj = SomenteNumeros(txtCnpj.Text);
            string ie = SomenteNumeros(txtIE.Text);
            string ibge = SomenteNumeros(txtCodigoIBGE.Text);
            string cep = SomenteNumeros(txtCep.Text);

            if (string.IsNullOrWhiteSpace(txtRazaoSocial.Text) || cnpj.Length != 14 || string.IsNullOrWhiteSpace(ie))
            {
                MessageBox.Show("Preencha corretamente a Razão Social, CNPJ (14 números) e Inscrição Estadual.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ibge.Length != 7 || cep.Length != 8 || txtUF.Text.Trim().Length != 2)
            {
                MessageBox.Show("Verifique o Código IBGE (7 dígitos), CEP (8 dígitos) e UF (2 letras).", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSerie.Text.Trim(), out int serie) || serie <= 0) serie = 1;
            if (!int.TryParse(txtUltimoNumero.Text.Trim(), out int ultimoNumero) || ultimoNumero < 0) ultimoNumero = 0;

            int crt = int.Parse(cbCrt.Text.Substring(0, 1));

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query;

                    if (empresaIdAtual == 0)
                    {
                        query = @"INSERT INTO tbempresa
                                  (RazaoSocial, NomeFantasia, CNPJ, InscricaoEstadual, CRT,
                                   Logradouro, Numero, Bairro, Municipio, CodigoMunicipioIBGE, UF, CEP,
                                   Ambiente, SerieNFCe, UltimoNumeroNFCe, ApiClientId, ApiClientSecret)
                                  OUTPUT INSERTED.EmpresaID
                                  VALUES
                                  (@RazaoSocial, @NomeFantasia, @CNPJ, @IE, @CRT,
                                   @Logradouro, @Numero, @Bairro, @Municipio, @IBGE, @UF, @CEP,
                                   @Ambiente, @Serie, @UltimoNumero, @ClientId, @ClientSecret)";
                    }
                    else
                    {
                        query = @"UPDATE tbempresa SET
                                    RazaoSocial = @RazaoSocial,
                                    NomeFantasia = @NomeFantasia,
                                    CNPJ = @CNPJ,
                                    InscricaoEstadual = @IE,
                                    CRT = @CRT,
                                    Logradouro = @Logradouro,
                                    Numero = @Numero,
                                    Bairro = @Bairro,
                                    Municipio = @Municipio,
                                    CodigoMunicipioIBGE = @IBGE,
                                    UF = @UF,
                                    CEP = @CEP,
                                    Ambiente = @Ambiente,
                                    SerieNFCe = @Serie,
                                    UltimoNumeroNFCe = @UltimoNumero,
                                    ApiClientId = @ClientId,
                                    ApiClientSecret = @ClientSecret
                                  WHERE EmpresaID = @EmpresaID";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RazaoSocial", txtRazaoSocial.Text.Trim());
                        cmd.Parameters.AddWithValue("@NomeFantasia", txtNomeFantasia.Text.Trim());
                        cmd.Parameters.AddWithValue("@CNPJ", cnpj);
                        cmd.Parameters.AddWithValue("@IE", ie);
                        cmd.Parameters.AddWithValue("@CRT", crt);
                        cmd.Parameters.AddWithValue("@Logradouro", txtLogradouro.Text.Trim());
                        cmd.Parameters.AddWithValue("@Numero", txtNumero.Text.Trim());
                        cmd.Parameters.AddWithValue("@Bairro", txtBairro.Text.Trim());
                        cmd.Parameters.AddWithValue("@Municipio", txtMunicipio.Text.Trim());
                        cmd.Parameters.AddWithValue("@IBGE", ibge);
                        cmd.Parameters.AddWithValue("@UF", txtUF.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@CEP", cep);
                        cmd.Parameters.AddWithValue("@Ambiente", cbAmbiente.Text);
                        cmd.Parameters.AddWithValue("@Serie", serie);
                        cmd.Parameters.AddWithValue("@UltimoNumero", ultimoNumero);
                        cmd.Parameters.AddWithValue("@ClientId", txtClientId.Text.Trim());
                        cmd.Parameters.AddWithValue("@ClientSecret", txtClientSecret.Text.Trim());

                        if (empresaIdAtual == 0)
                        {
                            empresaIdAtual = (int)cmd.ExecuteScalar();
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EmpresaID", empresaIdAtual);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                MessageBox.Show("Configurações fiscais salvas com sucesso!", "BrisMac PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar configurações: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}