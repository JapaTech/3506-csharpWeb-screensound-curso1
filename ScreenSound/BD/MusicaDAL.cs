using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScreenSound.Modelos;

namespace ScreenSound.BD
{
    internal class MusicaDAL : DAL<Musica>
    {
        private readonly ScreenSoundContext _context;

        public MusicaDAL(ScreenSoundContext context) : base(context)
        {
            //_context = context;
        }

        //public override IEnumerable<Musica> Listar()
        //{
        //    return _context.Musicas.ToList();
        //}

        //public override void Adicionar(Musica musica)
        //{
        //    _context.Musicas.Add(musica);
        //    _context.SaveChanges();
        //}

        //public override void Deletar(int id)
        //{
        //    var musica = _context.Musicas.Find(id);
        //    if (musica != null)
        //    {
        //        _context.Musicas.Remove(musica);
        //        _context.SaveChanges();
        //    }
        //}

        //public override void Deletar(Musica musica)
        //{
        //    _context.Musicas.Remove(musica);    
        //}

        //public override void Atualizar(Musica musica)
        //{
        //    _context.Musicas.Update(musica);
        //    _context.SaveChanges();
        //}

        public List<Musica> BuscarPorNome(string nome)
        {
            return _context.Musicas
                .Where(m => m.Nome.Contains(nome))
                .ToList();
        }

        public Musica BuscarPorNomeExato(string nome)
        {
            var musica = _context.Musicas.FirstOrDefault(m => m.Nome.ToLower() == nome.ToLower());
            return musica;
        }
    }
}
