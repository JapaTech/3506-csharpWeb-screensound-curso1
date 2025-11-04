using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSound.Modelos;
using Microsoft.Data.SqlClient;

namespace ScreenSound.BD
{
    internal class ArtistaDAL
    {
        public IEnumerable<Artista> Listar()
        {
            var lista = new List<Artista>();
            using var cn = new ScreenSoundContext().ObterConexao();
            cn.Open();

            string sql = "SELECT * FROM Artistas";
            SqlCommand cmd = new SqlCommand(sql, cn);
            using SqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                string nomeArtista = Convert.ToString(dataReader["Nome"]);
                string bioArtista = Convert.ToString(dataReader["Bio"]);
                int idArtista = Convert.ToInt32(dataReader["Id"]);
                Artista artista = new Artista(nomeArtista, bioArtista) { Id = idArtista };
                lista.Add(artista);
            }

            return lista;
        }

        public void Adicionar(Artista artista)
        {
            using var cn = new ScreenSoundContext().ObterConexao();
            cn.Open();
            string sql = "INSERT INTO Artistas (Nome, Bio, FotoPerfil) VALUES (@nome, @bio, @fotoPerfil)"; 
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nome", artista.Nome);
            cmd.Parameters.AddWithValue("@bio", artista.Bio);
            cmd.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);
            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
            
        }

        public void Deletar(int id)
        {
            using var cn = new ScreenSoundContext().ObterConexao();
            cn.Open();
            string sql = "DELETE FROM Artistas WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", id);
            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public void Deletar(Artista artista)
        {
            using var cn = new ScreenSoundContext().ObterConexao();
            cn.Open();
            string sql = "DELETE FROM Artistas WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@id", artista.Id);
            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public void Atualizar(Artista artista)
        {
            using var cn = new ScreenSoundContext().ObterConexao();
            cn.Open();
            string sql = "UPDATE Artistas SET Nome = @nome, Bio = @bio, FotoPerfil = @fotoPerfil WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nome", artista.Nome);
            cmd.Parameters.AddWithValue("@bio", artista.Bio);
            cmd.Parameters.AddWithValue("@fotoPerfil", artista.FotoPerfil);
            cmd.Parameters.AddWithValue("@id", artista.Id);
            int retorno = cmd.ExecuteNonQuery();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }
    }
}
