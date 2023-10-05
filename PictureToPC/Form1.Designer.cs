namespace Forms
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
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            pictureBox1 = new PictureBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            checkBox2 = new CheckBox();
            groupBox3 = new GroupBox();
            groupBox5 = new GroupBox();
            button13 = new Button();
            label8 = new Label();
            label3 = new Label();
            button10 = new Button();
            button11 = new Button();
            groupBox2 = new GroupBox();
            connectionsTB = new TextBox();
            label9 = new Label();
            label4 = new Label();
            name_tb = new TextBox();
            label5 = new Label();
            label7 = new Label();
            label6 = new Label();
            progressBar1 = new ProgressBar();
            textBox1 = new TextBox();
            groupBox4 = new GroupBox();
            button14 = new Button();
            hideMenue_cb = new CheckBox();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            pictureCountTB = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            comboBox2.FormattingEnabled = true;
            comboBox2.ImeMode = ImeMode.NoControl;
            comboBox2.Items.AddRange(new object[] { "Low", "Med", "High" });
            comboBox2.Location = new Point(8, 55);
            comboBox2.Margin = new Padding(4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(86, 33);
            comboBox2.TabIndex = 16;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            comboBox1.FormattingEnabled = true;
            comboBox1.ImeMode = ImeMode.NoControl;
            comboBox1.Items.AddRange(new object[] { "Low", "Med", "High" });
            comboBox1.Location = new Point(106, 55);
            comboBox1.Margin = new Padding(4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(86, 33);
            comboBox1.TabIndex = 15;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.FromArgb(30, 30, 30);
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1832, 1012);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.Indigo;
            button1.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button1.Location = new Point(166, 58);
            button1.Margin = new Padding(4);
            button1.Name = "button1";
            button1.Size = new Size(162, 64);
            button1.TabIndex = 18;
            button1.Text = "Next";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.BackColor = Color.MediumSlateBlue;
            button2.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button2.Location = new Point(234, 106);
            button2.Margin = new Padding(4);
            button2.Name = "button2";
            button2.Size = new Size(110, 56);
            button2.TabIndex = 19;
            button2.Text = "Copy";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.BackColor = Color.MediumSlateBlue;
            button3.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button3.Location = new Point(102, 106);
            button3.Margin = new Padding(4);
            button3.Name = "button3";
            button3.Size = new Size(146, 56);
            button3.TabIndex = 20;
            button3.Text = "Contrast";
            button3.UseVisualStyleBackColor = false;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button4.BackColor = Color.MediumSlateBlue;
            button4.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button4.ForeColor = Color.WhiteSmoke;
            button4.Location = new Point(0, 106);
            button4.Margin = new Padding(4);
            button4.Name = "button4";
            button4.Size = new Size(112, 56);
            button4.TabIndex = 21;
            button4.Text = "Crop";
            button4.UseVisualStyleBackColor = false;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button5.BackColor = Color.Indigo;
            button5.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button5.Location = new Point(0, 57);
            button5.Margin = new Padding(4);
            button5.Name = "button5";
            button5.Size = new Size(344, 54);
            button5.TabIndex = 22;
            button5.Text = "Do all";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(106, 26);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(69, 25);
            label1.TabIndex = 23;
            label1.Text = "Output";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(8, 26);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(71, 25);
            label2.TabIndex = 24;
            label2.Text = "Internal";
            label2.Click += label2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.BackColor = Color.FromArgb(30, 30, 40);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox5);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Controls.Add(groupBox4);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(1452, 366);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(362, 628);
            groupBox1.TabIndex = 25;
            groupBox1.TabStop = false;
            groupBox1.Text = "Control Panel";
            // 
            // checkBox2
            // 
            checkBox2.Location = new Point(22, 412);
            checkBox2.Margin = new Padding(4);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(75, 28);
            checkBox2.TabIndex = 33;
            checkBox2.Text = "Beta";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox3.Controls.Add(comboBox1);
            groupBox3.Controls.Add(comboBox2);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(label1);
            groupBox3.ForeColor = Color.White;
            groupBox3.Location = new Point(152, 398);
            groupBox3.Margin = new Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4);
            groupBox3.Size = new Size(201, 96);
            groupBox3.TabIndex = 31;
            groupBox3.TabStop = false;
            groupBox3.Text = "Quality";
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox5.Controls.Add(pictureCountTB);
            groupBox5.Controls.Add(button13);
            groupBox5.Controls.Add(button1);
            groupBox5.Controls.Add(label8);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(button10);
            groupBox5.Controls.Add(button11);
            groupBox5.ForeColor = Color.White;
            groupBox5.Location = new Point(10, 201);
            groupBox5.Margin = new Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4);
            groupBox5.Size = new Size(344, 202);
            groupBox5.TabIndex = 32;
            groupBox5.TabStop = false;
            groupBox5.Text = "Selection Controls";
            // 
            // button13
            // 
            button13.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button13.BackColor = Color.Indigo;
            button13.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            button13.ForeColor = Color.White;
            button13.Location = new Point(15, 58);
            button13.Margin = new Padding(4);
            button13.Name = "button13";
            button13.Size = new Size(158, 64);
            button13.TabIndex = 29;
            button13.Text = "Prev";
            button13.UseVisualStyleBackColor = false;
            button13.Click += button13_Click;
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label8.AutoSize = true;
            label8.Location = new Point(141, 28);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(65, 25);
            label8.TabIndex = 28;
            label8.Text = "Picture";
            label8.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(22, 153);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(65, 25);
            label3.TabIndex = 27;
            label3.Text = "Corner";
            label3.TextAlign = ContentAlignment.BottomLeft;
            // 
            // button10
            // 
            button10.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button10.BackColor = Color.MediumSlateBlue;
            button10.Location = new Point(212, 141);
            button10.Margin = new Padding(4);
            button10.Name = "button10";
            button10.Size = new Size(117, 46);
            button10.TabIndex = 25;
            button10.Text = "Next";
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button11.BackColor = Color.MediumSlateBlue;
            button11.Location = new Point(102, 141);
            button11.Margin = new Padding(4);
            button11.Name = "button11";
            button11.Size = new Size(116, 46);
            button11.TabIndex = 26;
            button11.Text = "Prev";
            button11.UseVisualStyleBackColor = false;
            button11.Click += button11_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox2.Controls.Add(connectionsTB);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(name_tb);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(progressBar1);
            groupBox2.Controls.Add(textBox1);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(10, 21);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(344, 171);
            groupBox2.TabIndex = 30;
            groupBox2.TabStop = false;
            groupBox2.Text = "Networking";
            groupBox2.Enter += groupBox2_Enter;
            // 
            // connectionsTB
            // 
            connectionsTB.BackColor = Color.White;
            connectionsTB.Location = new Point(126, 123);
            connectionsTB.Margin = new Padding(4);
            connectionsTB.MaxLength = 10;
            connectionsTB.Name = "connectionsTB";
            connectionsTB.ReadOnly = true;
            connectionsTB.Size = new Size(33, 31);
            connectionsTB.TabIndex = 38;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(8, 125);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(114, 25);
            label9.TabIndex = 37;
            label9.Text = "Connections:";
            label9.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 94);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(55, 25);
            label4.TabIndex = 35;
            label4.Text = "State:";
            label4.TextAlign = ContentAlignment.BottomLeft;
            // 
            // name_tb
            // 
            name_tb.BackColor = Color.White;
            name_tb.Location = new Point(184, 56);
            name_tb.Margin = new Padding(4);
            name_tb.MaxLength = 10;
            name_tb.Name = "name_tb";
            name_tb.Size = new Size(121, 31);
            name_tb.TabIndex = 36;
            name_tb.TextChanged += name_tb_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(184, 28);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(63, 25);
            label5.TabIndex = 35;
            label5.Text = "Name:";
            label5.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(170, 94);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(164, 25);
            label7.TabIndex = 34;
            label7.Text = "Receiving Progress:";
            label7.TextAlign = ContentAlignment.BottomLeft;
            label7.Click += label7_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(30, 28);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(58, 25);
            label6.TabIndex = 32;
            label6.Text = "Code:";
            label6.TextAlign = ContentAlignment.BottomLeft;
            label6.Click += label6_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(184, 122);
            progressBar1.Margin = new Padding(4);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(126, 28);
            progressBar1.TabIndex = 33;
            progressBar1.Click += progressBar1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(30, 56);
            textBox1.Margin = new Padding(4);
            textBox1.MaxLength = 10;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(121, 31);
            textBox1.TabIndex = 30;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            groupBox4.Controls.Add(button14);
            groupBox4.Controls.Add(button5);
            groupBox4.Controls.Add(button2);
            groupBox4.Controls.Add(button4);
            groupBox4.Controls.Add(button3);
            groupBox4.ForeColor = Color.White;
            groupBox4.Location = new Point(9, 454);
            groupBox4.Margin = new Padding(4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4);
            groupBox4.Size = new Size(344, 165);
            groupBox4.TabIndex = 31;
            groupBox4.TabStop = false;
            groupBox4.Text = "Picture Controls";
            // 
            // button14
            // 
            button14.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button14.BackColor = Color.Firebrick;
            button14.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            button14.Location = new Point(2, 20);
            button14.Margin = new Padding(4);
            button14.Name = "button14";
            button14.Size = new Size(111, 39);
            button14.TabIndex = 30;
            button14.Text = "Reset";
            button14.UseVisualStyleBackColor = false;
            button14.Click += button14_Click;
            // 
            // hideMenue_cb
            // 
            hideMenue_cb.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            hideMenue_cb.AutoSize = true;
            hideMenue_cb.ForeColor = SystemColors.Control;
            hideMenue_cb.Location = new Point(1734, 349);
            hideMenue_cb.Margin = new Padding(4);
            hideMenue_cb.Name = "hideMenue_cb";
            hideMenue_cb.Size = new Size(72, 29);
            hideMenue_cb.TabIndex = 30;
            hideMenue_cb.Text = "hide";
            hideMenue_cb.UseVisualStyleBackColor = true;
            hideMenue_cb.CheckedChanged += hideMenue_cb_CheckedChanged;
            // 
            // button6
            // 
            button6.AutoSize = true;
            button6.BackColor = SystemColors.MenuHighlight;
            button6.Cursor = Cursors.Cross;
            button6.Location = new Point(220, 129);
            button6.Margin = new Padding(4);
            button6.Name = "button6";
            button6.Size = new Size(34, 38);
            button6.TabIndex = 26;
            button6.Text = "1";
            button6.UseVisualStyleBackColor = false;
            button6.MouseDown += markers_MouseDown;
            // 
            // button7
            // 
            button7.AutoSize = true;
            button7.BackColor = SystemColors.MenuHighlight;
            button7.Cursor = Cursors.Cross;
            button7.Location = new Point(262, 129);
            button7.Margin = new Padding(4);
            button7.Name = "button7";
            button7.Size = new Size(34, 38);
            button7.TabIndex = 27;
            button7.Text = "2";
            button7.UseVisualStyleBackColor = false;
            button7.MouseDown += markers_MouseDown;
            // 
            // button8
            // 
            button8.AutoSize = true;
            button8.BackColor = SystemColors.MenuHighlight;
            button8.Cursor = Cursors.Cross;
            button8.Location = new Point(220, 171);
            button8.Margin = new Padding(4);
            button8.Name = "button8";
            button8.Size = new Size(34, 38);
            button8.TabIndex = 28;
            button8.Text = "3";
            button8.UseVisualStyleBackColor = false;
            button8.MouseDown += markers_MouseDown;
            // 
            // button9
            // 
            button9.AutoSize = true;
            button9.BackColor = SystemColors.MenuHighlight;
            button9.Cursor = Cursors.Cross;
            button9.Location = new Point(262, 171);
            button9.Margin = new Padding(4);
            button9.Name = "button9";
            button9.Size = new Size(34, 38);
            button9.TabIndex = 29;
            button9.Text = "4";
            button9.UseVisualStyleBackColor = false;
            button9.MouseDown += markers_MouseDown;
            // 
            // pictureCountTB
            // 
            pictureCountTB.BackColor = Color.Indigo;
            pictureCountTB.BorderStyle = BorderStyle.None;
            pictureCountTB.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            pictureCountTB.ForeColor = SystemColors.InactiveBorder;
            pictureCountTB.Location = new Point(290, 63);
            pictureCountTB.Margin = new Padding(4);
            pictureCountTB.MaxLength = 10;
            pictureCountTB.Name = "pictureCountTB";
            pictureCountTB.ReadOnly = true;
            pictureCountTB.Size = new Size(33, 27);
            pictureCountTB.TabIndex = 39;
            pictureCountTB.Text = "0";
            pictureCountTB.TextAlign = HorizontalAlignment.Center;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(1832, 1012);
            Controls.Add(hideMenue_cb);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(groupBox1);
            Controls.Add(pictureBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            MinimumSize = new Size(733, 704);
            Name = "Form1";
            Text = "PictureToPC";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private PictureBox pictureBox1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button11;
        private Button button10;
        private Label label3;
        private GroupBox groupBox2;
        private ProgressBar progressBar1;
        private Label label7;
        private GroupBox groupBox5;
        private Button button14;
        private Button button13;
        private Label label8;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private Label label6;
        private TextBox textBox1;
        private Label label4;
        private CheckBox hideMenue_cb;
        private TextBox name_tb;
        private Label label5;
        private CheckBox checkBox2;
        private Label label9;
        private TextBox connectionsTB;
        private TextBox pictureCountTB;
    }
}