using System;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Entidades;
namespace Sistema.Datos
{
    public class DT_tbl_Empleado
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;

        public ListStore listaEmpleado()
        {
            ListStore empleado_Datos = new ListStore(typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            sb.Clear();
            sb.Append("USE BDAyatoLovers;");
            sb.Append("SELECT * FROM BDAyatoLovers.Empleado;");

            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                while (idr.Read())
                {
                    /*
                     * 0 = ID
                     * 4 = Nombre
                     * 6 = Apellido
                     * 5 = Ciudad                    
                     * 7 = Cedula                    
                     */

                    empleado_Datos.AppendValues(idr[0], idr[4], idr[6], idr[5], idr[7]);
                }

                return empleado_Datos;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error lista: " + e.Message);
            }
            finally
            {
                idr.Close();
                con.CerrarConexion();
            }

            return empleado_Datos;
        }

        public bool guardarUsuario(tbl_Empleado emp)
        {
            bool guardado = false;
            int x = 0;
            sb.Clear();

            sb.Append("INSERT INTO BDAyatoLovers.Empleado");
            sb.Append("(correoPersonal, correoLaboral, telefono, nombre, ciudad, apellido, cedula, direccion)");
            sb.Append("VALUES('" + emp.CorreoPersonal + "','" + emp.CorreoLaboral + "','" + emp.Telefono + "','" + emp.Nombre + "','" + 
            emp.Ciudad + "','" + emp.Apellido + "','"  + emp.Cedula + "','" + emp.Direccion + "');");

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

        public tbl_Empleado listById(int idEmpleado)
        {
            tbl_Empleado te = new tbl_Empleado();
            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("SELECT * FROM Empleado where idEmpleado = " + idEmpleado);
            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                if (idr.Read())
                {
                    te.IdEmpleado = Convert.ToInt32(idr["idEmpleado"]);
                    te.Nombre = idr["nombre"].ToString();
                    te.Apellido = idr["apellido"].ToString();
                    te.Telefono = idr["telefono"].ToString();
                    te.Ciudad = idr["ciudad"].ToString();
                    te.Direccion = idr["direccion"].ToString();
                    te.Cedula = idr["cedula"].ToString();
                    te.CorreoPersonal = idr["correoPersonal"].ToString();
                    te.CorreoLaboral = idr["correoLaboral"].ToString();
                }
                return te;
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
        public bool editarEmpleado(tbl_Empleado tbe)
        {
            bool editado = false;
            int x = 0;
            sb.Clear();
            sb.Append("Update BDAyatoLovers.Empleado");
            sb.Append(" set nombre= '" +tbe.Nombre+"',");
            sb.Append(" apellido= '" + tbe.Apellido + "',");
            sb.Append(" telefono= '" + tbe.Telefono + "',");
            sb.Append(" ciudad= '" + tbe.Ciudad + "',");
            sb.Append(" direccion= '" + tbe.Direccion + "',");
            sb.Append(" cedula= '" + tbe.Cedula + "',");
            sb.Append(" correoPersonal= '" + tbe.CorreoPersonal + "',");
            sb.Append(" correoLaboral= '" + tbe.CorreoLaboral + "'");
            sb.Append(" WHERE idEmpleado= " + tbe.IdEmpleado);

            Console.WriteLine(sb.ToString());

            try
            {
                con.AbrirConexion();
                x = con.Ejecutar(CommandType.Text, sb.ToString());
                if (x > 0)
                {
                    editado = true;
                }
                return editado;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                con.CerrarConexion();
                idr.Close();
            }
        }

        public Int32 eliminarEmpleado(tbl_Empleado tbe)
        {
            int eliminado;
            sb.Clear();
            sb.Append("DELETE FROM BDAyatoLovers.Empleado WHERE idEmpleado= " +tbe.IdEmpleado);

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
            }
        }
        public DT_tbl_Empleado()
        {
        }
    }
}
