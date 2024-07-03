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
    public partial class TodaySalse : Form
    {
        DBConnection saleDB = new DBConnection();
        bool admin = false;
        public TodaySalse(bool admin)
        {
            InitializeComponent();
            this.admin = admin; 
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TodaySalse_Load(object sender, EventArgs e)
        {
            if (admin)
            {
                label3.Visible = true;
                label6.Visible = true;
            }
            else
            {
                label3.Visible = false;
                label6.Visible = false;
                dataGridView.Columns[6].Visible = false;
            }
            SqlDateTime sqlDateTime = new SqlDateTime(DateTime.Now);

            string dateTime = sqlDateTime.ToSqlString().Value;
            string date = dateTime.Substring(0, dateTime.IndexOf(' '));
            string time = dateTime.Substring(dateTime.IndexOf(" ") + 1);
            saleDB.GetTodaySoldItems(dataGridView, date);
            double total = 0.00, profit = 0.00;

            for (int i = 0; i < dataGridView.Rows.Count; i++) 
            {
                total += double.Parse(dataGridView.Rows[i].Cells[7].Value.ToString());
                profit += double.Parse(dataGridView.Rows[i].Cells[6].Value.ToString());
            }
            label4.Text = total.ToString();
            label6.Text = profit.ToString();
            label5.Text = dataGridView.Rows.Count.ToString();
        }
    }
}
