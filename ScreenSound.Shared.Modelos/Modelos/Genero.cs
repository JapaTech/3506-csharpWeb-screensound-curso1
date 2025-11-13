using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Shared.Modelos.Modelos
{
    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Musica> Musicas { get; set; }

        public Genero(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }

        override public string ToString()
        {
            return $"Genero: {Nome} - {Descricao}";
        }
    }
}
