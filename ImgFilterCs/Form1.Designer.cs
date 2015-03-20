namespace ImgFilterCs
{
    partial class Form1
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
            this.loadButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.invertButton = new System.Windows.Forms.Button();
            this.BlurButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.cursorPos = new System.Windows.Forms.Label();
            this.treshTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.maxDistTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CurrentImageLabel = new System.Windows.Forms.Label();
            this.SequenceViewPB = new System.Windows.Forms.PictureBox();
            this.NextImgButton = new System.Windows.Forms.Button();
            this.PrevFrameButton = new System.Windows.Forms.Button();
            this.LoadSequenceButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceViewPB)).BeginInit();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(6, 6);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(9, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(712, 450);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // invertButton
            // 
            this.invertButton.Location = new System.Drawing.Point(87, 6);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(75, 23);
            this.invertButton.TabIndex = 0;
            this.invertButton.Text = "Invert";
            this.invertButton.UseVisualStyleBackColor = true;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // BlurButton
            // 
            this.BlurButton.Location = new System.Drawing.Point(168, 6);
            this.BlurButton.Name = "BlurButton";
            this.BlurButton.Size = new System.Drawing.Size(75, 23);
            this.BlurButton.TabIndex = 2;
            this.BlurButton.Text = "Run Filter";
            this.BlurButton.UseVisualStyleBackColor = true;
            this.BlurButton.Click += new System.EventHandler(this.BlurButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Find";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // cursorPos
            // 
            this.cursorPos.AutoSize = true;
            this.cursorPos.Location = new System.Drawing.Point(6, 32);
            this.cursorPos.Name = "cursorPos";
            this.cursorPos.Size = new System.Drawing.Size(37, 13);
            this.cursorPos.TabIndex = 4;
            this.cursorPos.Tag = "Cursor X: {0} Y: {1}";
            this.cursorPos.Text = "Cursor";
            // 
            // treshTB
            // 
            this.treshTB.Location = new System.Drawing.Point(255, 35);
            this.treshTB.Name = "treshTB";
            this.treshTB.Size = new System.Drawing.Size(39, 20);
            this.treshTB.TabIndex = 5;
            this.treshTB.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Treshold:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(300, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max distance:";
            // 
            // maxDistTextBox
            // 
            this.maxDistTextBox.Location = new System.Drawing.Point(379, 35);
            this.maxDistTextBox.Name = "maxDistTextBox";
            this.maxDistTextBox.Size = new System.Drawing.Size(39, 20);
            this.maxDistTextBox.TabIndex = 7;
            this.maxDistTextBox.Text = "3";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(10, 10);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(737, 559);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.loadButton);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.invertButton);
            this.tabPage1.Controls.Add(this.maxDistTextBox);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.BlurButton);
            this.tabPage1.Controls.Add(this.treshTB);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.cursorPos);
            this.tabPage1.Location = new System.Drawing.Point(4, 36);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(729, 519);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Applying filters";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CurrentImageLabel);
            this.tabPage2.Controls.Add(this.SequenceViewPB);
            this.tabPage2.Controls.Add(this.NextImgButton);
            this.tabPage2.Controls.Add(this.PrevFrameButton);
            this.tabPage2.Controls.Add(this.LoadSequenceButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 36);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(729, 519);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tracker";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CurrentImageLabel
            // 
            this.CurrentImageLabel.AutoSize = true;
            this.CurrentImageLabel.Location = new System.Drawing.Point(259, 15);
            this.CurrentImageLabel.Name = "CurrentImageLabel";
            this.CurrentImageLabel.Size = new System.Drawing.Size(35, 13);
            this.CurrentImageLabel.TabIndex = 3;
            this.CurrentImageLabel.Text = "label3";
            // 
            // SequenceViewPB
            // 
            this.SequenceViewPB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SequenceViewPB.Location = new System.Drawing.Point(8, 42);
            this.SequenceViewPB.Name = "SequenceViewPB";
            this.SequenceViewPB.Size = new System.Drawing.Size(713, 469);
            this.SequenceViewPB.TabIndex = 2;
            this.SequenceViewPB.TabStop = false;
            this.SequenceViewPB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SequenceViewPB_MouseClick);
            // 
            // NextImgButton
            // 
            this.NextImgButton.Location = new System.Drawing.Point(214, 6);
            this.NextImgButton.Name = "NextImgButton";
            this.NextImgButton.Size = new System.Drawing.Size(39, 30);
            this.NextImgButton.TabIndex = 1;
            this.NextImgButton.Text = ">";
            this.NextImgButton.UseVisualStyleBackColor = true;
            this.NextImgButton.Click += new System.EventHandler(this.NextImgButton_Click);
            // 
            // PrevFrameButton
            // 
            this.PrevFrameButton.Location = new System.Drawing.Point(169, 6);
            this.PrevFrameButton.Name = "PrevFrameButton";
            this.PrevFrameButton.Size = new System.Drawing.Size(39, 30);
            this.PrevFrameButton.TabIndex = 1;
            this.PrevFrameButton.Text = "<";
            this.PrevFrameButton.UseVisualStyleBackColor = true;
            // 
            // LoadSequenceButton
            // 
            this.LoadSequenceButton.Location = new System.Drawing.Point(8, 6);
            this.LoadSequenceButton.Name = "LoadSequenceButton";
            this.LoadSequenceButton.Size = new System.Drawing.Size(119, 30);
            this.LoadSequenceButton.TabIndex = 0;
            this.LoadSequenceButton.Text = "Load sequence";
            this.LoadSequenceButton.UseVisualStyleBackColor = true;
            this.LoadSequenceButton.Click += new System.EventHandler(this.LoadSequenceButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(462, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 9;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 559);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SequenceViewPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button invertButton;
        private System.Windows.Forms.Button BlurButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label cursorPos;
        private System.Windows.Forms.TextBox treshTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox maxDistTextBox;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button LoadSequenceButton;
        private System.Windows.Forms.Button PrevFrameButton;
        private System.Windows.Forms.Button NextImgButton;
        private System.Windows.Forms.PictureBox SequenceViewPB;
        private System.Windows.Forms.Label CurrentImageLabel;
        private System.Windows.Forms.TextBox textBox1;
    }
}

