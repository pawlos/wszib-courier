using System.Threading.Tasks;
using Courier.Core.Commands;

namespace Courier.Api.Framework
{
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T : ICommand;
    }
}