using System;

namespace Framework.Shared.Messaging
{
    public interface ICommand
    {
        Guid CommandId { get; set; }
    }
}
