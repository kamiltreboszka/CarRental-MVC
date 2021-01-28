using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental
{
    public class LoggerImpl<T>
    {
        public static readonly ILog Log = LogManager.GetLogger(typeof(T));
    }
}