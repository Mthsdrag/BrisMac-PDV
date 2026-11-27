using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public partial class HistoricoVendas : Form
    {
        private readonly string connectionString =
            @"Server=.\SQLEXPRESS;Database=Pvd_BrisMac;Trusted_Connection=True;TrustServerCertificate=True;";

        public HistoricoVendas()
        {
            InitializeComponent();
        }

        private void HistoricoVendas_Load(object sender, EventArgs e)
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

            CarregarVendas();
            EstiloGrid.AplicarNoForm(this);
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CarregarVendas();
        }

        private void txtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                CarregarVendas();
            }
        }

        private void CarregarVendas()
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
            string termoBusca = txtBusca.Text.Trim();

            string query = @"
                SELECT 
                    VendaID,
                    DataVenda,
                    FormaPagamento,
                    CASE 
                        WHEN CPFCliente IS NULL OR LTRIM(RTRIM(CPFCliente)) = '' THEN '---'
                        ELSE CPFCliente
                    END AS CPFCliente,
                    ValorTotal,
                    CASE 
                        WHEN StatusFiscal IS NULL OR LTRIM(RTRIM(StatusFiscal)) = '' THEN 'Sem NFC-e'
                        ELSE StatusFiscal
                    END AS StatusFiscal,
                    CASE 
                        WHEN NumeroNFCe IS NULL OR NumeroNFCe = 0 THEN '---'
                        ELSE CAST(NumeroNFCe AS VARCHAR(20))
                    END AS NumeroNFCe,
                    ISNULL(IdNuvemFiscal, '') AS IdNuvemFiscal
                FROM tbvendas
                WHERE DataVenda BETWEEN @Inicio AND @Fim";

            if (forma != "Todas")
            {
                query += " AND FormaPagamento = @FormaPagamento";
            }

            if (!string.IsNullOrEmpty(termoBusca))
            {
                query += @" AND (
                                CAST(VendaID AS VARCHAR) = @BuscaExata
                                OR CPFCliente LIKE @Busca
                                OR EXISTS (
                                    SELECT 1 
                                    FROM tbitensvenda i
                                    INNER JOIN tbprodutos p ON i.ProdutoID = p.ProdutoID
                                    WHERE i.VendaID = tbvendas.VendaID
                                      AND p.Descricao LIKE @Busca
                                )
                            )";
            }

            query += " ORDER BY DataVenda DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Inicio", inicio);
                    cmd.Parameters.AddWithValue("@Fim", fim);

                    if (forma != "Todas")
                    {
                        cmd.Parameters.AddWithValue("@FormaPagamento", forma);
                    }

                    if (!string.IsNullOrEmpty(termoBusca))
                    {
                        cmd.Parameters.AddWithValue("@BuscaExata", termoBusca);
                        cmd.Parameters.AddWithValue("@Busca", "%" + termoBusca + "%");
                    }

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvVendas.DataSource = dt;
                    FormatarGridVendas();

                    decimal soma = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["ValorTotal"] != DBNull.Value)
                            soma += Convert.ToDecimal(row["ValorTotal"]);
                    }
                    lblTotalPeriodo.Text = soma.ToString("C2");
                }

                if (dgvVendas.Rows.Count > 0 && !dgvVendas.Rows[0].IsNewRow)
                {
                    int primeiraVendaId = Convert.ToInt32(dgvVendas.Rows[0].Cells["VendaID"].Value);
                    CarregarItensVenda(primeiraVendaId);
                    AtualizarTextoBotaoNfce();
                }
                else
                {
                    dgvItensVenda.DataSource = null;
                    btnEmitirNfce.Text = "Emitir / 2ª Via NFC-e";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar histórico: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatarGridVendas()
        {
            if (dgvVendas.Columns.Count == 0) return;

            dgvVendas.Columns["VendaID"].HeaderText = "Nº Venda";
            dgvVendas.Columns["VendaID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVendas.Columns["DataVenda"].HeaderText = "Data / Hora";
            dgvVendas.Columns["DataVenda"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            dgvVendas.Columns["FormaPagamento"].HeaderText = "Pagamento";

            dgvVendas.Columns["CPFCliente"].HeaderText = "CPF";
            dgvVendas.Columns["CPFCliente"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVendas.Columns["CPFCliente"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVendas.Columns["ValorTotal"].HeaderText = "Total";
            dgvVendas.Columns["ValorTotal"].DefaultCellStyle.Format = "C2";
            dgvVendas.Columns["ValorTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvVendas.Columns["StatusFiscal"].HeaderText = "Status NFC-e";
            dgvVendas.Columns["StatusFiscal"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVendas.Columns["StatusFiscal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvVendas.Columns["NumeroNFCe"].HeaderText = "Nº NFC-e";
            dgvVendas.Columns["NumeroNFCe"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvVendas.Columns["NumeroNFCe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Coluna técnica usada apenas para abrir o PDF da 2ª via
            dgvVendas.Columns["IdNuvemFiscal"].Visible = false;
        }

        private void dgvVendas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVendas.CurrentRow == null || dgvVendas.CurrentRow.IsNewRow) return;
            if (dgvVendas.CurrentRow.Cells["VendaID"].Value == null) return;

            if (int.TryParse(dgvVendas.CurrentRow.Cells["VendaID"].Value.ToString(), out int vendaId))
            {
                CarregarItensVenda(vendaId);
            }

            AtualizarTextoBotaoNfce();
        }

        private void AtualizarTextoBotaoNfce()
        {
            if (dgvVendas.CurrentRow == null || dgvVendas.CurrentRow.IsNewRow || !dgvVendas.Columns.Contains("StatusFiscal"))
                return;

            string status = dgvVendas.CurrentRow.Cells["StatusFiscal"].Value?.ToString() ?? "";

            if (status.Equals("Autorizada", StringComparison.OrdinalIgnoreCase))
                btnEmitirNfce.Text = "2ª Via NFC-e (PDF)";
            else if (status.Equals("Rejeitada", StringComparison.OrdinalIgnoreCase) || status.Equals("Erro", StringComparison.OrdinalIgnoreCase))
                btnEmitirNfce.Text = "Reemitir NFC-e";
            else
                btnEmitirNfce.Text = "Emitir NFC-e";
        }

        private async void btnEmitirNfce_Click(object sender, EventArgs e)
        {
            if (dgvVendas.CurrentRow == null || dgvVendas.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Selecione uma venda na lista primeiro.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int vendaId = Convert.ToInt32(dgvVendas.CurrentRow.Cells["VendaID"].Value);
            string statusFiscal = dgvVendas.CurrentRow.Cells["StatusFiscal"].Value?.ToString() ?? "";
            string numeroNfce = dgvVendas.CurrentRow.Cells["NumeroNFCe"].Value?.ToString() ?? "";
            string idNuvemFiscal = dgvVendas.CurrentRow.Cells["IdNuvemFiscal"].Value?.ToString() ?? "";
            string cpfAtual = dgvVendas.CurrentRow.Cells["CPFCliente"].Value?.ToString() ?? "---";

            // 1. Trava contra duplicidade + opção de 2ª via para notas já autorizadas
            if (statusFiscal.Equals("Autorizada", StringComparison.OrdinalIgnoreCase))
            {
                DialogResult respPdf = MessageBox.Show(
                    $"A venda #{vendaId} já possui a NFC-e nº {numeroNfce} autorizada (não é possível emitir novamente).\n\nDeseja abrir a 2ª via do cupom (PDF)?",
                    "NFC-e Já Autorizada",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (respPdf == DialogResult.Yes)
                {
                    if (string.IsNullOrWhiteSpace(idNuvemFiscal))
                    {
                        MessageBox.Show("ID da nota não encontrado para gerar a 2ª via.", "Aviso",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    this.Cursor = Cursors.WaitCursor;
                    this.Enabled = false;
                    try
                    {
                        await NuvemFiscalService.AbrirPdfNfceAsync(idNuvemFiscal);
                    }
                    catch (Exception exPdf)
                    {
                        MessageBox.Show("Erro ao abrir 2ª via do PDF: " + exPdf.Message, "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        this.Enabled = true;
                        this.Cursor = Cursors.Default;
                    }
                }
                return;
            }

            // 2. Abre pop-up para conferir, corrigir, adicionar ou apagar o CPF/CNPJ antes de emitir
            if (!PedirCpfCnpj(vendaId, cpfAtual, out string novoCpf))
            {
                return;
            }

            AtualizarCpfVenda(vendaId, novoCpf);

            // 3. Emite ou reemite a NFC-e via NuvemFiscalService
            this.Cursor = Cursors.WaitCursor;
            this.Enabled = false;

            try
            {
                ResultadoNfce resultado = await NuvemFiscalService.EmitirNfceAsync(vendaId);

                if (resultado.Sucesso)
                {
                    MessageBox.Show(
                        $"NFC-e nº {resultado.Numero} autorizada com sucesso!",
                        "BrisMac PDV - Fiscal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    try
                    {
                        await NuvemFiscalService.AbrirPdfNfceAsync(resultado.IdNuvemFiscal);
                    }
                    catch (Exception exPdf)
                    {
                        MessageBox.Show("A nota foi autorizada, mas não foi possível abrir o PDF: " + exPdf.Message,
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(
                        $"Não foi possível emitir a NFC-e da venda #{vendaId}:\n\n{resultado.Mensagem}",
                        "Atenção - Emissão Fiscal",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            finally
            {
                this.Enabled = true;
                this.Cursor = Cursors.Default;
                CarregarVendas();
            }
        }

        private bool PedirCpfCnpj(int vendaId, string cpfAtual, out string novoCpf)
        {
            string cpfConfirmado = "";

            using (Form popup = new Form())
            {
                popup.Text = $"Emitir NFC-e - Venda #{vendaId}";
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;
                popup.ClientSize = new Size(430, 170);

                Label lblInstrucao = new Label
                {
                    Text = "CPF ou CNPJ na nota:\n(corrija ou deixe vazio para emitir sem CPF)",
                    Location = new Point(15, 14),
                    Size = new Size(400, 48),
                    Font = new Font("Segoe UI", 9.5f)
                };

                TextBox txtCpf = new TextBox
                {
                    Text = cpfAtual == "---" ? "" : cpfAtual,
                    Location = new Point(15, 68),
                    Size = new Size(400, 28),
                    MaxLength = 18,
                    Font = new Font("Segoe UI", 11f)
                };

                Button btnEmitir = new Button
                {
                    Text = "Emitir NFC-e",
                    Location = new Point(210, 115),
                    Size = new Size(115, 36),
                    BackColor = Color.FromArgb(40, 167, 69),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
                };

                Button btnCancelar = new Button
                {
                    Text = "Cancelar",
                    Location = new Point(330, 115),
                    Size = new Size(85, 36),
                    DialogResult = DialogResult.Cancel,
                    Font = new Font("Segoe UI", 9.5f)
                };

                btnEmitir.Click += (s, e) =>
                {
                    string apenasNumeros = new string(txtCpf.Text.Where(char.IsDigit).ToArray());

                    if (string.IsNullOrEmpty(apenasNumeros))
                    {
                        cpfConfirmado = "";
                        popup.DialogResult = DialogResult.OK;
                        popup.Close();
                        return;
                    }

                    if (apenasNumeros.Length == 11 && CpfValido(apenasNumeros))
                    {
                        cpfConfirmado = apenasNumeros;
                        popup.DialogResult = DialogResult.OK;
                        popup.Close();
                        return;
                    }

                    if (apenasNumeros.Length == 14 && CnpjValido(apenasNumeros))
                    {
                        cpfConfirmado = apenasNumeros;
                        popup.DialogResult = DialogResult.OK;
                        popup.Close();
                        return;
                    }

                    MessageBox.Show(
                        "CPF ou CNPJ inválido! Verifique os dígitos ou deixe em branco para emitir sem CPF.",
                        "Documento Inválido",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCpf.Focus();
                    txtCpf.SelectAll();
                };

                popup.Controls.Add(lblInstrucao);
                popup.Controls.Add(txtCpf);
                popup.Controls.Add(btnEmitir);
                popup.Controls.Add(btnCancelar);

                popup.AcceptButton = btnEmitir;
                popup.CancelButton = btnCancelar;
                popup.Shown += (s, e) =>
                {
                    txtCpf.Focus();
                    txtCpf.SelectAll();
                };

                bool confirmou = popup.ShowDialog(this) == DialogResult.OK;
                novoCpf = cpfConfirmado;
                return confirmou;
            }
        }

        private void AtualizarCpfVenda(int vendaId, string novoCpf)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tbvendas SET CPFCliente = @CPF WHERE VendaID = @VendaID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CPF", novoCpf ?? "");
                    cmd.Parameters.AddWithValue("@VendaID", vendaId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private bool CpfValido(string cpf)
        {
            if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return false;

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (cpf[i] - '0') * (10 - i);

            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;
            if (cpf[9] - '0' != dv1) return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (cpf[i] - '0') * (11 - i);

            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;
            return cpf[10] - '0' == dv2;
        }

        private bool CnpjValido(string cnpj)
        {
            if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1) return false;

            int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma = 0;
            for (int i = 0; i < 12; i++)
                soma += (cnpj[i] - '0') * mult1[i];

            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;
            if (cnpj[12] - '0' != dv1) return false;

            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += (cnpj[i] - '0') * mult2[i];

            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;
            return cnpj[13] - '0' == dv2;
        }

        private void CarregarItensVenda(int vendaId)
        {
            string query = @"
                SELECT p.Descricao, i.Quantidade, i.PrecoUnitario, i.Subtotal
                FROM tbitensvenda i
                INNER JOIN tbprodutos p ON i.ProdutoID = p.ProdutoID
                WHERE i.VendaID = @VendaID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@VendaID", vendaId);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvItensVenda.DataSource = dt;
                    FormatarGridItens();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar itens da venda: " + ex.Message, "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatarGridItens()
        {
            if (dgvItensVenda.Columns.Count == 0) return;

            dgvItensVenda.Columns["Descricao"].HeaderText = "Produto";

            dgvItensVenda.Columns["Quantidade"].HeaderText = "Qtd.";
            dgvItensVenda.Columns["Quantidade"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvItensVenda.Columns["PrecoUnitario"].HeaderText = "Valor Unit.";
            dgvItensVenda.Columns["PrecoUnitario"].DefaultCellStyle.Format = "C2";
            dgvItensVenda.Columns["PrecoUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvItensVenda.Columns["Subtotal"].HeaderText = "Subtotal";
            dgvItensVenda.Columns["Subtotal"].DefaultCellStyle.Format = "C2";
            dgvItensVenda.Columns["Subtotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}