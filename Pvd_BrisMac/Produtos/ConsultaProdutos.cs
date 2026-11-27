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
    public partial class ConsultaProdutos : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public ConsultaProdutos()
        {
            InitializeComponent();
        }

        private bool EhAdministrador()
        {
            return !string.IsNullOrEmpty(SessaoUsuario.Perfil) &&
                   (SessaoUsuario.Perfil.Equals("Administrador", StringComparison.OrdinalIgnoreCase) ||
                    SessaoUsuario.Perfil.Equals("adm", StringComparison.OrdinalIgnoreCase));
        }

        private void PesquisaProdutos_Load_1(object sender, EventArgs e)
        {
            CarregarProdutos();
            EstiloGrid.AplicarNoForm(this);

            if (!EhAdministrador())
            {
                btnCadastrar.Visible = false;
                btnEditar.Visible = false;
                btnExcluir.Visible = false;
                btnControle.Visible = false;
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    string query = @"SELECT ProdutoID, CodigoBarras, Descricao, PrecoVenda, Estoque, Localizacao, AplicacaoMotos, DataCadastro 
                                     FROM tbProdutos 
                                     WHERE Descricao LIKE @pesquisa 
                                        OR CodigoBarras LIKE @pesquisa 
                                        OR AplicacaoMotos LIKE @pesquisa 
                                        OR Localizacao LIKE @pesquisa";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@pesquisa", "%" + txtPesquisaProdutos.Text.Trim() + "%");

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            DataTable tabela = new DataTable();
                            adaptador.Fill(tabela);

                            if (tabela.Rows.Count == 0)
                            {
                                MessageBox.Show("Nenhum produto encontrado na base de dados.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            dgvTabelaProdutos.DataSource = tabela;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Erro de conexão com o banco de dados.\n\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro inesperado durante a pesquisa: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTabelaProdutos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTabelaProdutos.SelectedRows[0].Cells["ProdutoID"].Value);
                string codigo = dgvTabelaProdutos.SelectedRows[0].Cells["CodigoBarras"].Value.ToString();
                string descricao = dgvTabelaProdutos.SelectedRows[0].Cells["Descricao"].Value.ToString();
                decimal preco = Convert.ToDecimal(dgvTabelaProdutos.SelectedRows[0].Cells["PrecoVenda"].Value);
                string localizacao = dgvTabelaProdutos.SelectedRows[0].Cells["Localizacao"].Value?.ToString() ?? "";
                string aplicacaoMotos = dgvTabelaProdutos.SelectedRows[0].Cells["AplicacaoMotos"].Value?.ToString() ?? "";

                EditarProdutos telaEditar = new EditarProdutos(id, codigo, descricao, preco, localizacao, aplicacaoMotos);
                telaEditar.ShowDialog();

                CarregarProdutos();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto na lista para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarregarProdutos()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    string query = "SELECT ProdutoID, CodigoBarras, Descricao, PrecoVenda, Estoque, Localizacao, AplicacaoMotos, DataCadastro FROM tbprodutos";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tabela = new DataTable();
                        adaptador.Fill(tabela);

                        dgvTabelaProdutos.DataSource = tabela;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTabelaProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!EhAdministrador())
                return;

            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgvTabelaProdutos.Rows[e.RowIndex].Cells["ProdutoID"].Value);
                string codigo = dgvTabelaProdutos.Rows[e.RowIndex].Cells["CodigoBarras"].Value.ToString();
                string descricao = dgvTabelaProdutos.Rows[e.RowIndex].Cells["Descricao"].Value.ToString();
                decimal preco = Convert.ToDecimal(dgvTabelaProdutos.Rows[e.RowIndex].Cells["PrecoVenda"].Value);
                string localizacao = dgvTabelaProdutos.Rows[e.RowIndex].Cells["Localizacao"].Value?.ToString() ?? "";
                string aplicacaoMotos = dgvTabelaProdutos.Rows[e.RowIndex].Cells["AplicacaoMotos"].Value?.ToString() ?? "";

                EditarProdutos telaEditar = new EditarProdutos(id, codigo, descricao, preco, localizacao, aplicacaoMotos);
                telaEditar.ShowDialog();

                CarregarProdutos();
            }
        }

        private void txtPesquisaProdutos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPesquisarProduto.PerformClick();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvTabelaProdutos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTabelaProdutos.SelectedRows[0].Cells["ProdutoID"].Value);
                string descricao = dgvTabelaProdutos.SelectedRows[0].Cells["Descricao"].Value.ToString();

                DialogResult resposta = MessageBox.Show(
                    $"Tem certeza absoluta que deseja EXCLUIR o produto '{descricao}'?\n\nEsta ação não pode ser desfeita.",
                    "Confirmação de Exclusão",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (resposta == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection conexao = new SqlConnection(connectionString))
                        {
                            conexao.Open();
                            string query = "DELETE FROM tbprodutos WHERE ProdutoID = @ProdutoID";

                            using (SqlCommand comando = new SqlCommand(query, conexao))
                            {
                                comando.Parameters.AddWithValue("@ProdutoID", id);
                                comando.ExecuteNonQuery();

                                MessageBox.Show("Produto excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CarregarProdutos();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            MessageBox.Show(
                                $"Não é possível excluir '{descricao}' porque este produto já possui vendas registradas no histórico.\n\nDica: Se não for mais vender este item, zere o estoque dele.",
                                "Exclusão Bloqueada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Erro no banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto na lista para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadastroProdutos telaCadastro = new CadastroProdutos();
            telaCadastro.ShowDialog();
            CarregarProdutos();
        }

        private void btnControle_Click(object sender, EventArgs e)
        {
            EntradaProdutos controle = new EntradaProdutos();
            controle.ShowDialog();
            CarregarProdutos();
        }
    }
}