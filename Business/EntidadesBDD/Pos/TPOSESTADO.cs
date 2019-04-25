using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TPOSESTADO
    {
        #region variables

        public String CESTADO { get; set; }
        public String DESCRIPCION { get; set; }

        #endregion variables

        #region metodos

        public List<TPOSESTADO> Listar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TPOSESTADO> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CESTADO, ");
                query.Append(" DESCRIPCION ");
                query.Append(" FROM TPOSESTADO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TPOSESTADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TPOSESTADO
                        {
                            CESTADO = reader["CESTADO"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString()
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

        #endregion metodos
    }
}
