namespace Pvd_BrisMac
{
    partial class FechamentoCaixa
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvResumoCaixa = new System.Windows.Forms.DataGridView();
            this.lblTotalDia = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.dtpDataCaixa = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnConfirmarFechamento = new System.Windows.Forms.Button();
            this.lblDiferenca = new System.Windows.Forms.Label();
            this.lblEsperadoGaveta = new System.Windows.Forms.Label();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.txtDinheiroContado = new System.Windows.Forms.TextBox();
            this.txtFundoTroco = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoCaixa)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label7.Location = new System.Drawing.Point(228, 597);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(269, 31);
            this.label7.TabIndex = 43;
            this.label7.Text = "Total Vendido no dia:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label6.Location = new System.Drawing.Point(653, 227);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(233, 25);
            this.label6.TabIndex = 42;
            this.label6.Text = "Conferência de Gaveta";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.label5.Location = new System.Drawing.Point(379, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(278, 31);
            this.label5.TabIndex = 41;
            this.label5.Text = "Fechamento de Caixa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label4.Location = new System.Drawing.Point(136, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 25);
            this.label4.TabIndex = 40;
            this.label4.Text = "Resumo do Sistema";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.Location = new System.Drawing.Point(444, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 25);
            this.label1.TabIndex = 37;
            this.label1.Text = "Data do Caixa";
            // 
            // dgvResumoCaixa
            // 
            this.dgvResumoCaixa.AllowUserToAddRows = false;
            this.dgvResumoCaixa.AllowUserToDeleteRows = false;
            this.dgvResumoCaixa.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResumoCaixa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumoCaixa.Location = new System.Drawing.Point(26, 271);
            this.dgvResumoCaixa.Name = "dgvResumoCaixa";
            this.dgvResumoCaixa.ReadOnly = true;
            this.dgvResumoCaixa.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResumoCaixa.Size = new System.Drawing.Size(457, 293);
            this.dgvResumoCaixa.TabIndex = 35;
            // 
            // lblTotalDia
            // 
            this.lblTotalDia.AutoSize = true;
            this.lblTotalDia.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalDia.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalDia.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblTotalDia.Location = new System.Drawing.Point(491, 586);
            this.lblTotalDia.Name = "lblTotalDia";
            this.lblTotalDia.Size = new System.Drawing.Size(171, 47);
            this.lblTotalDia.TabIndex = 34;
            this.lblTotalDia.Text = "R$ 0,00";
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(458, 164);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(125, 32);
            this.btnBuscar.TabIndex = 33;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // dtpDataCaixa
            // 
            this.dtpDataCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.dtpDataCaixa.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDataCaixa.Location = new System.Drawing.Point(458, 131);
            this.dtpDataCaixa.Name = "dtpDataCaixa";
            this.dtpDataCaixa.Size = new System.Drawing.Size(125, 27);
            this.dtpDataCaixa.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnConfirmarFechamento);
            this.panel1.Controls.Add(this.lblDiferenca);
            this.panel1.Controls.Add(this.lblEsperadoGaveta);
            this.panel1.Controls.Add(this.txtObservacao);
            this.panel1.Controls.Add(this.txtDinheiroContado);
            this.panel1.Controls.Add(this.txtFundoTroco);
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(553, 271);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(457, 293);
            this.panel1.TabIndex = 44;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(45, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 18);
            this.label10.TabIndex = 55;
            this.label10.Text = "Diferença:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(45, 71);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 18);
            this.label9.TabIndex = 54;
            this.label9.Text = "Esperado na Gaveta:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(47, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 18);
            this.label8.TabIndex = 53;
            this.label8.Text = "Observações";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(262, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 18);
            this.label3.TabIndex = 52;
            this.label3.Text = "Dinheiro Contado (R$)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(47, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 18);
            this.label2.TabIndex = 51;
            this.label2.Text = "Fundo de troco (R$)";
            // 
            // btnConfirmarFechamento
            // 
            this.btnConfirmarFechamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(64)))));
            this.btnConfirmarFechamento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarFechamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarFechamento.Location = new System.Drawing.Point(50, 233);
            this.btnConfirmarFechamento.Name = "btnConfirmarFechamento";
            this.btnConfirmarFechamento.Size = new System.Drawing.Size(353, 40);
            this.btnConfirmarFechamento.TabIndex = 50;
            this.btnConfirmarFechamento.Text = "Confirmar Fechamento";
            this.btnConfirmarFechamento.UseVisualStyleBackColor = false;
            this.btnConfirmarFechamento.Click += new System.EventHandler(this.btnConfirmarFechamento_Click);
            // 
            // lblDiferenca
            // 
            this.lblDiferenca.AutoSize = true;
            this.lblDiferenca.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiferenca.ForeColor = System.Drawing.Color.Black;
            this.lblDiferenca.Location = new System.Drawing.Point(339, 101);
            this.lblDiferenca.Name = "lblDiferenca";
            this.lblDiferenca.Size = new System.Drawing.Size(80, 24);
            this.lblDiferenca.TabIndex = 49;
            this.lblDiferenca.Text = "R$ 0,00";
            // 
            // lblEsperadoGaveta
            // 
            this.lblEsperadoGaveta.AutoSize = true;
            this.lblEsperadoGaveta.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEsperadoGaveta.ForeColor = System.Drawing.Color.Black;
            this.lblEsperadoGaveta.Location = new System.Drawing.Point(339, 71);
            this.lblEsperadoGaveta.Name = "lblEsperadoGaveta";
            this.lblEsperadoGaveta.Size = new System.Drawing.Size(80, 24);
            this.lblEsperadoGaveta.TabIndex = 48;
            this.lblEsperadoGaveta.Text = "R$ 0,00";
            // 
            // txtObservacao
            // 
            this.txtObservacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtObservacao.Location = new System.Drawing.Point(50, 156);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(369, 50);
            this.txtObservacao.TabIndex = 47;
            // 
            // txtDinheiroContado
            // 
            this.txtDinheiroContado.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtDinheiroContado.Location = new System.Drawing.Point(265, 30);
            this.txtDinheiroContado.Name = "txtDinheiroContado";
            this.txtDinheiroContado.Size = new System.Drawing.Size(154, 24);
            this.txtDinheiroContado.TabIndex = 46;
            this.txtDinheiroContado.Text = "0,00";
            this.txtDinheiroContado.TextChanged += new System.EventHandler(this.CalcularDiferenca_TextChanged);
            // 
            // txtFundoTroco
            // 
            this.txtFundoTroco.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtFundoTroco.Location = new System.Drawing.Point(50, 34);
            this.txtFundoTroco.Name = "txtFundoTroco";
            this.txtFundoTroco.Size = new System.Drawing.Size(139, 24);
            this.txtFundoTroco.TabIndex = 45;
            this.txtFundoTroco.Text = "0,00";
            this.txtFundoTroco.TextChanged += new System.EventHandler(this.CalcularDiferenca_TextChanged);
            // 
            // FechamentoCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Pvd_BrisMac.Properties.Resources.BackgorundBrisMAc;
            this.ClientSize = new System.Drawing.Size(1036, 671);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvResumoCaixa);
            this.Controls.Add(this.lblTotalDia);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dtpDataCaixa);
            this.Controls.Add(this.panel1);
            this.Name = "FechamentoCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrisMac PDV - Fechamento de Caixa";
            this.Load += new System.EventHandler(this.FechamentoCaixa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoCaixa)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvResumoCaixa;
        private System.Windows.Forms.Label lblTotalDia;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DateTimePicker dtpDataCaixa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.TextBox txtDinheiroContado;
        private System.Windows.Forms.TextBox txtFundoTroco;
        private System.Windows.Forms.Label lblEsperadoGaveta;
        private System.Windows.Forms.Button btnConfirmarFechamento;
        private System.Windows.Forms.Label lblDiferenca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}