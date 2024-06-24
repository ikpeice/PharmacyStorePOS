using Microsoft.Data.Sqlite;
using PharmacyStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace PharmacyStore
{
    public partial class OrderSearchForm : Form
    {
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        Helper _helper = new Helper();
        string _username;
        bool _privilege;
        List<string> descriptions;
        DataGridView _dataGridView_Order;
        public OrderSearchForm(DataGridView dataGridView_)
        {
            InitializeComponent();
            _dataGridView_Order = dataGridView_;
        }

        private void OrderSearchForm_Load(object sender, EventArgs e)
        {
            List<string> cat;
            List<string> comp;
            cat = productDB.GetColoumnItems("Category");
            comp = productDB.GetColoumnItems("Company");
            descriptions = productDB.GetColoumnItems("Description");
            comboBox1.Items.Clear();
            comboBox1.Items.Add("None");
            foreach (string item in cat)
            {
                if (!comboBox1.Items.Contains(item))
                    comboBox1.Items.Add(item);
            }
            comboBox2.Items.Clear();
            comboBox2.Items.Add("None");
            foreach (string item in comp)
            {
                if (!comboBox2.Items.Contains(item))
                    comboBox2.Items.Add(item);
            }
            numericUpDown1.Value = 1;
            int count = productDB.LoadStock(dataGridView, _privilege);
        }

        private void Add_button_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int rowIndex;
            if (dataGridView.SelectedRows.Count == 1 && numericUpDown1.Value > 0)
            {
                rowIndex = dataGridView.SelectedRows[0].Index;
                DataGridViewRow row = dataGridView.SelectedRows[0];
                int index = _dataGridView_Order.Rows.Add();
                _dataGridView_Order.Rows[index].Cells[0].Value = row.Cells[0].Value.ToString();
                _dataGridView_Order.Rows[index].Cells[1].Value = row.Cells[1].Value.ToString();
                _dataGridView_Order.Rows[index].Cells[2].Value = numericUpDown1.Value.ToString();
                _dataGridView_Order.Rows[index].Cells[3].Value = row.Cells[4].Value.ToString();
                _dataGridView_Order.Rows[index].Cells[4].Value = "0";
                _dataGridView_Order.Rows[index].Cells[5].Value = numericUpDown1.Value * Int32.Parse(row.Cells[4].Value.ToString());


/*                for (int i = 0; i < row.Cells.Count; i++)
                {
                    list.Add(row.Cells[i].Value.ToString());
                    _dataGridView_Order.Rows[index].Cells[i].Value = row.Cells[i].Value.ToString();// (dataGridView.SelectedRows[0].Cells[i]);
                }*/

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
