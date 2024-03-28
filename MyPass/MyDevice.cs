using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public partial class MyDevice : Form
    {
        private DbAzureConnectionString dbAzureConnectionString = new DbAzureConnectionString();
        private DbGenerateKey dbGenerate = new DbGenerateKey();
        private DbMemoryMain dbMemoryMain = new DbMemoryMain();

        // private string connectionString = "DefaultEndpointsProtocol=https;AccountName=cpeproject12121;AccountKey=YvpBD5VuE7Axl6jnXZsTaElNPrS/RO3gj79pnJHzXmIRQEZKMj6q8Wg0jL7G4IKGRcusNtHC5FrP+ASt3t+SHg==;EndpointSuffix=core.windows.net";
        // private string containerName = "cpemypassproject";


        public string connectionString
        {
            get
            {
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingAzureConnectionString = dbAzureConnectionString.azureConnectionStrings.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                return DecryptDataWithAes(existingAzureConnectionString.ConnectionString, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAzureConnection);
            }
        }
        public string containerName
        {
            get
            {
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingAzureConnectionString = dbAzureConnectionString.azureConnectionStrings.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                return DecryptDataWithAes(existingAzureConnectionString.ContainerName, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAzureContainer);
            }
        }


        //private string filePathToDeleteDataDevice = @"D:\Storage";

        public event EventHandler DataUpdated;

        private DbGenerateKey dbGenerate_this = new DbGenerateKey();
        public MyDevice()
        {
            InitializeComponent();
            dataGridView1.CellClick += DataGridView1_CellContentClick;
            //dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            //dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
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



        private void button1_Click(object sender, EventArgs e)
        {

        }
        private async Task PopulateDataGridView()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            // Define column(s) for the DataGridView if not already defined
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Number", "Number"); // Add a column for the number
                dataGridView1.Columns.Add("BlobName", "Device Name"); // Add a column with a header text

                // Add a DataGridViewButtonColumn for delete button
                DataGridViewImageColumn imageColumnDelete = new DataGridViewImageColumn();
                imageColumnDelete.Name = "DeleteButtonImage";
                imageColumnDelete.HeaderText = "Delete";
                imageColumnDelete.Image = imageList1.Images[0];
                dataGridView1.Columns.Add(imageColumnDelete);
                // กำหนดการจัดวางของข้อมูลในคอลัมน์แรก (Number)
                dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // ใช้งานได้ เก็บไว้ก่อน
                dataGridView1.Columns["Number"].Width = 300;
            }

            List<string> blobNames = new List<string>();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                blobNames.Add(blobItem.Name);
            }

            dataGridView1.Rows.Clear();

            int rowNum = 1;
            foreach (var blobName in blobNames)
            {
                //dataGridView1.Rows.Add(blobName);
                dataGridView1.Rows.Add(rowNum++, blobName);
            }

            // Auto-size the column width based on the content
            //dataGridView1.AutoResizeColumns();
        }

        private async void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["DeleteButtonImage"].Index)
            {
                string blobName = dataGridView1.Rows[e.RowIndex].Cells["BlobName"].Value.ToString();
                //MessageBox.Show($"blobName: {blobName}");
                DeleteServerPartMessengerBoxAlert deleteServerPartMessengerBoxAlert = new DeleteServerPartMessengerBoxAlert();
                deleteServerPartMessengerBoxAlert.ShowDialog();

                if (deleteServerPartMessengerBoxAlert.DialogResultEditForm == true)
                {
                    var existingGenerateKey = dbGenerate_this.GenerateKey.Find(1);
                    //MessageBox.Show($"blobName : {blobName}");
                    //MessageBox.Show($"DeviceName : {existingGenerateKey.DeviceName}");
                    if (blobName == $"{existingGenerateKey.DeviceName}.csv")
                    {
                        await DeleteBlobAsyncName(this,blobName);
                        DeleteFileAllInPath(dbGenerate_this.myAppMainFolderPath);
                        DeleteFileAllInPath(dbAzureConnectionString.myAppMainFolderPath);
                        MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("Success", "ลบข้อมูลทั้งหมดของแอพ MyPass ใน Device นี้ทั้งหมดเสร็จสิ้น!");
                        miniMessagerBoxTextBoxNormal.ShowDialog();
                        Application.Exit();
                    }
                    else 
                    {
                        await DeleteBlobAsyncName(this,blobName);
                        await PopulateDataGridView();
                    }
                }
                else 
                {
                    return;
                }

            }
        }

        static async Task DeleteBlobAsyncName(MyDevice myDevice,string blobName)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(myDevice.connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(myDevice.containerName);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    await blobClient.DeleteIfExistsAsync();
                    await blobClient.DeleteIfExistsAsync();
                    AzureMessagerBoxAlertNormal azureMessagerBoxAlertNormal = new AzureMessagerBoxAlertNormal("Success", $"Blob {blobClient.Uri} deleted successfully");
                    azureMessagerBoxAlertNormal.ShowDialog();
                    //MessageBox.Show($"Blob {blobClient.Uri} deleted successfully", "Debug");
                }
                else
                {
                    //AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Blob {blobClient.Uri} does not exist. No deletion performed.");
                    //azureMessagerBoxAlert.ShowDialog();
                    //MessageBox.Show($"Blob {blobClient.Uri} does not exist. No deletion performed.", "Debug");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting blob: {ex.Message}", "Debug");
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



        public async void MyDevice_Load(object sender, EventArgs e)
        {
            await PopulateDataGridView();
            DataUpdated?.Invoke(this, EventArgs.Empty);
        }

        private void MyDevice_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
