using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TSISUSUARIOPASSWORD
    {
        #region variables

        public String CUSUARIO { get; set; }
        public String PASSWORD { get; set; }
        public DateTime? FCADUCA { get; set; }
        public DateTime? FDESDE { get; set; }
        public DateTime? FHASTA { get; set; }

        #endregion variables

        #region metodos

        public List<TSISUSUARIOPASSWORD> ListarPasswordUsuario(string cusuario)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TSISUSUARIOPASSWORD> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CUSUARIO, ");
                query.Append(" PASSWORD, ");
                query.Append(" FCADUCA, ");
                query.Append(" FDESDE, ");
                query.Append(" FHASTA ");
                query.Append(" FROM TSISUSUARIOPASSWORD ");
                query.Append(" WHERE CUSUARIO = :CUSUARIO ");
                query.Append(" AND FHASTA = TO_DATE('29991231', 'YYYYMMDD') ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, cusuario, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TSISUSUARIOPASSWORD>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TSISUSUARIOPASSWORD
                        {
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            PASSWORD = reader["PASSWORD"].ToString(),
                            FCADUCA = Util.ConvertirFecha(reader["FCADUCA"].ToString()),
                            FDESDE = Util.ConvertirFecha(reader["FDESDE"].ToString()),
                            FHASTA = Util.ConvertirFecha(reader["FHASTA"].ToString())
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
