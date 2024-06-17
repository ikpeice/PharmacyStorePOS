using PharmacyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace PharmacyStore
{
    public partial class LogInForm : Form
    {
        DBConnection DB = new DBConnection(new SqliteConnection("Data Source=hello.db"));
        string _username;
        public LogInForm()
        {
            InitializeComponent();
            //DB.CreateUserTable();
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            //DB.Connect();
            //DB.RegisterUser("ikpe", "onlyice");
            _username = textBox1.Text;
            string password = textBox2.Text;
            if(DB.CheckPassword(_username, password))
            {
                MessageBox.Show("Successful Login");
                this.Visible = false;
                Form dashboard = new DashboardForm(this,_username);
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Login Failed");
            }

        }
    }
}
