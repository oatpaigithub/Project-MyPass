using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CsvHelper;

namespace TestFunctionSQL
{
    public partial class BackupPage : Form
    {
        public GenerateKey newGenerateKey = new GenerateKey();
        private MemoryMain newMemoryMain = new MemoryMain();
        private AzureConnectionString azureConnection = new AzureConnectionString();
       
        
        
        private DbMemoryMain dbMemoryMain = new DbMemoryMain();
        private DbGenerateKey dbGenerate = new DbGenerateKey();
        private DbLocalCiphertextContext dbLocalCiphertextContext = new DbLocalCiphertextContext();
        private DbLocalCiphertextContentEncrypt dbLocalCiphertextContentEncrypt = new DbLocalCiphertextContentEncrypt();
        private DbInMemoryDataBaseServer dbImportMemory = new DbInMemoryDataBaseServer();
        private DbImportFile dbImportFile = new DbImportFile();
        private DbAzureConnectionString dbAzureConnectionString = new DbAzureConnectionString();
        private string selectedFilePath;
        private string destinationFilePath;



        public bool CheckCancel = false;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;


        // Server Part Azure Server

        // private const string connectionString = "DefaultEndpointsProtocol=https;AccountName=cpeproject12121;AccountKey=YvpBD5VuE7Axl6jnXZsTaElNPrS/RO3gj79pnJHzXmIRQEZKMj6q8Wg0jL7G4IKGRcusNtHC5FrP+ASt3t+SHg==;EndpointSuffix=core.windows.net";
        // private const string containerName = "cpemypassproject";

        public string connectionString
        {
            get
            {
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingAzureConnectionString = dbAzureConnectionString.azureConnectionStrings.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                //DecryptDataWithAes(existingAzureConnectionString.ConnectionString, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAzureConnection);
                //return $"{existingAzureConnectionString.ConnectionString}";
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
                //DecryptDataWithAes(existingAzureConnectionString.ContainerName, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAzureContainer);
                //return $"{existingAzureConnectionString.ContainerName}";
                return DecryptDataWithAes(existingAzureConnectionString.ContainerName, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAzureContainer);

            }
        }


        //private const string filePath = "C:\\Users\\phitchayarat_piraban\\Desktop\\CPETEST3\\12121.txt";//Change to that location file

        private static bool isUploading = false;
        private static bool isDowloading = false;
        private static bool isDeleting = false;

        //

        private string ImportPath = "D:\\Storage\\Import";


        bool StagePasswordBoxUseSystemPasswordCharEye1 = true;
        bool StagePasswordBoxUseSystemPasswordCharEye2 = true;

        public BackupPage()
        {
            InitializeComponent();
        }

        private void ButtonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // ตั้งค่าสำหรับการให้ผู้ใช้เลือกไฟล์
            openFileDialog.Filter = "ไฟล์ฐานข้อมูล (*.db)|*.db";
            openFileDialog.Title = "เลือกไฟล์ที่ต้องการ import";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // นำเข้าไฟล์ที่เลือก
                //string selectedFilePath = openFileDialog.FileName;
                selectedFilePath = openFileDialog.FileName;

                // แสดง Path ใน TextBox
                textBoxBrowsePath.Text = selectedFilePath;

                // ทำงานกับไฟล์ที่นำเข้า ระวัง !!!!!!!<------------------------------------------------
                //ImportFile(selectedFilePath);
            }

        }

        private void ImportFile(string filePath)
        {
            try
            {

                // ตรวจสอบว่าไฟล์มีอยู่จริงหรือไม่
                if (File.Exists(filePath))
                {
                    // ทำงานกับไฟล์ที่นำเข้าได้ที่นี่
                    // เช่น คัดลอกไฟล์ไปยังโพลเดอร์ที่คุณต้องการ
                    string destinationFolderPath = dbImportFile.DatabasePath;
                    destinationFilePath = Path.Combine(destinationFolderPath, Path.GetFileName(filePath));

                    File.Copy(filePath, destinationFilePath, true);
                    //MessageBox.Show($"destinationFilePath:{destinationFilePath}", "แจ้ง destinationFilePath");
                    //MessageBox.Show("นำเข้าไฟล์เรียบร้อยแล้ว");
                }
                else
                {
                    MessageBox.Show("ไฟล์ที่เลือกไม่มีอยู่จริง");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการนำเข้าไฟล์: {ex.Message}");
            }
        }


        private void BackupPage_FormClosing(object sender, EventArgs e)
        {
            return;
        }




        private void DeleteDbFile(string folderPath)
        {
            string[] dbFiles = Directory.GetFiles(folderPath, "*.db");

            foreach (var dbFile in dbFiles)
            {
                try
                {
                    File.Delete(dbFile);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting file {dbFile}: {ex.Message}", "Error");
                    // ทำตามความต้องการ ยกเลิกการดำเนินการหรือทำอย่างอื่น
                }
            }
        }

        private void DeleteFileDirectly(string filePath)
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"ไฟล์ {filePath} ถูกลบแล้ว");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"เกิดข้อผิดพลาดในการลบไฟล์: {ex.Message}");
            }
        }





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

        // Function ส่วนของ กระบวนการ เข้ารหัส ต่างๆ
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
        private static void Split(string TextToSplit, out string First, out string Second)
        {
            int firstHalfLength = (int)(TextToSplit.Length / 2);
            int secondHalfLength = TextToSplit.Length - firstHalfLength;
            First = TextToSplit.Substring(0, firstHalfLength);
            Second = TextToSplit.Substring(firstHalfLength, secondHalfLength);
        }
        private static byte[] EncryptData(byte[] data, string keyBase64, out string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.GenerateIV();
                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");

                // Set the parameters with out keyword
                vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                // Encrypt the data
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                    }
                    return memoryStream.ToArray();
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
        private static void EncryptFile(string sourceFilePath, string keyBase64, string destinationFilePath, out string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.GenerateIV();
                Console.WriteLine($"Aes Cipher Mode : {aesAlgorithm.Mode}");
                Console.WriteLine($"Aes Padding Mode: {aesAlgorithm.Padding}");
                Console.WriteLine($"Aes Key Size : {aesAlgorithm.KeySize}");

                // Set the parameters with out keyword
                vectorBase64 = Convert.ToBase64String(aesAlgorithm.IV);

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                // Encrypt the file
                using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream destinationFileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(destinationFileStream, encryptor, CryptoStreamMode.Write))
                        {
                            byte[] buffer = new byte[1024]; // or any other suitable buffer size
                            int bytesRead;

                            while ((bytesRead = sourceFileStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                cryptoStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }




        // Server Azure Action ==============================================================


        static async Task UploadFileToBlobAsyncMemoryVersion(BackupPage backUpPage, byte[] data, string blobName)
        {
            //MessageBox.Show($"{filePath}", "Debug");
            if (isUploading) return; // เพิ่มเงื่อนไขเพื่อหยุดการเรียกตัวเอง
            isUploading = true;
            try
            {
                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(backUpPage.connectionString);

                // Get a reference to the container (create it if it doesn't exist)
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(backUpPage.containerName);
                await blobContainerClient.CreateIfNotExistsAsync();

                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                // อัปโหลดข้อมูล CSV ไปยัง Blob
                using (var memoryStream = new MemoryStream(data))
                {
                    await blobClient.UploadAsync(memoryStream, true);
                }

                // Output success message to the console
                Console.WriteLine($"อัปโหลดไฟล์ {blobName} สำเร็จแล้ว");
                //AzureMessagerBoxAlertNormal azureMessagerBoxAlert = new AzureMessagerBoxAlertNormal("Debug", $"File uploaded to {blobClient.Uri}");
                //azureMessagerBoxAlert.ShowDialog();
                // MessageBox.Show($"File uploaded to {blobClient.Uri}", "Debug");
            }
            catch (Exception ex)
            {
                // Handle and output any exceptions that occur during the upload
                AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Error uploading file: {ex.Message}");
                azureMessagerBoxAlert.ShowDialog();
                //MessageBox.Show($"Error uploading file: {ex.Message}", "Debug");
            }
            finally
            {
                isUploading = false;
            }
        }



        //ตกแต่ง
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
                StagePasswordBoxUseSystemPasswordCharEye2 = true;
            }

        }

        private void pictureButtonClosing_Click(object sender, EventArgs e)
        {
            CheckCancel = true;
            this.Close();
        }

        private async void buttonStyleMypassOK_Click(object sender, EventArgs e)
        {

            //var ciphertexrsImportFile = dbImportFile.ImportFile.ToList();
            CiphertextLocal newCiphertextLocal = new CiphertextLocal();
            InMemoryDataBaseServer newInMemoryDataBase = new InMemoryDataBaseServer();


            string ciphertext_Password;
            string vectorBase64;
            string firstHalf;
            string secondHalf;
            string VectorBase64ForData;
            string PathLocalForuse;
            string PathLocalEncrypt;
            string VectorBase64ForFile;


            if (myPassTextBoxDeviceName.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก New Device Name ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก DeviceName ด้วยครับ", "Error");
                return;
            }
            if (myPassTextBoxMasterPassword.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Master Password ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก MasterPassword ด้วยครับ", "Error");
                return;
            }
            if (myPassTextBoxReMasterPassword.Texts == "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Re-MasterPassword ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                //MessageBox.Show("กรุณากรอก ReMasterPassword ด้วยครับ", "Error");
                return;
            }
            if (myPassTextBoxMasterPassword.Texts != myPassTextBoxReMasterPassword.Texts && myPassTextBoxMasterPassword.Texts != "" && myPassTextBoxReMasterPassword.Texts != "")
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "Master Password กับ Re-MasterPassword ไม่ตรงกัน");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                return;

            }
            if (myPassTextBoxBrowse.Texts == "" || myPassTextBoxBrowse.Texts == null) 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณาเลือกไฟล์ที่ท่านต้องการ Import ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                return;
            }
            if (myPassTextBoxMasterPassword.Texts == myPassTextBoxReMasterPassword.Texts && myPassTextBoxMasterPassword.Texts != "" && myPassTextBoxReMasterPassword.Texts != "")
            {
                //1.Genkey64-->keyBase64
                using (Aes aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.KeySize = 256;
                    aesAlgorithm.GenerateKey();
                    newGenerateKey.GeneratedKey = Convert.ToBase64String(aesAlgorithm.Key);
                }


                //2.Hash Sha256
                // newGenerateKey.Sha256Hash = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + newGenerateKey.GeneratedKey);
                newMemoryMain.Sha256Hash = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts + newGenerateKey.GeneratedKey);
                dbMemoryMain.memoryMains.Add(newMemoryMain);
                dbMemoryMain.SaveChanges();
                //3. Hash MasterPassword
                newGenerateKey.HashMasterPassword = ComputeSha256Hash(myPassTextBoxMasterPassword.Texts);

                //4. Hash NameFile
                newGenerateKey.HashNameFile = SanitizeSha256(ComputeSha256Hash(GenerateRandomKey(64)));

                //5. แปลงค่า Sha256 ที่มีตัวอักษรพิเศษ
                // newGenerateKey.ChangeSha256HashForUse = SanitizeSha256(newGenerateKey.Sha256Hash);

                //6. DeviceName
                newGenerateKey.DeviceName = myPassTextBoxDeviceName.Texts;
                //สร้าง db ในการเก็บ Generatekey
                dbGenerate.Database.EnsureCreated();
                dbGenerate.GenerateKey.Add(newGenerateKey);
                dbGenerate.SaveChanges();
                // MessageBox.Show("Key ได้ถูกสร้างขึ้นสำเร็จแล้ว", "Successfully Generated Key !");

                // ==========================================================================
                // สุดสุดการทำงานการสร้าง Generatekey
                // ===========================================================================

                //============================================================================
                // ImportFile Process
                //===========================================================================
                //MessageBox.Show($"{selectedFilePath}", "Check Path Button");

                // ทำงานกับไฟล์ที่นำเข้า
                if (selectedFilePath == "" || selectedFilePath == null) 
                {
                    return;
                }
                else 
                {
                    ImportFile(selectedFilePath);
                    DbImportFile dbImportparameter = new DbImportFile(destinationFilePath);
                    var ciphertexrsImportFile = dbImportparameter.ImportFile.ToList();

                    dbLocalCiphertextContext.Database.EnsureCreated();

                    var ciphertextListLocal = dbLocalCiphertextContext.Ciphertexts.ToList();
                    var ciphertextListServer = dbImportMemory.InMemoryDataBase.ToList();
                    var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                    var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);


                    ExportFile newExport = new ExportFile();

                    var mergedImports = ciphertexrsImportFile
                        .Select(import => new
                        {
                            Id = import.id,
                            Title = import.Title,
                            Username = import.Username,
                            PainTextPassword = import.PainTextPassword,
                            URL = import.URL,
                            UsernameConnectURL = import.UsernameConnectURL,
                        })
                        .ToList();


                    foreach (var loopfor in mergedImports)
                    {
                        //เชื่อมข้อมูล Static => LocalPart
                        newCiphertextLocal.Id = loopfor.Id;

                        newCiphertextLocal.CiphertextTitle = loopfor.Title;
                        newCiphertextLocal.CiphertextUsername = loopfor.Username;
                        newCiphertextLocal.URL = loopfor.URL;
                        newCiphertextLocal.UsernameConnectURL = loopfor.UsernameConnectURL;

                        // เชื่อมข้อมูล Static => ServerPart
                        newInMemoryDataBase.Id = loopfor.Id;
                        newInMemoryDataBase.UsernameConnectURL = loopfor.UsernameConnectURL;

                        if (existingGenerateKey != null)
                        {
                            ciphertext_Password = EncryptDataWithAes(loopfor.PainTextPassword, existingMemoryMain.Sha256Hash, out vectorBase64);
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบ Key บนเครื่องคุณ", "Error");
                            return;
                        }

                        //newCiphertextLocal.CiphertextPassword = ciphertext_Password;

                        newCiphertextLocal.vectorBase64 = vectorBase64;

                        Split(ciphertext_Password, out firstHalf, out secondHalf);

                        newCiphertextLocal.CiphertextPasswordLocal = firstHalf;

                        newInMemoryDataBase.CiphertextPasswordServer = secondHalf;

                        dbLocalCiphertextContext.Ciphertexts.Add(newCiphertextLocal);
                        dbLocalCiphertextContext.SaveChanges();

                        dbImportMemory.InMemoryDataBase.Add(newInMemoryDataBase);
                        dbImportMemory.SaveChanges();

                    }
                    string vectorbase64Connectionstring;
                    string vectorbase64Container;
                    string Setname = $"{existingGenerateKey.DeviceName}.csv";
                    byte[] csvData = GenerateCsvFromDbContext();
                    byte[] encryptCsvData = EncryptData(csvData, existingMemoryMain.Sha256Hash, out VectorBase64ForData);
                    existingGenerateKey.VectorBase64ForAllData = VectorBase64ForData;
                    dbGenerate.SaveChanges();


                    PathLocalForuse = await dbLocalCiphertextContext.SetCombinePath();
                    PathLocalEncrypt = await dbLocalCiphertextContentEncrypt.SetCombinePath();

                    // Create Azure Connetion String
                    var existingImportparameter = dbImportparameter.ImportFile.Find(1);
                    dbAzureConnectionString.Database.EnsureCreated();
                    //EncryptDataWithAes(existingImportparameter.Connectionstring, existingMemoryMain.Sha256Hash, out vectorbase64Connectionstring); 
                    azureConnection.ConnectionString = EncryptDataWithAes(existingImportparameter.Connectionstring, existingMemoryMain.Sha256Hash, out vectorbase64Connectionstring);
                    existingGenerateKey.VectorBase64ForAzureConnection = vectorbase64Connectionstring;
                    dbGenerate.SaveChanges();


                    azureConnection.ContainerName = EncryptDataWithAes(existingImportparameter.ContainerName, existingMemoryMain.Sha256Hash, out vectorbase64Container);
                    existingGenerateKey.VectorBase64ForAzureContainer = vectorbase64Container;
                    dbGenerate.SaveChanges();


                    dbAzureConnectionString.azureConnectionStrings.Add(azureConnection);
                    dbAzureConnectionString.SaveChanges();



                    await UploadFileToBlobAsyncMemoryVersion(this, encryptCsvData, Setname);
                    // MessageBox.Show("Upload Part Server สำเร็จ", "Debug");
                    MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("Success", "Upload Server Part สำเร็จ");
                    miniMessagerBoxTextBoxNormal.ShowDialog();

                    EncryptFile(PathLocalForuse, existingMemoryMain.Sha256Hash, PathLocalEncrypt, out VectorBase64ForFile);
                    existingGenerateKey.VectorBase64ForFile = VectorBase64ForFile;
                    dbGenerate.SaveChanges();

                    DeleteFileDirectly(PathLocalForuse);

                    //DeleteDbFile(dbImportFile.DatabasePath);
                    DeleteFileAllInPath(dbImportFile.DatabasePath);
                    //MessageBox.Show("ไฟล์ Import ถูกลบแล้ว", "Debug");
                    //// Debug ================
                    //string CsvFilePath = $"D:\\Storage\\StreamMemoryToFileCSV";
                    //Directory.CreateDirectory(CsvFilePath);
                    //string CombinePathCsvFile = Path.Combine(CsvFilePath, $"CiphertextsServer.csv"); ;
                    //File.WriteAllBytes(CombinePathCsvFile, csvData);

                    //this.Close();
                    Login loginPage = new Login();
                    loginPage.Show();
                    //ปิดหน้าต่างปัจจุบัน
                    this.Hide();
                    //แสดงหน้าต่าง LoginForm

                    //Application.Exit();


                }


            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }

        private void buttonStyleMypassCancel_Click(object sender, EventArgs e)
        {
            CheckCancel = true;
            this.Close();
        }

        private void buttonStyleMypassBrows_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // ตั้งค่าสำหรับการให้ผู้ใช้เลือกไฟล์
            openFileDialog.Filter = "ไฟล์ฐานข้อมูล (*.db)|*.db";
            openFileDialog.Title = "เลือกไฟล์ที่ต้องการ import";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // นำเข้าไฟล์ที่เลือก
                //string selectedFilePath = openFileDialog.FileName;
                selectedFilePath = openFileDialog.FileName;

                // แสดง Path ใน TextBox
                myPassTextBoxBrowse.Texts = selectedFilePath;

                // ทำงานกับไฟล์ที่นำเข้า ระวัง !!!!!!!<------------------------------------------------
                //ImportFile(selectedFilePath);
            }

        }

        private void pictureBoxEyeWhite1_Click(object sender, EventArgs e)
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

        private void pictureBoxEyeWhite2_Click(object sender, EventArgs e)
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

        private void BackupPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Login loginPage = new Login();
            //loginPage.Show();
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
