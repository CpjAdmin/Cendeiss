using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Entidades
{
    public class Movimientos
    {
        public String cedula { get; set; }

        public String nombre { get; set; }

        public String descripcion { get; set; }
        public String documento { get; set; }
        public String tipo { get; set; }

        public Double aportes { get; set; }
        public Double rendimientos { get; set; }

        public String fecha { get; set; }


        public Movimientos()
        {
            this.cedula = "";
            this.nombre = "";
            this.descripcion = "";
            this.documento = "";
            this.tipo = "";
            this.aportes = 0;
            this.rendimientos = 0;
            this.fecha = "";
        }


        public Movimientos(String cedula, String nombre, String descripcion, String documento, String tipo, Double aportes, Double rendimientos, String fecha)
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.documento = documento;
            this.tipo = tipo;
            this.aportes = 0;
            this.rendimientos = 0;
            this.fecha = fecha;
        }
    }
}
