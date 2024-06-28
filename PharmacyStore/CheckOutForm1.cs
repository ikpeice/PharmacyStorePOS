using Microsoft.Data.Sqlite;
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

namespace PharmacyStore
{
    public partial class CheckOutForm : Form
    {
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        string total_received;
        string change;
        string invoice;
        DataGridView _dataGridView;
        public CheckOutForm(DataGridView dataGridView,string total_received, string change, string inv)
        {
            InitializeComponent();
            this.total_received = total_received;
            this.change = change;
            _dataGridView = dataGridView;
            invoice = inv;
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

        private void save_pictureBox_Click(object sender, EventArgs e)
        {
            int rowCount = _dataGridView.Rows.Count;
            foreach(DataGridViewRow row in _dataGridView.Rows)
            {
               List<string> item = new List<string>();
                
                foreach(DataGridViewCell cell in row.Cells)
                {
                    item.Add(cell.Value.ToString());
                }
                int cost = int.Parse(productDB.LoadItem(item[0])[5]);
                int qty = int.Parse(item[2]);
                // To do
                // calculate profit and store to db
            }
        }
    }
}
