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
    public partial class AzureMessagerBoxConnectingCheck : Form
    {
        public AzureMessagerBoxConnectingCheck(string HeadText, string ContentText)
        {
            InitializeComponent();

            labelHeadText.Text = HeadText;
            labelContentText.Text = ContentText;
            ClosingFormThis();
        }


        private async void ClosingFormThis() 
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            this.Close();
        }
        private void AzureMessagerBoxConnectingCheck_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
