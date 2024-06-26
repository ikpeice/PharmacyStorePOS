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
    public partial class OrderForm : Form
    {
        public OrderForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            double cash = 0.00;
            double transfer = 0.00;

            float total = float.Parse(Total_textBox.Text);

            if (cash_textBox.Text == string.Empty) cash = 0.00;
            else cash = float.Parse(cash_textBox.Text);

            if (transfer_textBox.Text == string.Empty) transfer = 0.00;
            else transfer = float.Parse(transfer_textBox.Text);

            if ((cash + transfer) >= total)
            {
                change_textBox.Text = ((cash + transfer) - total).ToString();
            }


        }

        private void transfer_textBox_TextChanged(object sender, EventArgs e)
        {
            double cash = 0.00;
            double transfer = 0.00;

                float total = float.Parse(Total_textBox.Text);

                if (cash_textBox.Text == string.Empty) cash = 0.00;
                else cash = float.Parse(cash_textBox.Text);

                if (transfer_textBox.Text == string.Empty) transfer = 0.00;
                else transfer = float.Parse(transfer_textBox.Text);

                if ((cash + transfer) >= total)
                {
                    change_textBox.Text = ((cash + transfer) - total).ToString();
                }

        }
    }
}
