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
    public class TMOVIMIENTOS
    {
        #region variables

        public String NUMEROMENSAJE { get; set; }
        public Decimal? VALORMONEDACUENTA { get; set; }

        #endregion variables

        #region metodos

        public List<TMOVIMIENTOS> ListarMovimientosProceso(DateTime? fproceso, string ccuenta, string proceso, string secuencia, Decimal? valor)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TMOVIMIENTOS> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" VALORMONEDACUENTA ");
                query.Append(" FROM TMOVIMIENTOS ");
                query.Append(" WHERE CCUENTA = :CCUENTA ");
                //query.Append(" AND FCONTABLE >= :FPROCESO ");
                query.Append(" AND NUMERODOCUMENTO LIKE '%'||:PROCESO||'%'||:SECUENCIA||'%' ");
                query.Append(" AND VALORMONEDACUENTA = :VALORMONEDACUENTA ");
                query.Append(" AND CATEGORIA = 'DEPVEF' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, ccuenta, ParameterDirection.Input));
                //comando.Parameters.Add(new OracleParameter("FPROCESO",OracleDbType.Date,fproceso,ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("PROCESO", OracleDbType.Varchar2, proceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Varchar2, secuencia, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALORMONEDACUENTA", OracleDbType.Decimal, valor, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TMOVIMIENTOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TMOVIMIENTOS
                        {
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            VALORMONEDACUENTA = Util.ConvertirDecimal(reader["VALORMONEDACUENTA"].ToString())
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

        public TMOVIMIENTOS ListarMovimientosProcesoLotes(DateTime? fcarga, string ccuenta, Decimal? valor)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TMOVIMIENTOS obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" NVL (SUM (VALORMONEDACUENTA), 0) VALORMONEDACUENTA ");
                query.Append(" FROM TMOVIMIENTOS ");
                query.Append(" WHERE CATEGORIA = 'DEPVEF' ");
                query.Append(" AND CSUBSISTEMA_ORIGEN = '04' ");
                query.Append(" AND CTRANSACCION_ORIGEN = '6041' ");
                query.Append(" AND RUBRO >= 9000 ");
                query.Append(" AND FAPPSERVER >= :FCARGA ");
                query.Append(" AND CCUENTA = :CCUENTA ");
                query.Append(" AND VALORMONEDACUENTA = :VALORMONEDACUENTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, fcarga, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, ccuenta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALORMONEDACUENTA", OracleDbType.Decimal, valor, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TMOVIMIENTOS
                        {
                            VALORMONEDACUENTA = Util.ConvertirDecimal(reader["VALORMONEDACUENTA"].ToString())
                        };
                    }
                }
                else
                {
                    obj = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                obj = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return obj;
        }

        #endregion metodos
    }
}
