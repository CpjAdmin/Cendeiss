using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Entidades
{
    public class Asociado
    {
        public string id { get; set; }

        public string nombre { get; set; }
        public string email { get; set; }
        public string email2 { get; set; }
        public string clave { get; set; }
        public string rol { get; set; }
        public string estado { get; set; }
        public string tel_habitacion { get; set; }
        public string tel_celular { get; set; }
        public string tel_trabajo { get; set; }
        public string direccion { get; set; }
        public string ubicacion_geografica { get; set; }
        public string f_ult_ingreso { get; set; }
        public string f_ult_edicion { get; set; }
        public string f_nacimiento { get; set; }
        

        public Asociado()
        {
            this.id = "0";
            this.nombre = "";
            this.email = "";
            this.email2 = "";
            this.clave = "";
            this.rol = "";
            this.estado = "A";
            this.tel_habitacion = "";
            this.tel_celular = "";
            this.tel_trabajo = "";
            this.direccion = "";
            this.ubicacion_geografica = "";
            this.f_ult_ingreso = "1990-01-01";
            this.f_ult_edicion = "1990-01-01";
            this.f_nacimiento  = "1990-01-01";
        }


        public Asociado(string id, string nombre, string email, string clave, string rol, string estado, string f_ult_ingreso,string f_ult_edicion)
        {
            this.id = id;
            this.nombre = nombre;
            this.email = email;
            this.clave = clave;
            this.rol = rol;
            this.estado = estado;
            this.f_ult_ingreso = f_ult_ingreso;
            this.f_ult_edicion = f_ult_edicion;
        }

        public Asociado(string id, string nombre, string email,string email2, string rol
                      , string estado, string tel_habitacion,string tel_celular,string tel_trabajo,string direccion,string ubicacion_geografica
                      , string f_nacimiento)
        {
            this.id = id;
            this.nombre = nombre;
            this.email = email;
            this.email2 = email2;
            this.rol = rol;
            this.estado = estado;
            this.tel_habitacion = tel_habitacion;
            this.tel_celular = tel_celular;
            this.tel_trabajo = tel_trabajo;
            this.direccion = direccion;
            this.ubicacion_geografica = ubicacion_geografica;
            this.f_nacimiento = f_nacimiento;
        }

    }
}
