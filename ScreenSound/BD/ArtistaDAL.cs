using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSound.Modelos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace ScreenSound.BD
{
    internal class ArtistaDAL
    {
        private readonly ScreenSoundContext context;

        public ArtistaDAL(ScreenSoundContext _context)
        {
            this.context = _context;
        }


        /* Listar com SQL Command
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
        */

        public IEnumerable<Artista> Listar()
        {   
            return context.Artistas.ToList();
        }

        /* Adicionar com SQL Command
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
        */

        public void Adicionar (Artista artista)
        {            
            context.Artistas.Add(artista);
            int retorno = context.SaveChanges();
            Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        /* Deletar com SQL Command
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
        */

        public void Deletar (Artista artista)
        {
            context.Artistas.Remove(artista);
            /*int retorno =*/ context.SaveChanges();
            //Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public void Deletar(int id)
        {
            context.Artistas.Remove(context.Artistas.Find(id));
            /*int retorno =*/ context.SaveChanges();
            //Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        /*Atualizar com SQL Command
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
        */
        public void Atualizar(Artista artista)
        {
            context.Artistas.Update(artista);
            /*int retorno =*/
            context.SaveChanges();
            //Console.WriteLine($"Linhas afetadas: {retorno}");
        }

        public List<Artista> BuscarPorNome(string nome)
        {
            List<Artista> artistasEncontrados = context.Artistas.Where(a => a.Nome.Contains(nome)).ToList();
            return artistasEncontrados;
        }

        public Artista BuscarPorNomeExato(string nome)
        {
            return context.Artistas.FirstOrDefault(a => a.Nome.Equals(nome));
        }

    }
}
