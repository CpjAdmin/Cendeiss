using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Entidades
{
    //Provincia - Canton - Distrito
    public class Provincia
    {
        public String cod_provincia { get; set; }
        public String nombre { get; set; }

        public Provincia()
        {
            cod_provincia = "0";
            nombre = "";
        }
    }

    public class Canton
    {
        public String cod_provincia { get; set; }
        public String cod_canton { get; set; }
        public String nombre { get; set; }
        public Canton()
        {
            cod_provincia = "0";
            cod_canton = "0";
            nombre = "";
        }

    }

    public class Distrito
    {
        public String cod_provincia { get; set; }
        public String cod_canton { get; set; }
        public String cod_distrito { get; set; }
        public String nombre { get; set; }

        public Distrito()
        {
            cod_provincia = "0";
            cod_canton = "0";
            cod_distrito = "0";
            nombre = "";
        }
    }
}
