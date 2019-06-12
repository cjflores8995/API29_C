﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Business
{
    public class LoadPropertiesConfig
    {
        public string VAR_MSJ_OK = string.Empty;
        public string VAR_MSJ_OK_DET = string.Empty;
        public string VAR_MSJ_ERROR = string.Empty;
        public string VAR_MSJ_ERROR_DET = string.Empty;
        public string VAR_CODIGO_ENTIDAD = string.Empty;
        public string VAR_PATH_CONCILIACION = string.Empty;
        public string VAR_INPUT = string.Empty;
        public string VAR_OUTPUT = string.Empty;
        public string VAR_FORMATO_CONCILIACION = string.Empty;
        public string VAR_FTP_LIBELULA = string.Empty;
        public bool GENERA_ESTRUCTUCTURA_LOCAL = false;
        public string RUTA_FTP_CONCILIACION = string.Empty;
        public string RUTA_LOCAL_CONCILIACION = string.Empty;
        




        public LoadPropertiesConfig()
        {
            VAR_MSJ_OK = ConfigurationManager.AppSettings["VAR_MSJ_OK"];
            VAR_MSJ_OK_DET = ConfigurationManager.AppSettings["VAR_MSJ_OK_DET"];
            VAR_MSJ_ERROR = ConfigurationManager.AppSettings["VAR_MSJ_ERROR"];
            VAR_MSJ_ERROR_DET = ConfigurationManager.AppSettings["VAR_MSJ_ERROR_DET"];
            VAR_CODIGO_ENTIDAD = ConfigurationManager.AppSettings["uafCodigoInstitucion"];
            VAR_PATH_CONCILIACION = ConfigurationManager.AppSettings["pathConciliacion"];
            VAR_INPUT = ConfigurationManager.AppSettings["INPUT"];
            VAR_OUTPUT = ConfigurationManager.AppSettings["OUTPUT"];
            VAR_FORMATO_CONCILIACION = ConfigurationManager.AppSettings["FORMATO_CONCILIACION"];
            VAR_FORMATO_CONCILIACION = ConfigurationManager.AppSettings["FORMATO_CONCILIACION"];
            VAR_FTP_LIBELULA = ConfigurationManager.AppSettings["ftpConciliacionParametros"];
            GENERA_ESTRUCTUCTURA_LOCAL = Convert.ToBoolean(ConfigurationManager.AppSettings["GENERA_ESTRUCTURA_LOCAL"]);
            RUTA_FTP_CONCILIACION = ConfigurationManager.AppSettings["RUTA_FTP_CONCILIACION"];
            RUTA_LOCAL_CONCILIACION = ConfigurationManager.AppSettings["RUTA_LOCAL_CONCILIACION"];
            

        }
    }
}