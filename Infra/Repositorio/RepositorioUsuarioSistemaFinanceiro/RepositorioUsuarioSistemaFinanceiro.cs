using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio.Generics;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositorio.RepositorioUsuarioSistemaFinanceiro
{
    public class RepositorioUsuarioSistemaFinanceiro : RepositoryGenerics<UsuarioSistemaFinanceiro>, InterfaceUsuarioSistemaFinanceiro
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositorioUsuarioSistemaFinanceiro()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task<IList<UsuarioSistemaFinanceiro>> ListarUsuariosSistema(int IdSistema)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioSistemaFinanceiro
                    .Where(sf=>sf.IdSistema == IdSistema).AsNoTracking()
                    .ToListAsync();           
            }
        }

        public async Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    banco.UsuarioSistemaFinanceiro.AsNoTracking()
                    .FirstOrDefaultAsync(usf => usf.EmailUsuario.Equals(emailUsuario));
            }
        }

        public async Task RemoverUsuarios(List<UsuarioSistemaFinanceiro> usuarios)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                    banco.UsuarioSistemaFinanceiro
                    .RemoveRange(usuarios);

                await banco.SaveChangesAsync();
            }
        }
    }
}
