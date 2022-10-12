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
    public class SaldosDAO
    {

        #region "Constructor de Saldo"
        private static SaldosDAO Saldo = null;
        private SaldosDAO() { }
        public static SaldosDAO getInstancia()
        {
            if (Saldo == null)
            {
                Saldo = new SaldosDAO();
            }
            return Saldo;
        }
        #endregion

        public List<Saldos> CargarSaldos(string usuario, int mes, int ano, int cod_proceso)
        {
            List<Saldos> lista = new List<Saldos>();

            SqlConnection conexion = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;

            try
            {
                //Instancia de Conexión
                conexion = ConexionDAO.getInstancia().ConexionBD();
                //Tipo y nombre del proceso a ejecutar
                cmd = new SqlCommand("dbo.sp_web_saldos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                // Parametros
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@mes", mes);
                cmd.Parameters.AddWithValue("@ano", ano);
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
                    Saldos ObjSaldos = new Saldos()
                    {
                        cedula = dr["cedula"].ToString(),
                        nombre = dr["nombre"].ToString(),
                        //descripcion = dr["descripcion"].ToString(),
                        saldo_ant_ap = Convert.ToDouble(dr["saldo_ant_ap"].ToString()),
                        aportes = Convert.ToDouble(dr["aportes"].ToString()),
                        debitos_ap = Convert.ToDouble(dr["debitos_ap"].ToString()),
                        saldo_act_ap = Convert.ToDouble(dr["saldo_act_ap"].ToString()),
                        saldo_ant_rend = Convert.ToDouble(dr["saldo_ant_rend"].ToString()),
                        rendimientos = Convert.ToDouble(dr["rendimientos"].ToString()),
                        debitos_rend = Convert.ToDouble(dr["debitos_rend"].ToString()),
                        saldo_act_rend = Convert.ToDouble(dr["saldo_act_rend"].ToString()),
                        total = Convert.ToDouble(dr["total"].ToString()),
                        periodo = DateTime.Parse(dr["periodo"].ToString()).ToString("dd-MM-yyyy")

                    };

                    lista.Add(ObjSaldos);
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
