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
    public class TSPI4DETALLES
    {
        #region variables

        public DateTime FECHAPROCESO { get; set; }
        public int NUMEROCORTE { get; set; }
        public DateTime FECHAVALIDACIONBCE { get; set; }
        public Int64 SGIROTRANSFERENCIAAUTORIZADO { get; set; }
        public int SECUENCIALUNICOBCE { get; set; }
        public DateTime FECHACOMPENSACIONCE { get; set; }
        public int ESTATUSOPI { get; set; }
        public string DETALLE { get; set; }

        #endregion variables

        #region metodos

        //INSERTAR UN REGISTRO
        public TSPI4DETALLES Insertar(TSPI4DETALLES obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("Fitbank");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region arma comando
                query.Append(" INSERT INTO TSPI4DETALLES( ");
                query.Append(" FECHAPROCESO, ");
                query.Append(" NUMEROCORTE, ");
                query.Append(" FECHAVALIDACIONBCE, ");
                query.Append(" SGIROTRANSFERENCIAAUTORIZADO, ");
                query.Append(" SECUENCIALUNICOBCE, ");
                query.Append(" FECHACOMPENSACIONCE, ");
                query.Append(" ESTATUSOPI, ");
                query.Append(" DETALLE ");
                query.Append(" ) VALUES ( ");
                query.Append(" :FECHAPROCESO, ");
                query.Append(" :NUMEROCORTE, ");
                query.Append(" :FECHAVALIDACIONBCE, ");
                query.Append(" :SGIROTRANSFERENCIAAUTORIZADO, ");
                query.Append(" :SECUENCIALUNICOBCE, ");
                query.Append(" :FECHACOMPENSACIONCE, ");
                query.Append(" :ESTATUSOPI, ");
                query.Append(" :DETALLE ");
                query.Append(" ) ");


                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FECHAPROCESO", OracleDbType.Date, obj.FECHAPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROCORTE", OracleDbType.Int32, obj.NUMEROCORTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FECHAVALIDACIONBCE", OracleDbType.Date, obj.FECHAVALIDACIONBCE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SGIROTRANSFERENCIAAUTORIZADO", OracleDbType.Int64, obj.SGIROTRANSFERENCIAAUTORIZADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIALUNICOBCE", OracleDbType.Int16, obj.SECUENCIALUNICOBCE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FECHACOMPENSACIONCE", OracleDbType.Date, obj.FECHACOMPENSACIONCE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ESTATUSOPI", OracleDbType.Int16, obj.ESTATUSOPI, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DETALLE", OracleDbType.Varchar2, obj.DETALLE, ParameterDirection.Input));

                #endregion arma comando

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
            return obj;
        }

        //ELIMINA TODOS LOS REGISTROS DE UN PROCESO

        public bool EliminarRegistrosSpi(DateTime fechaCorte, int numeroCorte)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("Fitbank");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" DELETE TSPI4DETALLES ");
                query.Append(" WHERE FECHAPROCESO = :FECHAPROCESO ");
                query.Append(" AND NUMEROCORTE = :NUMEROCORTE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();
                comando.CommandTimeout = comando.CommandTimeout * 5;

                comando.Parameters.Add(new OracleParameter("FECHAPROCESO", OracleDbType.Date, fechaCorte, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROCORTE", OracleDbType.Int32, numeroCorte, ParameterDirection.Input));

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

        //CUENTA LOS REGISTROS EXISTENTES DEL SPI
        /*
        public bool ContarRegistrosSpi(DateTime fechaCorte, int numeroCorte)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("Fitbank");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(NUMEROCORTE) TSPI4DETALLES ");
                query.Append(" WHERE FECHAPROCESO = :FECHAPROCESO ");
                query.Append(" AND NUMEROCORTE = :NUMEROCORTE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();
                comando.CommandTimeout = comando.CommandTimeout * 5;

                comando.Parameters.Add(new OracleParameter("FECHAPROCESO", OracleDbType.Date, fechaCorte, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROCORTE", OracleDbType.Int32, numeroCorte, ParameterDirection.Input));

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
        */
        #endregion metodos
    }
}
