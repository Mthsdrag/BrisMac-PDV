using Pvd_BrisMac;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class ConsultaUsuario : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public ConsultaUsuario()
        {
            InitializeComponent();
        }

        private void ConsultaUsuario_Load(object sender, EventArgs e)
        {
            CarregarUsuarios();
            EstiloGrid.AplicarNoForm(this);
        }

        private void CarregarUsuarios()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    string query = "SELECT UsuarioID, Nome, LoginUsuario, Perfil FROM tbusuarios ORDER BY Nome";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tabela = new DataTable();
                        adaptador.Fill(tabela);

                        dgvTabelaUsuarios.DataSource = tabela;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar usuários: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPesquisarUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();
                    string query = @"SELECT UsuarioID, Nome, LoginUsuario, Perfil 
                                     FROM tbusuarios 
                                     WHERE Nome LIKE @pesquisa 
                                        OR LoginUsuario LIKE @pesquisa 
                                        OR Perfil LIKE @pesquisa
                                     ORDER BY Nome";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@pesquisa", "%" + txtPesquisaUsuarios.Text.Trim() + "%");

                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            DataTable tabela = new DataTable();
                            adaptador.Fill(tabela);

                            if (tabela.Rows.Count == 0)
                            {
                                MessageBox.Show("Nenhum usuário encontrado na base de dados.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            dgvTabelaUsuarios.DataSource = tabela;
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

        private void txtPesquisaUsuarios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnPesquisarUsuario.PerformClick();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvTabelaUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTabelaUsuarios.SelectedRows[0].Cells["UsuarioID"].Value);
                string nome = dgvTabelaUsuarios.SelectedRows[0].Cells["Nome"].Value.ToString();
                string login = dgvTabelaUsuarios.SelectedRows[0].Cells["LoginUsuario"].Value.ToString();
                string perfil = dgvTabelaUsuarios.SelectedRows[0].Cells["Perfil"].Value.ToString();

                EditarUsuario telaEditar = new EditarUsuario(id, nome, login, perfil);
                telaEditar.ShowDialog();

                CarregarUsuarios();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um usuário na lista para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTabelaUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgvTabelaUsuarios.Rows[e.RowIndex].Cells["UsuarioID"].Value);
                string nome = dgvTabelaUsuarios.Rows[e.RowIndex].Cells["Nome"].Value.ToString();
                string login = dgvTabelaUsuarios.Rows[e.RowIndex].Cells["LoginUsuario"].Value.ToString();
                string perfil = dgvTabelaUsuarios.Rows[e.RowIndex].Cells["Perfil"].Value.ToString();

                EditarUsuario telaEditar = new EditarUsuario(id, nome, login, perfil);
                telaEditar.ShowDialog();

                CarregarUsuarios();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (dgvTabelaUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTabelaUsuarios.SelectedRows[0].Cells["UsuarioID"].Value);
                string nome = dgvTabelaUsuarios.SelectedRows[0].Cells["Nome"].Value.ToString();

                DialogResult resposta = MessageBox.Show(
                    $"Tem certeza absoluta que deseja EXCLUIR o usuário '{nome}'?\n\nEsta ação não pode ser desfeita.",
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
                            string query = "DELETE FROM tbusuarios WHERE UsuarioID = @UsuarioID";

                            using (SqlCommand comando = new SqlCommand(query, conexao))
                            {
                                comando.Parameters.AddWithValue("@UsuarioID", id);
                                comando.ExecuteNonQuery();

                                MessageBox.Show("Usuário excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CarregarUsuarios();
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 547)
                        {
                            MessageBox.Show(
                                $"Não é possível excluir '{nome}' porque este usuário já possui registros vinculados no sistema.",
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
                        MessageBox.Show("Erro ao excluir o usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um usuário na lista para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadastroUsuario telaCadastro = new CadastroUsuario();
            telaCadastro.ShowDialog();
            CarregarUsuarios();
        }
    }
}