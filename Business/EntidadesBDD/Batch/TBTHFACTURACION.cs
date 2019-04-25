using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TBTHFACTURACION
    {
        public String CTIPODOCUMENTO { get; set; }
        public String NUMERODOCUMENTO { get; set; }
        public DateTime? FPROCESO { get; set; }
        public String ERRORNOTIFICACION { get; set; }
        public String ERRORPDF { get; set; }
        public String ERRORXML { get; set; }

        public bool Insertar(TBTHFACTURACION obj)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region arma comando

                query.Append(" INSERT INTO TBTHFACTURACION ( ");
                query.Append(" CTIPODOCUMENTO, ");
                query.Append(" NUMERODOCUMENTO, ");
                query.Append(" FPROCESO, ");
                query.Append(" ERRORNOTIFICACION, ");
                query.Append(" ERRORPDF, ");
                query.Append(" ERRORXML ");
                query.Append(" ) VALUES ( ");
                query.Append(" :CTIPODOCUMENTO, ");
                query.Append(" :NUMERODOCUMENTO, ");
                query.Append(" :FPROCESO, ");
                query.Append(" :ERRORNOTIFICACION, ");
                query.Append(" :ERRORPDF, ");
                query.Append(" :ERRORXML ");
                query.Append(" ) ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CTIPODOCUMENTO", OracleDbType.Varchar2, obj.CTIPODOCUMENTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NUMERODOCUMENTO", OracleDbType.Varchar2, obj.NUMERODOCUMENTO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERRORNOTIFICACION", OracleDbType.Varchar2, obj.ERRORNOTIFICACION, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERRORPDF", OracleDbType.Varchar2, obj.ERRORPDF, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("ERRORXML", OracleDbType.Varchar2, obj.ERRORXML, ParameterDirection.Input));

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

        //query.Append(" CTIPODOCUMENTO, ");
        //query.Append(" NUMERODOCUMENTO, ");
        //query.Append(" FPROCESO, ");
        //query.Append(" ERRORNOTIFICACION, ");
        //query.Append(" ERRORPDF, ");
        //query.Append(" ERRORXML, ");

        //CTIPODOCUMENTO = reader["CTIPODOCUMENTO"].ToString(),
        //NUMERODOCUMENTO = reader["NUMERODOCUMENTO"].ToString(),
        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //ERRORNOTIFICACION = reader["ERRORNOTIFICACION"].ToString(),
        //ERRORPDF = reader["ERRORPDF"].ToString(),
        //ERRORXML = reader["ERRORXML"].ToString(),

        //query.Append(" CTIPODOCUMENTO = :CTIPODOCUMENTO, ");
        //query.Append(" NUMERODOCUMENTO = :NUMERODOCUMENTO, ");
        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" ERRORNOTIFICACION = :ERRORNOTIFICACION, ");
        //query.Append(" ERRORPDF = :ERRORPDF, ");
        //query.Append(" ERRORXML = :ERRORXML, ");

        //comando.Parameters.Add(new OracleParameter("CTIPODOCUMENTO", OracleDbType.Varchar2, obj.CTIPODOCUMENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NUMERODOCUMENTO", OracleDbType.Varchar2, obj.NUMERODOCUMENTO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ERRORNOTIFICACION", OracleDbType.Varchar2, obj.ERRORNOTIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ERRORPDF", OracleDbType.Varchar2, obj.ERRORPDF, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ERRORXML", OracleDbType.Varchar2, obj.ERRORXML, ParameterDirection.Input));

    }
}
