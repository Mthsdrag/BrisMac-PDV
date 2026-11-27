using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class CadastroUsuario : Form
    {
        private readonly string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public CadastroUsuario()
        {
            InitializeComponent();
        }

        private void CadastroUsuario_Load(object sender, EventArgs e)
        {
            if (cmbPerfil.Items.Count == 0)
            {
                cmbPerfil.Items.Add("Administrador");
                cmbPerfil.Items.Add("Funcionario");
            }
            cmbPerfil.SelectedIndex = 1; // Padrão: Funcionario
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) ||
                string.IsNullOrWhiteSpace(txtLogin.Text) ||
                string.IsNullOrWhiteSpace(txtSenha.Text) ||
                cmbPerfil.SelectedItem == null)
            {
                MessageBox.Show("Preencha todos os campos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    // Verifica se o login já existe
                    string checkQuery = "SELECT COUNT(*) FROM tbusuarios WHERE LoginUsuario = @login";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conexao))
                    {
                        checkCmd.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                        int existe = (int)checkCmd.ExecuteScalar();
                        if (existe > 0)
                        {
                            MessageBox.Show("Já existe um usuário com esse login.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtLogin.Focus();
                            return;
                        }
                    }

                    string query = "INSERT INTO tbusuarios (Nome, LoginUsuario, Senha, Perfil) VALUES (@nome, @login, @senha, @perfil)";
                    using (SqlCommand cmd = new SqlCommand(query, conexao))
                    {
                        cmd.Parameters.AddWithValue("@nome", txtNome.Text.Trim());
                        cmd.Parameters.AddWithValue("@login", txtLogin.Text.Trim());
                        cmd.Parameters.AddWithValue("@senha", txtSenha.Text.Trim());
                        cmd.Parameters.AddWithValue("@perfil", cmbPerfil.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Usuário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cadastrar usuário: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LimparCampos()
        {
            txtNome.Clear();
            txtLogin.Clear();
            txtSenha.Clear();
            cmbPerfil.SelectedIndex = 1;
            txtNome.Focus();
        }
    }
}