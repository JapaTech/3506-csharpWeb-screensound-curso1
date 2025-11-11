using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.BD
{
    public class DAL <T> where T : class
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

        public IEnumerable<T> ListarPorOrdem(Func<T, object> func)
        {
            return context.Set<T>().OrderBy(func).ToList();
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
    
        public IEnumerable<T> BuscarPor(Func<T, bool> condicao)
        {
           return context.Set<T>().Where(condicao);
        }

        public T? BuscarObjetoExato(Func<T, bool> condicao)
        {
            return context.Set<T>().AsNoTracking().FirstOrDefault(condicao);
        }
    }
}
