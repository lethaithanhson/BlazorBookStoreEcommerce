namespace BlazorEcommerce.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string oldPassword, string newPassword);
        int GetUserId();
        string GetUserEmail();
        string GetUserEmailById(int userId);
        Task<User> GetUserByEmail(string email);
        Task<ServiceResponse<List<UserResponse>>> GetUsers();
    }
}
