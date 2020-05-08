using System.Threading.Tasks;
using Common.Messages;

namespace Common.Dispatchers
{
    public interface ICommandDispatcher
    {
         Task SendAsync<T>(T command) where T : ICommand;
    }
}