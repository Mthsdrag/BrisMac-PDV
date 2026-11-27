using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class CadastroProdutos : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public CadastroProdutos()
        {
            InitializeComponent();
        }

        private void btnCadastrarProduto_Click_1(object sender, EventArgs e)
        {
            // Validação do nome/descrição
            if (string.IsNullOrWhiteSpace(txtDescriProduto.Text))
            {
                MessageBox.Show("Informe o nome do produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescriProduto.Focus();
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
            txtPrecoProduto.Text = precoValidado.ToString("F2");

            // Validação do código de barras (permite letras e números, apenas impede ficar vazio)
            if (string.IsNullOrWhiteSpace(txtCodigoBarras.Text))
            {
                MessageBox.Show("Informe ou gere um código de barras para o produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoBarras.Focus();
                return;
            }

            // Validação do estoque
            if (!int.TryParse(txtEstoque.Text, out int estoqueValidado) || estoqueValidado < 0)
            {
                MessageBox.Show("O estoque deve ser um número inteiro igual ou maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEstoque.Focus();
                return;
            }

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string query = @"INSERT INTO tbProdutos (CodigoBarras, Descricao, PrecoVenda, Estoque, Localizacao, AplicacaoMotos, DataCadastro) 
                             VALUES (@CodigoBarras, @Descricao, @PrecoVenda, @Estoque, @Localizacao, @AplicacaoMotos, @DataCadastro)";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@CodigoBarras", txtCodigoBarras.Text.Trim().ToUpper());
                        comando.Parameters.AddWithValue("@Descricao", txtDescriProduto.Text.Trim());
                        comando.Parameters.AddWithValue("@PrecoVenda", precoValidado);
                        comando.Parameters.AddWithValue("@Estoque", estoqueValidado);
                        comando.Parameters.AddWithValue("@Localizacao", txtLocalizacao.Text.Trim());
                        comando.Parameters.AddWithValue("@AplicacaoMotos", txtAplicacaoMotos.Text.Trim());
                        comando.Parameters.AddWithValue("@DataCadastro", DateTime.Now);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtCodigoBarras.Clear();
                        txtDescriProduto.Clear();
                        txtPrecoProduto.Clear();
                        txtEstoque.Clear();
                        txtLocalizacao.Clear();
                        txtAplicacaoMotos.Clear();
                        txtDescriProduto.Focus();
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                {
                    MessageBox.Show("Este Código de Barras já está registado em outro produto! Verifique o código e tente novamente.", "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Erro na base de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado ao cadastrar o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtEstoque_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            Random gerador = new Random();

            // Gera no padrão BRIS + 6 dígitos (ex: BRIS482915)
            string codigoInterno = "BRIS" + gerador.Next(100000, 999999).ToString();

            txtCodigoBarras.Text = codigoInterno;
            txtCodigoBarras.Focus();
        }
    }
}