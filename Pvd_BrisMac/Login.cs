using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void txtSenha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnEntrar.PerformClick();
            }
        }

        private void btnEntrar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text) || string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, preencha o usuário e a senha.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string connectionString = @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";
                bool loginValido = false;
                string perfil = "";

                using (SqlConnection conexao = new SqlConnection(connectionString))
                {
                    conexao.Open();

                    string query = "SELECT UsuarioID, Nome, Perfil FROM tbusuarios WHERE LoginUsuario = @usuario AND Senha = @senha";

                    using (SqlCommand comando = new SqlCommand(query, conexao))
                    {
                        comando.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
                        comando.Parameters.AddWithValue("@senha", txtSenha.Text.Trim());

                        using (SqlDataReader leitor = comando.ExecuteReader())
                        {
                            if (leitor.Read())
                            {
                                loginValido = true;
                                perfil = leitor["Perfil"].ToString().Trim();

                                SessaoUsuario.UsuarioID = Convert.ToInt32(leitor["UsuarioID"]);
                                SessaoUsuario.Nome = leitor["Nome"].ToString().Trim().Split(' ')[0];
                                SessaoUsuario.Perfil = perfil;
                            }
                        }
                    }
                }

                if (!loginValido)
                {
                    MessageBox.Show("Usuário ou senha incorretos.", "Acesso Negado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSenha.Clear();
                    txtSenha.Focus();
                    return;
                }

                this.Hide();
                DialogResult resultado;

                if (perfil.Equals("adm", StringComparison.OrdinalIgnoreCase) ||
                    perfil.Equals("Administrador", StringComparison.OrdinalIgnoreCase))
                {
                    using (MenuAdmin telaAdm = new MenuAdmin())
                        resultado = telaAdm.ShowDialog();
                }
                else if (perfil.Equals("Funcionario", StringComparison.OrdinalIgnoreCase) ||
                         perfil.Equals("Funcionário", StringComparison.OrdinalIgnoreCase) ||
                         perfil.Equals("Operador de Caixa", StringComparison.OrdinalIgnoreCase))
                {
                    using (MenuFuncionario telaFunc = new MenuFuncionario())
                        resultado = telaFunc.ShowDialog();
                }
                else
                {
                    MessageBox.Show($"Perfil '{perfil}' não reconhecido pelo sistema.", "Erro de Perfil",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Show();
                    return;
                }

                if (resultado == DialogResult.Retry)
                {
                    SessaoUsuario.UsuarioID = 0;
                    SessaoUsuario.Nome = null;
                    SessaoUsuario.Perfil = null;

                    txtUsuario.Clear();
                    txtSenha.Clear();
                    this.Show();
                    txtUsuario.Focus();
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}