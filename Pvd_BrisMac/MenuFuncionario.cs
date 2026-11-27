using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class MenuFuncionario : Form
    {
        public MenuFuncionario()
        {
            InitializeComponent();
        }

        private void MenuFuncionario_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
            {
                lblBoasVindas.Text = $"Olá, {SessaoUsuario.Nome}!";
            }
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            pdv frenteCaixa = new pdv();
            frenteCaixa.ShowDialog();
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            ConsultaProdutos telaConsulta = new ConsultaProdutos();
            telaConsulta.ShowDialog();
        }

        private void btnFechamentoCaixa_Click(object sender, EventArgs e)
        {
            FechamentoCaixa telaFechamento = new FechamentoCaixa();
            telaFechamento.ShowDialog();
        }

        private void btnHistoricoVendas_Click(object sender, EventArgs e)
        {
            HistoricoVendas telaHistorico = new HistoricoVendas();
            telaHistorico.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogResult resposta = MessageBox.Show(
         "Deseja sair da conta e voltar para o login?",
         "Sair",
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Question);

            if (resposta == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }
    }
}