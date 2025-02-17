using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication9.Code
{
    public class DatabaseHelper
    {
        private string connectionString;

        // Constructor that accepts a connection string (for flexibility)
        public DatabaseHelper(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Parameterless constructor that reads the connection string from the configuration file
        public DatabaseHelper()
        {
            // Check if the connection string exists in the configuration
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                this.connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
            else
            {
                // Handle the case where the connection string is missing
                throw new ConfigurationErrorsException("Connection string 'ConnectionString' not found in Web.config.");
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Execute a command and return the number of rows affected
        public int ExecuteNonQuery(SqlCommand command)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    command.Connection = conn;
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteNonQuery: {ex.Message}");
                throw;
            }
        }

        // Execute a command and return the first column of the first row
        public object ExecuteScalar(SqlCommand command)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    command.Connection = conn;
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteScalar: {ex.Message}");
                throw;
            }
        }

        // Execute a command and return a DataTable
        public DataTable ExecuteQuery(SqlCommand command)
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    command.Connection = conn;
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ExecuteQuery: {ex.Message}");
                throw;
            }
        }
    }
}