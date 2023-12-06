using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);
        UsuarioModel BuscarPorEmailELogin(string email, string login);

        List<UsuarioModel> BuscarTodos();
        UsuarioModel ListarPorId(int id); 

        UsuarioModel Adicionar(UsuarioModel contato);
        UsuarioModel Refresh(UsuarioModel contato);
        UsuarioModel ChangePassword(AlterarSenhaModel alterarSenhaModel);
        bool Apagar(int id);
        
    }
}
