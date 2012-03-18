using System;
using AppHbNotifier.Models;
using HipChat;

namespace AppHbNotifier.Notifiers
{
    public class HipChatSettings
    {
        public string Token { get; set; }
        public int RoomId { get; set; }
    }

    public class HipChatNotifier : INotifier<HipChatSettings>
    {
        private const string SuccessStatus = "succeeded";

        public string Name
        {
            get { return "HipChat"; }
        }

        public void Notify(HipChatSettings settings, Notification notification)
        {
            var token = settings.Token;
            var roomId = settings.RoomId;

            var client = new HipChatClient(token, roomId, "AppHarbor");
            string message = FormatMessage(notification);
            client.SendMessage(message, notification.Build.Status == SuccessStatus ? HipChatClient.BackgroundColor.green : HipChatClient.BackgroundColor.red);
        }

        private string FormatMessage(Notification notification)
        {
            string commitId = notification.Build.Commit.Id.Length > 10
                                  ? notification.Build.Commit.Id.Substring(0, 10)
                                  : notification.Build.Commit.Id;

            return String.Format("<b>{0}</b> build {1} <b>{2}</b><br/>- {3}", notification.Application.Name,
                                 commitId, notification.Build.Status,
                                 notification.Build.Commit.Message);
        }
    }
}