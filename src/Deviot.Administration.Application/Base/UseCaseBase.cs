using Deviot.Common;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Deviot.Administration.Application.Base
{
    public abstract class UseCaseBase
    {
        protected readonly INotifier _notifier;
        protected readonly ILogger _logger;

        protected const string INTERNAL_ERROR_MESSAGE = "Não foi possível executar a requisição nesse momento.";

        protected UseCaseBase(INotifier notifier, ILogger logger)
        {
            _notifier = notifier;
            _logger = logger;
        }

        protected virtual void NotifyOk(string message)
        {
            _logger.LogInformation(message);
            _notifier.Notify(HttpStatusCode.OK, message);
        }

        protected virtual void NotifyCreated(string message)
        {
            _logger.LogInformation(message);
            _notifier.Notify(HttpStatusCode.Created, message);
        }

        protected virtual void NotifyNoContent(string message)
        {
            _logger.LogInformation(message);
            _notifier.Notify(HttpStatusCode.NoContent, message);
        }

        protected virtual void NotifyBadRequest(string message)
        {
            _logger.LogWarning(message);
            _notifier.Notify(HttpStatusCode.BadRequest, message);
        }

        protected virtual void NotifyForbidden(string message)
        {
            _logger.LogWarning(message);
            _notifier.Notify(HttpStatusCode.Forbidden, message);
        }

        protected virtual void NotifyNotFound(string message)
        {
            _logger.LogWarning(message);
            _notifier.Notify(HttpStatusCode.NotFound, message);
        }

        protected virtual void NotifyInternalServerError(Exception exception)
        {
            var messages = Utils.GetAllExceptionMessages(exception);

            foreach (var message in messages)
                _logger.LogError(message);

            _notifier.Notify(HttpStatusCode.InternalServerError, INTERNAL_ERROR_MESSAGE);
        }
    }
}
