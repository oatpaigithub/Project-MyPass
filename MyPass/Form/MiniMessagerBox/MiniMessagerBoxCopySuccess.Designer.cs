
namespace TestFunctionSQL
{
    partial class MiniMessagerBoxCopySuccess
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelHeadtop = new System.Windows.Forms.Label();
            this.buttonStyleMypassOK = new TestFunctionSQL.ButtonStyleMypass();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(119, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 30);
            this.label1.TabIndex = 5;
            this.label1.Text = "คัดลอกรหัสผ่านของคุณสำเร็จ";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Controls.Add(this.pictureBox1);
            this.panelTop.Controls.Add(this.labelHeadtop);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(540, 65);
            this.panelTop.TabIndex = 4;
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
            // labelHeadtop
            // 
            this.labelHeadtop.AutoSize = true;
            this.labelHeadtop.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeadtop.ForeColor = System.Drawing.SystemColors.Window;
            this.labelHeadtop.Location = new System.Drawing.Point(12, 12);
            this.labelHeadtop.Name = "labelHeadtop";
            this.labelHeadtop.Size = new System.Drawing.Size(101, 37);
            this.labelHeadtop.TabIndex = 0;
            this.labelHeadtop.Text = "คัดลอก";
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
            this.buttonStyleMypassOK.TabIndex = 6;
            this.buttonStyleMypassOK.Text = "OK";
            this.buttonStyleMypassOK.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassOK.UseVisualStyleBackColor = false;
            this.buttonStyleMypassOK.Click += new System.EventHandler(this.buttonStyleMypassOK_Click);
            // 
            // MiniMessagerBoxCopySuccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(540, 245);
            this.Controls.Add(this.buttonStyleMypassOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MiniMessagerBoxCopySuccess";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiniMessagerBoxCopySuccess";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ButtonStyleMypass buttonStyleMypassOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelHeadtop;
    }
}