using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class AlumnoData
    {
        private readonly string _connectionString;

        public AlumnoData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public List<Alumno> GetAll()
        {
            var lista = new List<Alumno>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Alumnos", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Alumno
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Correo = reader.GetString("Correo")
                });
            }
            return lista;
        }

        public Alumno GetById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Alumnos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Alumno
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Correo = reader.GetString("Correo")
                };
            }
            return null;
        }

        public void Create(Alumno a)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO Alumnos (Nombre, Correo) VALUES (@Nombre, @Correo)", conn);
            cmd.Parameters.AddWithValue("@Nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@Correo", a.Correo);
            cmd.ExecuteNonQuery();
        }

        public void Update(Alumno a)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE Alumnos SET Nombre = @Nombre, Correo = @Correo WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", a.Id);
            cmd.Parameters.AddWithValue("@Nombre", a.Nombre);
            cmd.Parameters.AddWithValue("@Correo", a.Correo);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM Alumnos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
