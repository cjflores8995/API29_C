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
    public class TBTHDETALLETABULADO
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public Int32? SECUENCIA { get; set; }
        public String CESTADO { get; set; }
        public Int32? CPERSONA { get; set; }
        public String IDENTIFICACION { get; set; }
        public String CSUBSISTEMA { get; set; }
        public String CUENTA { get; set; }
        public String CREDITO { get; set; }
        public Decimal? VALOR { get; set; }
        public Int32? RUBRO { get; set; }
        public String CODIGORECHAZO { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FACTUALIZACION { get; set; }
        public String ERROR { get; set; }
        public String ORDREFERENCIA { get; set; }
        public String ORDCUENTA { get; set; }
        public String ORDNOMBRE { get; set; }
        public String ORDCUENTABCE { get; set; }
        public String RCPCUENTABCE { get; set; }
        public String LINEA { get; set; }

        public TBTHPROCESO tbthproceso { get; set; }
        public VBTHPROCESO vbthproceso { get; set; }

        #endregion variables

        #region metodos

        public TBTHDETALLETABULADO Insertar(TBTHDETALLETABULADO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHDETALLETABULADO ( ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CUENTA, ");
                query.Append(" CREDITO, ");
                query.Append(" VALOR, ");
                query.Append(" RUBRO, ");
                query.Append(" CODIGORECHAZO, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" CESTADO, ");
                query.Append(" ORDREFERENCIA, ");
                query.Append(" ORDCUENTA, ");
                query.Append(" ORDNOMBRE, ");
                query.Append(" ORDCUENTABCE, ");
                query.Append(" RCPCUENTABCE, ");
                query.Append(" LINEA ");
                query.Append(" ) VALUES ( ");
                query.Append(" :FPROCESO, ");
                query.Append(" :CPROCESO, ");
                query.Append(" :SECUENCIA, ");
                query.Append(" :CPERSONA, ");
                query.Append(" :IDENTIFICACION, ");
                query.Append(" :CSUBSISTEMA, ");
                query.Append(" :CUENTA, ");
                query.Append(" :CREDITO, ");
                query.Append(" :VALOR, ");
                query.Append(" :RUBRO, ");
                query.Append(" :CODIGORECHAZO, ");
                query.Append(" :FCARGA, ");
                query.Append(" :FACTUALIZACION, ");
                query.Append(" :CESTADO, ");
                query.Append(" :ORDREFERENCIA, ");
                query.Append(" :ORDCUENTA, ");
                query.Append(" :ORDNOMBRE, ");
                query.Append(" :ORDCUENTABCE, ");
                query.Append(" :RCPCUENTABCE, ");
                query.Append(" :LINEA ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUBSISTEMA", OracleDbType.Varchar2, obj.CSUBSISTEMA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUENTA", OracleDbType.Varchar2, obj.CUENTA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CREDITO", OracleDbType.Varchar2, obj.CREDITO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALOR", OracleDbType.Decimal, obj.VALOR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RUBRO", OracleDbType.Int32, obj.RUBRO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CODIGORECHAZO", OracleDbType.Varchar2, obj.CODIGORECHAZO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, obj.FACTUALIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ORDREFERENCIA", OracleDbType.Varchar2, obj.ORDREFERENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ORDCUENTA", OracleDbType.Varchar2, obj.ORDCUENTA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ORDNOMBRE", OracleDbType.Varchar2, obj.ORDNOMBRE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ORDCUENTABCE", OracleDbType.Varchar2, obj.ORDCUENTABCE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RCPCUENTABCE", OracleDbType.Varchar2, obj.RCPCUENTABCE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("LINEA", OracleDbType.Varchar2, obj.LINEA, ParameterDirection.Input));

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
            return obj;
        }

        public bool Actualizar(TBTHDETALLETABULADO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TBTHDETALLETABULADO SET ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" CPERSONA = :CPERSONA, ");
                query.Append(" CSUBSISTEMA = :CSUBSISTEMA, ");
                query.Append(" RUBRO = :RUBRO, ");
                query.Append(" CODIGORECHAZO = :CODIGORECHAZO, ");
                query.Append(" FACTUALIZACION = :FACTUALIZACION, ");
                query.Append(" ERROR = :ERROR ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND SECUENCIA = :SECUENCIA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUBSISTEMA", OracleDbType.Varchar2, obj.CSUBSISTEMA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RUBRO", OracleDbType.Int32, obj.RUBRO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CODIGORECHAZO", OracleDbType.Varchar2, obj.CODIGORECHAZO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, obj.FACTUALIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
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

        public bool ActualizarPersona(TBTHDETALLETABULADO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TBTHDETALLETABULADO SET ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" CPERSONA = :CPERSONA, ");
                query.Append(" CSUBSISTEMA = :CSUBSISTEMA, ");
                query.Append(" CODIGORECHAZO = :CODIGORECHAZO, ");
                query.Append(" FACTUALIZACION = :FACTUALIZACION, ");
                query.Append(" ERROR = :ERROR ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CPERSONA = :CPERSONA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUBSISTEMA", OracleDbType.Varchar2, obj.CSUBSISTEMA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CODIGORECHAZO", OracleDbType.Varchar2, obj.CODIGORECHAZO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, obj.FACTUALIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
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

        public bool ActualizarPersonaCuenta(TBTHDETALLETABULADO obj, string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TBTHDETALLETABULADO SET ");
                query.Append(" CUENTA = :CUENTANUEVA, ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" CPERSONA = :CPERSONA, ");
                query.Append(" CSUBSISTEMA = :CSUBSISTEMA, ");
                query.Append(" CODIGORECHAZO = :CODIGORECHAZO, ");
                query.Append(" FACTUALIZACION = :FACTUALIZACION, ");
                query.Append(" ERROR = :ERROR ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");

                if (tipo != "0002" && tipo != "0005")
                {
                    query.Append(" AND CUENTA = :CUENTA ");
                }

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUENTANUEVA", OracleDbType.Varchar2, obj.CUENTA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUBSISTEMA", OracleDbType.Varchar2, obj.CSUBSISTEMA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CODIGORECHAZO", OracleDbType.Varchar2, obj.CODIGORECHAZO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, obj.FACTUALIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));

                if (tipo != "0002" && tipo != "0005")
                {
                    comando.Parameters.Add(new OracleParameter("CUENTA", OracleDbType.Varchar2, obj.CUENTA, ParameterDirection.Input));
                }

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

        public bool ActualizarPendientes(DateTime? fechaProceso, Int32? CPROCESO, string estadoAntiguo, string estadoNuevo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TBTHDETALLETABULADO SET ");
                query.Append(" CESTADO = :CESTADONUEVO, ");
                query.Append(" FACTUALIZACION = :FACTUALIZACION ");
                query.Append(" WHERE CESTADO = :CESTADONUEVOANTIGUO ");
                query.Append(" AND FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADONUEVO", OracleDbType.Varchar2, estadoNuevo, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, DateTime.Now, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADONUEVOANTIGUO", OracleDbType.Varchar2, estadoAntiguo, ParameterDirection.Input));
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

        public List<TBTHDETALLETABULADO> ListarDesdeHasta(DateTime? fechaProceso, Int32? CPROCESO, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLETABULADO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT * FROM ( ");
                query.Append(" SELECT ");
                query.Append(" ROWNUM REGISTRO, ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CUENTA, ");
                query.Append(" CREDITO, ");
                query.Append(" VALOR, ");
                query.Append(" RUBRO, ");
                query.Append(" CODIGORECHAZO, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" LINEA ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" ORDER BY FPROCESO, CPROCESO, SECUENCIA ) ");
                query.Append(" WHERE REGISTRO BETWEEN :INICIO AND :FINAL ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("INICIO", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FINAL", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLETABULADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLETABULADO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            CREDITO = reader["CREDITO"].ToString(),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            CODIGORECHAZO = reader["CODIGORECHAZO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            LINEA = reader["LINEA"].ToString()
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

        public List<TBTHDETALLETABULADO> ListarDesdeHasta(DateTime? fechaProceso, Int32? CPROCESO, string cestado, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLETABULADO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CUENTA, ");
                query.Append(" CREDITO, ");
                query.Append(" VALOR, ");
                query.Append(" RUBRO, ");
                query.Append(" CODIGORECHAZO, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" LINEA ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = :CESTADO ");
                query.Append(" AND ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, cestado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLETABULADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLETABULADO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            CREDITO = reader["CREDITO"].ToString(),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            CODIGORECHAZO = reader["CODIGORECHAZO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            LINEA = reader["LINEA"].ToString()
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

        public List<TBTHDETALLETABULADO> ListarPersonaDesdeHasta(DateTime? fechaProceso, Int32? CPROCESO, string cestado, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLETABULADO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CUENTA, ");
                query.Append(" MAX(SECUENCIA) SECUENCIA, ");
                query.Append(" SUM (VALOR) VALOR, ");
                query.Append(" COUNT (*) NUMEROTRX ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = :CESTADO ");
                query.Append(" AND ROWNUM BETWEEN :INICIO AND :FINAL ");
                query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CUENTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, cestado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("INICIO", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FINAL", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLETABULADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLETABULADO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            LINEA = reader["NUMEROTRX"].ToString()
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

        public List<TBTHDETALLETABULADO> ListarPendientesPersonaCuentaDesdeHasta(DateTime? fechaProceso, Int32? CPROCESO, string estado, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLETABULADO> ltObj = null;

            try
            {
                #region armacomando

                query.Append("  SELECT ROWNUM, A.* FROM ( ");
                query.Append("  SELECT FPROCESO, ");
                query.Append("         CPROCESO, ");
                query.Append("         CPERSONA, ");
                query.Append("         IDENTIFICACION, ");
                query.Append("         CSUBSISTEMA, ");
                query.Append("         CUENTA, ");
                query.Append("         CODIGORECHAZO, ");
                query.Append("         COUNT (*) NUMTXN, ");
                query.Append("         SUM (VALOR) TOTAL ");
                query.Append("    FROM TBTHDETALLETABULADO ");
                query.Append("   WHERE FPROCESO = :FPROCESO ");
                query.Append("     AND CPROCESO = :CPROCESO ");
                query.Append("     AND CESTADO = :CESTADO ");
                query.Append("   GROUP BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CUENTA, CODIGORECHAZO ");
                query.Append("   ORDER BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CUENTA, CODIGORECHAZO ) A ");
                query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLETABULADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLETABULADO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["NUMTXN"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            VALOR = Util.ConvertirDecimal(reader["TOTAL"].ToString()),
                            CODIGORECHAZO = reader["CODIGORECHAZO"].ToString()
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

        public List<TBTHDETALLETABULADO> ListarPendientesPersonaDesdeHasta(DateTime? fechaProceso, Int32? CPROCESO, string estado, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLETABULADO> ltObj = null;

            try
            {
                #region armacomando

                query.Append("  SELECT ROWNUM, A.* FROM ( ");
                query.Append("  SELECT FPROCESO, ");
                query.Append("         CPROCESO, ");
                query.Append("         CPERSONA, ");
                query.Append("         IDENTIFICACION, ");
                query.Append("         CSUBSISTEMA, ");
                query.Append("         CODIGORECHAZO, ");
                query.Append("         COUNT (*) NUMTXN, ");
                query.Append("         SUM (VALOR) TOTAL ");
                query.Append("    FROM TBTHDETALLETABULADO ");
                query.Append("   WHERE FPROCESO = :FPROCESO ");
                query.Append("     AND CPROCESO = :CPROCESO ");
                query.Append("     AND CESTADO = :CESTADO ");
                query.Append("   GROUP BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CODIGORECHAZO ) A ");
                query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLETABULADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLETABULADO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["NUMTXN"].ToString()),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            VALOR = Util.ConvertirDecimal(reader["TOTAL"].ToString()),
                            CODIGORECHAZO = reader["CODIGORECHAZO"].ToString()
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

        public List<TBTHDETALLETABULADO> ListarPendientesPersonaCuenta(DateTime? fechaProceso, Int32? CPROCESO, string identificacion, string cuenta, string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLETABULADO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CUENTA, ");
                query.Append(" CREDITO, ");
                query.Append(" VALOR, ");
                query.Append(" RUBRO, ");
                query.Append(" CODIGORECHAZO, ");
                query.Append(" FCARGA, ");
                query.Append(" FACTUALIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" LINEA ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = :CESTADO ");
                query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");
                query.Append(" AND CUENTA = :CUENTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUENTA", OracleDbType.Varchar2, cuenta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLETABULADO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLETABULADO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CUENTA = reader["CUENTA"].ToString(),
                            CREDITO = reader["CREDITO"].ToString(),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
                            RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
                            CODIGORECHAZO = reader["CODIGORECHAZO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            LINEA = reader["LINEA"].ToString()
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

        public Int32 ContarRegistrosPersona(DateTime? fechaProceso, Int32? CPROCESO, string cestado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS FROM ( ");
                query.Append(" SELECT FPROCESO, ");
                query.Append("        CPROCESO, ");
                query.Append("        CPERSONA, ");
                query.Append("        IDENTIFICACION, ");
                query.Append("        CSUBSISTEMA, ");
                query.Append("        CUENTA, ");
                query.Append("        MAX(SECUENCIA) SECUENCIA, ");
                query.Append("        SUM (VALOR) VALOR, ");
                query.Append("        COUNT (*) NUMEROTRX ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = :CESTADO ");
                query.Append(" GROUP BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CUENTA) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, cestado, ParameterDirection.Input));

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

        public Int32 ContarPendientes(DateTime? fechaProceso, Int32? CPROCESO, string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE CESTADO = :CESTADO ");
                query.Append(" AND FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));
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

        public Int32 ContarPendientesValidarVista(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE CODIGORECHAZO IS NOT NULL ");
                query.Append(" AND FPROCESO = :FPROCESO ");
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

        public Int32 ContarPendientesPersonaCuenta(DateTime? fechaProceso, Int32? CPROCESO, string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS FROM ( ");
                query.Append("   SELECT FPROCESO, ");
                query.Append("          CPROCESO, ");
                query.Append("          CPERSONA, ");
                query.Append("          IDENTIFICACION, ");
                query.Append("          CSUBSISTEMA, ");
                query.Append("          CUENTA, ");
                query.Append("          CODIGORECHAZO, ");
                query.Append("          COUNT (*) NUMTXN, ");
                query.Append("          SUM (VALOR) TOTAL ");
                query.Append("     FROM TBTHDETALLETABULADO ");
                query.Append("    WHERE FPROCESO = :FPROCESO ");
                query.Append("      AND CPROCESO = :CPROCESO ");
                query.Append("      AND CESTADO = :CESTADO ");
                query.Append("    GROUP BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CUENTA, CODIGORECHAZO ) ");

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

        public Int32 ContarPendientesPersona(DateTime? fechaProceso, Int32? CPROCESO, string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS FROM ( ");
                query.Append("   SELECT FPROCESO, ");
                query.Append("          CPROCESO, ");
                query.Append("          CPERSONA, ");
                query.Append("          IDENTIFICACION, ");
                query.Append("          CSUBSISTEMA, ");
                query.Append("          CODIGORECHAZO, ");
                query.Append("          COUNT (*) NUMTXN, ");
                query.Append("          SUM (VALOR) TOTAL ");
                query.Append("     FROM TBTHDETALLETABULADO ");
                query.Append("    WHERE FPROCESO = :FPROCESO ");
                query.Append("      AND CPROCESO = :CPROCESO ");
                query.Append("      AND CESTADO = :CESTADO ");
                query.Append("    GROUP BY FPROCESO, CPROCESO, CPERSONA, IDENTIFICACION, CSUBSISTEMA, CODIGORECHAZO ) ");

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

        public Int32 ContarFinalizados(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE CESTADO IN ('TERMIN', 'RECHAZ', 'ERRPRO') ");
                query.Append(" AND FPROCESO = :FPROCESO ");
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

        public Int32 ContarConfirmados(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CODIGORECHAZO = '01' ");

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

        public Int32 SecuenciaProceso(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT MAX (SECUENCIA) SECUENCIA ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO AND CPROCESO = :CPROCESO ");

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
                        registros = Util.ConvertirNumero(reader["SECUENCIA"].ToString()).Value;
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

        public bool TotalesProceso(DateTime? fechaProceso, Int32? CPROCESO, out Int32 registros, out Decimal total)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool flag = false;
            registros = 0;
            total = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS, NVL(SUM(VALOR), 0) TOTAL ");
                query.Append(" FROM TBTHDETALLETABULADO ");
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
                        total = Util.ConvertirDecimal(reader["TOTAL"].ToString()).Value;
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                total = 0;
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return flag;
        }

        public bool TotalesProceso(DateTime? fechaProceso, Int32? CPROCESO, string cestado, out Int32 registros, out Decimal total)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool flag = false;
            registros = 0;
            total = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS, NVL(SUM(VALOR), 0) TOTAL ");
                query.Append(" FROM TBTHDETALLETABULADO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = :CESTADO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, cestado, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        total = Util.ConvertirDecimal(reader["TOTAL"].ToString()).Value;
                        registros = Util.ConvertirNumero(reader["REGISTROS"].ToString()).Value;
                    }
                }
                else
                {
                    registros = 0;
                }

                #endregion ejecutaComando

                flag = true;
            }
            catch (Exception ex)
            {
                flag = false;
                total = 0;
                registros = 0;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
            }
            finally
            {
                ado.CerrarConexion();
            }
            return flag;
        }

        #endregion metodos
    }
}
