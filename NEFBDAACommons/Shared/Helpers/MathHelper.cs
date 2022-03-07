using System;
using System.Collections.Generic;
using System.Text;

namespace NEFBDAACommons.Shared.Helpers
{
    public static class MathHelper
    {
        public static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
