using BlazorEcommerce.Shared.Enum;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlazorEcommerce.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public string GetUserEmail() => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);

        public  string GetUserEmailById(int userId) => _context.Users.Find(userId).Email;

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found.";
            }
            else if(user.Status == AccountStatus.Lock){
                response.Success = false;
                response.Message = "Your account is Lock !";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password.";
            }
            else
            {
                response.Data = CreateToken(user);
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            if (await UserExists(user.Email))
            {
                return new ServiceResponse<int>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<int> { Data = user.Id, Message = "Registration successful!" };
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(user => user.Email.ToLower()
                 .Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }else if (!VerifyPasswordHash(oldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Wrong password."
                };
            }
            
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
        public async Task<ServiceResponse<List<UserResponse>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Address)
                .ToListAsync();
            var response = new ServiceResponse<List<UserResponse>>();
            var userResponse = new List<UserResponse>();
            users.ForEach(u => userResponse.Add(new UserResponse
            {
                Id = u.Id,
                Address = u.Address,
                Email = u.Email,
                DateCreated = u.DateCreated,
                Role = u.Role,
                Status = u.Status
            }));
            response.Data = userResponse;
            return response;
        }
        public async Task<ServiceResponse<bool>> LockOrUnclock(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "User not fount." };
            }else
            {
                var response = new ServiceResponse<bool>();
                if(user.Status == AccountStatus.Unlock)
                {
                    user.Status = AccountStatus.Lock;
                    await _context.SaveChangesAsync();
                    response.Data = true;
                    response.Success = true;
                    response.Message = $"Account {user.Email} has been locked";
                }
                else if (user.Status == AccountStatus.Lock)
                {
                    user.Status = AccountStatus.Unlock;
                    await _context.SaveChangesAsync();
                    response.Data = true;
                    response.Success = true;
                    response.Message = $"Account {user.Email} has been unlocked";
                }
                return response;
            }
        }
        public async Task<ServiceResponse<bool>> UserRole(int userId, UserResponse userResponse)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool> { Data = false, Success = false, Message = "User not fount." };
            }
            else
            {
                user.Role = userResponse.Role;
                await _context.SaveChangesAsync();
                return new ServiceResponse<bool> { Data = true, Success = true, Message = "successful decentralization" };
            }
        }
    }
}
