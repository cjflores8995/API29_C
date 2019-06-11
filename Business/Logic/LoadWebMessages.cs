using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Business
{
    public class LoadWebMessages
    {
        public string VAR_MSJ_OK = string.Empty;
        public string VAR_MSJ_OK_DET = string.Empty;
        public string VAR_MSJ_ERROR = string.Empty;
        public string VAR_MSJ_ERROR_DET = string.Empty;


        public LoadWebMessages()
        {
            VAR_MSJ_OK = ConfigurationManager.AppSettings["VAR_MSJ_OK"];
            VAR_MSJ_OK_DET = ConfigurationManager.AppSettings["VAR_MSJ_OK_DET"];
            VAR_MSJ_ERROR = ConfigurationManager.AppSettings["VAR_MSJ_ERROR"];
            VAR_MSJ_ERROR_DET = ConfigurationManager.AppSettings["VAR_MSJ_ERROR_DET"];

        }
    }
}
