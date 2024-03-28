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
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;


namespace TestFunctionSQL
{
    public partial class Login : Form
    {

        private MemoryMain newMemoryMain = new MemoryMain();

        private DbGenerateKey DbGenerateKey_this = new DbGenerateKey();
        private DbServerCiphertextContext dbContextServer = new DbServerCiphertextContext();
        private DbGenerateKeyEncrypt DbGenerateKeyEncrypt_this = new DbGenerateKeyEncrypt();
        private DbMemoryMain dbMemoryMain = new DbMemoryMain();

        private DbAzureConnectionString dbAzureConnectionString = new DbAzureConnectionString();


        bool StagePasswordBoxUseSystemPasswordChar = true;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;


        public Login()
        {
            InitializeComponent();
            //this.KeyDown += Login_KeyDown;

        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            CheckPassword();
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

        static void DecryptFile(string inputFile, string outputFile, string keyBase64)
        {
            try
            {
                using (Aes aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                    aesAlgorithm.Mode = CipherMode.CBC; // Ensure that the same mode and padding are used for decryption
                    aesAlgorithm.Padding = PaddingMode.PKCS7;

                    // Create decryptor object
                    ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                    using (FileStream inputStream = File.OpenRead(inputFile))
                    using (FileStream outputStream = File.Create(outputFile))
                    using (CryptoStream cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        cryptoStream.CopyTo(outputStream);
                    }
                }
                Console.WriteLine("File decrypted successfully.");
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine("Decryption failed: " + ex.Message);
                // อื่นๆ ตรวจสอบข้อผิดพลาดเพิ่มเติมหรือทำการจัดการข้อผิดพลาดตามต้องการ
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                // อื่นๆ ตรวจสอบข้อผิดพลาดเพิ่มเติมหรือทำการจัดการข้อผิดพลาดตามต้องการ
            }
        }


        private void pictureBoxEye_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar == true)
            {
                myPassTextBoxMasterPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar = false;
                //textBoxMasterPassword.UseSystemPasswordChar = false;
                //StagePasswordBoxUseSystemPasswordChar = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar == false)
            {
                myPassTextBoxMasterPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar = true;
            }
        }

        private void CheckPassword() 
        {
            string Sha256Hash_this;
            //ค้นหาว่า GenerateKey 1 มีอยุ่ไหม
            var existingGeneratekey = DbGenerateKey_this.GenerateKey.Find(1);
            if (myPassTextBoxMasterPassword.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก MasterPassword ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก MasterPassword ด้วยครับ", "Error");
                return;
            }
            if (existingGeneratekey != null) 
            {
                Sha256Hash_this = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts);
                if (Sha256Hash_this != existingGeneratekey.HashMasterPassword) 
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "รหัสผ่านของคุณไม่ถูกต้อง");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show($"รหัสผ่านของคุณไม่ถูกต้อง", "ไม่สำเร็จ");
                    return;
                }

            }
            try
            {

                var existingAzureConnectionString = dbAzureConnectionString.azureConnectionStrings.Find(1);
                //var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                var existingGenerateKey = DbGenerateKey_this.GenerateKey.Find(1);

                //string filePath = mainPage.dbContextServer.DatabasePath;
                string filePath = $"{existingGenerateKey.DeviceName}.csv";

                var existingConnectionstring = DecryptDataWithAes(existingAzureConnectionString.ConnectionString, ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + existingGenerateKey.GeneratedKey), existingGenerateKey.VectorBase64ForAzureConnection);
                var existingContainer = DecryptDataWithAes(existingAzureConnectionString.ContainerName, ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + existingGenerateKey.GeneratedKey), existingGenerateKey.VectorBase64ForAzureContainer);
                var blobServiceClient = new BlobServiceClient(existingConnectionstring);

                // Get a reference to the container (create it if it doesn't exist)
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(existingContainer);
                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(filePath);

                if (blobClient.Exists())
                {
                    //MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("Success", "Downloaded Server Part Successfully!");
                    //miniMessagerBoxTextBoxNormal.ShowDialog();
                }
                else 
                {
                    //MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", $"Blob {blobClient.Uri} does not exist.");
                    //miniMessagerBoxTextBoxAlert.ShowDialog();
                    DeleteFileAllInPath(DbGenerateKey_this.myAppMainFolderPath);
                    //DeleteFileAllInPath(dbAzureConnectionString.myAppMainFolderPath);
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Server Part ของคุณถูกลบ", $"สิทธิ์ในการเข้าถึงของคุณถูกระงับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();

                    //AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Server Part ของคุณถูกลบ", $"สิทธิ์ในการเข้าถึงของคุณถูกระงับ");
                    //azureMessagerBoxAlert.ShowDialog();
                    Application.Exit();


                    return;
                }
            }
            catch (Exception ex)
            {
                AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Error downloading blob: {ex.Message}");
                azureMessagerBoxAlert.ShowDialog();
                return;
            }


            if (existingGeneratekey != null)
            {
                Sha256Hash_this = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts);
                if (Sha256Hash_this == existingGeneratekey.HashMasterPassword)
                {
                    // เข้าสู่หน้า MainPage
                    //MainPage mainpage = new MainPage();
                    //mainpage.Show();
                    //this.Hide();


                    newMemoryMain.Sha256Hash = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + existingGeneratekey.GeneratedKey);
                    dbMemoryMain.memoryMains.Add(newMemoryMain);
                    dbMemoryMain.SaveChanges();


                    ControlPanelForm controlPanelForm = new ControlPanelForm();
                    controlPanelForm.Show();
                    this.Hide();

                    //mainpage.LoadDataForamDatabaseServer().Wait();
                }
                else
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "รหัสผ่านของคุณไม่ถูกต้อง");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
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

        private void buttonStyleMypass1_Click(object sender, EventArgs e)
        {
            CheckPassword();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {

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



        private void DeleteFileAllInPath(string path)
        {
            try
            {
                // ตรวจสอบว่าไดเรกทอรีนั้นมีอยู่จริงหรือไม่
                if (Directory.Exists(path))
                {
                    // ลบไดเรกทอรีและไฟล์ทั้งหมดภายใน
                    Directory.Delete(path, true);
                    Console.WriteLine($"Deleted directory and all files within: {path}");
                }
                else
                {
                    Console.WriteLine($"Directory does not exist: {path}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


        }




        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // ป้องกันเสียง 'บีบ'
                buttonStyleMypass1.PerformClick();
            }

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
