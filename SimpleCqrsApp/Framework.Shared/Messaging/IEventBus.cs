using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface IEventBus
    {
        Task SendAsync<T>(T @event) where T : IEvent;
    }
}
