using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WebUsuario
    {
        public CanalRespuesta ValidaUsuario(string usuario, string clave, string sesion, string ip, out TSISUSUARIO objUsuario, out TSISTERMINAL objTerminal, out List<VSISUSUARIOTRANSACCIONES> ltTransacciones, out List<VSISUSUARIOMENU> ltMenu)
        {
            CanalRespuesta respuesta = new CanalRespuesta();
            TSISUSUARIOPASSWORD objPassword = new TSISUSUARIOPASSWORD();
            TSISUSUARIOSESION objSesion = new TSISUSUARIOSESION();
            objTerminal = new TSISTERMINAL();
            objUsuario = new TSISUSUARIO();
            ltTransacciones = new List<VSISUSUARIOTRANSACCIONES>();
            ltMenu = new List<VSISUSUARIOMENU>();

            try
            {
                objTerminal = ValidaTerminal(usuario, ip);
                if (objTerminal != null)
                {
                    objUsuario = ExisteUsuario(usuario);
                    if (objUsuario != null)
                    {
                        switch (objUsuario.CTIPOUSUARIO)
                        {
                            #region Valida FitBank
                            case "FIT":
                                wsS29.uciMethods ws = new wsS29.uciMethods();
                                wsS29.Iso8583 isoUsuario = new wsS29.Iso8583();
                                try
                                {
                                    isoUsuario.ISO_000_Message_Type = "1200";
                                    isoUsuario.ISO_002_PAN = objUsuario.CUSUARIO;
                                    isoUsuario.ISO_003_ProcessingCode = "970000";
                                    isoUsuario.ISO_004_AmountTransaction = 0;
                                    isoUsuario.ISO_007_TransDatetime = DateTime.Now;
                                    isoUsuario.ISO_011_SysAuditNumber = Util.GetSecuencial(10);
                                    isoUsuario.ISO_012_LocalDatetime = DateTime.Now;
                                    isoUsuario.ISO_015_SettlementDatel = DateTime.Now;
                                    isoUsuario.ISO_024_NetworkId = "555551";
                                    isoUsuario.ISO_018_MerchantType = "0007";
                                    isoUsuario.ISO_041_CardAcceptorID = "00000001";
                                    isoUsuario.ISO_052_PinBlock = Util.EncriptaFitBank(clave);
                                    isoUsuario = ws.ProcessingTransactionISO_WEB(isoUsuario);
                                    if (isoUsuario.ISO_039_ResponseCode == "000")
                                    {
                                        ltTransacciones = TraeTransacciones(usuario);
                                        ltMenu = TraeMenu(usuario);
                                    }
                                    respuesta.CError = isoUsuario.ISO_039_ResponseCode;
                                    respuesta.DError = isoUsuario.ISO_039p_ResponseDetail;
                                }
                                catch (Exception ex)
                                {
                                    respuesta.CError = "999";
                                    respuesta.DError = "ERROR VALIDACION FITBANK: " + Util.ReturnExceptionString(ex);
                                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                                }
                                break;
                            #endregion Valida FitBank
                            #region Validacion Normal
                            default:
                                try
                                {
                                    if (objUsuario.CESTADOUSUARIO == "ACT" || objUsuario.CESTADOUSUARIO == "CMB")
                                    {
                                        objPassword = ValidaPassword(usuario);
                                        if (objPassword != null)
                                        {
                                            objSesion = TraeSesion(objUsuario.CUSUARIO, objTerminal.CTERMINAL, sesion);
                                            if (objSesion != null)
                                            {
                                                if (objPassword.PASSWORD == Util.EncriptaFitBank(clave))
                                                {
                                                    if (Util.RestarFechasEnDias(Convert.ToDateTime(objPassword.FCADUCA), DateTime.Now) < 0)
                                                    {
                                                        ltTransacciones = TraeTransacciones(usuario);
                                                        ltMenu = TraeMenu(usuario);
                                                        if (ltTransacciones != null)
                                                        {
                                                            objSesion.SESION = sesion;
                                                            objSesion.INTENTOS = 0;
                                                            objSesion.Actualizar(objSesion);
                                                            respuesta.CError = "000";
                                                            respuesta.DError = "TRANSACCION REALIZADA CORRECTAMENTE";
                                                        }
                                                        else
                                                        {
                                                            objSesion.FFIN = DateTime.Now;
                                                            objSesion.Caducar(objSesion);
                                                            respuesta.CError = "999";
                                                            respuesta.DError = "USUARIO NO TIENE TRANSACCIONES ASIGNADAS";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        objSesion.FFIN = DateTime.Now;
                                                        objSesion.Caducar(objSesion);
                                                        respuesta.CError = "001";
                                                        respuesta.DError = "CAMBIO DE CLAVE";
                                                    }
                                                }
                                                else
                                                {
                                                    int intentos = objSesion.INTENTOS.Value + 1;

                                                    if (intentos == 3)
                                                    {
                                                        objUsuario.Bloquear(objUsuario.CUSUARIO);
                                                        objSesion.Caducar(objSesion);
                                                        respuesta.CError = "999";
                                                        respuesta.DError = "NUMERO DE INTENTOS EXCEDIDO USUARIO BLOQUEADO";
                                                    }
                                                    else
                                                    {
                                                        objSesion.SESION = "CLAVE INCORRECTA";
                                                        objSesion.INTENTOS = intentos;
                                                        objSesion.Actualizar(objSesion);
                                                        respuesta.CError = "999";
                                                        respuesta.DError = "CLAVE INCORRECTA";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                respuesta.CError = "999";
                                                respuesta.DError = "ERROR CREANDO SESSION";
                                            }
                                        }
                                        else
                                        {
                                            respuesta.CError = "999";
                                            respuesta.DError = "USUARIO NO REGISTRA CLAVE";
                                        }
                                    }
                                    else
                                    {
                                        respuesta.CError = "999";
                                        respuesta.DError = "USUARIO BLOQUEADO O INACTIVO";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    respuesta.CError = "999";
                                    respuesta.DError = "ERROR VALIDACION DEFAULT: " + Util.ReturnExceptionString(ex);
                                    Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                                }
                                break;
                            #endregion Validacion Normal
                        }
                    }
                    else
                    {
                        respuesta.CError = "999";
                        respuesta.DError = "USUARIO NO ENCONTRADO";
                    }
                }
                else
                {
                    respuesta.CError = "999";
                    respuesta.DError = "TERMINAL " + ip + " NO ENCONTRADA";
                }
            }
            catch (Exception ex)
            {
                respuesta.CError = "999";
                respuesta.DError = "ERROR EN SISTEMA: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return respuesta;
        }

        public CanalRespuesta ValidaUsuarioFacturacion(string usuario, string clave, string sesion, string ip, out TSISUSUARIO objUsuario)
        {
            CanalRespuesta objRespuesta = new CanalRespuesta();

            try
            {
                objUsuario = ExisteUsuario(usuario);
                if (objUsuario != null)
                {

                }
                else
                {
                    objRespuesta.CError = "999";
                    objRespuesta.DError = "USUARIO NO ENCONTRADO";
                }
            }
            catch (Exception ex)
            {
                objUsuario = null;
                objRespuesta.CError = "999";
                objRespuesta.DError = "ERROR EN SISTEMA: " + Util.ReturnExceptionString(ex);
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
            return objRespuesta;
        }

        public TSISTERMINAL ValidaTerminal(string usuario, string ip)
        {
            TSISTERMINAL objTerminal = new TSISTERMINAL();
            if (usuario == "admin")
            {
                ip = ConfigurationManager.AppSettings["defaulIp"];
            }
            objTerminal = objTerminal.Listar(ip);
            return objTerminal;
        }

        public TSISUSUARIO ExisteUsuario(string usuario)
        {
            return new TSISUSUARIO().Listar(usuario);
        }

        public TSISUSUARIOPASSWORD ValidaPassword(string usuario)
        {
            TSISUSUARIOPASSWORD objPassword = new TSISUSUARIOPASSWORD();
            objPassword = objPassword.ListarPasswordUsuario(usuario).Where(x => x.CUSUARIO == usuario).FirstOrDefault();
            return objPassword;
        }

        public List<VSISUSUARIOTRANSACCIONES> TraeTransacciones(string usuario)
        {
            VSISUSUARIOTRANSACCIONES objTransaccion = new VSISUSUARIOTRANSACCIONES();
            List<VSISUSUARIOTRANSACCIONES> VSisUsuarioTransacciones = null;
            VSisUsuarioTransacciones = objTransaccion.ListarTransaccionesUsuario(usuario);
            return VSisUsuarioTransacciones;
        }

        public List<VSISUSUARIOMENU> TraeMenu(string usuario)
        {
            VSISUSUARIOMENU objMenu = new VSISUSUARIOMENU();
            List<VSISUSUARIOMENU> VSisUsuarioMenu = null;
            VSisUsuarioMenu = objMenu.ListarMenuUsuario(usuario);
            return VSisUsuarioMenu;
        }

        public TSISUSUARIOSESION TraeSesion(string usuario, string terminal, string sesion)
        {
            TSISUSUARIOSESION objSesion = null;

            try
            {
                objSesion = new TSISUSUARIOSESION().TraeSesionActiva(usuario);
                if (objSesion == null)
                {
                    objSesion = new TSISUSUARIOSESION();
                    objSesion.CUSUARIO = usuario;
                    objSesion.CTERMINAL = terminal;
                    objSesion.SESION = sesion;
                    objSesion.FINICIO = DateTime.Now;
                    objSesion.FFIN = Util.FHasta();
                    objSesion.Insertar(objSesion);
                    objSesion = new TSISUSUARIOSESION().TraeSesionActiva(usuario);
                }
            }
            catch (Exception ex)
            {
                Logging.EscribirLog("", ex, "ERR");
            }
            return objSesion;
        }

        public Int32 CaducaSesion(string usuario)
        {
            TSISUSUARIOSESION objSesion = new TSISUSUARIOSESION();
            string error = string.Empty;
            int registros = 0;
            try
            {
                objSesion.CUSUARIO = usuario;
                objSesion.FFIN = DateTime.Now;
                objSesion.Caducar(objSesion);
            }
            catch (Exception ex)
            {
                registros = 0;
                error = ex.Message.ToString().ToUpper();
            }
            return registros;
        }
    }
}
