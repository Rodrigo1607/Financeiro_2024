using Domain.Interfaces.ICategoria;
using Domain.Interfaces.ISistemaFinanceiro;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio.Generics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.RepositorioSistemaFinanceiro
{
    public class RepositorioSistemaFinanceiro : RepositoryGenerics<SistemaFinanceiro>, InterfaceSistemaFinanceiro
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;
        public RepositorioSistemaFinanceiro()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }
        public async Task<IList<SistemaFinanceiro>> ListarSistemasUsuario(string emailUsuario)
        {
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                return await
                    (from sf in banco.SistemaFinanceiro
                     join us in banco.UsuarioSistemaFinanceiro on sf.Id equals us.IdSistema
                     where us.EmailUsuario.Equals(emailUsuario)
                     select sf).AsNoTracking().ToListAsync();
            }
        }
    }
}
