using Deviot.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Deviot.Administration.Test.TDD.Base
{
    public abstract class UseCaseBaseTest
    {
        protected readonly INotifier _notifier;

        protected const string INTERNAL_ERROR_MESSAGE = "Não foi possível executar a requisição nesse momento.";

        protected UseCaseBaseTest()
        {
            _notifier = new Notifier();
        }

        protected ILogger<T> GetLogger<T>()
        {
            return new NullLogger<T>();
        }
    }
}
