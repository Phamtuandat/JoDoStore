using Backend.Models.Identity;
using Backend.Repositories.Identity;
using Backend.Services.Communication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Services.Identity
{
    public class IdentitySevice : IIdentitySevice
    {
        private readonly IIdentityRepository _identityRespository;
        private readonly IConfiguration _config;

        public IdentitySevice(IIdentityRepository identityRespository, IConfiguration config)
        {
            _identityRespository = identityRespository;
            _config = config;
        }

        public async Task<AuthenticateBaseRes> RegisterAsync(RegisterRequest model)
        {
            var UserList = await _identityRespository.GetListAsync();
            var user = UserList.FirstOrDefault(u => u.Email == model.Email);

            if (user != null) return new AuthenticateBaseRes("Email is already existed");

            CreatePasswordHash(out byte[] PasswordHash, out byte[] PasswordSalt, model.Password);
            var newUser = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = model.PhoneNumber,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
            };
            CreateToken(newUser, out string accessToken, out string refreshToken);
            await _identityRespository.SaveUserAsync(newUser);
            return new AuthenticateBaseRes(newUser, accessToken, refreshToken);
        }
        public async Task<AuthenticateBaseRes> AuthenticateAsync(AuthenticateRequest model)
        {
            var userList = await _identityRespository.GetListAsync();
            var user = userList.FirstOrDefault(u => u.Email == model.Email);
            if (user == null) return new AuthenticateBaseRes("Email is not existed!");
            bool isValid = VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt);
            if (!isValid) return new AuthenticateBaseRes("Password is invalid, please try again");
            CreateToken(user, out string accessToken, out string refreshToken);
            return new AuthenticateBaseRes(user, accessToken, refreshToken);
        }
        public async Task<AuthenticateBaseRes> GetUserByToken(string email)
        {
            var userList = await _identityRespository.GetListAsync();
            var user = userList.FirstOrDefault(u => u.Email == email);
            if (user == null) return new AuthenticateBaseRes("token is invalid");
            CreateToken(user, out string accessToken, out string refreshToken);
            return new AuthenticateBaseRes(user, accessToken, refreshToken);
        }


        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }



        private void CreateToken(User user, out string accessToken,out string refreshToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:AccessKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.LastName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var jwtAccessToken = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );
            var jwtRefreshToken = new JwtSecurityToken
                (
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: credentials
                );
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtAccessToken);
            refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtRefreshToken);
        }
       
        private void CreatePasswordHash(out byte[] passwordHash, out byte[] passwordSalt, string password)
        {
            using (HMACSHA512 hmac = new())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[]? passwordHash, byte[]? passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }

        }

        
    }
}
