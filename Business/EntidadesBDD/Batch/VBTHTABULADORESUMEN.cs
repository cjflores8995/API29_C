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
    public class VBTHTABULADORESUMEN
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public String CESTADO { get; set; }
        public String ESTADO { get; set; }
        public Int32? REGISTROS { get; set; }
        public Decimal? TOTAL { get; set; }

        #endregion variables

        #region metodos

        public List<VBTHTABULADORESUMEN> Listar(DateTime fproceso, Int32 cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHTABULADORESUMEN> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CESTADO, ");
                query.Append(" ESTADO, ");
                query.Append(" REGISTROS, ");
                query.Append(" TOTAL ");
                query.Append(" FROM VBTHTABULADORESUMEN ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");

                query.Append(" ORDER BY ESTADO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, fproceso, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, cproceso, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<VBTHTABULADORESUMEN>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHTABULADORESUMEN
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CESTADO = reader["CESTADO"].ToString(),
                            ESTADO = reader["ESTADO"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString())
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

        //query.Append(" FPROCESO, ");
        //query.Append(" CPROCESO, ");
        //query.Append(" CESTADO, ");
        //query.Append(" ESTADO, ");
        //query.Append(" REGISTROS, ");
        //query.Append(" TOTAL, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //CESTADO = reader["CESTADO"].ToString(),
        //ESTADO = reader["ESTADO"].ToString(),
        //REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
        //TOTAL = Util.ConvertirDecimal(reader["TOTAL"].ToString()),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" CESTADO = :CESTADO, ");
        //query.Append(" ESTADO = :ESTADO, ");
        //query.Append(" REGISTROS = :REGISTROS, ");
        //query.Append(" TOTAL = :TOTAL, ");

        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CESTADO", OracleDbType.Varchar2, obj.CESTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("ESTADO", OracleDbType.Varchar2, obj.ESTADO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("REGISTROS", OracleDbType.Int32, obj.REGISTROS, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("TOTAL", OracleDbType.Decimal, obj.TOTAL, ParameterDirection.Input));

    }
}
