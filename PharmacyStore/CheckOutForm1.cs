using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyStore
{
    public partial class CheckOutForm : Form
    {
        string total_received;
        string change;
        public CheckOutForm(string total_received, string change)
        {
            InitializeComponent();
            this.total_received = total_received;
            this.change = change;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckOutForm_Load(object sender, EventArgs e)
        {
            label7.Text = "N " + total_received;
            label8.Text = "N "+this.change;
        }
    }
}
