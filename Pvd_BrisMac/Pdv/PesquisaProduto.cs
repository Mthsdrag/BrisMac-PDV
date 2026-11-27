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
    public partial class PesquisaProduto : Form
    {
        // Variáveis que o PDV vai ler quando esta tela fechar
        public string CodigoSelecionado { get; private set; } = "";
        public int QuantidadeSelecionada { get; private set; } = 1;

        public PesquisaProduto()
        {
            InitializeComponent();
           
        }

        private void txtNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string busca = txtNome.Text.Trim();

                if (busca == "") return;

                dataGridView1.Rows.Clear(); // Limpa buscas anteriores

                string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    string query = @"SELECT CodigoBarras, Descricao, PrecoVenda, Localizacao, AplicacaoMotos, Estoque 
                                     FROM tbprodutos 
                                     WHERE Descricao LIKE @busca 
                                        OR CodigoBarras LIKE @busca 
                                        OR AplicacaoMotos LIKE @busca 
                                        OR Localizacao LIKE @busca";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@busca", "%" + busca + "%");

                        try
                        {
                            conexao.Open();
                            using (SqlDataReader leitor = comando.ExecuteReader())
                            {
                                while (leitor.Read())
                                {
                                    dataGridView1.Rows.Add(
                                        leitor["CodigoBarras"].ToString(),
                                        leitor["Descricao"].ToString(),
                                        Convert.ToDecimal(leitor["PrecoVenda"]),
                                        leitor["Localizacao"].ToString(),
                                        leitor["AplicacaoMotos"].ToString(),
                                        Convert.ToInt32(leitor["Estoque"])
                                    );
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao buscar: " + ex.Message);
                        }
                    }
                }
            }
        }

        // Evento de Duplo Clique na tabela para selecionar o produto
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            SelecionarProduto();
        }

        // Atalho opcional: Apertar Enter na tabela também seleciona
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SelecionarProduto();
            }
        }

        private void SelecionarProduto()
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index >= 0)
            {
                // Pega o código de barras da primeira coluna
                CodigoSelecionado = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                // Pega a quantidade digitada na caixinha txtQtd da tela de pesquisa
                int qtd = 1;
                if (int.TryParse(txtQtd.Text, out qtd) && qtd > 0)
                {
                    QuantidadeSelecionada = qtd;
                }
                else
                {
                    QuantidadeSelecionada = 1; // Padrão se estiver vazio
                }

                this.Close();
            }
        }

        private void PesquisaProduto_Load(object sender, EventArgs e)
        {
            // Adiciona as colunas novas na tabela automaticamente se ainda não existirem
            if (!dataGridView1.Columns.Contains("Localizacao"))
                dataGridView1.Columns.Add("Localizacao", "Prateleira");

            if (!dataGridView1.Columns.Contains("AplicacaoMotos"))
                dataGridView1.Columns.Add("AplicacaoMotos", "Motos Compatíveis");

            if (!dataGridView1.Columns.Contains("Estoque"))
                dataGridView1.Columns.Add("Estoque", "Estoque");

            txtNome.Focus();
            if (string.IsNullOrWhiteSpace(txtQtd.Text))
            {
                txtQtd.Text = "1"; // Deixa a quantidade 1 por padrão ao abrir
            }
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            SelecionarProduto();
        }

        private void PesquisaProduto_Load_1(object sender, EventArgs e)
        {
            EstiloGrid.AplicarNoForm(this);
        }
    }
}