using Microsoft.Extensions.Logging;
using NEFBDAACommons.Security.Models.Auth;
using NEFBDAACommons.Security.Services;
using NEFBDAACommons.Shared.Helpers;
using System;

namespace NEFBDAACommons.Shared.Services
{
    public abstract class AbstractScheduleJob : AbstractService
    {
        private readonly IAuthenticationService _authenticationService;
        private AuthenticatedUserViewModel authenticatedUser;

        public AbstractScheduleJob(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        public void InvokeJob()
        {
            StartJob();
            try
            {
                ExecuteJob();
            }
            catch (Exception ex)
            {
                OnException(ex);
            }
            FinishJob();
        }

        protected virtual void OnException(Exception ex)
        {
            Logger.LogError($"Error occured during executing background job {GetType()}\n {ExceptionHelper.PrintException(ex)}", ex);
        }

        protected abstract void ExecuteJob();

        protected AuthenticatedUserViewModel GetUserForScheduleJob()
        {
            if (authenticatedUser == null)
            {
                authenticatedUser = _authenticationService.GetUserForScheduleJob();
            }
            return authenticatedUser;
        }

        protected virtual void StartJob()
        {
            Logger.Log(LogLevel.Debug, "Start a scheduled job");
        }
        protected virtual void FinishJob()
        {
            Logger.Log(LogLevel.Debug, "Finish a scheduled job");
        }
    }
}
