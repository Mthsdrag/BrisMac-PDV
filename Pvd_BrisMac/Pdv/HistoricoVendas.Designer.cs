namespace Pvd_BrisMac
{
    partial class HistoricoVendas
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
            this.dtpDataInicio = new System.Windows.Forms.DateTimePicker();
            this.dtpDataFim = new System.Windows.Forms.DateTimePicker();
            this.cmbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.lblTotalPeriodo = new System.Windows.Forms.Label();
            this.dgvVendas = new System.Windows.Forms.DataGridView();
            this.dgvItensVenda = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBusca = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEmitirNfce = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensVenda)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpDataInicio
            // 
            this.dtpDataInicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.dtpDataInicio.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataInicio.Location = new System.Drawing.Point(221, 120);
            this.dtpDataInicio.Name = "dtpDataInicio";
            this.dtpDataInicio.Size = new System.Drawing.Size(125, 27);
            this.dtpDataInicio.TabIndex = 0;
            // 
            // dtpDataFim
            // 
            this.dtpDataFim.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.dtpDataFim.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataFim.Location = new System.Drawing.Point(375, 120);
            this.dtpDataFim.Name = "dtpDataFim";
            this.dtpDataFim.Size = new System.Drawing.Size(125, 27);
            this.dtpDataFim.TabIndex = 1;
            // 
            // cmbFormaPagamento
            // 
            this.cmbFormaPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.cmbFormaPagamento.FormattingEnabled = true;
            this.cmbFormaPagamento.Location = new System.Drawing.Point(511, 117);
            this.cmbFormaPagamento.Name = "cmbFormaPagamento";
            this.cmbFormaPagamento.Size = new System.Drawing.Size(172, 30);
            this.cmbFormaPagamento.TabIndex = 2;
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.btnFiltrar.ForeColor = System.Drawing.Color.White;
            this.btnFiltrar.Location = new System.Drawing.Point(907, 107);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(132, 40);
            this.btnFiltrar.TabIndex = 3;
            this.btnFiltrar.Text = "Buscar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // lblTotalPeriodo
            // 
            this.lblTotalPeriodo.AutoSize = true;
            this.lblTotalPeriodo.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalPeriodo.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalPeriodo.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblTotalPeriodo.Location = new System.Drawing.Point(655, 567);
            this.lblTotalPeriodo.Name = "lblTotalPeriodo";
            this.lblTotalPeriodo.Size = new System.Drawing.Size(171, 47);
            this.lblTotalPeriodo.TabIndex = 4;
            this.lblTotalPeriodo.Text = "R$ 0,00";
            // 
            // dgvVendas
            // 
            this.dgvVendas.AllowUserToAddRows = false;
            this.dgvVendas.AllowUserToDeleteRows = false;
            this.dgvVendas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVendas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVendas.Location = new System.Drawing.Point(60, 252);
            this.dgvVendas.Name = "dgvVendas";
            this.dgvVendas.ReadOnly = true;
            this.dgvVendas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVendas.Size = new System.Drawing.Size(558, 293);
            this.dgvVendas.TabIndex = 5;
            this.dgvVendas.SelectionChanged += new System.EventHandler(this.dgvVendas_SelectionChanged);
            // 
            // dgvItensVenda
            // 
            this.dgvItensVenda.AllowUserToAddRows = false;
            this.dgvItensVenda.AllowUserToDeleteRows = false;
            this.dgvItensVenda.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItensVenda.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItensVenda.Location = new System.Drawing.Point(637, 252);
            this.dgvItensVenda.Name = "dgvItensVenda";
            this.dgvItensVenda.ReadOnly = true;
            this.dgvItensVenda.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItensVenda.Size = new System.Drawing.Size(558, 293);
            this.dgvItensVenda.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.Location = new System.Drawing.Point(216, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Data início";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label2.Location = new System.Drawing.Point(370, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Data Fim";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label3.Location = new System.Drawing.Point(506, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Pagamento";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label4.Location = new System.Drawing.Point(217, 208);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(198, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "Vendas Realizadas";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label5.Location = new System.Drawing.Point(499, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(257, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "Histórico de Vendas";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label6.Location = new System.Drawing.Point(777, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(281, 25);
            this.label6.TabIndex = 12;
            this.label6.Text = "Itens da Venda Selecionada";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label7.Location = new System.Drawing.Point(429, 578);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 31);
            this.label7.TabIndex = 13;
            this.label7.Text = "Total no Período:";
            // 
            // txtBusca
            // 
            this.txtBusca.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.txtBusca.Location = new System.Drawing.Point(689, 117);
            this.txtBusca.Name = "txtBusca";
            this.txtBusca.Size = new System.Drawing.Size(203, 27);
            this.txtBusca.TabIndex = 14;
            this.txtBusca.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusca_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label8.Location = new System.Drawing.Point(686, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(188, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "Produto / Nº / CPF";
            // 
            // btnEmitirNfce
            // 
            this.btnEmitirNfce.BackColor = System.Drawing.Color.LimeGreen;
            this.btnEmitirNfce.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.btnEmitirNfce.ForeColor = System.Drawing.Color.White;
            this.btnEmitirNfce.Location = new System.Drawing.Point(60, 551);
            this.btnEmitirNfce.Name = "btnEmitirNfce";
            this.btnEmitirNfce.Size = new System.Drawing.Size(156, 35);
            this.btnEmitirNfce.TabIndex = 16;
            this.btnEmitirNfce.Text = "Emitir / 2ª Via NFC-e";
            this.btnEmitirNfce.UseVisualStyleBackColor = false;
            this.btnEmitirNfce.Click += new System.EventHandler(this.btnEmitirNfce_Click);
            // 
            // HistoricoVendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Pvd_BrisMac.Properties.Resources.BackgorundBrisMAc;
            this.ClientSize = new System.Drawing.Size(1254, 671);
            this.Controls.Add(this.btnEmitirNfce);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBusca);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvItensVenda);
            this.Controls.Add(this.dgvVendas);
            this.Controls.Add(this.lblTotalPeriodo);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.cmbFormaPagamento);
            this.Controls.Add(this.dtpDataFim);
            this.Controls.Add(this.dtpDataInicio);
            this.Name = "HistoricoVendas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrisMac PDV - Histórico de Vendas";
            this.Load += new System.EventHandler(this.HistoricoVendas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItensVenda)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpDataInicio;
        private System.Windows.Forms.DateTimePicker dtpDataFim;
        private System.Windows.Forms.ComboBox cmbFormaPagamento;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.Label lblTotalPeriodo;
        private System.Windows.Forms.DataGridView dgvVendas;
        private System.Windows.Forms.DataGridView dgvItensVenda;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBusca;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnEmitirNfce;
    }
}