using ControleDeContatos.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {

        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarTodos(int usuarioId);

        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Refresh(ContatoModel contato);

        bool Apagar(int id);

        
    }
}
