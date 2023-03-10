using System;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Entidades;
using Sistema.Views;

namespace Sistema.Datos
{
    public class DT_VW_EmpleadoCargo
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;

        public ListStore listaCargo()
        {
            ListStore cargo_Datos = new ListStore(typeof(int), typeof(int), typeof(string), typeof(string));
            sb.Clear();
            sb.Append("USE BDAyatoLovers;");
            sb.Append("SELECT * FROM BDAyatoLovers.new_view;");

            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                while (idr.Read())
                {
                    /*
                     * [0] idCargo
                     * [1] nombre
                     * [2] descripcion
                     * [3] idDepartamento
                     */

                    cargo_Datos.AppendValues(idr[0], idr[1], idr[2], idr[3]);
                }

                return cargo_Datos;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error lista " + e.Message);
            }
            finally
            {
                idr.Close();
                con.CerrarConexion();
            }

            return cargo_Datos;
        }
        public VWEmpleadoCargo listById(int idEmpleado, int idCargo)
        {
            VWEmpleadoCargo vwur = new VWEmpleadoCargo();
            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("SELECT * FROM new_view WHERE IdEmpleado = '" + idEmpleado + "' and IdCargo = '" + idCargo + "';");
            Console.Write(sb.ToString());
            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                if (idr.Read())
                {
                    vwur.IdEmpleado = Convert.ToInt32(idr["IdEmpleado"]);
                    vwur.IdCargo = Convert.ToInt32(idr["IdCargo"]);
                    vwur.NombreEmp = Convert.ToString(idr["NombreEmpleado"]);
                    vwur.Nombrecargo = Convert.ToString(idr["NombreCargo"]);
                    // Revisar esto
                }
                return vwur;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                idr.Close();
                con.CerrarConexion();
            }
        }

        public DT_VW_EmpleadoCargo()
        {
        }
    }
}
