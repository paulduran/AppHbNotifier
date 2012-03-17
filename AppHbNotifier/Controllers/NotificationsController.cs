using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppHbNotifier.Models;

namespace AppHbNotifier.Controllers
{
    public class NotificationsController : ApiController
    {        
        // POST /api/values
        public HttpResponseMessage<Notification> Post(string id, Notification notification)
        {
            var response = new HttpResponseMessage<Notification>(notification, HttpStatusCode.Created);
            return response;
        }
    }
}