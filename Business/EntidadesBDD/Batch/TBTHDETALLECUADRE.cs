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
    public class TBTHDETALLECUADRE
    {
        public bool InsertarCuadre(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLECUADRE ");
                query.Append("    (SELECT TB.FPROCESO, ");
                query.Append("            TB.CPROCESO, ");
                query.Append("            TB.SECUENCIA, ");
                query.Append("            TB.SUBSECUENCIA, ");
                query.Append("            (SELECT NVL (SUM (VALORMONEDACUENTA), 0) ");
                query.Append("               FROM TMOVIMIENTOS TM ");
                query.Append("              WHERE TM.CCUENTA = TB.CUENTA ");
                query.Append("                AND TM.NUMERODOCUMENTO LIKE '%' || TO_CHAR (TB.CPROCESO) || '%' || TB.SECUENCIA || '%' ");
                query.Append("                AND TM.VALORMONEDACUENTA = TB.VALOR ");
                query.Append("                AND TM.CTRANSACCION_ORIGEN IN ('7100', '7178') ");
                query.Append("                AND TM.FCONTABLE >= TRUNC (FCARGA)) VALORFIT ");
                query.Append("       FROM TBTHDETALLEPROCESO TB ");
                query.Append("      WHERE TB.FPROCESO = :FPROCESO ");
                query.Append("        AND TB.CPROCESO = :CPROCESO ");
                query.Append("        AND TB.CTIPOPROCESO IN ('NOTCRE', 'NOTCREPRE', 'REVERSO', 'NOTDEB') ");
                query.Append("        AND TB.VALOR > 0) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();
                comando.CommandTimeout = comando.CommandTimeout * 5;

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                resp = ado.EjecutarComando(comando);

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                resp = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return resp;
        }

        public bool InsertarCuadre(TBTHDETALLEPROCESO obj, Decimal? valorfit)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLECUADRE ( ");

                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" VALORFIT ");

                query.Append(" ) VALUES ( ");

                query.Append(" :FPROCESO, ");
                query.Append(" :CPROCESO, ");
                query.Append(" :SECUENCIA, ");
                query.Append(" :VALORFIT ");

                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALORFIT", OracleDbType.Decimal, valorfit, ParameterDirection.Input));

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                resp = ado.EjecutarComando(comando);

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                resp = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return resp;
        }

        public bool BorraCuadre(TBTHDETALLEPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" DELETE TBTHDETALLECUADRE ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND SECUENCIA = :SECUENCIA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();
                comando.CommandTimeout = comando.CommandTimeout * 5;

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                resp = ado.EjecutarComando(comando);

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                resp = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return resp;
        }
    }
}
