using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
