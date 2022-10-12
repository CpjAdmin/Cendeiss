using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Entidades
{

    //Agregado por Carlos Fonseca SOL271275  24/05/2019
    //Se agrega la clase para enviar el resultado del envío del correo.
    public class Resultado
    {
        public int Error { get; set; }
        public string Mensaje { get; set; }

        public Resultado()
        {
            this.Error = 0;
            this.Mensaje = "";
        }


        public Resultado(int error, string mensaje)
        {
            this.Error = error;
            this.Mensaje = mensaje;
        }

    }

   
}
