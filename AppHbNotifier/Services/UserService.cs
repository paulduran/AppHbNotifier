using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppHbNotifier.Models;
using Raven.Client;

namespace AppHbNotifier.Services
{
    public interface IUserService
    {
        void Save(User user);
        User Get(string username);
    }

    public class UserService : IUserService
    {
        private readonly IDocumentSession session;
        
        public UserService(IDocumentSession session)
        {
            this.session = session;
        }
        public void Save(User user)
        {
            session.Store(user);
            session.SaveChanges();
        }
        public User Get(string uniqueId)
        {
            return session.Load<User>(uniqueId);            
        }
    }
}