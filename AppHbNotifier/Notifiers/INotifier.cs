using AppHbNotifier.Models;

namespace AppHbNotifier.Notifiers
{
    public interface INotifier<in TSettings> where TSettings : new()
    {
        string Name { get; }
        void Notify(TSettings settings, Notification notification);
    }
}