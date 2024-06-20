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
    public partial class AddItemForm : Form
    {
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        DataGridView _dataGridView;
        Label _label;
        public AddItemForm(DataGridView dataGridView, Label label)
        {
            InitializeComponent();
            _dataGridView = dataGridView;
            _label = label;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void addItem_button_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            list.Add(textBox1.Text);
            list.Add(richTextBox1.Text);
            list.Add(comboBox1.Items[comboBox1.SelectedIndex].ToString());
            list.Add(numericUpDown1.Value.ToString());
            list.Add(textBox2.Text);
            list.Add(textBox3.Text);
            list.Add(comboBox2.Items[comboBox2.SelectedIndex].ToString());
            list.Add(dateTimePicker1.Value.ToShortDateString());

            productDB.AddItem(list);
            int count = productDB.LoadStock(_dataGridView, true);
            _label.Text = count.ToString();
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            List<string> cat = new List<string>();
            List<string> comp = new List<string>();
            cat = productDB.GetColoumnItems("Category");
            comp = productDB.GetColoumnItems("Company");
            string prev = "";
            foreach (string item in cat)
            {
                if(prev != item)comboBox1.Items.Add(item);
                prev = item;
            }
            prev = "";
            foreach (string item in comp)
            {
                if (prev != item) comboBox2.Items.Add(item);
                prev = item;
            }
        }

        private void Done_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
