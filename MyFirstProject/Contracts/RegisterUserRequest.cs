using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Contracts
{
    public record RegisterUserRequest([Required] string UserName, [Required] string Email, [Required] string Password);
}
