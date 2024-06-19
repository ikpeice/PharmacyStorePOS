
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PharmacyStore.Models
{
    internal class DBConnection
    {
        SqliteConnection conn;
        ~DBConnection() {
        
        }
        public DBConnection(SqliteConnection _conn) 
        {
            this.conn = _conn;
        }
        public void Connect()
        {
            try
            {
               /* this.conn = new SqliteConnection("Data Source=hello.db");*/
                this.conn.Open();
                MessageBox.Show("Connection successful");
                conn.Close();
            }
            catch(SqliteException ex) {
                MessageBox.Show(ex.ToString());
            
            }
        }

        public void CreateUserTable()
        {
            string sql = "CREATE TABLE UserTable(" +
                "id INTEGER PRIMARY KEY," +
                "username TEXT NOT NULL," +
                "password TEXT NOT NULL)";
            try
            {
                this.conn.Open();
                var command = new SqliteCommand(sql, conn);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("User Table Created");
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());

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

        public List<string> LoadItem(string itemCode)
        {
            string sql = "SELECT * From product WHERE Code = @Code";
            List<string> rowItem = new List<string>();
            try
            {
                this.conn.Open();
                SqliteCommand command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Code", itemCode);
                command.ExecuteNonQuery();
                var reader = command.ExecuteReader();
                
                if (reader.HasRows)
                {
                    int i = 1; // start from Item code
                    while (reader.Read())
                    {
                        string item = reader.GetString(i);
                        rowItem.Add(item);
                        i++;
                    }
                    reader.Close();
                    
                }
                conn.Close();
            }
            catch(SqliteException ex) {
                MessageBox.Show(ex.ToString());
            }
            return rowItem;
        } 
        
        public void LoadStock(DataGridView dataGridView, bool privilege)
        {
            string sql = "SELECT * FROM product";
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
                        if (privilege)
                        {
                            dataGridView.Rows.Add(new object[] {
                            reader.GetString(reader.GetOrdinal("Code")),
                            reader.GetString(reader.GetOrdinal("Description")),
                            reader.GetString(reader.GetOrdinal("Category")),
                            reader.GetString(reader.GetOrdinal("Quantity")),
                            reader.GetString(reader.GetOrdinal("Cost Price")),
                            reader.GetString(reader.GetOrdinal("Selling Price")),
                            reader.GetString(reader.GetOrdinal("Company"))
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
                            reader.GetString(reader.GetOrdinal("Selling Price")),
                            reader.GetString(reader.GetOrdinal("Company"))
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

        }
    }
}
