using Common.RabbitMq;
using Common.Messages;
using System.Threading.Tasks;

namespace Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}