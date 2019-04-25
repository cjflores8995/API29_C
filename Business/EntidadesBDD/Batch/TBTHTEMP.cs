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
    public class TBTHTEMP
    {
        #region propiedades

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
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

        #endregion propiedades

        #region metodos

        public bool Insertar(TBTHTEMP obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TBTHTEMP (");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
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
                query.Append(" ) VALUES ( ");
                query.Append(" :FPROCESO, ");
                query.Append(" :CPROCESO, ");
                query.Append(" :CTIPOTRANSACCION, ");
                query.Append(" :CUSUARIO, ");
                query.Append(" :CSUCURSAL, ");
                query.Append(" :COFICINA, ");
                query.Append(" :CPERSONA, ");
                query.Append(" :IDENTIFICACION, ");
                query.Append(" :CUENTA, ");
                query.Append(" :RUBRO, ");
                query.Append(" :VALOR, ");
                query.Append(" :VALORPENDIENTE, ");
                query.Append(" :REFERENCIA, ");
                query.Append(" :CCONCEPTO, ");
                query.Append(" :SECUENCIABLQ, ");
                query.Append(" :PROCESA, ");
                query.Append(" :DEBITOTOTAL, ");
                query.Append(" :DESCRIPCIONMOVIMIENTO, ");
                query.Append(" :PARAMETROS, ");
                query.Append(" :NUMEROMENSAJE_ORIGINAL, ");
                query.Append(" :CSUBSISTEMA_ORIGINAL, ");
                query.Append(" :CTRANSACCION_ORIGINAL, ");
                query.Append(" :CUSUARIO_ORIGINAL, ");
                query.Append(" :NUMEROMENSAJE, ");
                query.Append(" :CESTADO, ");
                query.Append(" :CERROR, ");
                query.Append(" :DERROR, ");
                query.Append(" :FCARGA, ");
                query.Append(" :FACTUALIZACION, ");
                query.Append(" :SERVIDOR ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, obj.CTIPOTRANSACCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, obj.CUSUARIO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUENTA", OracleDbType.Varchar2, obj.CUENTA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("RUBRO", OracleDbType.Int32, obj.RUBRO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALOR", OracleDbType.Decimal, obj.VALOR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("VALORPENDIENTE", OracleDbType.Varchar2, obj.VALORPENDIENTE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("REFERENCIA", OracleDbType.Varchar2, obj.REFERENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONCEPTO", OracleDbType.Varchar2, obj.CCONCEPTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIABLQ", OracleDbType.Int32, obj.SECUENCIABLQ, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("PROCESA", OracleDbType.Varchar2, obj.PROCESA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DEBITOTOTAL", OracleDbType.Varchar2, obj.DEBITOTOTAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DESCRIPCIONMOVIMIENTO", OracleDbType.Varchar2, obj.DESCRIPCIONMOVIMIENTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("PARAMETROS", OracleDbType.Varchar2, obj.PARAMETROS, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE_ORIGINAL", OracleDbType.Varchar2, obj.NUMEROMENSAJE_ORIGINAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUBSISTEMA_ORIGINAL", OracleDbType.Varchar2, obj.CSUBSISTEMA_ORIGINAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CTRANSACCION_ORIGINAL", OracleDbType.Varchar2, obj.CTRANSACCION_ORIGINAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUSUARIO_ORIGINAL", OracleDbType.Varchar2, obj.CUSUARIO_ORIGINAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FACTUALIZACION", OracleDbType.Date, obj.FACTUALIZACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SERVIDOR", OracleDbType.Varchar2, obj.SERVIDOR, ParameterDirection.Input));

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

        public List<TBTHTEMP> ListarPendientesBloqueos(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHTEMP> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
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
                query.Append(" FROM TBTHPROCESODETALLE ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, CPROCESO, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHTEMP>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHTEMP
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
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

        public bool Borra(DateTime? fechaProceso, Int32? CPROCESO)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" DELETE TBTHTEMP ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

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

        #endregion metodos

        //query.Append(" FPROCESO, ");
        //query.Append(" CPROCESO, ");
        //query.Append(" CTIPOTRANSACCION, ");
        //query.Append(" CUSUARIO, ");
        //query.Append(" CSUCURSAL, ");
        //query.Append(" COFICINA, ");
        //query.Append(" CPERSONA, ");
        //query.Append(" IDENTIFICACION, ");
        //query.Append(" CUENTA, ");
        //query.Append(" RUBRO, ");
        //query.Append(" VALOR, ");
        //query.Append(" VALORPENDIENTE, ");
        //query.Append(" REFERENCIA, ");
        //query.Append(" CCONCEPTO, ");
        //query.Append(" SECUENCIABLQ, ");
        //query.Append(" PROCESA, ");
        //query.Append(" DEBITOTOTAL, ");
        //query.Append(" DESCRIPCIONMOVIMIENTO, ");
        //query.Append(" PARAMETROS, ");
        //query.Append(" NUMEROMENSAJE_ORIGINAL, ");
        //query.Append(" CSUBSISTEMA_ORIGINAL, ");
        //query.Append(" CTRANSACCION_ORIGINAL, ");
        //query.Append(" CUSUARIO_ORIGINAL, ");
        //query.Append(" NUMEROMENSAJE, ");
        //query.Append(" CESTADO, ");
        //query.Append(" CERROR, ");
        //query.Append(" DERROR, ");
        //query.Append(" FCARGA, ");
        //query.Append(" FACTUALIZACION, ");
        //query.Append(" SERVIDOR, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
        //CUSUARIO = reader["CUSUARIO"].ToString(),
        //CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
        //COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
        //CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
        //IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //CUENTA = reader["CUENTA"].ToString(),
        //RUBRO = Util.ConvertirNumero(reader["RUBRO"].ToString()),
        //VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),
        //VALORPENDIENTE = Util.ConvertirDecimal(reader["VALORPENDIENTE"].ToString()),
        //REFERENCIA = reader["REFERENCIA"].ToString(),
        //CCONCEPTO = reader["CCONCEPTO"].ToString(),
        //SECUENCIABLQ = Util.ConvertirNumero(reader["SECUENCIABLQ"].ToString()),
        //PROCESA = reader["PROCESA"].ToString(),
        //DEBITOTOTAL = reader["DEBITOTOTAL"].ToString(),
        //DESCRIPCIONMOVIMIENTO = reader["DESCRIPCIONMOVIMIENTO"].ToString(),
        //PARAMETROS = reader["PARAMETROS"].ToString(),
        //NUMEROMENSAJE_ORIGINAL = reader["NUMEROMENSAJE_ORIGINAL"].ToString(),
        //CSUBSISTEMA_ORIGINAL = reader["CSUBSISTEMA_ORIGINAL"].ToString(),
        //CTRANSACCION_ORIGINAL = reader["CTRANSACCION_ORIGINAL"].ToString(),
        //CUSUARIO_ORIGINAL = reader["CUSUARIO_ORIGINAL"].ToString(),
        //NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
        //CESTADO = reader["CESTADO"].ToString(),
        //CERROR = reader["CERROR"].ToString(),
        //DERROR = reader["DERROR"].ToString(),
        //FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //FACTUALIZACION = Util.ConvertirFecha(reader["FACTUALIZACION"].ToString()),
        //SERVIDOR = reader["SERVIDOR"].ToString(),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" CTIPOTRANSACCION = :CTIPOTRANSACCION, ");
        //query.Append(" CUSUARIO = :CUSUARIO, ");
        //query.Append(" CSUCURSAL = :CSUCURSAL, ");
        //query.Append(" COFICINA = :COFICINA, ");
        //query.Append(" CPERSONA = :CPERSONA, ");
        //query.Append(" IDENTIFICACION = :IDENTIFICACION, ");
        //query.Append(" CUENTA = :CUENTA, ");
        //query.Append(" RUBRO = :RUBRO, ");
        //query.Append(" VALOR = :VALOR, ");
        //query.Append(" VALORPENDIENTE = :VALORPENDIENTE, ");
        //query.Append(" REFERENCIA = :REFERENCIA, ");
        //query.Append(" CCONCEPTO = :CCONCEPTO, ");
        //query.Append(" SECUENCIABLQ = :SECUENCIABLQ, ");
        //query.Append(" PROCESA = :PROCESA, ");
        //query.Append(" DEBITOTOTAL = :DEBITOTOTAL, ");
        //query.Append(" DESCRIPCIONMOVIMIENTO = :DESCRIPCIONMOVIMIENTO, ");
        //query.Append(" PARAMETROS = :PARAMETROS, ");
        //query.Append(" NUMEROMENSAJE_ORIGINAL = :NUMEROMENSAJE_ORIGINAL, ");
        //query.Append(" CSUBSISTEMA_ORIGINAL = :CSUBSISTEMA_ORIGINAL, ");
        //query.Append(" CTRANSACCION_ORIGINAL = :CTRANSACCION_ORIGINAL, ");
        //query.Append(" CUSUARIO_ORIGINAL = :CUSUARIO_ORIGINAL, ");
        //query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");
        //query.Append(" CESTADO = :CESTADO, ");
        //query.Append(" CERROR = :CERROR, ");
        //query.Append(" DERROR = :DERROR, ");
        //query.Append(" FCARGA = :FCARGA, ");
        //query.Append(" FACTUALIZACION = :FACTUALIZACION, ");
        //query.Append(" SERVIDOR = :SERVIDOR, ");
    }
}
