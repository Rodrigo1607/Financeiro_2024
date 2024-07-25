using Domain.Interfaces.ICategoria;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio.Generics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositorio.RepositorioCategoria
{
    public class RepositorioCategoria : RepositoryGenerics<Categoria>, InterfaceCategoria
    {
        private readonly DbContextOptions<ContextBase> _OptionsBuilder;

        public RepositorioCategoria()
        {
            _OptionsBuilder = new DbContextOptions<ContextBase>();
        }

        //busca todas as categorias (Categoria) associadas a sistemas financeiros (SistemaFinanceiro)
        //que um usuário específico (identificado por email) pode acessar e que são atuais (SistemaAtual).
        public async Task<IList<Categoria>>ListarCategoriaUsuario(string emailUsuario)
        {            
            using (var banco = new ContextBase(_OptionsBuilder))
            {
                // Consulta LINQ para obter categorias associadas ao usuário e sistema atual
                return await
                    (from sf in banco.SistemaFinanceiro
                     join c in banco.Categoria on sf.Id equals c.IdSistema
                     join us in banco.UsuarioSistemaFinanceiro on sf.Id equals us.IdSistema
                     where us.EmailUsuario.Equals(emailUsuario) && us.SistemaAtual
                     select c).AsNoTracking().ToListAsync();
            }
        }
    }
}
