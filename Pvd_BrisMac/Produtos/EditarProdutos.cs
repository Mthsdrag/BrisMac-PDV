using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class EditarProdutos : Form
    {
        int idProdutoSelecionado;

        public EditarProdutos(int id, string codigo, string descricao, decimal preco, string localizacao, string aplicacaoMotos)
        {
            InitializeComponent();
            idProdutoSelecionado = id;

            txtCodigoBarras.Text = codigo;
            txtDescriProduto.Text = descricao;
            txtPrecoProduto.Text = preco.ToString("F2");
            txtLocalizacao.Text = localizacao;
            txtAplicacaoMotos.Text = aplicacaoMotos;
        }

        private void btnCadastrarProduto_Click(object sender, EventArgs e)
        {
            // Validação do nome/descrição
            if (string.IsNullOrWhiteSpace(txtDescriProduto.Text))
            {
                MessageBox.Show("O nome do produto não pode ficar vazio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescriProduto.Focus();
                return;
            }

            // Validação do código de barras (aceita letras e números)
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                MessageBox.Show("O código de barras não pode ficar vazio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoBarras.Focus();
                return;
            }

            // Validação do preço (aceita 1.000,00 | 1.000 | 1000 | 10,50 | 10.50)
            string precoLimpo = txtPrecoProduto.Text.Trim().Replace("R$", "").Trim();

            if (precoLimpo.Contains(","))
            {
                precoLimpo = precoLimpo.Replace(".", "");
            }
            else if (precoLimpo.Contains("."))
            {
                string[] partes = precoLimpo.Split('.');
                if (partes.Length == 2 && partes[1].Length <= 2)
                    precoLimpo = precoLimpo.Replace(".", ",");
                else
                    precoLimpo = precoLimpo.Replace(".", "");
            }

            if (!decimal.TryParse(precoLimpo, NumberStyles.AllowDecimalPoint, new CultureInfo("pt-BR"), out decimal precoValidado) || precoValidado <= 0)
            {
                MessageBox.Show("O valor do preço está inválido. Informe um valor maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecoProduto.Focus();
                return;
            }

            try
            {
                string stringConexao = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection conexao = new SqlConnection(stringConexao))
                {
                    conexao.Open();

                    string query = @"UPDATE tbprodutos 
                                     SET CodigoBarras = @CodigoBarras, 
                                         Descricao = @Descricao, 
                                         PrecoVenda = @PrecoVenda,
                                         Localizacao = @Localizacao,
                                         AplicacaoMotos = @AplicacaoMotos
                                     WHERE ProdutoID = @ProdutoID";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@CodigoBarras", txtCodigoBarras.Text.Trim().ToUpper());
                        comando.Parameters.AddWithValue("@Descricao", txtDescriProduto.Text.Trim());
                        comando.Parameters.AddWithValue("@PrecoVenda", precoValidado);
                        comando.Parameters.AddWithValue("@Localizacao", txtLocalizacao.Text.Trim().ToUpper());
                        comando.Parameters.AddWithValue("@AplicacaoMotos", txtAplicacaoMotos.Text.Trim());
                        comando.Parameters.AddWithValue("@ProdutoID", idProdutoSelecionado);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Este Código de Barras já está em uso por outro produto!", "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Erro ao editar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {

        }
    }
}