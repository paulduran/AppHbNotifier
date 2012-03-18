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

            User appuser = userService.Get(uniqueId) ?? new User { UserName = user.UserName, EmailAddress = user.EmailAddress };            
            appuser.AppHbOAuthToken = code;
            userService.Save(appuser);
            return appuser;
        }
    }
}