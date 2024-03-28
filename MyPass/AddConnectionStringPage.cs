using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Security.Cryptography;
using System.IO;

namespace TestFunctionSQL
{
    public partial class AddConnectionStringPage : Form
    {
        public bool DialogResultForm;

        private AzureConnectionString azureConnection = new AzureConnectionString();

        private DbMemoryMain dbMemoryMain_this = new DbMemoryMain();
        private DbAzureConnectionString dbAzureConnectionString = new DbAzureConnectionString();

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public string vectorBase64AzureConnectionstring;
        public string vectorBase64AzureContainer;
        public AddConnectionStringPage()
        {
            InitializeComponent();
        }

        private  void buttonStyleMypassSubmit_Click(object sender, EventArgs e)
        {
            if (myPassTextBoxConnectionString.Texts == "") 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Connection String ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                DialogResultForm = false;
                return;
            }
            if (myPassTextBoxContainerName.Texts == "") 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Container Name ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                DialogResultForm = false;
                return;
            }
            if (myPassTextBoxConnectionString.Texts != "" && myPassTextBoxContainerName.Texts != "") 
            {
                // ตรวจสอบการเชื่อมต่อกับ Azure Blob Storage
                //AzureMessagerBoxConnectingCheck azureMessagerBoxConnectingCheck = new AzureMessagerBoxConnectingCheck("การตรวจสอบการเชื่อมต่อกับ Azure Storage", "กรุณารอสักครู่");
                //azureMessagerBoxConnectingCheck.ShowDialog(); 
                //await Task.Delay(TimeSpan.FromSeconds(3));
                string vectorBase64Connection;
                string vectorBase64Container;
                var existingMemoryMain = dbMemoryMain_this.memoryMains.Find(1);

                try
                {
                    var blobServiceClient = new BlobServiceClient(myPassTextBoxConnectionString.Texts);

                    // Get a reference to the container (create it if it doesn't exist)
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(myPassTextBoxContainerName.Texts);

                    if (blobContainerClient.Exists())
                    {



                        EncryptDataWithAes(myPassTextBoxContainerName.Texts, existingMemoryMain.Sha256Hash, out vectorBase64Container);


                        azureConnection.ConnectionString = EncryptDataWithAes(myPassTextBoxConnectionString.Texts, existingMemoryMain.Sha256Hash, out vectorBase64Connection);
                        vectorBase64AzureConnectionstring = vectorBase64Connection;

                        azureConnection.ContainerName = EncryptDataWithAes(myPassTextBoxContainerName.Texts, existingMemoryMain.Sha256Hash, out vectorBase64Container);
                        vectorBase64AzureContainer = vectorBase64Container;

                        dbAzureConnectionString.Database.EnsureCreated();
                        dbAzureConnectionString.Add(azureConnection);
                        dbAzureConnectionString.SaveChanges();

                        DialogResultForm = true;
                        this.Close();

                    }
                    else
                    {

                        AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Blob does not exist. No deletion performed.");
                        azureMessagerBoxAlert.ShowDialog();
                        DialogResultForm = false;
                        return;
                    }
                }
                catch(Exception ex) 
                {
                    // Handle and output any exceptions that occur during the upload
                    AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Error uploading file: {ex.Message}");
                    azureMessagerBoxAlert.ShowDialog();

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






        private void AddConnectionStringPage_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void pictureButtonClosing_Click(object sender, EventArgs e)
        {
            DialogResultForm = false;
            this.Close();
        }

        private void AddConnectionStringPage_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void AddConnectionStringPage_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void AddConnectionStringPage_MouseUp(object sender, MouseEventArgs e)
        {

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
