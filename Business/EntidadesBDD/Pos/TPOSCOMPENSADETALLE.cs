using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TPOSCOMPENSADETALLE
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CCONVENIO { get; set; }
        public Int32? SECUENCIA { get; set; }
        public String CESTADO { get; set; }
        public String CERROR { get; set; }
        public String ARCHIVO { get; set; }
        public String MID { get; set; }
        public String TERMINAL { get; set; }
        public String TARJETA { get; set; }
        public String CCUENTA { get; set; }
        public DateTime? FTRANSACCION { get; set; }
        public DateTime? FCARGA { get; set; }
        public DateTime? FCOMPENSACION { get; set; }
        public String LOTE { get; set; }
        public String NUMEROTRANSACCION { get; set; }
        public String NUMEROAPROBACION { get; set; }
        public Decimal? VALORLIQUIDADO { get; set; }
        public Decimal? VALORTRANSACCION { get; set; }
        public Decimal? VALORIVA { get; set; }
        public Decimal? VALORTARIFA0 { get; set; }
        public Decimal? VALORTARIFAIVA { get; set; }
        public Decimal? VALORCOMISION { get; set; }
        public Decimal? VALORIVACOMISION { get; set; }
        public Decimal? VALORRETENCIONFUENTE { get; set; }
        public Decimal? VALORRETENCIONIVA { get; set; }
        public String DERROR { get; set; }
        public String LINEA { get; set; }
        public String NUMEROMENSAJE { get; set; }
        public String Estado { get; set; }

        #endregion variables

        #region metodos

        public bool Insertar(TPOSCOMPENSADETALLE obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool response = false;

            try
            {
                #region armaComando

                query.Append(" INSERT INTO TPOSCOMPENSADETALLE ( ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" ARCHIVO, ");
                query.Append(" MID, ");
                query.Append(" TERMINAL, ");
                query.Append(" TARJETA, ");
                query.Append(" CCUENTA, ");
                query.Append(" FTRANSACCION, ");
                query.Append(" FCARGA, ");
                query.Append(" FCOMPENSACION, ");
                query.Append(" LOTE, ");
                query.Append(" NUMEROTRANSACCION, ");
                query.Append(" NUMEROAPROBACION, ");
                query.Append(" VALORLIQUIDADO, ");
                query.Append(" VALORTRANSACCION, ");
                query.Append(" VALORIVA, ");
                query.Append(" VALORTARIFA0, ");
                query.Append(" VALORTARIFAIVA, ");
                query.Append(" VALORCOMISION, ");
                query.Append(" VALORIVACOMISION, ");
                query.Append(" VALORRETENCIONFUENTE, ");
                query.Append(" VALORRETENCIONIVA, ");
                query.Append(" DERROR, ");
                query.Append(" NUMEROMENSAJE, ");
                query.Append(" LINEA ");
                query.Append(" ) VALUES ( ");
                query.Append(" :FPROCESO, ");
                query.Append(" :CCONVENIO, ");
                query.Append(" :SECUENCIA, ");
                query.Append(" :CESTADO, ");
                query.Append(" :CERROR, ");
                query.Append(" :ARCHIVO, ");
                query.Append(" :MID, ");
                query.Append(" :TERMINAL, ");
                query.Append(" :TARJETA, ");
                query.Append(" :CCUENTA, ");
                query.Append(" :FTRANSACCION, ");
                query.Append(" :FCARGA, ");
                query.Append(" :FCOMPENSACION, ");
                query.Append(" :LOTE, ");
                query.Append(" :NUMEROTRANSACCION, ");
                query.Append(" :NUMEROAPROBACION, ");
                query.Append(" :VALORLIQUIDADO, ");
                query.Append(" :VALORTRANSACCION, ");
                query.Append(" :VALORIVA, ");
                query.Append(" :VALORTARIFA0, ");
                query.Append(" :VALORTARIFAIVA, ");
                query.Append(" :VALORCOMISION, ");
                query.Append(" :VALORIVACOMISION, ");
                query.Append(" :VALORRETENCIONFUENTE, ");
                query.Append(" :VALORRETENCIONIVA, ");
                query.Append(" :DERROR, ");
                query.Append(" :NUMEROMENSAJE, ");
                query.Append(" :LINEA ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter(
                    "FPROCESO",
                    OracleDbType.Date,
                    obj.FPROCESO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CCONVENIO",
                    OracleDbType.Int32,
                    obj.CCONVENIO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "SECUENCIA",
                    OracleDbType.Int32,
                    obj.SECUENCIA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CESTADO",
                    OracleDbType.Varchar2,
                    obj.CESTADO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CERROR",
                    OracleDbType.Varchar2,
                    obj.CERROR,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "ARCHIVO",
                    OracleDbType.Varchar2,
                    obj.ARCHIVO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "MID",
                    OracleDbType.Varchar2,
                    obj.MID,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "TERMINAL",
                    OracleDbType.Varchar2,
                    obj.TERMINAL,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "TARJETA",
                    OracleDbType.Varchar2,
                    obj.TARJETA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CCUENTA",
                    OracleDbType.Varchar2,
                    obj.CCUENTA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FTRANSACCION",
                    OracleDbType.Date,
                    obj.FTRANSACCION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FCARGA",
                    OracleDbType.Date,
                    obj.FCARGA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FCOMPENSACION",
                    OracleDbType.Date,
                    obj.FCOMPENSACION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "LOTE",
                    OracleDbType.Varchar2,
                    obj.LOTE,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "NUMEROTRANSACCION",
                    OracleDbType.Varchar2,
                    obj.NUMEROTRANSACCION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "NUMEROAPROBACION",
                    OracleDbType.Varchar2,
                    obj.NUMEROAPROBACION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORLIQUIDADO",
                    OracleDbType.Decimal,
                    obj.VALORLIQUIDADO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORTRANSACCION",
                    OracleDbType.Decimal,
                    obj.VALORTRANSACCION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORIVA",
                    OracleDbType.Decimal,
                    obj.VALORIVA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORTARIFA0",
                    OracleDbType.Decimal,
                    obj.VALORTARIFA0,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORTARIFAIVA",
                    OracleDbType.Decimal,
                    obj.VALORTARIFAIVA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORCOMISION",
                    OracleDbType.Decimal,
                    obj.VALORCOMISION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORIVACOMISION",
                    OracleDbType.Decimal,
                    obj.VALORIVACOMISION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORRETENCIONFUENTE",
                    OracleDbType.Decimal,
                    obj.VALORRETENCIONFUENTE,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORRETENCIONIVA",
                    OracleDbType.Decimal,
                    obj.VALORRETENCIONIVA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "DERROR",
                    OracleDbType.Varchar2,
                    obj.DERROR,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "NUMEROMENSAJE",
                    OracleDbType.Varchar2,
                    obj.NUMEROMENSAJE,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "LINEA",
                    OracleDbType.Varchar2,
                    obj.LINEA,
                    ParameterDirection.Input));

                #endregion armaComando

                #region ejecuta comando

                ado.AbrirConexion();
                response = ado.EjecutarComando(comando);

                #endregion ejecuta comando
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");
                response = false;
            }
            finally
            {
                ado.CerrarConexion();
            }
            return response;
        }

        public bool Actualizar(TPOSCOMPENSADETALLE obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TPOSCOMPENSADETALLE SET ");
                query.Append(" CESTADO = :CESTADO, ");
                query.Append(" CERROR = :CERROR, ");
                query.Append(" ARCHIVO = :ARCHIVO, ");
                query.Append(" MID = :MID, ");
                query.Append(" TERMINAL = :TERMINAL, ");
                query.Append(" TARJETA = :TARJETA, ");
                query.Append(" CCUENTA = :CCUENTA, ");
                query.Append(" FTRANSACCION = :FTRANSACCION, ");
                query.Append(" FCARGA = :FCARGA, ");
                query.Append(" FCOMPENSACION = :FCOMPENSACION, ");
                query.Append(" LOTE = :LOTE, ");
                query.Append(" NUMEROTRANSACCION = :NUMEROTRANSACCION, ");
                query.Append(" NUMEROAPROBACION = :NUMEROAPROBACION, ");
                query.Append(" VALORLIQUIDADO = :VALORLIQUIDADO, ");
                query.Append(" VALORTRANSACCION = :VALORTRANSACCION, ");
                query.Append(" VALORIVA = :VALORIVA, ");
                query.Append(" VALORTARIFA0 = :VALORTARIFA0, ");
                query.Append(" VALORTARIFAIVA = :VALORTARIFAIVA, ");
                query.Append(" VALORCOMISION = :VALORCOMISION, ");
                query.Append(" VALORIVACOMISION = :VALORIVACOMISION, ");
                query.Append(" VALORRETENCIONFUENTE = :VALORRETENCIONFUENTE, ");
                query.Append(" VALORRETENCIONIVA = :VALORRETENCIONIVA, ");
                query.Append(" DERROR = :DERROR, ");
                query.Append(" NUMEROMENSAJE = :NUMEROMENSAJE, ");
                query.Append(" LINEA = :LINEA ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");
                query.Append(" AND SECUENCIA = :SECUENCIA ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter(
                    "CESTADO",
                    OracleDbType.Varchar2,
                    obj.CESTADO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CERROR",
                    OracleDbType.Varchar2,
                    obj.CERROR,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "ARCHIVO",
                    OracleDbType.Varchar2,
                    obj.ARCHIVO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "MID",
                    OracleDbType.Varchar2,
                    obj.MID,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "TERMINAL",
                    OracleDbType.Varchar2,
                    obj.TERMINAL,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "TARJETA",
                    OracleDbType.Varchar2,
                    obj.TARJETA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CCUENTA",
                    OracleDbType.Varchar2,
                    obj.CCUENTA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FTRANSACCION",
                    OracleDbType.Date,
                    obj.FTRANSACCION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FCARGA",
                    OracleDbType.Date,
                    obj.FCARGA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FCOMPENSACION",
                    OracleDbType.Date,
                    obj.FCOMPENSACION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "LOTE",
                    OracleDbType.Varchar2,
                    obj.LOTE,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "NUMEROTRANSACCION",
                    OracleDbType.Varchar2,
                    obj.NUMEROTRANSACCION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "NUMEROAPROBACION",
                    OracleDbType.Varchar2,
                    obj.NUMEROAPROBACION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORLIQUIDADO",
                    OracleDbType.Decimal,
                    obj.VALORLIQUIDADO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORTRANSACCION",
                    OracleDbType.Decimal,
                    obj.VALORTRANSACCION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORIVA",
                    OracleDbType.Decimal,
                    obj.VALORIVA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORTARIFA0",
                    OracleDbType.Decimal,
                    obj.VALORTARIFA0,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORTARIFAIVA",
                    OracleDbType.Decimal,
                    obj.VALORTARIFAIVA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORCOMISION",
                    OracleDbType.Decimal,
                    obj.VALORCOMISION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORIVACOMISION",
                    OracleDbType.Decimal,
                    obj.VALORIVACOMISION,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORRETENCIONFUENTE",
                    OracleDbType.Decimal,
                    obj.VALORRETENCIONFUENTE,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "VALORRETENCIONIVA",
                    OracleDbType.Decimal,
                    obj.VALORRETENCIONIVA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "DERROR",
                    OracleDbType.Varchar2,
                    obj.DERROR,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "NUMEROMENSAJE",
                    OracleDbType.Varchar2,
                    obj.NUMEROMENSAJE,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "LINEA",
                    OracleDbType.Varchar2,
                    obj.LINEA,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "FPROCESO",
                    OracleDbType.Date,
                    obj.FPROCESO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "CCONVENIO",
                    OracleDbType.Int32,
                    obj.CCONVENIO,
                    ParameterDirection.Input));

                comando.Parameters.Add(new OracleParameter(
                    "SECUENCIA",
                    OracleDbType.Int32,
                    obj.SECUENCIA,
                    ParameterDirection.Input));

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

        public List<TPOSCOMPENSADETALLE> ListarDetalleProceso(DateTime? fecha, Int32? convenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TPOSCOMPENSADETALLE> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" ARCHIVO, ");
                query.Append(" MID, ");
                query.Append(" TERMINAL, ");
                query.Append(" TARJETA, ");
                query.Append(" CCUENTA, ");
                query.Append(" FTRANSACCION, ");
                query.Append(" FCARGA, ");
                query.Append(" FCOMPENSACION, ");
                query.Append(" LOTE, ");
                query.Append(" NUMEROTRANSACCION, ");
                query.Append(" NUMEROAPROBACION, ");
                query.Append(" VALORLIQUIDADO, ");
                query.Append(" VALORTRANSACCION, ");
                query.Append(" VALORIVA, ");
                query.Append(" VALORTARIFA0, ");
                query.Append(" VALORTARIFAIVA, ");
                query.Append(" VALORCOMISION, ");
                query.Append(" VALORIVACOMISION, ");
                query.Append(" VALORRETENCIONFUENTE, ");
                query.Append(" VALORRETENCIONIVA, ");
                query.Append(" DERROR, ");
                query.Append(" LINEA ");
                query.Append(" FROM TPOSCOMPENSADETALLE ");
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
                    ltObj = new List<TPOSCOMPENSADETALLE>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TPOSCOMPENSADETALLE
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            ARCHIVO = reader["ARCHIVO"].ToString(),
                            MID = reader["MID"].ToString(),
                            TERMINAL = reader["TERMINAL"].ToString(),
                            TARJETA = reader["TARJETA"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            FTRANSACCION = Util.ConvertirFecha(reader["FTRANSACCION"].ToString()),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FCOMPENSACION = Util.ConvertirFecha(reader["FCOMPENSACION"].ToString()),
                            LOTE = reader["LOTE"].ToString(),
                            NUMEROTRANSACCION = reader["NUMEROTRANSACCION"].ToString(),
                            NUMEROAPROBACION = reader["NUMEROAPROBACION"].ToString(),
                            VALORLIQUIDADO = Util.ConvertirDecimal(reader["VALORLIQUIDADO"].ToString()),
                            VALORTRANSACCION = Util.ConvertirDecimal(reader["VALORTRANSACCION"].ToString()),
                            VALORIVA = Util.ConvertirDecimal(reader["VALORIVA"].ToString()),
                            VALORTARIFA0 = Util.ConvertirDecimal(reader["VALORTARIFA0"].ToString()),
                            VALORTARIFAIVA = Util.ConvertirDecimal(reader["VALORTARIFAIVA"].ToString()),
                            VALORCOMISION = Util.ConvertirDecimal(reader["VALORCOMISION"].ToString()),
                            VALORIVACOMISION = Util.ConvertirDecimal(reader["VALORIVACOMISION"].ToString()),
                            VALORRETENCIONFUENTE = Util.ConvertirDecimal(reader["VALORRETENCIONFUENTE"].ToString()),
                            VALORRETENCIONIVA = Util.ConvertirDecimal(reader["VALORRETENCIONIVA"].ToString()),
                            DERROR = reader["DERROR"].ToString(),
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

        public List<TPOSCOMPENSADETALLE> ListarCompensadosXConvenio(DateTime? fecha, Int32? convenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TPOSCOMPENSADETALLE> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT FPROCESO, ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" ARCHIVO, ");
                query.Append(" MID, ");
                query.Append(" TERMINAL, ");
                query.Append(" TARJETA, ");
                query.Append(" CCUENTA, ");
                query.Append(" FTRANSACCION, ");
                query.Append(" FCARGA, ");
                query.Append(" FCOMPENSACION, ");
                query.Append(" LOTE, ");
                query.Append(" NUMEROTRANSACCION, ");
                query.Append(" NUMEROAPROBACION, ");
                query.Append(" VALORLIQUIDADO, ");
                query.Append(" VALORTRANSACCION, ");
                query.Append(" VALORIVA, ");
                query.Append(" VALORTARIFA0, ");
                query.Append(" VALORTARIFAIVA, ");
                query.Append(" VALORCOMISION, ");
                query.Append(" VALORIVACOMISION, ");
                query.Append(" VALORRETENCIONFUENTE, ");
                query.Append(" VALORRETENCIONIVA, ");
                query.Append(" DERROR, ");
                query.Append(" LINEA ");
                query.Append(" FROM TPOSCOMPENSADETALLE ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO IN ('CMP') ");
                query.Append(" ORDER BY FTRANSACCION ");

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
                    ltObj = new List<TPOSCOMPENSADETALLE>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TPOSCOMPENSADETALLE
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            ARCHIVO = reader["ARCHIVO"].ToString(),
                            MID = reader["MID"].ToString(),
                            TERMINAL = reader["TERMINAL"].ToString(),
                            TARJETA = reader["TARJETA"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            FTRANSACCION = Util.ConvertirFecha(reader["FTRANSACCION"].ToString()),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FCOMPENSACION = Util.ConvertirFecha(reader["FCOMPENSACION"].ToString()),
                            LOTE = reader["LOTE"].ToString(),
                            NUMEROTRANSACCION = reader["NUMEROTRANSACCION"].ToString(),
                            NUMEROAPROBACION = reader["NUMEROAPROBACION"].ToString(),
                            VALORLIQUIDADO = Util.ConvertirDecimal(reader["VALORLIQUIDADO"].ToString()),
                            VALORTRANSACCION = Util.ConvertirDecimal(reader["VALORTRANSACCION"].ToString()),
                            VALORIVA = Util.ConvertirDecimal(reader["VALORIVA"].ToString()),
                            VALORTARIFA0 = Util.ConvertirDecimal(reader["VALORTARIFA0"].ToString()),
                            VALORTARIFAIVA = Util.ConvertirDecimal(reader["VALORTARIFAIVA"].ToString()),
                            VALORCOMISION = Util.ConvertirDecimal(reader["VALORCOMISION"].ToString()),
                            VALORIVACOMISION = Util.ConvertirDecimal(reader["VALORIVACOMISION"].ToString()),
                            VALORRETENCIONFUENTE = Util.ConvertirDecimal(reader["VALORRETENCIONFUENTE"].ToString()),
                            VALORRETENCIONIVA = Util.ConvertirDecimal(reader["VALORRETENCIONIVA"].ToString()),
                            DERROR = reader["DERROR"].ToString(),
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

        public List<TPOSCOMPENSADETALLE> ListarPendientesCompensarXConvenio(DateTime? fecha, Int32? convenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TPOSCOMPENSADETALLE> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT FPROCESO, ");
                query.Append(" FPROCESO, ");
                query.Append(" CCONVENIO, ");
                query.Append(" SECUENCIA, ");
                query.Append(" CESTADO, ");
                query.Append(" CERROR, ");
                query.Append(" ARCHIVO, ");
                query.Append(" MID, ");
                query.Append(" TERMINAL, ");
                query.Append(" TARJETA, ");
                query.Append(" CCUENTA, ");
                query.Append(" FTRANSACCION, ");
                query.Append(" FCARGA, ");
                query.Append(" FCOMPENSACION, ");
                query.Append(" LOTE, ");
                query.Append(" NUMEROTRANSACCION, ");
                query.Append(" NUMEROAPROBACION, ");
                query.Append(" VALORLIQUIDADO, ");
                query.Append(" VALORTRANSACCION, ");
                query.Append(" VALORIVA, ");
                query.Append(" VALORTARIFA0, ");
                query.Append(" VALORTARIFAIVA, ");
                query.Append(" VALORCOMISION, ");
                query.Append(" VALORIVACOMISION, ");
                query.Append(" VALORRETENCIONFUENTE, ");
                query.Append(" VALORRETENCIONIVA, ");
                query.Append(" DERROR, ");
                query.Append(" LINEA ");
                query.Append(" FROM TPOSCOMPENSADETALLE ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");
                query.Append(" AND CESTADO IN ('CAR', 'REC') ");

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
                    ltObj = new List<TPOSCOMPENSADETALLE>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TPOSCOMPENSADETALLE
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
                            SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            CERROR = reader["CERROR"].ToString(),
                            ARCHIVO = reader["ARCHIVO"].ToString(),
                            MID = reader["MID"].ToString(),
                            TERMINAL = reader["TERMINAL"].ToString(),
                            TARJETA = reader["TARJETA"].ToString(),
                            CCUENTA = reader["CCUENTA"].ToString(),
                            FTRANSACCION = Util.ConvertirFecha(reader["FTRANSACCION"].ToString()),
                            FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
                            FCOMPENSACION = Util.ConvertirFecha(reader["FCOMPENSACION"].ToString()),
                            LOTE = reader["LOTE"].ToString(),
                            NUMEROTRANSACCION = reader["NUMEROTRANSACCION"].ToString(),
                            NUMEROAPROBACION = reader["NUMEROAPROBACION"].ToString(),
                            VALORLIQUIDADO = Util.ConvertirDecimal(reader["VALORLIQUIDADO"].ToString()),
                            VALORTRANSACCION = Util.ConvertirDecimal(reader["VALORTRANSACCION"].ToString()),
                            VALORIVA = Util.ConvertirDecimal(reader["VALORIVA"].ToString()),
                            VALORTARIFA0 = Util.ConvertirDecimal(reader["VALORTARIFA0"].ToString()),
                            VALORTARIFAIVA = Util.ConvertirDecimal(reader["VALORTARIFAIVA"].ToString()),
                            VALORCOMISION = Util.ConvertirDecimal(reader["VALORCOMISION"].ToString()),
                            VALORIVACOMISION = Util.ConvertirDecimal(reader["VALORIVACOMISION"].ToString()),
                            VALORRETENCIONFUENTE = Util.ConvertirDecimal(reader["VALORRETENCIONFUENTE"].ToString()),
                            VALORRETENCIONIVA = Util.ConvertirDecimal(reader["VALORRETENCIONIVA"].ToString()),
                            DERROR = reader["DERROR"].ToString(),
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

        public Int32 ContarRegistrosProceso(DateTime? fechaProceso, Int32? cconvenio)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armaComando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM TPOSCOMPENSADETALLE ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CCONVENIO = :CCONVENIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fechaProceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CCONVENIO", OracleDbType.Int32, cconvenio, ParameterDirection.Input));

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


        #endregion metodos

        //query.Append(" FPROCESO, ");
        //query.Append(" CCONVENIO, ");
        //query.Append(" SECUENCIA, ");
        //query.Append(" CESTADO, ");
        //query.Append(" CERROR, ");
        //query.Append(" ARCHIVO, ");
        //query.Append(" MID, ");
        //query.Append(" TERMINAL, ");
        //query.Append(" TARJETA, ");
        //query.Append(" CCUENTA, ");
        //query.Append(" FTRANSACCION, ");
        //query.Append(" FCARGA, ");
        //query.Append(" FCOMPENSACION, ");
        //query.Append(" LOTE, ");
        //query.Append(" NUMEROTRANSACCION, ");
        //query.Append(" NUMEROAPROBACION, ");
        //query.Append(" VALORLIQUIDADO, ");
        //query.Append(" VALORTRANSACCION, ");
        //query.Append(" VALORIVA, ");
        //query.Append(" VALORTARIFA0, ");
        //query.Append(" VALORTARIFAIVA, ");
        //query.Append(" VALORCOMISION, ");
        //query.Append(" VALORIVACOMISION, ");
        //query.Append(" VALORRETENCIONFUENTE, ");
        //query.Append(" VALORRETENCIONIVA, ");
        //query.Append(" DERROR, ");
        //query.Append(" LINEA, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CCONVENIO = Util.ConvertirNumero(reader["CCONVENIO"].ToString()),
        //SECUENCIA = Util.ConvertirNumero(reader["SECUENCIA"].ToString()),
        //CESTADO = reader["CESTADO"].ToString(),
        //CERROR = reader["CERROR"].ToString(),
        //ARCHIVO = reader["ARCHIVO"].ToString(),
        //MID = reader["MID"].ToString(),
        //TERMINAL = reader["TERMINAL"].ToString(),
        //TARJETA = reader["TARJETA"].ToString(),
        //CCUENTA = reader["CCUENTA"].ToString(),
        //FTRANSACCION = Util.ConvertirFecha(reader["FTRANSACCION"].ToString()),
        //FCARGA = Util.ConvertirFecha(reader["FCARGA"].ToString()),
        //FCOMPENSACION = Util.ConvertirFecha(reader["FCOMPENSACION"].ToString()),
        //LOTE = reader["LOTE"].ToString(),
        //NUMEROTRANSACCION = reader["NUMEROTRANSACCION"].ToString(),
        //NUMEROAPROBACION = reader["NUMEROAPROBACION"].ToString(),
        //VALORLIQUIDADO = Util.ConvertirDecimal(reader["VALORLIQUIDADO"].ToString()),
        //VALORTRANSACCION = Util.ConvertirDecimal(reader["VALORTRANSACCION"].ToString()),
        //VALORIVA = Util.ConvertirDecimal(reader["VALORIVA"].ToString()),
        //VALORTARIFA0 = Util.ConvertirDecimal(reader["VALORTARIFA0"].ToString()),
        //VALORTARIFAIVA = Util.ConvertirDecimal(reader["VALORTARIFAIVA"].ToString()),
        //VALORCOMISION = Util.ConvertirDecimal(reader["VALORCOMISION"].ToString()),
        //VALORIVACOMISION = Util.ConvertirDecimal(reader["VALORIVACOMISION"].ToString()),
        //VALORRETENCIONFUENTE = Util.ConvertirDecimal(reader["VALORRETENCIONFUENTE"].ToString()),
        //VALORRETENCIONIVA = Util.ConvertirDecimal(reader["VALORRETENCIONIVA"].ToString()),
        //DERROR = reader["DERROR"].ToString(),
        //LINEA = reader["LINEA"].ToString(),
    }
}
