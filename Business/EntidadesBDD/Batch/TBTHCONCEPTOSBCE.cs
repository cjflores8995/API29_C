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
    public class TBTHCONCEPTOSBCE
    {
        #region variables

        public String CBCE { get; set; }
        public String DESCRIPCION { get; set; }
        public Int32? CFIT { get; set; }

        #endregion variables

        #region metodos

        public List<TBTHCONCEPTOSBCE> Listar()
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<TBTHCONCEPTOSBCE> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CBCE, ");
                query.Append(" DESCRIPCION, ");
                query.Append(" CFIT ");
                query.Append(" FROM TBTHCONCEPTOSBCE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<TBTHCONCEPTOSBCE>();
                    while (reader.Read())
                    {
                        ltObj.Add(new TBTHCONCEPTOSBCE
                        {
                            CBCE = reader["CBCE"].ToString(),
                            DESCRIPCION = reader["DESCRIPCION"].ToString(),
                            CFIT = Util.ConvertirNumero(reader["CFIT"].ToString())
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
