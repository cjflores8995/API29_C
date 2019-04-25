using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WebFacturacion
    {
        public List<VFECOMPROBANTES> ListarComprobantes(string identificacion, string tipo, string comprobante, string fdesde, string fhasta)
        {
            return new VFECOMPROBANTES().ListarComprobantes(identificacion, tipo, comprobante, fdesde, fhasta);
        }
    }
}
