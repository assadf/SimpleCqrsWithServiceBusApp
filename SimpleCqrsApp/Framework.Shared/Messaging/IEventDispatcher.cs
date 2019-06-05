using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface IEventDispatcher
    {
        Task DispatchAsync<T>(T @event) where T : IEvent;
    }
}
