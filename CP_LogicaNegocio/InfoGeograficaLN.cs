using CP_Entidades;
using CP_AccesoDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_LogicaNegocio
{
    //Provincia - Canton - Distrito
    public class ProvinciaLN
    {
        #region "Logica de Negocio Provincia"
        private static ProvinciaLN ObjProvincia = null;
        private ProvinciaLN() { }
        public static ProvinciaLN getInstancia()
        {
            if (ObjProvincia == null)
            {
                ObjProvincia = new ProvinciaLN();
            }
            return ObjProvincia;
        }
        #endregion

        //Get Provincias
        public List<Provincia> GetProvincias()
        {
            try
            {
                return ProvinciaDAO.getInstancia().GetProvincias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class CantonLN
    {
        #region "Logica de Negocio Canton"
        private static CantonLN ObjCanton = null;
        private CantonLN() { }
        public static CantonLN getInstancia()
        {
            if (ObjCanton == null)
            {
                ObjCanton = new CantonLN();
            }
            return ObjCanton;
        }
        #endregion

        //Get Cantones
        public List<Canton> GetCantones(string provinciaId)
        {
            try
            {
                return CantonDAO.getInstancia().GetCantones(provinciaId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class DistritoLN
    {
        #region "Logica de Negocio Distrito"
        private static DistritoLN ObjDistrito = null;
        private DistritoLN() { }
        public static DistritoLN getInstancia()
        {
            if (ObjDistrito == null)
            {
                ObjDistrito = new DistritoLN();
            }
            return ObjDistrito;
        }
        #endregion

        ////Get Distritos
        public List<Distrito> GetDistritos(string provinciaId, string cantonId)
        {
            try
            {
                return DistritoDAO.getInstancia().GetDistritos(provinciaId, cantonId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
