using Shopping.App.DAL;
using Shopping.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.App.Service
{
    public class UserService
    {
        private readonly AppDbContext _dbContext;

        public bool IsLogIn
            => Preferences.Get("UserId", -1) > 0;

        public string Account
            => Preferences.Get("Account", null);

        public int UserId
            => Preferences.Get("UserId", -1);


        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }


        public User Login(string account, string password)
        {
            var user = _dbContext.Users.SingleOrDefault(u => u.Account == account);
            if (user is null || user.Password != password)
                return null;

            StorageUser(user);

            return user;
        }


        public User SignIn(string account, string password) 
        {
            bool exists = _dbContext.Users.Any(u => u.Account == account);
            if (exists)
                return null;

            var user = new User()
            {
                Account = account,
                Password = password,
            };

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            StorageUser(user);
            
            return user;    
        }

        private void StorageUser(User user)
        {
            Preferences.Set("UserId", user.ID);
            Preferences.Set("Account", user.Account);
        }

        public void LogOut()
        {
            Preferences.Remove("UserId");
            Preferences.Remove("Account");
        }


        public void ValidCashUser()
        {
            if (!Preferences.ContainsKey("UserId"))
                return;

            bool exists = _dbContext.Users
                             .Any(u => u.ID == Preferences.Get("UserId", -1)
                                     && u.Account == Preferences.Get("Account", ""));

            if (!exists)
                LogOut();
        }
    }
}
