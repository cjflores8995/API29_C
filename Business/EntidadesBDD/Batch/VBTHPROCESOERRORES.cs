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
    public class VBTHPROCESOERRORES
    {
        #region variables

        public DateTime? FPROCESO { get; set; }
        public Int32? CPROCESO { get; set; }
        public String CERROR { get; set; }
        public String DERROR { get; set; }
        public Int32? REGISTROS { get; set; }
        public Decimal? VALOR { get; set; }

        #endregion variables

        #region metodos

        public List<VBTHPROCESOERRORES> Listar(DateTime fproceso, Int32 cproceso)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<VBTHPROCESOERRORES> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" FPROCESO, ");
                query.Append(" CPROCESO, ");
                query.Append(" CERROR, ");
                query.Append(" DERROR, ");
                query.Append(" REGISTROS, ");
                query.Append(" VALOR ");
                query.Append(" FROM VBTHPROCESOERRORES ");
                query.Append(" WHERE FPROCESO = :FPROCESO ");
                query.Append(" AND CPROCESO = :CPROCESO ");
                query.Append(" ORDER BY CERROR ");

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
                    ltObj = new List<VBTHPROCESOERRORES>();
                    while (reader.Read())
                    {
                        ltObj.Add(new VBTHPROCESOERRORES
                        {
                            FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
                            CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
                            CERROR = reader["CERROR"].ToString(),
                            DERROR = reader["DERROR"].ToString(),
                            REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
                            VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString())
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
        //query.Append(" CERROR, ");
        //query.Append(" DERROR, ");
        //query.Append(" REGISTROS, ");
        //query.Append(" VALOR, ");

        //FPROCESO = Util.ConvertirFecha(reader["FPROCESO"].ToString()),
        //CPROCESO = Util.ConvertirNumero(reader["CPROCESO"].ToString()),
        //CERROR = reader["CERROR"].ToString(),
        //DERROR = reader["DERROR"].ToString(),
        //REGISTROS = Util.ConvertirNumero(reader["REGISTROS"].ToString()),
        //VALOR = Util.ConvertirDecimal(reader["VALOR"].ToString()),

        //query.Append(" FPROCESO = :FPROCESO, ");
        //query.Append(" CPROCESO = :CPROCESO, ");
        //query.Append(" CERROR = :CERROR, ");
        //query.Append(" DERROR = :DERROR, ");
        //query.Append(" REGISTROS = :REGISTROS, ");
        //query.Append(" VALOR = :VALOR, ");

        //comando.Parameters.Add(new OracleParameter("FPROCESO", OracleDbType.Date, obj.FPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CPROCESO", OracleDbType.Int32, obj.CPROCESO, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("CERROR", OracleDbType.Varchar2, obj.CERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("DERROR", OracleDbType.Varchar2, obj.DERROR, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("REGISTROS", OracleDbType.Int32, obj.REGISTROS, ParameterDirection.Input));
        //comando.Parameters.Add(new OracleParameter("VALOR", OracleDbType.Decimal, obj.VALOR, ParameterDirection.Input));

    }
}
