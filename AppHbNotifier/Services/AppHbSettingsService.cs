using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppHbNotifier.Services
{
    public interface IAppHbSettings
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }

    public class AppHbSettings : IAppHbSettings
    {
        public string ClientId
        {
            get { return ConfigurationManager.AppSettings["AppHbClientId"]; }
        }

        public string ClientSecret
        {
            get { return ConfigurationManager.AppSettings["AppHbClientSecretKey"]; }
        }
    }
}