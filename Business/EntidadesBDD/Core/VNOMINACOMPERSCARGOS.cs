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
    public class VNOMINACOMPERSCARGOS
    {
        #region variables

        public Int32? CODIGO { get; set; }
        public String NOMBRE { get; set; }
        public String ESTADO { get; set; }

        #endregion variables

        #region metodos

        public List<VNOMINACOMPERSCARGOS> ListarCargos(string tipo)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VNOMINACOMPERSCARGOS> ltObj = null;

            try
            {
                #region armaComando

                query.Append(" SELECT ");
                query.Append(" TO_NUMBER (COCUPACION) CODIGO, ");
                query.Append(" DESCRIPCION NOMBRE, ");
                query.Append(" CASE WHEN FHASTA = FNCFHASTA THEN 'A' ELSE 'I' END ESTADO ");
                query.Append(" FROM TOCUPACIONES ");
                query.Append(" WHERE 1 = 1 ");

                if (tipo == "V")
                {
                    query.Append(" AND TRUNC(FHASTA) = :FHASTA ");
                }
                else if (tipo == "M")
                {
                    query.Append(" AND TRUNC(FDESDE) = :FDESDE ");
                }

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                if (tipo == "V")
                {
                    comando.Parameters.Add(new OracleParameter("FHASTA", OracleDbType.Date, new DateTime(2999, 12, 31), ParameterDirection.Input));
                }
                else if (tipo == "M")
                {
                    comando.Parameters.Add(new OracleParameter("FDESDE", OracleDbType.Date, DateTime.Today, ParameterDirection.Input));
                }

                #endregion armaComando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VNOMINACOMPERSCARGOS>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VNOMINACOMPERSCARGOS
                        {
                            CODIGO = Util.ConvertirNumero(reader["CODIGO"].ToString()),
                            NOMBRE = reader["NOMBRE"].ToString(),
                            ESTADO = reader["ESTADO"].ToString()
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

        //query.Append(" COCUPACION, ");
        //query.Append(" OCUPACION, ");
        //query.Append(" FACTUALIZAOCUPACION, ");

        //COCUPACION = ado.ConvertirNumero(reader["COCUPACION"].ToString()),
        //OCUPACION = reader["OCUPACION"].ToString(),
        //FACTUALIZAOCUPACION = ado.ConvertirFecha(reader["FACTUALIZAOCUPACION"].ToString()),

    }
}
