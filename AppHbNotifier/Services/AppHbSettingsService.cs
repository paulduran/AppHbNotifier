using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AppHbNotifier.Services
{
    public interface IAppHbSettingsService
    {
        string ClientId { get; }
        string ClientSecret { get; }
    }

    public class AppHbSettingsService : IAppHbSettingsService
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