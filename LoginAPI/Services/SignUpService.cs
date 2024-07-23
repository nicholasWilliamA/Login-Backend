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

        public async Task<string> CreateNewAccount(SignUpModel param)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(param.Password, salt);
            var checkExistingAccount = await db.Users.Where(Q => Q.Username == param.Username)
                                                       .Select(Q => Q.Username).FirstOrDefaultAsync();
            if(checkExistingAccount != null)
            {
                return "Username has been taken!";
            }
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
