using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public static class Extensions
    {
        public static ILog GetLog<T>(this ILogger<T> logger)
        {
            return LoggerImpl<T>.Log;
        }
    }
}