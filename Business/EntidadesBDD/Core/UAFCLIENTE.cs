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
    public class UAFCLIENTE
    {
        public Int32? CODCOMPANIA { get; set; }
        public String TID { get; set; }
        public String IDE { get; set; }
        public String NRS { get; set; }
        public String NAC { get; set; }
        public String DIR { get; set; }
        public String CCC { get; set; }
        public String AEC { get; set; }
        public Int32? IMT { get; set; }
        public String CDR { get; set; }
        public DateTime? FCT { get; set; }

        public List<UAFCLIENTE> Listar(DateTime fcorte, int desde, int hasta)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<UAFCLIENTE> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CODCOMPANIA, ");
                query.Append(" TID, ");
                query.Append(" IDE, ");
                query.Append(" NRS, ");
                query.Append(" NAC, ");
                query.Append(" DIR, ");
                query.Append(" CCC, ");
                query.Append(" AEC, ");
                query.Append(" IMT, ");
                query.Append(" CDR, ");
                query.Append(" FCT ");
                query.Append(" FROM CLIENTES_UAF_XML@SIFCO ");
                query.Append(" WHERE CODCOMPANIA = 1 ");
                query.Append(" AND FCT = :FCT ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCT", OracleDbType.Date, fcorte, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<UAFCLIENTE>();
                    while (reader.Read())
                    {
                        ltObj.Add(new UAFCLIENTE
                        {
                            CODCOMPANIA = Util.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
                            TID = reader["TID"].ToString(),
                            IDE = reader["IDE"].ToString(),
                            NRS = reader["NRS"].ToString(),
                            NAC = reader["NAC"].ToString(),
                            DIR = reader["DIR"].ToString(),
                            CCC = reader["CCC"].ToString(),
                            AEC = reader["AEC"].ToString(),
                            IMT = Util.ConvertirNumero(reader["IMT"].ToString()),
                            CDR = reader["CDR"].ToString(),
                            FCT = Util.ConvertirFecha(reader["FCT"].ToString())
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

        //query.Append(" CODCOMPANIA, ");
        //query.Append(" TID, ");
        //query.Append(" IDE, ");
        //query.Append(" NRS, ");
        //query.Append(" NAC, ");
        //query.Append(" DIR, ");
        //query.Append(" CCC, ");
        //query.Append(" AEC, ");
        //query.Append(" IMT, ");
        //query.Append(" CDR, ");
        //query.Append(" FCT, ");

        //CODCOMPANIA = ado.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
        //TID = reader["TID"].ToString(),
        //IDE = reader["IDE"].ToString(),
        //NRS = reader["NRS"].ToString(),
        //NAC = reader["NAC"].ToString(),
        //DIR = reader["DIR"].ToString(),
        //CCC = reader["CCC"].ToString(),
        //AEC = reader["AEC"].ToString(),
        //IMT = ado.ConvertirNumero(reader["IMT"].ToString()),
        //CDR = reader["CDR"].ToString(),
        //FCT = ado.ConvertirFecha(reader["FCT"].ToString()),

        //CODCOMPANIA = ado.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
        //TID = reader["TID"].ToString(),
        //IDE = reader["IDE"].ToString(),
        //NRS = reader["NRS"].ToString(),
        //NAC = reader["NAC"].ToString(),
        //DIR = reader["DIR"].ToString(),
        //CCC = reader["CCC"].ToString(),
        //AEC = reader["AEC"].ToString(),
        //IMT = ado.ConvertirNumero(reader["IMT"].ToString()),
        //CDR = reader["CDR"].ToString(),
        //FCT = ado.ConvertirFecha(reader["FCT"].ToString()),

    }
}
