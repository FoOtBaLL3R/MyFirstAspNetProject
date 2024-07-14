using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Contracts
{
    public record LoginUserRequest([Required] string Email, [Required] string Password);
}
