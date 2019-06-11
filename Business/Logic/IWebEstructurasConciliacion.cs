using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IWebEstructurasConciliacion
    {
        CanalRespuesta GeneraEstructuraConciliacion(DateTime fechaInicio, DateTime fechaFin);
    }
}
