using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface ICommandBus
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
