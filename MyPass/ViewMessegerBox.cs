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
    public partial class ViewMessegerBox : Form
    {

        private string Password_Setup;
        public bool DialogResultEditForm;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;

        bool StagePasswordBoxUseSystemPasswordChar = true;

        public ViewMessegerBox(string Password)
        {
            InitializeComponent();
            Password_Setup = Password;
            myPassTextBoxCurrentPasswordReadOnly.Texts = Password;

        }


        private void buttonStyleMypassOK_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = true;
            Clipboard.SetText(Password_Setup);
            MiniMessagerBoxCopySuccess miniMessagerBoxCopySuccess = new MiniMessagerBoxCopySuccess();
            miniMessagerBoxCopySuccess.ShowDialog();
            this.Close();

        }

        private void buttonStyleMypassCancel_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = false;
            this.Close();
        }

        private void pictureBoxClosing_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = false;
            this.Close();
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

        private void pictureBoxEyeWhite3_Click(object sender, EventArgs e)
        {
            if (StagePasswordBoxUseSystemPasswordChar == true)
            {
                myPassTextBoxCurrentPasswordReadOnly.PasswordChar = false;
                StagePasswordBoxUseSystemPasswordChar = false;
            }
            else if (StagePasswordBoxUseSystemPasswordChar == false)
            {
                myPassTextBoxCurrentPasswordReadOnly.PasswordChar = true;
                StagePasswordBoxUseSystemPasswordChar = true;
            }

        }
    }









}
