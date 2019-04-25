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
    public class UAFPRODUCTO
    {
        public Int32? CODCOMPANIA { get; set; }
        public String TID { get; set; }
        public String IDE { get; set; }
        public String TCO { get; set; }
        public String NCO { get; set; }
        public String CAP { get; set; }
        public DateTime? FAC { get; set; }
        public String CDR { get; set; }
        public DateTime? FCT { get; set; }

        public List<UAFPRODUCTO> Listar(DateTime fcorte, string tid, string ide)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<UAFPRODUCTO> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CODCOMPANIA, ");
                query.Append(" TID, ");
                query.Append(" IDE, ");
                query.Append(" TCO, ");
                query.Append(" NCO, ");
                query.Append(" CAP, ");
                query.Append(" FAC, ");
                query.Append(" CDR, ");
                query.Append(" FCT ");
                query.Append(" FROM PRODUCTOS_UAF_XML@SIFCO ");
                query.Append(" WHERE CODCOMPANIA = 1 ");
                query.Append(" AND FCT = :FCT ");
                query.Append(" AND TID = :TID ");
                query.Append(" AND IDE = :IDE ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCT", OracleDbType.Date, fcorte, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TID", OracleDbType.Varchar2, tid, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDE", OracleDbType.Varchar2, ide, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<UAFPRODUCTO>();
                    while (reader.Read())
                    {
                        ltObj.Add(new UAFPRODUCTO
                        {
                            CODCOMPANIA = Util.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
                            TID = reader["TID"].ToString(),
                            IDE = reader["IDE"].ToString(),
                            TCO = reader["TCO"].ToString(),
                            NCO = reader["NCO"].ToString(),
                            CAP = reader["CAP"].ToString(),
                            FAC = Util.ConvertirFecha(reader["FAC"].ToString()),
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
        //query.Append(" TCO, ");
        //query.Append(" NCO, ");
        //query.Append(" CAP, ");
        //query.Append(" FAC, ");
        //query.Append(" CDR, ");
        //query.Append(" FCT, ");

        //CODCOMPANIA = ado.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
        //TID = reader["TID"].ToString(),
        //IDE = reader["IDE"].ToString(),
        //TCO = reader["TCO"].ToString(),
        //NCO = reader["NCO"].ToString(),
        //CAP = reader["CAP"].ToString(),
        //FAC = ado.ConvertirFecha(reader["FAC"].ToString()),
        //CDR = reader["CDR"].ToString(),
        //FCT = ado.ConvertirFecha(reader["FCT"].ToString()),

        //query.Append(" CODCOMPANIA = :CODCOMPANIA, ");
        //query.Append(" TID = :TID, ");
        //query.Append(" IDE = :IDE, ");
        //query.Append(" TCO = :TCO, ");
        //query.Append(" NCO = :NCO, ");
        //query.Append(" CAP = :CAP, ");
        //query.Append(" FAC = :FAC, ");
        //query.Append(" CDR = :CDR, ");
        //query.Append(" FCT = :FCT, ");
    }
}
