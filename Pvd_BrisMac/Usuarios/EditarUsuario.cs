using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class EditarUsuario : Form
    {
        string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";
        int usuarioId;

        public EditarUsuario(int id, string nome, string login, string perfil)
        {
            InitializeComponent();
            usuarioId = id;

            if (cmbPerfil.Items.Count == 0)
            {
                cmbPerfil.Items.Add("Administrador");
                cmbPerfil.Items.Add("Funcionario");
            }

            txtNome.Text = nome;
            txtLogin.Text = login;
            cmbPerfil.SelectedItem = perfil;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                cmbPerfil.SelectedItem == null)
            {
                MessageBox.Show("Preencha Nome, Login e Perfil.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string query;
                    if (string.IsNullOrWhiteSpace(txtSenha.Text))
                    {
                        query = "UPDATE tbusuarios SET Nome = @nome, LoginUsuario = @login, Perfil = @perfil WHERE UsuarioID = @id";
                    }
                    else
                    {
                        query = "UPDATE tbusuarios SET Nome = @nome, LoginUsuario = @login, Senha = @senha, Perfil = @perfil WHERE UsuarioID = @id";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
                        cmd.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                        cmd.Parameters.AddWithValue("@perfil", cmbPerfil.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@id", usuarioId);

                        if (!string.IsNullOrWhiteSpace(txtSenha.Text))
                        {
                            cmd.Parameters.AddWithValue("@senha", txtSenha.Text.Trim());
                        }

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Usuário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}