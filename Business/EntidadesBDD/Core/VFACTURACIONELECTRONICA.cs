using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class VFACTURACIONELECTRONICA
    {
        public String CTIPODOCUMENTOFACTURACION { get; set; }
        public String DOCUMENTOFACTURACION { get; set; }
        public String NUMERODOCUMENTO { get; set; }
        public String IDENTIFICACION { get; set; }
        public String DOCUMENTOSUSTENTO { get; set; }
        public DateTime? FECHAEMISION { get; set; }
        public String NUMEROAUTORIZACION { get; set; }
        public String ARCHIVOXML { get; set; }
        public String ARCHIVOPDF { get; set; }
        public string linkPdf { get; set; }
        public string linkXml { get; set; }

        public int ContarDocumentos()
        {
            return 10;
        }

        public List<VFACTURACIONELECTRONICA> ListarDocumentos(string identificacion, string tipo, string comprobante, string fdesde, string fhasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle("Fitbank");
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VFACTURACIONELECTRONICA> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CTIPODOCUMENTOFACTURACION, ");
                query.Append(" DOCUMENTOFACTURACION, ");
                query.Append(" NUMERODOCUMENTO, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" DOCUMENTOSUSTENTO, ");
                query.Append(" FECHAEMISION, ");
                query.Append(" NUMEROAUTORIZACION, ");
                query.Append(" ARCHIVOXML, ");
                query.Append(" ARCHIVOPDF ");
                query.Append(" FROM VFACTURACIONELECTRONICA VC ");
                query.Append(" WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(identificacion))
                {
                    query.Append(" AND IDENTIFICACION = :IDENTIFICACION ");
                }

                if (!string.IsNullOrEmpty(tipo))
                {
                    query.Append(" AND CTIPODOCUMENTOFACTURACION = :CTIPODOCUMENTOFACTURACION ");
                }

                if (!string.IsNullOrEmpty(comprobante))
                {
                    query.Append(" AND NUMERODOCUMENTO = :NUMERODOCUMENTO ");
                }

                if (!string.IsNullOrEmpty(fdesde) && !string.IsNullOrEmpty(fhasta))
                {
                    query.Append(" AND FEMISION BETWEEN :FDESDE AND :FHASTA ");
                }

                query.Append(" ORDER BY FECHAEMISION DESC ");

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
                    ltObj = new List<VFACTURACIONELECTRONICA>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VFACTURACIONELECTRONICA
                        {
                            CTIPODOCUMENTOFACTURACION = reader["CTIPODOCUMENTOFACTURACION"].ToString(),
                            DOCUMENTOFACTURACION = reader["DOCUMENTOFACTURACION"].ToString(),
                            NUMERODOCUMENTO = reader["NUMERODOCUMENTO"].ToString(),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            DOCUMENTOSUSTENTO = reader["DOCUMENTOSUSTENTO"].ToString(),
                            FECHAEMISION = Util.ConvertirFecha(reader["FECHAEMISION"].ToString()),
                            NUMEROAUTORIZACION = reader["NUMEROAUTORIZACION"].ToString(),
                            ARCHIVOXML = reader["ARCHIVOXML"].ToString(),
                            ARCHIVOPDF = reader["ARCHIVOPDF"].ToString()
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
    }
}
