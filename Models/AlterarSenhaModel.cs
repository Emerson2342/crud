using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a senha atual do usuário:")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuário:")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirme a nova senha usuário:")]
        [Compare("NewPassword", ErrorMessage = "As senhas não são idênticas!!")]
        public string ConfirmNewPassword { get; set; }

    }
}
