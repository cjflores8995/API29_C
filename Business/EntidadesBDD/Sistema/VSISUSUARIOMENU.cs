using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class VSISUSUARIOMENU
    {
        #region variables

        public String CUSUARIO { get; set; }
        public String CMENU { get; set; }
        public String MENU { get; set; }
        public String CMODULO { get; set; }
        public String MODULO { get; set; }
        public String CTRANSACCION { get; set; }
        public String TRANSACCION { get; set; }
        public String INTERNA { get; set; }
        public DateTime? FDESDE { get; set; }
        public DateTime? FHASTA { get; set; }
        public String HORADESDE { get; set; }
        public String HORAHASTA { get; set; }

        #endregion variables

        #region metodos

        public List<VSISUSUARIOMENU> ListarMenuUsuario(string cusuario)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VSISUSUARIOMENU> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CUSUARIO, ");
                query.Append(" CMENU, ");
                query.Append(" MENU, ");
                query.Append(" CMODULO, ");
                query.Append(" MODULO, ");
                query.Append(" CTRANSACCION, ");
                query.Append(" TRANSACCION, ");
                query.Append(" INTERNA, ");
                query.Append(" FDESDE, ");
                query.Append(" FHASTA, ");
                query.Append(" HORADESDE, ");
                query.Append(" HORAHASTA ");
                query.Append(" FROM VSISUSUARIOMENU ");
                query.Append(" WHERE CUSUARIO = :CUSUARIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, cusuario, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VSISUSUARIOMENU>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VSISUSUARIOMENU
                        {
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CMENU = reader["CMENU"].ToString(),
                            MENU = reader["MENU"].ToString(),
                            CMODULO = reader["CMODULO"].ToString(),
                            MODULO = reader["MODULO"].ToString(),
                            CTRANSACCION = reader["CTRANSACCION"].ToString(),
                            TRANSACCION = reader["TRANSACCION"].ToString(),
                            INTERNA = reader["INTERNA"].ToString(),
                            FDESDE = Util.ConvertirFecha(reader["FDESDE"].ToString()),
                            FHASTA = Util.ConvertirFecha(reader["FHASTA"].ToString()),
                            HORADESDE = reader["HORADESDE"].ToString(),
                            HORAHASTA = reader["HORAHASTA"].ToString()
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
    }
}
