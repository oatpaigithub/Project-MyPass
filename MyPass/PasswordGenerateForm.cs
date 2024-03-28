using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace TestFunctionSQL
{
    public partial class PasswordGenerateForm : Form
    {
        private const string LowerCaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UpperCaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumericChars = "0123456789";
        private const string SpecialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        string charSet = LowerCaseChars + UpperCaseChars + NumericChars + SpecialChars;
        public PasswordGenerateForm()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"อะไรหว่า: {TextboxLengthNumeric.Trim()}");
            if (numericUpDown1.Value < 8)
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณาใส่จำนวนหลักของ Password ที่ต้องการ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณาใส่จำนวนหลักของ Password ที่ต้องการ");
            }
            else  
            {
                int length = (int)numericUpDown1.Value;

                //bool useUpperCase = checkBoxUpperCaseChars.Checked;
                //bool useLowerCase = CheckBoxLowerCaseChars.Checked;
                //bool useNumbers = checkBoxNumericChars.Checked;
                //bool useSpecialChars = checkBoxSpecialChars.Checked;

                bool useUpperCase = toggleButtonMyPassUpperCaseChars.Checked;
                bool useLowerCase = toggleButtonMyPassLowerCaseChars.Checked;
                bool useNumbers = toggleButtonMyPassNumericChars.Checked;
                bool useSpecialChars = toggleButtonMyPassSpecialChars.Checked;


                if (!useUpperCase && !useLowerCase && !useNumbers && !useSpecialChars)
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "At least one character set must be selected.");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("At least one character set must be selected.");
                }
                else
                {
                    string password = GeneratePassword(length, useUpperCase, useLowerCase, useNumbers, useSpecialChars);
                    textBoxShow.Text = password;
                    myPassTextBoxPasswordGenerateReadOnly.Texts = password;
                }

            }



        }

        public static string GeneratePassword(int length, bool useUpperCase, bool useLowerCase, bool useNumbers, bool useSpecialChars)
        {

            // Initialize character set
            string charSet = "";
            if (useLowerCase)
                charSet += LowerCaseChars;
            if (useUpperCase)
                charSet += UpperCaseChars;
            if (useNumbers)
                charSet += NumericChars;
            if (useSpecialChars)
                charSet += SpecialChars;

            // Generate password
            //var rng = new RNGCryptoServiceProvider();
            //byte[] randomBytes = new byte[length]; // 1 byte per character
            //rng.GetBytes(randomBytes);

            //var password = new char[length];
            //for (int i = 0; i < length; i++)
            //{
            //    int index = randomBytes[i] % charSet.Length;
            //    password[i] = charSet[index];
            //}


            //return new string(password);

            // Generate random indices
            int[] indices = new int[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                indices[i] = random.Next(charSet.Length);
            }

            // Generate password
            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = charSet[indices[i]];
            }

            return new string(password);



        }

        private void textBoxLengthNumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ตรวจสอบว่าตัวอักษรที่ถูกพิมพ์เข้ามาเป็นตัวเลขหรือไม่
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // ถ้าไม่ใช่ตัวเลข ให้ยกเลิกการป้อนข้อมูล
                e.Handled = true;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // คัดลอกข้อความจาก textBoxPassword ไปยัง Clipboard
            if (textBoxShow.Text == "")
            {
                MessageBox.Show("ไม่มีรหัสผ่านเด้อ");

            }
            else 
            {

                Clipboard.SetText(textBoxShow.Text);

                // แจ้งเตือนว่าข้อความถูกคัดลอกไปยัง Clipboard
                MessageBox.Show("Password copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private void buttonStyleMypassPasswordGenerate_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"อะไรหว่า: {TextboxLengthNumeric.Trim()}");
            if (numericUpDown1.Value < 8)
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณาใส่จำนวนหลักของ Password ที่ต้องการ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                MessageBox.Show("กรุณาใส่จำนวนหลักของ Password ที่ต้องการ");
            }
            else
            {
                int length = (int)numericUpDown1.Value;

                //bool useUpperCase = checkBoxUpperCaseChars.Checked;
                //bool useLowerCase = CheckBoxLowerCaseChars.Checked;
                //bool useNumbers = checkBoxNumericChars.Checked;
                //bool useSpecialChars = checkBoxSpecialChars.Checked;

                bool useUpperCase = toggleButtonMyPassUpperCaseChars.Checked;
                bool useLowerCase = toggleButtonMyPassLowerCaseChars.Checked;
                bool useNumbers = toggleButtonMyPassNumericChars.Checked;
                bool useSpecialChars = toggleButtonMyPassSpecialChars.Checked;


                if (!useUpperCase && !useLowerCase && !useNumbers && !useSpecialChars)
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "At least one character set must be selected.");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("At least one character set must be selected.");
                }
                else
                {
                    string password = GeneratePassword(length, useUpperCase, useLowerCase, useNumbers, useSpecialChars);
                    textBoxShow.Text = password;
                    myPassTextBoxPasswordGenerateReadOnly.Texts = password;
                }

            }

        }

        private void pictureBoxCopy_Click(object sender, EventArgs e)
        {
            // คัดลอกข้อความจาก textBoxPassword ไปยัง Clipboard
            if (myPassTextBoxPasswordGenerateReadOnly.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error","คุณยังไม่ได้ทำการ Generate Password");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("ไม่มีรหัสผ่านเด้อ");

            }
            else
            {

                Clipboard.SetText(myPassTextBoxPasswordGenerateReadOnly.Texts);

                // แจ้งเตือนว่าข้อความถูกคัดลอกไปยัง Clipboard
                MiniMessagerBoxCopySuccess miniMessagerBoxCopySuccess = new MiniMessagerBoxCopySuccess();
                miniMessagerBoxCopySuccess.ShowDialog();

                //MessageBox.Show("Password copied to clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void pictureBoxCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }
        }

        private void panelTop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }

        }
    }
}
