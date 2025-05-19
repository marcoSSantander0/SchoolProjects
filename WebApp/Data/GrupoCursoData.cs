using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class GrupoCursoData
    {
        private readonly string _connectionString;

        public GrupoCursoData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public List<GrupoCurso> GetAll()
        {
            var lista = new List<GrupoCurso>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM GrupoCurso", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new GrupoCurso
                {
                    Id = reader.GetInt32("Id"),
                    GrupoId = reader.GetInt32("GrupoId"),
                    CursoId = reader.GetInt32("CursoId"),
                    InstructorId = reader.GetInt32("InstructorId")
                });
            }
            return lista;
        }

        public GrupoCurso GetById(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM GrupoCurso WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new GrupoCurso
                {
                    Id = reader.GetInt32("Id"),
                    GrupoId = reader.GetInt32("GrupoId"),
                    CursoId = reader.GetInt32("CursoId"),
                    InstructorId = reader.GetInt32("InstructorId")
                };
            }
            return null;
        }

        public void Insert(GrupoCurso gc)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO GrupoCurso (GrupoId, CursoId, InstructorId) VALUES (@GrupoId, @CursoId, @InstructorId)", conn);
            cmd.Parameters.AddWithValue("@GrupoId", gc.GrupoId);
            cmd.Parameters.AddWithValue("@CursoId", gc.CursoId);
            cmd.Parameters.AddWithValue("@InstructorId", gc.InstructorId);
            cmd.ExecuteNonQuery();
        }

        public void Update(GrupoCurso gc)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("UPDATE GrupoCurso SET GrupoId = @GrupoId, CursoId = @CursoId, InstructorId = @InstructorId WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", gc.Id);
            cmd.Parameters.AddWithValue("@GrupoId", gc.GrupoId);
            cmd.Parameters.AddWithValue("@CursoId", gc.CursoId);
            cmd.Parameters.AddWithValue("@InstructorId", gc.InstructorId);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM GrupoCurso WHERE Id = @Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
