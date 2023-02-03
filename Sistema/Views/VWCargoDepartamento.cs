using System;
using System.Data;
using System.Text;
using Gtk;

namespace Sistema.Datos
{
    public class VWCargoDepartamento
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;

        public ListStore listaCargo()
        {
            ListStore cargo_Datos = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string));
            sb.Clear();
            sb.Append("USE BDAyatoLovers;");
            sb.Append("SELECT * FROM BDAyatoLovers.VWCargoDept;");

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
        public VWCargoDepartamento()
        {
        }
    }
}
