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
using CsvHelper;
using System.Globalization;

namespace TestFunctionSQL
{
    public partial class CreateMasterPassWord : Form
    {

        public GenerateKey newGenerateKey = new GenerateKey();
        public MemoryMain newMemoryMain = new MemoryMain();

        private DbGenerateKey dbGenerate = new DbGenerateKey();
        private DbInMemoryDataBaseServer dbImportMemory = new DbInMemoryDataBaseServer();
        private DbMemoryMain dbMemoryMain = new DbMemoryMain();

        //private string HashName;

        //private string Aeskey;

        bool StagePasswordBoxUseSystemPasswordCharEye1 = true;
        bool StagePasswordBoxUseSystemPasswordCharEye2 = true;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;


        public CreateMasterPassWord()
        {
            InitializeComponent();
            //this.KeyPress += CreateMasterPassWord_KeyPress;
            //this.KeyDown += CreateMasterPassWord_KeyDown;
        }

        private void SubmitCreateGeneratekey_Click(object sender, EventArgs e)
        {
            if (textBoxDeviceName.Text == "")
            {
                MessageBox.Show("กรุณากรอก DeviceName ด้วยครับ", "Error");
                return;
            }
            if (textBoxMasterPassword.Text == "")
            {
                MessageBox.Show("กรุณากรอก MasterPassword ด้วยครับ", "Error");
                return;
            }
            if (textBoxReMasterPassword.Text == "")
            {
                MessageBox.Show("กรุณากรอก ReMasterPassword ด้วยครับ", "Error");
                return;
            }
            if (textBoxMasterPassword.Text == textBoxReMasterPassword.Text)
            {
                //1.Genkey64-->keyBase64
                using (Aes aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.KeySize = 256;
                    aesAlgorithm.GenerateKey();
                    newGenerateKey.GeneratedKey = Convert.ToBase64String(aesAlgorithm.Key);
                }

                //2.เชื่อม Master กับ Keybase64
                //newGenerateKey.AesKey = textBoxMasterPassword.Text + newGenerateKey.GeneratedKey;

                //3.Hash Sha256
                //newGenerateKey.Sha256Hash = ComputeSha256Hash(textBoxMasterPassword.Text + newGenerateKey.GeneratedKey);

                //4. Hash MasterPassword
                newGenerateKey.HashMasterPassword = ComputeSha256Hash(textBoxMasterPassword.Text);

                //5. Hash NameFile
                //newGenerateKey.HashNameFile = ComputeSha256Hash(GenerateRandomKey(32));
                newGenerateKey.HashNameFile = SanitizeSha256(ComputeSha256Hash(GenerateRandomKey(64)));

                // แปลงค่า Sha256 ที่มีตัวอักษรพิเศษ
                // newGenerateKey.ChangeSha256HashForUse = SanitizeSha256(newGenerateKey.Sha256Hash);
                // เก็บ Device Name
                newGenerateKey.DeviceName = textBoxDeviceName.Text;
                //สร้าง db ในการเก็บ Generatekey
                dbGenerate.Database.EnsureCreated();
                dbGenerate.GenerateKey.Add(newGenerateKey);
                dbGenerate.SaveChanges();
                MessageBox.Show("Key ได้ถูกสร้างขึ้นสำเร็จแล้ว", "Successfully Generated Key !");

                DbServerCiphertextContext dbContextServer = new DbServerCiphertextContext();
                // เข้าสู่หน้า Form1
                //MainPage form1 = new MainPage();
                //form1.Show();
                //this.Hide();


                // Control Panel
                ControlPanelForm controlPanelForm = new ControlPanelForm();
                controlPanelForm.Show();
                this.Hide();


            }
            else
            {
                MessageBox.Show("รหัสผ่านของคุณไม่ตรงกันกรุณากรอกใหม่", "Error");
            }
        }
        // Feature 
        static string GenerateRandomKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            byte[] data = new byte[length];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(data);
            }

            char[] key = new char[length];
            for (int i = 0; i < length; i++)
            {
                key[i] = chars[data[i] % chars.Length];
            }

            return new string(key);
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
        private string SanitizeSha256(string inputSha256)
        {
            // แทนที่อักขระที่ไม่ถูกต้องด้วย "_"
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
            {
                inputSha256 = inputSha256.Replace(invalidChar, '_');
            }
            return inputSha256;
        }




        // ตกแต่ง

        private void pictureBoxEye1_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordCharEye1 == true)
            {
                textBoxMasterPassword.UseSystemPasswordChar = false;
                StagePasswordBoxUseSystemPasswordCharEye1 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordCharEye1 == false)
            {
                textBoxMasterPassword.UseSystemPasswordChar = true;
                StagePasswordBoxUseSystemPasswordCharEye1 = true;
            }
        }

        private void pictureBoxEye2_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordCharEye2 == true)
            {
                textBoxReMasterPassword.UseSystemPasswordChar = false;
                StagePasswordBoxUseSystemPasswordCharEye2 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordCharEye2 == false)
            {
                textBoxReMasterPassword.UseSystemPasswordChar = true;
                StagePasswordBoxUseSystemPasswordCharEye1 = true;
            }

        }

        private void Backup_Click(object sender, EventArgs e)
        {
            BackupPage backupPage = new BackupPage();
            backupPage.ShowDialog();
            // ปิดหน้าต่างปัจจุบัน
            this.Hide();
        }

        private void buttonStyleMypassSubmit_Click(object sender, EventArgs e)
        {

            if (myPassTextBoxDevicename.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก DeviceName ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก DeviceName ด้วยครับ", "Error");
                return;
            }
            if (myPassTextBoxMasterPassword.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก MasterPassword ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก MasterPassword ด้วยครับ", "Error");
                return;
            }
            if (myPassTextBoxReMasterPassword.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก ReMasterPassword ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก ReMasterPassword ด้วยครับ", "Error");
                return;
            }
            if (myPassTextBoxMasterPassword.Texts != myPassTextBoxReMasterPassword.Texts) 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "MasterPassword และ ReMasterPassword ไม่ตรงกัน");
                miniMessagerBoxTextBoxAlert.ShowDialog();

                //MessageBox.Show("MasterPassword และ ReMasterPassword ไม่สอดคล้องกัน");
                return;

            }
            if (myPassTextBoxMasterPassword.Texts == myPassTextBoxReMasterPassword.Texts)
            {
                //AddConnectionStringPage addConnectionStringPage = new AddConnectionStringPage();
                //addConnectionStringPage.ShowDialog();

                //1.Genkey64-->keyBase64
                using (Aes aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.KeySize = 256;
                    aesAlgorithm.GenerateKey();
                    newGenerateKey.GeneratedKey = Convert.ToBase64String(aesAlgorithm.Key);
                }

                //3.Hash Sha256
                // ==== Memory =====================
                newMemoryMain.Sha256Hash = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + newGenerateKey.GeneratedKey);
                dbMemoryMain.memoryMains.Add(newMemoryMain);
                dbMemoryMain.SaveChanges();

                AddConnectionStringPage addConnectionStringPage = new AddConnectionStringPage();
                addConnectionStringPage.ShowDialog();

                if (addConnectionStringPage.DialogResultForm == true)
                {
                    //1.Genkey64-->keyBase64
                    //using (Aes aesAlgorithm = Aes.Create())
                    //{
                    //    aesAlgorithm.KeySize = 256;
                    //    aesAlgorithm.GenerateKey();
                    //    newGenerateKey.GeneratedKey = Convert.ToBase64String(aesAlgorithm.Key);
                    //}

                    //2.เชื่อม Master กับ Keybase64
                    //newGenerateKey.AesKey = textBoxMasterPassword.Text + newGenerateKey.GeneratedKey;

                    //3.Hash Sha256
                    // ==== Memory =====================
                    //newMemoryMain.Sha256Hash = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + newGenerateKey.GeneratedKey);
                    //dbMemoryMain.memoryMains.Add(newMemoryMain);
                    //dbMemoryMain.SaveChanges();
                    // newGenerateKey.Sha256Hash = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + newGenerateKey.GeneratedKey);

                    //4. Hash MasterPassword
                    newGenerateKey.HashMasterPassword = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts);

                    //5. Hash NameFile
                    //newGenerateKey.HashNameFile = ComputeSha256Hash(GenerateRandomKey(32));
                    newGenerateKey.HashNameFile = SanitizeSha256(ComputeSha256Hash(GenerateRandomKey(64)));

                    // แปลงค่า Sha256 ที่มีตัวอักษรพิเศษ
                    //newGenerateKey.ChangeSha256HashForUse = SanitizeSha256(newGenerateKey.Sha256Hash);

                    // เก็บ Device Name
                    newGenerateKey.DeviceName = myPassTextBoxDevicename.Texts;

                    newGenerateKey.VectorBase64ForAzureConnection = addConnectionStringPage.vectorBase64AzureConnectionstring;
                    newGenerateKey.VectorBase64ForAzureContainer = addConnectionStringPage.vectorBase64AzureContainer;

                    //สร้าง db ในการเก็บ Generatekey
                    dbGenerate.Database.EnsureCreated();
                    dbGenerate.GenerateKey.Add(newGenerateKey);
                    dbGenerate.SaveChanges();
                    // MessageBox.Show("Key ได้ถูกสร้างขึ้นสำเร็จแล้ว", "Successfully Generated Key !");

                    DbServerCiphertextContext dbContextServer = new DbServerCiphertextContext();
                    // เข้าสู่หน้า Form1
                    //MainPage form1 = new MainPage();
                    //form1.Show();
                    //this.Hide();

                    // Control Panel
                    ControlPanelForm controlPanelForm = new ControlPanelForm();
                    controlPanelForm.Show();
                    this.Hide();
                }
                else 
                {
                    return;
                }
              

            }
        }

        private byte[] GenerateCsvFromDbContext()
        {
            using (var memoryStream = new MemoryStream())
            using (var streamWriter = new StreamWriter(memoryStream))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                // ดึงข้อมูลจาก dbAppContext และเขียนลงใน CSV
                csvWriter.WriteRecords(dbImportMemory.InMemoryDataBase.ToList());
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
        }

        private void UploadServer() 
        {
            //var existingGenerateKey = dbGenerate.GenerateKey.Find(1);

            //string Setname = $"{existingGenerateKey.DeviceName}.csv";
            //byte[] csvData = GenerateCsvFromDbContext();
            //byte[] encryptCsvData = EncryptData(csvData, existingGenerateKey.Sha256Hash, out VectorBase64ForData);
            //existingGenerateKey.VectorBase64ForAllData = VectorBase64ForData;
            //dbGenerate.SaveChanges();

            //PathLocalForuse = await dbLocalCiphertextContext.SetCombinePath();
            //PathLocalEncrypt = await dbLocalCiphertextContentEncrypt.SetCombinePath();

            //await UploadFileToBlobAsyncMemoryVersion(this, encryptCsvData, Setname);
            //MessageBox.Show("Upload Part Server สำเร็จ", "Debug");
            //EncryptFile(PathLocalForuse, existingGenerateKey.Sha256Hash, PathLocalEncrypt, out VectorBase64ForFile);
            //existingGenerateKey.VectorBase64ForFile = VectorBase64ForFile;
            //dbGenerate.SaveChanges();

            //DeleteFileDirectly(PathLocalForuse);

            //DeleteDbFile(dbImportFile.DatabasePath);
            //MessageBox.Show("ไฟล์ Import ถูกลบแล้ว", "Debug");

        }




        private void buttonStyleMypassBackUp_Click(object sender, EventArgs e)
        {
            BackupPage backupPage = new BackupPage();
            backupPage.ShowDialog();
            // ปิดหน้าต่างปัจจุบัน
            if (backupPage.CheckCancel == false) 
            {
                this.Hide();
            }
            //this.Hide();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordCharEye1 == true)
            {
                myPassTextBoxMasterPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordCharEye1 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordCharEye1 == false)
            {
                myPassTextBoxMasterPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordCharEye1 = true;
            }


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordCharEye2 == true)
            {
                myPassTextBoxReMasterPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordCharEye2 = false;
            }
            else if (StagePasswordBoxUseSystemPasswordCharEye2 == false)
            {
                myPassTextBoxReMasterPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordCharEye2 = true;
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateMasterPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true; // ป้องกันเสียง 'บีบ'
                buttonStyleMypassSubmit.PerformClick(); // <<< เปลี่ยนตรงนี้
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
