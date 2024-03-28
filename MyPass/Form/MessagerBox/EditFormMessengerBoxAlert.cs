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
    public partial class EditFormMessengerBoxAlert : Form
    {
        public bool DialogResultEditForm;
        public bool CancelState;

        private bool isDragging = false;
        private Point lastCursor;
        private Point lastForm;
        public EditFormMessengerBoxAlert()
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResultEditForm = false;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
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
