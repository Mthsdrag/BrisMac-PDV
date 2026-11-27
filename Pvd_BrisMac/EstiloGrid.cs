using System.Drawing;
using System.Windows.Forms;

namespace Pvd_BrisMac
{
    public static class EstiloGrid
    {
        public static void AplicarNoForm(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is DataGridView dgv)
                {
                    Aplicar(dgv);
                }
                else if (c.HasChildren)
                {
                    AplicarNoForm(c);
                }
            }
        }

        public static void Aplicar(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.FixedSingle;
            dgv.BackgroundColor = Color.White;
            dgv.GridColor = Color.Silver;

            // Tira a coluna da setinha azul na esquerda (ganha espaço e limpa o visual)
            dgv.RowHeadersVisible = false;

            // Seleção da linha inteira e sem edição direta
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;

            // Cabeçalho (cinza escuro com texto branco em negrito)
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(90, 90, 90);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(90, 90, 90);
            dgv.ColumnHeadersHeight = 30;

            // Verde suave e confortável para a linha selecionada
            Color verdeSuave = Color.FromArgb(209, 242, 215);

            // Linhas normais (branco) e seleção (verde suave com texto preto)
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = verdeSuave;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            // Linhas alternadas (efeito zebrado cinza claro)
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 224, 224);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = verdeSuave;
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.Black;
        }
    }
}