using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AppHbNotifier.Services
{
    public interface IFormsAuthenticationService
    {
        void SetAuthCookie(string Identifier, bool Persist);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {

        public void SetAuthCookie(string Identifier, bool Persist)
        {
            FormsAuthentication.SetAuthCookie(Identifier, Persist);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}