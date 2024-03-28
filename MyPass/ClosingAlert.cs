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
    public partial class ClosingAlert : Form
    {
        public bool DialogResultEditForm;
        public ClosingAlert()
        {
            InitializeComponent();
        }

        private void buttonStyleMypassOK_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = true;
            this.Close();

        }

        private void buttonStyleMypassCancel_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = false;
            this.Close();

        }

        private void pictureBoxClosingButton_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = false;
            this.Close();
        }
    }
}
