using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    internal class DAL <T> where T : class
    {
        protected readonly ScreenSoundContext context;

        public DAL(ScreenSoundContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<T> Listar()
        {
            return context.Set<T>().ToList();
        }
        public void Adicionar(T objeto)
        {          
            context.Set<T>().Add(objeto);
            context.SaveChanges();
        
        }
        public void Atualizar(T objeto)
        {
            context.Set<T>().Update(objeto);
            /*int retorno =*/
            context.SaveChanges();
        }
        public  void Deletar(T objeto)
        {
            context.Set<T>().Remove(objeto);
            context.SaveChanges();
        }
        public void Deletar(int id)
        {
            var objeto = context.Set<T>().Find(id);

            if (objeto != null)
            {
                context.Set<T>().Remove(objeto);
            }
            context.SaveChanges();
        }

        public List<T> BuscarPorStringAbrangente(string termoDeBusca, Func<T, string> condicao)
        {
            List<T> lista = context.Set<T>()
                .Where(e => condicao(e).ToLower()
                .Contains(termoDeBusca)).ToList();
            return lista;
        }

        public T? BuscarObjetoExato(Func<T, bool> condicao)
        {
            return context.Set<T>().FirstOrDefault(condicao);
        }
    }
}
