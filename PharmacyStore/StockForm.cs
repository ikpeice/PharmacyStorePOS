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
                groupBox1.Enabled = true;
                int count = productDB.LoadStock(dataGridView1,_privilege);
                label4.Text = "Total Item Count = "+count.ToString();
            }


            else
            {
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = true;
                groupBox1.Enabled = false;
                int count = productDB.LoadStock(dataGridView2,_privilege);
                label4.Text = "Total Item Count = " + count.ToString();
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

        private void addNewItem_button_Click(object sender, EventArgs e)
        {
            Form form = new AddItemForm(dataGridView2,label4);
            form.ShowDialog();
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            for(int j = 0; j < dataGridView1.SelectedRows.Count; j++)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[j];
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    MessageBox.Show(row.Cells[i].Value.ToString());
                }
            }

            
        }
    }
}
