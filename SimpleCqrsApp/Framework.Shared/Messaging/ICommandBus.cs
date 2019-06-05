using System;
using System.Threading.Tasks;

namespace Framework.Shared.Messaging
{
    public interface ICommandBus
    {
        Task<Guid> SendAsync<T>(T command) where T : ICommand;
    }
}
