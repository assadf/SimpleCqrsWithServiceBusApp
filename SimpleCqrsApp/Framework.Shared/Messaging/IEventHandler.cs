using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}
