using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmacyStore.Models;

namespace PharmacyStore
{
    public partial class DashboardForm : Form
    {
        Form prev;
        string _username;
        private bool _adminPrivilege;// = &false;
        public DashboardForm(Form _prev, string user,bool adminPrivilege)
        {
            InitializeComponent();
            prev = _prev;
            _username = user;
            _adminPrivilege = adminPrivilege;
        }
         ~DashboardForm()
        {
            prev.Visible = true;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            prev.Visible = true;
            _adminPrivilege = false;
            SqlDateTime sqlDateTime = new SqlDateTime(DateTime.Now);

            string dateTime = sqlDateTime.ToSqlString().Value;
            string date = dateTime.Substring(0, dateTime.IndexOf(' '));
            string time = dateTime.Substring(dateTime.IndexOf(" ") + 1);
            DBConnection DB = new DBConnection();
            DB.Attendance(_username, "OUT", date, time);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            this.Text = "["+_username+"] -- Dashboard";
            
            if (_adminPrivilege)
            {
                addItem_button.Enabled = true;
                report_button.Enabled = true;
                employe_button.Enabled = true;
            }
            else
            {
                addItem_button.Enabled = false;
                report_button.Enabled = false;
                employe_button.Enabled = false;
            }
            SqlDateTime sqlDateTime = new SqlDateTime(DateTime.Now);

            string dateTime = sqlDateTime.ToSqlString().Value;
            string date = dateTime.Substring(0, dateTime.IndexOf(' '));
            string time = dateTime.Substring(dateTime.IndexOf(" ") + 1);
            DBConnection DB = new DBConnection();
            DB.Attendance(_username, "IN", date, time);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void onLog_out(object sender, EventArgs e)
        {
            this.Close();
            prev.Visible = true;
        }

        private void OnTimer_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            label2.Text = dt.GetDateTimeFormats()[11];
        }

        private void stock_button_Click(object sender, EventArgs e)
        {
            Form form = new StockForm(_username, _adminPrivilege);
            form.ShowDialog();
        }

        private void order_button_Click(object sender, EventArgs e)
        {
            Form form = new OrderForm(_username);
            form.Show();
        }

        private void sales_button_Click(object sender, EventArgs e)
        {
            Form form = new TodaySalse(_adminPrivilege);
            form.ShowDialog();
        }
    }
}
