using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoAutomatasUMG.Clases
{
    class Transicion
    {
        public String estado { get; set; }
        public String simbolo { get; set; }
        public String proximoEstado { get; set; }
        
        public Transicion()
        {
        }

        public Transicion(String estado, String simbolo, String proximoEstado)
        {
            this.estado = estado;
            this.simbolo = simbolo;
            this.proximoEstado = proximoEstado;
        }
    }
}
