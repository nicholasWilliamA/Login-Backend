using LoginAPI.Model;
using LoginEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace LoginAPI.Services
{
    public class SignUpService
    {
        private readonly LoginDBContext db;

        public SignUpService(LoginDBContext db)
        {
            this.db = db;
        }
        /// <summary>
        /// Create a new account for user and store it in the database.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<string> CreateNewAccount(SignUpModel param)
        {
            var checkExistingAccount = await db.Users.AnyAsync(Q => Q.Username == param.Username);
            if(checkExistingAccount)
            {
                return "Username has been taken!";
            }
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(param.Password, salt);
            var newAccount = new User
            {
                Name = param.Name,
                Username = param.Username,
                Password = hashedPassword
            };
            try
            {
                db.Users.Add(newAccount);
                await db.SaveChangesAsync();
                return "Successfully created new account!";
            }catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
