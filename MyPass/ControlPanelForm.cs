using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.IO;

namespace TestFunctionSQL
{
    public partial class ControlPanelForm : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
        int nLeftRect,
        int nTopRect,
        int nRightRect,
        int nBottomRect,
        int nWidthEllipse,
        int nHeightEllipse
        );

        private MainPage mainPage = new MainPage() {Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        private MyDevice myDevice = new MyDevice() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public ControlPanelForm()
        {
            InitializeComponent();
            //Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            this.ActiveControl = buttonPasswordManager;
            buttonPasswordManager.BackColor = Color.FromArgb(46, 51, 73);

            this.PnlFormLoader.Controls.Clear();
            mainPage.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(mainPage);


            var existingGenerateKey = mainPage.dbGenerate.GenerateKey.Find(1);
            labelDeviceName.Text = $"{existingGenerateKey.DeviceName}";


            mainPage.Show();

        }




        private void pictureBoxPowerButton_Click(object sender, EventArgs e)
        {
            mainPage.ClosingStepFunction();
        }

        private void buttonPasswordManager_Click(object sender, EventArgs e)
        {
            buttonPasswordManager.BackColor = Color.FromArgb(46,51,73);
            buttonDeleteServerPart.BackColor = Color.FromArgb(32, 32, 32);

            this.PnlFormLoader.Controls.Clear();
            mainPage.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(mainPage);
            mainPage.Show();

            // เรียกใช้งานเหตุการณ์ DataUpdated เพื่ออัปเดตข้อมูลใน MyDevice
            myDevice.MyDevice_Load(sender, e);
        }

        private void buttonDeleteServerPart_Click(object sender, EventArgs e)
        {
            CheckActionForm checkActionForm = new CheckActionForm();
            checkActionForm.ShowDialog();

            if (checkActionForm.CheckAction == true)
            {
                buttonPasswordManager.BackColor = Color.FromArgb(32, 32, 32);
                buttonDeleteServerPart.BackColor = Color.FromArgb(46, 51, 73);

                //this.PnlFormLoader.Controls.Clear();
                //DeleteServerPanelLoadForm deleteServerPanelLoadForm = new DeleteServerPanelLoadForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                //deleteServerPanelLoadForm.FormBorderStyle = FormBorderStyle.None;
                //this.PnlFormLoader.Controls.Add(deleteServerPanelLoadForm);
                //deleteServerPanelLoadForm.Show();
                


                this.PnlFormLoader.Controls.Clear();
                myDevice.FormBorderStyle = FormBorderStyle.None;
                this.PnlFormLoader.Controls.Add(myDevice);
                myDevice.Show();
            }
            else 
            {

         
            }

        }

        private void buttonPasswordManager_Leave(object sender, EventArgs e)
        {
            buttonPasswordManager.BackColor = Color.FromArgb(32, 32, 32);
        }

        private void buttonDeleteServerPart_Leave(object sender, EventArgs e)
        {
            //buttonDeleteServerPart.BackColor = Color.FromArgb(32, 32, 32);
        }

        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            string SetFullNameFile;
            string vectorBase64Conectionstring;
            string vectorBase64Container;
            var existingGenerateKey = mainPage.dbGenerate.GenerateKey.Find(1);
            var existingDbAzure = mainPage.dbAzureConnectionString.azureConnectionStrings.Find(1);
            var existingDbMemory = mainPage.dbMemoryMain.memoryMains.Find(1);
            SettingPage settingPage = new SettingPage(existingGenerateKey.DeviceName,existingDbAzure.ConnectionString,existingDbAzure.ContainerName);
            settingPage.ShowDialog();

            existingGenerateKey.DeviceName = settingPage.DevicenameChangeForm;

            if (settingPage.CheckAction == true)
            {
                //MessageBox.Show($"{changeDeviceName.DevicenameChangeForm}");
                await mainPage.UploadMemoryStepSetup(settingPage.DevicenameChangeForm);

                if (MainPage.CheckAzureUploadMemory == false)
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "CheckAzureUploadMemory = false");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    return;
                }
                //else 
                //{
                //    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "CheckAzureUploadMemory = false");
                //    miniMessagerBoxTextBoxAlert.ShowDialog();
                //    return;
                //}

                SetFullNameFile = $"{settingPage.DevicenameOld}.csv";
                //MessageBox.Show($"{SetFullNameFile}");
                mainPage.DeleteStepSetup(SetFullNameFile);

            }
            else 
            {
                // MessageBox.Show($"ไม่พบการเปลี่ยนแปลง");
            }

            // อัพเดท label ด้วยค่าใหม่
            labelDeviceName.Text = existingGenerateKey.DeviceName;

            if (settingPage.CheckActionForConnectionstring == true) 
            {
                var existingAzureConnectionStringmainPage = mainPage.dbAzureConnectionString.azureConnectionStrings.Find(1);
                EncryptDataWithAes(settingPage.ConnectionString_this,existingDbMemory.Sha256Hash,out vectorBase64Conectionstring);


                existingAzureConnectionStringmainPage.ConnectionString = EncryptDataWithAes(settingPage.ConnectionString_this, existingDbMemory.Sha256Hash, out vectorBase64Conectionstring);
                existingGenerateKey.VectorBase64ForAzureConnection = vectorBase64Conectionstring;

                existingAzureConnectionStringmainPage.ContainerName = EncryptDataWithAes(settingPage.ContainerName_this, existingDbMemory.Sha256Hash, out vectorBase64Container);
                existingGenerateKey.VectorBase64ForAzureContainer = vectorBase64Container;

                mainPage.dbAzureConnectionString.SaveChanges();
                mainPage.dbGenerate.SaveChanges();

                await mainPage.UploadMemoryStepSetup(settingPage.DevicenameChangeForm);
                if (MainPage.CheckAzureUploadMemory == true) 
                {
                    //MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "CheckAzureUploadMemory = true");
                    //miniMessagerBoxTextBoxAlert.ShowDialog();
                    return;
                }
                if (MainPage.CheckAzureUploadMemory == false) 
                {
                    //MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "CheckAzureUploadMemory = false");
                    //miniMessagerBoxTextBoxAlert.ShowDialog();


                    existingAzureConnectionStringmainPage.ConnectionString = settingPage.ConnectionString_Old;
                    existingAzureConnectionStringmainPage.ContainerName = settingPage.ContainerName_Old;
                    mainPage.dbAzureConnectionString.SaveChanges();

                    //MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("ใช้ตัวกเก่านะ", "ConnectionString_Old,ConnectionString_Old");
                    //miniMessagerBoxTextBoxNormal.ShowDialog();
                    return;

                }
            }
            else 
            {
            
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            PasswordGenerateForm passwordGenerateForm = new PasswordGenerateForm();
            passwordGenerateForm.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            mainPage.ExportStepSetup();
        }

        private void buttonStyleMypassPasswordGenerate_Click(object sender, EventArgs e)
        {
            PasswordGenerateForm passwordGenerateForm = new PasswordGenerateForm();
            passwordGenerateForm.ShowDialog();

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panelTopTool_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }

        }

        private void panelTopTool_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }


        }

        private void panelTopTool_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }

        }

        private void panelDevicename_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }

        }

        private void panelDevicename_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }

        }

        private void panelDevicename_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }

        }
    }
}
