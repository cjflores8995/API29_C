using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business
{
    public class TBTHDETALLEPROCESO
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public Int32? SECUENCIA { get; set; }
        public String CTIPOTRANSACCION { get; set; }
        public String CUSUARIO { get; set; }
        public Int32? CSUCURSAL { get; set; }
        public Int32? COFICINA { get; set; }
        public Int32? CPERSONA { get; set; }
        public String IDENTIFICACION { get; set; }
        public String CUENTA { get; set; }
        public Int32? RUBRO { get; set; }
        public Decimal? VALOR { get; set; }
        public Decimal? VALORPENDIENTE { get; set; }
        public Decimal? VALORMOVIMIENTO { get; set; }
        public String REFERENCIA { get; set; }
        public String CCONCEPTO { get; set; }
        public Int32? SECUENCIABLQ { get; set; }
        public String PROCESA { get; set; }
        public String DEBITOTOTAL { get; set; }
        public String DESCRIPCIONMOVIMIENTO { get; set; }
        public String PARAMETROS { get; set; }
        public String NUMEROMENSAJE_ORIGINAL { get; set; }
        public String CSUBSISTEMA_ORIGINAL { get; set; }
        public String CTRANSACCION_ORIGINAL { get; set; }
        public String CUSUARIO_ORIGINAL { get; set; }
        public String NUMEROMENSAJE { get; set; }
        public String CESTADO { get; set; }
        public String CERROR { get; set; }
        public String DERROR { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FACTUALIZACION { get; set; }
        public String SERVIDOR { get; set; }
        public Double? TIEMPOCORE { get; set; }
        public Double? TIEMPOTOTAL { get; set; }

        public TBTHTIPOTRANSACCION transaccion { get; set; }

        public String detailEntrada { get; set; }
        public String detailSalida { get; set; }

        public ThreadLocal<Stopwatch> timeCore { get; set; }
        public ThreadLocal<Stopwatch> timeTotal { get; set; }

        #endregion variables

        #region metodos

        public List<TBTHDETALLEPROCESO> ListarTransaccionesPersona(TBTHDETALLEPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLEPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" REFERENCIA, ");
                query.Append(" CCONCEPTO, ");
                query.Append(" SECUENCIABLQ, ");
                query.Append(" PROCESA, ");
                query.Append(" PARAMETROS, ");
                query.Append(" DESCRIPCIONMOVIMIENTO, ");
                query.Append(" NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" CSUBSISTEMA_ORIGINAL, ");
                query.Append(" CTRANSACCION_ORIGINAL, ");
                query.Append(" CUSUARIO_ORIGINAL, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" SERVIDOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" DEBITOTOTAL ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CPERSONA = :CPERSONA ");
                query.Append(" AND CTIPOTRANSACCION NOT IN ('INGBLQ') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLEPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLEPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            CCONCEPTO = reader["CCONCEPTO"].ToString(),
                            SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
                            PROCESA = reader["PROCESA"].ToString(),
                            DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
                            PARAMETROS = reader["PARAMETROS"].ToString(),
                            DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
                            NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
                            CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
                            CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
                            CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            SERVIDOR = reader["SERVIDOR"].ToString()
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

        public Int32 ContarAcreditacion(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public Int32 ContarTotal(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public Int32 ContarTotal(DateTime? fechaProceso, Int32? CPROCESO, string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CTIPOTRANSACCION = :CTIPOTRANSACCION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, tipo, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public Int32 ContarXEstado(DateTime? fechaProceso, Int32? CPROCESO, string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = :CESTADO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public Int32 ContarFinalizados(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO IN ('TERMIN') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public bool InsertarTabulado(TBTHDETALLEPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLEPROCESO (");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" PROCESA, ");
                query.Append(" CESTADO, ");
                query.Append(" FCARGA) ");

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" :CTIPOTRANSACCION, ");
                query.Append(" :CUSUARIO, ");
                query.Append(" :CSUCURSAL, ");
                query.Append(" :COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" 0, ");
                query.Append(" 1, ");
                query.Append(" :CESTADO, ");
                query.Append(" :FCARGA ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CPERSONA = :CPERSONA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, obj.CTIPOTRANSACCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
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

        public bool InsertarTabuladoFaltantes(TBTHDETALLEPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLEPROCESO (");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" PROCESA, ");
                query.Append(" CESTADO, ");
                query.Append(" FCARGA) ");

                query.Append(" SELECT ");
                query.Append(" A.FPROCESO, ");
                query.Append(" A.CPROCESO, ");
                query.Append(" A.SECUENCIA, ");
                query.Append(" :CTIPOTRANSACCION, ");
                query.Append(" :CUSUARIO, ");
                query.Append(" :CSUCURSAL, ");
                query.Append(" :COFICINA, ");
                query.Append(" A.CPERSONA, ");
                query.Append(" A.IDENTIFICACION, ");
                query.Append(" A.CUENTA, ");
                query.Append(" A.RUBRO, ");
                query.Append(" A.VALOR, ");
                query.Append(" 0, ");
                query.Append(" 1, ");
                query.Append(" :CESTADO, ");
                query.Append(" :FCARGA ");
                query.Append(" FROM TBTHDETALLETABULADO A ");
                query.Append(" WHERE A.FPROCESO = :FPROCESO ");
                query.Append(" AND A.CPROCESO = :CPROCESO ");
                query.Append(" AND A.CESTADO = 'TER' ");
                query.Append(" AND NOT EXISTS (SELECT 'X' ");
                query.Append("                   FROM TBTHDETALLEPROCESO ");
                query.Append("                  WHERE FPROCESO = A.FPROCESO ");
                query.Append("                    AND CPROCESO = A.CPROCESO ");
                query.Append("                    AND SECUENCIA = A.SECUENCIA) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, obj.CTIPOTRANSACCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));

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

        public bool InsertarTabuladoBloqueos(DateTime? fechaProceso, Int32? CPROCESO, Int32 secuencia)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLEPROCESO ( ");

                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" REFERENCIA, ");
                query.Append(" CCONCEPTO, ");
                query.Append(" SECUENCIABLQ, ");
                query.Append(" PROCESA, ");
                query.Append(" DEBITOTOTAL, ");
                query.Append(" DESCRIPCIONMOVIMIENTO, ");
                query.Append(" PARAMETROS, ");
                query.Append(" NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" CSUBSISTEMA_ORIGINAL, ");
                query.Append(" CTRANSACCION_ORIGINAL, ");
                query.Append(" CUSUARIO_ORIGINAL, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" SERVIDOR ");

                query.Append(" ) ");

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" (ROWNUM + :SECUENCIA) SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" REFERENCIA, ");
                query.Append(" CCONCEPTO, ");
                query.Append(" SECUENCIABLQ, ");
                query.Append(" PROCESA, ");
                query.Append(" DEBITOTOTAL, ");
                query.Append(" DESCRIPCIONMOVIMIENTO, ");
                query.Append(" PARAMETROS, ");
                query.Append(" NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" CSUBSISTEMA_ORIGINAL, ");
                query.Append(" CTRANSACCION_ORIGINAL, ");
                query.Append(" CUSUARIO_ORIGINAL, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" SERVIDOR ");
                query.Append(" FROM TBTHTEMP ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, secuencia, ParameterDirection.Input));
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

        public bool Actualizar(TBTHDETALLEPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
                query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" VALORPENDIENTE = :VALORPENDIENTE, ");
                query.Append(" VALORMOVIMIENTO = :VALORMOVIMIENTO, ");
                query.Append(" CERROR = :CERROR, ");
                query.Append(" DERROR = :DERROR, ");
                query.Append(" FACTUALIZACION = :FACTUALIZACION, ");
                query.Append(" SERVIDOR = :SERVIDOR, ");
                query.Append(" TIEMPOCORE = :TIEMPOCORE, ");
                query.Append(" TIEMPOTOTAL = :TIEMPOTOTAL ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND SECUENCIA = :SECUENCIA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALORPENDIENTE", OracleDbType.Decimal, obj.VALORPENDIENTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALORMOVIMIENTO", OracleDbType.Decimal, obj.VALORMOVIMIENTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, DateTime.Now, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SERVIDOR", OracleDbType.Varchar2, obj.SERVIDOR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TIEMPOCORE", OracleDbType.Double, obj.TIEMPOCORE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TIEMPOTOTAL", OracleDbType.Double, obj.TIEMPOTOTAL, ParameterDirection.Input));
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

        #region procesos

        public bool ActualizarPendientes(Int32 cproceso, string criterios)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
                query.Append(" CESTADO = 'PENPRO' ");
                query.Append(" WHERE CESTADO = 'CARGAD' ");
                query.Append(" AND PROCESA = '1' ");
                query.Append(criterios);
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

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

        public Int32 ContarPendientes(Int32 cproceso, string criterios)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE CESTADO = 'PENPRO' ");
                query.Append(criterios);
                query.Append(" AND PROCESA = '1' ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" ORDER BY FPROCESO, CPROCESO, CPERSONA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Convert.ToInt32(reader["REGISTROS"].ToString());
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public List<TBTHDETALLEPROCESO> ListarPendientes(Int32 cproceso, string criterios, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLEPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" REFERENCIA, ");
                query.Append(" CCONCEPTO, ");
                query.Append(" SECUENCIABLQ, ");
                query.Append(" PROCESA, ");
                query.Append(" PARAMETROS, ");
                query.Append(" DESCRIPCIONMOVIMIENTO, ");
                query.Append(" NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" CSUBSISTEMA_ORIGINAL, ");
                query.Append(" CTRANSACCION_ORIGINAL, ");
                query.Append(" CUSUARIO_ORIGINAL, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" SERVIDOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" DEBITOTOTAL ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE CESTADO = 'PENPRO' ");
                query.Append(criterios);
                query.Append(" AND PROCESA = '1' ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" ORDER BY FPROCESO, CPROCESO, CPERSONA) ");
                query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLEPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLEPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            CCONCEPTO = reader["CCONCEPTO"].ToString(),
                            SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
                            PROCESA = reader["PROCESA"].ToString(),
                            DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
                            PARAMETROS = reader["PARAMETROS"].ToString(),
                            DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
                            NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
                            CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
                            CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
                            CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            SERVIDOR = reader["SERVIDOR"].ToString()
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

        public List<TBTHDETALLEPROCESO> ListarPendientes(Int32 cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLEPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" REFERENCIA, ");
                query.Append(" CCONCEPTO, ");
                query.Append(" SECUENCIABLQ, ");
                query.Append(" PROCESA, ");
                query.Append(" PARAMETROS, ");
                query.Append(" DESCRIPCIONMOVIMIENTO, ");
                query.Append(" NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" CSUBSISTEMA_ORIGINAL, ");
                query.Append(" CTRANSACCION_ORIGINAL, ");
                query.Append(" CUSUARIO_ORIGINAL, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" SERVIDOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" DEBITOTOTAL, ");
                query.Append(" VALORMOVIMIENTO ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLEPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLEPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            CCONCEPTO = reader["CCONCEPTO"].ToString(),
                            SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
                            PROCESA = reader["PROCESA"].ToString(),
                            DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
                            PARAMETROS = reader["PARAMETROS"].ToString(),
                            DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
                            NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
                            CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
                            CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
                            CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            SERVIDOR = reader["SERVIDOR"].ToString(),
                            VALORMOVIMIENTO = Util.ConvertirDecimal(reader["VALORMOVIMIENTO"].ToString())
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

        #endregion procesos

        #region recuperacion

        public bool ActualizarPendientesRecuperacion(DateTime? fproceso, Int32? cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armacomando

                query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
                query.Append(" CESTADO = 'PENPRO' ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = 'CARGAD' ");
                query.Append(" AND PROCESA = '1' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                resp = ado.EjecutarComando(comando);

                #endregion ejecuta comando
            }
            catch (OracleException ox)
            {
                int error = ox.ErrorCode;
                resp = false;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ox, "ERR");
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

        public Int32 ContarPendientesRecuperacion(DateTime? fproceso, Int32? cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = 'PENPRO' ");
                query.Append(" AND PROCESA = '1' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public List<TBTHDETALLEPROCESO> ListarPendientesRecuperacion(DateTime? fproceso, Int32? cproceso, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLEPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" CUSUARIO, ");
                query.Append(" CSUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CUENTA, ");
                query.Append(" RUBRO, ");
                query.Append(" VALOR, ");
                query.Append(" REFERENCIA, ");
                query.Append(" CCONCEPTO, ");
                query.Append(" SECUENCIABLQ, ");
                query.Append(" PROCESA, ");
                query.Append(" PARAMETROS, ");
                query.Append(" DESCRIPCIONMOVIMIENTO, ");
                query.Append(" NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" CSUBSISTEMA_ORIGINAL, ");
                query.Append(" CTRANSACCION_ORIGINAL, ");
                query.Append(" CUSUARIO_ORIGINAL, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" SERVIDOR, ");
                query.Append(" VALORPENDIENTE, ");
                query.Append(" VALORMOVIMIENTO, ");
                query.Append(" DEBITOTOTAL ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = 'PENPRO' ");
                query.Append(" AND PROCESA = '1') ");
                query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLEPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLEPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
                            VALORMOVIMIENTO = Util.ConvertirDecimal(reader["VALORMOVIMIENTO"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            CCONCEPTO = reader["CCONCEPTO"].ToString(),
                            SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
                            PROCESA = reader["PROCESA"].ToString(),
                            DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
                            PARAMETROS = reader["PARAMETROS"].ToString(),
                            DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
                            NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
                            CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
                            CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
                            CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            SERVIDOR = reader["SERVIDOR"].ToString()
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

        #endregion recuperacion

        #region activabloqueos

        public bool ActualizarActivaBloqueos(TBTHDETALLEPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armacomando

                query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
                query.Append(" PROCESA = :PROCESA, ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" CERROR = :CERROR, ");
                query.Append(" DERROR = :DERROR, ");
                query.Append(" FACTUALIZACION = :FACTUALIZACION ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CPERSONA = :CPERSONA ");
                query.Append(" AND CTIPOTRANSACCION IN ('INGBLQ') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("PROCESA", OracleDbType.Varchar2, obj.PROCESA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, DateTime.Now, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));

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

        public Int32 ContarActivaBloqueos(DateTime? fproceso, Int32? cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS FROM ( ");
                query.Append(" SELECT FPROCESO, CPROCESO, CPERSONA ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE CTIPOTRANSACCION = 'INGBLQ' ");
                query.Append(" AND CESTADO = 'CARGAD' ");
                query.Append(" AND FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        public List<TBTHDETALLEPROCESO> ListarActivaBloqueos(DateTime? fproceso, Int32? cproceso, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLEPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CPERSONA ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE CTIPOTRANSACCION = 'INGBLQ' ");
                query.Append(" AND CESTADO = 'CARGAD' ");
                query.Append(" AND FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA) ");
                query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLEPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLEPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString())
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

        #endregion activabloqueos

        #region verificacion

        public List<TBTHDETALLEPROCESO> ListarPendientesVerificar(DateTime? fproceso, Int32? cproceso, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLEPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT * FROM ( SELECT ROWNUM NUM, A.* FROM ( ");
                query.Append(" SELECT * ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE CESTADO IN ('VERMOV', 'PROFIT') ");
                query.Append(" AND FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" ORDER BY FPROCESO ASC, CPROCESO ASC, SECUENCIA ASC ");
                query.Append(" ) A) B ");
                query.Append(" WHERE NUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLEPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLEPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
                            VALORMOVIMIENTO = Util.ConvertirDecimal(reader["VALORMOVIMIENTO"].ToString()),
                            REFERENCIA = reader["REFERENCIA"].ToString(),
                            CCONCEPTO = reader["CCONCEPTO"].ToString(),
                            SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
                            PROCESA = reader["PROCESA"].ToString(),
                            DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
                            DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
                            PARAMETROS = reader["PARAMETROS"].ToString(),
                            NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
                            CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
                            CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
                            CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            SERVIDOR = reader["SERVIDOR"].ToString(),
                            TIEMPOCORE = Util.ConvertirDouble(reader["TIEMPOCORE"].ToString()),
                            TIEMPOTOTAL = Util.ConvertirDouble(reader["TIEMPOTOTAL"].ToString())
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

        #endregion verificacion

        #region mantenimiento tarjetas

        public Int32 ContarRegistrosFecha(DateTime? fechaProceso, Int32? CPROCESO, string fecha)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLEPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND REFERENCIA LIKE :FECHA || '/%' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FECHA", OracleDbType.Varchar2, fecha, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando
            }
            catch (Exception ex)
            {
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return registros;
        }

        #endregion mantenimiento tarjetas

        #endregion metodos

        #region metodos a eliminar

        //public bool Insertar(TBTHDETALLEPROCESO obj)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armaComando

        //        query.Append(" INSERT INTO TBTHDETALLEPROCESO (");
        //        query.Append(" FPROCESO, ");
        //        query.Append(" CPROCESO, ");
        //        query.Append(" SECUENCIA, ");
        //        query.Append(" CTIPOTRANSACCION, ");
        //        query.Append(" CUSUARIO, ");
        //        query.Append(" CSUCURSAL, ");
        //        query.Append(" COFICINA, ");
        //        query.Append(" CPERSONA, ");
        //        query.Append(" IDENTIFICACION, ");
        //        query.Append(" CUENTA, ");
        //        query.Append(" RUBRO, ");
        //        query.Append(" VALOR, ");
        //        query.Append(" VALORPENDIENTE, ");
        //        query.Append(" REFERENCIA, ");
        //        query.Append(" CCONCEPTO, ");
        //        query.Append(" SECUENCIABLQ, ");
        //        query.Append(" PROCESA, ");
        //        query.Append(" DEBITOTOTAL, ");
        //        query.Append(" DESCRIPCIONMOVIMIENTO, ");
        //        query.Append(" PARAMETROS, ");
        //        query.Append(" NUMEROMENSAJE_ORIGINAL, ");
        //        query.Append(" CSUBSISTEMA_ORIGINAL, ");
        //        query.Append(" CTRANSACCION_ORIGINAL, ");
        //        query.Append(" CUSUARIO_ORIGINAL, ");
        //        query.Append(" NUMEROMENSAJE, ");
        //        query.Append(" CESTADO, ");
        //        query.Append(" CERROR, ");
        //        query.Append(" DERROR, ");
        //        query.Append(" FCARGA, ");
        //        query.Append(" FACTUALIZACION, ");
        //        query.Append(" SERVIDOR ");
        //        query.Append(" ) VALUES ( ");
        //        query.Append(" :FPROCESO, ");
        //        query.Append(" :CPROCESO, ");
        //        query.Append(" :SECUENCIA, ");
        //        query.Append(" :CTIPOTRANSACCION, ");
        //        query.Append(" :CUSUARIO, ");
        //        query.Append(" :CSUCURSAL, ");
        //        query.Append(" :COFICINA, ");
        //        query.Append(" :CPERSONA, ");
        //        query.Append(" :IDENTIFICACION, ");
        //        query.Append(" :CUENTA, ");
        //        query.Append(" :RUBRO, ");
        //        query.Append(" :VALOR, ");
        //        query.Append(" :VALORPENDIENTE, ");
        //        query.Append(" :REFERENCIA, ");
        //        query.Append(" :CCONCEPTO, ");
        //        query.Append(" :SECUENCIABLQ, ");
        //        query.Append(" :PROCESA, ");
        //        query.Append(" :DEBITOTOTAL, ");
        //        query.Append(" :DESCRIPCIONMOVIMIENTO, ");
        //        query.Append(" :PARAMETROS, ");
        //        query.Append(" :NUMEROMENSAJE_ORIGINAL, ");
        //        query.Append(" :CSUBSISTEMA_ORIGINAL, ");
        //        query.Append(" :CTRANSACCION_ORIGINAL, ");
        //        query.Append(" :CUSUARIO_ORIGINAL, ");
        //        query.Append(" :NUMEROMENSAJE, ");
        //        query.Append(" :CESTADO, ");
        //        query.Append(" :CERROR, ");
        //        query.Append(" :DERROR, ");
        //        query.Append(" :FCARGA, ");
        //        query.Append(" :FACTUALIZACION, ");
        //        query.Append(" :SERVIDOR ");
        //        query.Append(" ) ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, obj.CTIPOTRANSACCION, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CUENTA", OracleDbType.Varchar2, obj.CUENTA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("RUBRO", OracleDbType.Int32, obj.RUBRO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("VALOR", OracleDbType.Decimal, obj.VALOR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("VALORPENDIENTE", OracleDbType.Varchar2, obj.VALORPENDIENTE, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CCONCEPTO", OracleDbType.Varchar2, obj.CCONCEPTO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("SECUENCIABLQ", OracleDbType.Int32, obj.SECUENCIABLQ, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("PROCESA", OracleDbType.Varchar2, obj.PROCESA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("DEBITOTOTAL", OracleDbType.Varchar2, obj.DEBITOTOTAL, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("DESCRIPCIONMOVIMIENTO", OracleDbType.Varchar2, obj.DESCRIPCIONMOVIMIENTO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("PARAMETROS", OracleDbType.Varchar2, obj.PARAMETROS, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE_ORIGINAL", OracleDbType.Varchar2, obj.NUMEROMENSAJE_ORIGINAL, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CSUBSISTEMA_ORIGINAL", OracleDbType.Varchar2, obj.CSUBSISTEMA_ORIGINAL, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CTRANSACCION_ORIGINAL", OracleDbType.Varchar2, obj.CTRANSACCION_ORIGINAL, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CUSUARIO_ORIGINAL", OracleDbType.Varchar2, obj.CUSUARIO_ORIGINAL, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, obj.FACTUALIZACION, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("SERVIDOR", OracleDbType.Varchar2, obj.SERVIDOR, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarValorCero()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armaComando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CERROR = '001', ");
        //        query.Append(" DERROR = 'TRANSACCION NO PROCESADA VALOR IGUAL A 0', ");
        //        query.Append(" FACTUALIZACION = SYSDATE, ");
        //        query.Append(" CESTADO = 'TER' ");
        //        query.Append(" WHERE VALOR = 0 ");
        //        query.Append(" AND CESTADO = 'CAR' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarCuentaCero()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armaComando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CERROR = 'GEN001', ");
        //        query.Append(" DERROR = 'CUENTA NO EXISTE', ");
        //        query.Append(" FACTUALIZACION = SYSDATE, ");
        //        query.Append(" CESTADO = 'TER' ");
        //        query.Append(" WHERE CUENTA = '0' ");
        //        query.Append(" AND CESTADO = 'CAR' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarADepurado()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armaComando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CESTADO = 'DEP', ");
        //        query.Append(" FACTUALIZACION = SYSDATE ");
        //        query.Append(" WHERE CESTADO = 'CAR' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarCorrecto(TBTHDETALLEPROCESO obj)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armaComando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CESTADO = 'PEN', ");
        //        query.Append(" CERROR = NULL, ");
        //        query.Append(" DERROR = NULL, ");
        //        query.Append(" PROCESA = '1' ");
        //        query.Append(" WHERE FPROCESO = :FPROCESO ");
        //        query.Append(" AND CPROCESO = :CPROCESO ");
        //        query.Append(" AND CTIPOTRANSACCION = 'INGBLQ' ");
        //        query.Append(" AND REFERENCIA = :REFERENCIA ");
        //        query.Append(" AND CESTADO <> 'TER' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarIncorrecto(TBTHDETALLEPROCESO obj)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armaComando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CERROR = :CERROR, ");
        //        query.Append(" DERROR = :DERROR, ");
        //        query.Append(" CESTADO = :CESTADO, ");
        //        query.Append(" FACTUALIZACION = SYSDATE ");
        //        query.Append(" WHERE FPROCESO = :FPROCESO ");
        //        query.Append(" AND CPROCESO = :CPROCESO ");
        //        query.Append(" AND CTIPOTRANSACCION = 'INGBLQ' ");
        //        query.Append(" AND REFERENCIA = :REFERENCIA ");
        //        query.Append(" AND CESTADO <> 'TER' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarPendientesVerificar()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CESTADO = 'VRP' ");
        //        query.Append(" WHERE CESTADO IN ('VER') ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public Int32 ContarPendientesVerificar()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    Int32 registros = 0;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT COUNT(*) REGISTROS ");
        //        query.Append(" FROM TBTHDETALLEPROCESO ");
        //        query.Append(" WHERE CESTADO = 'VRP' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
        //            }
        //        }
        //        else
        //        {
        //            registros = 0;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        registros = 0;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return registros;
        //}

        //public bool ActualizarActivarBloqueos(DateTime? fechaProceso, Int32? CPROCESO, Int32? cpersona)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" PROCESA = '1', ");
        //        query.Append(" FACTUALIZACION = :FACTUALIZACION ");
        //        query.Append(" WHERE FPROCESO = :FPROCESO ");
        //        query.Append(" AND CPROCESO = :CPROCESO ");
        //        query.Append(" AND CPERSONA = :CPERSONA ");
        //        query.Append(" AND CTIPOTRANSACCION IN ('INGBLQ') ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, DateTime.Now, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, cpersona, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarPendientes()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CESTADO = 'PEN' ");
        //        query.Append(" WHERE CESTADO = 'DEP' ");
        //        query.Append(" AND PROCESA = '1' ");
        //        query.Append(" AND CTIPOTRANSACCION NOT IN ('REVERSO', 'NOTDEBPAR') ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarBloqueo(TBTHDETALLEPROCESO obj)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CESTADO = :CESTADO, ");
        //        query.Append(" CERROR = :CERROR, ");
        //        query.Append(" DERROR = :DERROR ");
        //        query.Append(" WHERE FPROCESO = :FPROCESO ");
        //        query.Append(" AND CPROCESO = :CPROCESO ");
        //        query.Append(" AND CTIPOTRANSACCION = 'INGBLQ' ");
        //        query.Append(" AND REFERENCIA = :REFERENCIA ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarCredito(TBTHDETALLEPROCESO obj)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" CTIPOTRANSACCION = :CTIPOTRANSACCION, ");
        //        query.Append(" REFERENCIA = :REFERENCIA ");
        //        query.Append(" WHERE FPROCESO = :FPROCESO ");
        //        query.Append(" AND CPROCESO = :CPROCESO ");
        //        query.Append(" AND CPERSONA = :CPERSONA ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, obj.CTIPOTRANSACCION, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public bool ActualizarProcesarPersona(DateTime? fechaProceso, Int32? CPROCESO, Int32? cpersona)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    bool resp = false;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" UPDATE TBTHDETALLEPROCESO SET ");
        //        query.Append(" PROCESA = '1', ");
        //        query.Append(" FACTUALIZACION = :FACTUALIZACION ");
        //        query.Append(" WHERE FPROCESO = :FPROCESO ");
        //        query.Append(" AND CPROCESO = :CPROCESO ");
        //        query.Append(" AND CPERSONA = :CPERSONA ");
        //        query.Append(" AND CTIPOTRANSACCION NOT IN ('INGBLQ') ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, DateTime.Now, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, cpersona, ParameterDirection.Input));

        //        #endregion armaComando

        //        #region ejecuta comando

        //        ado.AbrirConexion();
        //        resp = ado.EjecutarComando(comando);

        //        #endregion ejecuta comando
        //    }
        //    catch (Exception ex)
        //    {
        //        resp = false;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return resp;
        //}

        //public List<TBTHDETALLEPROCESO> ListarPendientes(string tipo, int inicio, int final)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    List<TBTHDETALLEPROCESO> ltObj = null;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT ");
        //        query.Append(" FPROCESO, ");
        //        query.Append(" CPROCESO, ");
        //        query.Append(" SECUENCIA, ");
        //        query.Append(" CTIPOTRANSACCION, ");
        //        query.Append(" CUSUARIO, ");
        //        query.Append(" CSUCURSAL, ");
        //        query.Append(" COFICINA, ");
        //        query.Append(" CPERSONA, ");
        //        query.Append(" IDENTIFICACION, ");
        //        query.Append(" CUENTA, ");
        //        query.Append(" RUBRO, ");
        //        query.Append(" VALOR, ");
        //        query.Append(" REFERENCIA, ");
        //        query.Append(" CCONCEPTO, ");
        //        query.Append(" SECUENCIABLQ, ");
        //        query.Append(" PROCESA, ");
        //        query.Append(" PARAMETROS, ");
        //        query.Append(" DESCRIPCIONMOVIMIENTO, ");
        //        query.Append(" NUMEROMENSAJE_ORIGINAL, ");
        //        query.Append(" CSUBSISTEMA_ORIGINAL, ");
        //        query.Append(" CTRANSACCION_ORIGINAL, ");
        //        query.Append(" CUSUARIO_ORIGINAL, ");
        //        query.Append(" NUMEROMENSAJE, ");
        //        query.Append(" CESTADO, ");
        //        query.Append(" CERROR, ");
        //        query.Append(" DERROR, ");
        //        query.Append(" FCARGA, ");
        //        query.Append(" FACTUALIZACION, ");
        //        query.Append(" SERVIDOR, ");
        //        query.Append(" VALORPENDIENTE, ");
        //        query.Append(" DEBITOTOTAL ");
        //        query.Append(" FROM TBTHDETALLEPROCESO ");
        //        query.Append(" WHERE CESTADO = 'PEN' ");
        //        query.Append(" AND ROWNUM BETWEEN " + inicio + " AND " + final + " ");
        //        switch (tipo)
        //        {
        //            case "AFE":
        //                query.Append(" AND FPROCESO >= TRUNC(SYSDATE) - 1 ");
        //                query.Append(" AND CTIPOTRANSACCION NOT IN ('INGBLQ', 'REVERSO', 'NOTDEBPAR') ");
        //                break;
        //            case "BLQ":
        //                query.Append(" AND FPROCESO >= TRUNC(SYSDATE) - 1 ");
        //                query.Append(" AND CTIPOTRANSACCION IN ('INGBLQ') ");
        //                query.Append(" AND PROCESA = '1' ");
        //                break;
        //            case "REC":
        //                query.Append(" AND CTIPOTRANSACCION IN ('NOTDEBPAR') ");
        //                break;
        //        }

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armacomando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            ltObj = new List<TBTHDETALLEPROCESO>();
        //            while (reader.Read())
        //            {
        //                ltObj.Add(new TBTHDETALLEPROCESO
        //                {
        //                    FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //                    CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //                    SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
        //                    CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
        //                    CUSUARIO = reader["CUSUARIO"].ToString(),
        //                    CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
        //                    COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
        //                    CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
        //                    IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //                    CUENTA = reader["CUENTA"].ToString(),
        //                    RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
        //                    VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
        //                    VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
        //                    REFERENCIA = reader["REFERENCIA"].ToString(),
        //                    CCONCEPTO = reader["CCONCEPTO"].ToString(),
        //                    SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
        //                    PROCESA = reader["PROCESA"].ToString(),
        //                    DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
        //                    PARAMETROS = reader["PARAMETROS"].ToString(),
        //                    DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
        //                    NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
        //                    CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
        //                    CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
        //                    CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
        //                    NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
        //                    CESTADO = reader["CESTADO"].ToString(),
        //                    CERROR = reader["CERROR"].ToString(),
        //                    DERROR = reader["DERROR"].ToString(),
        //                    FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //                    FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
        //                    SERVIDOR = reader["SERVIDOR"].ToString()
        //                });
        //            }
        //        }
        //        else
        //        {
        //            ltObj = null;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        ltObj = null;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return ltObj;
        //}

        //public List<TBTHDETALLEPROCESO> ListarPendientes(int desde, int hasta)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    List<TBTHDETALLEPROCESO> ltObj = null;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT * FROM ( ");
        //        query.Append("   SELECT FPROCESO, ");
        //        query.Append("          CPROCESO, ");
        //        query.Append("          CPERSONA ");
        //        query.Append("     FROM TBTHDETALLEPROCESO ");
        //        query.Append("    WHERE CESTADO = 'PEN' ");
        //        query.Append("      AND CTIPOTRANSACCION NOT IN ('REVERSO', 'NOTDEBPAR') ");
        //        query.Append("      AND PROCESA = '1' ");
        //        query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA ) ");
        //        query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

        //        #endregion armacomando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            ltObj = new List<TBTHDETALLEPROCESO>();
        //            while (reader.Read())
        //            {
        //                ltObj.Add(new TBTHDETALLEPROCESO
        //                {
        //                    FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //                    CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //                    CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString())
        //                });
        //            }
        //        }
        //        else
        //        {
        //            ltObj = null;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        ltObj = null;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return ltObj;
        //}

        //public List<TBTHDETALLEPROCESO> ListarPendientesBloqueosPersona(TBTHDETALLEPROCESO obj)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    List<TBTHDETALLEPROCESO> ltObj = null;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT FPROCESO, ");
        //        query.Append("        CPROCESO, ");
        //        query.Append("        SECUENCIA, ");
        //        query.Append("        CTIPOTRANSACCION, ");
        //        query.Append("        CUSUARIO, ");
        //        query.Append("        CSUCURSAL, ");
        //        query.Append("        COFICINA, ");
        //        query.Append("        CPERSONA, ");
        //        query.Append("        IDENTIFICACION, ");
        //        query.Append("        CUENTA, ");
        //        query.Append("        RUBRO, ");
        //        query.Append("        VALOR, ");
        //        query.Append("        REFERENCIA, ");
        //        query.Append("        CCONCEPTO, ");
        //        query.Append("        SECUENCIABLQ, ");
        //        query.Append("        PROCESA, ");
        //        query.Append("        PARAMETROS, ");
        //        query.Append("        DESCRIPCIONMOVIMIENTO, ");
        //        query.Append("        NUMEROMENSAJE_ORIGINAL, ");
        //        query.Append("        CSUBSISTEMA_ORIGINAL, ");
        //        query.Append("        CTRANSACCION_ORIGINAL, ");
        //        query.Append("        CUSUARIO_ORIGINAL, ");
        //        query.Append("        NUMEROMENSAJE, ");
        //        query.Append("        CESTADO, ");
        //        query.Append("        CERROR, ");
        //        query.Append("        DERROR, ");
        //        query.Append("        FCARGA, ");
        //        query.Append("        FACTUALIZACION, ");
        //        query.Append("        SERVIDOR, ");
        //        query.Append("        VALORPENDIENTE, ");
        //        query.Append("        DEBITOTOTAL ");
        //        query.Append("   FROM TBTHDETALLEPROCESO ");
        //        query.Append("  WHERE CESTADO = 'PEN' ");
        //        query.Append("    AND FPROCESO = :FPROCESO ");
        //        query.Append("    AND CPROCESO = :CPROCESO ");
        //        query.Append("    AND CPERSONA = :CPERSONA ");
        //        query.Append("    AND CTIPOTRANSACCION IN ('INGBLQ') ");
        //        query.Append("    AND PROCESA = '1' ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));

        //        #endregion armacomando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            ltObj = new List<TBTHDETALLEPROCESO>();
        //            while (reader.Read())
        //            {
        //                ltObj.Add(new TBTHDETALLEPROCESO
        //                {
        //                    FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //                    CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //                    SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
        //                    CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
        //                    CUSUARIO = reader["CUSUARIO"].ToString(),
        //                    CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
        //                    COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
        //                    CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
        //                    IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //                    CUENTA = reader["CUENTA"].ToString(),
        //                    RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
        //                    VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
        //                    VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
        //                    REFERENCIA = reader["REFERENCIA"].ToString(),
        //                    CCONCEPTO = reader["CCONCEPTO"].ToString(),
        //                    SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
        //                    PROCESA = reader["PROCESA"].ToString(),
        //                    DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
        //                    PARAMETROS = reader["PARAMETROS"].ToString(),
        //                    DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
        //                    NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
        //                    CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
        //                    CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
        //                    CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
        //                    NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
        //                    CESTADO = reader["CESTADO"].ToString(),
        //                    CERROR = reader["CERROR"].ToString(),
        //                    DERROR = reader["DERROR"].ToString(),
        //                    FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //                    FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
        //                    SERVIDOR = reader["SERVIDOR"].ToString()
        //                });
        //            }
        //        }
        //        else
        //        {
        //            ltObj = null;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        ltObj = null;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return ltObj;
        //}

        //public List<TBTHDETALLEPROCESO> ListarBloqueosInactivos(int desde, int hasta)
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    List<TBTHDETALLEPROCESO> ltObj = null;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT * FROM ( ");
        //        query.Append("   SELECT FPROCESO, ");
        //        query.Append("          CPROCESO, ");
        //        query.Append("          CPERSONA ");
        //        query.Append("     FROM TBTHDETALLEPROCESO ");
        //        query.Append("    WHERE CTIPOTRANSACCION IN ('INGBLQ') ");
        //        query.Append("      AND PROCESA = '0' ");
        //        query.Append("      AND CESTADO NOT IN ('TER') ");
        //        query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA ) ");
        //        query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
        //        comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

        //        #endregion armacomando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            ltObj = new List<TBTHDETALLEPROCESO>();
        //            while (reader.Read())
        //            {
        //                ltObj.Add(new TBTHDETALLEPROCESO
        //                {
        //                    FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //                    CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //                    CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString())
        //                });
        //            }
        //        }
        //        else
        //        {
        //            ltObj = null;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        ltObj = null;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return ltObj;
        //}

        //public List<TBTHDETALLEPROCESO> ListarProcesados()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    List<TBTHDETALLEPROCESO> ltObj = null;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT ");
        //        query.Append(" FPROCESO, ");
        //        query.Append(" CPROCESO, ");
        //        query.Append(" SECUENCIA, ");
        //        query.Append(" CTIPOTRANSACCION, ");
        //        query.Append(" CUENTA, ");
        //        query.Append(" REFERENCIA, ");
        //        query.Append(" NUMEROMENSAJE, ");
        //        query.Append(" CESTADO, ");
        //        query.Append(" CERROR, ");
        //        query.Append(" DERROR, ");
        //        query.Append(" FACTUALIZACION ");
        //        query.Append(" FROM TBTHDETALLEPROCESO ");
        //        query.Append(" WHERE CESTADO IN ('PRF', 'PRC') ");
        //        query.Append(" AND (CERROR IN ('907', 'CON001') OR CERROR IS NULL) ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            ltObj = new List<TBTHDETALLEPROCESO>();
        //            while (reader.Read())
        //            {
        //                ltObj.Add(new TBTHDETALLEPROCESO
        //                {
        //                    FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //                    CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //                    SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
        //                    CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
        //                    CUENTA = reader["CUENTA"].ToString(),
        //                    REFERENCIA = reader["REFERENCIA"].ToString(),
        //                    NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
        //                    CESTADO = reader["CESTADO"].ToString(),
        //                    CERROR = reader["CERROR"].ToString(),
        //                    DERROR = reader["DERROR"].ToString(),
        //                    FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString())
        //                });
        //            }
        //        }
        //        else
        //        {
        //            ltObj = null;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        ltObj = null;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return ltObj;
        //}

        //public Int32 ContarBloqueosInactivos()
        //{
        //    AccesoDatosOracle ado = new AccesoDatosOracle();
        //    OracleCommand comando = new OracleCommand();
        //    StringBuilder query = new StringBuilder();
        //    Int32 registros = 0;

        //    try
        //    {
        //        #region armacomando

        //        query.Append(" SELECT COUNT(*) REGISTROS FROM ( ");
        //        query.Append("   SELECT FPROCESO, ");
        //        query.Append("          CPROCESO, ");
        //        query.Append("          CPERSONA ");
        //        query.Append("     FROM TBTHDETALLEPROCESO ");
        //        query.Append("    WHERE CTIPOTRANSACCION IN ('INGBLQ') ");
        //        query.Append("      AND PROCESA = '0' ");
        //        query.Append("      AND CESTADO NOT IN ('TER') ");
        //        query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA) ");

        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = query.ToString();

        //        #endregion armaComando

        //        #region ejecutaComando

        //        ado.AbrirConexion();
        //        OracleDataReader reader = ado.EjecutarSentencia(comando);

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
        //            }
        //        }
        //        else
        //        {
        //            registros = 0;
        //        }

        //        #endregion ejecutaComando
        //    }
        //    catch (Exception ex)
        //    {
        //        registros = 0;
        //        Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
        //    }
        //    finally
        //    {
        //        ado.CerrarConexion();
        //    }
        //    return registros;
        //}

        #endregion metodos a eliminar
    }
}
