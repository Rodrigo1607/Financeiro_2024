using Domain.Interfaces.Generics;
using Infra.Configuracao;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace Infra.Repositorio.Generics
{
    public class RepositoryGenerics<T> : InterfaceGeneric <T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositoryGenerics()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task Add(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            { 
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                 data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().FindAsync(Id);
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                return await data.Set<T>().ToListAsync();
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_OptionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }

        
        
       bool disposed = false;

       SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

       
        // Implementação do método Dispose
        public void Dispose()
        {
            // Chama Dispose(true) e suprime a finalização
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Método Dispose protegido que faz o trabalho real de liberar recursos
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }    



    }
}
