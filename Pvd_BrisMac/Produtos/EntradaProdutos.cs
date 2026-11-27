using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Pvd_BrisMac
{
    public partial class EntradaProdutos : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        // Guarda o código exato do produto encontrado (permite buscar pelo nome e atualizar pelo código)
        string codigoProdutoAtual = "";

        public EntradaProdutos()
        {
            InitializeComponent();
            CarregarSugestoesProdutos();
        }

        private void CarregarSugestoesProdutos()
        {
            AutoCompleteStringCollection listaProdutos = new AutoCompleteStringCollection();

            try
            {
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

                txtCodigoBarras.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtCodigoBarras.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtCodigoBarras.AutoCompleteCustomSource = listaProdutos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar sugestões de produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ExecutarBusca();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoProdutoAtual))
            {
                MessageBox.Show("Busque um produto primeiro antes de adicionar ao estoque.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoBarras.Focus();
                return;
            }

            if (!int.TryParse(txtQuantidadeEntrada.Text, out int quantidadeNova) || quantidadeNova <= 0)
            {
                MessageBox.Show("Digite uma quantidade válida maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult resposta = MessageBox.Show(
                $"Deseja realmente adicionar {quantidadeNova} unidades ao produto: {lbNomeProduto.Text}?",
                "Confirmação de Entrada",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resposta == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    string query = "UPDATE tbprodutos SET Estoque = Estoque + @quantidadeNova WHERE CodigoBarras = @codigo";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@quantidadeNova", quantidadeNova);
                        comando.Parameters.AddWithValue("@codigo", codigoProdutoAtual);

                        int linhasAfetadas = comando.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            MessageBox.Show("Estoque atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimparCampos();
                        }
                        else
                        {
                            MessageBox.Show("Erro: Produto não encontrado para atualizar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o estoque: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEstornar_Click(object sender, EventArgs e)
        {

        }

        private void btnEstornar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(codigoProdutoAtual))
            {
                MessageBox.Show("Busque um produto primeiro antes de remover do estoque.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigoBarras.Focus();
                return;
            }

            if (!int.TryParse(txtQuantidadeEntrada.Text, out int quantidadeErrada) || quantidadeErrada <= 0)
            {
                MessageBox.Show("Digite uma quantidade válida para estornar.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    // PASSO 1: Verifica quanto o produto tem no estoque atualmente
                    string queryConsulta = "SELECT Estoque FROM tbprodutos WHERE CodigoBarras = @codigo";
                    int estoqueAtual = 0;

                    using (SqlCommand cmdConsulta = new SqlCommand(queryConsulta, conexao))
                    {
                        cmdConsulta.Parameters.AddWithValue("@codigo", codigoProdutoAtual);
                        object resultado = cmdConsulta.ExecuteScalar();

                        if (resultado != null)
                        {
                            estoqueAtual = Convert.ToInt32(resultado);
                        }
                        else
                        {
                            MessageBox.Show("Produto não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    // PASSO 2: Trava de segurança contra estoque negativo
                    if (quantidadeErrada > estoqueAtual)
                    {
                        MessageBox.Show($"Erro: Você está tentando remover {quantidadeErrada} unidades, mas no estoque há apenas {estoqueAtual}!",
                                        "Estoque Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // PASSO 3: Confirmação e estorno
                    DialogResult resposta = MessageBox.Show(
                        $"ATENÇÃO: Deseja REMOVER {quantidadeErrada} unidades do produto {lbNomeProduto.Text}?",
                        "Confirmação de Estorno",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (resposta == DialogResult.No)
                    {
                        return;
                    }

                    string queryUpdate = "UPDATE tbprodutos SET Estoque = Estoque - @quantidadeErrada WHERE CodigoBarras = @codigo";

                    using (SqlCommand comando = new SqlCommand(queryUpdate, conexao))
                    {
                        comando.Parameters.AddWithValue("@quantidadeErrada", quantidadeErrada);
                        comando.Parameters.AddWithValue("@codigo", codigoProdutoAtual);

                        comando.ExecuteNonQuery();

                        MessageBox.Show("Estorno realizado com sucesso! O estoque foi corrigido.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao processar estorno: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCodigoBarras_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ExecutarBusca();
            }
        }

        private void ExecutarBusca()
        {
            string termoDigitado = txtCodigoBarras.Text.Trim();

            if (string.IsNullOrWhiteSpace(termoDigitado))
            {
                MessageBox.Show("Digite um código de barras ou o nome do produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    // Prioriza código exato; depois nome que começa com o texto; depois nome que contém o texto
                    string query = @"SELECT TOP 1 CodigoBarras, Descricao, Estoque 
                                     FROM tbprodutos 
                                     WHERE CodigoBarras = @busca 
                                        OR Descricao LIKE @busca + '%' 
                                        OR Descricao LIKE '%' + @busca + '%'
                                     ORDER BY 
                                        CASE 
                                            WHEN CodigoBarras = @busca THEN 1
                                            WHEN Descricao LIKE @busca + '%' THEN 2
                                            ELSE 3
                                        END";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@busca", termoDigitado);

                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                codigoProdutoAtual = leitor["CodigoBarras"].ToString();
                                lbNomeProduto.Text = leitor["Descricao"].ToString();
                                lbEstoqueAtual.Text = leitor["Estoque"].ToString();
                                txtQuantidadeEntrada.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Produto não encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                codigoProdutoAtual = "";
                                lbNomeProduto.Text = "---";
                                lbEstoqueAtual.Text = "---";
                                txtCodigoBarras.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao buscar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            codigoProdutoAtual = "";
            txtCodigoBarras.Text = "";
            txtQuantidadeEntrada.Text = "";
            lbNomeProduto.Text = "---";
            lbEstoqueAtual.Text = "---";
            txtCodigoBarras.Focus();
        }

        private void txtQuantidadeEntrada_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnConfirmar_Click(sender, e);
            }
        }

        private void EntradaProdutos_Load(object sender, EventArgs e)
        {

        }
    }
}