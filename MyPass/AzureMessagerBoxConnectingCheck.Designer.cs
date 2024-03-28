
namespace TestFunctionSQL
{
    partial class AzureMessagerBoxConnectingCheck
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelHeadText = new System.Windows.Forms.Label();
            this.labelContentText = new System.Windows.Forms.Label();
            this.pictureBoxButtonClose = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Controls.Add(this.labelHeadText);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(540, 65);
            this.panelTop.TabIndex = 17;
            // 
            // labelHeadText
            // 
            this.labelHeadText.AutoSize = true;
            this.labelHeadText.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadText.ForeColor = System.Drawing.SystemColors.Window;
            this.labelHeadText.Location = new System.Drawing.Point(12, 16);
            this.labelHeadText.Name = "labelHeadText";
            this.labelHeadText.Size = new System.Drawing.Size(105, 30);
            this.labelHeadText.TabIndex = 0;
            this.labelHeadText.Text = "HeadText";
            // 
            // labelContentText
            // 
            this.labelContentText.Font = new System.Drawing.Font("Nirmala UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelContentText.ForeColor = System.Drawing.Color.White;
            this.labelContentText.Location = new System.Drawing.Point(140, 132);
            this.labelContentText.Name = "labelContentText";
            this.labelContentText.Size = new System.Drawing.Size(318, 95);
            this.labelContentText.TabIndex = 18;
            this.labelContentText.Text = "contentText";
            this.labelContentText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestFunctionSQL.ResourceMypass.Loading1_2x;
            this.pictureBox1.Location = new System.Drawing.Point(28, 109);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // AzureMessagerBoxConnectingCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(540, 245);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.pictureBoxButtonClose);
            this.Controls.Add(this.labelContentText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AzureMessagerBoxConnectingCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AzureMessagerBoxConnectingCheck";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AzureMessagerBoxConnectingCheck_FormClosing);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxButtonClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBoxButtonClose;
        private System.Windows.Forms.Label labelHeadText;
        private System.Windows.Forms.Label labelContentText;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}