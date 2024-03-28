
namespace TestFunctionSQL
{
    partial class MiniMessagerBoxTextBoxNormal
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
            this.buttonStyleMypassOK = new TestFunctionSQL.ButtonStyleMypass();
            this.labelContentText = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.pictureBoxButtonClose = new System.Windows.Forms.PictureBox();
            this.labelHeadText = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonClose)).BeginInit();
            this.SuspendLayout();
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
            this.buttonStyleMypassOK.TabIndex = 13;
            this.buttonStyleMypassOK.Text = "OK";
            this.buttonStyleMypassOK.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassOK.UseVisualStyleBackColor = false;
            this.buttonStyleMypassOK.Click += new System.EventHandler(this.buttonStyleMypassOK_Click);
            // 
            // labelContentText
            // 
            this.labelContentText.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContentText.ForeColor = System.Drawing.Color.White;
            this.labelContentText.Location = new System.Drawing.Point(19, 123);
            this.labelContentText.Name = "labelContentText";
            this.labelContentText.Size = new System.Drawing.Size(467, 67);
            this.labelContentText.TabIndex = 12;
            this.labelContentText.Text = "contentText";
            this.labelContentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Controls.Add(this.pictureBoxButtonClose);
            this.panelTop.Controls.Add(this.labelHeadText);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(540, 65);
            this.panelTop.TabIndex = 11;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // pictureBoxButtonClose
            // 
            this.pictureBoxButtonClose.Image = global::TestFunctionSQL.ResourceMypass.On_button_fill_2x;
            this.pictureBoxButtonClose.Location = new System.Drawing.Point(487, 12);
            this.pictureBoxButtonClose.Name = "pictureBoxButtonClose";
            this.pictureBoxButtonClose.Size = new System.Drawing.Size(41, 40);
            this.pictureBoxButtonClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxButtonClose.TabIndex = 1;
            this.pictureBoxButtonClose.TabStop = false;
            this.pictureBoxButtonClose.Click += new System.EventHandler(this.pictureBoxButtonClose_Click);
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
            // MiniMessagerBoxTextBoxNormal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(540, 245);
            this.Controls.Add(this.buttonStyleMypassOK);
            this.Controls.Add(this.labelContentText);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "MiniMessagerBoxTextBoxNormal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniMessagerBoxTextBoxNormal";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MiniMessagerBoxTextBoxNormal_KeyPress);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private ButtonStyleMypass buttonStyleMypassOK;
        private System.Windows.Forms.Label labelContentText;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxButtonClose;
        private System.Windows.Forms.Label labelHeadText;
    }
}