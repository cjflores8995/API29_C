using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public static class Logging
    {
        public static ILog _loggerGeneral;
        public static ILog _loggerError;

        public static bool IniciaLog()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                _loggerGeneral = LogManager.GetLogger("General");
                _loggerError = LogManager.GetLogger("Error");

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void EscribirLog(string mensaje, Exception exception, string tipo)
        {
            switch (tipo)
            {
                case "GEN":
                    _loggerGeneral.Info("\t" + mensaje, exception);
                    break;
                case "ERR":
                    _loggerError.Error("\t" + mensaje, exception);
                    break;
                default:
                    _loggerGeneral.Info("\t" + mensaje, exception);
                    break;
            }
        }
    }
}
