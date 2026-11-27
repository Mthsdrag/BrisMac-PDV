namespace Pvd_BrisMac
{
    partial class EditarProdutos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditarProdutos));
            this.btnCadastrarProduto = new System.Windows.Forms.Button();
            this.lbCodigoBarras = new System.Windows.Forms.Label();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.lbPrecoProduto = new System.Windows.Forms.Label();
            this.lbDescriProduto = new System.Windows.Forms.Label();
            this.txtPrecoProduto = new System.Windows.Forms.TextBox();
            this.txtDescriProduto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAplicacaoMotos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLocalizacao = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCadastrarProduto
            // 
            this.btnCadastrarProduto.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCadastrarProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrarProduto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrarProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnCadastrarProduto.ForeColor = System.Drawing.Color.White;
            this.btnCadastrarProduto.Location = new System.Drawing.Point(348, 628);
            this.btnCadastrarProduto.Name = "btnCadastrarProduto";
            this.btnCadastrarProduto.Size = new System.Drawing.Size(259, 52);
            this.btnCadastrarProduto.TabIndex = 22;
            this.btnCadastrarProduto.Text = "Salvar";
            this.btnCadastrarProduto.UseVisualStyleBackColor = false;
            this.btnCadastrarProduto.Click += new System.EventHandler(this.btnCadastrarProduto_Click);
            // 
            // lbCodigoBarras
            // 
            this.lbCodigoBarras.AutoSize = true;
            this.lbCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbCodigoBarras.Location = new System.Drawing.Point(351, 301);
            this.lbCodigoBarras.Name = "lbCodigoBarras";
            this.lbCodigoBarras.Size = new System.Drawing.Size(140, 20);
            this.lbCodigoBarras.TabIndex = 19;
            this.lbCodigoBarras.Text = "Codigo de Barras";
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtCodigoBarras.Location = new System.Drawing.Point(355, 343);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(257, 23);
            this.txtCodigoBarras.TabIndex = 18;
            this.txtCodigoBarras.TextChanged += new System.EventHandler(this.txtCodigoBarras_TextChanged);
            // 
            // lbPrecoProduto
            // 
            this.lbPrecoProduto.AutoSize = true;
            this.lbPrecoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbPrecoProduto.Location = new System.Drawing.Point(354, 214);
            this.lbPrecoProduto.Name = "lbPrecoProduto";
            this.lbPrecoProduto.Size = new System.Drawing.Size(139, 20);
            this.lbPrecoProduto.TabIndex = 17;
            this.lbPrecoProduto.Text = "Preço do Produto";
            // 
            // lbDescriProduto
            // 
            this.lbDescriProduto.AutoSize = true;
            this.lbDescriProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.lbDescriProduto.Location = new System.Drawing.Point(353, 127);
            this.lbDescriProduto.Name = "lbDescriProduto";
            this.lbDescriProduto.Size = new System.Drawing.Size(139, 20);
            this.lbDescriProduto.TabIndex = 16;
            this.lbDescriProduto.Text = "Nome do Produto";
            // 
            // txtPrecoProduto
            // 
            this.txtPrecoProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtPrecoProduto.Location = new System.Drawing.Point(355, 256);
            this.txtPrecoProduto.Name = "txtPrecoProduto";
            this.txtPrecoProduto.Size = new System.Drawing.Size(257, 23);
            this.txtPrecoProduto.TabIndex = 15;
            // 
            // txtDescriProduto
            // 
            this.txtDescriProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtDescriProduto.Location = new System.Drawing.Point(359, 169);
            this.txtDescriProduto.Name = "txtDescriProduto";
            this.txtDescriProduto.Size = new System.Drawing.Size(257, 23);
            this.txtDescriProduto.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.Location = new System.Drawing.Point(354, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 23;
            this.label1.Text = "Edição de Produtos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.Location = new System.Drawing.Point(350, 465);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "Motos Compatíveis";
            // 
            // txtAplicacaoMotos
            // 
            this.txtAplicacaoMotos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtAplicacaoMotos.Location = new System.Drawing.Point(350, 505);
            this.txtAplicacaoMotos.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAplicacaoMotos.Multiline = true;
            this.txtAplicacaoMotos.Name = "txtAplicacaoMotos";
            this.txtAplicacaoMotos.Size = new System.Drawing.Size(257, 101);
            this.txtAplicacaoMotos.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.Location = new System.Drawing.Point(351, 382);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Localização";
            // 
            // txtLocalizacao
            // 
            this.txtLocalizacao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocalizacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtLocalizacao.Location = new System.Drawing.Point(351, 422);
            this.txtLocalizacao.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtLocalizacao.Name = "txtLocalizacao";
            this.txtLocalizacao.Size = new System.Drawing.Size(257, 23);
            this.txtLocalizacao.TabIndex = 32;
            // 
            // EditarProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(966, 749);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAplicacaoMotos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLocalizacao);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCadastrarProduto);
            this.Controls.Add(this.lbCodigoBarras);
            this.Controls.Add(this.txtCodigoBarras);
            this.Controls.Add(this.lbPrecoProduto);
            this.Controls.Add(this.lbDescriProduto);
            this.Controls.Add(this.txtPrecoProduto);
            this.Controls.Add(this.txtDescriProduto);
            this.Name = "EditarProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BrisMac PDV - Editar Produto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCadastrarProduto;
        private System.Windows.Forms.Label lbCodigoBarras;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.Label lbPrecoProduto;
        private System.Windows.Forms.Label lbDescriProduto;
        private System.Windows.Forms.TextBox txtPrecoProduto;
        private System.Windows.Forms.TextBox txtDescriProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAplicacaoMotos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLocalizacao;
    }
}