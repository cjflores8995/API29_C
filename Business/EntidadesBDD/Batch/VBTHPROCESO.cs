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
    public class VBTHPROCESO
    {
        #region varibales

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public String CTIPOPROCESO { get; set; }
        public String TIPOPROCESO { get; set; }
        public String CESTADO { get; set; }
        public String ESTADO { get; set; }
        public String CUSUARIO { get; set; }
        public String USUARIO { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FMODIFICACION { get; set; }
        public String ERROR { get; set; }
        public String DESCRIPCION { get; set; }
        public Int32? CORTE { get; set; }
        public String CTIPOARCHIVO { get; set; }
        public String TIPOARCHIVO { get; set; }
        public String ARCHIVOORIGEN { get; set; }
        public String ARCHIVORESPUESTA { get; set; }
        public String ARCHIVOLOTES { get; set; }
        public String DATOSLOTE { get; set; }
        public String CREPARTO { get; set; }
        public String REPARTO { get; set; }
        public String CSUCURSALREPARTO { get; set; }
        public String COFICINAREPARTO { get; set; }
        public String CODIGOAUTORIZA { get; set; }
        public DateTime? FCODIGOAUTORIZA { get; set; }
        public String CUSUARIOAUTORIZA { get; set; }
        public DateTime? FAUTORIZA { get; set; }
        public Int32? REGISTROS { get; set; }
        public Decimal? TOTAL { get; set; }

        public string link1 { get; set; }
        public string link2 { get; set; }

        #endregion varibales

        #region metodos

        public List<VBTHPROCESO> ListarXEstado(string estado)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" TIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" USUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" TIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" REPARTO, ");
                query.Append(" CSUCURSALREPARTO, ");
                query.Append(" COFICINAREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA, ");
                query.Append(" CUSUARIOAUTORIZA, ");
                query.Append(" FAUTORIZA, ");
                query.Append(" REGISTROS, ");
                query.Append(" TOTAL ");
                query.Append(" FROM VBTHPROCESO ");
                query.Append(" WHERE CESTADO = :CESTADO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, estado, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            TIPOPROCESO = reader["TIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            USUARIO = reader["USUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            TIPOARCHIVO = reader["TIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            REPARTO = reader["REPARTO"].ToString(),
                            CSUCURSALREPARTO = reader["CSUCURSALREPARTO"].ToString(),
                            COFICINAREPARTO = reader["COFICINAREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),
                            CUSUARIOAUTORIZA = reader["CUSUARIOAUTORIZA"].ToString(),
                            FAUTORIZA = Util.ConvertirFecha(reader["FAUTORIZA"].ToString()),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString())
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

        public VBTHPROCESO Listar(DateTime? fproceso, Int32? cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            VBTHPROCESO obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" TIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" USUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" TIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" REPARTO, ");
                query.Append(" CSUCURSALREPARTO, ");
                query.Append(" COFICINAREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA, ");
                query.Append(" CUSUARIOAUTORIZA, ");
                query.Append(" FAUTORIZA, ");
                query.Append(" REGISTROS, ");
                query.Append(" TOTAL ");
                query.Append(" FROM VBTHPROCESO ");
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
                        obj = new VBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            TIPOPROCESO = reader["TIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            USUARIO = reader["USUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            TIPOARCHIVO = reader["TIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            REPARTO = reader["REPARTO"].ToString(),
                            CSUCURSALREPARTO = reader["CSUCURSALREPARTO"].ToString(),
                            COFICINAREPARTO = reader["COFICINAREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),
                            CUSUARIOAUTORIZA = reader["CUSUARIOAUTORIZA"].ToString(),
                            FAUTORIZA = Util.ConvertirFecha(reader["FAUTORIZA"].ToString()),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString())
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

        public List<VBTHPROCESO> Listar(DateTime? fdesde, DateTime? fhasta, Int32? cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" TIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" USUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" TIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" REPARTO, ");
                query.Append(" CSUCURSALREPARTO, ");
                query.Append(" COFICINAREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA, ");
                query.Append(" CUSUARIOAUTORIZA, ");
                query.Append(" FAUTORIZA, ");
                query.Append(" REGISTROS, ");
                query.Append(" TOTAL ");
                query.Append(" FROM VBTHPROCESO ");
                query.Append(" WHERE 1 = 1 ");

                if (fdesde != null && fhasta != null)
                {
                    query.Append(" AND FPROCESO BETWEEN :FDESDE AND :FHASTA ");
                }

                if (cproceso != null)
                {
                    query.Append(" AND CPROCESO = :CPROCESO ");
                }

                query.Append(" ORDER BY FPROCESO DESC, CPROCESO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (fdesde != null && fhasta != null)
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, fdesde, ParameterDirection.Input));
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, fhasta, ParameterDirection.Input));
                }

                if (cproceso != null)
                {
                    comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));
                }

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            TIPOPROCESO = reader["TIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            USUARIO = reader["USUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            TIPOARCHIVO = reader["TIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            REPARTO = reader["REPARTO"].ToString(),
                            CSUCURSALREPARTO = reader["CSUCURSALREPARTO"].ToString(),
                            COFICINAREPARTO = reader["COFICINAREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),
                            CUSUARIOAUTORIZA = reader["CUSUARIOAUTORIZA"].ToString(),
                            FAUTORIZA = Util.ConvertirFecha(reader["FAUTORIZA"].ToString()),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString())
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

        public List<VBTHPROCESO> ListarPendientesAutorizar(Int32? cproceso, string usuario)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" TIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" USUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" TIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" REPARTO, ");
                query.Append(" CSUCURSALREPARTO, ");
                query.Append(" COFICINAREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA, ");
                query.Append(" CUSUARIOAUTORIZA, ");
                query.Append(" FAUTORIZA, ");
                query.Append(" REGISTROS, ");
                query.Append(" TOTAL ");
                query.Append(" FROM VBTHPROCESO ");
                query.Append(" WHERE CESTADO = 'PENAUT' ");

                if (cproceso != null)
                {
                    query.Append(" AND CPROCESO = :CPROCESO ");
                }

                if (!string.IsNullOrEmpty(usuario))
                {
                    query.Append(" AND CUSUARIO = :CUSUARIO ");
                }

                query.Append(" ORDER BY FPROCESO, CPROCESO DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (cproceso != null)
                {
                    comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));
                }

                if (!string.IsNullOrEmpty(usuario))
                {
                    comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, usuario, ParameterDirection.Input));
                }

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            TIPOPROCESO = reader["TIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            USUARIO = reader["USUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            TIPOARCHIVO = reader["TIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            REPARTO = reader["REPARTO"].ToString(),
                            CSUCURSALREPARTO = reader["CSUCURSALREPARTO"].ToString(),
                            COFICINAREPARTO = reader["COFICINAREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),
                            CUSUARIOAUTORIZA = reader["CUSUARIOAUTORIZA"].ToString(),
                            FAUTORIZA = Util.ConvertirFecha(reader["FAUTORIZA"].ToString()),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString())
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

        public List<VBTHPROCESO> ListarXEstadoTipo(string cestado, string ctipoProceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CTIPOPROCESO, ");
                query.Append(" TIPOPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" CUSUARIO, ");
                query.Append(" USUARIO, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" ERROR, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CORTE, ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" TIPOARCHIVO, ");
                query.Append(" ARCHIVOORIGEN, ");
                query.Append(" ARCHIVORESPUESTA, ");
                query.Append(" ARCHIVOLOTES, ");
                query.Append(" DATOSLOTE, ");
                query.Append(" CREPARTO, ");
                query.Append(" REPARTO, ");
                query.Append(" CSUCURSALREPARTO, ");
                query.Append(" COFICINAREPARTO, ");
                query.Append(" CODIGOAUTORIZA, ");
                query.Append(" FCODIGOAUTORIZA, ");
                query.Append(" CUSUARIOAUTORIZA, ");
                query.Append(" FAUTORIZA, ");
                query.Append(" REGISTROS, ");
                query.Append(" TOTAL ");
                query.Append(" FROM VBTHPROCESO ");
                query.Append(" WHERE CESTADO = :CESTADO ");
                query.Append(" AND CTIPOPROCESO = :CTIPOPROCESO ");

                query.Append(" ORDER BY FPROCESO, CPROCESO DESC ");

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
                    ltObj = new List<VBTHPROCESO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESO
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
                            TIPOPROCESO = reader["TIPOPROCESO"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            USUARIO = reader["USUARIO"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            ERROR = reader["ERROR"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            TIPOARCHIVO = reader["TIPOARCHIVO"].ToString(),
                            ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
                            ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
                            ARCHIVOLOTES = reader["ARCHIVOLOTES"].ToString(),
                            DATOSLOTE = reader["DATOSLOTE"].ToString(),
                            CREPARTO = reader["CREPARTO"].ToString(),
                            REPARTO = reader["REPARTO"].ToString(),
                            CSUCURSALREPARTO = reader["CSUCURSALREPARTO"].ToString(),
                            COFICINAREPARTO = reader["COFICINAREPARTO"].ToString(),
                            CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
                            FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),
                            CUSUARIOAUTORIZA = reader["CUSUARIOAUTORIZA"].ToString(),
                            FAUTORIZA = Util.ConvertirFecha(reader["FAUTORIZA"].ToString()),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString())
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
        //query.Append(" CPROCESO, ");
        //query.Append(" CTIPOPROCESO, ");
        //query.Append(" TIPOPROCESO, ");
        //query.Append(" CESTADO, ");
        //query.Append(" ESTADO, ");
        //query.Append(" CUSUARIO, ");
        //query.Append(" USUARIO, ");
        //query.Append(" FCARGA, ");
        //query.Append(" FMODIFICACION, ");
        //query.Append(" ERROR, ");
        //query.Append(" DESCRIPCION, ");
        //query.Append(" CORTE, ");
        //query.Append(" CTIPOARCHIVO, ");
        //query.Append(" TIPOARCHIVO, ");
        //query.Append(" ARCHIVOORIGEN, ");
        //query.Append(" ARCHIVORESPUESTA, ");
        //query.Append(" CREPARTO, ");
        //query.Append(" REPARTO, ");
        //query.Append(" CODIGOAUTORIZA, ");
        //query.Append(" FCODIGOAUTORIZA, ");
        //query.Append(" CUSUARIOAUTORIZA, ");
        //query.Append(" FAUTORIZA, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //CTIPOPROCESO = reader["CTIPOPROCESO"].ToString(),
        //TIPOPROCESO = reader["TIPOPROCESO"].ToString(),
        //CESTADO = reader["CESTADO"].ToString(),
        //ESTADO = reader["ESTADO"].ToString(),
        //CUSUARIO = reader["CUSUARIO"].ToString(),
        //USUARIO = reader["USUARIO"].ToString(),
        //FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
        //ERROR = reader["ERROR"].ToString(),
        //DESCRIPCION = reader["DESCRIPCION"].ToString(),
        //CORTE = Util.ConvertirNumero(reader["CORTE"].ToString()),
        //CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
        //TIPOARCHIVO = reader["TIPOARCHIVO"].ToString(),
        //ARCHIVOORIGEN = reader["ARCHIVOORIGEN"].ToString(),
        //ARCHIVORESPUESTA = reader["ARCHIVORESPUESTA"].ToString(),
        //CREPARTO = reader["CREPARTO"].ToString(),
        //REPARTO = reader["REPARTO"].ToString(),
        //CODIGOAUTORIZA = reader["CODIGOAUTORIZA"].ToString(),
        //FCODIGOAUTORIZA = Util.ConvertirFecha(reader["FCODIGOAUTORIZA"].ToString()),
        //CUSUARIOAUTORIZA = reader["CUSUARIOAUTORIZA"].ToString(),
        //FAUTORIZA = Util.ConvertirFecha(reader["FAUTORIZA"].ToString()),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" CTIPOPROCESO = :CTIPOPROCESO, ");
        //query.Append(" TIPOPROCESO = :TIPOPROCESO, ");
        //query.Append(" CESTADO = :CESTADO, ");
        //query.Append(" ESTADO = :ESTADO, ");
        //query.Append(" CUSUARIO = :CUSUARIO, ");
        //query.Append(" USUARIO = :USUARIO, ");
        //query.Append(" FCARGA = :FCARGA, ");
        //query.Append(" FMODIFICACION = :FMODIFICACION, ");
        //query.Append(" ERROR = :ERROR, ");
        //query.Append(" DESCRIPCION = :DESCRIPCION, ");
        //query.Append(" CORTE = :CORTE, ");
        //query.Append(" CTIPOARCHIVO = :CTIPOARCHIVO, ");
        //query.Append(" TIPOARCHIVO = :TIPOARCHIVO, ");
        //query.Append(" ARCHIVOORIGEN = :ARCHIVOORIGEN, ");
        //query.Append(" ARCHIVORESPUESTA = :ARCHIVORESPUESTA, ");
        //query.Append(" CREPARTO = :CREPARTO, ");
        //query.Append(" REPARTO = :REPARTO, ");
        //query.Append(" CODIGOAUTORIZA = :CODIGOAUTORIZA, ");
        //query.Append(" FCODIGOAUTORIZA = :FCODIGOAUTORIZA, ");
        //query.Append(" CUSUARIOAUTORIZA = :CUSUARIOAUTORIZA, ");
        //query.Append(" FAUTORIZA = :FAUTORIZA, ");

        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTIPOPROCESO", OracleDbType.Varchar2, obj.CTIPOPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TIPOPROCESO", OracleDbType.Varchar2, obj.TIPOPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ESTADO", OracleDbType.Varchar2, obj.ESTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("USUARIO", OracleDbType.Varchar2, obj.USUARIO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ERROR", OracleDbType.Varchar2, obj.ERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DESCRIPCION", OracleDbType.Varchar2, obj.DESCRIPCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CORTE", OracleDbType.Int32, obj.CORTE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTIPOARCHIVO", OracleDbType.Varchar2, obj.CTIPOARCHIVO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TIPOARCHIVO", OracleDbType.Varchar2, obj.TIPOARCHIVO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ARCHIVOORIGEN", OracleDbType.Varchar2, obj.ARCHIVOORIGEN, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ARCHIVORESPUESTA", OracleDbType.Varchar2, obj.ARCHIVORESPUESTA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CREPARTO", OracleDbType.Varchar2, obj.CREPARTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("REPARTO", OracleDbType.Varchar2, obj.REPARTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CODIGOAUTORIZA", OracleDbType.Varchar2, obj.CODIGOAUTORIZA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCODIGOAUTORIZA", OracleDbType.Date, obj.FCODIGOAUTORIZA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CUSUARIOAUTORIZA", OracleDbType.Varchar2, obj.CUSUARIOAUTORIZA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FAUTORIZA", OracleDbType.Date, obj.FAUTORIZA, ParameterDirection.Input));

    }
}
