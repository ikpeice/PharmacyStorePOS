﻿using PharmacyStore.Models;
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
using System.Runtime.CompilerServices;
using System.Threading;


namespace PharmacyStore
{
    public partial class LogInForm : Form
    {
        DBConnection staffDB = new DBConnection();
        string _username;
        private bool _adminPrivilege = false;
        public LogInForm()
        {
            InitializeComponent();
            
            
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            _username = textBox1.Text;
            string password = textBox2.Text;
            switch (checkBox1.Checked)
            {
                case true:
                    if (staffDB.CheckPassword(_username, password, true))
                    {
                        _adminPrivilege = true;
                        this.Visible = false;
                        Form dashboard = new DashboardForm(this, _username, _adminPrivilege);
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Try again");
                    }
                    break;

                case false:
                    if (staffDB.CheckPassword(_username, password))
                    {
                        //MessageBox.Show("Successful Login");
                        this.Visible = false;
                        Form dashboard = new DashboardForm(this, _username, _adminPrivilege);
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! Try again");
                    }
                    break;
            }

        }

        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                _adminPrivilege = false;
            }
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            staffDB.CreateTables();
        }
    }
}
