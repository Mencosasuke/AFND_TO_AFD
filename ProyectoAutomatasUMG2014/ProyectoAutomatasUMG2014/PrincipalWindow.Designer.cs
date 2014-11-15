namespace ProyectoAutomatasUMG
{
    partial class PrincipalWindow
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtPathFile = new System.Windows.Forms.TextBox();
            this.txtAFND = new System.Windows.Forms.TextBox();
            this.txtAFD = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCadena = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCompCadena = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(313, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "Buscar";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtPathFile
            // 
            this.txtPathFile.BackColor = System.Drawing.Color.Snow;
            this.txtPathFile.Enabled = false;
            this.txtPathFile.Location = new System.Drawing.Point(12, 21);
            this.txtPathFile.Name = "txtPathFile";
            this.txtPathFile.Size = new System.Drawing.Size(295, 20);
            this.txtPathFile.TabIndex = 1;
            // 
            // txtAFND
            // 
            this.txtAFND.BackColor = System.Drawing.Color.Snow;
            this.txtAFND.Location = new System.Drawing.Point(12, 61);
            this.txtAFND.Multiline = true;
            this.txtAFND.Name = "txtAFND";
            this.txtAFND.ReadOnly = true;
            this.txtAFND.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAFND.Size = new System.Drawing.Size(295, 290);
            this.txtAFND.TabIndex = 2;
            // 
            // txtAFD
            // 
            this.txtAFD.BackColor = System.Drawing.Color.Snow;
            this.txtAFD.Location = new System.Drawing.Point(677, 61);
            this.txtAFD.Multiline = true;
            this.txtAFD.Name = "txtAFD";
            this.txtAFD.ReadOnly = true;
            this.txtAFD.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAFD.Size = new System.Drawing.Size(295, 290);
            this.txtAFD.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Snow;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(313, 61);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(358, 290);
            this.dataGridView1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Image = global::ProyectoAutomatasUMG.Properties.Resources.info_icon;
            this.label1.Location = new System.Drawing.Point(952, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 20);
            this.label1.TabIndex = 5;
            // 
            // txtCadena
            // 
            this.txtCadena.BackColor = System.Drawing.Color.Snow;
            this.txtCadena.Enabled = false;
            this.txtCadena.Location = new System.Drawing.Point(12, 390);
            this.txtCadena.Name = "txtCadena";
            this.txtCadena.Size = new System.Drawing.Size(295, 20);
            this.txtCadena.TabIndex = 6;
            this.txtCadena.TextChanged += new System.EventHandler(this.txtCadena_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(12, 365);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Comprobar cadena:";
            // 
            // btnCompCadena
            // 
            this.btnCompCadena.Location = new System.Drawing.Point(337, 388);
            this.btnCompCadena.Name = "btnCompCadena";
            this.btnCompCadena.Size = new System.Drawing.Size(75, 23);
            this.btnCompCadena.TabIndex = 8;
            this.btnCompCadena.Text = "Comprobar";
            this.btnCompCadena.UseVisualStyleBackColor = true;
            this.btnCompCadena.Click += new System.EventHandler(this.btnCompCadena_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.label3.Location = new System.Drawing.Point(389, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "© 2014 David Mencos, All rights reserved.";
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Location = new System.Drawing.Point(429, 393);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(0, 13);
            this.lblResultado.TabIndex = 10;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // PrincipalWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(984, 462);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCompCadena);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCadena);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtAFD);
            this.Controls.Add(this.txtAFND);
            this.Controls.Add(this.txtPathFile);
            this.Controls.Add(this.btnSearch);
            this.Name = "PrincipalWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Convertidor de AFN a AFD";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtPathFile;
        private System.Windows.Forms.TextBox txtAFND;
        private System.Windows.Forms.TextBox txtAFD;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TextBox txtCadena;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCompCadena;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}

