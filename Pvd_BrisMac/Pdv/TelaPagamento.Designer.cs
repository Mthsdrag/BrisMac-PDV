namespace Pvd_BrisMac
{
    partial class TelaPagamento
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalPagar = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTroco = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.txtValorRecebido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.chkEmitirNfce = new System.Windows.Forms.CheckBox();
            this.txtCpfCliente = new System.Windows.Forms.TextBox();
            this.cbFormaPagamento = new System.Windows.Forms.ComboBox();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Navy;
            this.panel1.Controls.Add(this.lblTotalPagar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 116);
            this.panel1.TabIndex = 22;
            // 
            // lblTotalPagar
            // 
            this.lblTotalPagar.AutoSize = true;
            this.lblTotalPagar.Font = new System.Drawing.Font("Microsoft Sans Serif", 36.25F, System.Drawing.FontStyle.Bold);
            this.lblTotalPagar.ForeColor = System.Drawing.Color.Yellow;
            this.lblTotalPagar.Location = new System.Drawing.Point(299, 30);
            this.lblTotalPagar.Name = "lblTotalPagar";
            this.lblTotalPagar.Size = new System.Drawing.Size(202, 57);
            this.lblTotalPagar.TabIndex = 22;
            this.lblTotalPagar.Text = "R$ 0,00";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(116, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 25);
            this.label1.TabIndex = 27;
            this.label1.Text = "TOTAL A PAGAR";
            // 
            // lblTroco
            // 
            this.lblTroco.AutoSize = true;
            this.lblTroco.Font = new System.Drawing.Font("Microsoft Sans Serif", 30.25F, System.Drawing.FontStyle.Bold);
            this.lblTroco.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblTroco.Location = new System.Drawing.Point(315, 28);
            this.lblTroco.Name = "lblTroco";
            this.lblTroco.Size = new System.Drawing.Size(171, 47);
            this.lblTroco.TabIndex = 23;
            this.lblTroco.Text = "R$ 0,00";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // txtValorRecebido
            // 
            this.txtValorRecebido.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtValorRecebido.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F);
            this.txtValorRecebido.Location = new System.Drawing.Point(302, 52);
            this.txtValorRecebido.Name = "txtValorRecebido";
            this.txtValorRecebido.Size = new System.Drawing.Size(192, 56);
            this.txtValorRecebido.TabIndex = 24;
            this.txtValorRecebido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtValorRecebido.TextChanged += new System.EventHandler(this.txtValorRecebido_TextChanged);
            this.txtValorRecebido.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValorRecebido_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(297, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 25);
            this.label3.TabIndex = 27;
            this.label3.Text = "VALOR RECEBIDO";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.lblTroco);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 230);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 116);
            this.panel2.TabIndex = 28;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(208, 45);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 25);
            this.label4.TabIndex = 27;
            this.label4.Text = "TROCO";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.chkEmitirNfce);
            this.panel3.Controls.Add(this.txtCpfCliente);
            this.panel3.Controls.Add(this.cbFormaPagamento);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtValorRecebido);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 116);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 116);
            this.panel3.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(18, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 20);
            this.label2.TabIndex = 31;
            this.label2.Text = "CPF/CNPJ na nota (opcional)";
            // 
            // chkEmitirNfce
            // 
            this.chkEmitirNfce.AutoSize = true;
            this.chkEmitirNfce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.chkEmitirNfce.Location = new System.Drawing.Point(22, 6);
            this.chkEmitirNfce.Name = "chkEmitirNfce";
            this.chkEmitirNfce.Size = new System.Drawing.Size(246, 24);
            this.chkEmitirNfce.TabIndex = 30;
            this.chkEmitirNfce.Text = "Emitir NFC-e (Cupom Fiscal)";
            this.chkEmitirNfce.UseVisualStyleBackColor = true;
            this.chkEmitirNfce.CheckedChanged += new System.EventHandler(this.chkEmitirNfce_CheckedChanged);
            // 
            // txtCpfCliente
            // 
            this.txtCpfCliente.Enabled = false;
            this.txtCpfCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.txtCpfCliente.Location = new System.Drawing.Point(22, 70);
            this.txtCpfCliente.MaxLength = 18;
            this.txtCpfCliente.Name = "txtCpfCliente";
            this.txtCpfCliente.Size = new System.Drawing.Size(246, 35);
            this.txtCpfCliente.TabIndex = 29;
            this.txtCpfCliente.Tag = "";
            // 
            // cbFormaPagamento
            // 
            this.cbFormaPagamento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.cbFormaPagamento.FormattingEnabled = true;
            this.cbFormaPagamento.Items.AddRange(new object[] {
            "Dinheiro",
            "PIX",
            "Cartão de Crédito",
            "Cartão de Débito"});
            this.cbFormaPagamento.Location = new System.Drawing.Point(537, 70);
            this.cbFormaPagamento.Name = "cbFormaPagamento";
            this.cbFormaPagamento.Size = new System.Drawing.Size(221, 37);
            this.cbFormaPagamento.TabIndex = 28;
            this.cbFormaPagamento.SelectedIndexChanged += new System.EventHandler(this.cbFormaPagamento_SelectedIndexChanged_1);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnConfirmar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(0, 346);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(800, 116);
            this.btnConfirmar.TabIndex = 30;
            this.btnConfirmar.Text = "CONFIRMAR COMPRA (ENTER)";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // TelaPagamento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.BackgroundImage = global::Pvd_BrisMac.Properties.Resources.BackgorundBrisMAc;
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "TelaPagamento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrisMac PDV - Finalizar Venda";
            this.Load += new System.EventHandler(this.TelaPagamento_Load_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTotalPagar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox txtValorRecebido;
        private System.Windows.Forms.Label lblTroco;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.ComboBox cbFormaPagamento;
        private System.Windows.Forms.CheckBox chkEmitirNfce;
        private System.Windows.Forms.TextBox txtCpfCliente;
        private System.Windows.Forms.Label label2;
    }
}