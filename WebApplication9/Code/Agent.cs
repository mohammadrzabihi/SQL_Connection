using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication9.Code
{
    public class Agent
    {
        private DatabaseHelper _dbHelper;

        public Agent()
        {
            _dbHelper = new DatabaseHelper();
        }

        // Define a struct to represent an agent
        public struct strcAgent
        {
            public int ID { get; set; } // identity
            public string Name { get; set; }
            public bool IsActive { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public int NoActor { get; set; }
        }

        // Method to select all agents
        public DataTable SelectAll()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT id, name, isActive, dateFrom, dateTo, NoActor FROM agent");
                return _dbHelper.ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SelectAll: {ex.Message}");
                throw;
            }
        }

        // Method to insert a new agent
        public int Insert(strcAgent agent)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO agent (name, isActive, dateFrom, dateTo, NoActor) VALUES (@Name, @IsActive, @DateFrom, @DateTo, @NoActor); SELECT SCOPE_IDENTITY();");
                command.Parameters.AddWithValue("@Name", agent.Name);
                command.Parameters.AddWithValue("@IsActive", agent.IsActive);
                command.Parameters.AddWithValue("@DateFrom", agent.DateFrom);
                command.Parameters.AddWithValue("@DateTo", agent.DateTo);
                command.Parameters.AddWithValue("@NoActor", agent.NoActor);
                return Convert.ToInt32(_dbHelper.ExecuteScalar(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Insert: {ex.Message}");
                throw;
            }
        }

        // Method to update an agent
        public int Update(strcAgent agent)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE agent SET name = @Name, isActive = @IsActive, dateFrom = @DateFrom, dateTo = @DateTo, NoActor = @NoActor WHERE ID = @ID");
                command.Parameters.AddWithValue("@Name", agent.Name);
                command.Parameters.AddWithValue("@IsActive", agent.IsActive);
                command.Parameters.AddWithValue("@DateFrom", agent.DateFrom);
                command.Parameters.AddWithValue("@DateTo", agent.DateTo);
                command.Parameters.AddWithValue("@NoActor", agent.NoActor);
                command.Parameters.AddWithValue("@ID", agent.ID);
                return _dbHelper.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                throw;
            }
        }

        // Method to delete an agent by ID
        public int Delete(int agentID)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM agent WHERE ID = @ID");
                command.Parameters.AddWithValue("@ID", agentID);
                return _dbHelper.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                throw;
            }
        }

        // Method to delete an agent by NoActor
        public int DeleteByNoActor(int noActor)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM agent WHERE NoActor = @NoActor");
                command.Parameters.AddWithValue("@NoActor", noActor);
                return _dbHelper.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteByNoActor: {ex.Message}");
                throw;
            }
        }
    }
}