using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class pdv : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public pdv()
        {
            InitializeComponent();
            CarregarSugestoesProdutos();
            this.Shown += (s, e) => txtNomeProduto.Focus();
        }

        // Função central que adiciona o produto (usada pelo Código de Barras, F2 e Nome do Produto)
        private void AdicionarProduto(string codigo, int quantidadeDesejada = -1)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return;

            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                string query = "SELECT Descricao, PrecoVenda, Localizacao FROM tbprodutos WHERE CodigoBarras = @codigo";

                using (SqlCommand comando = new SqlCommand(query, conexao))
                {
                    comando.Parameters.AddWithValue("@codigo", codigo.Trim());

                    try
                    {
                        conexao.Open();
                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                string descricao = leitor["Descricao"].ToString();
                                string localizacao = leitor["Localizacao"]?.ToString().Trim() ?? "";

                                // Mostra "Descrição / P1 A" se houver prateleira cadastrada
                                string textoExibicao = string.IsNullOrWhiteSpace(localizacao)
                                    ? descricao
                                    : $"{descricao} / {localizacao}";

                                decimal precoUnitario = 0;
                                decimal.TryParse(leitor["PrecoVenda"].ToString(), out precoUnitario);

                                int qtd = 1;

                                // Se veio uma quantidade da tela de F2, usa ela. Senão, olha a caixinha principal.
                                if (quantidadeDesejada > 0)
                                {
                                    qtd = quantidadeDesejada;
                                }
                                else
                                {
                                    if (!int.TryParse(txtQtd.Text, out qtd) || qtd <= 0)
                                    {
                                        qtd = 1;
                                    }
                                }

                                decimal subtotal = precoUnitario * qtd;

                                // Adiciona na tabela
                                dataGridView1.Rows.Add(codigo.Trim(), textoExibicao, qtd, precoUnitario, subtotal);

                                // Atualiza o letreiro verde gigante
                                AtualizarTotal();

                                // Reseta a quantidade principal
                                txtQtd.Text = "1";
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao buscar produto: " + ex.Message);
                    }
                }
            }
        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string codigo = txtCodigoBarras.Text.Trim();

                AdicionarProduto(codigo);

                txtCodigoBarras.Clear();
                txtCodigoBarras.Focus();
            }
        }

        private async void pdv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                PesquisaProduto telaBusca = new PesquisaProduto();
                telaBusca.ShowDialog();

                if (!string.IsNullOrEmpty(telaBusca.CodigoSelecionado))
                {
                    AdicionarProduto(telaBusca.CodigoSelecionado, telaBusca.QuantidadeSelecionada);
                    txtNomeProduto.Focus();
                }
            }
            else if (e.KeyCode == Keys.F12)
            {
                await AbrirTelaPagamentoAsync();
            }
        }

        private void AtualizarTotal()
        {
            decimal totalVenda = 0;

            foreach (DataGridViewRow linha in dataGridView1.Rows)
            {
                if (linha.Cells[4].Value != null)
                {
                    totalVenda += Convert.ToDecimal(linha.Cells[4].Value);
                }
            }

            lblTotal.Text = totalVenda.ToString("C2");
            AjustarFonteTotal();
        }

        private void AjustarFonteTotal()
        {
            float tamanhoFonte = 32f;

            while (tamanhoFonte > 10f)
            {
                using (Font fonteTeste = new Font(lblTotal.Font.FontFamily, tamanhoFonte, lblTotal.Font.Style))
                {
                    Size tamanhoTexto = TextRenderer.MeasureText(lblTotal.Text, fonteTeste);

                    if (tamanhoTexto.Width <= lblTotal.Width - 10 && tamanhoTexto.Height <= lblTotal.Height - 6)
                    {
                        break;
                    }
                }
                tamanhoFonte -= 1f;
            }

            lblTotal.Font = new Font(lblTotal.Font.FontFamily, tamanhoFonte, lblTotal.Font.Style);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.CurrentRow != null)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    txtNomeProduto.Focus();
                    AtualizarTotal();
                }
            }
        }

        private async Task AbrirTelaPagamentoAsync()
        {
            if (dataGridView1.Rows.Count == 0 || (dataGridView1.Rows.Count == 1 && dataGridView1.Rows[0].IsNewRow))
            {
                MessageBox.Show("O carrinho está vazio! Adicione produtos para finalizar a venda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string totalString = lblTotal.Text.Replace("R$", "").Replace(" ", "").Trim();
            decimal totalVenda = 0;
            decimal.TryParse(totalString, out totalVenda);

            TelaPagamento telaPagamento = new TelaPagamento(totalVenda);

            if (telaPagamento.ShowDialog() == DialogResult.OK)
            {
                int idVendaGerado = 0;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        int usuarioLogadoId = SessaoUsuario.UsuarioID > 0 ? SessaoUsuario.UsuarioID : 1;

                        string queryVenda = @"INSERT INTO tbvendas (DataVenda, ValorTotal, FormaPagamento, CPFCliente, UsuarioID) 
                                              OUTPUT INSERTED.VendaID
                                              VALUES (@DataVenda, @ValorTotal, @FormaPagamento, @CPFCliente, @UsuarioID)";

                        using (SqlCommand cmdVenda = new SqlCommand(queryVenda, conn, transaction))
                        {
                            cmdVenda.Parameters.AddWithValue("@DataVenda", DateTime.Now);
                            cmdVenda.Parameters.AddWithValue("@ValorTotal", totalVenda);
                            cmdVenda.Parameters.AddWithValue("@FormaPagamento", telaPagamento.FormaPagamentoEscolhida);
                            cmdVenda.Parameters.AddWithValue("@CPFCliente", telaPagamento.CpfCnpjCliente);
                            cmdVenda.Parameters.AddWithValue("@UsuarioID", usuarioLogadoId);

                            idVendaGerado = (int)cmdVenda.ExecuteScalar();
                        }

                        string queryItens = @"INSERT INTO tbitensvenda (VendaID, ProdutoID, Quantidade, PrecoUnitario, Subtotal) 
                                              VALUES (@VendaID, (SELECT ProdutoID FROM tbprodutos WHERE CodigoBarras = @CodigoBarras), @Quantidade, @PrecoUnitario, @Subtotal)";

                        string queryEstoque = @"UPDATE tbprodutos 
                                                SET Estoque = Estoque - @QtdVendida 
                                                WHERE CodigoBarras = @CodigoBarras";

                        foreach (DataGridViewRow linha in dataGridView1.Rows)
                        {
                            if (linha.IsNewRow) continue;

                            string codigoBarrasItem = linha.Cells[0].Value.ToString();
                            decimal quantidadeVendida = Convert.ToDecimal(linha.Cells[2].Value);

                            using (SqlCommand cmdItem = new SqlCommand(queryItens, conn, transaction))
                            {
                                cmdItem.Parameters.AddWithValue("@VendaID", idVendaGerado);
                                cmdItem.Parameters.AddWithValue("@CodigoBarras", codigoBarrasItem);
                                cmdItem.Parameters.AddWithValue("@Quantidade", quantidadeVendida);
                                cmdItem.Parameters.AddWithValue("@PrecoUnitario", Convert.ToDecimal(linha.Cells[3].Value));
                                cmdItem.Parameters.AddWithValue("@Subtotal", Convert.ToDecimal(linha.Cells[4].Value));

                                cmdItem.ExecuteNonQuery();
                            }

                            using (SqlCommand cmdEstoque = new SqlCommand(queryEstoque, conn, transaction))
                            {
                                cmdEstoque.Parameters.AddWithValue("@QtdVendida", quantidadeVendida);
                                cmdEstoque.Parameters.AddWithValue("@CodigoBarras", codigoBarrasItem);

                                cmdEstoque.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Erro ao gravar a venda no banco de dados: " + ex.Message, "Erro Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Limpa o carrinho logo após gravar no banco com sucesso
                dataGridView1.Rows.Clear();
                AtualizarTotal();
                txtCodigoBarras.Clear();
                txtNomeProduto.Clear();

                // Se marcou para emitir NFC-e, envia para a Nuvem Fiscal
                if (telaPagamento.EmitirNfce)
                {
                    this.Cursor = Cursors.WaitCursor;
                    this.Enabled = false;

                    try
                    {
                        ResultadoNfce resultado = await NuvemFiscalService.EmitirNfceAsync(idVendaGerado);

                        if (resultado.Sucesso)
                        {
                            MessageBox.Show(
                                $"Venda gravada e NFC-e nº {resultado.Numero} autorizada com sucesso!",
                                "BrisMac PDV - Fiscal",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            try
                            {
                                await NuvemFiscalService.AbrirPdfNfceAsync(resultado.IdNuvemFiscal);
                            }
                            catch (Exception exPdf)
                            {
                                MessageBox.Show("A nota foi autorizada, mas não foi possível abrir o PDF: " + exPdf.Message,
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show(
                                $"A venda #{idVendaGerado} foi gravada normalmente no caixa, mas houve um erro ao emitir a NFC-e:\n\n{resultado.Mensagem}",
                                "Atenção - Emissão Fiscal",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                    }
                    finally
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("Venda gravada com sucesso!", "BrisMac PDV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                txtNomeProduto.Focus();
            }
        }

        private async void btnFinalizarVenda_Click(object sender, EventArgs e)
        {
            await AbrirTelaPagamentoAsync();
        }

        private void txtNomeProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string nomeDigitado = txtNomeProduto.Text.Trim();
                if (string.IsNullOrEmpty(nomeDigitado)) return;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT TOP 1 CodigoBarras FROM tbprodutos WHERE Descricao LIKE @Nome + '%'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nomeDigitado);

                        object resultado = cmd.ExecuteScalar();

                        if (resultado != null)
                        {
                            string codigoEncontrado = resultado.ToString();
                            AdicionarProduto(codigoEncontrado);

                            txtNomeProduto.Clear();
                            txtNomeProduto.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Produto não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void CarregarSugestoesProdutos()
        {
            AutoCompleteStringCollection listaProdutos = new AutoCompleteStringCollection();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT Descricao FROM tbprodutos";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listaProdutos.Add(reader["Descricao"].ToString());
                    }
                }
            }

            txtNomeProduto.AutoCompleteCustomSource = listaProdutos;
        }

        private void pdv_Load(object sender, EventArgs e)
        {
            EstiloGrid.AplicarNoForm(this);
            this.ActiveControl = txtNomeProduto;
        }
    }
}