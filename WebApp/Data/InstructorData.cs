using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class InstructorData
    {
        private readonly string _connectionString;
        public InstructorData(IConfiguration configuration)
        {
            try
            {
                _connectionString = configuration.GetConnectionString("MySqlConnection");
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error connecting to the database", ex);
            }
        }

        //Obtener todos los instructores
        public List<Instructor> GetAll()
        {
            var list = new List<Instructor>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();

            var cmd = new MySqlCommand("SELECT * FROM Instructores", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Instructor
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Correo = reader.GetString("Correo"),
                    Especialidad = reader.GetString("Especialidad")
                });
            }
            return list;
        }

        public Instructor GetById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Instructores WHERE Id = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Instructor
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Correo = reader.GetString("Correo"),
                    Especialidad = reader.GetString("Especialidad")
                };
            }
            return null;
        }

        public void Create(Instructor i)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO Instructores (Nombre, Correo, Especialidad) VALUES (@Nombre, @Correo, @Especialidad)", conn);
            cmd.Parameters.AddWithValue("@Nombre", i.Nombre);
            cmd.Parameters.AddWithValue("@Correo", i.Correo);
            cmd.Parameters.AddWithValue("@Especialidad", i.Especialidad);
            cmd.ExecuteNonQuery();
        }

        public void Update(Instructor i)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE Instructores SET Nombre = @Nombre, Correo = @Correo, Especialidad = @Especialidad WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", i.Id);
            cmd.Parameters.AddWithValue("@Nombre", i.Nombre);
            cmd.Parameters.AddWithValue("@Correo", i.Correo);
            cmd.Parameters.AddWithValue("@Especialidad", i.Especialidad);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM Instructores WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

    }
}