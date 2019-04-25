using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TBTHTIPOPROCESO
    {
        public String CTIPOPROCESO { get; set; }
        public String DESCRIPCION { get; set; }
        public String ACTIVO { get; set; }
        public String WEB { get; set; }

        public List<TBTHTIPOPROCESO> Listar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHTIPOPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" ACTIVO, ");
                query.Append(" WEB ");
                query.Append(" FROM TBTHTIPOPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHTIPOPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHTIPOPROCESO
                        {
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            ACTIVO = reader["ACTIVO"].ToString(),
                            WEB = reader["WEB"].ToString()
                        });
                    }
                }
                else
                {
                    ltObj = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                ltObj = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return ltObj;
        }
    }
}
