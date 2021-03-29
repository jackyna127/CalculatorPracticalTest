using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCalculatorTest.Utilities
{
    public static class Loggers
    {
       // private static ILog logger = LogManager.GetLogger("mylog");
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void Log(string logMessage, string logLevel = "Info")
        {
            log4net.Config.XmlConfigurator.Configure();
            switch (logLevel.ToUpper())
            {
                case "DEBUG":
                    logger.Debug(logMessage);
                    break;
                case "INFO":
                    logger.Info(logMessage);
                    break;
                case "WARN":
                    logger.Warn(logMessage);
                    break;
                case "ERROR":
                    logger.Error(logMessage);
                    break;
                case "FATAL":
                    logger.Fatal(logMessage);
                    break;
            }
        }
    }
}
