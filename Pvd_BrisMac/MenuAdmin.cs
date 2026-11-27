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
    public partial class MenuAdmin : Form
    {
        public MenuAdmin()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(SessaoUsuario.Nome))
            {
                lblBoasVindas.Text = $"Olá, {SessaoUsuario.Nome}!";
            }
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

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            // Instancia e abre a tela de Consulta de Produtos
            ConsultaProdutos telaConsulta = new ConsultaProdutos();
            telaConsulta.ShowDialog();
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            pdv FrenteCaixa = new pdv();
            FrenteCaixa.ShowDialog();
        }

        private void btnHistoricoVendas_Click(object sender, EventArgs e)
        {
            HistoricoVendas telaHistorico = new HistoricoVendas();
            telaHistorico.ShowDialog();
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            Relatorio telaRelatorio = new Relatorio();
            telaRelatorio.ShowDialog();
        }

        private void btnFechamentoCaixa_Click(object sender, EventArgs e)
        {
            FechamentoCaixa telaFechamento = new FechamentoCaixa();
            telaFechamento.ShowDialog();
        }

        private void btnFuncionarios_Click(object sender, EventArgs e)
        {
            ConsultaUsuario telaConsultaUsuario = new ConsultaUsuario();
            telaConsultaUsuario.ShowDialog();
        }

        private void MenuAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnConfigFiscal_Click(object sender, EventArgs e)
        {
            ConfigEmpresa telaConfig = new ConfigEmpresa();
            telaConfig.ShowDialog();
        }
    }
}