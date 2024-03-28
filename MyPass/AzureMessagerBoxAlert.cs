﻿using System;
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
    public partial class AzureMessagerBoxAlert : Form
    {
        public bool DialogResultForm;
        public AzureMessagerBoxAlert(string HeadText, string ContentText)
        {
            InitializeComponent();

            labelHeadText.Text = HeadText;
            labelContentText.Text = ContentText;
        }

        private void buttonStyleMypassOK_Click(object sender, EventArgs e)
        {
            DialogResultForm = true;
            this.Close();

        }

        private void pictureBoxButtonClose_Click(object sender, EventArgs e)
        {
            DialogResultForm = false;
            this.Close();
        }
    }
}
