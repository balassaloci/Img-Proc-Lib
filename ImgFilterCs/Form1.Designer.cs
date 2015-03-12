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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(12, 12);
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
            this.pictureBox1.Location = new System.Drawing.Point(34, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(485, 362);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // invertButton
            // 
            this.invertButton.Location = new System.Drawing.Point(93, 12);
            this.invertButton.Name = "invertButton";
            this.invertButton.Size = new System.Drawing.Size(75, 23);
            this.invertButton.TabIndex = 0;
            this.invertButton.Text = "Invert";
            this.invertButton.UseVisualStyleBackColor = true;
            this.invertButton.Click += new System.EventHandler(this.invertButton_Click);
            // 
            // BlurButton
            // 
            this.BlurButton.Location = new System.Drawing.Point(174, 12);
            this.BlurButton.Name = "BlurButton";
            this.BlurButton.Size = new System.Drawing.Size(75, 23);
            this.BlurButton.TabIndex = 2;
            this.BlurButton.Text = "Run Filter";
            this.BlurButton.UseVisualStyleBackColor = true;
            this.BlurButton.Click += new System.EventHandler(this.BlurButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 12);
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
            this.cursorPos.Location = new System.Drawing.Point(12, 38);
            this.cursorPos.Name = "cursorPos";
            this.cursorPos.Size = new System.Drawing.Size(37, 13);
            this.cursorPos.TabIndex = 4;
            this.cursorPos.Tag = "Cursor X: {0} Y: {1}";
            this.cursorPos.Text = "Cursor";
            // 
            // treshTB
            // 
            this.treshTB.Location = new System.Drawing.Point(261, 41);
            this.treshTB.Name = "treshTB";
            this.treshTB.Size = new System.Drawing.Size(39, 20);
            this.treshTB.TabIndex = 5;
            this.treshTB.Text = "5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(204, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Treshold:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(306, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Max distance:";
            // 
            // maxDistTextBox
            // 
            this.maxDistTextBox.Location = new System.Drawing.Point(385, 41);
            this.maxDistTextBox.Name = "maxDistTextBox";
            this.maxDistTextBox.Size = new System.Drawing.Size(39, 20);
            this.maxDistTextBox.TabIndex = 7;
            this.maxDistTextBox.Text = "3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.maxDistTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treshTB);
            this.Controls.Add(this.cursorPos);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BlurButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.invertButton);
            this.Controls.Add(this.loadButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

