using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppHbNotifier.Models;

namespace AppHbNotifier.Services
{
    public interface IUserService
    {
        void Save(User user);
        User Get(string username);
    }

    public class UserService : IUserService
    {
        private static readonly IDictionary<string , User > Users = new Dictionary<string, User>();
        public void Save(User user)
        {
            Users[user.UniqueId] = user;
        }
        public User Get(string uniqueId)
        {
            User user;
            if (Users.TryGetValue(uniqueId, out user))
                return user;
            return null;
        }
    }
}