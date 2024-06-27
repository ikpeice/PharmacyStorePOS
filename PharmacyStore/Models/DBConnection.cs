﻿
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


        public void InserSoldItem(List<string> row)
        {
            string sql = "INSERT INTO soldItem (Code, Description, Cashier, Invoice, Amount, Profit, Date, Time) " +
            "VALUES (@Code, @Description, @Cashier, @Invoice, @Amount, @Profit, @Date, @Time)";
            try
            {
                this.conn.Open();
                var command = new SqliteCommand(sql, conn);
                command.Parameters.AddWithValue("Code", row[0]);
                command.Parameters.AddWithValue("Description", row[1]);
                command.Parameters.AddWithValue("Cashier", row[2]);
                command.Parameters.AddWithValue("Invoice", row[3]);
                command.Parameters.AddWithValue("Amount", row[4]);
                command.Parameters.AddWithValue("Profit", row[5]);
                command.Parameters.AddWithValue("Date", row[6]);
                command.Parameters.AddWithValue("Time", row[7]);
                command.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Inserted successfully");
            }
            catch (SqliteException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
