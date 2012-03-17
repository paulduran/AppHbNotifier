using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppHbNotifier.Models
{
    public class Notification
    {
        public Application Application { get; set; }
        public Build Build { get; set; }
    }
    public class Application
    {
        public string Name { get; set; }
    }
    public class Build
    {
        public Commit Commit { get; set; }
        public string Status { get; set; }
    }
    public class Commit
    {
        public string Id { get; set; }
        public string Message { get; set; }
    }
}