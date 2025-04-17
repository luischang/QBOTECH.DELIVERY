using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBOTECH.DELIVERY.CORE.Enums
{
    public enum DeliveryStatus
    {
        P, // Pendiente
        A, // Aceptado
        E, // En camino
        D, // Entregado
        C, // Cancelado
        F, // Fallido
        R, // Reprogramado
        X  // Devuelto
    }
}
