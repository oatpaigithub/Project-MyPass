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
    public partial class AddURLForm : Form
    {
        public string URLinput { get; private set; }
        public AddURLForm()
        {
            InitializeComponent();
        }


        static bool IsUrlValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (textBoxURL.Text == "") 
            {
                MessageBox.Show("กรุณากรอก URL ด้วยครับ");
            }
            if (IsUrlValid(textBoxURL.Text))
            {
                MessageBox.Show("URL ของคุณถูกต้อง");
                URLinput = textBoxURL.Text;
                this.Close();
            }
            else 
            {
                MessageBox.Show("URL ของคุณไม่ถูกต้องถูกต้อง");
                return;
            }

        }
        private void AddURLForm_FormClosing(object sender, EventArgs e) 
        {
            return;
        }

    }
}
