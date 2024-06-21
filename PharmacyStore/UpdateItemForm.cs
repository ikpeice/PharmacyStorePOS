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
    public partial class UpdateItemForm : Form
    {
        List<string> itemData;
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        public UpdateItemForm(List<string> ItemData)
        {
            InitializeComponent();
            this.itemData = ItemData;
           
        }

        private void Update_button_Click(object sender, EventArgs e)
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

            productDB.UpdateItem(list);
            this.Close();
        }

        private void UpdateItemForm_Load(object sender, EventArgs e)
        {
            List<string> cat;
            List<string> comp;
            cat = productDB.GetColoumnItems("Category");
            comp = productDB.GetColoumnItems("Company");
            comboBox1.Items.Clear();
            foreach (string item in cat)
            {
                if (!comboBox1.Items.Contains(item))
                    comboBox1.Items.Add(item);
            }
            comboBox2.Items.Clear();
            foreach (string item in comp)
            {
                if (!comboBox2.Items.Contains(item))
                    comboBox2.Items.Add(item);
            }

            textBox1.Text = itemData[0];
            richTextBox1.Text = itemData[1];
            comboBox1.SelectedItem = itemData[2];
            numericUpDown1.Value = int.Parse(itemData[3]);
            textBox2.Text = itemData[4];
            textBox3.Text = itemData[5];
            comboBox2.SelectedItem = itemData[6];
            
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            
        }
    }
}
