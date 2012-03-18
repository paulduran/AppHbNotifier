using System.Web.Mvc;
using AppHbNotifier.Services;

namespace AppHbNotifier.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppHarborOAuthService oAuthService;
        private readonly IFormsAuthenticationService formsAuthenticationService;

        public AccountController(IAppHarborOAuthService oAuthService, IFormsAuthenticationService formsAuthenticationService)
        {
            this.oAuthService = oAuthService;
            this.formsAuthenticationService = formsAuthenticationService;
        }

        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OAuthComplete(string id, string code, string returnUrl)
        {
            var user = oAuthService.OAuthCallback(code, id);
            ActionResult result = null;
            if (user != null)
            {
                // Log the user in
                formsAuthenticationService.SetAuthCookie(user.UserName, true);

                if (string.IsNullOrEmpty(returnUrl))
                {
                    result = RedirectToAction("Index", new { Controller = "Account" });
                }
                else
                {
                    result = Redirect(returnUrl);
                }
            }

            return result;
        }
    }
}
