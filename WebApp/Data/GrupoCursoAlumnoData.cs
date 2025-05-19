using MySql.Data.MySqlClient;
using System.Collections.Generic;
using WebApp.Models;
using Microsoft.Extensions.Configuration;

namespace WebApp.Data
{
    public class GrupoCursoAlumnoData
    {
        private readonly string _connectionString;

        public GrupoCursoAlumnoData(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("MySqlConnection");
        }

        public List<GrupoCursoAlumno> GetAll()
        {
            var lista = new List<GrupoCursoAlumno>();
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM GrupoCursoAlumno", conn);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new GrupoCursoAlumno
                {
                    GrupoCursoId = reader.GetInt32("GrupoCursoId"),
                    AlumnoId = reader.GetInt32("AlumnoId")
                });
            }
            return lista;
        }

        public void Insert(GrupoCursoAlumno gca)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("INSERT INTO GrupoCursoAlumno (GrupoCursoId, AlumnoId) VALUES (@GrupoCursoId, @AlumnoId)", conn);
            cmd.Parameters.AddWithValue("@GrupoCursoId", gca.GrupoCursoId);
            cmd.Parameters.AddWithValue("@AlumnoId", gca.AlumnoId);
            cmd.ExecuteNonQuery();
        }

        public void Delete(int grupoCursoId, int alumnoId)
        {
            using var conn = new MySqlConnection(_connectionString);
            conn.Open();
            var cmd = new MySqlCommand("DELETE FROM GrupoCursoAlumno WHERE GrupoCursoId = @GrupoCursoId AND AlumnoId = @AlumnoId", conn);
            cmd.Parameters.AddWithValue("@GrupoCursoId", grupoCursoId);
            cmd.Parameters.AddWithValue("@AlumnoId", alumnoId);
            cmd.ExecuteNonQuery();
        }
    }
}
