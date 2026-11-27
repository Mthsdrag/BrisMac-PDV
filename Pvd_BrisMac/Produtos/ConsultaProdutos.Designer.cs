namespace Pvd_BrisMac
{
    partial class ConsultaProdutos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultaProdutos));
            this.txtPesquisaProdutos = new System.Windows.Forms.TextBox();
            this.btnPesquisarProduto = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnEditar = new System.Windows.Forms.Button();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnCadastrar = new System.Windows.Forms.Button();
            this.btnControle = new System.Windows.Forms.Button();
            this.dgvTabelaProdutos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabelaProdutos)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPesquisaProdutos
            // 
            this.txtPesquisaProdutos.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.txtPesquisaProdutos.Location = new System.Drawing.Point(281, 94);
            this.txtPesquisaProdutos.Name = "txtPesquisaProdutos";
            this.txtPesquisaProdutos.Size = new System.Drawing.Size(452, 23);
            this.txtPesquisaProdutos.TabIndex = 1;
            this.txtPesquisaProdutos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPesquisaProdutos_KeyDown);
            // 
            // btnPesquisarProduto
            // 
            this.btnPesquisarProduto.BackColor = System.Drawing.Color.LimeGreen;
            this.btnPesquisarProduto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPesquisarProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisarProduto.ForeColor = System.Drawing.Color.White;
            this.btnPesquisarProduto.Location = new System.Drawing.Point(739, 84);
            this.btnPesquisarProduto.Name = "btnPesquisarProduto";
            this.btnPesquisarProduto.Size = new System.Drawing.Size(107, 33);
            this.btnPesquisarProduto.TabIndex = 2;
            this.btnPesquisarProduto.Text = "Pesquisar";
            this.btnPesquisarProduto.UseVisualStyleBackColor = false;
            this.btnPesquisarProduto.Click += new System.EventHandler(this.btnPesquisarProduto_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.label1.Location = new System.Drawing.Point(398, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Consulta de Produtos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(350, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(315, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Digite o nome, código, moto ou prateleira";
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(618, 583);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(183, 44);
            this.btnEditar.TabIndex = 6;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Crimson;
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnExcluir.ForeColor = System.Drawing.Color.White;
            this.btnExcluir.Location = new System.Drawing.Point(416, 583);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(183, 44);
            this.btnExcluir.TabIndex = 7;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.BackColor = System.Drawing.Color.LimeGreen;
            this.btnCadastrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCadastrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnCadastrar.ForeColor = System.Drawing.Color.White;
            this.btnCadastrar.Location = new System.Drawing.Point(214, 583);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(183, 44);
            this.btnCadastrar.TabIndex = 9;
            this.btnCadastrar.Text = "Cadastrar";
            this.btnCadastrar.UseVisualStyleBackColor = false;
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // btnControle
            // 
            this.btnControle.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnControle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnControle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnControle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold);
            this.btnControle.ForeColor = System.Drawing.Color.White;
            this.btnControle.Location = new System.Drawing.Point(416, 633);
            this.btnControle.Name = "btnControle";
            this.btnControle.Size = new System.Drawing.Size(183, 44);
            this.btnControle.TabIndex = 10;
            this.btnControle.Text = "Controle de Estoque";
            this.btnControle.UseVisualStyleBackColor = false;
            this.btnControle.Click += new System.EventHandler(this.btnControle_Click);
            // 
            // dgvTabelaProdutos
            // 
            this.dgvTabelaProdutos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTabelaProdutos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTabelaProdutos.Location = new System.Drawing.Point(62, 137);
            this.dgvTabelaProdutos.Name = "dgvTabelaProdutos";
            this.dgvTabelaProdutos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTabelaProdutos.Size = new System.Drawing.Size(891, 440);
            this.dgvTabelaProdutos.TabIndex = 11;
            this.dgvTabelaProdutos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTabelaProdutos_CellDoubleClick);
            // 
            // ConsultaProdutos
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1015, 725);
            this.Controls.Add(this.dgvTabelaProdutos);
            this.Controls.Add(this.btnControle);
            this.Controls.Add(this.btnCadastrar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPesquisarProduto);
            this.Controls.Add(this.txtPesquisaProdutos);
            this.Name = "ConsultaProdutos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BrisMac PDV - Consulta de Produtos";
            this.Load += new System.EventHandler(this.PesquisaProdutos_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTabelaProdutos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.TextBox txtPesquisaProdutos;
        private System.Windows.Forms.Button btnPesquisarProduto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnEditar;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnCadastrar;
        private System.Windows.Forms.Button btnControle;
        private System.Windows.Forms.DataGridView dgvTabelaProdutos;
    }
}