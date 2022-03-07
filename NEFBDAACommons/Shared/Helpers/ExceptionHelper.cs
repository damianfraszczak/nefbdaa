using NEFBDAACommons.Shared.Extensions;
using System;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class ExceptionHelper
    {
        public static string PrintException(Exception ex)
        {
            return ex.ToLogString();
        }
    }
}
