using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Razor;
using FluentEmail.SendGrid;
using FluentEmail.Smtp;
using NEFBDAACommons.Database.Services.CrudService;
using NEFBDAACommons.DI.Attributes;
using NEFBDAACommons.Security.Services;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Helpers;
using NEFBDAACommons.Shared.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace NEFBDAACommons.Shared.Services
{
    [TransientService]
    public class EmailScheduleService : AbstractScheduleJob
    {
        private readonly IEmailService emailService;
        private readonly IConfigService configService;

        public EmailScheduleService(
            IAuthenticationService authenticationService,
            IEmailService emailService,
            IConfigService configService) : base(authenticationService)
        {
            this.emailService = emailService;
            this.configService = configService;
        }

        protected override void ExecuteJob()
        {
            // verify if user has inserted config properties
            var config = configService.GetAppConfig<BaseAppConfigViewModel>(GetUserForScheduleJob());
            if (config.IsEmailConfigValid())
            {
                Email.DefaultSender = BuildSender(config);
                Email.DefaultRenderer = new RazorRenderer();

                var notSendEmails = emailService.GetNotSendEmails(GetUserForScheduleJob()).ToList();
                notSendEmails.ForEach(emailEntity =>
                {
                    try
                    {
                        var email = Email
                            .From(emailEntity.From)
                            .To(emailEntity.To)
                            .Subject(emailEntity.Subject)
                            .UsingTemplate(emailEntity.Template, emailEntity.TemplateParams.ToDynamicObject())
                            .Send();
                        emailEntity.DateSend = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        emailEntity.LastException = ex.ToLogString();
                    }
                });
                emailService.BatchUpdate(notSendEmails, GetUserForScheduleJob());
            }
        }

        private ISender BuildSender(BaseAppConfigViewModel config)
        {
            if (!string.IsNullOrWhiteSpace(config.EmailSendGridApiKey))
            {
                return new SendGridSender(config.EmailSendGridApiKey);
            }
            else
            {
                var smtpClient = new SmtpClient();
                smtpClient.Host = config.EmailHost;
                smtpClient.Port = ParsingHelper.GetInt(config.EmailPort);
                smtpClient.Credentials = new NetworkCredential()
                {
                    UserName = config.EmailUser,
                    Password = config.EmailPassword
                };
                return new SmtpSender(smtpClient);
            }
        }
    }
}
