using Microsoft.Extensions.Logging;

namespace NEFBDAACommons.Shared.Services
{

    public abstract class AbstractService
    {
        private ILogger _logger;

        protected ILogger Logger
        {
            get
            {

                return _logger = _logger ?? ApplicationLogger.CreateLogger(GetType());
            }
        }
    }
}
