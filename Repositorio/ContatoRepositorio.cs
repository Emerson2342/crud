
using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _context;
        public ContatoRepositorio(BancoContext bancoContent)
        {
            this._context = bancoContent;

        }
        public ContatoModel ListarPorId(int id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }
        public List<ContatoModel> BuscarTodos(int usuarioId)
        {
            return _context.Contatos.Where(x => x.UsuarioId == usuarioId).ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();

            return contato;
        }
        public ContatoModel Refresh(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);
            if (contatoDB == null)
            {
                throw new Exception("Houve um erro na atualização no contato");
            }

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;
            //contatoDB.DataCadastro = contato.DataCadastro;

            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
            
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null) throw new Exception("Houve um erro ao deletar o contato");
            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();
            return true;
        }
    }
}
