using System;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Entidades;
namespace Sistema.Datos
{
    public class DT_tbl_CargoEmpleado
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;

        public DT_tbl_CargoEmpleado()
        {
        }

        public Int32 getIdCargo(string cargo)
        {
            int existe = 0;
            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("SELECT idCargo from Cargo where nombre = '" + cargo + "';");

            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                if (idr.Read())
                {
                    existe = Convert.ToInt32(idr["idCargo"]);
                }
                return existe;
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

        public bool guardarCargoEmpleado(tbl_CargoEmpleado tbce)
        {
            bool guardado = false;
            int x = 0;
            sb.Clear();
            sb.Append("INSERT INTO BDAyatoLovers.EmpleadoCargo");
            sb.Append("(idEmpleado, idCargo)");
            sb.Append("VALUES(" + tbce.IdEmpleado + ", " + tbce.IdCargo + ");");

            try
            {
                con.AbrirConexion();
                x = con.Ejecutar(CommandType.Text, sb.ToString());
                if (x > 0)
                {
                    guardado = true;
                }
                return guardado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                con.CerrarConexion();
            }
        }

        public Int32 eliminarCargo(tbl_CargoEmpleado tbce)
        {
            int eliminado;
            sb.Clear();
            sb.Append("DELETE FROM BDAyatoLovers.EmpleadoCargo WHERE idEmpleado = "
            + tbce.IdEmpleado + " and idCargo = " + tbce.IdCargo);

            try
            {
                con.AbrirConexion();
                eliminado = con.Ejecutar(CommandType.Text, sb.ToString());
                return eliminado;
            }
            catch (Exception e)
            {
                ms = new MessageDialog(null, DialogFlags.Modal,
                MessageType.Error, ButtonsType.Ok, e.Message);
                ms.Run();
                ms.Destroy();
                throw;
            }
            finally
            {
                con.CerrarConexion();
                idr.Close();
            }
        }

    }
}
