
namespace TestFunctionSQL
{
    partial class ViewMessegerBox
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxClosing = new System.Windows.Forms.PictureBox();
            this.labelHeadtop = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonStyleMypassCancel = new TestFunctionSQL.ButtonStyleMypass();
            this.buttonStyleMypassOK = new TestFunctionSQL.ButtonStyleMypass();
            this.myPassTextBoxCurrentPasswordReadOnly = new TestFunctionSQL.MyPassTextBox();
            this.pictureBoxEyeWhite3 = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClosing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEyeWhite3)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(224, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 30);
            this.label1.TabIndex = 9;
            this.label1.Text = "รหัสผ่านของคุณคือ";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Controls.Add(this.pictureBoxClosing);
            this.panelTop.Controls.Add(this.labelHeadtop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(641, 65);
            this.panelTop.TabIndex = 8;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // pictureBoxClosing
            // 
            this.pictureBoxClosing.Image = global::TestFunctionSQL.ResourceMypass.On_button_fill_2x;
            this.pictureBoxClosing.Location = new System.Drawing.Point(588, 12);
            this.pictureBoxClosing.Name = "pictureBoxClosing";
            this.pictureBoxClosing.Size = new System.Drawing.Size(41, 40);
            this.pictureBoxClosing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxClosing.TabIndex = 1;
            this.pictureBoxClosing.TabStop = false;
            this.pictureBoxClosing.Click += new System.EventHandler(this.pictureBoxClosing_Click);
            // 
            // labelHeadtop
            // 
            this.labelHeadtop.AutoSize = true;
            this.labelHeadtop.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadtop.ForeColor = System.Drawing.SystemColors.Window;
            this.labelHeadtop.Location = new System.Drawing.Point(12, 12);
            this.labelHeadtop.Name = "labelHeadtop";
            this.labelHeadtop.Size = new System.Drawing.Size(179, 37);
            this.labelHeadtop.TabIndex = 0;
            this.labelHeadtop.Text = "แสดงรหัสผ่าน";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(224, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(321, 30);
            this.label3.TabIndex = 13;
            this.label3.Text = "คุณต้องการ Copy ไปใช้งานหรือไม่";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestFunctionSQL.ResourceMypass.PasswordView_2x;
            this.pictureBox1.Location = new System.Drawing.Point(49, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 110);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // buttonStyleMypassCancel
            // 
            this.buttonStyleMypassCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(56)))), ((int)(((byte)(59)))));
            this.buttonStyleMypassCancel.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(56)))), ((int)(((byte)(59)))));
            this.buttonStyleMypassCancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.buttonStyleMypassCancel.BorderRadius = 20;
            this.buttonStyleMypassCancel.BorderSize = 0;
            this.buttonStyleMypassCancel.FlatAppearance.BorderSize = 0;
            this.buttonStyleMypassCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStyleMypassCancel.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStyleMypassCancel.ForeColor = System.Drawing.Color.White;
            this.buttonStyleMypassCancel.Location = new System.Drawing.Point(445, 266);
            this.buttonStyleMypassCancel.Name = "buttonStyleMypassCancel";
            this.buttonStyleMypassCancel.Size = new System.Drawing.Size(150, 40);
            this.buttonStyleMypassCancel.TabIndex = 11;
            this.buttonStyleMypassCancel.Text = "No";
            this.buttonStyleMypassCancel.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassCancel.UseVisualStyleBackColor = false;
            this.buttonStyleMypassCancel.Click += new System.EventHandler(this.buttonStyleMypassCancel_Click);
            // 
            // buttonStyleMypassOK
            // 
            this.buttonStyleMypassOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(170)))), ((int)(((byte)(18)))));
            this.buttonStyleMypassOK.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(170)))), ((int)(((byte)(18)))));
            this.buttonStyleMypassOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(170)))), ((int)(((byte)(18)))));
            this.buttonStyleMypassOK.BorderRadius = 20;
            this.buttonStyleMypassOK.BorderSize = 0;
            this.buttonStyleMypassOK.FlatAppearance.BorderSize = 0;
            this.buttonStyleMypassOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStyleMypassOK.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStyleMypassOK.ForeColor = System.Drawing.Color.White;
            this.buttonStyleMypassOK.Location = new System.Drawing.Point(29, 266);
            this.buttonStyleMypassOK.Name = "buttonStyleMypassOK";
            this.buttonStyleMypassOK.Size = new System.Drawing.Size(150, 40);
            this.buttonStyleMypassOK.TabIndex = 10;
            this.buttonStyleMypassOK.Text = "Copy";
            this.buttonStyleMypassOK.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassOK.UseVisualStyleBackColor = false;
            this.buttonStyleMypassOK.Click += new System.EventHandler(this.buttonStyleMypassOK_Click);
            // 
            // myPassTextBoxCurrentPasswordReadOnly
            // 
            this.myPassTextBoxCurrentPasswordReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxCurrentPasswordReadOnly.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxCurrentPasswordReadOnly.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxCurrentPasswordReadOnly.BorderRadius = 10;
            this.myPassTextBoxCurrentPasswordReadOnly.BorderSize = 2;
            this.myPassTextBoxCurrentPasswordReadOnly.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myPassTextBoxCurrentPasswordReadOnly.ForeColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxCurrentPasswordReadOnly.Location = new System.Drawing.Point(225, 159);
            this.myPassTextBoxCurrentPasswordReadOnly.Margin = new System.Windows.Forms.Padding(4);
            this.myPassTextBoxCurrentPasswordReadOnly.Multiline = false;
            this.myPassTextBoxCurrentPasswordReadOnly.Name = "myPassTextBoxCurrentPasswordReadOnly";
            this.myPassTextBoxCurrentPasswordReadOnly.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.myPassTextBoxCurrentPasswordReadOnly.PasswordChar = true;
            this.myPassTextBoxCurrentPasswordReadOnly.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxCurrentPasswordReadOnly.PlaceholderText = "";
            this.myPassTextBoxCurrentPasswordReadOnly.ReadOnly = true;
            this.myPassTextBoxCurrentPasswordReadOnly.Size = new System.Drawing.Size(250, 36);
            this.myPassTextBoxCurrentPasswordReadOnly.TabIndex = 35;
            this.myPassTextBoxCurrentPasswordReadOnly.Texts = "";
            this.myPassTextBoxCurrentPasswordReadOnly.UnderlinedStyle = false;
            // 
            // pictureBoxEyeWhite3
            // 
            this.pictureBoxEyeWhite3.Image = global::TestFunctionSQL.ResourceMypass.EyeWhithe1x;
            this.pictureBoxEyeWhite3.Location = new System.Drawing.Point(485, 167);
            this.pictureBoxEyeWhite3.Name = "pictureBoxEyeWhite3";
            this.pictureBoxEyeWhite3.Size = new System.Drawing.Size(29, 20);
            this.pictureBoxEyeWhite3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxEyeWhite3.TabIndex = 39;
            this.pictureBoxEyeWhite3.TabStop = false;
            this.pictureBoxEyeWhite3.Click += new System.EventHandler(this.pictureBoxEyeWhite3_Click);
            // 
            // ViewMessegerBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(641, 323);
            this.Controls.Add(this.pictureBoxEyeWhite3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonStyleMypassCancel);
            this.Controls.Add(this.buttonStyleMypassOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.myPassTextBoxCurrentPasswordReadOnly);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewMessegerBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewMessegerBox";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClosing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEyeWhite3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonStyleMypass buttonStyleMypassCancel;
        private ButtonStyleMypass buttonStyleMypassOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxClosing;
        private System.Windows.Forms.Label labelHeadtop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MyPassTextBox myPassTextBoxCurrentPasswordReadOnly;
        private System.Windows.Forms.PictureBox pictureBoxEyeWhite3;
    }
}