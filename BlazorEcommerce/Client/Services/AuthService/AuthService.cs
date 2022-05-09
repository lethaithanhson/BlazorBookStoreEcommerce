namespace BlazorEcommerce.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/change-password", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }
        public async Task<List<UserResponse>> GetUsers()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<UserResponse>>>("api/auth");
            return result.Data;
        }

        public async Task<ServiceResponse<bool>> LockOrUnlock(int userId)
        {
            var result = await _http.PutAsJsonAsync($"api/auth/{userId}/lockorunlock", userId);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
        public async Task<ServiceResponse<bool>> UserRole(int userId, UserResponse userResponse)
        {
            var result = await _http.PutAsJsonAsync($"api/auth/{userId}/userrole", userResponse);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }
    }
}
