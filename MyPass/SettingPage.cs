using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public partial class SettingPage : Form
    {
        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        public string DevicenameChangeForm;
        public string DevicenameOld;

        public string ConnectionString_this;
        public string ContainerName_this;

        public string ConnectionString_Old;
        public string ContainerName_Old;


        public bool CheckAction = false;
        public bool CheckActionForConnectionstring = false;
        MainPage mainPage = new MainPage();


        public bool MouseClickTextBoxDeviceName = false;

        public bool MouseClickTextBoxConnectionString = false;
        public bool MouseClickTextBoxContainerName = false;

        public SettingPage(string DeviceName, string ConnectionString, string ContainerName)
        {
            InitializeComponent();
            DevicenameChangeForm = DeviceName;
            DevicenameOld = DeviceName;
            textBoxDeviceNameChange.Text = DevicenameChangeForm;
            myPassTextBoxDeviceName.Texts = DevicenameChangeForm;

            ConnectionString_Old = ConnectionString;
            ContainerName_Old = ContainerName;

            myPassTextBoxConnectionString.Texts = ConnectionString;
            myPassTextBoxContainerName.Texts = ContainerName;


        }

        private void ChangeDeviceName_Load(object sender, EventArgs e)
        {

        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (MouseClickTextBoxDeviceName == true) 
            {
                if (textBoxDeviceNameChange.Text == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก DeviceName ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    // MessageBox.Show("กรุณากรอก DeviceName ด้วยครับ");
                    return;

                }
                else
                {

                    // แสดง MessageBox และรับผลลัพธ์
                    DialogResult result = MessageBox.Show("คุณมั่นใจ DeviceName ของคุณแล้วใช่ไหม?", "ยืนยันการเปลี่ยนชื่อ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        DevicenameChangeForm = textBoxDeviceNameChange.Text;
                        CheckAction = true;
                        //mainPage.UploadMemoryStepSetup(DevicenameChangeForm);
                        //mainPage.DeleteStepSetup(DevicenameOld);
                        this.Close();

                    }
                    else
                    {
                        return;
                    }
                }
            }



        }

        private void buttonStyleMypassSubmit_Click(object sender, EventArgs e)
        {
            if (MouseClickTextBoxDeviceName == true) 
            {
                if (myPassTextBoxDeviceName.Texts == "")
                {
                    MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก DeviceName ด้วยครับ");
                    miniMessagerBoxTextBoxAlert.ShowDialog();
                    // MessageBox.Show("กรุณากรอก DeviceName ด้วยครับ");
                    return;

                }
                else
                {
                    EditDeviceNameFormMessengerBoxAlert editDeviceNameFormMessengerBoxAlert = new EditDeviceNameFormMessengerBoxAlert();
                    editDeviceNameFormMessengerBoxAlert.ShowDialog();
                    // แสดง MessageBox และรับผลลัพธ์

                    // DialogResult result = MessageBox.Show("คุณมั่นใจ Device Name ของคุณแล้วใช่ไหม?", "ยืนยันการเปลี่ยนชื่อ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (editDeviceNameFormMessengerBoxAlert.DialogResultEditForm == true)
                    {
                        DevicenameChangeForm = myPassTextBoxDeviceName.Texts;
                        CheckAction = true;
                        //mainPage.UploadMemoryStepSetup(DevicenameChangeForm);
                        //mainPage.DeleteStepSetup(DevicenameOld);
                        this.Close();

                    }
                    else
                    {
                        return;

                    }

                }



            }


        }

        private void pictureBoxClosing_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void toggleButtonMyPassLightMode_CheckedChanged(object sender, EventArgs e)
        {
            //if (toggleButtonMyPassLightMode.Checked)
            //{
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //    MessageBox.Show($"{documentsPath}");
            //    this.BackColor = Color.FromArgb(230, 230, 230);

            //}
            //else 
            //{
            //    this.BackColor = Color.FromArgb(26,26,26);
            
            //}

        }

        private void buttonStyleMypassChangCloudStorageSetup_Click(object sender, EventArgs e)
        {
            if (myPassTextBoxConnectionString.Texts == "") 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Connection String ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                return;
            }
            if (myPassTextBoxContainerName.Texts == "") 
            {
                MiniMessagerBoxTextBoxAlert miniMessagerBoxTextBoxAlert = new MiniMessagerBoxTextBoxAlert("Error", "กรุณากรอก Container Name ด้วยครับ");
                miniMessagerBoxTextBoxAlert.ShowDialog();
                return;
            }

            if (myPassTextBoxConnectionString.Texts != "" && myPassTextBoxContainerName.Texts != "") 
            {
                //MiniMessagerBoxTextBoxNormal miniMessagerBoxTextBoxNormal = new MiniMessagerBoxTextBoxNormal("แจ้งเตือน", "กรุณาตรวจสอบ ConnectionString และ ContainerName ให้มั่นใจอีกครั้ง");
                if (MouseClickTextBoxConnectionString == true || MouseClickTextBoxContainerName == true) 
                {
                    ConnectionString_this = myPassTextBoxConnectionString.Texts;
                    ContainerName_this = myPassTextBoxContainerName.Texts;

                    CheckActionForConnectionstring = true;
                    this.Close();
                }

            }


        }


        private void panelToptool_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastCursor = Cursor.Position;
                lastForm = this.Location;
            }

        }

        private void panelToptool_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int deltaX = Cursor.Position.X - lastCursor.X;
                int deltaY = Cursor.Position.Y - lastCursor.Y;
                this.Location = new Point(lastForm.X + deltaX, lastForm.Y + deltaY);
            }

        }

        private void panelToptool_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }

        }

        private void myPassTextBoxDeviceName_MouseClick(object sender, MouseEventArgs e)
        {
            MouseClickTextBoxDeviceName = true;
        }

        private void myPassTextBoxDeviceName_Click(object sender, EventArgs e)
        {
            MouseClickTextBoxDeviceName = true;
        }

        private void myPassTextBoxConnectionString_Click(object sender, EventArgs e)
        {
            MouseClickTextBoxConnectionString = true;
        }

        private void myPassTextBoxContainerName_Click(object sender, EventArgs e)
        {
            MouseClickTextBoxContainerName = true;
        }
    }
}
