
namespace TestFunctionSQL
{
    partial class MiniMessagerBoxTextBoxAlert
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
            this.labelContentText = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelHeadText = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonStyleMypassOK = new TestFunctionSQL.ButtonStyleMypass();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // labelContentText
            // 
            this.labelContentText.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContentText.ForeColor = System.Drawing.Color.White;
            this.labelContentText.Location = new System.Drawing.Point(100, 123);
            this.labelContentText.Name = "labelContentText";
            this.labelContentText.Size = new System.Drawing.Size(386, 67);
            this.labelContentText.TabIndex = 8;
            this.labelContentText.Text = "contentText";
            this.labelContentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelContentText.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.labelHeadText);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(540, 65);
            this.panelTop.TabIndex = 7;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestFunctionSQL.ResourceMypass.On_button_fill_2x;
            this.pictureBox1.Location = new System.Drawing.Point(487, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // labelHeadText
            // 
            this.labelHeadText.AutoSize = true;
            this.labelHeadText.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadText.ForeColor = System.Drawing.SystemColors.Window;
            this.labelHeadText.Location = new System.Drawing.Point(12, 12);
            this.labelHeadText.Name = "labelHeadText";
            this.labelHeadText.Size = new System.Drawing.Size(139, 37);
            this.labelHeadText.TabIndex = 0;
            this.labelHeadText.Text = "HeadText";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TestFunctionSQL.ResourceMypass.alert1;
            this.pictureBox2.Location = new System.Drawing.Point(19, 107);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 64);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // buttonStyleMypassOK
            // 
            this.buttonStyleMypassOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.buttonStyleMypassOK.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.buttonStyleMypassOK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(200)))), ((int)(((byte)(83)))));
            this.buttonStyleMypassOK.BorderRadius = 20;
            this.buttonStyleMypassOK.BorderSize = 0;
            this.buttonStyleMypassOK.FlatAppearance.BorderSize = 0;
            this.buttonStyleMypassOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStyleMypassOK.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStyleMypassOK.ForeColor = System.Drawing.Color.White;
            this.buttonStyleMypassOK.Location = new System.Drawing.Point(378, 193);
            this.buttonStyleMypassOK.Name = "buttonStyleMypassOK";
            this.buttonStyleMypassOK.Size = new System.Drawing.Size(150, 40);
            this.buttonStyleMypassOK.TabIndex = 9;
            this.buttonStyleMypassOK.Text = "OK";
            this.buttonStyleMypassOK.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassOK.UseVisualStyleBackColor = false;
            this.buttonStyleMypassOK.Click += new System.EventHandler(this.buttonStyleMypassOK_Click);
            // 
            // MiniMessagerBoxTextBoxAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(540, 245);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.buttonStyleMypassOK);
            this.Controls.Add(this.labelContentText);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MiniMessagerBoxTextBoxAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniMessagerBoxAlert";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MiniMessagerBoxTextBoxAlert_KeyDown);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ButtonStyleMypass buttonStyleMypassOK;
        private System.Windows.Forms.Label labelContentText;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelHeadText;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}