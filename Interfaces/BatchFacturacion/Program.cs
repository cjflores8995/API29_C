using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using timer = System.Timers;
using System.Configuration;

namespace BatchFacturacion
{
    class Program
    {
        public static timer.Timer timerProceso = null;
        public static timer.Timer timerVerifica = null;

        public static Thread esperaTimer = null;

        static void Main(string[] args)
        {
            Console.Title = "Batch De Facturacion - 29 De Octubre";

            bool flag = true;

            if (flag)
            {
                flag = Logging.IniciaLog();
            }

            if (flag)
            {
                flag = Facturacion.CargarParametros();
            }

            if (flag)
            {
                timerVerifica = new timer.Timer(Facturacion.verificaTime);
                timerVerifica.Enabled = true;
                timerVerifica.Elapsed += Verifica;

                timerProceso = new timer.Timer(Facturacion.publicaTime);
                timerProceso.Enabled = true;
                timerProceso.Elapsed += Proceso;

                Program obj = new Program();
                obj.IniciaProceso();
            }

            if (!flag)
            {
                Console.ReadLine();
            }
        }

        public void IniciaProceso()
        {
            esperaTimer = new Thread((ThreadStart)delegate
            {
                Inactividad();
            });

            esperaTimer.Name = "timerEspera";
            esperaTimer.Start();
        }

        public void Inactividad()
        {
            while (true)
            {
                Util.ImprimePantalla("...");
                System.Threading.Thread.Sleep(Facturacion.batchTime);
            }
        }

        public static void Verifica(object sender, ElapsedEventArgs e)
        {
            timerVerifica.Stop();

            if (!timerProceso.Enabled)
            {
                timerProceso.Start();
            }

            timerVerifica.Start();
        }

        public static void Proceso(object sender, ElapsedEventArgs e)
        {
            try
            {
                timerProceso.Stop();
                new Facturacion().ProcesoFacturacion();
                timerProceso.Start();
            }
            catch (Exception ex)
            {
                Logging.EscribirLog(MethodBase.GetCurrentMethod().DeclaringType + "::" + MethodBase.GetCurrentMethod().Name + " ", ex, "ERR");
                Util.ImprimePantalla(" [FCT] " + "ERROR " + ex.Message.ToString());
            }
        }
    }
}
