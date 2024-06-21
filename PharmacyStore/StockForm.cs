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

namespace PharmacyStore
{
    public partial class StockForm : Form
    {
        DBConnection productDB = new DBConnection(new SqliteConnection("Data Source=ProductDB.db"));
        string _username;
        bool _privilege;
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

        }

        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            MessageBox.Show("data changed");
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("data changed at "+e.RowIndex.ToString()+","+e.ColumnIndex.ToString());
        }

        private void addNewItem_button_Click(object sender, EventArgs e)
        {
            Form form = new AddItemForm(dataGridView1,label4);
            form.ShowDialog();
        }

        private void Delete_button_Click(object sender, EventArgs e)
        {
            for(int j = 0; j < dataGridView1.SelectedRows.Count; j++)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[j];
                
                productDB.DeleteItem(row.Index);
                int count = productDB.LoadStock(dataGridView1, _privilege);
                label4.Text = "Total Item Count : " + count.ToString();
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
            }
        }
    }
}
