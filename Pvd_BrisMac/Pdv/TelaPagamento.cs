using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class TelaPagamento : Form
    {
        private decimal TotalDaVenda = 0;
        public string FormaPagamentoEscolhida { get; private set; } = "Dinheiro";
        public string CpfCnpjCliente { get; private set; } = "";
        public bool EmitirNfce { get; private set; } = false;

        public TelaPagamento(decimal total)
        {
            InitializeComponent();
            TotalDaVenda = total;
        }

        private void TelaPagamento_Load_1(object sender, EventArgs e)
        {
            lblTotalPagar.Text = TotalDaVenda.ToString("C2");
            lblTroco.Text = "R$ 0,00";
            cbFormaPagamento.SelectedIndex = 0; // Seleciona "Dinheiro" por padrão ao abrir
            chkEmitirNfce.Checked = false;
            txtCpfCliente.Clear();
            txtValorRecebido.Focus();
        }

        private bool ConverterValor(string texto, out decimal valor)
        {
            string limpo = texto.Trim();

            // Se tiver ponto e vírgula (ex: "1.000,00"), remove o ponto de milhar
            if (limpo.Contains(".") && limpo.Contains(","))
            {
                limpo = limpo.Replace(".", "");
            }
            // Se tiver só ponto (ex: "1000.50" digitado no teclado numérico), troca por vírgula
            else if (limpo.Contains("."))
            {
                limpo = limpo.Replace(".", ",");
            }

            return decimal.TryParse(limpo, out valor);
        }

        private void txtValorRecebido_TextChanged(object sender, EventArgs e)
        {
            if (ConverterValor(txtValorRecebido.Text, out decimal recebido))
            {
                decimal troco = recebido - TotalDaVenda;

                if (troco >= 0)
                {
                    lblTroco.Text = troco.ToString("C2");
                    lblTroco.ForeColor = Color.Lime;
                }
                else
                {
                    lblTroco.Text = "Falta dinheiro!";
                    lblTroco.ForeColor = Color.Red;
                }
            }
            else
            {
                lblTroco.Text = "R$ 0,00";
                lblTroco.ForeColor = Color.Lime;
            }
        }

        private void txtValorRecebido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                Finalizar();
            }
        }

        private void btnConfirmar_Click_1(object sender, EventArgs e)
        {
            Finalizar();
        }

        private void Finalizar()
        {
            ConverterValor(txtValorRecebido.Text, out decimal recebido);

            if (recebido < TotalDaVenda)
            {
                MessageBox.Show("O valor recebido não cobre o total da venda!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorRecebido.Focus();
                return;
            }

            // Limpa pontos, traços e barras do CPF/CNPJ
            string docLimpo = new string(txtCpfCliente.Text.Where(char.IsDigit).ToArray());

            if (!string.IsNullOrEmpty(docLimpo) && docLimpo.Length != 11 && docLimpo.Length != 14)
            {
                MessageBox.Show("CPF/CNPJ inválido! Digite 11 números para CPF, 14 para CNPJ ou deixe em branco.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCpfCliente.Focus();
                return;
            }

            FormaPagamentoEscolhida = cbFormaPagamento.Text;
            CpfCnpjCliente = docLimpo;
            EmitirNfce = chkEmitirNfce.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cbFormaPagamento_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cbFormaPagamento.Text != "Dinheiro")
            {
                // F2 preenche sem ponto de milhar (ex: "1000,00")
                txtValorRecebido.Text = TotalDaVenda.ToString("F2");
                txtValorRecebido.Enabled = false;
                btnConfirmar.Focus();
            }
            else
            {
                txtValorRecebido.Text = "";
                txtValorRecebido.Enabled = true;
                txtValorRecebido.Focus();
            }
        }

        private void chkEmitirNfce_CheckedChanged(object sender, EventArgs e)
        {
            txtCpfCliente.Enabled = chkEmitirNfce.Checked;

            if (!chkEmitirNfce.Checked)
            {
                txtCpfCliente.Clear();
            }
        }
    }
}