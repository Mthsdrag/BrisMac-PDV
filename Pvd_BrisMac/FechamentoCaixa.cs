using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class FechamentoCaixa : Form
    {
        private readonly string connectionString =
            @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        private decimal totalDinheiro = 0m;
        private decimal totalPix = 0m;
        private decimal totalDebito = 0m;
        private decimal totalCredito = 0m;
        private decimal totalDia = 0m;

        public FechamentoCaixa()
        {
            InitializeComponent();
        }

        private void FechamentoCaixa_Load(object sender, EventArgs e)
        {
            EstiloGrid.AplicarNoForm(this);
            dtpDataCaixa.Value = DateTime.Today;
            CarregarResumoDia();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CarregarResumoDia();
        }

        private void CarregarResumoDia()
        {
            DateTime inicio = dtpDataCaixa.Value.Date;
            DateTime fim = inicio.AddDays(1).AddTicks(-1);

            totalDinheiro = 0m;
            totalPix = 0m;
            totalDebito = 0m;
            totalCredito = 0m;
            totalDia = 0m;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            FormaPagamento,
                            COUNT(*) AS QtdVendas,
                            ISNULL(SUM(ValorTotal), 0) AS Total
                        FROM tbvendas
                        WHERE DataVenda BETWEEN @Inicio AND @Fim
                        GROUP BY FormaPagamento
                        ORDER BY Total DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Inicio", inicio);
                        cmd.Parameters.AddWithValue("@Fim", fim);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvResumoCaixa.DataSource = dt;
                        FormatarGrid();

                        foreach (DataRow row in dt.Rows)
                        {
                            string forma = row["FormaPagamento"].ToString();
                            decimal valor = Convert.ToDecimal(row["Total"]);
                            totalDia += valor;

                            if (forma.Equals("Dinheiro", StringComparison.OrdinalIgnoreCase))
                                totalDinheiro += valor;
                            else if (forma.Equals("Pix", StringComparison.OrdinalIgnoreCase))
                                totalPix += valor;
                            else if (forma.IndexOf("Débito", StringComparison.OrdinalIgnoreCase) >= 0)
                                totalDebito += valor;
                            else if (forma.IndexOf("Crédito", StringComparison.OrdinalIgnoreCase) >= 0)
                                totalCredito += valor;
                        }
                    }
                }

                lblTotalDia.Text = totalDia.ToString("C2");
                AtualizarCalculoGaveta();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar caixa: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatarGrid()
        {
            if (dgvResumoCaixa.Columns.Count == 0) return;

            dgvResumoCaixa.Columns["FormaPagamento"].HeaderText = "Forma de Pagamento";

            dgvResumoCaixa.Columns["QtdVendas"].HeaderText = "Qtd.";
            dgvResumoCaixa.Columns["QtdVendas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvResumoCaixa.Columns["Total"].HeaderText = "Total";
            dgvResumoCaixa.Columns["Total"].DefaultCellStyle.Format = "C2";
            dgvResumoCaixa.Columns["Total"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void CalcularDiferenca_TextChanged(object sender, EventArgs e)
        {
            AtualizarCalculoGaveta();
        }

        private void AtualizarCalculoGaveta()
        {
            decimal fundoTroco = ConverterDecimal(txtFundoTroco.Text);
            decimal dinheiroContado = ConverterDecimal(txtDinheiroContado.Text);

            decimal esperadoGaveta = fundoTroco + totalDinheiro;
            decimal diferenca = dinheiroContado - esperadoGaveta;

            lblEsperadoGaveta.Text = esperadoGaveta.ToString("C2");
            lblDiferenca.Text = diferenca.ToString("C2");

            if (diferenca < 0)
                lblDiferenca.ForeColor = Color.Crimson;
            else if (diferenca > 0)
                lblDiferenca.ForeColor = Color.ForestGreen;
            else
                lblDiferenca.ForeColor = Color.Black;
        }

        private decimal ConverterDecimal(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0m;

            string limpo = texto.Replace("R$", "").Trim();
            if (decimal.TryParse(limpo, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal valor))
                return valor;

            return 0m;
        }

        private void btnConfirmarFechamento_Click(object sender, EventArgs e)
        {
            decimal fundoTroco = ConverterDecimal(txtFundoTroco.Text);
            decimal dinheiroContado = ConverterDecimal(txtDinheiroContado.Text);
            decimal diferenca = dinheiroContado - (fundoTroco + totalDinheiro);

            DialogResult confirm = MessageBox.Show(
                $"Confirmar fechamento do dia {dtpDataCaixa.Value:dd/MM/yyyy}?\n\n" +
                $"Total Vendido: {totalDia:C2}\n" +
                $"Esperado na Gaveta: {(fundoTroco + totalDinheiro):C2}\n" +
                $"Dinheiro Contado: {dinheiroContado:C2}\n" +
                $"Diferença: {diferenca:C2}",
                "Confirmar Fechamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO tbfechamentocaixa
                            (DataCaixa, TotalDinheiro, TotalPix, TotalDebito, TotalCredito,
                             TotalVendido, FundoTroco, DinheiroContado, Diferenca, Observacao)
                        VALUES
                            (@DataCaixa, @TotalDinheiro, @TotalPix, @TotalDebito, @TotalCredito,
                             @TotalVendido, @FundoTroco, @DinheiroContado, @Diferenca, @Observacao)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@DataCaixa", dtpDataCaixa.Value.Date);
                        cmd.Parameters.AddWithValue("@TotalDinheiro", totalDinheiro);
                        cmd.Parameters.AddWithValue("@TotalPix", totalPix);
                        cmd.Parameters.AddWithValue("@TotalDebito", totalDebito);
                        cmd.Parameters.AddWithValue("@TotalCredito", totalCredito);
                        cmd.Parameters.AddWithValue("@TotalVendido", totalDia);
                        cmd.Parameters.AddWithValue("@FundoTroco", fundoTroco);
                        cmd.Parameters.AddWithValue("@DinheiroContado", dinheiroContado);
                        cmd.Parameters.AddWithValue("@Diferenca", diferenca);
                        cmd.Parameters.AddWithValue("@Observacao", txtObservacao.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Fechamento de caixa registrado com sucesso!", "Sucesso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFundoTroco.Text = "0,00";
                txtDinheiroContado.Text = "0,00";
                txtObservacao.Clear();
                AtualizarCalculoGaveta();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar fechamento: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}