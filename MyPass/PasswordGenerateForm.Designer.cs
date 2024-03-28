
namespace TestFunctionSQL
{
    partial class PasswordGenerateForm
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
            this.CheckBoxLowerCaseChars = new System.Windows.Forms.CheckBox();
            this.checkBoxUpperCaseChars = new System.Windows.Forms.CheckBox();
            this.checkBoxNumericChars = new System.Windows.Forms.CheckBox();
            this.checkBoxSpecialChars = new System.Windows.Forms.CheckBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.textBoxShow = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toggleButtonMyPassSpecialChars = new TestFunctionSQL.ToggleButtonMyPass();
            this.toggleButtonMyPassNumericChars = new TestFunctionSQL.ToggleButtonMyPass();
            this.toggleButtonMyPassLowerCaseChars = new TestFunctionSQL.ToggleButtonMyPass();
            this.toggleButtonMyPassUpperCaseChars = new TestFunctionSQL.ToggleButtonMyPass();
            this.buttonStyleMypassPasswordGenerate = new TestFunctionSQL.ButtonStyleMypass();
            this.myPassTextBoxPasswordGenerateReadOnly = new TestFunctionSQL.MyPassTextBox();
            this.pictureBoxCloseButton = new System.Windows.Forms.PictureBox();
            this.pictureBoxCopy = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCloseButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckBoxLowerCaseChars
            // 
            this.CheckBoxLowerCaseChars.AutoSize = true;
            this.CheckBoxLowerCaseChars.Location = new System.Drawing.Point(46, 399);
            this.CheckBoxLowerCaseChars.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBoxLowerCaseChars.Name = "CheckBoxLowerCaseChars";
            this.CheckBoxLowerCaseChars.Size = new System.Drawing.Size(126, 17);
            this.CheckBoxLowerCaseChars.TabIndex = 0;
            this.CheckBoxLowerCaseChars.Text = "LowerCaseChars(a-z)";
            this.CheckBoxLowerCaseChars.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpperCaseChars
            // 
            this.checkBoxUpperCaseChars.AutoSize = true;
            this.checkBoxUpperCaseChars.Checked = true;
            this.checkBoxUpperCaseChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpperCaseChars.Location = new System.Drawing.Point(46, 377);
            this.checkBoxUpperCaseChars.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxUpperCaseChars.Name = "checkBoxUpperCaseChars";
            this.checkBoxUpperCaseChars.Size = new System.Drawing.Size(129, 17);
            this.checkBoxUpperCaseChars.TabIndex = 2;
            this.checkBoxUpperCaseChars.Text = "UpperCaseChars(A-Z)";
            this.checkBoxUpperCaseChars.UseVisualStyleBackColor = true;
            // 
            // checkBoxNumericChars
            // 
            this.checkBoxNumericChars.AutoSize = true;
            this.checkBoxNumericChars.Location = new System.Drawing.Point(46, 421);
            this.checkBoxNumericChars.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxNumericChars.Name = "checkBoxNumericChars";
            this.checkBoxNumericChars.Size = new System.Drawing.Size(113, 17);
            this.checkBoxNumericChars.TabIndex = 3;
            this.checkBoxNumericChars.Text = "NumericChars(0-9)";
            this.checkBoxNumericChars.UseVisualStyleBackColor = true;
            // 
            // checkBoxSpecialChars
            // 
            this.checkBoxSpecialChars.AutoSize = true;
            this.checkBoxSpecialChars.Location = new System.Drawing.Point(46, 443);
            this.checkBoxSpecialChars.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxSpecialChars.Name = "checkBoxSpecialChars";
            this.checkBoxSpecialChars.Size = new System.Drawing.Size(91, 17);
            this.checkBoxSpecialChars.TabIndex = 4;
            this.checkBoxSpecialChars.Text = "SpecialChars ";
            this.checkBoxSpecialChars.UseVisualStyleBackColor = true;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(46, 472);
            this.buttonGenerate.Margin = new System.Windows.Forms.Padding(2);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(112, 19);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // textBoxShow
            // 
            this.textBoxShow.BackColor = System.Drawing.Color.White;
            this.textBoxShow.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBoxShow.Location = new System.Drawing.Point(21, 295);
            this.textBoxShow.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxShow.Name = "textBoxShow";
            this.textBoxShow.ReadOnly = true;
            this.textBoxShow.Size = new System.Drawing.Size(173, 20);
            this.textBoxShow.TabIndex = 7;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.numericUpDown1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numericUpDown1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.numericUpDown1.ForeColor = System.Drawing.Color.White;
            this.numericUpDown1.Location = new System.Drawing.Point(56, 238);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(125, 22);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(52, 214);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 21);
            this.label1.TabIndex = 10;
            this.label1.Text = "Length";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(53, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 17);
            this.label2.TabIndex = 42;
            this.label2.Text = "UpperCaseChars(A-Z)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(53, 305);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 17);
            this.label3.TabIndex = 43;
            this.label3.Text = "LowerCaseChars(a-z)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(53, 333);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 17);
            this.label4.TabIndex = 44;
            this.label4.Text = "NumericChars(0-9)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(53, 361);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 17);
            this.label5.TabIndex = 45;
            this.label5.Text = "SpecialChars ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(49, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(276, 37);
            this.label6.TabIndex = 46;
            this.label6.Text = "Password Generator";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 276);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(209, 215);
            this.panel1.TabIndex = 47;
            // 
            // toggleButtonMyPassSpecialChars
            // 
            this.toggleButtonMyPassSpecialChars.AutoSize = true;
            this.toggleButtonMyPassSpecialChars.Checked = true;
            this.toggleButtonMyPassSpecialChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleButtonMyPassSpecialChars.Location = new System.Drawing.Point(261, 360);
            this.toggleButtonMyPassSpecialChars.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassSpecialChars.Name = "toggleButtonMyPassSpecialChars";
            this.toggleButtonMyPassSpecialChars.OffBackColor = System.Drawing.Color.Gray;
            this.toggleButtonMyPassSpecialChars.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.toggleButtonMyPassSpecialChars.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.toggleButtonMyPassSpecialChars.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.toggleButtonMyPassSpecialChars.Size = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassSpecialChars.TabIndex = 41;
            this.toggleButtonMyPassSpecialChars.UseVisualStyleBackColor = true;
            // 
            // toggleButtonMyPassNumericChars
            // 
            this.toggleButtonMyPassNumericChars.AutoSize = true;
            this.toggleButtonMyPassNumericChars.Checked = true;
            this.toggleButtonMyPassNumericChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleButtonMyPassNumericChars.Location = new System.Drawing.Point(261, 332);
            this.toggleButtonMyPassNumericChars.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassNumericChars.Name = "toggleButtonMyPassNumericChars";
            this.toggleButtonMyPassNumericChars.OffBackColor = System.Drawing.Color.Gray;
            this.toggleButtonMyPassNumericChars.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.toggleButtonMyPassNumericChars.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.toggleButtonMyPassNumericChars.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.toggleButtonMyPassNumericChars.Size = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassNumericChars.TabIndex = 40;
            this.toggleButtonMyPassNumericChars.UseVisualStyleBackColor = true;
            // 
            // toggleButtonMyPassLowerCaseChars
            // 
            this.toggleButtonMyPassLowerCaseChars.AutoSize = true;
            this.toggleButtonMyPassLowerCaseChars.Checked = true;
            this.toggleButtonMyPassLowerCaseChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleButtonMyPassLowerCaseChars.Location = new System.Drawing.Point(261, 304);
            this.toggleButtonMyPassLowerCaseChars.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassLowerCaseChars.Name = "toggleButtonMyPassLowerCaseChars";
            this.toggleButtonMyPassLowerCaseChars.OffBackColor = System.Drawing.Color.Gray;
            this.toggleButtonMyPassLowerCaseChars.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.toggleButtonMyPassLowerCaseChars.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.toggleButtonMyPassLowerCaseChars.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.toggleButtonMyPassLowerCaseChars.Size = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassLowerCaseChars.TabIndex = 39;
            this.toggleButtonMyPassLowerCaseChars.UseVisualStyleBackColor = true;
            // 
            // toggleButtonMyPassUpperCaseChars
            // 
            this.toggleButtonMyPassUpperCaseChars.AutoSize = true;
            this.toggleButtonMyPassUpperCaseChars.Checked = true;
            this.toggleButtonMyPassUpperCaseChars.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleButtonMyPassUpperCaseChars.Location = new System.Drawing.Point(261, 276);
            this.toggleButtonMyPassUpperCaseChars.MinimumSize = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassUpperCaseChars.Name = "toggleButtonMyPassUpperCaseChars";
            this.toggleButtonMyPassUpperCaseChars.OffBackColor = System.Drawing.Color.Gray;
            this.toggleButtonMyPassUpperCaseChars.OffToggleColor = System.Drawing.Color.Gainsboro;
            this.toggleButtonMyPassUpperCaseChars.OnBackColor = System.Drawing.Color.MediumSlateBlue;
            this.toggleButtonMyPassUpperCaseChars.OnToggleColor = System.Drawing.Color.WhiteSmoke;
            this.toggleButtonMyPassUpperCaseChars.Size = new System.Drawing.Size(45, 22);
            this.toggleButtonMyPassUpperCaseChars.TabIndex = 38;
            this.toggleButtonMyPassUpperCaseChars.UseVisualStyleBackColor = true;
            // 
            // buttonStyleMypassPasswordGenerate
            // 
            this.buttonStyleMypassPasswordGenerate.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.buttonStyleMypassPasswordGenerate.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.buttonStyleMypassPasswordGenerate.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.buttonStyleMypassPasswordGenerate.BorderRadius = 10;
            this.buttonStyleMypassPasswordGenerate.BorderSize = 0;
            this.buttonStyleMypassPasswordGenerate.FlatAppearance.BorderSize = 0;
            this.buttonStyleMypassPasswordGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStyleMypassPasswordGenerate.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStyleMypassPasswordGenerate.ForeColor = System.Drawing.Color.White;
            this.buttonStyleMypassPasswordGenerate.Location = new System.Drawing.Point(56, 413);
            this.buttonStyleMypassPasswordGenerate.Name = "buttonStyleMypassPasswordGenerate";
            this.buttonStyleMypassPasswordGenerate.Size = new System.Drawing.Size(250, 35);
            this.buttonStyleMypassPasswordGenerate.TabIndex = 37;
            this.buttonStyleMypassPasswordGenerate.Text = "Password Generator";
            this.buttonStyleMypassPasswordGenerate.TextColor = System.Drawing.Color.White;
            this.buttonStyleMypassPasswordGenerate.UseVisualStyleBackColor = false;
            this.buttonStyleMypassPasswordGenerate.Click += new System.EventHandler(this.buttonStyleMypassPasswordGenerate_Click);
            // 
            // myPassTextBoxPasswordGenerateReadOnly
            // 
            this.myPassTextBoxPasswordGenerateReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxPasswordGenerateReadOnly.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxPasswordGenerateReadOnly.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(85)))), ((int)(((byte)(85)))));
            this.myPassTextBoxPasswordGenerateReadOnly.BorderRadius = 10;
            this.myPassTextBoxPasswordGenerateReadOnly.BorderSize = 2;
            this.myPassTextBoxPasswordGenerateReadOnly.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myPassTextBoxPasswordGenerateReadOnly.ForeColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxPasswordGenerateReadOnly.Location = new System.Drawing.Point(56, 163);
            this.myPassTextBoxPasswordGenerateReadOnly.Margin = new System.Windows.Forms.Padding(4);
            this.myPassTextBoxPasswordGenerateReadOnly.Multiline = false;
            this.myPassTextBoxPasswordGenerateReadOnly.Name = "myPassTextBoxPasswordGenerateReadOnly";
            this.myPassTextBoxPasswordGenerateReadOnly.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.myPassTextBoxPasswordGenerateReadOnly.PasswordChar = false;
            this.myPassTextBoxPasswordGenerateReadOnly.PlaceholderColor = System.Drawing.Color.DarkGray;
            this.myPassTextBoxPasswordGenerateReadOnly.PlaceholderText = "";
            this.myPassTextBoxPasswordGenerateReadOnly.ReadOnly = true;
            this.myPassTextBoxPasswordGenerateReadOnly.Size = new System.Drawing.Size(250, 36);
            this.myPassTextBoxPasswordGenerateReadOnly.TabIndex = 35;
            this.myPassTextBoxPasswordGenerateReadOnly.Texts = "";
            this.myPassTextBoxPasswordGenerateReadOnly.UnderlinedStyle = false;
            // 
            // pictureBoxCloseButton
            // 
            this.pictureBoxCloseButton.Image = global::TestFunctionSQL.ResourceMypass.On_button_fill_2x;
            this.pictureBoxCloseButton.Location = new System.Drawing.Point(374, 12);
            this.pictureBoxCloseButton.Name = "pictureBoxCloseButton";
            this.pictureBoxCloseButton.Size = new System.Drawing.Size(31, 29);
            this.pictureBoxCloseButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCloseButton.TabIndex = 48;
            this.pictureBoxCloseButton.TabStop = false;
            this.pictureBoxCloseButton.Click += new System.EventHandler(this.pictureBoxCloseButton_Click);
            // 
            // pictureBoxCopy
            // 
            this.pictureBoxCopy.Image = global::TestFunctionSQL.ResourceMypass.CopyWhite_2x;
            this.pictureBoxCopy.Location = new System.Drawing.Point(331, 167);
            this.pictureBoxCopy.Name = "pictureBoxCopy";
            this.pictureBoxCopy.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxCopy.TabIndex = 36;
            this.pictureBoxCopy.TabStop = false;
            this.pictureBoxCopy.Click += new System.EventHandler(this.pictureBoxCopy_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestFunctionSQL.ResourceMypass.Copy;
            this.pictureBox1.Location = new System.Drawing.Point(197, 295);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 22);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // panelTop
            // 
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(417, 60);
            this.panelTop.TabIndex = 49;
            this.panelTop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseDown);
            this.panelTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseMove);
            this.panelTop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelTop_MouseUp);
            // 
            // PasswordGenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.ClientSize = new System.Drawing.Size(417, 489);
            this.Controls.Add(this.pictureBoxCloseButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.toggleButtonMyPassSpecialChars);
            this.Controls.Add(this.toggleButtonMyPassNumericChars);
            this.Controls.Add(this.toggleButtonMyPassLowerCaseChars);
            this.Controls.Add(this.toggleButtonMyPassUpperCaseChars);
            this.Controls.Add(this.buttonStyleMypassPasswordGenerate);
            this.Controls.Add(this.pictureBoxCopy);
            this.Controls.Add(this.myPassTextBoxPasswordGenerateReadOnly);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxShow);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.checkBoxSpecialChars);
            this.Controls.Add(this.checkBoxNumericChars);
            this.Controls.Add(this.checkBoxUpperCaseChars);
            this.Controls.Add(this.CheckBoxLowerCaseChars);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PasswordGenerateForm";
            this.Text = "PasswordGenerateForm";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCloseButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBoxLowerCaseChars;
        private System.Windows.Forms.CheckBox checkBoxUpperCaseChars;
        private System.Windows.Forms.CheckBox checkBoxNumericChars;
        private System.Windows.Forms.CheckBox checkBoxSpecialChars;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.TextBox textBoxShow;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private MyPassTextBox myPassTextBoxPasswordGenerateReadOnly;
        private System.Windows.Forms.PictureBox pictureBoxCopy;
        private ButtonStyleMypass buttonStyleMypassPasswordGenerate;
        private ToggleButtonMyPass toggleButtonMyPassUpperCaseChars;
        private ToggleButtonMyPass toggleButtonMyPassLowerCaseChars;
        private ToggleButtonMyPass toggleButtonMyPassNumericChars;
        private ToggleButtonMyPass toggleButtonMyPassSpecialChars;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxCloseButton;
        private System.Windows.Forms.Panel panelTop;
    }
}