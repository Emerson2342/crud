using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email) 
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            

            if(_sessao.BuscarSessaoUsuario() != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
           
        }

        [HttpPost]

        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                   
                    if (usuario !=null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha do usuário é inválida, tente novamente.";
                    }
                }
                return View("Index");
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Usuário e senha inválidos. Por favor, tente novamente!"+
                         $" Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }


        [HttpPost]        
        public IActionResult LinkRedPassword(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email,
                        redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string newPassorwd = usuario.GegenateNewPassword();                        
                        string message = $"Sua nova senha é: {newPassorwd}";

                        bool emailSend = _email.Send(usuario.Email, "Sistema de Contatos - Nova Senha", message);

                        if (emailSend)
                        {
                            _usuarioRepositorio.Refresh(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para seu Email cadastrado uma nova senha. ";
                        }
                        else
                        {
                            TempData["MensagemErro"] = $"Não conseguimos enviar o email. Por favor, tente novamente.";
                            
                        }
                        return RedirectToAction("Index", "Login");

                    }
                    TempData["MensagemErro"] = $"Não conseguimos redefinir sua Senha. " +
                        $"Por favor, verifique os dados informados!";
                    return View("RedefinirSenha");
                }
                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos redefinir sua Senha, tente novamente." +
                    $"Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
