﻿using Domain.Interfaces.ICategoria;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;

namespace Domain.Servicos
{
    public class CategoriaServico : ICategoriaServico
    {
        private readonly InterfaceCategoria _interfaceCategoria;
       
        public CategoriaServico(InterfaceCategoria interfaceCategoria)
        {
            _interfaceCategoria = interfaceCategoria;
        }

        public async Task AdicionarCategoria(Categoria categoria)
        {
            var valido = categoria.ValidaPropriedadeString(categoria.Nome, "Nome");
            if(valido)
            {
                await _interfaceCategoria.Add(categoria);
            }
        
        }

        public async Task AtualizarCategoria(Categoria categoria)
        {
            var valido = categoria.ValidaPropriedadeString(categoria.Nome, "Nome");
            if (valido)
            {
                await _interfaceCategoria.Update(categoria);
            }
        }
    }
}
