using Microsoft.Data.Sqlite;
using PharmacyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyStore
{
    public partial class OrderForm : Form
    {
        double cash = 0.00;
        double transfer = 0.00;
        double total = 0.00;
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        public OrderForm()
        {
            InitializeComponent();
        }

        private void Search_pictureBox_Click(object sender, EventArgs e)
        {
            Form form = new OrderSearchForm(dataGridView, Total_textBox);
            form.ShowDialog();
        }

        private void dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

        }

        private void dataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            Total_textBox.Text = (float.Parse(Total_textBox.Text) - float.Parse(e.Row.Cells[5].Value.ToString())).ToString();
        }

        private void Refresh_button_Click(object sender, EventArgs e)
        {
            int rowCount = dataGridView.Rows.Count;
            float total = 0;
            for(int i = 0; i < rowCount; i++)
            {
                float amount = float.Parse(dataGridView.Rows[i].Cells[3].Value.ToString());
                int qty = Int32.Parse(dataGridView.Rows[i].Cells[2].Value.ToString());
                dataGridView.Rows[i].Cells[5].Value = ((float)qty * amount);
                total += ((float)qty * amount);

            }
            Total_textBox.Text = total.ToString();
        }

        private void cash_textBox_TextChanged(object sender, EventArgs e)
        {

            total = double.Parse(Total_textBox.Text);

            if (cash_textBox.Text == string.Empty) cash = 0.00;
            else cash = double.Parse(cash_textBox.Text);

            if (transfer_textBox.Text == string.Empty) transfer = 0.00;
            else transfer = double.Parse(transfer_textBox.Text);

            if ((cash + transfer) >= total)
            {
                change_textBox.Text = ((cash + transfer) - total).ToString();
            }


        }

        private void transfer_textBox_TextChanged(object sender, EventArgs e)
        {


                total = double.Parse(Total_textBox.Text);

                if (cash_textBox.Text == string.Empty) cash = 0.00;
                else cash = double.Parse(cash_textBox.Text);

                if (transfer_textBox.Text == string.Empty) transfer = 0.00;
                else transfer = double.Parse(transfer_textBox.Text);

                if ((cash + transfer) >= total)
                {
                    change_textBox.Text = ((cash + transfer) - total).ToString();
                }

        }

        private void checkOut_button_Click(object sender, EventArgs e)
        {

            Form form = new CheckOutForm(dataGridView, Total_textBox.Text, change_textBox.Text, invoice_textBox.Text);
            form.ShowDialog();
        }

        private void cancelOrder_button_Click(object sender, EventArgs e)
        {
            if(dataGridView.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Do you want to cancle the order?",
                                "Cancel Order",
                                MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    dataGridView.Rows.Clear();
                    Total_textBox.Text = "0.00";
                    cash_textBox.Text = "0.00";
                    transfer_textBox.Text = "0.00";
                    change_textBox.Text = "0.00";
                }
            }

        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            string inv = productDB.ReadInvoice();
            if (inv != null)
            {
                invoice_textBox.Text = (int.Parse(inv)+1).ToString();
            }
        }
    }
}
