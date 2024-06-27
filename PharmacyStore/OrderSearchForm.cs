using Microsoft.Data.Sqlite;
using Microsoft.IdentityModel.Tokens;
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
        TextBox _totalTextBox;
        public OrderSearchForm(DataGridView dataGridView_, TextBox _textBox)
        {
            InitializeComponent();
            _dataGridView_Order = dataGridView_;
            _totalTextBox = _textBox;
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
            label5.Text = "Item Count : " + count.ToString();
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
                _dataGridView_Order.Rows[index].Cells[3].Value = (float)Int32.Parse(row.Cells[4].Value.ToString());
                _dataGridView_Order.Rows[index].Cells[4].Value = "0.00";
                _dataGridView_Order.Rows[index].Cells[5].Value = (float)numericUpDown1.Value * (float)Int32.Parse(row.Cells[4].Value.ToString());
                float x = float.Parse(_totalTextBox.Text);
                float y = (float)numericUpDown1.Value * (float)Int32.Parse(row.Cells[4].Value.ToString());
                _totalTextBox.Text = (x + y).ToString();

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

        private void Search_textBox_TextChanged(object sender, EventArgs e)
        {
            string text = Search_textBox.Text;
            if (text.Length >= 2)
            {
                Search_listBox.Items.Clear();
                Search_listBox.Visible = true;
                foreach (string desp in descriptions)
                {
                    if (_helper.search(text, desp))// desp.Contains(text))
                    {
                        Search_listBox.Items.Add(desp);
                    }
                }
            }
        }

        private void Search_listBox_Click(object sender, EventArgs e)
        {
            string item = Search_listBox.Text;
            string category = comboBox1.Text;
            string company = comboBox2.Text;
            //MessageBox.Show(text);
            Search_listBox.Visible = false;
            int count = productDB.Search(item, category, company, dataGridView, false);// : productDB.Search(item, category, company, dataGridView2, _privilege);
            dataGridView.Refresh();
            label5.Text = "Item Count : " + count.ToString();
            descriptions = productDB.GetColoumnItems("Description");
        }

        private void Search_pictureBox_Click(object sender, EventArgs e)
        {
            string item = Search_listBox.Text;
            string category = comboBox1.Text;
            string company = comboBox2.Text;
            if (!item.IsNullOrEmpty() || category != string.Empty || company != string.Empty)
            {
                Search_listBox.Visible = false;
                int count = productDB.Search(item, category, company, dataGridView, false);// : productDB.Search(item, category, company, dataGridView2, _privilege);
                dataGridView.Refresh();
                label5.Text = "Item Count : " + count.ToString();
                descriptions = productDB.GetColoumnItems("Description");
            }

        }
    }
}
