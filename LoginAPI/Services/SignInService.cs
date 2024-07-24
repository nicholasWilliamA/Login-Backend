using LoginAPI.Model;
using LoginEntities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAPI.Services
{
    public class SignInService
    {
        private readonly IConfiguration _configuration;
        private readonly LoginDBContext db;

        public SignInService(IConfiguration configuration, LoginDBContext dBContext)
        {
            _configuration = configuration;
            this.db = dBContext;
        }
        /// <summary>
        /// Generate a JWT token for a given parameter.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        private string GenerateToken(SignInModel param)
        {
            var jwtSettings = new JwtSettings();
            _configuration.Bind(nameof(JwtSettings), jwtSettings);
            var secretKey = jwtSettings.AccessTokenSecret;
            var expirationInMinutes = Convert.ToInt32(jwtSettings.AccessTokenExpirationMinutes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);
            // Create the token descriptor which contains the token configuration.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, param.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Sign in function that check if the username and password from parameter match the database.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<UserModel?> SignInAccount(SignInModel param)
        {
            //Get data from database that match the username from parameter.
            var data = await db.Users.FirstOrDefaultAsync(Q => Q.Username == param.Username);
            if(data == null)
            {
                return null;
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(param.Password, data.Password);
            if (!isPasswordValid)
            {
                return null;
            }

            var token = GenerateToken(param);
            return new UserModel
            {
                Name = data.Name,
                Token = token
            };
        }
    }
}
