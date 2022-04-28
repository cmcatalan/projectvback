namespace ProjectVBack.Application.Dtos
{
    public record RegisterRequest(string UserName, string FirstName, string LastName, string Email, string Password);
}