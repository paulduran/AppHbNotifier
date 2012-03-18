using System;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using AppHbNotifier.Models;
using Newtonsoft.Json;

namespace AppHbNotifier.Services
{
    public interface IAppHarborService
    {
        Uri GetAuthorizationUrl();
        Uri GetAuthorizationUrl(string redirectUri);
        RedirectResult RedirectToAuthorizationResult(string redirectUri);
        RedirectResult RedirectToAuthorizationResult();
        string GetAccessToken(string code);
        User GetUserInformation(string token);
        HttpWebRequest GetAuthenticatedWebRequest(string token, string url);
    }

    public class AppHarborService : IAppHarborService
    {
        private readonly string clientId;
        private readonly string secret ;
        public AppHarborService(IAppHbSettingsService settings)
        {
            clientId = settings.ClientId;
            secret = settings.ClientSecret;
        }

        public Uri GetAuthorizationUrl()
        {
            return new Uri(string.Format("https://appharbor.com/user/authorizations/new?client_id={0}", clientId));
        }

        public Uri GetAuthorizationUrl(string redirectUri)
        {
            return new Uri(string.Format("https://appharbor.com/user/authorizations/new?client_id={0}&redirect_uri={1}", clientId, HttpUtility.UrlEncode(redirectUri)));
        }

        public RedirectResult RedirectToAuthorizationResult(string redirectUri)
        {
            return new RedirectResult(GetAuthorizationUrl(redirectUri).AbsoluteUri);
        }

        public RedirectResult RedirectToAuthorizationResult()
        {
            return new RedirectResult(GetAuthorizationUrl().ToString());
        }

        public string GetAccessToken(string code)
        {
            WebRequest req = WebRequest.Create("https://appharbor.com/tokens");
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";

            string parameters = string.Format("client_id={0}&client_secret={1}&code={2}", clientId, secret, HttpUtility.UrlEncode(code));
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(parameters);
            req.ContentLength = bytes.Length;
            Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            var resp = req.GetResponse();
            var sr = new StreamReader(resp.GetResponseStream());
            var respStr = sr.ReadToEnd().Trim();
            var nvc = HttpUtility.ParseQueryString(respStr);

            return nvc["access_token"];
        }

        public User GetUserInformation(string token)
        {
            var wc = new WebClient();
            wc.Headers.Add("Authorization", "BEARER " + token);
            wc.Headers.Add("Accept", "application/json");
            var str = wc.DownloadString("https://appharbor.com/user");
            dynamic obj = JsonConvert.DeserializeObject(str);
            return new User
                       {
                           EmailAddress = obj.email_addresses[0].Value,
                           UserName = obj.username,
                           Id = obj.id
                       };
        }
      
        public HttpWebRequest GetAuthenticatedWebRequest(string token, string url)
        {
            var ret = (HttpWebRequest)WebRequest.Create(url);
            ret.Headers.Add("Authorization", "BEARER " + token);
            return ret;
        }
    }
}