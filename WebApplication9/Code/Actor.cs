using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication9.Code
{
    public class Actor
    {
        private DatabaseHelper _dbHelper;

        public Actor()
        {
            _dbHelper = new DatabaseHelper();
        }

        // Define a struct to represent an actor
        public struct strcActor
        {
            public int ID { get; set; } // Identity
            public string Name { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public int IdGenderType { get; set; } // New field
            public int IdMilitaryServiceType { get; set; } // New field
            public string MilitaryServiceStatus { get; internal set; }
            public string Gender { get; internal set; }
        }

        // Method to select all actors
        public DataTable SelectAll()
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT ID, name, username, password, IdGenderType, IdMilitaryServiceType FROM actor");
                return _dbHelper.ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SelectAll: {ex.Message}");
                throw;
            }
        }

        // Method to insert a new actor
        public int Insert(strcActor actor)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO actor (name, username, password, IdGenderType, IdMilitaryServiceType) VALUES (@Name, @Username, @Password, @IdGenderType, @IdMilitaryServiceType); SELECT SCOPE_IDENTITY();");
                command.Parameters.AddWithValue("@Name", actor.Name);
                command.Parameters.AddWithValue("@Username", actor.Username);
                command.Parameters.AddWithValue("@Password", actor.Password);
                command.Parameters.AddWithValue("@IdGenderType", actor.IdGenderType); // New parameter
                command.Parameters.AddWithValue("@IdMilitaryServiceType", actor.IdMilitaryServiceType); // New parameter
                return Convert.ToInt32(_dbHelper.ExecuteScalar(command));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Insert: {ex.Message}");
                throw;
            }
        }

        // Method to update an actor
        public int Update(strcActor actor)
        {
            try
            {
                SqlCommand command = new SqlCommand("UPDATE actor SET name = @Name, password = @Password, IdGenderType = @IdGenderType, IdMilitaryServiceType = @IdMilitaryServiceType WHERE ID = @ID");
                command.Parameters.AddWithValue("@Name", actor.Name);
                command.Parameters.AddWithValue("@Password", actor.Password);
                command.Parameters.AddWithValue("@IdGenderType", actor.IdGenderType); // New parameter
                command.Parameters.AddWithValue("@IdMilitaryServiceType", actor.IdMilitaryServiceType); // New parameter
                command.Parameters.AddWithValue("@ID", actor.ID);
                return _dbHelper.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
                throw;
            }
        }

        // Method to delete an actor
        public int Delete(string username)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM actor WHERE username = @Username");
                command.Parameters.AddWithValue("@Username", username);
                return _dbHelper.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                throw;
            }
        }

        // Method to delete an actor
        public int Delete(int actorID)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM actor WHERE ID = @ID");
                command.Parameters.AddWithValue("@ID", actorID);
                return _dbHelper.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
                throw;
            }
        }
        public DataTable GetLookupData(string lookupType)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT id, name FROM lookup WHERE id IN (1, 2)"); // Adjust the query as needed
                return _dbHelper.ExecuteQuery(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetLookupData: {ex.Message}");
                throw;
            }
        }

    }
}
