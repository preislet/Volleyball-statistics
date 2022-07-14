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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label_domaci = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textBox_Domaci = new System.Windows.Forms.TextBox();
            this.textBox_Hoste = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_VybranaSestava = new System.Windows.Forms.Label();
            this.Button_NovaSestava = new System.Windows.Forms.Button();
            this.button_sestava = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_domaci
            // 
            this.label_domaci.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_domaci.AutoSize = true;
            this.label_domaci.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_domaci.Location = new System.Drawing.Point(12, 7);
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
            this.label2.Location = new System.Drawing.Point(23, 61);
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
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_VybranaSestava, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(570, 164);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // textBox_Domaci
            // 
            this.textBox_Domaci.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Domaci.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Domaci.Location = new System.Drawing.Point(148, 4);
            this.textBox_Domaci.Name = "textBox_Domaci";
            this.textBox_Domaci.Size = new System.Drawing.Size(419, 46);
            this.textBox_Domaci.TabIndex = 2;
            this.textBox_Domaci.TextChanged += new System.EventHandler(this.textBox_Domaci_TextChanged);
            // 
            // textBox_Hoste
            // 
            this.textBox_Hoste.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_Hoste.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Hoste.Location = new System.Drawing.Point(148, 58);
            this.textBox_Hoste.Name = "textBox_Hoste";
            this.textBox_Hoste.Size = new System.Drawing.Size(419, 46);
            this.textBox_Hoste.TabIndex = 3;
            this.textBox_Hoste.TextChanged += new System.EventHandler(this.textBox_Hoste_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(13, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sestava:";
            // 
            // label_VybranaSestava
            // 
            this.label_VybranaSestava.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label_VybranaSestava.AutoSize = true;
            this.label_VybranaSestava.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_VybranaSestava.Location = new System.Drawing.Point(199, 116);
            this.label_VybranaSestava.Name = "label_VybranaSestava";
            this.label_VybranaSestava.Size = new System.Drawing.Size(317, 40);
            this.label_VybranaSestava.TabIndex = 5;
            this.label_VybranaSestava.Text = "Nebyla vybrána sestava\r\n";
            // 
            // Button_NovaSestava
            // 
            this.Button_NovaSestava.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Button_NovaSestava.Location = new System.Drawing.Point(347, 260);
            this.Button_NovaSestava.Name = "Button_NovaSestava";
            this.Button_NovaSestava.Size = new System.Drawing.Size(201, 61);
            this.Button_NovaSestava.TabIndex = 3;
            this.Button_NovaSestava.Text = "Nová Sestava";
            this.Button_NovaSestava.UseVisualStyleBackColor = true;
            this.Button_NovaSestava.Click += new System.EventHandler(this.Button_NovaSestava_Click);
            // 
            // button_sestava
            // 
            this.button_sestava.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button_sestava.Location = new System.Drawing.Point(32, 260);
            this.button_sestava.Name = "button_sestava";
            this.button_sestava.Size = new System.Drawing.Size(255, 61);
            this.button_sestava.TabIndex = 4;
            this.button_sestava.Text = "Vybrat Sestavu";
            this.button_sestava.UseVisualStyleBackColor = true;
            this.button_sestava.Click += new System.EventHandler(this.button_sestava_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.Location = new System.Drawing.Point(659, 377);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(259, 93);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_sestava);
            this.Controls.Add(this.Button_NovaSestava);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
        private Label label1;
        private Button button_sestava;
        private Label label_VybranaSestava;
        private Button button1;
    }
}