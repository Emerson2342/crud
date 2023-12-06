using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ControleDeContatos.Controllers
{
    [PaginaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao) 
        { 
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index23()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);
            return View(contatos);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult DeleteConfirm(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }
        public IActionResult Delete(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if(apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                    
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o contato, tente novamente!";
                }
                return RedirectToAction("Index23");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ops, não conseguimos apagar o contato, mais detalhes do erro" +
                    $"{erro.Message}!";
                return RedirectToAction("Index23");
            }
        }

        [HttpPost]
        public IActionResult Create(ContatoModel contato) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Adicionar(contato);

                    TempData["MensagemSucesso"] = "Contato condastrado com sucesso!";
                    return RedirectToAction("Index23");
                }

                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ops, não conseguimos cadastrar seu contato, tente novamente." +
                    $"Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index23");
            }
        }
        [HttpPost]
        public IActionResult Edit(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                   contato = _contatoRepositorio.Refresh(contato);
                    TempData["MensagemSucesso"] = "Contato editado com sucesso!";
                    return RedirectToAction("Index23");
                }
                return View(contato);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = "Ops, não conseguimos editar seu contato, tente novamente." +
                    $"Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index23");
            }
        }
    }
}
