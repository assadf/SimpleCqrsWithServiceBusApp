using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}
