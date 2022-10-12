using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Entidades
{
    public class Saldos
    {

        public String cedula { get; set; }

        public String nombre { get; set; }

        public String descripcion { get; set; }

        public Double saldo_ant_ap { get; set; }
        public Double aportes { get; set; }

        public Double debitos_ap { get; set; }

        public Double saldo_act_ap { get; set; }

        public Double saldo_ant_rend { get; set; }

        public Double rendimientos { get; set; }

        public Double debitos_rend { get; set; }

        public Double saldo_act_rend { get; set; }

        public Double total { get; set; }

        public String periodo { get; set; }

       
        public Saldos()
        {
            this.cedula = "";
            this.nombre = "";
            this.descripcion = "";
            this.saldo_ant_ap = 0;
            this.aportes = 0;
            this.debitos_ap = 0;
            this.saldo_act_ap = 0;
            this.saldo_ant_rend = 0;
            this.rendimientos = 0;
            this.debitos_rend = 0;
            this.saldo_act_rend = 0;
            this.total = 0;
            this.periodo = "";
           
        }


        public Saldos(String cedula, String nombre, String descripcion, Double saldo_ant_ap, Double aportes, Double debitos_ap,Double saldo_act_ap,Double saldo_ant_rend, Double rendimientos, Double debitos_rend, Double saldo_act_rend, Double total, String periodo )
        {
            this.cedula = cedula;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.saldo_ant_ap = saldo_ant_ap;
            this.aportes = aportes;
            this.debitos_ap = debitos_ap;
            this.saldo_act_ap = saldo_act_ap;
            this.saldo_ant_rend = saldo_ant_rend;
            this.rendimientos = rendimientos;
            this.debitos_rend = debitos_rend;
            this.saldo_act_rend = saldo_act_rend;
            this.total = total;
            this.periodo = periodo;
        }
    }
}
