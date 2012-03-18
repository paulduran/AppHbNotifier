using System;
using System.Globalization;
using AppHbNotifier.Models;

namespace AppHbNotifier.Services
{
    public interface IAppHarborOAuthService
    {
        Uri GetAuthenticationEndpoint(string returnToUrl);
        User OAuthCallback(string code, string uniqueId);
    }

    public class AppHarborOAuthService : IAppHarborOAuthService
    {
        private readonly IAppHarborService appHarborService;
        private readonly IUserService userService;

        public AppHarborOAuthService(IAppHarborService appHarborService, IUserService userService)
        {
            this.appHarborService = appHarborService;
            this.userService = userService;
        }

        public Uri GetAuthenticationEndpoint(string returnToUrl)
        {
            return new Uri(appHarborService.GetAuthorizationUrl(returnToUrl).AbsoluteUri);
        }

        public User OAuthCallback(string code, string uniqueId)
        {
            var token = appHarborService.GetAccessToken(code);
            var user = appHarborService.GetUserInformation(token);

            User masterUser = null;
            if (!string.IsNullOrEmpty(uniqueId))
            {
                masterUser = userService.Get(uniqueId);
            }

            var appUser = AppHarborCreateOrUpdateAccountIfNeeded(token, user, masterUser);
            return appUser;
        }

        private User AppHarborCreateOrUpdateAccountIfNeeded(string accessToken, User user, User returnUser)
        {
            if (null == returnUser)
            {
                returnUser = new User {UserName = user.UserName, EmailAddress = user.EmailAddress};               
                userService.Save(returnUser);
            }
            returnUser.AppHbOAuthToken = accessToken;            
            return returnUser;
        }

        private string GenerateUniqueId(string userName)
        {
            return userName.MD5Hash(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
        }
    }
}