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

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int qty = Int32.Parse(dataGridView.Rows[rowIndex].Cells[e.ColumnIndex].Value.ToString());
            float amount = float.Parse(dataGridView.Rows[rowIndex].Cells[3].Value.ToString());
            if (qty > 0) {
                dataGridView.Rows[rowIndex].Cells[5].Value = (float)qty * amount;
            }

        }
    }
}
