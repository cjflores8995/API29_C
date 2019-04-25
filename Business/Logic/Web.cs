using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class Web
    {
        #region variables

        public static List<TSISROL> ltRol = null;
        public static List<VFITOFICINAS> ltOficinas = null;

        public static Int32 bddTimeout = 30;
        public static Int32 bddProcesos = 30;

        #endregion variables

        public static void IniciaSistema()
        {
            string error = string.Empty;

            Logging.IniciaLog();

            #region Parametros Globales

            try { bddTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["bddTimeout"].Trim()); }
            catch { bddTimeout = 30; }

            try { bddProcesos = Convert.ToInt32(ConfigurationManager.AppSettings["bddProcesos"].Trim()); }
            catch { bddProcesos = 30; }

            #endregion Parametros Globales

            #region Carga Tablas Globales

            #region ROL
            try
            {
                ltRol = new TSISROL().Listar(null);
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ROL ", ex, "ERR");
            }
            #endregion ROL

            #region FITOFICINAS
            try
            {
                ltOficinas = new VFITOFICINAS().Listar(null);
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " VOFICINAS ", ex, "ERR");
            }
            #endregion FITOFICINAS

            #endregion Carga Tablas Globales
        }

        public Int32 EjecutaQuery(string query, out string error)
        {
            return new BddAuxiliar().EjcutaQuery(query, out error);
        }

        public static string TraeDescripcionRol(string crol)
        {
            try
            {
                var obj = ltRol.Where(x => x.CROL == crol).LastOrDefault();
                return obj.DESCRIPCION;
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                return "";
            }
        }

        public static string TraeDescripcionAgencia(Int32? cagencia)
        {
            string nombreAgencia = string.Empty;
            try
            {
                if (cagencia != null)
                {
                    var obj = ltOficinas.Where(x => x.COFICINA == cagencia).LastOrDefault();
                    nombreAgencia = obj.OFICINA;
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return nombreAgencia;
        }
    }
}
