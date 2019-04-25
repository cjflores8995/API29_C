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
    public class VFITOFICINAS
    {
        #region variables

        public Int32? CSUCURSAL { get; set; }
        public String SUCURSAL { get; set; }
        public Int32? COFICINA { get; set; }
        public String OFICINA { get; set; }
        public String CIUDAD { get; set; }

        #endregion variables

        #region metodos

        public List<VFITOFICINAS> Listar(string coficina)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VFITOFICINAS> ltObj = new List<VFITOFICINAS>();

            try
            {
                #region armaComando

                query.Append(" SELECT  ");
                query.Append(" CSUCURSAL, ");
                query.Append(" SUCURSAL, ");
                query.Append(" COFICINA, ");
                query.Append(" OFICINA, ");
                query.Append(" CIUDAD ");
                query.Append(" FROM VFITOFICINAS ");
                query.Append(" WHERE 1 = 1 ");

                if (!string.IsNullOrEmpty(coficina))
                {
                    query.Append(" AND COFICINA = :COFICINA ");
                }

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (!string.IsNullOrEmpty(coficina))
                {
                    comando.Parameters.Add(new OracleParameter("COFICINA", OracleDbType.Varchar2, coficina, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VFITOFICINAS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VFITOFICINAS
                        {
                            CSUCURSAL = Util.ConvertirNumero(reader["CSUCURSAL"].ToString()),
                            SUCURSAL = reader["SUCURSAL"].ToString(),
                            COFICINA = Util.ConvertirNumero(reader["COFICINA"].ToString()),
                            OFICINA = reader["OFICINA"].ToString(),
                            CIUDAD = reader["CIUDAD"].ToString()
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
