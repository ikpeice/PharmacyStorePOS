
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace PharmacyStore.Models
{
    internal class DBConnection
    {
        SqliteConnection conn = new SqliteConnection("Data Source=db/ProductDB.db");
        ~DBConnection() {
        
        }   
        readonly string DBsource = "Data Source=db/ProductDB.db";
        public DBConnection() 
        {
            if (!Directory.Exists("db/"))
            {
                Directory.CreateDirectory("db/");
            }
            
        }
        public string Source
        {
            get { return DBsource; }
        }

        public void CreateTables()
        {
            List<string> tables = new List<string>();
            tables.Add("CREATE TABLE IF NOT EXISTS invoice (id INTEGER NOT NULL, Invoice INTEGER NOT NULL, PRIMARY KEY(id))");

            tables.Add("CREATE TABLE IF NOT EXISTS product (id INTEGER NOT NULL, Code INTEGER NOT NULL, Description TEXT NOT NULL, Category TEXT NOT NULL, Quantity INTEGER NOT NULL, CostPrice INTEGER NOT NULL, SellingPrice INTEGER NOT NULL, Company TEXT, ExpirationDate TEXT, PRIMARY KEY( id AUTOINCREMENT) )");
            tables.Add("CREATE TABLE IF NOT EXISTS \"soldItems\" (\r\n\t\"id\"\tINTEGER NOT NULL,\r\n\t\"Code\"\tTEXT NOT NULL,\r\n\t\"Description\"\tTEXT NOT NULL,\r\n\t\"Cashier\"\tTEXT NOT NULL,\r\n\t\"Invoice\"\tINTEGER NOT NULL,\r\n\t\"Quantity\"\tINTEGER NOT NULL,\r\n\t\"Amount\"\tNUMERIC NOT NULL,\r\n\t\"Profit\"\tNUMERIC NOT NULL,\r\n\t\"Date\"\tTEXT NOT NULL,\r\n\t\"Time\"\tTEXT NOT NULL,\r\n\tPRIMARY KEY(\"id\")\r\n)");
            tables.Add("CREATE TABLE IF NOT EXISTS \"staff\" (\r\n\t\"id\"\tINTEGER NOT NULL,\r\n\t\"fullName\"\tTEXT NOT NULL,\r\n\t\"username\"\tTEXT NOT NULL,\r\n\t\"password\"\tTEXT NOT NULL,\r\n\t\"admin\"\tINTEGER NOT NULL,\r\n\t\"phone\"\tINTEGER,\r\n\t\"email\"\tTEXT,\r\n\tPRIMARY KEY(\"id\")\r\n)");
            foreach(string table in tables)
            {
                try
                {
                    this.conn.Open();
                    var command = new SqliteCommand(table, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                    
                }
                catch (SqliteException ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }

        }
        public void RegisterUser(string username, string password)
        {
            string sql = "INSERT INTO UserTable (userName, password) " +
                "VALUES (@username, @password)";
            try
            {
                this.conn.Open();
                var command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("username",username); 
                command.Parameters.AddWithValue("password", password);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Inserted successfully");
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public bool CheckPassword(string username, string password, bool admin = false)
        {
            string sql = "SELECT * " +
                    "FROM staff " +
                    "WHERE username = @username";
            bool result = false;
            try
            {
                this.conn.Open();
                var command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue ("@username",username);
                //command.Parameters.AddWithValue("@password",password);
                int res = command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
/*                        var id = reader.GetInt32(0);
                        var user = reader.GetString(1);*/
                        var pass = reader.GetString(3);
                        switch (admin)
                        {
                            case true:
                                string _admin = reader.GetString(4);
                                if (_admin == "1" && pass == password)
                                {
                                    result = true;
                                }
                                break;
                            case false:
                                if (pass == password)
                                {
                                    result = true;
                                }
                                break;
                        }
                    }
                }
                conn.Close();
            }
            catch (SqliteException ex) {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }

        public string GetItemCell(string description, string columnHeader = "")
        {
            string sql = "SELECT * From product WHERE Description = @Description";
            string item = "";
            try
            {
                this.conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Description", description);
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        item = reader.GetString(reader.GetOrdinal(columnHeader));

                    }
                    reader.Close();
                    
                }
                conn.Close();
            }
            catch(SqliteException ex) {
                MessageBox.Show(ex.ToString());
            }
            return item;
        } 
        
        public int LoadStock(DataGridView dataGridView, bool privilege)
        {
            int count = 0;
            string sql = "SELECT * FROM product";
            dataGridView.Rows.Clear();
            try
            {
                this.conn.Open();
                SqliteCommand command = new SqliteCommand(@sql, conn);
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                        if (privilege)
                        {
                            dataGridView.Rows.Add(new object[] {
                            reader.GetString(reader.GetOrdinal("Code")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetString(reader.GetOrdinal("Category")),
                            reader.GetString(reader.GetOrdinal("Quantity")),
                            reader.GetString(reader.GetOrdinal("CostPrice")),
                            reader.GetString(reader.GetOrdinal("SellingPrice")),
                            reader.GetString(reader.GetOrdinal("Company")),
                            reader.GetString(reader.GetOrdinal("ExpirationDate"))
                            });
                        }
                        else
                        {
                            dataGridView.Rows.Add(new object[] {
                            reader.GetString(reader.GetOrdinal("Code")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetString(reader.GetOrdinal("Category")),
                            reader.GetString(reader.GetOrdinal("Quantity")),
                            /*reader.GetString(reader.GetOrdinal("Cost Price")),*/
                            reader.GetString(reader.GetOrdinal("SellingPrice")),
                            reader.GetString(reader.GetOrdinal("Company")),
                            reader.GetString(reader.GetOrdinal("ExpirationDate"))
                        });
                        }

                    }
                }
                this.conn.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return count;
        }
    
        public void AddItem(List<string> row)
        {
            string sql = "INSERT INTO product (Code, Description, Category, Quantity, CostPrice, SellingPrice, Company, ExpirationDate) " +
                        //"VALUES ("+ row[0]+", "+ row[1]+", "+ row[2]+", "+ row[3]+", "+ row[4]+", "+ row[5]+", "+ row[6]+", "+ row[7]+")";
                        "VALUES (@Code, @Description, @Category, @Quantity, @CostPrice, @SellingPrice, @Company, @ExpirationDate)";
            try
            {
                this.conn.Open();
                var command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Code", row[0]);
                command.Parameters.AddWithValue("Description", row[1]);
                command.Parameters.AddWithValue("Category", row[2]);
                command.Parameters.AddWithValue("Quantity", row[3]);
                command.Parameters.AddWithValue("CostPrice", row[4]);
                command.Parameters.AddWithValue("SellingPrice", row[5]);
                command.Parameters.AddWithValue("Company", row[6]);
                command.Parameters.AddWithValue("ExpirationDate", row[7]);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Inserted successfully");
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public List<string> GetColoumnItems(string coloumnHeader)
        {
            List<string> items = new List<string>();
            string sql = "SELECT "+coloumnHeader+" FROM product";
            try
            {
                this.conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                //command.Parameters.AddWithValue("coln", coloumnHeader);
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        items.Add(reader.GetString(0));
                    }
                }
                this.conn.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return items;
        }

        public void DeleteItem(string code)
        {
            string sql = "DELETE FROM product WHERE Code = @Code";
            try
            {
                conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Code", code);
                command.ExecuteNonQuery();
                MessageBox.Show("Deleted successfully");
                conn.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void UpdateItem(List<string> data)
        {
            string sql = "UPDATE product " +
                "SET Description = @Description, " +
                "Category = @Category, " +
                "Quantity = @Quantity, " +
                "CostPrice = @CostPrice, " +
                "SellingPrice = @SellingPrice, " +
                "Company = @Company, " +
                "ExpirationDate = @ExpirationDate " +
                "WHERE Code = @Code";
            try
            {
                conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);

                command.Parameters.AddWithValue("Code", data[0]);
                command.Parameters.AddWithValue("Description", data[1]);
                command.Parameters.AddWithValue("Category", data[2]);
                command.Parameters.AddWithValue("Quantity", data[3]);
                command.Parameters.AddWithValue("CostPrice", data[4]);
                command.Parameters.AddWithValue("SellingPrice", data[5]);
                command.Parameters.AddWithValue("Company", data[6]);
                command.Parameters.AddWithValue("ExpirationDate", data[7]);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Updated Successfully","From DB",
                    MessageBoxButtons.OKCancel
                    );
            }
            catch(SqliteException ex) {
                DialogResult result = MessageBox.Show(ex.ToString(), "From DB",MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    MessageBox.Show("You just Canceled me");
                }
            }
        }

        public int Search(string item, string category, string company, DataGridView dataGridView, bool privilege)
        {
            int count = 0;
            string sql = "SELECT * FROM product WHERE Description like @Description OR " +
                "Category like @Category OR Company like @Company";
            dataGridView.Rows.Clear();
            try
            {
                this.conn.Open();
                SqliteCommand command = new SqliteCommand(@sql, conn);
                command.Parameters.AddWithValue("Description", item);
                command.Parameters.AddWithValue("Category", category);
                command.Parameters.AddWithValue("Company", company);
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count++;
                        if (privilege)
                        {
                            dataGridView.Rows.Add(new object[] {
                            reader.GetString(reader.GetOrdinal("Code")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetString(reader.GetOrdinal("Category")),
                            reader.GetString(reader.GetOrdinal("Quantity")),
                            reader.GetString(reader.GetOrdinal("CostPrice")),
                            reader.GetString(reader.GetOrdinal("SellingPrice")),
                            reader.GetString(reader.GetOrdinal("Company")),
                            reader.GetString(reader.GetOrdinal("ExpirationDate"))
                            });
                        }
                        else
                        {
                            dataGridView.Rows.Add(new object[] {
                            reader.GetString(reader.GetOrdinal("Code")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetString(reader.GetOrdinal("Category")),
                            reader.GetString(reader.GetOrdinal("Quantity")),
                            /*reader.GetString(reader.GetOrdinal("Cost Price")),*/
                            reader.GetString(reader.GetOrdinal("SellingPrice")),
                            reader.GetString(reader.GetOrdinal("Company")),
                            reader.GetString(reader.GetOrdinal("ExpirationDate"))
                        });
                        }

                    }
                }
                this.conn.Close();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return count;
        }


        public bool InserSoldItem(List<string> row)
        {
            bool state = false;
            string sql = "INSERT INTO soldItems (Code, Description, Cashier, Invoice, Quantity, Amount, Profit, Date, Time) " +
            "VALUES (@Code, @Description, @Cashier, @Invoice, @Quantity, @Amount, @Profit, @Date, @Time)";
            try
            {
                this.conn.Open();
                var command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Code", row[0]);
                command.Parameters.AddWithValue("Description", row[1]);
                command.Parameters.AddWithValue("Cashier", row[2]);
                command.Parameters.AddWithValue("Invoice", row[3]);
                command.Parameters.AddWithValue("Quantity", row[4]);
                command.Parameters.AddWithValue("Amount", row[5]);
                command.Parameters.AddWithValue("Profit", row[6]);
                command.Parameters.AddWithValue("Date", row[7]);
                command.Parameters.AddWithValue("Time", row[8]);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Inserted successfully");
                state = true;
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
                state = false;
            }
            return state;
        }
        
        public string ReadInvoice()
        {
            string inv = "";
            string sql = "SELECT invoice FROM invoice WHERE id = 1";
            try
            {
                conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                command.ExecuteNonQuery();
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        inv = reader.GetString(reader.GetOrdinal("Invoice"));
                    }
                }
                conn.Close();
            }
            catch(SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return inv;
        }

        public void UpdateInvoice(string inv)
        {
            string sql = "UPDATE invoice SET Invoice = @Invoice WHERE id = 1";
            try
            {
                conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Invoice", inv);
                command.ExecuteNonQuery();
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void UpdateQuantity(string description, string Quantity)
        {
            string sql = "UPDATE product SET Quantity = @Quantity WHERE Description = @Description";
            try
            {
                conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Quantity", Quantity);
                command.Parameters.AddWithValue("Description", description);
                command.ExecuteNonQuery();
            }
            catch(SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }
    }
}
