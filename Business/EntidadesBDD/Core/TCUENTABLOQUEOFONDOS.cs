using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TCUENTABLOQUEOFONDOS
    {
        #region variables

        public Int32? SBLOQUEOFONDOS { get; set; }
        public String CCUENTA { get; set; }
        public Decimal? MONTOLIBERADO { get; set; }
        public Decimal? MONTOPENDIENTE { get; set; }
        public Decimal? VALORBLOQUEO { get; set; }
        public String REFERENCIA { get; set; }

        #endregion variables

        #region metodos

        public List<TCUENTABLOQUEOFONDOS> ListarBloqueosRecuperacion(string ccuenta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TCUENTABLOQUEOFONDOS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" SBLOQUEOFONDOS, ");
                query.Append(" CCUENTA, ");
                query.Append(" VALORBLOQUEO, ");
                query.Append(" MONTOLIBERADO, ");
                query.Append(" MONTOPENDIENTE, ");
                query.Append(" REFERENCIA ");
                query.Append(" FROM TCUENTABLOQUEOFONDOS ");
                query.Append(" WHERE FHASTA = :FHASTA ");
                query.Append(" AND CCONCEPTO NOT IN ('1107', '1108', '1152', '1124', '1130') ");
                query.Append(" AND ESTATUSBLOQUEO = 'ING' ");
                query.Append(" AND CCUENTA = :CCUENTA ");

                comando.Connection = ado.oraConexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Util.FHasta(), ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, ccuenta, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TCUENTABLOQUEOFONDOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TCUENTABLOQUEOFONDOS
                        {
                            SBLOQUEOFONDOS = Util.ConvertirNumero(reader["SBLOQUEOFONDOS"].ToString()),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            VALORBLOQUEO = Util.ConvertirDecimal(reader["VALORBLOQUEO"].ToString()),
                            MONTOLIBERADO = Util.ConvertirDecimal(reader["MONTOLIBERADO"].ToString()),
                            MONTOPENDIENTE = Util.ConvertirDecimal(reader["MONTOPENDIENTE"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString()
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

        public List<TCUENTABLOQUEOFONDOS> ListarBloqueosCredito(string ccuentaCredito)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TCUENTABLOQUEOFONDOS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" SBLOQUEOFONDOS, ");
                query.Append(" CCUENTA, ");
                query.Append(" VALORBLOQUEO, ");
                query.Append(" MONTOLIBERADO, ");
                query.Append(" MONTOPENDIENTE, ");
                query.Append(" REFERENCIA ");
                query.Append(" FROM TCUENTABLOQUEOFONDOS ");
                query.Append(" WHERE FHASTA = :FHASTA ");
                query.Append(" AND CCONCEPTO IN ('1111', '1132') ");
                query.Append(" AND ESTATUSBLOQUEO = 'ING' ");
                query.Append(" AND REFERENCIA = :REFERENCIA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Util.FHasta(), ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, ccuentaCredito, ParameterDirection.Input));

                #endregion armaComando


                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TCUENTABLOQUEOFONDOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TCUENTABLOQUEOFONDOS
                        {
                            SBLOQUEOFONDOS = Util.ConvertirNumero(reader["SBLOQUEOFONDOS"].ToString()),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            VALORBLOQUEO = Util.ConvertirDecimal(reader["VALORBLOQUEO"].ToString()),
                            MONTOLIBERADO = Util.ConvertirDecimal(reader["MONTOLIBERADO"].ToString()),
                            MONTOPENDIENTE = Util.ConvertirDecimal(reader["MONTOPENDIENTE"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString()
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
