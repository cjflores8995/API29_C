using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class VNOTIFICACIONPERSONADATOS
    {
        public Int32? CPERSONA { get; set; }
        public String IDENTIFICACION { get; set; }
        public String NOMBRELEGAL { get; set; }
        public String CORREO { get; set; }
        public String CELULAR { get; set; }

        public VNOTIFICACIONPERSONADATOS ListarPendientes(string identificacion)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            VNOTIFICACIONPERSONADATOS obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT  ");
                query.Append(" CPERSONA, ");
                query.Append(" IDENTIFICACION, ");
                query.Append(" NOMBRELEGAL, ");
                query.Append(" CORREO, ");
                query.Append(" CELULAR ");
                query.Append(" FROM VNOTIFICACIONPERSONADATOS ");
                query.Append(" WHERE IDENTIFICACION = :IDENTIFICACION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, identificacion, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new VNOTIFICACIONPERSONADATOS
                        {
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
                            NOMBRELEGAL = reader["NOMBRELEGAL"].ToString(),
                            CORREO = reader["CORREO"].ToString(),
                            CELULAR = reader["CELULAR"].ToString()
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

        //query.Append(" CPERSONA, ");
        //query.Append(" IDENTIFICACION, ");
        //query.Append(" NOMBRELEGAL, ");
        //query.Append(" CORREO, ");
        //query.Append(" CELULAR, ");

        //CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
        //IDENTIFICACION = reader["IDENTIFICACION"].ToString(),
        //NOMBRELEGAL = reader["NOMBRELEGAL"].ToString(),
        //CORREO = reader["CORREO"].ToString(),
        //CELULAR = reader["CELULAR"].ToString(),

        //query.Append(" CPERSONA = :CPERSONA, ");
        //query.Append(" IDENTIFICACION = :IDENTIFICACION, ");
        //query.Append(" NOMBRELEGAL = :NOMBRELEGAL, ");
        //query.Append(" CORREO = :CORREO, ");
        //query.Append(" CELULAR = :CELULAR, ");

        //comando.Parameters.Add(new OracleParameter("CPERSONA", OracleDbType.Int32, obj.CPERSONA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("IDENTIFICACION", OracleDbType.Varchar2, obj.IDENTIFICACION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("NOMBRELEGAL", OracleDbType.Varchar2, obj.NOMBRELEGAL, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CORREO", OracleDbType.Varchar2, obj.CORREO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CELULAR", OracleDbType.Varchar2, obj.CELULAR, ParameterDirection.Input));

    }
}
