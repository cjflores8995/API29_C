using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class ISO8583_HISTORICO
    {
        #region variables

        public String WISO_000_MESSAGE_TYPE { get; set; }
        public String WISO_BITMAP { get; set; }
        public String WISO_002_PAN { get; set; }
        public String WISO_003_PROCESSINGCODE { get; set; }
        public Decimal? WISO_004_AMOUNTTRANSACTION { get; set; }
        public Decimal? WISO_006_BILLAMOUNT { get; set; }
        public DateTime? WISO_007_TRANSDATETIME { get; set; }
        public Decimal? WISO_008_BILLFEEAMOUNT { get; set; }
        public String WISO_011_SYSAUDITNUMBER { get; set; }
        public DateTime? WISO_012_LOCALDATETIME { get; set; }
        public DateTime? WISO_013_LOCALDATE { get; set; }
        public DateTime? WISO_015_SETTLEMENTDATEL { get; set; }
        public String WISO_018_MERCHANTTYPE { get; set; }
        public String WISO_019_ACQCOUNTRYCODE { get; set; }
        public String WISO_022_POSENTRYMODE { get; set; }
        public String WISO_023_CARDSEQ { get; set; }
        public String WISO_024_NETWORKID { get; set; }
        public Decimal? WISO_028_TRANFEEAMOUNT { get; set; }
        public Decimal? WISO_029_SETTLEMENTFEE { get; set; }
        public Decimal? WISO_030_PROCFEE { get; set; }
        public String WISO_032_ACQINSID { get; set; }
        public String WISO_033_FWDINSID { get; set; }
        public String WISO_034_PANEXT { get; set; }
        public String WISO_035_TRACK2 { get; set; }
        public String WISO_036_TRACK3 { get; set; }
        public String WISO_037_RETRIEVALREFERENCENRO { get; set; }
        public String WISO_038_AUTORIZATIONNUMBER { get; set; }
        public String WISO_039_RESPONSECODE { get; set; }
        public String WISO_039P_RESPONSEDETAIL { get; set; }
        public String WISO_041_CARDACCEPTORID { get; set; }
        public String WISO_042_CARD_ACC_ID_CODE { get; set; }
        public String WISO_043_CARDACCEPTORLOC { get; set; }
        public String WISO_044_ADDRESPDATA { get; set; }
        public Int32? WISO_049_TRANCURRCODE { get; set; }
        public Int32? WISO_051_CARDCURRCODE { get; set; }
        public String WISO_052_PINBLOCK { get; set; }
        public String WISO_054_ADITIONALAMOUNTS { get; set; }
        public String WISO_055_EMV { get; set; }
        public String WISO_090_ORIGINALDATA { get; set; }
        public String WISO_102_ACCOUNTID_1 { get; set; }
        public String WISO_103_ACCOUNTID_2 { get; set; }
        public String WISO_104_TRANDESCRIPTION { get; set; }
        public String WISO_114_EXTENDEDDATA { get; set; }
        public String WISO_115_EXTENDEDDATA { get; set; }
        public String WISO_120_EXTENDEDDATA { get; set; }
        public String WISO_121_EXTENDEDDATA { get; set; }
        public String WISO_122_EXTENDEDDATA { get; set; }
        public String WISO_123_EXTENDEDDATA { get; set; }
        public String WISO_124_EXTENDEDDATA { get; set; }
        public Int32? WSISO_LOGSTATUS { get; set; }
        public DateTime? WSISO_TRANDATETIME { get; set; }
        public DateTime? WSISO_TRANDATETIMERESPONSE { get; set; }
        public Int32? WSISO_SFRETRYCOUNTS { get; set; }
        public String WSISO_FLAGSTOREFPRWARD { get; set; }
        public Int32? WISO_012_LOCALDATETIME_DECIMAL { get; set; }
        public Decimal? WISO_TEMPOTRX_VALUE { get; set; }
        public Decimal? WISO_TEMPOBDD_VALUE { get; set; }
        public Decimal? WISO_TEMPOAUT_VALUE { get; set; }
        public String WISO_IP { get; set; }
        public DateTime? WISO_FECHACOMPENSACION { get; set; }

        #endregion variables

        #region metodos

        public bool ActualizarCompensacion(ISO8583_HISTORICO obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("S29");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE ISO8583_HISTORICO SET ");
                query.Append(" WISO_FECHACOMPENSACION = :FCOMPENSACION ");
                query.Append(" WHERE wISO_002_PAN = :TARJETA ");
                query.Append(" AND TRUNC(WISO_007_TRANSDATETIME) = TRUNC(:FTRANSACCION) ");
                query.Append(" AND WISO_018_MERCHANTTYPE = '0010' ");
                query.Append(" AND WISO_024_NETWORKID = '555554' ");
                query.Append(" AND WISO_039_RESPONSECODE = '000' ");
                query.Append(" AND WISO_041_CARDACCEPTORID = :MID ");
                query.Append(" AND WISO_011_SYSAUDITNUMBER = :TRANSACCION ");
                query.Append(" AND WISO_038_AUTORIZATIONNUMBER = :AUTORIZACION ");
                query.Append(" AND WISO_FECHACOMPENSACION IS NULL ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCOMPENSACION", OracleDbType.Date, Util.FSysdate(), ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TARJETA", OracleDbType.Varchar2, obj.WISO_002_PAN, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FTRANSACCION", OracleDbType.Date, obj.WISO_007_TRANSDATETIME, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("MID", OracleDbType.Varchar2, obj.WISO_041_CARDACCEPTORID, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSACCION", OracleDbType.Varchar2, obj.WISO_011_SYSAUDITNUMBER, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("AUTORIZACION", OracleDbType.Varchar2, obj.WISO_038_AUTORIZATIONNUMBER, ParameterDirection.Input));

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

        public ISO8583_HISTORICO BuscaTransaccionXMidTransaccion(string tarjeta, DateTime? ftransaccion, string mid, string transaccion, string aprobacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("S29");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            ISO8583_HISTORICO obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" WISO_002_PAN, ");
                query.Append(" WISO_004_AMOUNTTRANSACTION, ");
                query.Append(" WISO_007_TRANSDATETIME, ");
                query.Append(" WISO_011_SYSAUDITNUMBER, ");
                query.Append(" WISO_038_AUTORIZATIONNUMBER, ");
                query.Append(" WISO_039_RESPONSECODE, ");
                query.Append(" WISO_041_CARDACCEPTORID, ");
                query.Append(" WISO_044_ADDRESPDATA, ");
                query.Append(" WISO_102_ACCOUNTID_1, ");
                query.Append(" WISO_FECHACOMPENSACION ");
                query.Append(" FROM ISO8583_HISTORICO ");
                query.Append(" WHERE WISO_002_PAN = :TARJETA ");
                query.Append(" AND TRUNC(WISO_007_TRANSDATETIME) = TRUNC(:FTRANSACCION) ");
                query.Append(" AND WISO_018_MERCHANTTYPE = '0010' ");
                query.Append(" AND WISO_024_NETWORKID = '555554' ");
                query.Append(" AND WISO_039_RESPONSECODE = '000' ");
                query.Append(" AND WISO_041_CARDACCEPTORID = :MID ");
                query.Append(" AND WISO_011_SYSAUDITNUMBER = :TRANSACCION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("TARJETA", OracleDbType.Varchar2, tarjeta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FTRANSACCION", OracleDbType.Date, ftransaccion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("MID", OracleDbType.Varchar2, mid, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSACCION", OracleDbType.Varchar2, transaccion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ISO8583_HISTORICO
                        {
                            WISO_002_PAN = reader["WISO_002_PAN"].ToString(),
                            WISO_004_AMOUNTTRANSACTION = Util.ConvertirDecimal(reader["WISO_004_AMOUNTTRANSACTION"].ToString()),
                            WISO_007_TRANSDATETIME = Util.ConvertirFecha(reader["WISO_007_TRANSDATETIME"].ToString()),
                            WISO_011_SYSAUDITNUMBER = reader["WISO_011_SYSAUDITNUMBER"].ToString(),
                            WISO_038_AUTORIZATIONNUMBER = reader["WISO_038_AUTORIZATIONNUMBER"].ToString(),
                            WISO_039_RESPONSECODE = reader["WISO_039_RESPONSECODE"].ToString(),
                            WISO_041_CARDACCEPTORID = reader["WISO_041_CARDACCEPTORID"].ToString(),
                            WISO_044_ADDRESPDATA = reader["WISO_044_ADDRESPDATA"].ToString(),
                            WISO_102_ACCOUNTID_1 = reader["WISO_102_ACCOUNTID_1"].ToString(),
                            WISO_FECHACOMPENSACION = Util.ConvertirFecha(reader["WISO_FECHACOMPENSACION"].ToString())
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

        public ISO8583_HISTORICO BuscaTransaccionXAprobacion(string tarjeta, DateTime? ftransaccion, string mid, string transaccion, string aprobacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("S29");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            ISO8583_HISTORICO obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" WISO_002_PAN, ");
                query.Append(" WISO_004_AMOUNTTRANSACTION, ");
                query.Append(" WISO_007_TRANSDATETIME, ");
                query.Append(" WISO_011_SYSAUDITNUMBER, ");
                query.Append(" WISO_038_AUTORIZATIONNUMBER, ");
                query.Append(" WISO_039_RESPONSECODE, ");
                query.Append(" WISO_041_CARDACCEPTORID, ");
                query.Append(" WISO_044_ADDRESPDATA, ");
                query.Append(" WISO_102_ACCOUNTID_1, ");
                query.Append(" WISO_FECHACOMPENSACION ");
                query.Append(" FROM ISO8583_HISTORICO ");
                query.Append(" WHERE wISO_002_PAN = :TARJETA ");
                query.Append(" AND TRUNC(WISO_007_TRANSDATETIME) = TRUNC(:FTRANSACCION) ");
                query.Append(" AND WISO_018_MERCHANTTYPE = '0010' ");
                query.Append(" AND WISO_024_NETWORKID = '555554' ");
                query.Append(" AND WISO_039_RESPONSECODE = '000' ");
                query.Append(" AND WISO_038_AUTORIZATIONNUMBER = :TRANSACCION ");
                //query.Append(" AND WISO_011_SYSAUDITNUMBER = :TRANSACCION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("TARJETA", OracleDbType.Varchar2, tarjeta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FTRANSACCION", OracleDbType.Date, ftransaccion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSACCION", OracleDbType.Varchar2, aprobacion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ISO8583_HISTORICO
                        {
                            WISO_002_PAN = reader["WISO_002_PAN"].ToString(),
                            WISO_004_AMOUNTTRANSACTION = Util.ConvertirDecimal(reader["WISO_004_AMOUNTTRANSACTION"].ToString()),
                            WISO_007_TRANSDATETIME = Util.ConvertirFecha(reader["WISO_007_TRANSDATETIME"].ToString()),
                            WISO_011_SYSAUDITNUMBER = reader["WISO_011_SYSAUDITNUMBER"].ToString(),
                            WISO_038_AUTORIZATIONNUMBER = reader["WISO_038_AUTORIZATIONNUMBER"].ToString(),
                            WISO_039_RESPONSECODE = reader["WISO_039_RESPONSECODE"].ToString(),
                            WISO_041_CARDACCEPTORID = reader["WISO_041_CARDACCEPTORID"].ToString(),
                            WISO_044_ADDRESPDATA = reader["WISO_044_ADDRESPDATA"].ToString(),
                            WISO_102_ACCOUNTID_1 = reader["WISO_102_ACCOUNTID_1"].ToString(),
                            WISO_FECHACOMPENSACION = Util.ConvertirFecha(reader["WISO_FECHACOMPENSACION"].ToString())
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

        public ISO8583_HISTORICO BuscaTransaccionXTransaccion(string tarjeta, DateTime? ftransaccion, string mid, string transaccion, string aprobacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("S29");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            ISO8583_HISTORICO obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" WISO_002_PAN, ");
                query.Append(" WISO_004_AMOUNTTRANSACTION, ");
                query.Append(" WISO_007_TRANSDATETIME, ");
                query.Append(" WISO_011_SYSAUDITNUMBER, ");
                query.Append(" WISO_038_AUTORIZATIONNUMBER, ");
                query.Append(" WISO_039_RESPONSECODE, ");
                query.Append(" WISO_041_CARDACCEPTORID, ");
                query.Append(" WISO_044_ADDRESPDATA, ");
                query.Append(" WISO_102_ACCOUNTID_1, ");
                query.Append(" WISO_FECHACOMPENSACION ");
                query.Append(" FROM ISO8583_HISTORICO ");
                query.Append(" WHERE wISO_002_PAN = :TARJETA ");
                query.Append(" AND TRUNC(WISO_007_TRANSDATETIME) = TRUNC(:FTRANSACCION) ");
                query.Append(" AND WISO_018_MERCHANTTYPE = '0010' ");
                query.Append(" AND WISO_024_NETWORKID = '555554' ");
                query.Append(" AND WISO_039_RESPONSECODE = '000' ");
                query.Append(" AND WISO_011_SYSAUDITNUMBER = :TRANSACCION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("TARJETA", OracleDbType.Varchar2, tarjeta, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FTRANSACCION", OracleDbType.Date, ftransaccion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSACCION", OracleDbType.Varchar2, aprobacion, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new ISO8583_HISTORICO
                        {
                            WISO_002_PAN = reader["WISO_002_PAN"].ToString(),
                            WISO_004_AMOUNTTRANSACTION = Util.ConvertirDecimal(reader["WISO_004_AMOUNTTRANSACTION"].ToString()),
                            WISO_007_TRANSDATETIME = Util.ConvertirFecha(reader["WISO_007_TRANSDATETIME"].ToString()),
                            WISO_011_SYSAUDITNUMBER = reader["WISO_011_SYSAUDITNUMBER"].ToString(),
                            WISO_038_AUTORIZATIONNUMBER = reader["WISO_038_AUTORIZATIONNUMBER"].ToString(),
                            WISO_039_RESPONSECODE = reader["WISO_039_RESPONSECODE"].ToString(),
                            WISO_041_CARDACCEPTORID = reader["WISO_041_CARDACCEPTORID"].ToString(),
                            WISO_044_ADDRESPDATA = reader["WISO_044_ADDRESPDATA"].ToString(),
                            WISO_102_ACCOUNTID_1 = reader["WISO_102_ACCOUNTID_1"].ToString(),
                            WISO_FECHACOMPENSACION = Util.ConvertirFecha(reader["WISO_FECHACOMPENSACION"].ToString())
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

        /// <summary>
        /// <description>Metodo que devuelve la tarjeta del usuario en caso de que Coincida con los parametros</description>
        /// </summary>
        /// <returns>String</returns>
        public String obtenerTarjetaFavorita(DateTime? ftransaccion, String numeroAprobacion, String tarjetaUltimosDigitos)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("S29");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            String numeroTarjeta = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" WISO_002_PAN ");
                query.Append(" FROM ISO8583_HISTORICO ");
                query.Append(" WHERE WISO_018_MERCHANTTYPE = '0010' ");
                query.Append(" AND WISO_039_RESPONSECODE = '000' ");
                query.Append(" AND TRUNC(WISO_007_TRANSDATETIME) = TRUNC(:FTRANSACCION) ");
                query.Append(" AND WISO_038_AUTORIZATIONNUMBER = :NUMEROAPROBACION ");
                query.Append(" AND WISO_024_NETWORKID = '555554' ");
                query.Append(" AND WISO_002_PAN LIKE '%' || :ULTIMOSDIGITOS ");
                query.Append(" AND ROWNUM = 1 ");

                #endregion armaComando

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FTRANSACCION", OracleDbType.Date, ftransaccion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TRANSACCION", OracleDbType.Varchar2, numeroAprobacion, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ULTIMOSDIGITOS", OracleDbType.Varchar2, tarjetaUltimosDigitos, ParameterDirection.Input));


                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        numeroTarjeta = reader["WISO_002_PAN"].ToString();
                    }
                }

                #endregion ejecutaComando

                ado.CerrarConexion();

            }
            catch (Exception ex)
            {
                numeroTarjeta = null;
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name, ex, "ERR");

            }
            finally
            {
                ado.CerrarConexion();
            }

            return numeroTarjeta ?? "0000000000000000";
        }

        #endregion metodos

        //query.Append(" WISO_000_MESSAGE_TYPE, ");
        //query.Append(" WISO_BITMAP, ");
        //query.Append(" WISO_002_PAN, ");
        //query.Append(" WISO_003_PROCESSINGCODE, ");
        //query.Append(" WISO_004_AMOUNTTRANSACTION, ");
        //query.Append(" WISO_006_BILLAMOUNT, ");
        //query.Append(" WISO_007_TRANSDATETIME, ");
        //query.Append(" WISO_008_BILLFEEAMOUNT, ");
        //query.Append(" WISO_011_SYSAUDITNUMBER, ");
        //query.Append(" WISO_012_LOCALDATETIME, ");
        //query.Append(" WISO_013_LOCALDATE, ");
        //query.Append(" WISO_015_SETTLEMENTDATEL, ");
        //query.Append(" WISO_018_MERCHANTTYPE, ");
        //query.Append(" WISO_019_ACQCOUNTRYCODE, ");
        //query.Append(" WISO_022_POSENTRYMODE, ");
        //query.Append(" WISO_023_CARDSEQ, ");
        //query.Append(" WISO_024_NETWORKID, ");
        //query.Append(" WISO_028_TRANFEEAMOUNT, ");
        //query.Append(" WISO_029_SETTLEMENTFEE, ");
        //query.Append(" WISO_030_PROCFEE, ");
        //query.Append(" WISO_032_ACQINSID, ");
        //query.Append(" WISO_033_FWDINSID, ");
        //query.Append(" WISO_034_PANEXT, ");
        //query.Append(" WISO_035_TRACK2, ");
        //query.Append(" WISO_036_TRACK3, ");
        //query.Append(" WISO_037_RETRIEVALREFERENCENRO, ");
        //query.Append(" WISO_038_AUTORIZATIONNUMBER, ");
        //query.Append(" WISO_039_RESPONSECODE, ");
        //query.Append(" WISO_039P_RESPONSEDETAIL, ");
        //query.Append(" WISO_041_CARDACCEPTORID, ");
        //query.Append(" WISO_042_CARD_ACC_ID_CODE, ");
        //query.Append(" WISO_043_CARDACCEPTORLOC, ");
        //query.Append(" WISO_044_ADDRESPDATA, ");
        //query.Append(" WISO_049_TRANCURRCODE, ");
        //query.Append(" WISO_051_CARDCURRCODE, ");
        //query.Append(" WISO_052_PINBLOCK, ");
        //query.Append(" WISO_054_ADITIONALAMOUNTS, ");
        //query.Append(" WISO_055_EMV, ");
        //query.Append(" WISO_090_ORIGINALDATA, ");
        //query.Append(" WISO_102_ACCOUNTID_1, ");
        //query.Append(" WISO_103_ACCOUNTID_2, ");
        //query.Append(" WISO_104_TRANDESCRIPTION, ");
        //query.Append(" WISO_114_EXTENDEDDATA, ");
        //query.Append(" WISO_115_EXTENDEDDATA, ");
        //query.Append(" WISO_120_EXTENDEDDATA, ");
        //query.Append(" WISO_121_EXTENDEDDATA, ");
        //query.Append(" WISO_122_EXTENDEDDATA, ");
        //query.Append(" WISO_123_EXTENDEDDATA, ");
        //query.Append(" WISO_124_EXTENDEDDATA, ");
        //query.Append(" WSISO_LOGSTATUS, ");
        //query.Append(" WSISO_TRANDATETIME, ");
        //query.Append(" WSISO_TRANDATETIMERESPONSE, ");
        //query.Append(" WSISO_SFRETRYCOUNTS, ");
        //query.Append(" WSISO_FLAGSTOREFPRWARD, ");
        //query.Append(" WISO_012_LOCALDATETIME_DECIMAL, ");
        //query.Append(" WISO_TEMPOTRX_VALUE, ");
        //query.Append(" WISO_TEMPOBDD_VALUE, ");
        //query.Append(" WISO_TEMPOAUT_VALUE, ");
        //query.Append(" WISO_IP, ");
        //query.Append(" WISO_FECHACOMPENSACION, ");

        //WISO_052_PINBLOCK = reader["WISO_052_PINBLOCK"].ToString(),
        //WISO_054_ADITIONALAMOUNTS = reader["WISO_054_ADITIONALAMOUNTS"].ToString(),
        //WISO_055_EMV = reader["WISO_055_EMV"].ToString(),
        //WISO_090_ORIGINALDATA = reader["WISO_090_ORIGINALDATA"].ToString(),
        //WISO_102_ACCOUNTID_1 = reader["WISO_102_ACCOUNTID_1"].ToString(),
        //WISO_103_ACCOUNTID_2 = reader["WISO_103_ACCOUNTID_2"].ToString(),
        //WISO_104_TRANDESCRIPTION = reader["WISO_104_TRANDESCRIPTION"].ToString(),
        //WISO_114_EXTENDEDDATA = reader["WISO_114_EXTENDEDDATA"].ToString(),
        //WISO_115_EXTENDEDDATA = reader["WISO_115_EXTENDEDDATA"].ToString(),
        //WISO_120_EXTENDEDDATA = reader["WISO_120_EXTENDEDDATA"].ToString(),
        //WISO_121_EXTENDEDDATA = reader["WISO_121_EXTENDEDDATA"].ToString(),
        //WISO_122_EXTENDEDDATA = reader["WISO_122_EXTENDEDDATA"].ToString(),
        //WISO_123_EXTENDEDDATA = reader["WISO_123_EXTENDEDDATA"].ToString(),
        //WISO_124_EXTENDEDDATA = reader["WISO_124_EXTENDEDDATA"].ToString(),
        //WSISO_LOGSTATUS = Util.ConvertirNumero(reader["WSISO_LOGSTATUS"].ToString()),
        //WSISO_TRANDATETIME = Util.ConvertirFecha(reader["WSISO_TRANDATETIME"].ToString()),
        //WSISO_TRANDATETIMERESPONSE = Util.ConvertirFecha(reader["WSISO_TRANDATETIMERESPONSE"].ToString()),
        //WSISO_SFRETRYCOUNTS = Util.ConvertirNumero(reader["WSISO_SFRETRYCOUNTS"].ToString()),
        //WSISO_FLAGSTOREFPRWARD = reader["WSISO_FLAGSTOREFPRWARD"].ToString(),
        //WISO_012_LOCALDATETIME_DECIMAL = Util.ConvertirNumero(reader["WISO_012_LOCALDATETIME_DECIMAL"].ToString()),
        //WISO_TEMPOTRX_VALUE = Util.ConvertirDecimal(reader["WISO_TEMPOTRX_VALUE"].ToString()),
        //WISO_TEMPOBDD_VALUE = Util.ConvertirDecimal(reader["WISO_TEMPOBDD_VALUE"].ToString()),
        //WISO_TEMPOAUT_VALUE = Util.ConvertirDecimal(reader["WISO_TEMPOAUT_VALUE"].ToString()),
        //WISO_IP = reader["WISO_IP"].ToString(),
        //WISO_FECHACOMPENSACION = Util.ConvertirFecha(reader["WISO_FECHACOMPENSACION"].ToString()),
    }
}
