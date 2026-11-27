namespace Pvd_BrisMac
{
    partial class pdv
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pdv));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.txtQtd = new System.Windows.Forms.TextBox();
            this.lbCodigoBarras = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDescricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQtd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValorUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFinalizarVenda = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInfoEstoque = new System.Windows.Forms.Label();
            this.txtNomeProduto = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.txtCodigoBarras.Location = new System.Drawing.Point(670, 359);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(257, 31);
            this.txtCodigoBarras.TabIndex = 15;
            this.txtCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyDown);
            // 
            // txtQtd
            // 
            this.txtQtd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtQtd.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.txtQtd.Location = new System.Drawing.Point(670, 439);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(257, 31);
            this.txtQtd.TabIndex = 16;
            this.txtQtd.Text = "1";
            this.txtQtd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbCodigoBarras
            // 
            this.lbCodigoBarras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCodigoBarras.AutoSize = true;
            this.lbCodigoBarras.BackColor = System.Drawing.Color.Transparent;
            this.lbCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.lbCodigoBarras.ForeColor = System.Drawing.Color.White;
            this.lbCodigoBarras.Location = new System.Drawing.Point(711, 324);
            this.lbCodigoBarras.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCodigoBarras.Name = "lbCodigoBarras";
            this.lbCodigoBarras.Size = new System.Drawing.Size(179, 25);
            this.lbCodigoBarras.TabIndex = 18;
            this.lbCodigoBarras.Text = "Codigo de Barras";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(739, 411);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Quantidade";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colDescricao,
            this.colQtd,
            this.colValorUnit,
            this.colSubtotal});
            this.dataGridView1.Location = new System.Drawing.Point(12, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(577, 612);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // colCodigo
            // 
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.Name = "colCodigo";
            // 
            // colDescricao
            // 
            this.colDescricao.HeaderText = "Descrição do Produto";
            this.colDescricao.Name = "colDescricao";
            // 
            // colQtd
            // 
            this.colQtd.HeaderText = "Quantidade";
            this.colQtd.Name = "colQtd";
            // 
            // colValorUnit
            // 
            this.colValorUnit.HeaderText = "Valor Unit.";
            this.colValorUnit.Name = "colValorUnit";
            // 
            // colSubtotal
            // 
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.Name = "colSubtotal";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Location = new System.Drawing.Point(595, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(424, 144);
            this.panel1.TabIndex = 21;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 40.25F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblTotal.Location = new System.Drawing.Point(78, 30);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(272, 76);
            this.lblTotal.TabIndex = 22;
            this.lblTotal.Text = "R$ 0,00";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(726, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Pressione Enter para adicionar";
            // 
            // btnFinalizarVenda
            // 
            this.btnFinalizarVenda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinalizarVenda.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnFinalizarVenda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinalizarVenda.FlatAppearance.BorderSize = 0;
            this.btnFinalizarVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFinalizarVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalizarVenda.ForeColor = System.Drawing.Color.White;
            this.btnFinalizarVenda.Location = new System.Drawing.Point(608, 487);
            this.btnFinalizarVenda.Name = "btnFinalizarVenda";
            this.btnFinalizarVenda.Size = new System.Drawing.Size(386, 147);
            this.btnFinalizarVenda.TabIndex = 23;
            this.btnFinalizarVenda.Text = "Finalizar Venda";
            this.btnFinalizarVenda.UseVisualStyleBackColor = false;
            this.btnFinalizarVenda.Click += new System.EventHandler(this.btnFinalizarVenda_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(711, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(196, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Pressione F2 para pesquisar um produto";
            // 
            // lblInfoEstoque
            // 
            this.lblInfoEstoque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoEstoque.AutoSize = true;
            this.lblInfoEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.lblInfoEstoque.ForeColor = System.Drawing.Color.White;
            this.lblInfoEstoque.Location = new System.Drawing.Point(709, 244);
            this.lblInfoEstoque.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInfoEstoque.Name = "lblInfoEstoque";
            this.lblInfoEstoque.Size = new System.Drawing.Size(179, 25);
            this.lblInfoEstoque.TabIndex = 26;
            this.lblInfoEstoque.Text = "Nome do Produto";
            // 
            // txtNomeProduto
            // 
            this.txtNomeProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNomeProduto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtNomeProduto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtNomeProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.txtNomeProduto.Location = new System.Drawing.Point(670, 272);
            this.txtNomeProduto.Name = "txtNomeProduto";
            this.txtNomeProduto.Size = new System.Drawing.Size(257, 31);
            this.txtNomeProduto.TabIndex = 27;
            this.txtNomeProduto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNomeProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNomeProduto_KeyDown);
            // 
            // pdv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1031, 646);
            this.Controls.Add(this.txtNomeProduto);
            this.Controls.Add(this.lblInfoEstoque);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnFinalizarVenda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCodigoBarras);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.txtCodigoBarras);
            this.KeyPreview = true;
            this.Name = "pdv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrisMac PDV - Frente de Caixa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.pdv_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pdv_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.TextBox txtQtd;
        private System.Windows.Forms.Label lbCodigoBarras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDescricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQtd;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValorUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFinalizarVenda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInfoEstoque;
        private System.Windows.Forms.TextBox txtNomeProduto;
    }
}