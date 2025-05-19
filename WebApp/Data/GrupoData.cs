using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class GrupoData
    {
        private readonly string _connectionString;

        public GrupoData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public List<Grupo> GetAll()
        {
            var lista = new List<Grupo>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Grupos", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Grupo
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Turno = reader.IsDBNull(2) ? "" : reader.GetString("Turno")
                });
            }
            return lista;
        }

        public Grupo GetById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM Grupos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Grupo
                {
                    Id = reader.GetInt32("Id"),
                    Nombre = reader.GetString("Nombre"),
                    Turno = reader.IsDBNull(2) ? "" : reader.GetString("Turno")
                };
            }
            return null;
        }

        public void Insert(Grupo g)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO Grupos (Nombre, Turno) VALUES (@Nombre, @Turno)", conn);
            cmd.Parameters.AddWithValue("@Nombre", g.Nombre);
            cmd.Parameters.AddWithValue("@Turno", g.Turno);
            cmd.ExecuteNonQuery();
        }

        public void Update(Grupo g)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE Grupos SET Nombre = @Nombre, Turno = @Turno WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", g.Id);
            cmd.Parameters.AddWithValue("@Nombre", g.Nombre);
            cmd.Parameters.AddWithValue("@Turno", g.Turno);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM Grupos WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
