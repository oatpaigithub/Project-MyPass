
namespace TestFunctionSQL
{
    partial class AddConnectionStringPage
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
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureButtonClosing = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonStyleMypassSubmit = new TestFunctionSQL.ButtonStyleMypass();
            this.myPassTextBoxContainerName = new TestFunctionSQL.MyPassTextBox();
            this.myPassTextBoxConnectionString = new TestFunctionSQL.MyPassTextBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureButtonClosing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(205, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 45);
            this.label4.TabIndex = 14;
            this.label4.Text = "Azure Storage";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(194, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 30);
            this.label3.TabIndex = 16;
            this.label3.Text = "Connection String";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(194, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(173, 30);
            this.label1.TabIndex = 18;
            this.label1.Text = "Container Name";
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(660, 83);
            this.panelTop.TabIndex = 23;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // pictureButtonClosing
            // 
            this.pictureButtonClosing.Image = global::TestFunctionSQL.ResourceMypass.On_button_fill_2x;
            this.pictureButtonClosing.Location = new System.Drawing.Point(600, 12);
            this.pictureButtonClosing.Name = "pictureButtonClosing";
            this.pictureButtonClosing.Size = new System.Drawing.Size(48, 48);
            this.pictureButtonClosing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureButtonClosing.TabIndex = 22;
            this.pictureButtonClosing.TabStop = false;
            this.pictureButtonClosing.Click += new System.EventHandler(this.pictureButtonClosing_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestFunctionSQL.ResourceMypass.Microsoft_Azure_svg;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // buttonStyleMypassSubmit
            // 
            this.buttonStyleMypassSubmit.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.buttonStyleMypassSubmit.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.buttonStyleMypassSubmit.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.buttonStyleMypassSubmit.BorderRadius = 12;
            this.buttonStyleMypassSubmit.BorderSize = 0;
            this.buttonStyleMypassSubmit.FlatAppearance.BorderSize = 0;
            this.buttonStyleMypassSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStyleMypassSubmit.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStyleMypassSubmit.ForeColor = System.Drawing.Color.White;
            this.buttonStyleMypassSubmit.Location = new System.Drawing.Point(199, 250);
            this.buttonStyleMypassSubmit.Name = "buttonStyleMypassSubmit";
            this.buttonStyleMypassSubmit.Size = new System.Drawing.Size(250, 30);
            this.buttonStyleMypassSubmit.TabIndex = 21;
            this.buttonStyleMypassSubmit.Text = "Submit";
            this.buttonStyleMypassSubmit.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassSubmit.UseVisualStyleBackColor = false;
            this.buttonStyleMypassSubmit.Click += new System.EventHandler(this.buttonStyleMypassSubmit_Click);
            // 
            // myPassTextBoxContainerName
            // 
            this.myPassTextBoxContainerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxContainerName.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxContainerName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxContainerName.BorderRadius = 10;
            this.myPassTextBoxContainerName.BorderSize = 2;
            this.myPassTextBoxContainerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myPassTextBoxContainerName.ForeColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxContainerName.Location = new System.Drawing.Point(199, 198);
            this.myPassTextBoxContainerName.Margin = new System.Windows.Forms.Padding(4);
            this.myPassTextBoxContainerName.Multiline = false;
            this.myPassTextBoxContainerName.Name = "myPassTextBoxContainerName";
            this.myPassTextBoxContainerName.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.myPassTextBoxContainerName.PasswordChar = false;
            this.myPassTextBoxContainerName.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxContainerName.PlaceholderText = "";
            this.myPassTextBoxContainerName.ReadOnly = false;
            this.myPassTextBoxContainerName.Size = new System.Drawing.Size(250, 31);
            this.myPassTextBoxContainerName.TabIndex = 17;
            this.myPassTextBoxContainerName.Texts = "";
            this.myPassTextBoxContainerName.UnderlinedStyle = false;
            // 
            // myPassTextBoxConnectionString
            // 
            this.myPassTextBoxConnectionString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxConnectionString.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxConnectionString.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxConnectionString.BorderRadius = 10;
            this.myPassTextBoxConnectionString.BorderSize = 2;
            this.myPassTextBoxConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myPassTextBoxConnectionString.ForeColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxConnectionString.Location = new System.Drawing.Point(199, 129);
            this.myPassTextBoxConnectionString.Margin = new System.Windows.Forms.Padding(4);
            this.myPassTextBoxConnectionString.Multiline = false;
            this.myPassTextBoxConnectionString.Name = "myPassTextBoxConnectionString";
            this.myPassTextBoxConnectionString.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.myPassTextBoxConnectionString.PasswordChar = false;
            this.myPassTextBoxConnectionString.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxConnectionString.PlaceholderText = "";
            this.myPassTextBoxConnectionString.ReadOnly = false;
            this.myPassTextBoxConnectionString.Size = new System.Drawing.Size(250, 31);
            this.myPassTextBoxConnectionString.TabIndex = 15;
            this.myPassTextBoxConnectionString.Texts = "";
            this.myPassTextBoxConnectionString.UnderlinedStyle = false;
            // 
            // AddConnectionStringPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(660, 317);
            this.Controls.Add(this.pictureButtonClosing);
            this.Controls.Add(this.buttonStyleMypassSubmit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myPassTextBoxContainerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.myPassTextBoxConnectionString);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "AddConnectionStringPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddConnectionStringPage";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddConnectionStringPage_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AddConnectionStringPage_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AddConnectionStringPage_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AddConnectionStringPage_MouseUp);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureButtonClosing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private MyPassTextBox myPassTextBoxConnectionString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private MyPassTextBox myPassTextBoxContainerName;
        private ButtonStyleMypass buttonStyleMypassSubmit;
        private System.Windows.Forms.PictureBox pictureButtonClosing;
        private System.Windows.Forms.Panel panelTop;
    }
}