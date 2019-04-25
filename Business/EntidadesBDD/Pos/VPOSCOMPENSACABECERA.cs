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
    public class VPOSCOMPENSACABECERA
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CCONVENIO { get; set; }
        public String NOMBRE { get; set; }
        public DateTime? FAUTORIZACION { get; set; }
        public String CUSUARIOAUTORIZACION { get; set; }
        public String USUARIOAUTORIZACION { get; set; }
        public String ERROR { get; set; }
        public String TRANSFERENCIA { get; set; }
        public String COMISION { get; set; }
        public String RETENCIONFTE { get; set; }
        public String RETENCIONIVA { get; set; }
        public String CESTADO { get; set; }
        public String ESTADO { get; set; }
        public Int32? REGISTROS { get; set; }
        public Int32? COMPENSADOS { get; set; }
        public Int32? RECHAZADOS { get; set; }
        public Decimal? TOTALTRANSACCION { get; set; }
        public Decimal? TOTALLIQUIDADO { get; set; }
        public Decimal? TOTALCOMISION { get; set; }
        public Decimal? TOTALIVACOMISION { get; set; }
        public Decimal? TOTALRETENCIONFTE { get; set; }
        public Decimal? TOTALRETENCIONIVA { get; set; }

        public String link { get; set; }

        #endregion variables

        #region metodos

        public VPOSCOMPENSACABECERA Listar(DateTime? fproceso, Int32? cconvenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            VPOSCOMPENSACABECERA obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" NOMBRE, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION, ");
                query.Append(" USUARIOAUTORIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" TRANSFERENCIA, ");
                query.Append(" COMISION, ");
                query.Append(" RETENCIONFTE, ");
                query.Append(" RETENCIONIVA, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" REGISTROS, ");
                query.Append(" COMPENSADOS, ");
                query.Append(" RECHAZADOS, ");
                query.Append(" TOTALTRANSACCION, ");
                query.Append(" TOTALLIQUIDADO, ");
                query.Append(" TOTALCOMISION, ");
                query.Append(" TOTALIVACOMISION, ");
                query.Append(" TOTALRETENCIONFTE, ");
                query.Append(" TOTALRETENCIONIVA ");
                query.Append(" FROM VPOSCOMPENSACABECERA ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, cconvenio, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new VPOSCOMPENSACABECERA
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
                            USUARIOAUTORIZACION = reader["USUARIOAUTORIZACION"].ToString(),
                            ERROR = reader["ERROR"].ToString(),
                            TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
                            COMISION = reader["COMISION"].ToString(),
                            RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
                            RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
                            RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
                            TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
                            TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
                            TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
                            TOTALIVACOMISION = Util.ConvertirDecimal(reader["TOTALIVACOMISION"].ToString()),
                            TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
                            TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString())
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

        public List<VPOSCOMPENSACABECERA> Listar(DateTime? fechaDesde, DateTime? fechaHasta, Int32? cconvenio, string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCOMPENSACABECERA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" NOMBRE, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION, ");
                query.Append(" USUARIOAUTORIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" TRANSFERENCIA, ");
                query.Append(" COMISION, ");
                query.Append(" RETENCIONFTE, ");
                query.Append(" RETENCIONIVA, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" REGISTROS, ");
                query.Append(" COMPENSADOS, ");
                query.Append(" RECHAZADOS, ");
                query.Append(" TOTALTRANSACCION, ");
                query.Append(" TOTALLIQUIDADO, ");
                query.Append(" TOTALCOMISION, ");
                query.Append(" TOTALIVACOMISION, ");
                query.Append(" TOTALRETENCIONFTE, ");
                query.Append(" TOTALRETENCIONIVA ");
                query.Append(" FROM VPOSCOMPENSACABECERA ");
                query.Append(" WHERE 1 = 1 ");

                if (fechaDesde != null && fechaHasta != null)
                {
                    query.Append(" AND FPROCESO BETWEEN :FDESDE AND :FHASTA ");
                }

                if (cconvenio != null)
                {
                    query.Append(" AND CCONVENIO = :CCONVENIO ");
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    query.Append(" AND CESTADO = :CESTADO ");
                }

                query.Append(" ORDER BY FPROCESO DESC, NOMBRE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (fechaDesde != null && fechaHasta != null)
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, fechaDesde, ParameterDirection.Input));
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, fechaHasta, ParameterDirection.Input));
                }

                if (cconvenio != null)
                {
                    comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, cconvenio, ParameterDirection.Input));
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCOMPENSACABECERA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCOMPENSACABECERA
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
                            USUARIOAUTORIZACION = reader["USUARIOAUTORIZACION"].ToString(),
                            ERROR = reader["ERROR"].ToString(),
                            TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
                            COMISION = reader["COMISION"].ToString(),
                            RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
                            RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
                            RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
                            TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
                            TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
                            TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
                            TOTALIVACOMISION = Util.ConvertirDecimal(reader["TOTALIVACOMISION"].ToString()),
                            TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
                            TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString())
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

        public List<VPOSCOMPENSACABECERA> ListarPendientesLectura()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCOMPENSACABECERA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" NOMBRE, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION, ");
                query.Append(" USUARIOAUTORIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" TRANSFERENCIA, ");
                query.Append(" COMISION, ");
                query.Append(" RETENCIONFTE, ");
                query.Append(" RETENCIONIVA, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" REGISTROS, ");
                query.Append(" COMPENSADOS, ");
                query.Append(" RECHAZADOS, ");
                query.Append(" TOTALTRANSACCION, ");
                query.Append(" TOTALLIQUIDADO, ");
                query.Append(" TOTALCOMISION, ");
                query.Append(" TOTALIVACOMISION, ");
                query.Append(" TOTALRETENCIONFTE, ");
                query.Append(" TOTALRETENCIONIVA ");
                query.Append(" FROM VPOSCOMPENSACABECERA ");
                query.Append(" WHERE CESTADO IN ('PEN') ");
                query.Append(" ORDER BY FPROCESO, CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCOMPENSACABECERA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCOMPENSACABECERA
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
                            USUARIOAUTORIZACION = reader["USUARIOAUTORIZACION"].ToString(),
                            ERROR = reader["ERROR"].ToString(),
                            TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
                            COMISION = reader["COMISION"].ToString(),
                            RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
                            RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
                            RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
                            TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
                            TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
                            TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
                            TOTALIVACOMISION = Util.ConvertirDecimal(reader["TOTALIVACOMISION"].ToString()),
                            TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
                            TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString())
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

        public List<VPOSCOMPENSACABECERA> ListarPendientesCompensar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCOMPENSACABECERA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" NOMBRE, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION, ");
                query.Append(" USUARIOAUTORIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" TRANSFERENCIA, ");
                query.Append(" COMISION, ");
                query.Append(" RETENCIONFTE, ");
                query.Append(" RETENCIONIVA, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" REGISTROS, ");
                query.Append(" COMPENSADOS, ");
                query.Append(" RECHAZADOS, ");
                query.Append(" TOTALTRANSACCION, ");
                query.Append(" TOTALLIQUIDADO, ");
                query.Append(" TOTALCOMISION, ");
                query.Append(" TOTALIVACOMISION, ");
                query.Append(" TOTALRETENCIONFTE, ");
                query.Append(" TOTALRETENCIONIVA ");
                query.Append(" FROM VPOSCOMPENSACABECERA ");
                query.Append(" WHERE CESTADO IN ('LEC', 'REC') ");
                query.Append(" ORDER BY FPROCESO, CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCOMPENSACABECERA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCOMPENSACABECERA
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
                            USUARIOAUTORIZACION = reader["USUARIOAUTORIZACION"].ToString(),
                            ERROR = reader["ERROR"].ToString(),
                            TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
                            COMISION = reader["COMISION"].ToString(),
                            RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
                            RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
                            RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
                            TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
                            TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
                            TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
                            TOTALIVACOMISION = Util.ConvertirDecimal(reader["TOTALIVACOMISION"].ToString()),
                            TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
                            TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString())
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

        public List<VPOSCOMPENSACABECERA> ListarPendientesAutorizar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCOMPENSACABECERA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CCONVENIO, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" SUM (REGISTROS) REGISTROS, ");
                query.Append(" SUM (COMPENSADOS) COMPENSADOS, ");
                query.Append(" SUM (RECHAZADOS) RECHAZADOS, ");
                query.Append(" SUM (TOTALTRANSACCION) TOTALTRANSACCION, ");
                query.Append(" SUM (TOTALLIQUIDADO) TOTALLIQUIDADO, ");
                query.Append(" SUM (TOTALCOMISION) TOTALCOMISION, ");
                query.Append(" SUM (TOTALIVACOMISION) TOTALIVACOMISION, ");
                query.Append(" SUM (TOTALRETENCIONFTE) TOTALRETENCIONFTE, ");
                query.Append(" SUM (TOTALRETENCIONIVA) TOTALRETENCIONIVA ");
                query.Append(" FROM VPOSCOMPENSACABECERA ");
                query.Append(" WHERE CESTADO IN ('AUT') ");
                query.Append(" GROUP BY CCONVENIO, FAUTORIZACION ");
                query.Append(" ORDER BY CCONVENIO, FAUTORIZACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCOMPENSACABECERA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCOMPENSACABECERA
                        {
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
                            RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
                            TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
                            TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
                            TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
                            TOTALIVACOMISION = Util.ConvertirDecimal(reader["TOTALIVACOMISION"].ToString()),
                            TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
                            TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString())
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

        public List<VPOSCOMPENSACABECERA> ListarAutorizadosConvenio(Int32? cconvenio, DateTime? fautorizacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VPOSCOMPENSACABECERA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" NOMBRE, ");
                query.Append(" FAUTORIZACION, ");
                query.Append(" CUSUARIOAUTORIZACION, ");
                query.Append(" USUARIOAUTORIZACION, ");
                query.Append(" ERROR, ");
                query.Append(" TRANSFERENCIA, ");
                query.Append(" COMISION, ");
                query.Append(" RETENCIONFTE, ");
                query.Append(" RETENCIONIVA, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" REGISTROS, ");
                query.Append(" COMPENSADOS, ");
                query.Append(" RECHAZADOS, ");
                query.Append(" TOTALTRANSACCION, ");
                query.Append(" TOTALLIQUIDADO, ");
                query.Append(" TOTALCOMISION, ");
                query.Append(" TOTALIVACOMISION, ");
                query.Append(" TOTALRETENCIONFTE, ");
                query.Append(" TOTALRETENCIONIVA ");
                query.Append(" FROM VPOSCOMPENSACABECERA ");
                query.Append(" WHERE CESTADO IN ('AUT') ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");
                query.Append(" AND FAUTORIZACION = :FAUTORIZACION ");
                query.Append(" ORDER BY FPROCESO, CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, cconvenio, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZACION", OracleDbType.Date, fautorizacion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VPOSCOMPENSACABECERA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VPOSCOMPENSACABECERA
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
                            CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
                            USUARIOAUTORIZACION = reader["USUARIOAUTORIZACION"].ToString(),
                            ERROR = reader["ERROR"].ToString(),
                            TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
                            COMISION = reader["COMISION"].ToString(),
                            RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
                            RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
                            RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
                            TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
                            TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
                            TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
                            TOTALIVACOMISION = Util.ConvertirDecimal(reader["TOTALIVACOMISION"].ToString()),
                            TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
                            TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString())
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

        //query.Append(" FPROCESO, ");
        //query.Append(" CCONVENIO, ");
        //query.Append(" NOMBRE, ");
        //query.Append(" FAUTORIZACION, ");
        //query.Append(" CUSUARIOAUTORIZACION, ");
        //query.Append(" USUARIOAUTORIZACION, ");
        //query.Append(" ERROR, ");
        //query.Append(" TRANSFERENCIA, ");
        //query.Append(" COMISION, ");
        //query.Append(" RETENCIONFTE, ");
        //query.Append(" RETENCIONIVA, ");
        //query.Append(" CESTADO, ");
        //query.Append(" ESTADO, ");
        //query.Append(" REGISTROS, ");
        //query.Append(" COMPENSADOS, ");
        //query.Append(" RECHAZADOS, ");
        //query.Append(" TOTALTRANSACCION, ");
        //query.Append(" TOTALLIQUIDADO, ");
        //query.Append(" TOTALCOMISION, ");
        //query.Append(" TOTALRETENCIONFTE, ");
        //query.Append(" TOTALRETENCIONIVA, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
        //NOMBRE = reader["NOMBRE"].ToString(),
        //FAUTORIZACION = Util.ConvertirFecha(reader["FAUTORIZACION"].ToString()),
        //CUSUARIOAUTORIZACION = reader["CUSUARIOAUTORIZACION"].ToString(),
        //USUARIOAUTORIZACION = reader["USUARIOAUTORIZACION"].ToString(),
        //ERROR = reader["ERROR"].ToString(),
        //TRANSFERENCIA = reader["TRANSFERENCIA"].ToString(),
        //COMISION = reader["COMISION"].ToString(),
        //RETENCIONFTE = reader["RETENCIONFTE"].ToString(),
        //RETENCIONIVA = reader["RETENCIONIVA"].ToString(),
        //CESTADO = reader["CESTADO"].ToString(),
        //ESTADO = reader["ESTADO"].ToString(),
        //REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
        //COMPENSADOS = Util.ConvertirNumero(reader["COMPENSADOS"].ToString()),
        //RECHAZADOS = Util.ConvertirNumero(reader["RECHAZADOS"].ToString()),
        //TOTALTRANSACCION = Util.ConvertirDecimal(reader["TOTALTRANSACCION"].ToString()),
        //TOTALLIQUIDADO = Util.ConvertirDecimal(reader["TOTALLIQUIDADO"].ToString()),
        //TOTALCOMISION = Util.ConvertirDecimal(reader["TOTALCOMISION"].ToString()),
        //TOTALRETENCIONFTE = Util.ConvertirDecimal(reader["TOTALRETENCIONFTE"].ToString()),
        //TOTALRETENCIONIVA = Util.ConvertirDecimal(reader["TOTALRETENCIONIVA"].ToString()),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CCONVENIO = :CCONVENIO, ");
        //query.Append(" NOMBRE = :NOMBRE, ");
        //query.Append(" FAUTORIZACION = :FAUTORIZACION, ");
        //query.Append(" CUSUARIOAUTORIZACION = :CUSUARIOAUTORIZACION, ");
        //query.Append(" USUARIOAUTORIZACION = :USUARIOAUTORIZACION, ");
        //query.Append(" ERROR = :ERROR, ");
        //query.Append(" TRANSFERENCIA = :TRANSFERENCIA, ");
        //query.Append(" COMISION = :COMISION, ");
        //query.Append(" RETENCIONFTE = :RETENCIONFTE, ");
        //query.Append(" RETENCIONIVA = :RETENCIONIVA, ");
        //query.Append(" CESTADO = :CESTADO, ");
        //query.Append(" ESTADO = :ESTADO, ");
        //query.Append(" REGISTROS = :REGISTROS, ");
        //query.Append(" COMPENSADOS = :COMPENSADOS, ");
        //query.Append(" RECHAZADOS = :RECHAZADOS, ");
        //query.Append(" TOTALTRANSACCION = :TOTALTRANSACCION, ");
        //query.Append(" TOTALLIQUIDADO = :TOTALLIQUIDADO, ");
        //query.Append(" TOTALCOMISION = :TOTALCOMISION, ");
        //query.Append(" TOTALRETENCIONFTE = :TOTALRETENCIONFTE, ");
        //query.Append(" TOTALRETENCIONIVA = :TOTALRETENCIONIVA, ");

    }
}
