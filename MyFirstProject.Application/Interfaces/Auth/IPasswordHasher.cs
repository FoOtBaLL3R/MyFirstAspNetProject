namespace MyFirstProject.Application.Interfaces.Auth
{
    public interface IPasswordHasher
    {
        string Generate(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}