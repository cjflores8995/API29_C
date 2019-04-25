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
    public class TBTHTIPOARCHIVO
    {
        public String CTIPOARCHIVO { get; set; }
        public String DESCRIPCION { get; set; }
        public String ACTIVO { get; set; }
        public String WEB { get; set; }
        public String COMANDO { get; set; }

        public List<TBTHTIPOARCHIVO> Listar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHTIPOARCHIVO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CTIPOARCHIVO, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" ACTIVO, ");
                query.Append(" WEB, ");
                query.Append(" COMANDO ");
                query.Append(" FROM TBTHTIPOARCHIVO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHTIPOARCHIVO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHTIPOARCHIVO
                        {
                            CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            ACTIVO = reader["ACTIVO"].ToString(),
                            COMANDO = reader["COMANDO"].ToString(),
                            WEB = reader["WEB"].ToString()
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


        //query.Append(" CTIPOARCHIVO, ");
        //query.Append(" DESCRIPCION, ");
        //query.Append(" ACTIVO, ");
        //query.Append(" COMANDO, ");

        //CTIPOARCHIVO = reader["CTIPOARCHIVO"].ToString(),
        //DESCRIPCION = reader["DESCRIPCION"].ToString(),
        //ACTIVO = reader["ACTIVO"].ToString(),
        //COMANDO = reader["COMANDO"].ToString(),

        //query.Append(" CTIPOARCHIVO = :CTIPOARCHIVO, ");
        //query.Append(" DESCRIPCION = :DESCRIPCION, ");
        //query.Append(" ACTIVO = :ACTIVO, ");
        //query.Append(" COMANDO = :COMANDO, ");

        //comando.Parameters.Add(new OracleParameter("CTIPOARCHIVO", OracleDbType.Varchar2, obj.CTIPOARCHIVO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DESCRIPCION", OracleDbType.Varchar2, obj.DESCRIPCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ACTIVO", OracleDbType.Varchar2, obj.ACTIVO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("COMANDO", OracleDbType.Varchar2, obj.COMANDO, ParameterDirection.Input));

    }
}
