using System;

namespace AppHbNotifier.Models
{
    public class User
    {
        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public string UniqueId { get; set; }

        public string AppHbOAuthToken { get; set; }        
    }
}