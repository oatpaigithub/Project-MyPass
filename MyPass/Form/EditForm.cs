using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Media;

namespace TestFunctionSQL
{
    public partial class EditForm : Form
    {
        bool StagePasswordBoxUseSystemPasswordChar = true;
        bool StagePasswordBoxUseSystemPasswordChar2 = true;
        bool StagePasswordBoxUseSystemPasswordChar3 = true;
        bool StagePasswordBoxUseSystemPasswordChar4 = true;

        private CiphertextLocal editedCiphertext;
        private CiphertextServer editedCiphertextServer;

        private MemoryMain newMemoryMain = new MemoryMain();

        private DbGenerateKey DbGenerateKey_this = new DbGenerateKey();
        private DbLocalCiphertextContext DbLocalCiphertextContext_this = new DbLocalCiphertextContext();
        private DbGenerateKey dbGenerateKey_this = new DbGenerateKey();
        private DbMemoryMain dbMemoryMain = new DbMemoryMain();

        public CiphertextLocal EditedCiphertext { get; private set; }
        public CiphertextServer EditedCiphertextServer { get; private set; }

        public string ApplicationTypeIndex;

        public string CiphertextPassword_this;

        private string CiphertextPassword_Setup;
        private string CiphertextPasswordConnect;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        public EditForm(CiphertextLocal ciphertext, CiphertextServer ciphertextServer)
        {
            InitializeComponent();


            // กำหนดค่าเริ่มต้นของฟอร์มด้วยข้อมูลที่มีอยู่
            editedCiphertext = ciphertext;
            editedCiphertextServer = ciphertextServer;

            var existingGenerateKey = dbGenerateKey_this.GenerateKey.Find(1);
            var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);


            //textBoxTitle.Text = editedCiphertext.CiphertextTitle;
            //textBoxUsername.Text = editedCiphertext.CiphertextUsername;

            //ApplicationTypeIndex = editedCiphertext.UsernameConnectURL.Trim();


            myPassTextBoxTitle.Texts = editedCiphertext.CiphertextTitle;
            myPassTextBoxUsername.Texts = editedCiphertext.CiphertextUsername;
            myPassTextBoxURL.Texts = editedCiphertext.URL;

            CiphertextPasswordConnect = editedCiphertext.CiphertextPasswordLocal + editedCiphertextServer.CiphertextPasswordServer;

            CiphertextPassword_Setup = DecryptDataWithAes(CiphertextPasswordConnect, existingMemoryMain.Sha256Hash,editedCiphertext.vectorBase64);

            myPassTextBoxCurrentPasswordReadOnly.Texts = CiphertextPassword_Setup;
            //myPassTextBoxNewPassword.Texts = CiphertextPassword_Setup;
            //myPassTextBoxConfirmNewPassword.Texts = CiphertextPassword_Setup;


            ApplicationTypeIndex = editedCiphertext.UsernameConnectURL.Trim();


        }

        private void Submitbutton_Click(object sender, EventArgs e)
        {
            string keyBase64;
            string vectorBase64;
            var existingGenerateKey = DbGenerateKey_this.GenerateKey.Find(1);
            var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
            if (existingGenerateKey != null)
            {
                keyBase64 = existingMemoryMain.Sha256Hash;
            }
            else 
            {
                MessageBox.Show("ไม่พบ Aeskey ภายในเครื่องของคุณ", "ไม่สำเร็จ");
                return;
            }


            editedCiphertext.CiphertextTitle = textBoxTitle.Text;
            editedCiphertext.CiphertextUsername = textBoxUsername.Text;
            //editedCiphertext.URL = $"{comboBoxUserType.SelectedItem.ToString().Trim()}";

            if (comboBoxUserType.SelectedItem.ToString().Trim() == "Facebook")
            {
                editedCiphertext.URL = "https://facebook.com/";
                editedCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{editedCiphertext.URL}";
            }
            else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Youtube")
            {
                editedCiphertext.URL = "https://www.youtube.com/";
                editedCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{editedCiphertext.URL}";
            }
            else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Discord")
            {
                editedCiphertext.URL = "https://discord.com/";
                editedCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{editedCiphertext.URL}";
            }
            else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Twitter")
            {
                editedCiphertext.URL = "https://twitter.com/";
                editedCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{editedCiphertext.URL}";

            } else 
            {
                editedCiphertext.URL = $"{comboBoxUserType.SelectedItem.ToString()}";
                editedCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{comboBoxUserType.SelectedItem.ToString()}";
            }





            //editedCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{comboBoxUserType.SelectedItem.ToString().Trim()}";

            var existingCiphertext = DbLocalCiphertextContext_this.Ciphertexts.FirstOrDefault(c => c.URL.Trim() == editedCiphertext.URL.Trim());
            var existingCiphertext2 = DbLocalCiphertextContext_this.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == editedCiphertext.UsernameConnectURL);
            var existingCiphertext3 = DbLocalCiphertextContext_this.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == ApplicationTypeIndex);

            if (textBoxPassword.Text == "")
            {
                MessageBox.Show("กรุณากรอก Password ด้วยครับ", "Error");
                return;
            }
            if (textBoxTitle.Text == "")
            {
                MessageBox.Show("กรุณากรอก Title ด้วยครับ", "Error");
                return;
            }
            if (textBoxUsername.Text == "")
            {
                MessageBox.Show("กรุณากรอก Username ด้วยครับ", "Error");
                return;
            }
            if (comboBoxUserType.SelectedItem == null || comboBoxUserType.SelectedItem.ToString().Trim() == "...Add")
            {
                MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                return;
            }
            else 
            {
                if (existingCiphertext2 == null)
                {
                    CiphertextPassword_this = EncryptDataWithAes(textBoxPassword.Text, keyBase64, out vectorBase64);
                    editedCiphertext.vectorBase64 = vectorBase64;

                    EditedCiphertext = editedCiphertext;


                    this.Close();

                }
                else
                {
                    MessageBox.Show($"{ApplicationTypeIndex}", "ApplicationTypeIndex");
                    MessageBox.Show($"{editedCiphertext.UsernameConnectURL}", "Edite UsernameConnectURL");
                    if (ApplicationTypeIndex == editedCiphertext.UsernameConnectURL)
                    {
                        CiphertextPassword_this = EncryptDataWithAes(textBoxPassword.Text, keyBase64, out vectorBase64);
                        editedCiphertext.vectorBase64 = vectorBase64;

                        EditedCiphertext = editedCiphertext;


                        this.Close();


                    }
                    else
                    {
                        MessageBox.Show("เจอ User+ApplicationType ที่ตรงกับฐานข้อมูล");
                        return;
                    }

                }

            }




        }

        private static string EncryptDataWithAes(string plainText, string keyBase64, out string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.GenerateIV();
                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");

                //set the parameters with out keyword
                vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                byte[] encryptedData;

                //Encryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                        encryptedData = ms.ToArray();
                    }
                }

                return Convert.ToBase64String(encryptedData);
            }
        }

        private static string DecryptDataWithAes(string cipherText, string keyBase64, string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");
                Console.WriteLine($"Aes Block Size : {aesAlgorithm.BlockSize}");


                // Create decryptor object
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                byte[] cipher = Convert.FromBase64String(cipherText);

                //Decryption will be done in a memory stream through a CryptoStream object
                using (MemoryStream ms = new MemoryStream(cipher))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }





        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // MessageBox.Show("EditForm ถูกปิดลง", "Debug");
            return;
        }

        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar == true)
            {
                textBoxPassword.UseSystemPasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar == false)
            {
                textBoxPassword.UseSystemPasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar = true;
            }

        }




        private void comboBoxUserType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBoxUserType.SelectedItem.ToString().Trim() == "Add...") 
            {
                AddURLForm addURLForm = new AddURLForm();
                addURLForm.ShowDialog();
                if (addURLForm.URLinput == null)
                {
                    MessageBox.Show($"{addURLForm.URLinput}", "มันว่างเปล่า");
                    return;
                }
                else 
                {
                    MessageBox.Show($"{addURLForm.URLinput}", "Link URL ของคุณ");
                    comboBoxUserType.Items.Insert(comboBoxUserType.Items.Count - 1, addURLForm.URLinput);
                    comboBoxUserType.SelectedItem = addURLForm.URLinput;
                }
            }

        }

        static bool IsUrlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps) &&
           (result.Host.EndsWith(".com", StringComparison.OrdinalIgnoreCase));
        }

        private void SubmitStepSteup() 
        {
            {
                string keyBase64;
                string vectorBase64;
                var existingGenerateKey = DbGenerateKey_this.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                if (existingGenerateKey != null)
                {
                    keyBase64 = existingMemoryMain.Sha256Hash;
                }
                else
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบ Aeskey ภายในเครื่องของคุณ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    return;
                }


                editedCiphertext.CiphertextTitle = myPassTextBoxTitle.Texts;
                editedCiphertext.CiphertextUsername = myPassTextBoxUsername.Texts;
                editedCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{myPassTextBoxURL.Texts}";


                var existingCiphertext = DbLocalCiphertextContext_this.Ciphertexts.FirstOrDefault(c => c.URL.Trim() == editedCiphertext.URL.Trim());
                var existingCiphertext2 = DbLocalCiphertextContext_this.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == editedCiphertext.UsernameConnectURL);
                var existingCiphertext3 = DbLocalCiphertextContext_this.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == ApplicationTypeIndex);



                if (myPassTextBoxNewPassword.Texts == "" && myPassTextBoxConfirmNewPassword.Texts == "")
                {
                    if (existingCiphertext2 == null)
                    {
                        // Debug
                        //MessageBox.Show($"{myPassTextBoxCurrentPasswordReadOnly.Texts}", "myPassTextBoxCurrentPasswordReadOnly");
                        CiphertextPassword_this = EncryptDataWithAes(myPassTextBoxCurrentPasswordReadOnly.Texts, keyBase64, out vectorBase64);
                        editedCiphertext.vectorBase64 = vectorBase64;

                        editedCiphertext.URL = myPassTextBoxURL.Texts;

                        EditedCiphertext = editedCiphertext;


                        this.Close();

                    }
                    else
                    {
                        // Debug
                        //MessageBox.Show($"{ApplicationTypeIndex}", "ApplicationTypeIndex");
                        //MessageBox.Show($"UsernameConnectURL : {editedCiphertext.UsernameConnectURL}", "Edite UsernameConnectURL");
                        if (ApplicationTypeIndex == editedCiphertext.UsernameConnectURL)
                        {
                            CiphertextPassword_this = EncryptDataWithAes(myPassTextBoxCurrentPasswordReadOnly.Texts, keyBase64, out vectorBase64);
                            editedCiphertext.vectorBase64 = vectorBase64;

                            editedCiphertext.URL = myPassTextBoxURL.Texts;

                            EditedCiphertext = editedCiphertext;


                            this.Close();


                        }
                        else
                        {
                            editedCiphertext.UsernameConnectURL = $"{ApplicationTypeIndex}";
                            MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "เจอ User + ApplicationType ที่ตรงกับฐานข้อมูล");
                            miniMessagerBoxTextBoxAlert.ShowDialog();
                            // MessageBox.Show("เจอ User + ApplicationType ที่ตรงกับฐานข้อมูล");
                            return;
                        }

                    }
                }
                if (myPassTextBoxTitle.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Title ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("กรุณากรอก Title ด้วยครับ", "Error");
                    return;
                }
                if (myPassTextBoxUsername.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Username ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("กรุณากรอก Username ด้วยครับ", "Error");
                    return;
                }

                //if (myPassTextBoxNewPassword.Texts == "")
                //{
                //    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก New Password ด้วยครับ");
                //    miniMessagerBoxTextBoxAlert.ShowDialog();
                //    //MessageBox.Show("NewPassword และ Re-NewPassword ไม่ตรงกัน ", "Error");
                //    return;
                //}

                //if (myPassTextBoxConfirmNewPassword.Texts == "")
                //{
                //    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Confirm New Password ด้วยครับ");
                //    miniMessagerBoxTextBoxAlert.ShowDialog();
                //    //MessageBox.Show("NewPassword และ Re-NewPassword ไม่ตรงกัน ", "Error");
                //    return;
                //}



                if (myPassTextBoxURL.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก URL ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                    return;
                }
                if (myPassTextBoxNewPassword.Texts != myPassTextBoxConfirmNewPassword.Texts)
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "New Password และ Confirm New Password ไม่ตรงกัน ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("NewPassword และ Re-NewPassword ไม่ตรงกัน ", "Error");
                    return;
                }
                if(myPassTextBoxNewPassword.Texts == myPassTextBoxConfirmNewPassword.Texts && myPassTextBoxNewPassword.Texts != "" && myPassTextBoxConfirmNewPassword.Texts != "")
                {
                    if (existingCiphertext2 == null)
                    {
                        // Debug
                        //MessageBox.Show($"myPassTextBoxNewPassword:{myPassTextBoxNewPassword.Texts}", "myPassTextBoxNewPassword");
                        CiphertextPassword_this = EncryptDataWithAes(myPassTextBoxNewPassword.Texts, keyBase64, out vectorBase64);
                        editedCiphertext.vectorBase64 = vectorBase64;

                        editedCiphertext.URL = myPassTextBoxURL.Texts;

                        EditedCiphertext = editedCiphertext;


                        this.Close();

                    }
                    else
                    {
                        // Debug
                        //MessageBox.Show($"{ApplicationTypeIndex}", "ApplicationTypeIndex");
                        //MessageBox.Show($"UsernameConnectURL : {editedCiphertext.UsernameConnectURL}", "Edite UsernameConnectURL");
                        if (ApplicationTypeIndex == editedCiphertext.UsernameConnectURL)
                        {
                            CiphertextPassword_this = EncryptDataWithAes(myPassTextBoxNewPassword.Texts, keyBase64, out vectorBase64);
                            editedCiphertext.vectorBase64 = vectorBase64;

                            editedCiphertext.URL = myPassTextBoxURL.Texts;

                            EditedCiphertext = editedCiphertext;


                            this.Close();


                        }
                        else
                        {
                            editedCiphertext.UsernameConnectURL = $"{ApplicationTypeIndex}";
                            MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "เจอ User + ApplicationType ที่ตรงกับฐานข้อมูล");
                            miniMessagerBoxTextBoxAlert.ShowDialog();
                            //MessageBox.Show("เจอ User + ApplicationType ที่ตรงกับฐานข้อมูล");
                            return;
                        }

                    }

                }
                //if (comboBoxUserType.SelectedItem == null || comboBoxUserType.SelectedItem.ToString().Trim() == "...Add")
                //{
                //    MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                //    return;
                //}





            }


        }

        private void buttonStyleMypassSubmit_Click(object sender, EventArgs e)
        {
            if (myPassTextBoxConfirmNewPassword.Texts != "" && myPassTextBoxNewPassword.Texts != "" && myPassTextBoxNewPassword.Texts == myPassTextBoxConfirmNewPassword.Texts)
            {
                // เล่นเสียงแจ้งเตือน
                SystemSounds.Exclamation.Play();
                EditFormMessengerBoxAlertCopyPart editFormMessengerBoxAlertCopyPart = new EditFormMessengerBoxAlertCopyPart(CiphertextPassword_Setup);
                editFormMessengerBoxAlertCopyPart.ShowDialog();
                if (editFormMessengerBoxAlertCopyPart.DialogResultEditForm == true)
                {
                    EditFormMessengerBoxAlert editFormMessengerBoxAlert = new EditFormMessengerBoxAlert();
                    editFormMessengerBoxAlert.ShowDialog();
                    if (editFormMessengerBoxAlert.DialogResultEditForm == true)
                    {
                        SubmitStepSteup();
                    }
                    else
                    {
                        return;
                    }

                }
                else
                {
                    return;
                }

            }
            if (myPassTextBoxConfirmNewPassword.Texts != "" && myPassTextBoxNewPassword.Texts != "" && myPassTextBoxNewPassword.Texts != myPassTextBoxConfirmNewPassword.Texts) 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "New Password และ Re-NewPassword ไม่ตรงกัน ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("New Password และ Re-NewPassword ไม่ตรงกัน ", "Error");
                return;
            }
            if (myPassTextBoxConfirmNewPassword.Texts == "" && myPassTextBoxConfirmNewPassword.Texts == "") 
            {
                EditFormMessengerBoxAlert editFormMessengerBoxAlert = new EditFormMessengerBoxAlert();
                editFormMessengerBoxAlert.ShowDialog();
                if (editFormMessengerBoxAlert.DialogResultEditForm == true)
                {
                    SubmitStepSteup();
                }
                else
                {
                    return;
                }
            }




        }

        private void pictureBoxEyeWhite_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar == true)
            {
                myPassTextBoxNewPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar == false)
            {
                myPassTextBoxNewPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar = true;
            }

        }
        private void pictureBoxEyeWhite2_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar2 == true)
            {
                myPassTextBoxConfirmNewPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar2 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar2 == false)
            {
                myPassTextBoxConfirmNewPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar2 = true;
            }

        }
        private void pictureBoxEyeBox3_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar3 == true)
            {
                myPassTextBoxCurrentPasswordReadOnly.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar3 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar3 == false)
            {
                myPassTextBoxCurrentPasswordReadOnly.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar3 = true;
            }

        }



        private void pictureBoxClosingButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStyleMypassPasswordGenerate_Click(object sender, EventArgs e)
        {
            PasswordGenerateForm passwordGenerateForm = new PasswordGenerateForm();
            passwordGenerateForm.ShowDialog();

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

        private void pictureBoxCopyWhite_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(myPassTextBoxCurrentPasswordReadOnly.Texts);

            // แจ้งเตือนว่าข้อความถูกคัดลอกไปยัง Clipboard
            MiniMessagerBoxCopySuccess miniMessagerBoxCopySuccess = new MiniMessagerBoxCopySuccess();
            miniMessagerBoxCopySuccess.ShowDialog();
        }

        private void pictureBoxEyeWhite3_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar4 == true)
            {
                myPassTextBoxCurrentPasswordReadOnly.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar4 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar4 == false)
            {
                myPassTextBoxCurrentPasswordReadOnly.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar4 = true;
            }

        }
    }
}
