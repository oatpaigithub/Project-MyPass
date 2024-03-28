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
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System.Data.SQLite;
using Microsoft.EntityFrameworkCore.InMemory;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Azure;
using Azure.Core;

namespace TestFunctionSQL
{

    public partial class MainPage : Form
    {
        //====================ตกแต่ง==============================
        //EyeButton
        bool StagePasswordBoxUseSystemPasswordChar = true;

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



        // TextBox String
        //private string TextBoxPlaceholderTitile = "FB_faceหลัก_Faceหลุม";
        private string TextBoxPlaceholderUsername = "";
        private string TextBoxPlaceholderPassword = "";



        // ทำการสร้าง Binding ไว้ใช้สำหรับ List
        private BindingList<CiphertextLocal> BindingListCiphertext = new BindingList<CiphertextLocal>();
        private BindingList<CiphertextServer> BindingListCiphertextServer = new BindingList<CiphertextServer>();

        // ทำการสร้าง ที่ระบุ Source ของการ Binding
        private BindingSource bindingSourceLocal;
        private BindingSource bindingSourceServer;


        //ทำการสร้าง Path ของ Database SQLite
        private DbLocalCiphertextContext dbContextLocal = new DbLocalCiphertextContext();
        private DbServerCiphertextContext dbContextServer = new DbServerCiphertextContext();
        public DbGenerateKey dbGenerate = new DbGenerateKey();

        private DbLocalCiphertextContentEncrypt dbContextLocalEncrypt = new DbLocalCiphertextContentEncrypt();
        private DbGenerateKeyEncrypt dbGenerateKeyEncrypt = new DbGenerateKeyEncrypt();
        public DbMemoryMain dbMemoryMain = new DbMemoryMain();
        //private DbExportFile dbExportFile = new DbExportFile();
        //private DbInMemoryDataBaseServer dbInMemoryDataBaseServer = new DbInMemoryDataBaseServer();

        public DbAzureConnectionString dbAzureConnectionString = new DbAzureConnectionString();

        // สร้าง C
        public GenerateKey newGenerateKey = new GenerateKey();
        private MemoryMain newMemoryMain = new MemoryMain();


        // ชั่วคราว
        private string filePathToDelete = @"D:\Storage\ServerRemote";
        private string filePathToDeleteDataDevice = @"D:\Storage";

        // Server Part Azure Server

        //private  string connectionString = "DefaultEndpointsProtocol=https;AccountName=cpeproject12121;AccountKey=YvpBD5VuE7Axl6jnXZsTaElNPrS/RO3gj79pnJHzXmIRQEZKMj6q8Wg0jL7G4IKGRcusNtHC5FrP+ASt3t+SHg==;EndpointSuffix=core.windows.net";
        //private string containerName = "cpemypassproject";

        //private const string connectionStringNoconst = $"{}";

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
        public bool CheckAction { get; private set; }
        public bool CheckPartServer = true;

        public static bool CheckAzureUploadMemory;


        public bool CheckMaskString = true;

        private Dictionary<int, bool> maskStatus = new Dictionary<int, bool>();
        // NamePath
        private string NamePath;

        public MainPage()
        {
            InitializeComponent();
            InitializeTextBoxPlaceholder();

            // เพิ่ม event handler Load
            Load += MainPage_Load;

            // ตกแต่ง
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));


            // ทำการโหลดข้อมูล จากฐานเก็บข้อมูล

            LoadDataFromDatabaseLocal();

            // สร้าง BindingList 
            bindingSourceLocal = new BindingSource();
            // ทำการ BindingSource ไปที่ Ciphertext
            bindingSourceLocal.DataSource = BindingListCiphertext;


            // สร้าง DataGridView และกำหนดคุณสมบัติ True = จะสร้าง Column ทุกตัวใน วัตถุนั้นๆ False = กำหนดเอง
            DataGridViewInformationPasswordManager.AutoGenerateColumns = false;

            // เพิ่ม DataGridView เข้าใน Controls ของ Form
            this.Controls.Add(DataGridViewInformationPasswordManager);

            // กำหนด BindingSource เป็นแหล่งข้อมูลให้ DataGridView
            DataGridViewInformationPasswordManager.DataSource = bindingSourceLocal;


            // เพิ่ม Column สำหรับแสดงข้อมูลใน DataGridView
            // เพิ่ม Title Column 
            DataGridViewTextBoxColumn titleColumn = new DataGridViewTextBoxColumn();
            titleColumn.Name = "CiphertextTitle";
            titleColumn.HeaderText = "Title";
            titleColumn.DataPropertyName = "CiphertextTitle"; // ระบุ property ที่จะเชื่อมโยง
            DataGridViewInformationPasswordManager.Columns.Add(titleColumn);




            // เพิ่ม Username Column 
            DataGridViewTextBoxColumn usernameColumn = new DataGridViewTextBoxColumn();
            usernameColumn.Name = "CiphertextUsername";
            usernameColumn.HeaderText = "Username";
            usernameColumn.DataPropertyName = "CiphertextUsername";
            DataGridViewInformationPasswordManager.Columns.Add(usernameColumn);


            // Image EyeView Column
            DataGridViewImageColumn imageColunsEyeView = new DataGridViewImageColumn();
            imageColunsEyeView.Name = "EyeView";
            imageColunsEyeView.HeaderText = "";
            imageColunsEyeView.Image = imageListEye.Images[0];
            DataGridViewInformationPasswordManager.Columns.Add(imageColunsEyeView);

            DataGridViewImageColumn imageColumnCopy = new DataGridViewImageColumn();
            imageColumnCopy.Name = "CopyImage";
            imageColumnCopy.HeaderText = "";
            imageColumnCopy.Image = imageListCopy.Images[1];
            DataGridViewInformationPasswordManager.Columns.Add(imageColumnCopy);




            // เพิ่ม Image View Column
            DataGridViewImageColumn imageColumnView = new DataGridViewImageColumn();
            imageColumnView.Name = "ViewButtonImage";
            imageColumnView.HeaderText = "ViewPassword";
            imageColumnView.Image = imageList1.Images[2];
            DataGridViewInformationPasswordManager.Columns.Add(imageColumnView);

            // เพิ่ม Image Delete Column
            DataGridViewImageColumn imageColumnDelete = new DataGridViewImageColumn();
            imageColumnDelete.Name = "DeleteButtonImage";
            imageColumnDelete.HeaderText = "Delete";
            imageColumnDelete.Image = imageList1.Images[1];
            DataGridViewInformationPasswordManager.Columns.Add(imageColumnDelete);


            // สร้าง DataGridViewImageColumn และกำหนดรูปภาพจาก ImageList ให้กับคอลัมน์
            DataGridViewImageColumn imageColumnEdit = new DataGridViewImageColumn();
            imageColumnEdit.Name = "EditButtonImage";
            imageColumnEdit.HeaderText = "Edit";
            imageColumnEdit.Image = imageList1.Images[0]; // กำหนดรูปภาพให้เป็นรูปภาพแรกใน ImageList
            DataGridViewInformationPasswordManager.Columns.Add(imageColumnEdit);


            // เพิ่ม URL Column 
            DataGridViewTextBoxColumn urlColumn = new DataGridViewTextBoxColumn();
            urlColumn.Name = "URLColumn";
            urlColumn.HeaderText = "URL";
            urlColumn.DataPropertyName = "URL"; // ระบุ property ที่จะเชื่อมโยง
            DataGridViewInformationPasswordManager.Columns.Add(urlColumn);




            DataGridViewInformationPasswordManager.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //DataGridViewInformationPasswordManager.Columns["EditButtonImage"].Width = 300;
            DataGridViewInformationPasswordManager.Columns["EyeView"].Width = 40;
            DataGridViewInformationPasswordManager.Columns["CopyImage"].Width = 32;
            DataGridViewInformationPasswordManager.CellClick += DataGridView1_CellClick;
            //DataGridViewInformationPasswordManager.




        }
        //==================================================================================
        // Function Part Interactive ต่างๆ จำพวกปุ่ม ตาราง การปิดโปรแกรม เป็นต้น 



        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsDatabaseFileLocalExist())
            {
                if (BindingListCiphertextServer != null)
                {
                    // ตรวจสอบว่าคลิกที่ Column "ViewButton" หรือไม่
                    if (e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["ViewButtonImage"].Index && e.RowIndex >= 0)
                    {
                        // ดำเนินการเมื่อปุ่มถูกคลิก
                        // ตัวอย่าง: แสดง Popup ข้อมูลที่อยู่ใน row นั้น
                        int rowIndex = e.RowIndex;
                        string CipherTextPasswordConnect;
                        if (rowIndex >= 0 && rowIndex < BindingListCiphertextServer.Count)
                        {
                            if (BindingListCiphertextServer[rowIndex] != null)
                            {
                                CiphertextLocal selectedCiphertextlocal = BindingListCiphertext[rowIndex];
                                CiphertextServer selectedCiphertextServer = BindingListCiphertextServer[rowIndex];
                                // Debug Mode =============================================
                                //MessageBox.Show($"selectedCiphertextlocal:{selectedCiphertextlocal.UsernameConnectURL}");
                                //MessageBox.Show($"selectedCiphertextServer:{selectedCiphertextServer.UsernameConnectURL}");
                                var existingCiphertextLocal = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL == selectedCiphertextlocal.UsernameConnectURL);

                                var existingCiphertextServer = dbContextServer.CiphertextsServer.FirstOrDefault(c => c.UsernameConnectURL == selectedCiphertextServer.UsernameConnectURL);
                                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);

                                // ส่ง object ไปยัง CheckActionForm
                                CheckActionForm checkActionForm = new CheckActionForm();

                                // เรียก CheckActionForm popUp ขึ้นมา
                                checkActionForm.ShowDialog();


                                if (checkActionForm.CheckAction)
                                {
                                    if (existingCiphertextLocal != null && existingCiphertextServer != null)
                                    {
                                        if (existingCiphertextLocal.UsernameConnectURL == existingCiphertextServer.UsernameConnectURL)
                                        {
                                            CipherTextPasswordConnect = existingCiphertextLocal.CiphertextPasswordLocal + existingCiphertextServer.CiphertextPasswordServer;

                                            string decryptedData = DecryptDataWithAes(CipherTextPasswordConnect, existingMemoryMain.Sha256Hash, existingCiphertextLocal.vectorBase64);
                                            //MessageBox.Show($"รหัสผ่านของคุณคือ {decryptedData}", "แสดงรหัสผ่าน");
                                            // แสดง MessageBox และกำหนดปุ่ม Copy
                                            ViewMessegerBox viewMessegerBox = new ViewMessegerBox(decryptedData);
                                            viewMessegerBox.ShowDialog();




                                            //DialogResult result = MessageBox.Show($"รหัสผ่านของคุณคือ \n{decryptedData}\nคุณต้องการ Copy ไปใช้งานหรือไม่?", "แสดงรหัสผ่าน", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                            //// ถ้าผู้ใช้งานกด OK
                                            //if (result == DialogResult.Yes)
                                            //{
                                            //    // คัดลอก Username ลงใน Clipboard
                                            //    Clipboard.SetText(decryptedData);
                                            //    MessageBox.Show("คัดลอก รหัสผ่านของคุณ สำเร็จ", "คัดลอก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //}
                                        }
                                        else
                                        {
                                            MessageBox.Show("ไม่พบ part ของ Local และ Server ที่ตรงกัน ", "Error");
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบข้อมูลของ Part ใน DataBase");
                                        miniMessagerBoxTextBoxAlert.ShowDialog();
                                        // MessageBox.Show("ไม่พบข้อมูลของ Part ใน DataBase ", "Error");
                                        return;
                                    }

                                }
                                else
                                {

                                    //MessageBox.Show("ยินดีต้อนรับเข้าสู้หน้าหลัก");
                                }


                            }

                        }
                        else
                        {
                            MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบ Server Part");
                            miniMessagerBoxTextBoxAlert.ShowDialog();
                            //MessageBox.Show("ไม่พบ Server Part", "Error");
                            return;
                        }


                    }
                    // ตรวจสอบว่าคลิกที่ Column "DeleteButton" หรือไม่
                    else if (e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["DeleteButtonImage"].Index && e.RowIndex >= 0)
                    {
                        // ดำเนินการเมื่อปุ่ม "Delete" ถูกคลิก
                        int rowIndex = e.RowIndex;
                        if (rowIndex >= 0 && rowIndex < BindingListCiphertextServer.Count)
                        {
                            DeleteRowTableMessengerBoxAlert deleteRowTableMessengerBoxAlert = new DeleteRowTableMessengerBoxAlert();
                            deleteRowTableMessengerBoxAlert.ShowDialog();

                            if (deleteRowTableMessengerBoxAlert.DialogResultForm == true)
                            {

                                CiphertextLocal selectedCiphertextlocal = BindingListCiphertext[rowIndex];
                                CiphertextServer selectedCiphertextServer = BindingListCiphertextServer[rowIndex];

                                var existingCiphertextLocal = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL == selectedCiphertextlocal.UsernameConnectURL);
                                var existingCiphertextServer = dbContextServer.CiphertextsServer.FirstOrDefault(c => c.UsernameConnectURL == selectedCiphertextServer.UsernameConnectURL);
                                // ส่ง object ไปยัง CheckActionForm
                                CheckActionForm checkActionForm = new CheckActionForm();

                                // เรียก CheckActionForm popUp ขึ้นมา
                                checkActionForm.ShowDialog();
                                if (checkActionForm.CheckAction)
                                {

                                    if (existingCiphertextLocal != null && existingCiphertextServer != null)
                                    {
                                        if (existingCiphertextLocal.UsernameConnectURL == existingCiphertextServer.UsernameConnectURL)
                                        {

                                            dbContextLocal.Ciphertexts.Remove(existingCiphertextLocal); // ลบจาก DbSet
                                            dbContextLocal.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล

                                            dbContextServer.CiphertextsServer.Remove(existingCiphertextServer);
                                            dbContextServer.SaveChanges();

                                            BindingListCiphertext.RemoveAt(rowIndex);
                                            BindingListCiphertextServer.RemoveAt(rowIndex);

                                            SaveDataToListBinding();


                                        }
                                        else
                                        {
                                            MessageBox.Show("ไม่พบ part ของ Local และ Server ที่ตรงกัน ", "Error");
                                            return;
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("ไม่พบข้อมูลของ part ใน DataBase ", "Error");
                                        return;
                                    }

                                }
                                else
                                {

                                }

                            }
                        }
                        else
                        {
                            MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบ Server Part");
                            miniMessagerBoxTextBoxAlert.ShowDialog();
                        }
                    }
                    // ตรวจสอบว่าคลิกที่ Column "EditButton" หรือไม่
                    else if (e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["EditButtonImage"].Index && e.RowIndex >= 0)
                    {

                        int rowIndex = e.RowIndex;

                        if (rowIndex >= 0 && rowIndex < BindingListCiphertextServer.Count)
                        {
                            string firstHalf;
                            string secondHalf;

                            // Access the selected Ciphertext and CiphertextServer objects
                            CiphertextLocal selectedCiphertext = BindingListCiphertext[rowIndex];
                            CiphertextServer selectedCiphertextServer = BindingListCiphertextServer[rowIndex];



                            // ส่ง object ไปยัง CheckActionForm
                            CheckActionForm checkActionForm = new CheckActionForm();

                            // เรียก CheckActionForm popUp ขึ้นมา
                            checkActionForm.ShowDialog();
                            if (checkActionForm.CheckAction)
                            {
                                // ส่ง object ไปยัง EditForm
                                EditForm editForm = new EditForm(selectedCiphertext, selectedCiphertextServer);
                                // เรียก EditForm popUp ขึ้นมา
                                editForm.ShowDialog();


                                // นำค่าที่ได้รับการแก้ไขแล้วจาก EditForm มาเก็บไว้ใน Object ใหม่
                                CiphertextLocal editedCiphertext = editForm.EditedCiphertext;

                                if (editedCiphertext != null)
                                {
                                    //ทำกระบวนการ Add ใหม่ เพื่อทำการ update ค่า
                                    Split(editForm.CiphertextPassword_this, out firstHalf, out secondHalf);

                                    editedCiphertext.CiphertextPasswordLocal = firstHalf;
                                    //editedCiphertext.CiphertextPasswordServer = secondHalf;

                                    selectedCiphertextServer.CiphertextPasswordServer = secondHalf;

                                    BindingListCiphertext[rowIndex] = editedCiphertext;
                                    SaveDataToListBinding();

                                    // loop หาค่า ID ที่ตรงกับ Database แล้วทำการ แก้ไขค่าตรงนั้น โดยใช้ Find()

                                    var existingCiphertextServer = dbContextServer.CiphertextsServer.Find(selectedCiphertextServer.Id);
                                    if (existingCiphertextServer != null)
                                    {
                                        existingCiphertextServer.CiphertextPasswordServer = selectedCiphertextServer.CiphertextPasswordServer;
                                        existingCiphertextServer.UsernameConnectURL = $"{editedCiphertext.UsernameConnectURL}";


                                    }
                                    else
                                    {
                                        MessageBox.Show($"ไม่ตรงกับ Id ที่มีอยู่ใน DataBase Server", "Error");
                                    }

                                    var existingCiphertext = dbContextLocal.Ciphertexts.Find(editedCiphertext.Id);

                                    if (existingCiphertext != null)
                                    {

                                        existingCiphertext.CiphertextTitle = editedCiphertext.CiphertextTitle;
                                        existingCiphertext.CiphertextUsername = editedCiphertext.CiphertextUsername;
                                        existingCiphertext.URL = $"{editedCiphertext.URL}";
                                        existingCiphertext.UsernameConnectURL = $"{editedCiphertext.UsernameConnectURL}";

                                        //existingCiphertext.CiphertextPassword = editedCiphertext.CiphertextPassword;
                                        existingCiphertext.vectorBase64 = editedCiphertext.vectorBase64;


                                    }
                                    else
                                    {

                                        MessageBox.Show($"ไม่ตรงกับ Id ที่มีอยู่ใน DataBase Local", "Error");


                                    }


                                    dbContextLocal.SaveChanges();
                                    dbContextServer.SaveChanges();
                                    //ciphertext = editedCiphertext;
                                    //SaveDataToListBinding();
                                }
                                else
                                {
                                    // MessageBox.Show("คุณไม่ได้ทำการเปลี่ยนแปลงค่า", "แจ้งให้ทราบว่า");
                                }

                            }
                            else
                            {

                            }

                        }
                        else
                        {
                            MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบ Server Part");
                            miniMessagerBoxTextBoxAlert.ShowDialog();
                        }

                    }
                    // ตรวจสอบว่าคลิกที่ Column "URLColumn" หรือไม่
                    else if (e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["URLColumn"].Index && e.RowIndex >= 0)
                    {
                        int rowIndex = e.RowIndex;

                        if (rowIndex >= 0 && rowIndex < BindingListCiphertextServer.Count)
                        {
                            CiphertextLocal selectedCiphertext = BindingListCiphertext[rowIndex];
                            string url_this = selectedCiphertext.URL;

                            string pattern = @"^(https?://)?(www\.)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";

                            // ตรวจสอบว่า input เป็น URL หรือไม่
                            Regex regex = new Regex(@"^(http|https)://([\w-]+\.)+([^\s]+)$");
                            bool isUrl = regex.IsMatch(url_this);

                            // ดำเนินการตามผลลัพธ์
                            if (isUrl)
                            {
                                // เปิด URL
                                System.Diagnostics.Process.Start("chrome", url_this);
                            }
                            else
                            {
                                // ค้นหา input
                                System.Diagnostics.Process.Start("chrome", $"https://{url_this}");
                            }


                        }
                        //if (Regex.IsMatch(url_this, pattern)) 
                        //    {
                        //    Process.Start(url_this);
                        //    }
                        //}
                        //else
                        //{
                        //    MessageBox.Show("ไม่พบ Part Server Error code:VB04", "Error");
                        //    return;
                        //}
                    }
                    else if (e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["EyeView"].Index && e.RowIndex >= 0)
                    {
                        int rowIndex = e.RowIndex;
                        if (rowIndex >= 0 && rowIndex < BindingListCiphertextServer.Count)
                        {
                            if (!maskStatus.ContainsKey(rowIndex))
                            {
                                maskStatus[rowIndex] = true;
                            }
                            else
                            {
                                maskStatus[rowIndex] = !maskStatus[rowIndex];
                            }
                            DataGridViewInformationPasswordManager.Refresh();
                        }

                    }
                    else if (e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["CopyImage"].Index && e.RowIndex >= 0) 
                    {
                        int rowIndex = e.RowIndex;
                        if (rowIndex >= 0 && rowIndex < BindingListCiphertextServer.Count) 
                        {
                            CiphertextLocal selectedCiphertext = BindingListCiphertext[rowIndex];
                            string Username = selectedCiphertext.CiphertextUsername;
                            Clipboard.SetText(Username);
                            MiniMessagerBoxTextBoxNormal miniMessagerBoxCopySuccess = new MiniMessagerBoxTextBoxNormal("คัดลอก", "คัดลอก Username ของคุณสำเร็จ");
                            miniMessagerBoxCopySuccess.ShowDialog();

                        }


                    }
                }
                else
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบ Server Part");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("ไม่พบ Part Local Error code:DV01 ", "Error");
            }

        }
        private void buttonCreateItem_Click(object sender, EventArgs e)
        {

            // สร้าง Object ใหม่ของคลาส Ciphertext
            try
            {

                CiphertextLocal newCiphertext = new CiphertextLocal();
                CiphertextServer newCiphertextServer = new CiphertextServer();


                string vectorBase64;
                string ciphertext_Password;
                string firstHalf;
                string secondHalf;


                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);

                //รับ Input string จาก TextBox มาเก็บไว้ใน Object
                newCiphertext.CiphertextTitle = myPassTextBoxTitle.Texts;
                newCiphertext.CiphertextUsername = myPassTextBoxUsername.Texts;

                //ทำการ Encryption
                if (existingGenerateKey != null)
                {
                    ciphertext_Password = EncryptDataWithAes(myPassTextBoxPassword.Texts, existingMemoryMain.Sha256Hash, out vectorBase64);
                }
                else
                {
                    MessageBox.Show("ไม่พบ Key บนเครื่องคุณ", "Error");
                    return;
                }


                // ในความเป็นจริงไม่ต้องเก็บ อันนี้เก็บเพื่อการ Test ว่า funtion  EncryptDataWithAes และ Split ทำงานถูกต้องหรือไม่
                //newCiphertext.CiphertextPassword = ciphertext_Password;
                //==============================================================

                // เก็บ vectorBase64 ไว้ใช้ในการ Decryption
                newCiphertext.vectorBase64 = vectorBase64;

                // ทำการแยกเป็น 2 Part
                Split(ciphertext_Password, out firstHalf, out secondHalf);

                // นำไปเก็บไว้ใน Object 
                newCiphertext.CiphertextPasswordLocal = firstHalf;

                // ในความเป็นจริงไม่ต้องเก็บ อันนี้เก็บเพื่อการ Test ในกรณีที่ยังไม่ได้สร้างที่เก็บแยกไป Server จริงๆ
                //newCiphertext.CiphertextPasswordServer = secondHalf;

                // นำไปเก็บไว้ใน Object ====Database Server ==========
                newCiphertextServer.CiphertextPasswordServer = secondHalf;

                if (myPassTextBoxPassword.Texts == "")
                {
                    MessageBox.Show("กรุณากรอก Password ด้วยครับ", "Error");
                    return;
                }
                if (myPassTextBoxTitle.Texts == "" || myPassTextBoxTitle.Texts == "FB_faceหลัก_Faceหลุม")
                {
                    MessageBox.Show("กรุณากรอก Title ด้วยครับ", "Error");
                    return;
                }
                if (myPassTextBoxUsername.Texts == "")
                {
                    MessageBox.Show("กรุณากรอก Username ด้วยครับ", "Error");
                    return;
                }
                #region -> ตัวเก่า
                //if (comboBoxUserType.SelectedItem == null || comboBoxUserType.SelectedItem.ToString().Trim() == "...Add")
                //{
                //    MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                //    return;
                //}
                //else
                //{
                //    if (comboBoxUserType.SelectedItem.ToString().Trim() == "Facebook")
                //    {
                //        newCiphertext.URL = "https://facebook.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //    }
                //    else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Youtube")
                //    {
                //        newCiphertext.URL = "https://www.youtube.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //    }
                //    else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Discord")
                //    {
                //        newCiphertext.URL = "https://discord.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";

                //    }
                //    else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Twitter")
                //    {
                //        newCiphertext.URL = "https://twitter.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";

                //    }
                //    else
                //    {
                //        newCiphertext.URL = $"{comboBoxUserType.SelectedItem.ToString()}";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{comboBoxUserType.SelectedItem.ToString()}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{comboBoxUserType.SelectedItem.ToString()}";
                //    }


                //    //newCiphertext.URL = $"{comboBoxUserType.SelectedItem.ToString()}";

                //    //newCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{comboBoxUserType.SelectedItem.ToString()}";

                //    var existingCiphertext = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == newCiphertext.UsernameConnectURL.Trim());
                //    //var existingCiphertextUsername = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.CiphertextUsername == newCiphertext.CiphertextUsername);
                //    //var existingCiphertextUserType = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.URL == newCiphertext.URL);


                //    if (existingCiphertext != null)
                //    {

                //        MessageBox.Show("กรุณากรอก Username หรือ URL ใหม่ด้วยครับ", "Error");
                //        return;

                //    }
                //    else
                //    {

                //        //// เพิ่ม Object ล่าสุดลงใน List
                //        BindingListCiphertext.Add(newCiphertext);
                //        BindingListCiphertextServer.Add(newCiphertextServer);
                //        SaveDataToListBinding();
                //        //// บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
                //        //// ฝั่ง Local  =============================
                //        dbContextLocal.Ciphertexts.Add(newCiphertext);
                //        dbContextLocal.SaveChanges();
                //        //// ฝั่ง Server =============================
                //        dbContextServer.CiphertextsServer.Add(newCiphertextServer);
                //        dbContextServer.SaveChanges();
                //        //ตกแต่ง TextBox
                //        //textBoxTitle.Text = TextBoxPlaceholderTitile;
                //        //textBoxUsername.Text = TextBoxPlaceholderUsername;
                //        //textBoxPassword.Text = TextBoxPlaceholderPassword;
                //        InitializeTextBoxPlaceholder();

                //    }
                //}
                #endregion

                if (myPassTextBoxURL.Texts == "")
                {
                    MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                    return;
                }
                else
                {
                    newCiphertext.URL = $"{myPassTextBoxURL.Texts}";
                    newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{myPassTextBoxURL.Texts}";
                    newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{myPassTextBoxURL.Texts}";

                    var existingCiphertext = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == newCiphertext.UsernameConnectURL.Trim());
                    if (existingCiphertext != null)
                    {

                        MessageBox.Show("กรุณากรอก Username หรือ URL ใหม่ด้วยครับ", "Error");
                        return;

                    }
                    else
                    {

                        //// เพิ่ม Object ล่าสุดลงใน List
                        BindingListCiphertext.Add(newCiphertext);
                        BindingListCiphertextServer.Add(newCiphertextServer);
                        SaveDataToListBinding();
                        //// บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
                        //// ฝั่ง Local  =============================
                        dbContextLocal.Ciphertexts.Add(newCiphertext);
                        dbContextLocal.SaveChanges();
                        //// ฝั่ง Server =============================
                        dbContextServer.CiphertextsServer.Add(newCiphertextServer);
                        dbContextServer.SaveChanges();

                        InitializeTextBoxPlaceholder();

                    }


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการสร้าง Item: {ex.Message}", "Error");
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveDataToDatabaseLocal();
            //SaveDataToDatabaseServer();
        }
        private void DeleteDataServer_Click(object sender, EventArgs e)
        {
            // กำหนดที่อยู่ของไฟล์ที่ต้องการลบ

            // เรียกใช้เมธอด Delete จาก System.IO เพื่อลบไฟล์
            try
            {
                DeleteDbFile(filePathToDelete);
                MessageBox.Show("ไฟล์ถูกลบแล้ว", "Delete Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการลบไฟล์: {ex.Message}");
            }

        }

        // =================================================================================
        // Function Hide
        private static string HideEmail(string email) 
        {
            // Split email into username and domain
            string[] parts = email.Split('@');
            string username = parts[0];
            string domain = parts[1];

            // Hide characters in username
            int visibleChars = Math.Min(1, username.Length);
            string hiddenUsername = username.Substring(0, visibleChars) + new string('*', username.Length - visibleChars);

            // Hide characters in domain
            int visibleDomainChars = Math.Min(1, domain.Length);
            string hiddenDomain = domain.Substring(0, visibleDomainChars) + new string('*', domain.Length - visibleDomainChars);

            // Combine hidden username and domain
            string hiddenEmail = hiddenUsername + "@" + hiddenDomain;

            return hiddenEmail;
        }





        //==================================================================================
        // Function Part Database And DataBinding <LoadFeature>
        private async void MainPage_Load(object sender, EventArgs e)
        {
            await LoadDataForamDatabaseServer();
        }
        //private async void LoadDataFromGenerate() 
        //{
        //    dbGenerate.Database.EnsureCreated();
        //}
        private async void LoadDataFromDatabaseLocal()
        {
            try
            {
                if (File.Exists(await dbContextLocalEncrypt.SetCombinePath()))
                {
                    string PathLocalForuse;
                    string PathLocalEncrypt;
                    var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                    var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                    PathLocalForuse = await dbContextLocal.SetCombinePath();
                    PathLocalEncrypt = await dbContextLocalEncrypt.SetCombinePath();
                    DecryptFile(PathLocalEncrypt, existingMemoryMain.Sha256Hash, PathLocalForuse, existingGenerateKey.VectorBase64ForFile);


                    dbContextLocal.Database.EnsureCreated();
                    List<CiphertextLocal> loadedData = dbContextLocal.Ciphertexts.ToList();
                    BindingListCiphertext = new BindingList<CiphertextLocal>(loadedData);


                }
                else
                {
                    dbContextLocal.Database.EnsureCreated();
                    List<CiphertextLocal> loadedData = dbContextLocal.Ciphertexts.ToList();
                    BindingListCiphertext = new BindingList<CiphertextLocal>(loadedData);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลจากฐานข้อมูล CiphertextLocal: " + ex.Message);
            }
        }
        public async Task LoadDataForamDatabaseServer()
        {

            // Notion
            // Notion
            bool hasDataGenerateKey = dbGenerate.GenerateKey.Any();
            bool hasDataLocalPart = dbContextLocal.Ciphertexts.Any();
            if (hasDataLocalPart)
            {
                //string VectorBase64;
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                //await DownloadBlobAsync(this);

                byte[] blobData = await DownloadBlobAsyncMemoryVersion(this);

                if (blobData == null)
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "ไม่พบ Server Part");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    return;
                }

                byte[] decryptedData = DecryptData(blobData, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAllData);

                //  นำข้อมูล CSV เข้าฐานข้อมูล 
                using (var memoryStream = new MemoryStream(decryptedData))
                using (var reader = new StreamReader(memoryStream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true }))
                {
                    var records = csv.GetRecords<CSVPrototypePartServer>();

                    // เพิ่มข้อมูลจากไฟล์ CSV เข้าสู่ฐานข้อมูล


                    foreach (var record in records)
                    {
                        dbContextServer.CiphertextsServer.Add(new CiphertextServer { CiphertextPasswordServer = record.CiphertextPasswordServer, UsernameConnectURL = record.UsernameConnectURL });
                    }
                    dbContextServer.SaveChanges();


                }
                if (CheckPartServer)
                {
                    //dbContextServer.Database.EnsureCreated();

                    List<CiphertextServer> loadedData = dbContextServer.CiphertextsServer.ToList();
                    BindingListCiphertextServer = new BindingList<CiphertextServer>(loadedData);
                }
                else
                {
                    MessageBox.Show("ไม่พบส่วนของ Server บน Cloud", "Error");
                }

            }
            else
            {
                //dbContextServer.Database.EnsureCreated();
                List<CiphertextServer> loadedData = dbContextServer.CiphertextsServer.ToList();

                BindingListCiphertextServer = new BindingList<CiphertextServer>(loadedData);
            }



            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการโหลดข้อมูลจากฐานข้อมูล CiphertextServer: " + ex.Message, "Debug");
            }

        }
        private void SaveDataToListBinding()
        {
            try
            {
                bindingSourceLocal.DataSource = BindingListCiphertext;
                DataGridViewInformationPasswordManager.DataSource = bindingSourceLocal;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการฺ Binding: " + ex.Message, "Debug");
            }
        }

        //==================================================================================

        // Check FileExist && Delet
        static bool IsDatabaseFileLocalExist()
        {
            // กำหนดที่อยู่ของโฟลเดอร์ที่ต้องการตรวจสอบ
            //test
            //string folderPath = @"C:\Users\Admin\source\repos\TestFunctionSQL\TestFunctionSQL\bin\Debug";
            //=====================================
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string myAppFolderPath = Path.Combine(documentsPath, "MyPassDocument\\GenerateKey");

            // ตรวจสอบว่าโฟลเดอร์มีอยู่หรือไม่
            if (!Directory.Exists(myAppFolderPath))
            {
                // ถ้าโฟลเดอร์ไม่มีอยู่ ส่งค่า false
                return false;
            }

            // ตรวจสอบไฟล์ .db ในโฟลเดอร์
            string[] dbFiles = Directory.GetFiles(myAppFolderPath, "*.db");

            // ถ้ามีไฟล์ .db ในโฟลเดอร์ แสดงว่า User เคยสร้าง CreateUser แล้ว
            return dbFiles.Length > 0;
        }
        static bool IsDatabaseFileServerExist()
        {
            // กำหนดที่อยู่ของโฟลเดอร์ที่ต้องการตรวจสอบ
            //test
            //string folderPath = @"C:\Users\Admin\source\repos\TestFunctionSQL\TestFunctionSQL\bin\Debug";
            //=====================================
            string folderPath = @"D:\Storage\ServerRemote";

            // ตรวจสอบว่าโฟลเดอร์มีอยู่หรือไม่
            if (!Directory.Exists(folderPath))
            {
                // ถ้าโฟลเดอร์ไม่มีอยู่ ส่งค่า false
                return false;
            }

            // ตรวจสอบไฟล์ .db ในโฟลเดอร์
            string[] dbFiles = Directory.GetFiles(folderPath, "*.db");

            // ถ้ามีไฟล์ .db ในโฟลเดอร์ แสดงว่า User เคยสร้าง CreateUser แล้ว
            return dbFiles.Length > 0;
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
        static bool IsUrlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps) &&
           (result.Host.EndsWith(".com", StringComparison.OrdinalIgnoreCase));
        }

        //==================================================================================
        //คาดว่าไม่น่าจะได้ใช้งาน

        //==================================================================================

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
        private static void DecryptFile(string sourceFilePath, string keyBase64, string destinationFilePath, string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

                // Create decryptor object
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                // Decrypt the file
                using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream destinationFileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(sourceFileStream, decryptor, CryptoStreamMode.Read))
                        {
                            byte[] buffer = new byte[1024]; // or any other suitable buffer size
                            int bytesRead;

                            while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                destinationFileStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
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
                csvWriter.WriteRecords(dbContextServer.CiphertextsServer.ToList());
                streamWriter.Flush();
                return memoryStream.ToArray();
            }
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
        private static byte[] DecryptData(byte[] encryptedData, string keyBase64, string vectorBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.IV = Convert.FromBase64String(vectorBase64);

                // Create decryptor object
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                // Decrypt the data
                using (MemoryStream memoryStream = new MemoryStream(encryptedData))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream decryptedMemoryStream = new MemoryStream())
                        {
                            cryptoStream.CopyTo(decryptedMemoryStream);
                            return decryptedMemoryStream.ToArray();
                        }
                    }
                }
            }
        }

        private static void EncryptFileVersionNotIV64(string inputFile, string outputFile, string keyBase64)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                aesAlgorithm.Key = Convert.FromBase64String(keyBase64);
                aesAlgorithm.Mode = CipherMode.CBC; // Choose a cipher mode (e.g., CBC)
                aesAlgorithm.Padding = PaddingMode.PKCS7; // Choose a padding mode (e.g., PKCS7)

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                using (FileStream inputStream = File.OpenRead(inputFile))
                using (FileStream outputStream = File.Create(outputFile))
                using (CryptoStream cryptoStream = new CryptoStream(outputStream, encryptor, CryptoStreamMode.Write))
                {
                    inputStream.CopyTo(cryptoStream);
                }
            }
        }

        private static void DecryptFileVersionNotIV64(string inputFile, string outputFile, string keyBase64)
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

        private static void EncryptFileVersionNot64(string sourceFilePath, string keyBase64, string destinationFilePath)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                // Generate a random Initialization Vector (IV)
                aesAlgorithm.GenerateIV();

                // Set the key, ensuring secure key handling
                aesAlgorithm.Key = GetSecureKey(keyBase64);// Implement proper key derivation/storage

                // Create encryptor object
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor();

                // Encrypt the file in a secure manner
                using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream destinationFileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(destinationFileStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(aesAlgorithm.IV, 0, aesAlgorithm.IV.Length); // Store IV directly alongside ciphertext

                    byte[] buffer = new byte[1024]; // Consider adjusting buffer size for performance
                    int bytesRead;
                    while ((bytesRead = sourceFileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cryptoStream.Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine($"File encrypted successfully to '{destinationFilePath}'");
            }
        }
        private static void DecryptFileVersionNot64(string sourceFilePath, string keyBase64, string destinationFilePath)
        {
            using (Aes aesAlgorithm = Aes.Create())
            {
                // Read the stored IV from the beginning of the encrypted file
                byte[] iv = new byte[aesAlgorithm.BlockSize / 8];
                using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    if (sourceFileStream.Read(iv, 0, iv.Length) != iv.Length)
                    {
                        throw new CryptographicException("Invalid IV format or file reading error!");
                    }
                    aesAlgorithm.IV = iv;
                }

                // Set the key, ensuring secure key handling
                aesAlgorithm.Key = GetSecureKey(keyBase64); // Implement proper key derivation/storage

                // Create decryptor object
                ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor();

                // Decrypt the file in a secure manner
                using (FileStream sourceFileStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read))
                using (FileStream destinationFileStream = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
                using (CryptoStream cryptoStream = new CryptoStream(sourceFileStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[1024]; // Consider adjusting buffer size for performance
                    int bytesRead;
                    while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        destinationFileStream.Write(buffer, 0, bytesRead);
                    }
                }

                Console.WriteLine($"File decrypted successfully to '{destinationFilePath}'");
            }
        }

        private static byte[] GetSecureKey(string keyBase64)
        {
            // Implement robust key derivation using a Key Derivation Function (KDF) like PBKDF2 or Argon2
            // Ensure secure key storage using a dedicated key management system or secure key derivation libraries

            // Placeholder for demonstration:
            return Convert.FromBase64String(keyBase64);
        }


        // Server Azure Action ==============================================================
        static async Task UploadFileToBlobAsync(MainPage mainPage)
        {
            string filePath = mainPage.dbContextServer.DatabasePath;
            //MessageBox.Show($"{filePath}", "Debug");
            if (isUploading) return; // เพิ่มเงื่อนไขเพื่อหยุดการเรียกตัวเอง
            isUploading = true;
            try
            {
                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);

                // Get a reference to the container (create it if it doesn't exist)
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(mainPage.containerName);
                await blobContainerClient.CreateIfNotExistsAsync();

                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(filePath));

                // Open the local file for reading
                using (var fileStream = File.OpenRead(filePath))
                {
                    // Upload the file to the blob in Azure Storage
                    await blobClient.UploadAsync(fileStream, true);
                }

                // Output success message to the console
                MessageBox.Show($"File uploaded to {blobClient.Uri}", "Debug");
            }
            catch (Exception ex)
            {
                // Handle and output any exceptions that occur during the upload
                MessageBox.Show($"Error uploading file: {ex.Message}", "Debug");
            }
            finally
            {
                isUploading = false;
            }
        }

        // Asynchronous method to download a blob from Azure  Storage
        static async Task DownloadBlobAsync(MainPage mainPage)
        {
            var existingGenerateKey = mainPage.dbGenerate.GenerateKey.Find(1);
            string Setname = $"CiphertextsServer{existingGenerateKey.HashNameFile}.csv";
            //string filePath = mainPage.dbContextServer.DatabasePath;
            string filePath = $"CiphertextsServer{existingGenerateKey.DeviceName}.csv";
            //MessageBox.Show($"filePath:{filePath}", "Debug");
            if (isDowloading) return; // เพิ่มเงื่อนไขเพื่อหยุดการเรียกตัวเอง
            try
            {
                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);

                // Get a reference to the container
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(mainPage.containerName);

                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(filePath);
                //var blobClient = blobContainerClient.GetBlobClient("123.txt");
                // Check if the blob exists in Azure Storage
                if (await blobClient.ExistsAsync())
                {
                    // Download information about the blob
                    BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                    // Open a local file for writing
                    using (var fileStream = File.OpenWrite(filePath))
                    {
                        // Copy the content of the blob to the local file
                        await blobDownloadInfo.Content.CopyToAsync(fileStream);


                    }

                    // Read the blob content as a stream
                    //using (Stream stream = blobDownloadInfo.Content)
                    //{
                    //    // Assuming the content is text, you can read it as text
                    //    using (StreamReader reader = new StreamReader(stream))
                    //    {
                    //        string content = await reader.ReadToEndAsync();
                    //        Console.WriteLine(content);
                    //    }
                    //}


                    // Output success message to the console
                    MessageBox.Show($"Blob downloaded to local_path_to_save_downloaded_file", "Debug");
                }
                else
                {
                    // Output a message if the blob does not exist
                    MessageBox.Show($"Blob {blobClient.Uri} does not exist.", "Debug");
                    mainPage.CheckPartServer = false;
                }
            }
            catch (Exception ex)
            {
                // Handle and output any exceptions that occur during the download
                MessageBox.Show($"Error downloading blob: {ex.Message}", "Debug");
            }
            finally
            {
                isDowloading = false;
            }
        }

        // Asynchronous method to delete a blob from Azure Blob Storage
        static async Task DeleteBlobAsync(MainPage mainPage)
        {

            string filePath = mainPage.dbContextServer.DatabasePath;
            if (isDeleting) return; // เพิ่มเงื่อนไขเพื่อหยุดการเรียกตัวเอง
            try
            {
                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);

                // Get a reference to the container
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(mainPage.containerName);

                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(filePath));

                // Check if the blob exists in Azure Storage before deleting
                if (await blobClient.ExistsAsync())
                {
                    // Delete the blob
                    await blobClient.DeleteIfExistsAsync();

                    // Output success message to the console
                    MessageBox.Show($"Blob {blobClient.Uri} deleted successfully", "Debug");
                }
                else
                {
                    // Output a message if the blob does not exist
                    MessageBox.Show($"Blob {blobClient.Uri} does not exist. No deletion performed.", "Debug");
                }
            }
            catch (Exception ex)
            {
                // Handle and output any exceptions that occur during the deletion
                MessageBox.Show($"Error deleting blob: {ex.Message}", "Debug");
            }
            finally
            {
                isDeleting = false;
            }
        }

        static async Task UploadFileToBlobAsyncMemoryVersion(MainPage mainPage, byte[] data, string blobName)
        {
            //MessageBox.Show($"{filePath}", "Debug");
            if (isUploading) return; // เพิ่มเงื่อนไขเพื่อหยุดการเรียกตัวเอง
            isUploading = true;
            try
            {

                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);

                // Get a reference to the container (create it if it doesn't exist)
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(mainPage.containerName);


                var blobClient = blobContainerClient.GetBlobClient(blobName);

                if (!blobContainerClient.Exists())
                {
                    // เมื่อเชื่อมต่อไม่สำเร็จ แสดง MessageBox เพื่อแจ้งเตือนผู้ใช้
                    AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", "ไม่สามารถเชื่อมต่อกับเซิร์ฟเวอร์ได้ โปรดตรวจสอบ connection string และลองอีกครั้ง");
                    azureMessagerBoxAlert.ShowDialog();
                    //MessageBox.Show("ไม่สามารถเชื่อมต่อกับเซิร์ฟเวอร์ได้ โปรดตรวจสอบ connection string และลองอีกครั้ง", "เกิดข้อผิดพลาด");
                    return;
                }

                // Check if the connection to Azure Blob Storage is successful
                if (blobContainerClient.Exists())
                {

                    // อัปโหลดข้อมูล CSV ไปยัง Blob
                    using (var memoryStream = new MemoryStream(data))
                    {
                        await blobClient.UploadAsync(memoryStream, true);
                    }

                    MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("Success", "Upload Server Part สำเร็จ");
                    miniMessagerBoxTextBoxNormal.ShowDialog();
                    CheckAzureUploadMemory = true;
                }
                else
                {
                    // Handle if connection to Azure Blob Storage fails
                    //throw new Exception("Failed to connect to Azure Blob Storage.");
                    MessageBox.Show($"Blob {blobClient.Uri} does not exist. No deletion performed.", "Debug");
                }

            }
            catch (Exception ex)
            {

                // Handle and output any exceptions that occur during the upload
                AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Error uploading file: {ex.Message}");
                azureMessagerBoxAlert.ShowDialog();
                CheckAzureUploadMemory = false;

                //MessageBox.Show($"Error uploading file: {ex.Message}", "Debug");
            }
            finally
            {
                isUploading = false;
            }
        }

        static async Task<byte[]> DownloadBlobAsyncMemoryVersion(MainPage mainPage)
        {
            var existingGenerateKey = mainPage.dbGenerate.GenerateKey.Find(1);
            //string filePath = mainPage.dbContextServer.DatabasePath;
            string filePath = $"{existingGenerateKey.DeviceName}.csv";
            //MessageBox.Show($"filePath:{filePath}", "Debug");
            try
            {
                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);

                // Get a reference to the container
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(mainPage.containerName);

                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(filePath);
                //var blobClient = blobContainerClient.GetBlobClient("123.txt");
                // Check if the blob exists in Azure Storage
                if (await blobClient.ExistsAsync())
                {
                    // Download information about the blob
                    BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                    // Open a local file for writing
                    // อ่านเนื้อหาของ blob ลงใน MemoryStream
                    using (var memoryStream = new MemoryStream())
                    {
                        await blobDownloadInfo.Content.CopyToAsync(memoryStream);

                        // รับ byte array จาก MemoryStream
                        byte[] fileBytes = memoryStream.ToArray();
                        // แสดงข้อความสำเร็จไปยังคอนโซล หรือทำการประมวลผลเพิ่มเติมตามต้องการของแอปพลิเคชัน
                        //MessageBox.Show($"Blob ดาวน์โหลดลงในหน่วยความจำแล้ว. Type : {memoryStream.GetType()}");

                        MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("Success", "Downloaded Server Part Successfully!");
                        miniMessagerBoxTextBoxNormal.ShowDialog();

                        //MessageBox.Show($"Blob downloaded Server Part to Memory ", "Debug");

                        return fileBytes;

                    }

                }
                else
                {
                    // Output a message if the blob does not exist
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", $"Blob {blobClient.Uri} does not exist.");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    // MessageBox.Show($"Blob {blobClient.Uri} does not exist.", "Debug");
                    mainPage.CheckPartServer = false;
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle and output any exceptions that occur during the download
                AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Error downloading blob: {ex.Message}");
                azureMessagerBoxAlert.ShowDialog();
                //MessageBox.Show($"Error downloading blob: {ex.Message}", "Debug");
                return null;
            }
        }

        static async Task DeleteBlobAsyncName(string DeviceName, MainPage mainPage)
        {

            if (isDeleting) return; // เพิ่มเงื่อนไขเพื่อหยุดการเรียกตัวเอง
            try
            {
                // Create a BlobServiceClient to connect to Azure Storage
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);

                // Get a reference to the container
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(mainPage.containerName);

                // Get a reference to the blob in the container
                var blobClient = blobContainerClient.GetBlobClient(DeviceName);

                // Check if the blob exists in Azure Storage before deleting
                if (await blobClient.ExistsAsync())
                {
                    // Delete the blob
                    await blobClient.DeleteIfExistsAsync();

                    // Output success message to the console
                    AzureMessagerBoxAlertNormal azureMessagerBoxAlertNormal = new AzureMessagerBoxAlertNormal("Success", $"Blob {blobClient.Uri} deleted successfully");
                    azureMessagerBoxAlertNormal.ShowDialog();
                    //MessageBox.Show($"Blob {blobClient.Uri} deleted successfully", "Debug");
                }
                else
                {
                    // Output a message if the blob does not exist
                    AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", $"Blob {blobClient.Uri} does not exist. No deletion performed.");
                    azureMessagerBoxAlert.ShowDialog();
                    //MessageBox.Show($"Blob {blobClient.Uri} does not exist. No deletion performed.", "Debug");
                }
            }
            catch (Exception ex)
            {
                // Handle and output any exceptions that occur during the deletion
                AzureMessagerBoxAlertNormal azureMessagerBoxAlertNormal = new AzureMessagerBoxAlertNormal("Success", $"Error deleting blob: {ex.Message}");
                azureMessagerBoxAlertNormal.ShowDialog();
                //MessageBox.Show($"Error deleting blob: {ex.Message}", "Debug");
            }
            finally
            {
                isDeleting = false;
            }
        }

        static async Task<bool> CheckAzureConnectionAsync(MainPage mainPage)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(mainPage.connectionString);
                await blobServiceClient.GetPropertiesAsync();
                return true; // การเชื่อมต่อสำเร็จ
            }
            catch (RequestFailedException ex)
            {
                // การเชื่อมต่อไม่สำเร็จ
                AzureMessagerBoxAlert azureMessagerBoxAlert = new AzureMessagerBoxAlert("Error", "การเชื่อมต่อไม่สำเร็จ");
                azureMessagerBoxAlert.ShowDialog();
                Console.WriteLine($"Failed to connect to Azure Storage: {ex.Message}");
                return false;
            }
        }



        //===============================================================================

        // Closing
        private async void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            // ทำการปิดโปรแกรม
            // การทำงาน Part Server
            //string VectorBase64ForData;
            //string VectorBase64ForFile;
            //string PathLocalForuse;
            //string PathLocalEncrypt;
            //string PathGenerateForUse;
            //string PathGenerateEncrypt;

            //if (Directory.Exists(filePathToDeleteDataDevice))
            //{
            //    var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
            //    string Setname = $"{existingGenerateKey.DeviceName}CiphertextsServer{existingGenerateKey.HashNameFile}.csv";
            //    byte[] csvData = GenerateCsvFromDbContext();
            //    byte[] encryptCsvData = EncryptData(csvData, existingGenerateKey.Sha256Hash, out VectorBase64ForData);
            //    existingGenerateKey.VectorBase64ForAllData = VectorBase64ForData;
            //    dbGenerate.SaveChanges();


            //    PathLocalForuse = await dbContextLocal.SetCombinePath();
            //    PathLocalEncrypt = await dbContextLocalEncrypt.SetCombinePath();

            //    PathGenerateForUse = dbGenerate.DatabasePath;
            //    //PathGenerateEncrypt = dbGenerateKeyEncrypt.DatabasePath;



            //    // แสดง MessageBox และรับผลลัพธ์
            //    DialogResult result = MessageBox.Show("คุณต้องการที่จะปิดโปรแกรมหรือไม่?", "ยืนยันการปิด", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    // ตรวจสอบผลลัพธ์ที่ได้

            //    if (result == DialogResult.Yes)
            //    {
            //        // ทำตามขั้นตอนที่ต้องการก่อนปิดแอป (ถ้ามี)
            //        //await UploadFileToBlobAsync(this);
            //        await UploadFileToBlobAsyncMemoryVersion(this, encryptCsvData, Setname);
            //        //DeleteDbFile(filePathToDelete);
            //        //MessageBox.Show("ไฟล์ Part Server ที่ Local  ถูกลบแล้ว", "Debug");
            //        MessageBox.Show("Upload Part Server สำเร็จ", "Debug");

            //        EncryptFile(PathLocalForuse, existingGenerateKey.Sha256Hash, PathLocalEncrypt, out VectorBase64ForFile);
            //        existingGenerateKey.VectorBase64ForFile = VectorBase64ForFile;
            //        dbGenerate.SaveChanges();
            //        DeleteFileDirectly(PathLocalForuse);

            //        // ใจเย็นๆๆๆๆๆๆ
            //        //EncryptFileVersionNot64(PathGenerateForUse,PathGenerateEncrypt,existingGenerateKey.HashMasterPassword);
            //        //DeleteFileDirectly(PathGenerateForUse);
            //        Application.Exit();

            //    }
            //    else
            //    {
            //        // ยกเลิกการปิดแอป
            //        e.Cancel = true;
            //    }
            //}
            //else 
            //{
            //    Console.WriteLine($"Directory does not exist: {filePathToDeleteDataDevice}");
            //    Application.Exit();
            //}



        }

        // ButtonTest
        private async void TestFilePart_Click(object sender, EventArgs e)
        {
            await UploadFileToBlobAsync(this);
        }

        private async void TestFileDowloadButton_Click(object sender, EventArgs e)
        {
            await DownloadBlobAsync(this);
        }

        private async void TestFileDeleteButton_Click(object sender, EventArgs e)
        {
            await DeleteBlobAsync(this);
        }

        private void TestDowloadToMemory_Click(object sender, EventArgs e)
        {
            try
            {
                string VectorBase64;
                // ดาวน์โหลดข้อมูล Blob และรับ byte array กลับมา

                byte[] csvData = GenerateCsvFromDbContext();
                // บันทึกไฟล์ CSV ที่เข้ารหัสลงในเครื่อง
                string CsvFilePath = $"D:\\Storage\\StreamMemoryToFileCSV";
                Directory.CreateDirectory(CsvFilePath);
                string CombinePathCsvFile = Path.Combine(CsvFilePath, $"CiphertextsServer.csv"); ;
                File.WriteAllBytes(CombinePathCsvFile, csvData);

                MessageBox.Show("บันทึกลงไฟล์สำเร็จ CsvFilePath ", "แจ้งเตือน");

                // Encrypttion Data

                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                byte[] encryptCsvData = EncryptData(csvData, existingMemoryMain.Sha256Hash, out VectorBase64);
                existingGenerateKey.VectorBase64ForAllData = VectorBase64;
                dbGenerate.SaveChanges();
                string CsvFileEncryptPath = $"D:\\Storage\\StreamMemoryToFileCSVEncrypt";
                Directory.CreateDirectory(CsvFilePath);
                string CombinePathCsvFileEncrypt = Path.Combine(CsvFilePath, $"CiphertextsServerEncrypt.csv"); ;
                File.WriteAllBytes(CombinePathCsvFileEncrypt, encryptCsvData);

                MessageBox.Show("บันทึกลงไฟล์สำเร็จ Encrypttion File ", "แจ้งเตือน");


                // Decryption DAta

                byte[] encryptedData = File.ReadAllBytes(CombinePathCsvFileEncrypt);
                byte[] decryptCsvData = DecryptData(encryptedData, existingMemoryMain.Sha256Hash, existingGenerateKey.VectorBase64ForAllData);
                string CsvFileDecryptPath = $"D:\\Storage\\StreamMemoryToFileCSVDecrypt";
                Directory.CreateDirectory(CsvFilePath);
                string CombinePathCsvFileDecrypt = Path.Combine(CsvFilePath, $"CiphertextsServerDecrypt.csv"); ;
                File.WriteAllBytes(CombinePathCsvFileDecrypt, decryptCsvData);

                MessageBox.Show("บันทึกลงไฟล์สำเร็จ Decrypttion File ", "แจ้งเตือน");




            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Debug");
            }
        }
        private void buttonDowloadCSV_Click(object sender, EventArgs e)
        {

        }

        // Check
        public void SetCheckAction(bool value)
        {
            CheckAction = value;
        }




        //ตกแต่ง Textbox
        private void InitializeTextBoxPlaceholder()
        {
            // ตั้งค่า Textbox และกำหนดสีตัวหนังสือเทา
            //myPassTextBox1.ForeColor = Color.Gray;
            //myPassTextBox1.Text = TextBoxPlaceholderTitile;
            textBoxUsername.ForeColor = Color.Gray;
            textBoxUsername.Text = "";
            textBoxPassword.ForeColor = Color.Gray;
            textBoxPassword.Text = "";

            // กำหนด Event Handlers
            //myPassTextBox1.Enter += textBoxTitle_Enter;
            //myPassTextBox1.Leave += textBoxTitle_Leave;
            textBoxUsername.Enter += textBoxUsername_Enter;
            textBoxUsername.Leave += textBoxUsername_Leave;
            textBoxPassword.Enter += textBoxPassword_Enter;
            textBoxPassword.Leave += textBoxPassword_Leave;
        }

        private void InitializeMyPassTextBoxPlaceholder()
        {
            myPassTextBoxTitle.PlaceholderText = "FB_faceหลัก_Faceหลุม";
            myPassTextBoxUsername.Texts = "";
            myPassTextBoxPassword.Texts = "";
            myPassTextBoxURL.Texts = "";
        }



        //private void textBoxTitle_Enter(object sender, EventArgs e)
        //{
        //    // เมื่อ TextBox ได้รับการ Focus, ให้ลบข้อความไกด์และเปลี่ยนสีตัวหนังสือเป็นสีดำ
        //    if (myPassTextBox1.Text == TextBoxPlaceholderTitile)
        //    {
        //        myPassTextBox1.Text = "";
        //        myPassTextBox1.ForeColor = Color.Black;
        //    }
        //}

        //private void textBoxTitle_Leave(object sender, EventArgs e)
        //{
        //    // เมื่อ TextBox ทำการลอกออก, ถ้าไม่มีข้อความใน TextBox ให้กลับไปใส่ข้อความไกด์และเปลี่ยนสีตัวหนังสือเป็นสีเทา
        //    if (myPassTextBox1.Text == "")
        //    {
        //        myPassTextBox1.Text = TextBoxPlaceholderTitile;
        //        myPassTextBox1.ForeColor = Color.Gray;
        //    }
        //}

        private void textBoxUsername_Enter(object sender, EventArgs e)
        {
            // เมื่อ TextBox ได้รับการ Focus, ให้ลบข้อความไกด์และเปลี่ยนสีตัวหนังสือเป็นสีดำ
            if (textBoxUsername.Text == TextBoxPlaceholderUsername)
            {
                textBoxUsername.Text = "";
                textBoxUsername.ForeColor = Color.Black;
            }

        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            // เมื่อ TextBox ทำการลอกออก, ถ้าไม่มีข้อความใน TextBox ให้กลับไปใส่ข้อความไกด์และเปลี่ยนสีตัวหนังสือเป็นสีเทา
            if (textBoxUsername.Text == "")
            {
                textBoxUsername.Text = TextBoxPlaceholderUsername;
                textBoxUsername.ForeColor = Color.Gray;
            }
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == TextBoxPlaceholderPassword)
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.Black;
            }

        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = TextBoxPlaceholderPassword;
                textBoxPassword.ForeColor = Color.Gray;
            }

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
            if (comboBoxUserType.SelectedItem.ToString().Trim() == "...Add")
            {

                // เรียกฟังก์ชันหรือแสดงกล่องข้อความให้ผู้ใช้กรอกข้อมูลเพิ่มเติม
                // ในที่นี้ จะให้แสดงกล่องข้อความเพื่อกรอกข้อมูลใหม่
                AddURLForm addUrlForm = new AddURLForm();
                addUrlForm.ShowDialog();

                //if()
                if (addUrlForm.URLinput == null)
                {
                    MessageBox.Show($"{addUrlForm.URLinput}", "มันว่างเปล่า");
                    return;

                }
                else
                {
                    MessageBox.Show($"{addUrlForm.URLinput}", "Link URL ของคุณ");
                    comboBoxUserType.Items.Insert(comboBoxUserType.Items.Count - 1, addUrlForm.URLinput);
                    comboBoxUserType.SelectedItem = addUrlForm.URLinput;

                }

            }
        }

        private void ButtonExport_Click(object sender, EventArgs e)
        {
            try
            {

                var ciphertextsListLocal = dbContextLocal.Ciphertexts.ToList();
                var ciphertextsListServer = dbContextServer.CiphertextsServer.ToList();
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                CiphertextLocal newCiphertext = new CiphertextLocal();
                ExportFile newExport = new ExportFile();


                var mergedCiphertexts = ciphertextsListLocal.Join(ciphertextsListServer,
                    local => local.UsernameConnectURL,
                    server => server.UsernameConnectURL,
                    (local, server) => new
                    {
                        Id = local.Id,
                        Title = local.CiphertextTitle,
                        Username = local.CiphertextUsername,
                        MergedValue = local.CiphertextPasswordLocal + server.CiphertextPasswordServer,// นำค่า Value มารวมกัน
                        vectorBase64 = local.vectorBase64,
                        URL = local.URL,
                        UsernameConnectURL = local.UsernameConnectURL,
                    }).ToList();




                // สร้าง SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "SQLite Database Files (*.db)|*.db|All files (*.*)|*.*";
                saveFileDialog.Title = "บันทึกไฟล์ฐานข้อมูล SQLite";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // เมื่อผู้ใช้เลือกตำแหน่งและชื่อไฟล์ที่ต้องการบันทึก
                    string filePath = saveFileDialog.FileName;
                    DbExportFile dbExportFile = new DbExportFile(filePath);
                    dbExportFile.Database.EnsureCreated();
                    foreach (var mergedCiphertext in mergedCiphertexts)
                    {

                        string decryptedData = DecryptDataWithAes(mergedCiphertext.MergedValue, existingMemoryMain.Sha256Hash, mergedCiphertext.vectorBase64);

                        newExport.Id = mergedCiphertext.Id;
                        newExport.Title = mergedCiphertext.Title;
                        newExport.Username = mergedCiphertext.Username;
                        newExport.PainTextPassword = decryptedData;
                        newExport.URL = mergedCiphertext.URL;
                        newExport.UsernameConnectURL = mergedCiphertext.UsernameConnectURL;


                        dbExportFile.ExportFile.Add(newExport);
                        dbExportFile.SaveChanges();

                        //MessageBox.Show($"Ciphertext ID: {mergedCiphertext.Id}, รหัสผ่าน: {decryptedData}", "ข้อมูล Ciphertext", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // บันทึกข้อมูลลงในไฟล์ ตามที่ User Set Path เอง 
                    //System.IO.File.WriteAllLines(filePath, dbExportFile.ExportFile.Select(item => $"{item.Id},{item.Title},{item.Username},{item.PainTextPassword},{item.URL},{item.UsernameConnectURL}"));

                    // แสดง MessageBox หรือทำอย่างอื่นตามที่คุณต้องการ 
                    MessageBox.Show($"ข้อมูลถูก export ไปยังไฟล์ {filePath} แล้ว", "การทำงานเสร็จสิ้น", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

            }
            catch (Exception ex)
            {
                // แสดง Inner Exception ใน MessageBox
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.InnerException?.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            BackupPage backupPage = new BackupPage();
            backupPage.ShowDialog();
        }

        private void buttonEncryptionFile_Click(object sender, EventArgs e)
        {
            string vectorBase64_this;
            var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
            //byte[] key_this = Encoding.UTF8.GetBytes(existingGenerateKey.Sha256Hash);
            //MessageBox.Show($"KeyLength = {key_this.Length}");
            MasterPasswordInpuForm masterPasswordInpuForm = new MasterPasswordInpuForm();
            masterPasswordInpuForm.ShowDialog();

            string inputfilePath = $"D:\\Storage\\LocalCiphertexts\\LocalCiphertexts61ObVZ7lWpmxUhaVk8RJdjI1oi9wFbH5+4P6I6P7S+M=.db";
            //EncryptFile(inputfilePath, outputfilePathEncryption, key_this);
            EncryptFile(dbGenerate.DatabasePath, ComputeSha256Hash(masterPasswordInpuForm.MasterPassword), outputfilePathEncryption, out vectorBase64_this);

            // Write vectorBase64 to file
            string outputfilePathEncryption_Path = $"D:\\Storage\\LocalCiphertextsEncryption\\vectorBase64.txt";
            using (StreamWriter writer = new StreamWriter(outputfilePathEncryption_Path))
            {
                writer.WriteLine(vectorBase64_this);
            }


            //MasterPasswordInpuForm masterPasswordInpuForm = new MasterPasswordInpuForm();
            //masterPasswordInpuForm.ShowDialog();

            ////EncryptFileVersionNotIV64(dbGenerate.DatabasePath,outputfilePathEncryption, ComputeSha256Hash(masterPasswordInpuForm.MasterPassword));
            //EncryptFileVersionNot64(dbGenerate.DatabasePath, ComputeSha256Hash(masterPasswordInpuForm.MasterPassword), outputfilePathEncryption);




        }

        public string outputfilePathEncryption
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                var folderPath = $"D:\\Storage\\LocalCiphertextsEncryption";
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);

                Directory.CreateDirectory(folderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(folderPath, $"LocalCiphertextsEncryption{existingGenerateKey.HashNameFile}.db");
            }

        }
        public string outputfilePathDecryption
        {
            get
            {
                // สร้างโฟลเดอร์หากยังไม่มี
                var folderPath = $"D:\\Storage\\LocalCiphertextsDecryption";
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);

                Directory.CreateDirectory(folderPath);

                // ส่งคืนเส้นทางเต็มรูปแบบที่รวมถึงโฟลเดอร์
                return Path.Combine(folderPath, $"LocalCiphertextsDecryption{existingGenerateKey.HashNameFile}.db");
            }

        }

        private void buttonDecryptionFile_Click(object sender, EventArgs e)
        {
            string outputfilePathEncryption_Path = $"D:\\Storage\\LocalCiphertextsEncryption\\vectorBase64.txt";
            string vectorBase64_this;
            var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
            string inputfilePath = outputfilePathEncryption;
            using (StreamReader reader = new StreamReader(outputfilePathEncryption_Path))
            {
                vectorBase64_this = reader.ReadLine();
            }
            MasterPasswordInpuForm masterPasswordInpuForm = new MasterPasswordInpuForm();
            masterPasswordInpuForm.ShowDialog();

            //DecryptFile(inputfilePath, existingGenerateKey.Sha256Hash, outputfilePathDecryption, vectorBase64_this);
            DecryptFile(inputfilePath, ComputeSha256Hash(masterPasswordInpuForm.MasterPassword), outputfilePathDecryption, vectorBase64_this);

            //string inputfilePath = outputfilePathEncryption;

            //MasterPasswordInpuForm masterPasswordInpuForm = new MasterPasswordInpuForm();
            //masterPasswordInpuForm.ShowDialog();

            ////DecryptFileVersionNotIV64(inputfilePath, outputfilePathDecryption, ComputeSha256Hash(masterPasswordInpuForm.MasterPassword));
            //DecryptFileVersionNot64(inputfilePath, ComputeSha256Hash(masterPasswordInpuForm.MasterPassword), outputfilePathDecryption);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PasswordGenerateForm passwordGenerateForm = new PasswordGenerateForm();
            passwordGenerateForm.ShowDialog();
        }

        private void ButtonReadMemory_Click(object sender, EventArgs e)
        {

        }

        private void buttonUploadCSVTest_Click(object sender, EventArgs e)
        {

        }

        private void buttonDeleteServerPart_Click(object sender, EventArgs e)
        {
            MyDevice myDevice = new MyDevice();
            myDevice.ShowDialog();

        }

        public async void buttonResetApp_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("คุณต้องการที่จะลบข้อมูลทั้งหมดที่เกี่ยวกับ Device นี้ใช่หรือไม่?", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                CheckActionForm checkActionForm = new CheckActionForm();
                checkActionForm.ShowDialog();

                if (checkActionForm.CheckAction == true)
                {
                    await DeleteBlobAsync(this);
                    DeleteFileAllInPath(filePathToDeleteDataDevice);
                    Application.Exit();
                }
                else
                {
                }

            }
            else
            {
                // กลับเข้าสู่หน้าหลัก
                return;
            }

        }

        private async void pictureBoxCloseProgram_Click(object sender, EventArgs e)
        {
            // ทำการปิดโปรแกรม
            // การทำงาน Part Server
            string VectorBase64ForData;
            string VectorBase64ForFile;
            string PathLocalForuse;
            string PathLocalEncrypt;
            string PathGenerateForUse;
            string PathGenerateEncrypt;

            if (Directory.Exists(filePathToDeleteDataDevice))
            {
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                string Setname = $"{existingGenerateKey.DeviceName}.csv";
                byte[] csvData = GenerateCsvFromDbContext();
                byte[] encryptCsvData = EncryptData(csvData, existingMemoryMain.Sha256Hash, out VectorBase64ForData);
                existingGenerateKey.VectorBase64ForAllData = VectorBase64ForData;
                dbGenerate.SaveChanges();


                PathLocalForuse = await dbContextLocal.SetCombinePath();
                PathLocalEncrypt = await dbContextLocalEncrypt.SetCombinePath();

                PathGenerateForUse = dbGenerate.DatabasePath;
                //PathGenerateEncrypt = dbGenerateKeyEncrypt.DatabasePath;



                // แสดง MessageBox และรับผลลัพธ์
                DialogResult result = MessageBox.Show("คุณต้องการที่จะปิดโปรแกรมหรือไม่?", "ยืนยันการปิด", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // ตรวจสอบผลลัพธ์ที่ได้

                if (result == DialogResult.Yes)
                {
                    // ทำตามขั้นตอนที่ต้องการก่อนปิดแอป (ถ้ามี)
                    //await UploadFileToBlobAsync(this);
                    await UploadFileToBlobAsyncMemoryVersion(this, encryptCsvData, Setname);
                    //DeleteDbFile(filePathToDelete);
                    //MessageBox.Show("ไฟล์ Part Server ที่ Local  ถูกลบแล้ว", "Debug");
                    MessageBox.Show("Upload Part Server สำเร็จ", "Debug");

                    EncryptFile(PathLocalForuse, existingMemoryMain.Sha256Hash, PathLocalEncrypt, out VectorBase64ForFile);
                    existingGenerateKey.VectorBase64ForFile = VectorBase64ForFile;
                    dbGenerate.SaveChanges();
                    DeleteFileDirectly(PathLocalForuse);

                    // ใจเย็นๆๆๆๆๆๆ
                    //EncryptFileVersionNot64(PathGenerateForUse,PathGenerateEncrypt,existingGenerateKey.HashMasterPassword);
                    //DeleteFileDirectly(PathGenerateForUse);
                    Application.Exit();

                }
                else
                {
                    // ยกเลิกการปิดแอป
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Directory does not exist: {filePathToDeleteDataDevice}");
                Application.Exit();
            }


        }



        public async void ClosingStepFunction()
        {
            // ทำการปิดโปรแกรม
            // การทำงาน Part Server
            string VectorBase64ForData;
            string VectorBase64ForFile;
            string PathLocalForuse;
            string PathLocalEncrypt;
            string PathGenerateForUse;
            string PathGenerateEncrypt;

            if (Directory.Exists(filePathToDeleteDataDevice))
            {
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                string Setname = $"{existingGenerateKey.DeviceName}.csv";
                byte[] csvData = GenerateCsvFromDbContext();
                byte[] encryptCsvData = EncryptData(csvData, existingMemoryMain.Sha256Hash, out VectorBase64ForData);
                existingGenerateKey.VectorBase64ForAllData = VectorBase64ForData;
                dbGenerate.SaveChanges();


                PathLocalForuse = await dbContextLocal.SetCombinePath();
                PathLocalEncrypt = await dbContextLocalEncrypt.SetCombinePath();

                PathGenerateForUse = dbGenerate.DatabasePath;
                //PathGenerateEncrypt = dbGenerateKeyEncrypt.DatabasePath;



                // แสดง MessageBox และรับผลลัพธ์
                ClosingAlert closingAlert = new ClosingAlert();
                closingAlert.ShowDialog();

                // DialogResult result = MessageBox.Show("คุณต้องการที่จะปิดโปรแกรมหรือไม่?", "ยืนยันการปิด", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                // ตรวจสอบผลลัพธ์ที่ได้

                if (closingAlert.DialogResultEditForm == true)
                {
                    // ทำตามขั้นตอนที่ต้องการก่อนปิดแอป (ถ้ามี)
                    //await UploadFileToBlobAsync(this);
                    await UploadFileToBlobAsyncMemoryVersion(this, encryptCsvData, Setname);
                    //DeleteDbFile(filePathToDelete);
                    //MessageBox.Show("ไฟล์ Part Server ที่ Local  ถูกลบแล้ว", "Debug");
                    // MessageBox.Show("Upload Part Server สำเร็จ", "Debug");

                    EncryptFile(PathLocalForuse, existingMemoryMain.Sha256Hash, PathLocalEncrypt, out VectorBase64ForFile);
                    existingGenerateKey.VectorBase64ForFile = VectorBase64ForFile;
                    dbGenerate.SaveChanges();
                    DeleteFileDirectly(PathLocalForuse);

                    // ใจเย็นๆๆๆๆๆๆ
                    //EncryptFileVersionNot64(PathGenerateForUse,PathGenerateEncrypt,existingGenerateKey.HashMasterPassword);
                    //DeleteFileDirectly(PathGenerateForUse);
                    Application.Exit();

                }
                else
                {
                    // ยกเลิกการปิดแอป
                    return;
                }
            }
            else
            {
                Console.WriteLine($"Directory does not exist: {filePathToDeleteDataDevice}");
                Application.Exit();
            }

        }
        // Setup Action
        public async Task UploadMemoryStepSetup(string Devicename)
        {
            // ทำการปิดโปรแกรม
            // การทำงาน Part Server
            string VectorBase64ForData;
            string VectorBase64ForFile;
            string PathLocalForuse;
            string PathLocalEncrypt;
            string PathGenerateForUse;
            string PathGenerateEncrypt;

            if (Directory.Exists(filePathToDeleteDataDevice))
            {
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                string Setname = $"{Devicename}.csv";
                byte[] csvData = GenerateCsvFromDbContext();
                byte[] encryptCsvData = EncryptData(csvData, existingMemoryMain.Sha256Hash, out VectorBase64ForData);
                existingGenerateKey.VectorBase64ForAllData = VectorBase64ForData;
                dbGenerate.SaveChanges();


                PathLocalForuse = await dbContextLocal.SetCombinePath();
                PathLocalEncrypt = await dbContextLocalEncrypt.SetCombinePath();

                PathGenerateForUse = dbGenerate.DatabasePath;
                //PathGenerateEncrypt = dbGenerateKeyEncrypt.DatabasePath;

                await UploadFileToBlobAsyncMemoryVersion(this, encryptCsvData, Setname);
                // MessageBox.Show("UpDate Device สำเร็จ", "Debug");

            }
            else
            {
                Console.WriteLine($"Directory does not exist: {filePathToDeleteDataDevice}");
                Application.Exit();
            }

        }

        public async void DeleteStepSetup(string DeviceName)
        {
            await DeleteBlobAsyncName(DeviceName, this);
        }

        public void ExportStepSetup()
        {
            try
            {

                var ciphertextsListLocal = dbContextLocal.Ciphertexts.ToList();
                var ciphertextsListServer = dbContextServer.CiphertextsServer.ToList();
                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingAzureConnectionString = dbAzureConnectionString.azureConnectionStrings.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);
                CiphertextLocal newCiphertext = new CiphertextLocal();
                ExportFile newExport = new ExportFile();


                var mergedCiphertexts = ciphertextsListLocal.Join(ciphertextsListServer,
                    local => local.UsernameConnectURL,
                    server => server.UsernameConnectURL,
                    (local, server) => new
                    {
                        Id = local.Id,
                        Title = local.CiphertextTitle,
                        Username = local.CiphertextUsername,
                        MergedValue = local.CiphertextPasswordLocal + server.CiphertextPasswordServer,// นำค่า Value มารวมกัน
                        vectorBase64 = local.vectorBase64,
                        URL = local.URL,
                        UsernameConnectURL = local.UsernameConnectURL,
                    }).ToList();




                // สร้าง SaveFileDialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "SQLite Database Files (*.db)|*.db";
                saveFileDialog.Title = "บันทึกไฟล์ฐานข้อมูล SQLite";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // เมื่อผู้ใช้เลือกตำแหน่งและชื่อไฟล์ที่ต้องการบันทึก
                    string filePath = saveFileDialog.FileName;
                    if (filePath == "")
                    {
                        MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "คุณไม่ได้กำหนดชื่อไฟล์ที่คุณต้องการ Export กรุณากำหนดชื่อด้วย");
                        miniMessagerBoxTextBoxAlert.ShowDialog();
                        return;
                    }
                    DbExportFile dbExportFile = new DbExportFile(filePath);
                    dbExportFile.Database.EnsureCreated();
                    foreach (var mergedCiphertext in mergedCiphertexts)
                    {

                        string decryptedData = DecryptDataWithAes(mergedCiphertext.MergedValue, existingMemoryMain.Sha256Hash, mergedCiphertext.vectorBase64);
                        //string decryptedDataConnectionstring = DecryptDataWithAes(existingAzureConnectionString.ConnectionString, existingMemoryMain.Sha256Hash,existingGenerateKey.VectorBase64ForAzureConnection);

                        newExport.Id = mergedCiphertext.Id;
                        newExport.Title = mergedCiphertext.Title;
                        newExport.Username = mergedCiphertext.Username;
                        newExport.PainTextPassword = decryptedData;
                        newExport.URL = mergedCiphertext.URL;
                        newExport.UsernameConnectURL = mergedCiphertext.UsernameConnectURL;
                        if (newExport.Id <= 1)
                        {
                            newExport.Connectionstring = connectionString;
                            newExport.ContainerName = containerName;
                        }

                        dbExportFile.ExportFile.Add(newExport);
                        dbExportFile.SaveChanges();

                        //MessageBox.Show($"Ciphertext ID: {mergedCiphertext.Id}, รหัสผ่าน: {decryptedData}", "ข้อมูล Ciphertext", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // บันทึกข้อมูลลงในไฟล์ ตามที่ User Set Path เอง 
                    //System.IO.File.WriteAllLines(filePath, dbExportFile.ExportFile.Select(item => $"{item.Id},{item.Title},{item.Username},{item.PainTextPassword},{item.URL},{item.UsernameConnectURL}"));

                    // แสดง MessageBox หรือทำอย่างอื่นตามที่คุณต้องการ
                    MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("การทำงานเสร็จสิ้น", $"ข้อมูลถูก export ไปยังไฟล์ {filePath} แล้ว");
                    miniMessagerBoxTextBoxNormal.ShowDialog();

                    //MessageBox.Show($"ข้อมูลถูก export ไปยังไฟล์ {filePath} แล้ว", "การทำงานเสร็จสิ้น", MessageBoxButtons.OK, MessageBoxIcon.Information);


                }

            }
            catch (Exception ex)
            {
                // แสดง Inner Exception ใน MessageBox
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.InnerException?.Message}", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void buttonStyleMypass1_Click(object sender, EventArgs e)
        {

        }


        private void buttonStyleMypass1_Click_2(object sender, EventArgs e)
        {
            // สร้าง Object ใหม่ของคลาส Ciphertext
            try
            {

                CiphertextLocal newCiphertext = new CiphertextLocal();
                CiphertextServer newCiphertextServer = new CiphertextServer();


                string vectorBase64;
                string ciphertext_Password;
                string firstHalf;
                string secondHalf;


                var existingGenerateKey = dbGenerate.GenerateKey.Find(1);
                var existingMemoryMain = dbMemoryMain.memoryMains.Find(1);

                //รับ Input string จาก TextBox มาเก็บไว้ใน Object
                newCiphertext.CiphertextTitle = myPassTextBoxTitle.Texts;
                newCiphertext.CiphertextUsername = myPassTextBoxUsername.Texts;

                //ทำการ Encryption
                if (existingGenerateKey != null)
                {
                    ciphertext_Password = EncryptDataWithAes(myPassTextBoxPassword.Texts, existingMemoryMain.Sha256Hash, out vectorBase64);
                }
                else
                {
                    MessageBox.Show("ไม่พบ Key บนเครื่องคุณ", "Error");
                    return;
                }


                // ในความเป็นจริงไม่ต้องเก็บ อันนี้เก็บเพื่อการ Test ว่า funtion  EncryptDataWithAes และ Split ทำงานถูกต้องหรือไม่
                //newCiphertext.CiphertextPassword = ciphertext_Password;
                //==============================================================

                // เก็บ vectorBase64 ไว้ใช้ในการ Decryption
                newCiphertext.vectorBase64 = vectorBase64;

                // ทำการแยกเป็น 2 Part
                Split(ciphertext_Password, out firstHalf, out secondHalf);

                // นำไปเก็บไว้ใน Object 
                newCiphertext.CiphertextPasswordLocal = firstHalf;

                // ในความเป็นจริงไม่ต้องเก็บ อันนี้เก็บเพื่อการ Test ในกรณีที่ยังไม่ได้สร้างที่เก็บแยกไป Server จริงๆ
                //newCiphertext.CiphertextPasswordServer = secondHalf;

                // นำไปเก็บไว้ใน Object ====Database Server ==========
                newCiphertextServer.CiphertextPasswordServer = secondHalf;

                if (myPassTextBoxPassword.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Password ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("กรุณากรอก Password ด้วยครับ", "Error");
                    return;
                }
                if (myPassTextBoxTitle.Texts == "" || myPassTextBoxTitle.Texts == "FB_faceหลัก_Faceหลุม")
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
                #region -> ตัวเก่า
                //if (comboBoxUserType.SelectedItem == null || comboBoxUserType.SelectedItem.ToString().Trim() == "...Add")
                //{
                //    MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                //    return;
                //}
                //else
                //{
                //    if (comboBoxUserType.SelectedItem.ToString().Trim() == "Facebook")
                //    {
                //        newCiphertext.URL = "https://facebook.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //    }
                //    else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Youtube")
                //    {
                //        newCiphertext.URL = "https://www.youtube.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //    }
                //    else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Discord")
                //    {
                //        newCiphertext.URL = "https://discord.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";

                //    }
                //    else if (comboBoxUserType.SelectedItem.ToString().Trim() == "Twitter")
                //    {
                //        newCiphertext.URL = "https://twitter.com/";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{newCiphertext.URL}";

                //    }
                //    else
                //    {
                //        newCiphertext.URL = $"{comboBoxUserType.SelectedItem.ToString()}";
                //        newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{comboBoxUserType.SelectedItem.ToString()}";
                //        newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{comboBoxUserType.SelectedItem.ToString()}";
                //    }


                //    //newCiphertext.URL = $"{comboBoxUserType.SelectedItem.ToString()}";

                //    //newCiphertext.UsernameConnectURL = $"{textBoxUsername.Text}{comboBoxUserType.SelectedItem.ToString()}";

                //    var existingCiphertext = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == newCiphertext.UsernameConnectURL.Trim());
                //    //var existingCiphertextUsername = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.CiphertextUsername == newCiphertext.CiphertextUsername);
                //    //var existingCiphertextUserType = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.URL == newCiphertext.URL);


                //    if (existingCiphertext != null)
                //    {

                //        MessageBox.Show("กรุณากรอก Username หรือ URL ใหม่ด้วยครับ", "Error");
                //        return;

                //    }
                //    else
                //    {

                //        //// เพิ่ม Object ล่าสุดลงใน List
                //        BindingListCiphertext.Add(newCiphertext);
                //        BindingListCiphertextServer.Add(newCiphertextServer);
                //        SaveDataToListBinding();
                //        //// บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
                //        //// ฝั่ง Local  =============================
                //        dbContextLocal.Ciphertexts.Add(newCiphertext);
                //        dbContextLocal.SaveChanges();
                //        //// ฝั่ง Server =============================
                //        dbContextServer.CiphertextsServer.Add(newCiphertextServer);
                //        dbContextServer.SaveChanges();
                //        //ตกแต่ง TextBox
                //        //textBoxTitle.Text = TextBoxPlaceholderTitile;
                //        //textBoxUsername.Text = TextBoxPlaceholderUsername;
                //        //textBoxPassword.Text = TextBoxPlaceholderPassword;
                //        InitializeTextBoxPlaceholder();

                //    }
                //}
                #endregion

                if (myPassTextBoxURL.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก URL ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    //MessageBox.Show("กรุณากรอก URL ด้วยครับ", "Error");
                    return;
                }

                newCiphertext.URL = $"{myPassTextBoxURL.Texts}";
                newCiphertext.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{myPassTextBoxURL.Texts}";
                newCiphertextServer.UsernameConnectURL = $"{myPassTextBoxUsername.Texts}{myPassTextBoxURL.Texts}";

                var existingCiphertext = dbContextLocal.Ciphertexts.FirstOrDefault(c => c.UsernameConnectURL.Trim() == newCiphertext.UsernameConnectURL.Trim());
                if (existingCiphertext != null)
                {

                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Username หรือ URL ใหม่ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();

                    //MessageBox.Show("กรุณากรอก Username หรือ URL ใหม่ด้วยครับ", "Error");
                    return;

                }
                else
                {

                    //// เพิ่ม Object ล่าสุดลงใน List
                    BindingListCiphertext.Add(newCiphertext);
                    BindingListCiphertextServer.Add(newCiphertextServer);
                    SaveDataToListBinding();
                    //// บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
                    //// ฝั่ง Local  =============================
                    dbContextLocal.Ciphertexts.Add(newCiphertext);
                    dbContextLocal.SaveChanges();
                    //// ฝั่ง Server =============================
                    dbContextServer.CiphertextsServer.Add(newCiphertextServer);
                    dbContextServer.SaveChanges();

                    InitializeTextBoxPlaceholder();
                    InitializeMyPassTextBoxPlaceholder();

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาดในการสร้าง Item: {ex.Message}", "Error");
            }

        }

        private void pictureBoxEyeWhite_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar == true)
            {
                myPassTextBoxPassword.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar == false)
            {
                myPassTextBoxPassword.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar = true;
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private string UnmaskUsername(string username)
        {
            if (username.Contains('@'))
            {
                string[] parts = username.Split('@');
                string maskedPart = MaskString(parts[0]);
                string domain = parts[1];
                return $"{maskedPart}@{MaskString(domain)}";
            }
            else
            {
                return MaskString(username);
            }
        }
        // ฟังก์ชันสำหรับการซ่อนตัวอักษร
        private string MaskString(string input)
        {
            if (input.Contains('@'))
            {
                string[] parts = input.Split('@');
                string usernamePart = parts[0];
                string domainPart = parts[1];
                string maskedUsername = usernamePart.Substring(0, 1) + new string('*', usernamePart.Length - 1);
                string maskedDomain = domainPart.Substring(0, 1) + new string('*', domainPart.Length - 1);
                return maskedUsername + "@" + maskedDomain;
            }
            else
            {
                return input.Substring(0, 1) + new string('*', input.Length - 1);
            }
        }


        private void DataGridViewInformationPasswordManager_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == DataGridViewInformationPasswordManager.Columns["CiphertextUsername"].Index)
            {
                string originalUsername = e.Value.ToString();
                if (maskStatus.ContainsKey(e.RowIndex) && maskStatus[e.RowIndex])
                {
                    e.Value = originalUsername;
                }
                else
                {
                    string maskedUsername = MaskString(originalUsername);
                    e.Value = maskedUsername;
                }
            }

        }





    }
}



