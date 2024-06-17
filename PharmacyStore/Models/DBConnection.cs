
using Microsoft.Data.Sqlite;

using System.Windows.Forms;

namespace PharmacyStore.Models
{
    internal class DBConnection
    {
        SqliteConnection conn;
        ~DBConnection() {
        
        }
        public DBConnection(SqliteConnection conn) 
        {
            this.conn = conn;
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

        public bool CheckPassword(string username, string password)
        {
            string sql = "SELECT * " +
                    "FROM UserTable " +
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
                        var pass = reader.GetString(2);
                        if (pass == password)
                        {
                            result = true;
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

    }
}
