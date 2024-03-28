using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestFunctionSQL
{
    public partial class EditFormMessengerBoxAlertCopyPart : Form
    {
        public bool DialogResultEditForm;
        private string SetCurrentPassword;

        public EditFormMessengerBoxAlertCopyPart(string CurrentPassword)
        {

            InitializeComponent();
            SetCurrentPassword = CurrentPassword;

        }
        

        private void buttonStyleMypassCopyCurrentPassword_Click(object sender, EventArgs e)
        {
            // เล่นเสียงแจ้งเตือน
            SystemSounds.Exclamation.Play();
            Clipboard.SetText(SetCurrentPassword);
            MiniMessagerBoxCopySuccess miniMessagerBoxCopySuccess = new MiniMessagerBoxCopySuccess();
            miniMessagerBoxCopySuccess.Show();

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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = false;
            this.Close();
        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
