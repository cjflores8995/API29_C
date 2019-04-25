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
    public class VBTHPROCESORESUMEN
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public String DESCRIPCION { get; set; }
        public String CARGA { get; set; }
        public String REPORTE { get; set; }
        public String CESTADO { get; set; }
        public String ESTADO { get; set; }
        public Int32? TOTAL { get; set; }
        public Int32? FINALIZADOS { get; set; }
        public Int32? PENDIENTES { get; set; }
        public Int32? CORRECTOS { get; set; }
        public Int32? ERRORES { get; set; }
        public Decimal? TOTALCREDITOS { get; set; }
        public Decimal? TOTALDEBITOS { get; set; }
        public String Link { get; set; }

        #endregion variables

        #region metodos

        public List<VBTHPROCESORESUMEN> ListarCargados()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESORESUMEN> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CARGA, ");
                query.Append(" REPORTE, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" TOTAL, ");
                query.Append(" FINALIZADOS, ");
                query.Append(" PENDIENTES, ");
                query.Append(" CORRECTOS, ");
                query.Append(" ERRORES ");
                query.Append(" FROM VBTHPROCESORESUMEN ");
                query.Append(" WHERE CESTADO = 'CARGAD' ");
                query.Append(" AND CARGA = '1' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHPROCESORESUMEN>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESORESUMEN
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CARGA = reader["CARGA"].ToString(),
                            REPORTE = reader["REPORTE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            TOTAL = Util.ConvertirNumero(reader["TOTAL"].ToString()),
                            FINALIZADOS = Util.ConvertirNumero(reader["FINALIZADOS"].ToString()),
                            PENDIENTES = Util.ConvertirNumero(reader["PENDIENTES"].ToString()),
                            CORRECTOS = Util.ConvertirNumero(reader["CORRECTOS"].ToString()),
                            ERRORES = Util.ConvertirNumero(reader["ERRORES"].ToString())
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

        public List<VBTHPROCESORESUMEN> ListarFinalizados()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESORESUMEN> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CARGA, ");
                query.Append(" REPORTE, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" TOTAL, ");
                query.Append(" FINALIZADOS, ");
                query.Append(" PENDIENTES, ");
                query.Append(" CORRECTOS, ");
                query.Append(" ERRORES ");
                query.Append(" FROM VBTHPROCESORESUMEN ");
                query.Append(" WHERE CESTADO = 'PENPRO' ");
                query.Append(" AND CARGA = '1' ");
                query.Append(" AND REPORTE = '0' ");
                query.Append(" AND PENDIENTES = 0 ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHPROCESORESUMEN>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESORESUMEN
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CARGA = reader["CARGA"].ToString(),
                            REPORTE = reader["REPORTE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            TOTAL = Util.ConvertirNumero(reader["TOTAL"].ToString()),
                            FINALIZADOS = Util.ConvertirNumero(reader["FINALIZADOS"].ToString()),
                            PENDIENTES = Util.ConvertirNumero(reader["PENDIENTES"].ToString()),
                            CORRECTOS = Util.ConvertirNumero(reader["CORRECTOS"].ToString()),
                            ERRORES = Util.ConvertirNumero(reader["ERRORES"].ToString())
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

        public List<VBTHPROCESORESUMEN> ListarResumen(DateTime? fechaDesde, DateTime? fechaHasta, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESORESUMEN> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CARGA, ");
                query.Append(" REPORTE, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" TOTAL, ");
                query.Append(" FINALIZADOS, ");
                query.Append(" PENDIENTES, ");
                query.Append(" CORRECTOS, ");
                query.Append(" ERRORES, ");
                query.Append(" TOTALCREDITOS, ");
                query.Append(" TOTALDEBITOS ");
                query.Append(" FROM VBTHPROCESORESUMEN ");
                query.Append(" WHERE 1 = 1 ");

                if (fechaDesde != null && fechaHasta != null)
                    query.Append(" AND TRUNC(FPROCESO) BETWEEN :FDESDE AND :FHASTA ");

                if (CPROCESO != null)
                    query.Append(" AND CPROCESO = :CPROCESO ");

                query.Append(" ORDER BY FPROCESO DESC, CPROCESO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (fechaDesde != null && fechaHasta != null)
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, fechaDesde, ParameterDirection.Input));
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, fechaHasta, ParameterDirection.Input));
                }

                if (CPROCESO != null)
                    comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHPROCESORESUMEN>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESORESUMEN
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CARGA = reader["CARGA"].ToString(),
                            REPORTE = reader["REPORTE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            TOTAL = Util.ConvertirNumero(reader["TOTAL"].ToString()),
                            FINALIZADOS = Util.ConvertirNumero(reader["FINALIZADOS"].ToString()),
                            PENDIENTES = Util.ConvertirNumero(reader["PENDIENTES"].ToString()),
                            CORRECTOS = Util.ConvertirNumero(reader["CORRECTOS"].ToString()),
                            ERRORES = Util.ConvertirNumero(reader["ERRORES"].ToString()),
                            TOTALCREDITOS = Util.ConvertirDecimal(reader["TOTALCREDITOS"].ToString()),
                            TOTALDEBITOS = Util.ConvertirDecimal(reader["TOTALDEBITOS"].ToString())
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

        public VBTHPROCESORESUMEN ListarResumen(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            VBTHPROCESORESUMEN obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CARGA, ");
                query.Append(" REPORTE, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" TOTAL, ");
                query.Append(" FINALIZADOS, ");
                query.Append(" PENDIENTES, ");
                query.Append(" CORRECTOS, ");
                query.Append(" ERRORES, ");
                query.Append(" TOTALCREDITOS, ");
                query.Append(" TOTALDEBITOS ");
                query.Append(" FROM VBTHPROCESORESUMEN ");
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
                        obj = new VBTHPROCESORESUMEN
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CARGA = reader["CARGA"].ToString(),
                            REPORTE = reader["REPORTE"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            TOTAL = Util.ConvertirNumero(reader["TOTAL"].ToString()),
                            FINALIZADOS = Util.ConvertirNumero(reader["FINALIZADOS"].ToString()),
                            PENDIENTES = Util.ConvertirNumero(reader["PENDIENTES"].ToString()),
                            CORRECTOS = Util.ConvertirNumero(reader["CORRECTOS"].ToString()),
                            ERRORES = Util.ConvertirNumero(reader["ERRORES"].ToString()),
                            TOTALCREDITOS = Util.ConvertirDecimal(reader["TOTALCREDITOS"].ToString()),
                            TOTALDEBITOS = Util.ConvertirDecimal(reader["TOTALDEBITOS"].ToString())
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
