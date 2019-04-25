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
    public class TBTHTIPOTRANSACCION
    {
        public String CTIPOTRANSACCION { get; set; }
        public String DESCRIPCION { get; set; }
        public String CSUBSISTEMA { get; set; }
        public String CTRANSACCION { get; set; }
        public String VERSIONTRANSACCION { get; set; }
        public String TIPO { get; set; }
        public String COMANDO { get; set; }

        public List<TBTHTIPOTRANSACCION> Listar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHTIPOTRANSACCION> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" CTIPOTRANSACCION, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CSUBSISTEMA, ");
                query.Append(" CTRANSACCION, ");
                query.Append(" VERSIONTRANSACCION, ");
                query.Append(" TIPO, ");
                query.Append(" COMANDO ");
                query.Append(" FROM TBTHTIPOTRANSACCION ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHTIPOTRANSACCION>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHTIPOTRANSACCION
                        {
                            CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
                            CTRANSACCION = reader["CTRANSACCION"].ToString(),
                            VERSIONTRANSACCION = reader["VERSIONTRANSACCION"].ToString(),
                            TIPO = reader["TIPO"].ToString(),
                            COMANDO = reader["COMANDO"].ToString()
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

        //query.Append(" CTIPOTRANSACCION, ");
        //query.Append(" DESCRIPCION, ");
        //query.Append(" CSUBSISTEMA, ");
        //query.Append(" CTRANSACCION, ");
        //query.Append(" VERSIONTRANSACCION, ");
        //query.Append(" TIPO, ");
        //query.Append(" COMANDO, ");

        //CTIPOTRANSACCION = reader["CTIPOTRANSACCION"].ToString(),
        //DESCRIPCION = reader["DESCRIPCION"].ToString(),
        //CSUBSISTEMA = reader["CSUBSISTEMA"].ToString(),
        //CTRANSACCION = reader["CTRANSACCION"].ToString(),
        //VERSIONTRANSACCION = reader["VERSIONTRANSACCION"].ToString(),
        //TIPO = reader["TIPO"].ToString(),
        //COMANDO = reader["COMANDO"].ToString(),

        //query.Append(" CTIPOTRANSACCION = :CTIPOTRANSACCION, ");
        //query.Append(" DESCRIPCION = :DESCRIPCION, ");
        //query.Append(" CSUBSISTEMA = :CSUBSISTEMA, ");
        //query.Append(" CTRANSACCION = :CTRANSACCION, ");
        //query.Append(" VERSIONTRANSACCION = :VERSIONTRANSACCION, ");
        //query.Append(" TIPO = :TIPO, ");
        //query.Append(" COMANDO = :COMANDO, ");

        //comando.Parameters.Add(new OracleParameter("CTIPOTRANSACCION", OracleDbType.Varchar2, obj.CTIPOTRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DESCRIPCION", OracleDbType.Varchar2, obj.DESCRIPCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CSUBSISTEMA", OracleDbType.Varchar2, obj.CSUBSISTEMA, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CTRANSACCION", OracleDbType.Varchar2, obj.CTRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("VERSIONTRANSACCION", OracleDbType.Varchar2, obj.VERSIONTRANSACCION, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TIPO", OracleDbType.Varchar2, obj.TIPO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("COMANDO", OracleDbType.Varchar2, obj.COMANDO, ParameterDirection.Input));

    }
}
