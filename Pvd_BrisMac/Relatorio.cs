using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class Relatorio : Form
    {
        private readonly string connectionString =
            @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public Relatorio()
        {
            InitializeComponent();
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {
            dtpDataFim.Value = DateTime.Today;
            dtpDataInicio.Value = DateTime.Today.AddDays(-7);

            if (cmbFormaPagamento.Items.Count == 0)
            {
                cmbFormaPagamento.Items.AddRange(new object[]
                {
                    "Todas",
                    "Dinheiro",
                    "Pix",
                    "Cartão de Débito",
                    "Cartão de Crédito"
                });
            }
            cmbFormaPagamento.SelectedIndex = 0;

            CarregarRelatorio();
            EstiloGrid.AplicarNoForm(this);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarRelatorio();
        }

        private void CarregarRelatorio()
        {
            DateTime inicio = dtpDataInicio.Value.Date;
            DateTime fim = dtpDataFim.Value.Date.AddDays(1).AddTicks(-1);

            if (inicio > dtpDataFim.Value.Date)
            {
                MessageBox.Show("A Data Início não pode ser maior que a Data Fim.", "Atenção",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string forma = cmbFormaPagamento.SelectedItem?.ToString() ?? "Todas";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    CarregarResumoPagamento(conn, inicio, fim, forma);
                    CarregarProdutosMaisVendidos(conn, inicio, fim, forma);
                    CarregarTotalPeriodo(conn, inicio, fim, forma);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao gerar relatório: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarResumoPagamento(SqlConnection conn, DateTime inicio, DateTime fim, string forma)
        {
            string query = @"
                SELECT 
                    FormaPagamento,
                    COUNT(*) AS QtdVendas,
                    ISNULL(SUM(ValorTotal), 0) AS TotalPago
                FROM tbvendas
                WHERE DataVenda BETWEEN @Inicio AND @Fim";

            if (forma != "Todas")
            {
                query += " AND FormaPagamento = @FormaPagamento";
            }

            query += @"
                GROUP BY FormaPagamento
                ORDER BY TotalPago DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Inicio", inicio);
                cmd.Parameters.AddWithValue("@Fim", fim);
                if (forma != "Todas")
                {
                    cmd.Parameters.AddWithValue("@FormaPagamento", forma);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPagamento.DataSource = dt;
                FormatarGridPagamento();
            }
        }

        private void FormatarGridPagamento()
        {
            if (dgvPagamento.Columns.Count == 0) return;

            dgvPagamento.Columns["FormaPagamento"].HeaderText = "Forma de Pagamento";

            dgvPagamento.Columns["QtdVendas"].HeaderText = "Qtd. Vendas";
            dgvPagamento.Columns["QtdVendas"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvPagamento.Columns["TotalPago"].HeaderText = "Total";
            dgvPagamento.Columns["TotalPago"].DefaultCellStyle.Format = "C2";
            dgvPagamento.Columns["TotalPago"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void CarregarProdutosMaisVendidos(SqlConnection conn, DateTime inicio, DateTime fim, string forma)
        {
            string query = @"
                SELECT 
                    p.Descricao AS Produto,
                    SUM(i.Quantidade) AS QtdVendida,
                    SUM(i.Subtotal) AS TotalGerado
                FROM tbitensvenda i
                INNER JOIN tbvendas v ON i.VendaID = v.VendaID
                INNER JOIN tbprodutos p ON i.ProdutoID = p.ProdutoID
                WHERE v.DataVenda BETWEEN @Inicio AND @Fim";

            if (forma != "Todas")
            {
                query += " AND v.FormaPagamento = @FormaPagamento";
            }

            query += @"
                GROUP BY p.Descricao
                ORDER BY QtdVendida DESC, TotalGerado DESC";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Inicio", inicio);
                cmd.Parameters.AddWithValue("@Fim", fim);
                if (forma != "Todas")
                {
                    cmd.Parameters.AddWithValue("@FormaPagamento", forma);
                }

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvProdutos.DataSource = dt;
                FormatarGridProdutos();
            }
        }

        private void FormatarGridProdutos()
        {
            if (dgvProdutos.Columns.Count == 0) return;

            dgvProdutos.Columns["Produto"].HeaderText = "Produto";

            dgvProdutos.Columns["QtdVendida"].HeaderText = "Qtd. Vendida";
            dgvProdutos.Columns["QtdVendida"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvProdutos.Columns["TotalGerado"].HeaderText = "Total Gerado";
            dgvProdutos.Columns["TotalGerado"].DefaultCellStyle.Format = "C2";
            dgvProdutos.Columns["TotalGerado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void CarregarTotalPeriodo(SqlConnection conn, DateTime inicio, DateTime fim, string forma)
        {
            string query = @"
                SELECT ISNULL(SUM(ValorTotal), 0)
                FROM tbvendas
                WHERE DataVenda BETWEEN @Inicio AND @Fim";

            if (forma != "Todas")
            {
                query += " AND FormaPagamento = @FormaPagamento";
            }

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Inicio", inicio);
                cmd.Parameters.AddWithValue("@Fim", fim);
                if (forma != "Todas")
                {
                    cmd.Parameters.AddWithValue("@FormaPagamento", forma);
                }

                object resultado = cmd.ExecuteScalar();
                decimal total = (resultado != null && resultado != DBNull.Value)
                    ? Convert.ToDecimal(resultado)
                    : 0m;

                lblTotalPeriodo.Text = total.ToString("C2");
            }
        }
    }
}