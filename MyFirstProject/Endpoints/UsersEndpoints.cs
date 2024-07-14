using MyFirstProject.Application.Services;
using MyFirstProject.Contracts;

namespace MyFirstProject.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);

            return app;
        }

        private static async Task<IResult> Register(RegisterUserRequest requestUser, UsersService usersService)
        {
            await usersService.Register(requestUser.UserName, requestUser.Email, requestUser.Password);

            return Results.Ok();
        }
        private static async Task<IResult> Login(LoginUserRequest userLoginRequest, UsersService usersService)
        {
            var token = await usersService.Login(userLoginRequest.Email, userLoginRequest.Password);
            
            return Results.Ok(token);
        }
    }
}
