using Business;
using System;
using System.Reflection;
using System.Threading;
using System.Timers;
using timer = System.Timers;

namespace BatchProcesos
{
    class Program
    {
        #region variables

        public static Thread timerEspera = null;

        public static timer.Timer timerLecturaArchivos = null;
        public static timer.Timer timerTabuladoVista = null;
        public static timer.Timer timerTabuladoCredito = null;
        public static timer.Timer timerTabuladoBloqueos = null;
        public static timer.Timer timerTabuladoSPI3 = null;
        public static timer.Timer timerTabuladoLotes = null;
        public static timer.Timer timerEjecutaNormal = null;
        public static timer.Timer timerEjecutaBloqueos = null;
        public static timer.Timer timerEjecutaRecuperacion = null;
        public static timer.Timer timerEfectivizarCheques = null;
        public static timer.Timer timerActivaBloqueos = null;
        public static timer.Timer timerVerificaProcesos = null;
        public static timer.Timer timerFinalizaProcesos = null;

        #endregion variables

        static void Main(string[] args)
        {
            string error = "OK";
            bool flag = true;

            try
            {
                if (flag && error == "OK")
                {
                    flag = Logging.IniciaLog();
                }

                if (flag && error == "OK")
                {
                    flag = BthProcesos.CargarParametros();
                }

                if (flag && error == "OK")
                {
                    #region lectura
                    if (BthProcesos.glbLecturaActiva)
                    {
                        timerLecturaArchivos = new timer.Timer(BthProcesos.glbLecturaTiempo);
                        timerLecturaArchivos.Enabled = true;
                        timerLecturaArchivos.Elapsed += LecturaArchivos;
                    }
                    #endregion lectura

                    #region Tabulado Valida Cuentas
                    if (BthProcesos.glbTabuladoVistaActiva)
                    {
                        timerTabuladoVista = new timer.Timer(BthProcesos.glbTabuladoVistaTiempo);
                        timerTabuladoVista.Enabled = true;
                        timerTabuladoVista.Elapsed += TabuladoVista;
                    }
                    #endregion Tabulado Valida Cuentas

                    #region Tabulado Valida Credito
                    if (BthProcesos.glbTabuladoCreditoActiva)
                    {
                        timerTabuladoCredito = new timer.Timer(BthProcesos.glbTabuladoCreditoTiempo);
                        timerTabuladoCredito.Enabled = true;
                        timerTabuladoCredito.Elapsed += TabuladoCredito;
                    }
                    #endregion Tabulado Valida Credito

                    #region Tabulado Bloqueos
                    if (BthProcesos.glbTabuladoBloqueosActiva)
                    {
                        timerTabuladoBloqueos = new timer.Timer(BthProcesos.glbTabuladoBloqueosTiempo);
                        timerTabuladoBloqueos.Enabled = true;
                        timerTabuladoBloqueos.Elapsed += TabuladoBloqueos;
                    }
                    #endregion Tabulado Bloqueos

                    #region Tabulado SPI3
                    if (BthProcesos.glbTabuladoSpi3Activa)
                    {
                        timerTabuladoSPI3 = new timer.Timer(BthProcesos.glbTabuladoSpi3Tiempo);
                        timerTabuladoSPI3.Enabled = true;
                        timerTabuladoSPI3.Elapsed += TabuladoSPI3;
                    }
                    #endregion Tabulado SPI3

                    #region Tabulado Lotes
                    if (BthProcesos.glbTabuladoLotesActiva)
                    {
                        timerTabuladoLotes = new timer.Timer(BthProcesos.glbTabuladoLotesTiempo);
                        timerTabuladoLotes.Enabled = true;
                        timerTabuladoLotes.Elapsed += TabuladoLotes;
                    }
                    #endregion Tabulado Lotes

                    #region ejecuta normal
                    if (BthProcesos.glbEjecutaNormalActiva)
                    {
                        timerEjecutaNormal = new timer.Timer(BthProcesos.glbEjecutaNormalTiempo);
                        timerEjecutaNormal.Enabled = true;
                        timerEjecutaNormal.Elapsed += EjecutaNormal;
                    }
                    #endregion ejecuta normal

                    #region ejecuta bloqueos
                    if (BthProcesos.glbEjecutaBloqueosActiva)
                    {
                        timerEjecutaBloqueos = new timer.Timer(BthProcesos.glbEjecutaBloqueosTiempo);
                        timerEjecutaBloqueos.Enabled = true;
                        timerEjecutaBloqueos.Elapsed += EjecutaBloqueos;
                    }
                    #endregion ejecuta bloqueos

                    #region ejecuta recuperacion
                    if (BthProcesos.glbEjecutaRecuperacionActiva)
                    {
                        timerEjecutaRecuperacion = new timer.Timer(BthProcesos.glbEjecutaRecuperacionTiempo);
                        timerEjecutaRecuperacion.Enabled = true;
                        timerEjecutaRecuperacion.Elapsed += EjecutaRecuperacion;
                    }
                    #endregion ejecuta recuperacion

                    #region efectivizar cheques
                    if (BthProcesos.glbEfectivizarChequesActiva)
                    {
                        timerEfectivizarCheques = new timer.Timer(BthProcesos.glbEfectivizarChequesTiempo);
                        timerEfectivizarCheques.Enabled = true;
                        timerEfectivizarCheques.Elapsed += EfectivizaCheques;
                    }
                    #endregion efectivizar cheques

                    #region activaBloqueos
                    if (BthProcesos.glbActivaBlqueosActiva)
                    {
                        timerActivaBloqueos = new timer.Timer(BthProcesos.glbActivaBloqueosTiempo);
                        timerActivaBloqueos.Enabled = true;
                        timerActivaBloqueos.Elapsed += ActivaBloqueos;
                    }
                    #endregion activaBloqueos

                    #region Verifica
                    if (BthProcesos.glbVerificaActiva)
                    {
                        timerVerificaProcesos = new timer.Timer(BthProcesos.glbVerificaTiempo);
                        timerVerificaProcesos.Enabled = true;
                        timerVerificaProcesos.Elapsed += Verifica;
                    }
                    #endregion Verifica

                    #region finaliza procesos
                    if (BthProcesos.glbFinalizaActiva)
                    {
                        timerFinalizaProcesos = new timer.Timer(BthProcesos.glbFinalizaTiempo);
                        timerFinalizaProcesos.Enabled = true;
                        timerFinalizaProcesos.Elapsed += FinalizaProcesos;
                    }
                    #endregion finaliza procesos

                    //---------------------------------------------------

                    #region Mantenimiento Tarjetas
                    //if (BatchGlobal.tarjetasActiva)
                    //{
                    //    timerTarjetas = new timer.Timer(30000);
                    //    timerTarjetas.Enabled = true;
                    //    timerTarjetas.Elapsed += Tarjetas;
                    //}
                    #endregion Mantenimiento Tarjetas

                    Program obj = new Program();
                    obj.IniciaProceso();
                }

                if (!flag || error != "OK")
                {
                    Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " [ERR] " + error);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla("ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Console.ReadLine();
            }
        }

        public void IniciaProceso()
        {
            timerEspera = new Thread((ThreadStart)delegate
            {
                Inactividad();
            });

            timerEspera.Name = "timerEspera";
            timerEspera.Start();
        }

        public void Inactividad()
        {
            while (true)
            {
                Console.WriteLine(DateTime.Now.ToString("HH:mm:ss") + " ...");
                System.Threading.Thread.Sleep(1800000);
            }
        }

        public static void LecturaArchivos(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerLecturaArchivos.Stop();
                new BthProcesos().ProcesoLecturaArchivos();
                timerLecturaArchivos.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void TabuladoVista(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerTabuladoVista.Stop();
                new BthProcesos().ProcesoTabuladoVista();
                timerTabuladoVista.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void TabuladoCredito(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerTabuladoCredito.Stop();
                new BthProcesos().ProcesoTabuladoCredito();
                timerTabuladoCredito.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void TabuladoBloqueos(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerTabuladoBloqueos.Stop();
                new BthProcesos().ProcesoTabuladoBloqueos();
                timerTabuladoBloqueos.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void TabuladoSPI3(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerTabuladoSPI3.Stop();
                new BthProcesos().ProcesoTabuladoSPI3();
                timerTabuladoSPI3.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void TabuladoLotes(object sender, ElapsedEventArgs e)
        {
            try
            {
                bool flag = false;
                timerTabuladoLotes.Stop();
                new BthProcesos().ProcesoTabuladoLotes(out flag);
                if (flag)
                    timerTabuladoLotes.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void EjecutaNormal(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerEjecutaNormal.Stop();
                string criteriosProceso = " AND CESTADO = 'PENPRO' ";
                string criteriosDetalle = " AND CTIPOTRANSACCION NOT IN ('REVERSO', 'INGBLQ', 'NOTDEBMTJ', 'NOTDEBREC') ";
                new BthProcesos().ProcesoEjecuta(criteriosProceso, criteriosDetalle, BthProcesos.glbEjecutaNormalProcesos);
                timerEjecutaNormal.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void EjecutaBloqueos(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerEjecutaBloqueos.Stop();
                string criteriosProceso = " AND CESTADO = 'PENPRO' ";
                string criteriosDetalle = " AND CTIPOTRANSACCION IN ('INGBLQ') ";
                new BthProcesos().ProcesoEjecuta(criteriosProceso, criteriosDetalle, BthProcesos.glbEjecutaBloqueosProcesos);
                timerEjecutaBloqueos.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void EjecutaRecuperacion(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerEjecutaRecuperacion.Stop();
                new BthProcesos().ProcesoRecuperacion();
                timerEjecutaRecuperacion.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void EfectivizaCheques(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerEfectivizarCheques.Stop();
                new BthProcesos().ProcesoEfectivizarCheques();
                timerEfectivizarCheques.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void ActivaBloqueos(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerActivaBloqueos.Stop();
                new BthProcesos().ProcesoActivarBloqueos();
                timerActivaBloqueos.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void Verifica(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerVerificaProcesos.Stop();
                new BthProcesos().ProcesoVerifica();
                timerVerificaProcesos.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }

        public static void FinalizaProcesos(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerFinalizaProcesos.Stop();
                new BthProcesos().ProcesoFinalizaProcesos();
                timerFinalizaProcesos.Start();
            }
            catch (Exception ex)
            {
                Util.ImprimePantalla(MethodBase.GetCurrentMethod().Name, "ERROR GENERAL: " + ex.Message.ToString());
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
            }
        }
    }
}
