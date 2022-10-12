using CP_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_AccesoDatos
{
    public class MovimientosDAO
    {
        #region "Constructor de Saldo"
        private static MovimientosDAO Saldo = null;
        private MovimientosDAO() { }
        public static MovimientosDAO getInstancia()
        {
            if (Saldo == null)
            {
                Saldo = new MovimientosDAO();
            }
            return Saldo;
        }
        #endregion

        public List<Movimientos> CargarMovimientos(string usuario, string f_ini, string f_fin, int cod_proceso)
        {
            List<Movimientos> lista = new List<Movimientos>();

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;

            try
            {
                //Instancia de Conexión
                conexion = ConexionDAO.getInstancia().ConexionBD();
                //Tipo y nombre del proceso a ejecutar
                cmd = new SqlCommand("dbo.sp_web_Movimientos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parametros
                cmd.Parameters.AddWithValue("@cidasociad", usuario);
                cmd.Parameters.AddWithValue("@f_inicio", f_ini);
                cmd.Parameters.AddWithValue("@f_fin", f_fin);
                cmd.Parameters.AddWithValue("@cod_proceso", cod_proceso);

                //Paramtero de Salida @msg
                //cmd.Parameters.Add("@msg", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;

                conexion.Open();
                dr = cmd.ExecuteReader();

                //if (cmd.Parameters["@msg"].Value != null)
                //{
                //    String mensaje = cmd.Parameters["@msg"].Value.ToString();
                //}

                while (dr.Read())
                {
                    Movimientos ObjMovimientos = new Movimientos()
                    {
                        cedula = dr["cedula"].ToString(),
                        nombre = dr["nombre"].ToString(),
                        descripcion = dr["descripcion"].ToString(),
                        documento = dr["documento"].ToString(),
                        tipo = dr["tipo"].ToString(),
                        aportes = Convert.ToDouble(dr["aportes"].ToString()),
                        rendimientos = Convert.ToDouble(dr["rendimientos"].ToString()),
                        fecha = DateTime.Parse(dr["fecha"].ToString()).ToString("dd-MM-yyyy")
                };

                    lista.Add(ObjMovimientos);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }
    }
}
