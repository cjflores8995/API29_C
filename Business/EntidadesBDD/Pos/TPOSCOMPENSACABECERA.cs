using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TPOSCOMPENSACABECERA
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CCONVENIO { get; set; }
        public DateTime? FAUTORIZACION { get; set; }
        public String CUSUARIOAUTORIZACION { get; set; }
        public String ERROR { get; set; }
        public String TRANSFERENCIA { get; set; }
        public String COMISION { get; set; }
        public String RETENCIONFTE { get; set; }
        public String RETENCIONIVA { get; set; }
        public String CESTADO { get; set; }

        #endregion variables

        #region metodos

        public bool Insertar(TPOSCOMPENSACABECERA obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TPOSCOMPENSACABECERA ( ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" CESTADO ");
                query.Append(" ) VALUES ( ");
                query.Append(" :FPROCESO, ");
                query.Append(" :CCONVENIO, ");
                query.Append(" :CESTADO ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, obj.CCONVENIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));

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

        public bool Actualizar(TPOSCOMPENSACABECERA obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" FAUTORIZACION = :FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION = :CUSUARIOAUTORIZACION, ");
                query.Append(" ERROR = :ERROR, ");
                query.Append(" TRANSFERENCIA = :TRANSFERENCIA, ");
                query.Append(" COMISION = :COMISION, ");
                query.Append(" RETENCIONFTE = :RETENCIONFTE, ");
                query.Append(" RETENCIONIVA = :RETENCIONIVA, ");
                query.Append(" CESTADO = :CESTADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, obj.FAUTORIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIOAUTORIZACION", OracleDbType.Varchar2, obj.CUSUARIOAUTORIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSFERENCIA", OracleDbType.Varchar2, obj.TRANSFERENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COMISION", OracleDbType.Varchar2, obj.COMISION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RETENCIONFTE", OracleDbType.Varchar2, obj.RETENCIONFTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RETENCIONIVA", OracleDbType.Varchar2, obj.RETENCIONIVA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, obj.CCONVENIO, ParameterDirection.Input));

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

        public bool Actualizar(VPOSCOMPENSACABECERA obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" FAUTORIZACION = :FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION = :CUSUARIOAUTORIZACION, ");
                query.Append(" ERROR = :ERROR, ");
                query.Append(" TRANSFERENCIA = :TRANSFERENCIA, ");
                query.Append(" COMISION = :COMISION, ");
                query.Append(" RETENCIONFTE = :RETENCIONFTE, ");
                query.Append(" RETENCIONIVA = :RETENCIONIVA, ");
                query.Append(" CESTADO = :CESTADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, obj.FAUTORIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIOAUTORIZACION", OracleDbType.Varchar2, obj.CUSUARIOAUTORIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSFERENCIA", OracleDbType.Varchar2, obj.TRANSFERENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COMISION", OracleDbType.Varchar2, obj.COMISION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RETENCIONFTE", OracleDbType.Varchar2, obj.RETENCIONFTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RETENCIONIVA", OracleDbType.Varchar2, obj.RETENCIONIVA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, obj.CCONVENIO, ParameterDirection.Input));

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

        public bool Autorizar(TPOSCOMPENSACABECERA obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" FAUTORIZACION = :FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION = :CUSUARIOAUTORIZACION, ");
                query.Append(" CESTADO = :CESTADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, obj.FAUTORIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIOAUTORIZACION", OracleDbType.Varchar2, obj.CUSUARIOAUTORIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, "AUT", ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, obj.CCONVENIO, ParameterDirection.Input));

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

        public bool ActualizarAutorizar(VPOSCOMPENSACABECERA obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" ERROR = :ERROR, ");
                query.Append(" CESTADO = :CESTADO ");
                query.Append(" WHERE CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO = 'AUT' ");
                query.Append(" AND FAUTORIZACION = :FAUTORIZACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, obj.CCONVENIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, obj.FAUTORIZACION, ParameterDirection.Input));

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

        public bool ActualizarFinalizado(DateTime? fproceso, Int32? cconvenio, string descripcionError)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" CESTADO = 'FIN', ");
                query.Append(" ERROR = :ERROR ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, descripcionError, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, cconvenio, ParameterDirection.Input));

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

        public bool ActualizarReferenciaComision(Int32? convenio, DateTime fautorizacion, string referenciaDebito)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" COMISION = :COMISION ");
                query.Append(" WHERE CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO = 'AUT' ");
                query.Append(" AND FAUTORIZACION = :FAUTORIZACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("COMISION", OracleDbType.Varchar2, referenciaDebito, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, convenio, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, fautorizacion, ParameterDirection.Input));

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

        public bool ActualizarReferenciaRetencionFTE(Int32? convenio, DateTime fautorizacion, string referenciaDebito)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" RETENCIONFTE = :RETENCIONFTE ");
                query.Append(" WHERE CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO = 'AUT' ");
                query.Append(" AND FAUTORIZACION = :FAUTORIZACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("RETENCIONFTE", OracleDbType.Varchar2, referenciaDebito, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, convenio, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, fautorizacion, ParameterDirection.Input));

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

        public bool ActualizarReferenciaRetencionIVA(Int32? convenio, DateTime fautorizacion, string referenciaDebito)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" RETENCIONIVA = :RETENCIONIVA ");
                query.Append(" WHERE CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO = 'AUT' ");
                query.Append(" AND FAUTORIZACION = :FAUTORIZACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("RETENCIONIVA", OracleDbType.Varchar2, referenciaDebito, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, convenio, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, fautorizacion, ParameterDirection.Input));

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

        public bool ActualizarReferenciaTransferencia(Int32? convenio, DateTime fautorizacion, string referenciaTransferencia)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSACABECERA SET ");
                query.Append(" TRANSFERENCIA = :TRANSFERENCIA ");
                query.Append(" WHERE CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO = 'AUT' ");
                query.Append(" AND FAUTORIZACION = :FAUTORIZACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("TRANSFERENCIA", OracleDbType.Varchar2, referenciaTransferencia, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, convenio, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, fautorizacion, ParameterDirection.Input));

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

        public TPOSCOMPENSACABECERA Listar(DateTime? fecha, Int32? convenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TPOSCOMPENSACABECERA obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" TRANSFERENCIA, ");
                query.Append(" COMISION, ");
                query.Append(" RETENCIONFTE, ");
                query.Append(" RETENCIONIVA, ");
                query.Append(" CESTADO ");
                query.Append(" FROM TPOSCOMPENSACABECERA ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fecha, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, convenio, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TPOSCOMPENSACABECERA
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
                            ERROR = reader["ERROR"].ToString(),
                            TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
                            COMISION = reader["COMISION"].ToString(),
                            RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
                            RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
                            CESTADO = reader["CESTADO"].ToString()
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

        //query.Append(" FPROCESO, ");
        //query.Append(" CCONVENIO, ");
        //query.Append(" FAUTORIZACION, ");
        //query.Append(" CUSUARIOAUTORIZACION, ");
        //query.Append(" ERROR, ");
        //query.Append(" TRANSFERENCIA, ");
        //query.Append(" COMISION, ");
        //query.Append(" RETENCIONFTE, ");
        //query.Append(" RETENCIONIVA, ");
        //query.Append(" CESTADO ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
        //FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
        //CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
        //ERROR = reader["ERROR"].ToString(),
        //TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
        //COMISION = reader["COMISION"].ToString(),
        //RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
        //RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
        //CESTADO = reader["CESTADO"].ToString(),

    }
}
