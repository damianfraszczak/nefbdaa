using System;
using Hangfire;
using NEFBDAACommons;
using NEFBDAACommons.DI.Services;
using NEFBDAACommons.Shared.Services;

namespace NEFBDAAStarter.Configuration
{
    public static class JobsScheduler
    {
        public static void OrganizeJobs()
        {
            RecurringJob.AddOrUpdate(
                CommonsConstants.SEND_EMAIL_JOB,
                () => JobsScheduler.RunService(typeof(EmailScheduleService)),
                "0 */5 * * * *");
        }

        public static void RunService(Type serviceType)
        {
            var service = DependencyInjector.GetService<AbstractScheduleJob>(serviceType);
            if (service != null)
            {
                service.InvokeJob();
            }
        }
    }
}
