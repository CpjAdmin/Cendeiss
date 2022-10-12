using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Entidades
{
    public class Proceso
    {
        public int cod_proceso { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string id_usuario { get; set; }
        public string navegador { get; set; }
        public string pagina { get; set; }
        public string proceso { get; set; }
        public string terminal_id { get; set; }

        public Proceso()
        {
            this.cod_proceso = 0;
            this.nombre = "";
            this.descripcion = "";
            this.id_usuario = "";
            this.navegador = "";
            this.pagina = "";
            this.proceso = "";
            this.terminal_id = "";

        }

        public Proceso(int cod_proceso, string nombre, string descripcion)
        {
            this.cod_proceso = this.cod_proceso;
            this.nombre = this.nombre;
            this.descripcion = this.descripcion;
        }
    }
}
