using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NEFBDAACommons.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ToLogString(this Exception exception)
        {
            return ToLogString(exception, Environment.StackTrace);
        }

        public static string ToLogString(this Exception exception, string environmentStackTrace)
        {
            var environmentStackTraceLines = ExceptionExtensions.GetUserStackTraceLines(environmentStackTrace);
            if (environmentStackTraceLines.Any())
            {
                environmentStackTraceLines.RemoveAt(0);
            }
                

            var stackTraceLines = ExceptionExtensions.GetStackTraceLines(exception.StackTrace);
            stackTraceLines.AddRange(environmentStackTraceLines);

            var fullStackTrace = String.Join(Environment.NewLine, stackTraceLines);

            var logMessage = exception.Message + Environment.NewLine + fullStackTrace;
            return logMessage;
        }
        private static List<string> GetStackTraceLines(string stackTrace)
        {
            return stackTrace.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
        }

        private static List<string> GetUserStackTraceLines(string fullStackTrace)
        {
            var regex = new Regex(@"([^\)]*\)) in (.*):line (\d)*$");
            var stackTraceLines = ExceptionExtensions.GetStackTraceLines(fullStackTrace);
            return stackTraceLines.Where(l => regex.IsMatch(l)).ToList();
        }
    }
}
