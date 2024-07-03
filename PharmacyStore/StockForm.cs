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
using System.Reflection.Emit;
using Microsoft.IdentityModel.Tokens;

namespace PharmacyStore
{
    public partial class StockForm : Form
    {
        DBConnection productDB = new DBConnection();
        Helper _helper = new Helper();   
        string _username;
        bool _privilege;
        List<string> descriptions;
        public StockForm(string user, bool adminPrivilege)
        {
            InitializeComponent();
            _username = user;
            _privilege = adminPrivilege;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            
            if (_privilege)
            {
                dataGridView1.Enabled = true;
                dataGridView2.Enabled = false;
                groupBox1.Enabled = true;
                tabControl1.SelectTab(1);
                int count = productDB.LoadStock(dataGridView1,_privilege);
                label4.Text = "Total Item Count = "+count.ToString();
            }
            else
            {
                dataGridView1.Enabled = false;
                dataGridView2.Enabled = true;
                groupBox1.Enabled = false;
                int count = productDB.LoadStock(dataGridView2,_privilege);
                label4.Text = "Total Item Count = " + count.ToString();
            }

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

        }

        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void addNewItem_button_Click(object sender, EventArgs e)
        {
            Form form = new AddItemForm(dataGridView1,label4);
            form.ShowDialog();
            int count = productDB.LoadStock(dataGridView1, true);
            label4.Text = "Total Item Count : " + count.ToString();
            descriptions = productDB.GetColoumnItems("Description");
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            for(int j = 0; j < dataGridView1.SelectedRows.Count; j++)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[j];

                productDB.DeleteItem(row.Cells[0].Value.ToString());
                int count = productDB.LoadStock(dataGridView1, _privilege);
                label4.Text = "Total Item Count : " + count.ToString();
                descriptions = productDB.GetColoumnItems("Description");
            }

            
        }

        private void Update_button_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int rowIndex;
            if (dataGridView1.SelectedRows.Count == 1)
            {
                rowIndex = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow row = dataGridView1.SelectedRows [0];
                for(int i=0;i< row.Cells.Count;i++)
                {
                    list.Add(row.Cells [i].Value.ToString());
                }
                Form updateForm = new UpdateItemForm(list);
                updateForm.ShowDialog();
                int count = productDB.LoadStock(dataGridView1, true);
                label4.Text = "Total Item Count : " + count.ToString();
                descriptions = productDB.GetColoumnItems("Description");
            }
        }
/*        private bool search(string text, string in_text)
        {
            string up_in = in_text.ToUpper();
            string low_in = in_text.ToLower();
            int len = in_text.Length;
            int len2 = text.Length;
            int x = 0;
            for (int i = 0; i < len; i++)
            {
                if (up_in[i] == text[x] || low_in[x] == text[x])
                {
                    if (x + 1 == len2) return true;
                    x += 1;
                }
                else { x = 0; }
            }
            return false;
        }*/

        private void Search_textBox_TextChanged(object sender, EventArgs e)
        {
            string text = Search_textBox.Text;
            if(text.Length >= 2)
            {
                Search_listBox.Items.Clear();
                Search_listBox.Visible = true;
                foreach(string desp in descriptions)
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
            Search_listBox.Visible=false;
            int count = _privilege ? productDB.Search(item, category, company, dataGridView1, _privilege) : productDB.Search(item, category, company, dataGridView2, _privilege);
            dataGridView1.Refresh();
            label4.Text = "Total Item Count : " + count.ToString();
            descriptions = productDB.GetColoumnItems("Description");
            
        }

        private void Search_listBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Search_pictureBox_Click(object sender, EventArgs e)
        {
            string item = Search_listBox.Text;
            string category = comboBox1.Text;
            string company = comboBox2.Text;
            if (!item.IsNullOrEmpty() || category != string.Empty || company !=string.Empty)
            {
                //MessageBox.Show(text);
                Search_listBox.Visible = false;
                int count = _privilege ? productDB.Search(item, category, company, dataGridView1, _privilege) : productDB.Search(item, category, company, dataGridView2, _privilege);
                dataGridView1.Refresh();
                dataGridView2.Refresh();
                label4.Text = "Total Item Count : " + count.ToString();
                descriptions = productDB.GetColoumnItems("Description");
            }
            
        }
    }
}
