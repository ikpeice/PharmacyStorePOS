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
    public partial class DashboardForm : Form
    {
        Form prev;
        string _username;
        public DashboardForm(Form _prev, string user)
        {
            InitializeComponent();
            prev = _prev;
            _username = user;
        }
         ~DashboardForm()
        {
            prev.Visible = true;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            prev.Visible = true;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.Text = "["+_username+"] -- Dashboard";
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
