using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class VFECOMPROBANTES
    {
        public String ORIGEN { get; set; }
        public String CTIPODOCUMENTO { get; set; }
        public String TIPODOCUMENTO { get; set; }
        public String NUMERODOCUMENTO { get; set; }
        public DateTime? FEMISION { get; set; }
        public DateTime? FENVIO { get; set; }
        public String CLAVEACCESO { get; set; }
        public String NUMEROAUTORIZACION { get; set; }
        public Int32? VERIFICADOR { get; set; }
        public String DOCUMENTOSUSTENTO { get; set; }
        public String RESPUESTA { get; set; }
        public String IDENTIFICACION { get; set; }
        public String NOMBRE { get; set; }
        public String EMAIL { get; set; }

        public string linkPdf { get; set; }
        public string linkXml { get; set; }

        public Int32 ContarPendientes()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            Int32 registros = 0;

            try
            {
                #region armacomando

                query.Append(" SELECT COUNT(*) REGISTROS ");
                query.Append(" FROM VFECOMPROBANTES VC ");
                query.Append(" WHERE NOT EXISTS ( SELECT 'X' FROM TBTHFACTURACION BF ");
                query.Append(" WHERE VC.CTIPODOCUMENTO = BF.CTIPODOCUMENTO ");
                query.Append(" AND VC.NUMERODOCUMENTO = BF.NUMERODOCUMENTO ) ");

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

        public List<VFECOMPROBANTES> ListarPendientes(int desde, int hasta, string criterios)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VFECOMPROBANTES> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" ORIGEN, ");
                query.Append(" CTIPODOCUMENTO, ");
                query.Append(" TIPODOCUMENTO, ");
                query.Append(" NUMERODOCUMENTO, ");
                query.Append(" FEMISION, ");
                query.Append(" FENVIO, ");
                query.Append(" CLAVEACCESO, ");
                query.Append(" NUMEROAUTORIZACION, ");
                query.Append(" VERIFICADOR, ");
                query.Append(" DOCUMENTOSUSTENTO, ");
                query.Append(" RESPUESTA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" EMAIL ");
                query.Append(" FROM VFECOMPROBANTES VC ");
                query.Append(" WHERE ROWNUM BETWEEN :DESDE AND :HASTA ");
                query.Append(" AND NUMEROAUTORIZACION IS NOT NULL ");
                query.Append(" AND NOT EXISTS ( SELECT 'X' FROM TBTHFACTURACION BF ");
                query.Append(" WHERE VC.CTIPODOCUMENTO = BF.CTIPODOCUMENTO ");
                query.Append(" AND VC.NUMERODOCUMENTO = BF.NUMERODOCUMENTO ) ");

                if (!string.IsNullOrEmpty(criterios))
                {
                    query.Append(criterios);
                }

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
                    ltObj = new List<VFECOMPROBANTES>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VFECOMPROBANTES
                        {
                            ORIGEN = reader["ORIGEN"].ToString(),
                            CTIPODOCUMENTO = reader["CTIPODOCUMENTO"].ToString(),
                            TIPODOCUMENTO = reader["TIPODOCUMENTO"].ToString(),
                            NUMERODOCUMENTO = reader["NUMERODOCUMENTO"].ToString(),
                            FEMISION = Util.ConvertirFecha(reader["FEMISION"].ToString()),
                            FENVIO = Util.ConvertirFecha(reader["FENVIO"].ToString()),
                            CLAVEACCESO = reader["CLAVEACCESO"].ToString(),
                            NUMEROAUTORIZACION = reader["NUMEROAUTORIZACION"].ToString(),
                            VERIFICADOR = Util.ConvertirNumero(reader["VERIFICADOR"].ToString()),
                            DOCUMENTOSUSTENTO = reader["DOCUMENTOSUSTENTO"].ToString(),
                            RESPUESTA = reader["RESPUESTA"].ToString(),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            EMAIL = reader["EMAIL"].ToString()
                        });
                    }

                    ado.CerrarConexion();
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

        public List<VFECOMPROBANTES> ListarComprobantes(string identificacion, string tipo, string comprobante, string fdesde, string fhasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VFECOMPROBANTES> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" ORIGEN, ");
                query.Append(" CTIPODOCUMENTO, ");
                query.Append(" TIPODOCUMENTO, ");
                query.Append(" NUMERODOCUMENTO, ");
                query.Append(" FEMISION, ");
                query.Append(" FENVIO, ");
                query.Append(" CLAVEACCESO, ");
                query.Append(" NUMEROAUTORIZACION, ");
                query.Append(" VERIFICADOR, ");
                query.Append(" DOCUMENTOSUSTENTO, ");
                query.Append(" RESPUESTA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRE, ");
                query.Append(" EMAIL ");
                query.Append(" FROM VFECOMPROBANTES VC ");
                query.Append(" WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(identificacion))
                {
                    query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");
                }

                if (!string.IsNullOrEmpty(tipo))
                {
                    query.Append(" AND CTIPODOCUMENTO = :CTIPODOCUMENTO ");
                }

                if (!string.IsNullOrEmpty(comprobante))
                {
                    query.Append(" AND NUMERODOCUMENTO = :NUMERODOCUMENTO ");
                }

                if (!string.IsNullOrEmpty(fdesde) && !string.IsNullOrEmpty(fhasta))
                {
                    query.Append(" AND FEMISION BETWEEN :FDESDE AND :FHASTA ");
                }

                query.Append(" ORDER BY FEMISION DESC ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (!string.IsNullOrEmpty(identificacion))
                {
                    comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));
                }

                if (!string.IsNullOrEmpty(tipo))
                {
                    comando.Parameters.Add(new OracleParameter("CTIPODOCUMENTO", OracleDbType.Varchar2, tipo, ParameterDirection.Input));
                }

                if (!string.IsNullOrEmpty(comprobante))
                {
                    comando.Parameters.Add(new OracleParameter("NUMERODOCUMENTO", OracleDbType.Varchar2, comprobante, ParameterDirection.Input));
                }

                if (!string.IsNullOrEmpty(fdesde) && !string.IsNullOrEmpty(fhasta))
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, Convert.ToDateTime(fdesde), ParameterDirection.Input));
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, Convert.ToDateTime(fhasta), ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VFECOMPROBANTES>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VFECOMPROBANTES
                        {
                            ORIGEN = reader["ORIGEN"].ToString(),
                            CTIPODOCUMENTO = reader["CTIPODOCUMENTO"].ToString(),
                            TIPODOCUMENTO = reader["TIPODOCUMENTO"].ToString(),
                            NUMERODOCUMENTO = reader["NUMERODOCUMENTO"].ToString(),
                            FEMISION = Util.ConvertirFecha(reader["FEMISION"].ToString()),
                            FENVIO = Util.ConvertirFecha(reader["FENVIO"].ToString()),
                            CLAVEACCESO = reader["CLAVEACCESO"].ToString(),
                            NUMEROAUTORIZACION = reader["NUMEROAUTORIZACION"].ToString(),
                            VERIFICADOR = Util.ConvertirNumero(reader["VERIFICADOR"].ToString()),
                            DOCUMENTOSUSTENTO = reader["DOCUMENTOSUSTENTO"].ToString(),
                            RESPUESTA = reader["RESPUESTA"].ToString(),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            EMAIL = reader["EMAIL"].ToString()
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

        //query.Append(" ORIGEN, ");
        //query.Append(" CTIPODOCUMENTO, ");
        //query.Append(" TIPODOCUMENTO, ");
        //query.Append(" NUMERODOCUMENTO, ");
        //query.Append(" FEMISION, ");
        //query.Append(" FENVIO, ");
        //query.Append(" CLAVEACCESO, ");
        //query.Append(" NUMEROAUTORIZACION, ");
        //query.Append(" VERIFICADOR, ");
        //query.Append(" DOCUMENTOSUSTENTO, ");
        //query.Append(" RESPUESTA, ");
        //query.Append(" IDENTIFICACION, ");
        //query.Append(" NOMBRE, ");
        //query.Append(" EMAIL, ");

        //ORIGEN = reader["ORIGEN"].ToString(),
        //CTIPODOCUMENTO = reader["CTIPODOCUMENTO"].ToString(),
        //TIPODOCUMENTO = reader["TIPODOCUMENTO"].ToString(),
        //NUMERODOCUMENTO = reader["NUMERODOCUMENTO"].ToString(),
        //FEMISION = Util.ConvertirFecha(reader["FEMISION"].ToString()),
        //FENVIO = Util.ConvertirFecha(reader["FENVIO"].ToString()),
        //CLAVEACCESO = reader["CLAVEACCESO"].ToString(),
        //NUMEROAUTORIZACION = reader["NUMEROAUTORIZACION"].ToString(),
        //VERIFICADOR = Util.ConvertirNumero(reader["VERIFICADOR"].ToString()),
        //DOCUMENTOSUSTENTO = reader["DOCUMENTOSUSTENTO"].ToString(),
        //RESPUESTA = reader["RESPUESTA"].ToString(),
        //IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //NOMBRE = reader["NOMBRE"].ToString(),
        //EMAIL = reader["EMAIL"].ToString(),

        //query.Append(" ORIGEN = :ORIGEN, ");
        //query.Append(" CTIPODOCUMENTO = :CTIPODOCUMENTO, ");
        //query.Append(" TIPODOCUMENTO = :TIPODOCUMENTO, ");
        //query.Append(" NUMERODOCUMENTO = :NUMERODOCUMENTO, ");
        //query.Append(" FEMISION = :FEMISION, ");
        //query.Append(" FENVIO = :FENVIO, ");
        //query.Append(" CLAVEACCESO = :CLAVEACCESO, ");
        //query.Append(" NUMEROAUTORIZACION = :NUMEROAUTORIZACION, ");
        //query.Append(" VERIFICADOR = :VERIFICADOR, ");
        //query.Append(" DOCUMENTOSUSTENTO = :DOCUMENTOSUSTENTO, ");
        //query.Append(" RESPUESTA = :RESPUESTA, ");
        //query.Append(" IDENTIFICACION = :IDENTIFICACION, ");
        //query.Append(" NOMBRE = :NOMBRE, ");
        //query.Append(" EMAIL = :EMAIL, ");

        //comando.Parameters.Add(new OracleParameter("ORIGEN", OracleDbType.Varchar2, obj.ORIGEN, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTIPODOCUMENTO", OracleDbType.Varchar2, obj.CTIPODOCUMENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TIPODOCUMENTO", OracleDbType.Varchar2, obj.TIPODOCUMENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMERODOCUMENTO", OracleDbType.Varchar2, obj.NUMERODOCUMENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FEMISION", OracleDbType.Date, obj.FEMISION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FENVIO", OracleDbType.Date, obj.FENVIO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CLAVEACCESO", OracleDbType.Varchar2, obj.CLAVEACCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMEROAUTORIZACION", OracleDbType.Varchar2, obj.NUMEROAUTORIZACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("VERIFICADOR", OracleDbType.Int32, obj.VERIFICADOR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DOCUMENTOSUSTENTO", OracleDbType.Varchar2, obj.DOCUMENTOSUSTENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("RESPUESTA", OracleDbType.Varchar2, obj.RESPUESTA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NOMBRE", OracleDbType.Varchar2, obj.NOMBRE, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("EMAIL", OracleDbType.Varchar2, obj.EMAIL, ParameterDirection.Input));

    }
}
