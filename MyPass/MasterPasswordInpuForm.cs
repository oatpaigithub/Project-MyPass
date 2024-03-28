using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public partial class MasterPasswordInpuForm : Form
    {
        public string MasterPassword { get; private set; }
        public MasterPasswordInpuForm()
        {
            InitializeComponent();
        }
        //public MasterPasswordInpuForm(string masterpassword)
        //{
        //    InitializeComponent();
        //}

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (textBoxMasterPassword.Text == "")
            {
                MessageBox.Show("กรุณากรอก MasterPassword ด้วยครับ", "แจ้งเตือน");
            }
            else 
            {
                MasterPassword = textBoxMasterPassword.Text;
                this.Close();
            }

        }

        private void MasterPasswordInpuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            return;
        }
    }
}
