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
    public class TSISUSUARIOSESION
    {
        #region variables

        public String CUSUARIO { get; set; }
        public DateTime? FFIN { get; set; }
        public DateTime? FINICIO { get; set; }
        public String SESION { get; set; }
        public String CTERMINAL { get; set; }
        public Int32? INTENTOS { get; set; }

        #endregion variables

        #region metodos

        public TSISUSUARIOSESION TraeSesionActiva(string cusuario)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TSISUSUARIOSESION obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CUSUARIO, ");
                query.Append(" FFIN, ");
                query.Append(" FINICIO, ");
                query.Append(" SESION, ");
                query.Append(" CTERMINAL, ");
                query.Append(" INTENTOS ");
                query.Append(" FROM TSISUSUARIOSESION ");
                query.Append(" WHERE CUSUARIO = :CUSUARIO ");
                query.Append(" AND FFIN = :FHASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, cusuario, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Util.FHasta(), ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TSISUSUARIOSESION
                        {
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FFIN = Util.ConvertirFecha(reader["FFIN"].ToString()),
                            FINICIO = Util.ConvertirFecha(reader["FINICIO"].ToString()),
                            SESION = reader["SESION"].ToString(),
                            CTERMINAL = reader["CTERMINAL"].ToString(),
                            INTENTOS = Util.ConvertirNumero(reader["INTENTOS"].ToString())
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

        public bool Insertar(TSISUSUARIOSESION obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TSISUSUARIOSESION ( ");
                query.Append(" CUSUARIO, ");
                query.Append(" FFIN, ");
                query.Append(" FINICIO, ");
                query.Append(" SESION, ");
                query.Append(" CTERMINAL, ");
                query.Append(" INTENTOS ");
                query.Append(" ) VALUES ( ");
                query.Append(" :CUSUARIO, ");
                query.Append(" :FFIN, ");
                query.Append(" :FINICIO, ");
                query.Append(" :SESION, ");
                query.Append(" :CTERMINAL, ");
                query.Append(" :INTENTOS ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FFIN", OracleDbType.Date, obj.FFIN, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FINICIO", OracleDbType.Date, obj.FINICIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SESION", OracleDbType.Varchar2, obj.SESION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTERMINAL", OracleDbType.Varchar2, obj.CTERMINAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("INTENTOS", OracleDbType.Int32, obj.INTENTOS, ParameterDirection.Input));

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

        public bool Actualizar(TSISUSUARIOSESION obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TSISUSUARIOSESION SET ");
                query.Append(" SESION = :SESION, ");
                query.Append(" INTENTOS = :INTENTOS, ");
                query.Append(" FINICIO = :FINICIO ");
                query.Append(" WHERE CUSUARIO = :CUSUARIO ");
                query.Append(" AND FFIN = :FFIN ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("SESION", OracleDbType.Varchar2, obj.SESION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("INTENTOS", OracleDbType.Int32, obj.INTENTOS, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FINICIO", OracleDbType.Date, obj.FINICIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FFIN", OracleDbType.Date, obj.FFIN, ParameterDirection.Input));

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

        public bool Caducar(TSISUSUARIOSESION obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TSISUSUARIOSESION SET ");
                query.Append(" FFIN = :FFIN ");
                query.Append(" WHERE CUSUARIO = :CUSUARIO ");
                query.Append(" AND FFIN = :FHASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FFIN", OracleDbType.Date, obj.FFIN, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Util.FHasta(), ParameterDirection.Input));

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

        #endregion metodos
    }
}
