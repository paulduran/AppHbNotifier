using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppHbNotifier.Services;

namespace AppHbNotifier.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAppHbSettings settings;

        public HomeController(IAppHbSettings settings)
        {
            this.settings = settings;
        }

        public ActionResult Index()
        {
            return
                View("Index", (object)string.Format("https://appharbor.com/user/authorizations/new?client_id={0}&redirect_uri={1}",
                                   settings.ClientId, HttpUtility.UrlEncode(ToPublicUri("/Account/OAuthComplete"))));
        }

        private string ToPublicUri(string relativeUri)
        {

            var uriBuilder = new UriBuilder
                                 {
                                     Host = Request.Url.Host,
                                     Path = "/",
                                     Port = 80,
                                     Scheme = "http",
                                 };

            if (Request.IsLocal)
            {
                uriBuilder.Port = Request.Url.Port;
            }

            return new Uri(uriBuilder.Uri, relativeUri).AbsoluteUri;
        }
    }
}
