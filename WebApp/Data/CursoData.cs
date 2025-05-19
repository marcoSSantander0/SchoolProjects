using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class CursoData
    {
        private readonly string _connectionString;

        public CursoData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public List<Curso> GetAll()
        {
            var list = new List<Curso>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Cursos", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Curso
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                });
            }
            return list;
        }

        public Curso GetById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Cursos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Curso
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                };
            }
            return null;
        }

        public void Create(Curso c)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO Cursos (Nombre) VALUES (@Nombre)", conn);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.ExecuteNonQuery();
        }

        public void Update(Curso c)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE Cursos SET Nombre = @Nombre WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", c.Id);
            cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM Cursos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
