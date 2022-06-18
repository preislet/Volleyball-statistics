namespace Volleyball_statistics
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_domaci = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Domaci = new System.Windows.Forms.TextBox();
            this.textBox_Hoste = new System.Windows.Forms.TextBox();
            this.Button_NovaSestava = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_domaci
            // 
            this.label_domaci.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_domaci.AutoSize = true;
            this.label_domaci.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_domaci.Location = new System.Drawing.Point(12, 13);
            this.label_domaci.Name = "label_domaci";
            this.label_domaci.Size = new System.Drawing.Size(120, 40);
            this.label_domaci.TabIndex = 0;
            this.label_domaci.Text = "Domací:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(23, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hosté:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.4386F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.5614F));
            this.tableLayoutPanel1.Controls.Add(this.label_domaci, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Domaci, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_Hoste, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 135);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // textBox_Domaci
            // 
            this.textBox_Domaci.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Domaci.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Domaci.Location = new System.Drawing.Point(148, 10);
            this.textBox_Domaci.Name = "textBox_Domaci";
            this.textBox_Domaci.Size = new System.Drawing.Size(419, 46);
            this.textBox_Domaci.TabIndex = 2;
            // 
            // textBox_Hoste
            // 
            this.textBox_Hoste.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Hoste.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Hoste.Location = new System.Drawing.Point(148, 78);
            this.textBox_Hoste.Name = "textBox_Hoste";
            this.textBox_Hoste.Size = new System.Drawing.Size(419, 46);
            this.textBox_Hoste.TabIndex = 3;
            // 
            // Button_NovaSestava
            // 
            this.Button_NovaSestava.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Button_NovaSestava.Location = new System.Drawing.Point(622, 285);
            this.Button_NovaSestava.Name = "Button_NovaSestava";
            this.Button_NovaSestava.Size = new System.Drawing.Size(201, 61);
            this.Button_NovaSestava.TabIndex = 3;
            this.Button_NovaSestava.Text = "Nová Sestava";
            this.Button_NovaSestava.UseVisualStyleBackColor = true;
            this.Button_NovaSestava.Click += new System.EventHandler(this.Button_NovaSestava_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.Button_NovaSestava);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Volleyball";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Label label_domaci;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBox_Domaci;
        private TextBox textBox_Hoste;
        private Button Button_NovaSestava;
    }
}