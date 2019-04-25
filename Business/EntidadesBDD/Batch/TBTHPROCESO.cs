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
    public class TBTHPROCESO
    {
        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public String CTIPOPROCESO { get; set; }
        public String CESTADO { get; set; }
        public String CUSUARIO { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FMODIFICACION { get; set; }
        public String ERROR { get; set; }
        public String DESCRIPCION { get; set; }
        public Int32? CORTE { get; set; }
        public String CTIPOARCHIVO { get; set; }
        public String ARCHIVOORIGEN { get; set; }
        public String ARCHIVORESPUESTA { get; set; }
        public String ARCHIVOLOTES { get; set; }
        public String DATOSLOTE { get; set; }
        public String CREPARTO { get; set; }
        public String CODIGOAUTORIZA { get; set; }
        public DateTime? FCODIGOAUTORIZA { get; set; }

        public String CERROR { get; set; }
        public String DERROR { get; set; }
        public TBTHREPARTO reparto { get; set; }
        public TBTHTIPOTRANSACCION transaccion { get; set; }
        public String detailEntrada { get; set; }
        public String detailSalida { get; set; }

        public List<TBTHPROCESO> ListarXEstado(string cestado, bool order)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA ");
                query.Append(" FROM TBTHPROCESO ");
                query.Append(" WHERE CESTADO = :CESTADO ");

                if (order)
                    query.Append(" ORDER BY FPROCESO, CPROCESO ");
                else
                    query.Append(" ORDER BY FPROCESO DESC, CPROCESO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, cestado, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString())
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

        public List<TBTHPROCESO> ListarXEstadoTipo(string cestado, string ctipoProceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA ");
                query.Append(" FROM TBTHPROCESO ");
                query.Append(" WHERE CESTADO = :CESTADO ");
                query.Append(" AND CTIPOPROCESO = :CTIPOPROCESO ");
                query.Append(" ORDER BY FPROCESO, CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, cestado, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTIPOPROCESO", OracleDbType.Varchar2, ctipoProceso, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString())
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

        public List<TBTHPROCESO> ListarXCriterios(string criterios)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA ");
                query.Append(" FROM TBTHPROCESO ");
                query.Append(" WHERE 1 = 1 ");
                query.Append(criterios + " ");
                query.Append(" ORDER BY FPROCESO, CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString())
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

        public List<TBTHPROCESO> ListarPendientesLectura()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA ");
                query.Append(" FROM TBTHPROCESO ");
                query.Append(" WHERE CESTADO = 'CARGAD' ");
                query.Append(" AND CTIPOPROCESO IN ('EFECHE', 'SPITAB') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString())
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

        public List<TBTHPROCESO> ListarPendientesProceso()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA ");
                query.Append(" FROM TBTHPROCESO ");
                query.Append(" WHERE CESTADO IN ('PENPRO', 'PENVAL') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString())
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

        public TBTHPROCESO Listar(DateTime fproceso, Int32 cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TBTHPROCESO obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA ");
                query.Append(" FROM TBTHPROCESO ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString())
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

        public bool Actualizar(TBTHPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region arma comando

                query.Append(" UPDATE TBTHPROCESO SET ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" FMODIFICACION = :FMODIFICACION, ");
                query.Append(" ERROR = :ERROR, ");
                query.Append(" CODIGOAUTORIZA = :CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA = :FCODIGOAUTORIZA, ");
                query.Append(" ARCHIVORESPUESTA = :ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES = :ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE = :DATOSLOTE ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CODIGOAUTORIZA", OracleDbType.Varchar2, obj.CODIGOAUTORIZA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCODIGOAUTORIZA", OracleDbType.Date, obj.FCODIGOAUTORIZA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ARCHIVORESPUESTA", OracleDbType.Varchar2, obj.ARCHIVORESPUESTA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ARCHIVOLOTES", OracleDbType.Varchar2, obj.ARCHIVOLOTES, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DATOSLOTE", OracleDbType.Varchar2, obj.DATOSLOTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));

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
            return resp;
        }

        public bool Insertar(TBTHPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHPROCESO (");

                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" CREPARTO ");

                query.Append(" ) VALUES ( ");

                query.Append(" :FPROCESO, ");
                query.Append(" :CPROCESO, ");
                query.Append(" :CTIPOPROCESO, ");
                query.Append(" :CESTADO, ");
                query.Append(" :CUSUARIO, ");
                query.Append(" :FCARGA, ");
                query.Append(" :DESCRIPCION, ");
                query.Append(" :CORTE, ");
                query.Append(" :CTIPOARCHIVO, ");
                query.Append(" :ARCHIVOORIGEN, ");
                query.Append(" :CREPARTO ");

                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTIPOPROCESO", OracleDbType.Varchar2, obj.CTIPOPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESCRIPCION", OracleDbType.Varchar2, obj.DESCRIPCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CORTE", OracleDbType.Int32, obj.CORTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTIPOARCHIVO", OracleDbType.Varchar2, obj.CTIPOARCHIVO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ARCHIVOORIGEN", OracleDbType.Varchar2, obj.ARCHIVOORIGEN, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CREPARTO", OracleDbType.Varchar2, obj.CREPARTO, ParameterDirection.Input));

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

        public bool Autorizar(VBTHPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region arma comando

                query.Append(" UPDATE TBTHPROCESO SET ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" FMODIFICACION = :FMODIFICACION, ");
                query.Append(" CUSUARIOAUTORIZA = :CUSUARIOAUTORIZA, ");
                query.Append(" FAUTORIZA = :FAUTORIZA ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIOAUTORIZA", OracleDbType.Varchar2, obj.CUSUARIOAUTORIZA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FAUTORIZA", OracleDbType.Date, obj.FAUTORIZA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));

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
            return resp;
        }

        //query.Append(" FPROCESO, ");
        //query.Append(" CPROCESO, ");
        //query.Append(" CTIPOPROCESO, ");
        //query.Append(" CESTADO, ");
        //query.Append(" CUSUARIO, ");
        //query.Append(" FCARGA, ");
        //query.Append(" FMODIFICACION, ");
        //query.Append(" ERROR, ");
        //query.Append(" DESCRIPCION, ");
        //query.Append(" CORTE, ");
        //query.Append(" CTIPOARCHIVO, ");
        //query.Append(" ARCHIVOORIGEN, ");
        //query.Append(" ARCHIVORESPUESTA, ");
        //query.Append(" CREPARTO, ");
        //query.Append(" CODIGOAUTORIZA, ");
        //query.Append(" FCODIGOAUTORIZA, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
        //CESTADO = reader["CESTADO"].ToString(),
        //CUSUARIO = reader["CUSUARIO"].ToString(),
        //FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
        //ERROR = reader["ERROR"].ToString(),
        //DESCRIPCION = reader["DESCRIPCION"].ToString(),
        //CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
        //CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
        //ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
        //ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
        //CREPARTO = reader["CREPARTO"].ToString(),
        //CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
        //FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" CTIPOPROCESO = :CTIPOPROCESO, ");
        //query.Append(" CESTADO = :CESTADO, ");
        //query.Append(" CUSUARIO = :CUSUARIO, ");
        //query.Append(" FCARGA = :FCARGA, ");
        //query.Append(" FMODIFICACION = :FMODIFICACION, ");
        //query.Append(" ERROR = :ERROR, ");
        //query.Append(" DESCRIPCION = :DESCRIPCION, ");
        //query.Append(" CORTE = :CORTE, ");
        //query.Append(" CTIPOARCHIVO = :CTIPOARCHIVO, ");
        //query.Append(" ARCHIVOORIGEN = :ARCHIVOORIGEN, ");
        //query.Append(" ARCHIVORESPUESTA = :ARCHIVORESPUESTA, ");
        //query.Append(" CREPARTO = :CREPARTO, ");
        //query.Append(" CODIGOAUTORIZA = :CODIGOAUTORIZA, ");
        //query.Append(" FCODIGOAUTORIZA = :FCODIGOAUTORIZA, ");

        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTIPOPROCESO", OracleDbType.Varchar2, obj.CTIPOPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DESCRIPCION", OracleDbType.Varchar2, obj.DESCRIPCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CORTE", OracleDbType.Int32, obj.CORTE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTIPOARCHIVO", OracleDbType.Varchar2, obj.CTIPOARCHIVO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ARCHIVOORIGEN", OracleDbType.Varchar2, obj.ARCHIVOORIGEN, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ARCHIVORESPUESTA", OracleDbType.Varchar2, obj.ARCHIVORESPUESTA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CREPARTO", OracleDbType.Varchar2, obj.CREPARTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CODIGOAUTORIZA", OracleDbType.Varchar2, obj.CODIGOAUTORIZA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCODIGOAUTORIZA", OracleDbType.Date, obj.FCODIGOAUTORIZA, ParameterDirection.Input));

    }
}
