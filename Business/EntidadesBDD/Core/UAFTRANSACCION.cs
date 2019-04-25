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
    public class UAFTRANSACCION
    {
        public Int32? CODCOMPANIA { get; set; }
        public String TID { get; set; }
        public String IDE { get; set; }
        public DateTime? FTR { get; set; }
        public String NTR { get; set; }
        public String NCO { get; set; }
        public Int32? VDE { get; set; }
        public Int32? VCR { get; set; }
        public Int32? VEF { get; set; }
        public Int32? VCH { get; set; }
        public Int32? VVT { get; set; }
        public String MND { get; set; }
        public String TTR { get; set; }
        public String NOB { get; set; }
        public String PDO { get; set; }
        public String IDO { get; set; }
        public String COB { get; set; }
        public String CAT { get; set; }
        public String CCT { get; set; }
        public String CDR { get; set; }
        public DateTime? FCT { get; set; }
        public Int32? TRX { get; set; }
        public Int32? TDE { get; set; }
        public Int32? TCR { get; set; }
        public Int32? TEF { get; set; }
        public Int32? TCH { get; set; }
        public Int32? TVT { get; set; }

        public List<UAFTRANSACCION> Listar(DateTime fcorte, string tid, string ide, string nco)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            List<UAFTRANSACCION> ltObj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" CODCOMPANIA, ");
                query.Append(" TID, ");
                query.Append(" IDE, ");
                query.Append(" FTR, ");
                query.Append(" NTR, ");
                query.Append(" NCO, ");
                query.Append(" VDE, ");
                query.Append(" VCR, ");
                query.Append(" VEF, ");
                query.Append(" VCH, ");
                query.Append(" VVT, ");
                query.Append(" MND, ");
                query.Append(" TTR, ");
                query.Append(" NOB, ");
                query.Append(" PDO, ");
                query.Append(" IDO, ");
                query.Append(" COB, ");
                query.Append(" CAT, ");
                query.Append(" CCT, ");
                query.Append(" CDR, ");
                query.Append(" FCT ");
                query.Append(" FROM TRANSACCIONES_UAF_XML@SIFCO ");
                query.Append(" WHERE CODCOMPANIA = 1 ");
                query.Append(" AND FCT = :FCT ");
                query.Append(" AND TID = :TID ");
                query.Append(" AND IDE = :IDE ");
                query.Append(" AND NCO = :NCO ");

                comando.CommandType = CommandType.Text;
                comando.CommandText = query.ToString();

                comando.Parameters.Add(new OracleParameter("FCT", OracleDbType.Date, fcorte, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("TID", OracleDbType.Varchar2, tid, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("IDE", OracleDbType.Varchar2, ide, ParameterDirection.Input));
                comando.Parameters.Add(new OracleParameter("NCO", OracleDbType.Varchar2, nco, ParameterDirection.Input));

                #endregion armacomando

                #region ejecutaComando

                ado.AbrirConexion();
                OracleDataReader reader = ado.EjecutarSentencia(comando);

                if (reader.HasRows)
                {
                    ltObj = new List<UAFTRANSACCION>();
                    while (reader.Read())
                    {
                        ltObj.Add(new UAFTRANSACCION
                        {
                            CODCOMPANIA = Util.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
                            TID = reader["TID"].ToString(),
                            IDE = reader["IDE"].ToString(),
                            FTR = Util.ConvertirFecha(reader["FTR"].ToString()),
                            NTR = reader["NTR"].ToString(),
                            NCO = reader["NCO"].ToString(),
                            VDE = Util.ConvertirNumero(reader["VDE"].ToString()),
                            VCR = Util.ConvertirNumero(reader["VCR"].ToString()),
                            VEF = Util.ConvertirNumero(reader["VEF"].ToString()),
                            VCH = Util.ConvertirNumero(reader["VCH"].ToString()),
                            VVT = Util.ConvertirNumero(reader["VVT"].ToString()),
                            MND = reader["MND"].ToString(),
                            TTR = reader["TTR"].ToString(),
                            NOB = reader["NOB"].ToString(),
                            PDO = reader["PDO"].ToString(),
                            IDO = reader["IDO"].ToString(),
                            COB = reader["COB"].ToString(),
                            CAT = reader["CAT"].ToString(),
                            CCT = reader["CCT"].ToString(),
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

        public UAFTRANSACCION Totales(DateTime fcorte)
        {
            AccesoDatosOracle ado = new AccesoDatosOracle();
            OracleCommand comando = new OracleCommand();
            StringBuilder query = new StringBuilder();
            UAFTRANSACCION obj = null;

            try
            {
                #region armacomando

                query.Append(" SELECT ");
                query.Append(" COUNT(*) TRX, ");
                query.Append(" SUM(VDE) TDE, ");
                query.Append(" SUM(VCR) TCR, ");
                query.Append(" SUM(VEF) TEF, ");
                query.Append(" SUM(VCH) TCH, ");
                query.Append(" SUM(VVT) TVT ");
                query.Append(" FROM TRANSACCIONES_UAF_XML@SIFCO ");
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
                    while (reader.Read())
                    {
                        obj = new UAFTRANSACCION
                        {
                            TRX = Util.ConvertirNumero(reader["TRX"].ToString()),
                            TDE = Util.ConvertirNumero(reader["TDE"].ToString()),
                            TCR = Util.ConvertirNumero(reader["TCR"].ToString()),
                            TEF = Util.ConvertirNumero(reader["TEF"].ToString()),
                            TCH = Util.ConvertirNumero(reader["TCH"].ToString()),
                            TVT = Util.ConvertirNumero(reader["TVT"].ToString())
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



        //query.Append(" CODCOMPANIA, ");
        //query.Append(" TID, ");
        //query.Append(" IDE, ");
        //query.Append(" FTR, ");
        //query.Append(" NTR, ");
        //query.Append(" NCO, ");
        //query.Append(" VDE, ");
        //query.Append(" VCR, ");
        //query.Append(" VEF, ");
        //query.Append(" VCH, ");
        //query.Append(" VVT, ");
        //query.Append(" MND, ");
        //query.Append(" TTR, ");
        //query.Append(" NOB, ");
        //query.Append(" PDO, ");
        //query.Append(" IDO, ");
        //query.Append(" COB, ");
        //query.Append(" CAT, ");
        //query.Append(" CCT, ");
        //query.Append(" CDR, ");
        //query.Append(" FCT, ");

        //CODCOMPANIA = ado.ConvertirNumero(reader["CODCOMPANIA"].ToString()),
        //TID = reader["TID"].ToString(),
        //IDE = reader["IDE"].ToString(),
        //FTR = ado.ConvertirFecha(reader["FTR"].ToString()),
        //NTR = reader["NTR"].ToString(),
        //NCO = reader["NCO"].ToString(),
        //VDE = ado.ConvertirNumero(reader["VDE"].ToString()),
        //VCR = ado.ConvertirNumero(reader["VCR"].ToString()),
        //VEF = ado.ConvertirNumero(reader["VEF"].ToString()),
        //VCH = ado.ConvertirNumero(reader["VCH"].ToString()),
        //VVT = ado.ConvertirNumero(reader["VVT"].ToString()),
        //MND = reader["MND"].ToString(),
        //TTR = reader["TTR"].ToString(),
        //NOB = reader["NOB"].ToString(),
        //PDO = reader["PDO"].ToString(),
        //IDO = reader["IDO"].ToString(),
        //COB = reader["COB"].ToString(),
        //CAT = reader["CAT"].ToString(),
        //CCT = reader["CCT"].ToString(),
        //CDR = reader["CDR"].ToString(),
        //FCT = ado.ConvertirFecha(reader["FCT"].ToString()),

        //query.Append(" CODCOMPANIA = :CODCOMPANIA, ");
        //query.Append(" TID = :TID, ");
        //query.Append(" IDE = :IDE, ");
        //query.Append(" FTR = :FTR, ");
        //query.Append(" NTR = :NTR, ");
        //query.Append(" NCO = :NCO, ");
        //query.Append(" VDE = :VDE, ");
        //query.Append(" VCR = :VCR, ");
        //query.Append(" VEF = :VEF, ");
        //query.Append(" VCH = :VCH, ");
        //query.Append(" VVT = :VVT, ");
        //query.Append(" MND = :MND, ");
        //query.Append(" TTR = :TTR, ");
        //query.Append(" NOB = :NOB, ");
        //query.Append(" PDO = :PDO, ");
        //query.Append(" IDO = :IDO, ");
        //query.Append(" COB = :COB, ");
        //query.Append(" CAT = :CAT, ");
        //query.Append(" CCT = :CCT, ");
        //query.Append(" CDR = :CDR, ");
        //query.Append(" FCT = :FCT, ");
    }
}
