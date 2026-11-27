namespace Pvd_BrisMac
{
    partial class CadastroUsuario
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lbCodigoBarras = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lbPrecoProduto = new System.Windows.Forms.Label();
            this.lbDescriProduto = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.cmbPerfil = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.Location = new System.Drawing.Point(351, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 25);
            this.label1.TabIndex = 41;
            this.label1.Text = "Cadastro de Usuários";
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSalvar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(345, 481);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(259, 52);
            this.btnSalvar.TabIndex = 40;
            this.btnSalvar.Text = "Cadastrar Usuário";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lbCodigoBarras
            // 
            this.lbCodigoBarras.AutoSize = true;
            this.lbCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbCodigoBarras.Location = new System.Drawing.Point(341, 305);
            this.lbCodigoBarras.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCodigoBarras.Name = "lbCodigoBarras";
            this.lbCodigoBarras.Size = new System.Drawing.Size(56, 20);
            this.lbCodigoBarras.TabIndex = 37;
            this.lbCodigoBarras.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtSenha.Location = new System.Drawing.Point(345, 344);
            this.txtSenha.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(257, 23);
            this.txtSenha.TabIndex = 36;
            this.txtSenha.UseSystemPasswordChar = true;
            // 
            // lbPrecoProduto
            // 
            this.lbPrecoProduto.AutoSize = true;
            this.lbPrecoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbPrecoProduto.Location = new System.Drawing.Point(344, 222);
            this.lbPrecoProduto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPrecoProduto.Name = "lbPrecoProduto";
            this.lbPrecoProduto.Size = new System.Drawing.Size(136, 20);
            this.lbPrecoProduto.TabIndex = 35;
            this.lbPrecoProduto.Text = "Login do Usuario";
            // 
            // lbDescriProduto
            // 
            this.lbDescriProduto.AutoSize = true;
            this.lbDescriProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbDescriProduto.Location = new System.Drawing.Point(343, 139);
            this.lbDescriProduto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescriProduto.Name = "lbDescriProduto";
            this.lbDescriProduto.Size = new System.Drawing.Size(126, 20);
            this.lbDescriProduto.TabIndex = 34;
            this.lbDescriProduto.Text = "Nome completo";
            // 
            // txtLogin
            // 
            this.txtLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtLogin.Location = new System.Drawing.Point(345, 262);
            this.txtLogin.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLogin.Name = "txtLogin";
            this.txtLogin.Size = new System.Drawing.Size(257, 23);
            this.txtLogin.TabIndex = 33;
            // 
            // txtNome
            // 
            this.txtNome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtNome.Location = new System.Drawing.Point(349, 179);
            this.txtNome.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(255, 23);
            this.txtNome.TabIndex = 32;
            // 
            // cmbPerfil
            // 
            this.cmbPerfil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPerfil.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.cmbPerfil.FormattingEnabled = true;
            this.cmbPerfil.Items.AddRange(new object[] {
            "Administrador",
            "Funcionário"});
            this.cmbPerfil.Location = new System.Drawing.Point(345, 425);
            this.cmbPerfil.Name = "cmbPerfil";
            this.cmbPerfil.Size = new System.Drawing.Size(260, 25);
            this.cmbPerfil.TabIndex = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.Location = new System.Drawing.Point(345, 386);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 20);
            this.label4.TabIndex = 48;
            this.label4.Text = "Perfil";
            // 
            // CadastroUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::Pvd_BrisMac.Properties.Resources.BackgorundBrisMAc;
            this.ClientSize = new System.Drawing.Size(946, 584);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbPerfil);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lbCodigoBarras);
            this.Controls.Add(this.txtSenha);
            this.Controls.Add(this.lbPrecoProduto);
            this.Controls.Add(this.lbDescriProduto);
            this.Controls.Add(this.txtLogin);
            this.Controls.Add(this.txtNome);
            this.Name = "CadastroUsuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CadastroUsuario";
            this.Load += new System.EventHandler(this.CadastroUsuario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lbCodigoBarras;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Label lbPrecoProduto;
        private System.Windows.Forms.Label lbDescriProduto;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.ComboBox cmbPerfil;
        private System.Windows.Forms.Label label4;
    }
}