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
    public class BddAuxiliar
    {
        public DateTime FechaContable()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            DateTime fecha = DateTime.Now;

            try
            {
                #region armaComando

                query.Append(" SELECT MAX (FCONTABLE) FECHA ");
                query.Append(" FROM TSUCURSALFECHACONTABLE ");
                query.Append(" WHERE FHASTA = TO_DATE ('31/12/2999', 'DD/MM/YYYY') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        fecha = Convert.ToDateTime(reader["FECHA"].ToString());
                    }
                }
                else
                {
                    fecha = DateTime.Now;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                fecha = DateTime.Now;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return fecha;
        }

        public decimal ValorPendienteCredito(string ccuenta, string origen)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            decimal valorpendiente = 0;

            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SPCREDITOCALCULAVALORPENDIENTE";

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "VCREDITO";
                par.Value = ccuenta;
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "VORIGEN";
                par.Value = origen;
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "VPENDIENTE";
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Decimal;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    valorpendiente = Convert.ToDecimal(comando.Parameters["VPENDIENTE"].Value.ToString().Replace(",", "").Replace(".", ","));
                }

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                valorpendiente = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return valorpendiente;
        }

        public void CargaSifco(string fecha, string proceso, out string error, out Int32 registrosCorrectos, out Int32 registrosError)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            error = "OK";
            registrosCorrectos = 0;
            registrosError = 0;
            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "BATCHCARGASIFCO";

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "FECHAPROCESO";
                par.Value = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CPROCESO";
                par.Value = proceso;
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERROR";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CORRECTOS";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERRORES";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    error = comando.Parameters["ERROR"].Value.ToString();
                    registrosCorrectos = Convert.ToInt32(comando.Parameters["CORRECTOS"].Value.ToString());
                    registrosError = Convert.ToInt32(comando.Parameters["ERRORES"].Value.ToString());
                }
                else
                {
                    error = "ERROR EJECUTANDO PROCEDIMIENTO";
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public void CargaDesbloqueos(string fecha, string usuario, out string proceso, out string error, out Int32 registrosCorrectos, out Int32 registrosError)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            error = "";
            proceso = "";
            registrosCorrectos = 0;
            registrosError = 0;

            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "BATCHCARGADESBLOQUEOS";

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "FPROCESO";
                par.Value = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CUSUARIO";
                par.Value = usuario;
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "NUMEROPROCESO";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERROR";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CORRECTOS";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERRORES";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    proceso = comando.Parameters["NUMEROPROCESO"].Value.ToString();
                    error = comando.Parameters["ERROR"].Value.ToString();
                    registrosCorrectos = Convert.ToInt32(comando.Parameters["CORRECTOS"].Value.ToString());
                    registrosError = Convert.ToInt32(comando.Parameters["ERRORES"].Value.ToString());
                }
                else
                {
                    error = "ERROR EJECUTANDO PROCEDIMIENTO";
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                error = Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public void CargaCobrosSifco(string fecha, string usuario, out string proceso, out string error, out Int32 registrosCorrectos, out Int32 registrosError)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            error = "";
            proceso = "";
            registrosCorrectos = 0;
            registrosError = 0;

            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "BATCHCARGACOBROSSIFCO";

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "FPROCESO";
                par.Value = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CUSUARIO";
                par.Value = usuario;
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "NUMEROPROCESO";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERROR";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CORRECTOS";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERRORES";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    proceso = comando.Parameters["NUMEROPROCESO"].Value.ToString();
                    error = comando.Parameters["ERROR"].Value.ToString();
                    registrosCorrectos = Convert.ToInt32(comando.Parameters["CORRECTOS"].Value.ToString());
                    registrosError = Convert.ToInt32(comando.Parameters["ERRORES"].Value.ToString());
                }
                else
                {
                    error = "ERROR EJECUTANDO PROCEDIMIENTO";
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                error = Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public void ConvivenciaFaltante(string fcontable, string fproceso, out string error)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();

            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "PKG_CONVIVENCIA_FIT_SIFCO.ENVIA_CONVIVENCIA_FALTANTE@SIFCO";

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "pFechaContable";
                par.Value = Convert.ToDateTime(fcontable);
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Date;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "PFECHAPROCESO";
                par.Value = Convert.ToDateTime(fcontable);
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Date;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    error = "OK";
                }
                else
                {
                    error = "ERROR EJECUTANDO PROCEDIMIENTO";
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                error = Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public void ConvivenciaSifcoCargaComprobantes(string fecha, out string error)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            int registros = 0;
            error = "OK";

            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "PKG_CONVICONTABLE_SIFCO_FIT.CARGAR_DATOS_SIFCO";
                comando.CommandTimeout = 300;

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "pFecha";
                par.Value = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();

                if (ado.EjecutarComando(ref comando))
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (OracleException oex)
            {
                if (oex.ErrorCode != 20620)
                {
                    error = "ERROR EN SP CARGAR_DATOS_SIFCO: " + oex.ErrorCode + " " + oex.Message;
                }
            }
            catch (Exception ex)
            {
                error = "ERROR EN SP CARGAR_DATOS_SIFCO: " + ex.Message.ToString().ToUpper();
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public void ConvivenciaSifcoConvivencia(string fecha, out string error)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            int registros = 0;
            error = "OK";
            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "PKG_CONVICONTABLE_SIFCO_FIT.CONVIVENCIACONTABLESIFCO_FIT";
                comando.CommandTimeout = 120;

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "pFecha";
                par.Value = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                error = ex.Message.ToString();
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public void ConvivenciaSifcoPasaComprobantes(string fecha, string usuario, out string proceso, out string error, out Int32 registrosCorrectos, out Int32 registrosError)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            int registros = 0;
            error = "OK";
            proceso = "";
            registrosCorrectos = 0;
            registrosError = 0;
            try
            {
                #region armaComando

                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "BATCHCARGACOMPROBANTES";
                comando.CommandTimeout = 120;

                OracleParameter par = null;

                par = new OracleParameter();
                par.ParameterName = "FECHAPROCESO";
                par.Value = Convert.ToDateTime(fecha).ToString("yyyyMMdd");
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CUSUARIO";
                par.Value = usuario;
                par.Direction = ParameterDirection.Input;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CPROCESO";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERROR";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "CORRECTOS";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                par = new OracleParameter();
                par.ParameterName = "ERRORES";
                par.Size = 2000;
                par.Direction = ParameterDirection.Output;
                par.OracleDbType = OracleDbType.Varchar2;
                comando.Parameters.Add(par);

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    registros = 0;
                    proceso = comando.Parameters["CPROCESO"].Value.ToString();
                    error = comando.Parameters["ERROR"].Value.ToString();
                    registrosCorrectos = Convert.ToInt32(comando.Parameters["CORRECTOS"].Value.ToString());
                    registrosError = Convert.ToInt32(comando.Parameters["ERRORES"].Value.ToString());
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                error = "ERROR EN SP BATCHCARGACOMPROBANTES: " + ex.Message.ToString().ToUpper();
            }
            finally
            {
                ado.CerrarConexion();
            }
        }

        public Int32 GetSQBatchProceso()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 secuencia = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT SQ_BHPROCESO.NEXTVAL SECUENCIA FROM DUAL ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        secuencia = Util.ConvertirNumero(reader["SECUENCIA"].ToString()).Value;
                        break;
                    }
                }
                else
                {
                    secuencia = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                secuencia = -1;
            }
            finally
            {
                ado.CerrarConexion();
            }
            return secuencia;
        }

        public DataTable DataEstructurasI01(DateTime fechaCorte)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            DataTable dt = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FECHACORTE, ");
                query.Append(" NUMEROIDDEPOSITO, ");
                query.Append(" TIPOIDEMISRDEPSITARIO, ");
                query.Append(" IDEMISRDEPORITARIO, ");
                query.Append(" FEMISION, ");
                query.Append(" FCOMPRA, ");
                query.Append(" TIPOINSTRUMENTO, ");
                query.Append(" PAISEMISIONDEPOSITARIO, ");
                query.Append(" VALORNOMINAL, ");
                query.Append(" VALORCOMPRA, ");
                query.Append(" PERIOCIDADPAGOCUPON, ");
                query.Append(" CLASIFIEMISODEPOSITARIO, ");
                query.Append(" TIPOEMISORDEPOSITARIO ");
                query.Append(" FROM ESTRUCTURA_I01_INVESTOR ");
                query.Append(" WHERE FECHACORTE = :FECHACORTE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FECHACORTE", OracleDbType.Date, fechaCorte, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    dt = new DataTable();
                    dt.TableName = "DATA";
                    dt.Load(reader);
                }
                else
                {
                    dt = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return dt;
        }

        public DataTable DataEstructurasI02(DateTime fechaCorte)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            DataTable dt = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FECHACORTE, ");
                query.Append(" NUMEROIDDEPOSITO, ");
                query.Append(" TIPOIDEMISRDEPSITARIO, ");
                query.Append(" IDEMISRDEPORITARIO, ");
                query.Append(" FEMISION, ");
                query.Append(" FCOMPRA, ");
                query.Append(" FVENCIMIENTO, ");
                query.Append(" CALIFICACIONRIESGO, ");
                query.Append(" CALIFICADORARIESGO, ");
                query.Append(" FULTIMACALIFICACION, ");
                query.Append(" CCONTABLE, ");
                query.Append(" VLIBRODOLARES, ");
                query.Append(" ESTADOTITULO, ");
                query.Append(" TASANOMINAL, ");
                query.Append(" MONTOINTERES, ");
                query.Append(" CRIESGONORMATIVA, ");
                query.Append(" PROVISIONCONTITUIDA ");
                query.Append(" FROM ESTRUCTURA_I02_INVESTOR ");
                query.Append(" WHERE FECHACORTE = :FECHACORTE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FECHACORTE", OracleDbType.Date, fechaCorte, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    dt = new DataTable();
                    dt.TableName = "DATA";
                    dt.Load(reader);
                }
                else
                {
                    dt = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return dt;
        }

        public DataTable UAFTotales(DateTime fechaCorte, out string error)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            DataTable dt = new DataTable();
            error = "OK";

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" COUNT(*) TRA, ");
                query.Append(" SUM(VDE) TDE, ");
                query.Append(" SUM(VCR) TCR, ");
                query.Append(" SUM(VEF) TEF, ");
                query.Append(" SUM(VCH) TCH, ");
                query.Append(" SUM(VVT) TVT ");
                query.Append(" FROM TRANSACCIONES_UAF_XML@SIFCO ");
                query.Append(" WHERE FCT = :FCORTE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCORTE", OracleDbType.Date, fechaCorte, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    dt.TableName = "DATA";
                    dt.Load(reader);
                }
                else
                {
                    dt = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                error = ex.Message.ToString().ToUpper();
            }
            finally
            {
                ado.CerrarConexion();
            }
            return dt;
        }

        public DataTable UAFClientes(DateTime fechaCorte, out string error)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            DataTable dt = new DataTable();
            error = "OK";

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" TID, ");
                query.Append(" IDE, ");
                query.Append(" NRS, ");
                query.Append(" NAC, ");
                query.Append(" DIR, ");
                query.Append(" CCC, ");
                query.Append(" AEC, ");
                query.Append(" IMT, ");
                query.Append(" CDR, ");
                query.Append(" TO_CHAR(FCT, 'YYYYMMDD') FCT ");
                query.Append(" FROM CLIENTES_UAF_XML@SIFCO ");
                query.Append(" WHERE FCT = :FCORTE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCORTE", OracleDbType.Date, fechaCorte, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    dt.TableName = "DATA";
                    dt.Load(reader);
                }
                else
                {
                    dt = null;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                error = ex.Message.ToString().ToUpper();
            }
            finally
            {
                ado.CerrarConexion();
            }
            return dt;
        }

        public Int32 EjcutaQuery(string query, out string error)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            int registros = 0;
            error = "OK";
            try
            {
                #region armaComando

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                if (ado.EjecutarComando(ref comando))
                {
                    registros = -9;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                error = ex.Message.ToString().ToUpper();
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public decimal GetIva()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            decimal response = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" MAX (PORCENTAJE) IVA ");
                query.Append(" FROM TTARIFARIOPRODUCTO ");
                query.Append(" WHERE FHASTA = :FHASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Util.FHasta(), ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response = Util.ConvertirDecimal(reader["IVA"].ToString()).Value;
                        break;
                    }
                }
                else
                {
                    response = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                response = 0;
            }
            finally
            {
                ado.CerrarConexion();
            }
            return response;
        }

        public decimal GetComisionSPI()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            decimal response = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT UNIQUE (COBROMAXIMO) VALOR ");
                query.Append(" FROM TTARIFARIOPRODUCTO ");
                query.Append(" WHERE CSUBSISTEMA = '04' ");
                query.Append(" AND CTRANSACCION = '7178' ");
                query.Append(" AND RUBRO IN (1104) ");
                query.Append(" AND FHASTA = :FHASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Util.FHasta(), ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        response = Util.ConvertirDecimal(reader["VALOR"].ToString()).Value;
                        break;
                    }
                }
                else
                {
                    response = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                response = 0;
            }
            finally
            {
                ado.CerrarConexion();
            }
            return response;
        }

        public bool ActualizarCuotaSifco(DateTime fproceso, string cuenta, string credito, string observacion, string respuesta, Decimal valorCobrado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE T06CUOTASXCOBRAR@SIFCO SET ");
                query.Append(" OBSERVACION = :OBSERVACION, ");
                query.Append(" RESPUESTA = :RESPUESTA, ");
                query.Append(" COBRADO = :COBRADO ");
                query.Append(" WHERE CUENTA = :CUENTA ");
                query.Append(" AND CREDITO = :CREDITO ");
                query.Append(" AND FECHA_PROC = :FPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("OBSERVACION", OracleDbType.Varchar2, observacion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RESPUESTA", OracleDbType.Varchar2, respuesta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COBRADO", OracleDbType.Decimal, valorCobrado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUENTA", OracleDbType.Varchar2, cuenta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CREDITO", OracleDbType.Varchar2, credito, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));

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
