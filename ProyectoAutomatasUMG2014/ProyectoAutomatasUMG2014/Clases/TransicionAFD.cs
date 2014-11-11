using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoAutomatasUMG.Clases
{
    class TransicionAFD : Transicion
    {
        public bool marca { get; set; }
        public List<String> componentes { get; set; }
        public bool aceptacion { get; set; }

        public TransicionAFD() { }
        public TransicionAFD(bool marca, List<String> componentes, bool aceptacion)
        {
            this.marca = marca;
            this.componentes = componentes;
            this.aceptacion = aceptacion;
        }
    }
}
