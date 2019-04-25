using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TSISROL
    {
        #region variables

        public String CROL { get; set; }
        public String DESCRIPCION { get; set; }

        #endregion variables

        #region metodos

        public List<TSISROL> Listar(string crol)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TSISROL> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CROL, ");
                query.Append(" DESCRIPCION ");
                query.Append(" FROM TSISROL ");
                query.Append(" WHERE 1 = 1 ");
                if (!string.IsNullOrEmpty(crol))
                {
                    query.Append(" AND CROL = :CROL ");
                }

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (!string.IsNullOrEmpty(crol))
                {
                    comando.Parameters.Add(new OracleParameter("CROL", OracleDbType.Varchar2, crol, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TSISROL>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TSISROL
                        {
                            CROL = reader["CROL"].ToString(),
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
