using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PharmacyStore.Models;

namespace PharmacyStore
{
    public partial class StockForm : Form
    {
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        string _username;
        bool _privilege;
        public StockForm(string user, bool adminPrivilege)
        {
            InitializeComponent();
            _username = user;
            _privilege = adminPrivilege;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            if (_privilege)
            {
                dataGridView1.Enabled = true;
                dataGridView2.Enabled = false;
                productDB.LoadStock(dataGridView1,_privilege);
                
            }


            else
            {
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = true;
                productDB.LoadStock(dataGridView2,_privilege);

            }
                
        }

        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            MessageBox.Show("data changed");
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("data changed at "+e.RowIndex.ToString()+","+e.ColumnIndex.ToString());
            
        }
    }
}
