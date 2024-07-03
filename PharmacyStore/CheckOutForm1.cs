using Microsoft.Data.Sqlite;
using Microsoft.Identity.Client;
using PharmacyStore.Models;
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

namespace PharmacyStore
{
    public partial class CheckOutForm : Form
    {
        DBConnection productDB = new DBConnection();
        private string total_received;
        private string change;
        private string cashierName="";
        Label invoice_label;
        DataGridView _dataGridView;
        public CheckOutForm(DataGridView dataGridView,string total_received, string change, Label _invoice_label, string _cashierName)
        {
            InitializeComponent();
            this.total_received = total_received;
            this.change = change;
            _dataGridView = dataGridView;
            invoice_label = _invoice_label;
            this.cashierName = _cashierName;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CheckOutForm_Load(object sender, EventArgs e)
        {
            label7.Text = "N " + total_received;
            label8.Text = "N "+this.change;
            label10.Text = invoice_label.Text;
        }

        private void save_pictureBox_Click(object sender, EventArgs e)
        {
            SqlDateTime sqlDateTime = new SqlDateTime(DateTime.Now);

            string dateTime = sqlDateTime.ToSqlString().Value;
            bool state = false;
            string date = dateTime.Substring(0, dateTime.IndexOf(' '));
            string time = dateTime.Substring(dateTime.IndexOf(" ") + 1);




            int rowCount = _dataGridView.Rows.Count;
            //double profit = 0.00;
            foreach (DataGridViewRow row in _dataGridView.Rows)
            {
                List<string> soldItem = new List<string>();
                double cost = double.Parse(productDB.GetItemCell(row.Cells[1].Value.ToString(), "CostPrice"));
                double soldPrice = double.Parse(row.Cells[3].Value.ToString()); 
                int qty = int.Parse(row.Cells[2].Value.ToString());
                double profit = (soldPrice - cost) * qty;

                soldItem.Add(row.Cells[0].Value.ToString()); // item code
                soldItem.Add(row.Cells[1].Value.ToString()); // item description
                soldItem.Add(cashierName);
                soldItem.Add(invoice_label.Text);
                soldItem.Add(row.Cells[2].Value.ToString()); // Qty
                soldItem.Add(row.Cells[3].Value.ToString()); // Amount
                soldItem.Add(profit.ToString());
                soldItem.Add((soldPrice*qty).ToString()); // total
                soldItem.Add(date);
                soldItem.Add(time);

                state = productDB.InserSoldItem(soldItem);
                if (state)
                {
                    int _qty = int.Parse(productDB.GetItemCell(row.Cells[1].Value.ToString(), "Quantity"));
                    _qty -= qty;
                    productDB.UpdateQuantity(row.Cells[1].Value.ToString(), _qty.ToString());
                }

            }
            if (state)
            {
                invoice_label.Text = (Int32.Parse(invoice_label.Text) + 1).ToString();
                productDB.UpdateInvoice(invoice_label.Text);
                _dataGridView.Rows.Clear();
                this.Close();
            }

        }
    }
}
