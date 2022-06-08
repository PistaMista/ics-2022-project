using CarPool.BL.Models;

namespace CarPool.App.Messages
{
    public record UserSignedInMessage<T> : Message<T>
        where T : IModel
    {
    }
}
