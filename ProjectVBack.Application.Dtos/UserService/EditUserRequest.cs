namespace ProjectVBack.Application.Dtos
{
    public record EditUserRequest(string FirstName, string LastName, string OldPassword, string NewPassword);
}