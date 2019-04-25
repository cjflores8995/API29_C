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
    public class TBTHDETALLECHEQUES
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public Int32? SECUENCIA { get; set; }
        public String CCUENTA { get; set; }
        public Decimal? MONTO { get; set; }
        public String BANCO { get; set; }
        public String NUMEROCHEQUE { get; set; }
        public String CUENTACHEQUE { get; set; }
        public string ACCION { get; set; }
        public String CESTADO { get; set; }
        public String CERROR { get; set; }
        public String DERROR { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FMODIFICACION { get; set; }
        public String NUMEROMENSAJE { get; set; }
        public String PENDIENTECOMISION { get; set; }
        public Int32? CSUCURSAL { get; set; }
        public Int32? COFICINA { get; set; }

        public TBTHTIPOTRANSACCION transaccion { get; set; }

        #endregion variables

        public TBTHDETALLECHEQUES Insertar(TBTHDETALLECHEQUES obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region arma comando

                query.Append(" INSERT INTO TBTHDETALLECHEQUES ( ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CCUENTA, ");
                query.Append(" MONTO, ");
                query.Append(" BANCO, ");
                query.Append(" NUMEROCHEQUE, ");
                query.Append(" CUENTACHEQUE, ");
                query.Append(" ACCION, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" FCARGA, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" NUMEROMENSAJE ");
                query.Append(" ) VALUES ( ");
                query.Append(" :FPROCESO, ");
                query.Append(" :CPROCESO, ");
                query.Append(" :SECUENCIA, ");
                query.Append(" :CCUENTA, ");
                query.Append(" :MONTO, ");
                query.Append(" :BANCO, ");
                query.Append(" :NUMEROCHEQUE, ");
                query.Append(" :CUENTACHEQUE, ");
                query.Append(" :ACCION, ");
                query.Append(" :CESTADO, ");
                query.Append(" :CERROR, ");
                query.Append(" :DERROR, ");
                query.Append(" :FCARGA, ");
                query.Append(" :FMODIFICACION, ");
                query.Append(" :NUMEROMENSAJE ");
                query.Append(" ) ");


                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, obj.CCUENTA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("MONTO", OracleDbType.Decimal, obj.MONTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("BANCO", OracleDbType.Varchar2, obj.BANCO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROCHEQUE", OracleDbType.Varchar2, obj.NUMEROCHEQUE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CUENTACHEQUE", OracleDbType.Varchar2, obj.CUENTACHEQUE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ACCION", OracleDbType.Varchar2, obj.ACCION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));

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
            return obj;
        }

        public Int32 ContarRegistrosProceso(TBTHPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLECHEQUES ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));

                #endregion armacomando

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

        public Int32 ContarRegistrosTerminados(TBTHPROCESO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHDETALLECHEQUES ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND CESTADO = 'TERMIN' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));

                #endregion armacomando

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

        public bool ActualizarEstado(string estadonew, string estadoold)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armacomando

                query.Append(" UPDATE TBTHDETALLECHEQUES SET ");
                query.Append(" CESTADO = :CESTADONEW ");
                query.Append(" WHERE CESTADO = :CESTADOOLD ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADONEW", OracleDbType.Varchar2, estadonew, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CESTADOOLD", OracleDbType.Varchar2, estadoold, ParameterDirection.Input));

                #endregion armacomando

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

        public bool ActualizarProceso(TBTHDETALLECHEQUES obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armacomando

                query.Append(" UPDATE TBTHDETALLECHEQUES SET ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" CERROR = :CERROR, ");
                query.Append(" DERROR = :DERROR, ");
                query.Append(" FMODIFICACION = :FMODIFICACION, ");
                query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");
                query.Append(" PENDIENTECOMISION = :PENDIENTECOMISION, ");
                query.Append(" CSUCURSAL = :CSUCURSAL, ");
                query.Append(" COFICINA = :COFICINA ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" AND SECUENCIA = :SECUENCIA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("PENDIENTECOMISION", OracleDbType.Varchar2, obj.PENDIENTECOMISION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CSUCURSAL", OracleDbType.Int32, obj.CSUCURSAL, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Int32, obj.COFICINA, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));

                #endregion armacomando

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

        public Int32 ContarPendientesProcesar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TBTHPROCESO A, TBTHDETALLECHEQUES B ");
                query.Append(" WHERE A.FPROCESO = B.FPROCESO ");
                query.Append(" AND A.CPROCESO = B.CPROCESO ");
                query.Append(" AND A.CTIPOPROCESO = 'EFECHE' ");
                query.Append(" AND A.CESTADO = 'PENPRO' ");
                query.Append(" AND B.CESTADO = 'PENPRO' ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

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

        public List<TBTHDETALLECHEQUES> ListarPendientesProcesar(int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHDETALLECHEQUES> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" B.FPROCESO, ");
                query.Append(" B.CPROCESO, ");
                query.Append(" B.SECUENCIA, ");
                query.Append(" B.CCUENTA, ");
                query.Append(" B.MONTO, ");
                query.Append(" B.BANCO, ");
                query.Append(" B.NUMEROCHEQUE, ");
                query.Append(" B.CUENTACHEQUE, ");
                query.Append(" B.ACCION, ");
                query.Append(" B.CESTADO, ");
                query.Append(" B.CERROR, ");
                query.Append(" B.DERROR, ");
                query.Append(" B.FCARGA, ");
                query.Append(" B.FMODIFICACION, ");
                query.Append(" B.NUMEROMENSAJE, ");
                query.Append(" B.PENDIENTECOMISION, ");
                query.Append(" B.CSUCURSAL, ");
                query.Append(" B.COFICINA ");
                query.Append(" FROM TBTHPROCESO A, TBTHDETALLECHEQUES B ");
                query.Append(" WHERE A.FPROCESO = B.FPROCESO ");
                query.Append(" AND A.CPROCESO = B.CPROCESO ");
                query.Append(" AND A.CESTADO = 'PENPRO' ");
                query.Append(" AND A.CTIPOPROCESO = 'EFECHE' ");
                query.Append(" AND B.CESTADO = 'PENPRO' ");
                query.Append(" AND ROWNUM BETWEEN :DESDE AND :HASTA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("DESDE", OracleDbType.Int32, desde, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("HASTA", OracleDbType.Int32, hasta, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHDETALLECHEQUES>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHDETALLECHEQUES
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            MONTO = Util.ConvertirDecimal(reader["MONTO"].ToString()),
                            BANCO = reader["BANCO"].ToString(),
                            NUMEROCHEQUE = reader["NUMEROCHEQUE"].ToString(),
                            CUENTACHEQUE = reader["CUENTACHEQUE"].ToString(),
                            ACCION = reader["ACCION"].ToString(),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),
                            PENDIENTECOMISION = reader["PENDIENTECOMISION"].ToString(),
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString())
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

        public bool Totales(DateTime? fechaProceso, Int32? CPROCESO, out Int32 registros, out Decimal total)
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

                query.Append(" SELECT COUNT (*) REGISTROS, NVL (SUM (MONTO), 0) TOTAL ");
                query.Append(" FROM TBTHDETALLECHEQUES ");
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

        //query.Append(" FPROCESO, ");
        //query.Append(" CPROCESO, ");
        //query.Append(" SECUENCIA, ");
        //query.Append(" CCUENTA, ");
        //query.Append(" MONTO, ");
        //query.Append(" BANCO, ");
        //query.Append(" NUMEROCHEQUE, ");
        //query.Append(" CUENTACHEQUE, ");
        //query.Append(" ACCION, ");
        //query.Append(" CESTADO, ");
        //query.Append(" CERROR, ");
        //query.Append(" DERROR, ");
        //query.Append(" FCARGA, ");
        //query.Append(" FMODIFICACION, ");
        //query.Append(" NUMEROMENSAJE, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
        //CCUENTA = reader["CCUENTA"].ToString(),
        //MONTO = Util.ConvertirDecimal(reader["MONTO"].ToString()),
        //BANCO = reader["BANCO"].ToString(),
        //NUMEROCHEQUE = reader["NUMEROCHEQUE"].ToString(),
        //CUENTACHEQUE = reader["CUENTACHEQUE"].ToString(),
        //ACCION = reader["ACCION"].ToString(),
        //CESTADO = reader["CESTADO"].ToString(),
        //CERROR = reader["CERROR"].ToString(),
        //DERROR = reader["DERROR"].ToString(),
        //FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
        //NUMEROMENSAJE = reader["NUMEROMENSAJE"].ToString(),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" SECUENCIA = :SECUENCIA, ");
        //query.Append(" CCUENTA = :CCUENTA, ");
        //query.Append(" MONTO = :MONTO, ");
        //query.Append(" BANCO = :BANCO, ");
        //query.Append(" NUMEROCHEQUE = :NUMEROCHEQUE, ");
        //query.Append(" CUENTACHEQUE = :CUENTACHEQUE, ");
        //query.Append(" ACCION = :ACCION, ");
        //query.Append(" CESTADO = :CESTADO, ");
        //query.Append(" CERROR = :CERROR, ");
        //query.Append(" DERROR = :DERROR, ");
        //query.Append(" FCARGA = :FCARGA, ");
        //query.Append(" FMODIFICACION = :FMODIFICACION, ");
        //query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");

        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("SECUENCIA", OracleDbType.Int32, obj.SECUENCIA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CCUENTA", OracleDbType.Varchar2, obj.CCUENTA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("MONTO", OracleDbType.Decimal, obj.MONTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("BANCO", OracleDbType.Varchar2, obj.BANCO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROCHEQUE", OracleDbType.Varchar2, obj.NUMEROCHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CUENTACHEQUE", OracleDbType.Varchar2, obj.CUENTACHEQUE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ACCION", OracleDbType.Varchar2, obj.ACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FCARGA", OracleDbType.Date, obj.FCARGA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FMODIFICACION", OracleDbType.Date, obj.FMODIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROMENSAJE", OracleDbType.Varchar2, obj.NUMEROMENSAJE, ParameterDirection.Input));

    }
}
