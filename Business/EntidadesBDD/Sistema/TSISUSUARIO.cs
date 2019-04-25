using AccessData;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Business
{
    public class TSISUSUARIO
    {
        #region variables

        public String CUSUARIO { get; set; }
        public String CTIPOUSUARIO { get; set; }
        public Int32? CPERSONA { get; set; }
        public String ALIAS { get; set; }
        public String EMAIL { get; set; }
        public String CELULAR { get; set; }
        public String CROL { get; set; }
        public String CESTADOUSUARIO { get; set; }
        public DateTime? FCREACION { get; set; }
        public DateTime? FMODIFICACION { get; set; }
        public String CUSUARIOMODIFICACION { get; set; }

        #endregion variables

        #region metodos

        public TSISUSUARIO Listar(string cusuario)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            TSISUSUARIO obj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CUSUARIO, ");
                query.Append(" CTIPOUSUARIO, ");
                query.Append(" CPERSONA, ");
                query.Append(" ALIAS, ");
                query.Append(" EMAIL, ");
                query.Append(" CELULAR, ");
                query.Append(" CROL, ");
                query.Append(" CESTADOUSUARIO, ");
                query.Append(" FCREACION, ");
                query.Append(" FMODIFICACION, ");
                query.Append(" CUSUARIOMODIFICACION ");
                query.Append(" FROM TSISUSUARIO ");
                query.Append(" WHERE CESTADOUSUARIO <> 'ELI' ");
                query.Append(" AND CTIPOUSUARIO NOT IN ('FCT') ");
                query.Append(" AND CUSUARIO = :CUSUARIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, cusuario, ParameterDirection.Input));

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj = new TSISUSUARIO
                        {
                            CUSUARIO = reader["CUSUARIO"].ToString(),
                            CTIPOUSUARIO = reader["CTIPOUSUARIO"].ToString(),
                            CPERSONA = Util.ConvertirNumero(reader["CPERSONA"].ToString()),
                            ALIAS = reader["ALIAS"].ToString(),
                            EMAIL = reader["EMAIL"].ToString(),
                            CELULAR = reader["CELULAR"].ToString(),
                            CROL = reader["CROL"].ToString(),
                            CESTADOUSUARIO = reader["CESTADOUSUARIO"].ToString(),
                            FCREACION = Util.ConvertirFecha(reader["FCREACION"].ToString()),
                            FMODIFICACION = Util.ConvertirFecha(reader["FMODIFICACION"].ToString()),
                            CUSUARIOMODIFICACION = reader["CUSUARIOMODIFICACION"].ToString()
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

        public bool Bloquear(string cusuario)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            bool resp = false;

            try
            {
                #region armaComando

                query.Append(" UPDATE TSISUSUARIO SET ");
                query.Append(" CESTADOUSUARIO = 'BLQ' ");
                query.Append(" WHERE CUSUARIO = :CUSUARIO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("CUSUARIO", OracleDbType.Varchar2, cusuario, ParameterDirection.Input));

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
    }
}
