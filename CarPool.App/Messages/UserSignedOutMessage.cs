using CarPool.BL.Models;

namespace CarPool.App.Messages
{
    public record UserSignedOutMessage<T> : Message<T>
        where T : IModel
    {
    }
}
