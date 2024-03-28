using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public partial class CheckActionForm : Form
    {
        private DbGenerateKey DbGenerateKey_this = new DbGenerateKey();
        private DbServerCiphertextContext dbContextServer = new DbServerCiphertextContext();
        private bool CheckMasterPassword;
        bool StagePasswordBoxUseSystemPasswordChar = true;

        public bool CheckAction { get; private set; }


        public CheckActionForm()
        {
            InitializeComponent();

        }


        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                //return builder.ToString(); //return to string
                return System.Convert.ToBase64String(bytes); //return as byte >> base 64
            }
        }



        private void buttonStyleMypassSubmit_Click(object sender, EventArgs e)
        {
            string GeneratedKey_this;
            string Sha256Hash_this;
            {
                if (myPassTextBoxMasterPassword.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก MasterPassword ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    return;
                    //MessageBox.Show("กรุณากรอก MasterPassword ด้วยครับ", "Error");
                }
                //ค้นหาว่า GenerateKey 1 มีอยุ่ไหม
                var existingGeneratekey = DbGenerateKey_this.GenerateKey.Find(1);
                if (existingGeneratekey != null)
                {
                    GeneratedKey_this = existingGeneratekey.GeneratedKey;
                    Sha256Hash_this = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts);


                    if (Sha256Hash_this == existingGeneratekey.HashMasterPassword)
                    {
                        MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("Success", "รหัสผ่านของคุณยืนยันสำเร็จ");
                        miniMessagerBoxTextBoxNormal.ShowDialog();
                        //MessageBox.Show($"รหัสผ่านของคุณยืนยันสำเร็จ", "สำเร็จ");
                        this.CheckAction = true;
                        this.Close();

                    }
                    else
                    {
                        MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Success", "รหัสผ่านของคุณไม่ถูกต้อง");
                        miniMessagerBoxTextBoxAlert.ShowDialog();
                        this.CheckAction = false;
                        //MessageBox.Show($"รหัสผ่านของคุณไม่ถูกต้อง", "ไม่สำเร็จ");
                        return;

                    }

                }
                else
                {
                    MessageBox.Show($"ไม่พบ GenerateKey ภายในเครื่องของคุณ", "ไม่สำเร็จ");
                    return;
                }
            }

        }

        private void pictureBoxEye1_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar == true)
            {
                myPassTextBoxMasterPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar == false)
            {
                myPassTextBoxMasterPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar = true;
            }

        }

        private void pictureBoxCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckActionForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // ป้องกันเสียง 'บีบ'
                buttonStyleMypassSubmit.PerformClick(); // <<< เปลี่ยนตรงนี้
            }
        }
    }
}
