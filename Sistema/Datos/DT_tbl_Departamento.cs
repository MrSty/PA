using System;
using Sistema.Entidades;
using Sistema.Datos;
using System.Data;
using System.Text;
using Gtk;

namespace Sistema.Datos
{
    public class DT_tbl_Departamento
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;

        public ListStore listaDepartamento()
        {
            ListStore departamento_Datos = new ListStore(typeof(int), typeof(string), typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            sb.Clear();
            sb.Append("USE BDAyatoLovers;");
            sb.Append("SELECT * FROM BDAyatoLovers.Departamento;");

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

                    departamento_Datos.AppendValues(idr[0], idr[1], idr[2], idr[3], idr[5]);
                }

                return departamento_Datos;
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

            return departamento_Datos;
        }

        public bool guardarDepartamento(tbl_Departamento dep)
        {
            bool guardado = false;
            int x = 0;
            sb.Clear();

            sb.Append("INSERT INTO BDAyatoLovers.Departamento");
            sb.Append("(nombre, cantEmpleado, jefeDepartamento, ext, gmail)");
            sb.Append("VALUES('" + dep.Nombre + "'," + dep.CantEmpleado + ",'" + dep.JefeDepartamento + "','" + dep.Ext + "','" +
            dep.Gmail + "');");

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

        public bool editarDepartamento(tbl_Departamento tbe)
        {
            bool editado = false;
            int x = 0;
            sb.Clear();
            sb.Append("update BDAyatoLovers.Departamento");
            sb.Append(" set nombre= '" + tbe.Nombre + "',");
            sb.Append(" cantEmpleado= " + tbe.CantEmpleado + ",");
            sb.Append(" jefeDepartamento= '" + tbe.JefeDepartamento + "',");
            sb.Append(" ext= '" + tbe.Ext + "',");
            sb.Append(" gmail= '" + tbe.Gmail + "'");
            sb.Append(" WHERE idDepartamento= " + tbe.IdDepartamento);

            Console.WriteLine(tbe.ToString());

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
            }
        }

        public tbl_Departamento listById(int idDepto)
        {
            tbl_Departamento td = new tbl_Departamento();
            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("SELECT * FROM Departamento where idDepartamento = " + idDepto);
            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                if (idr.Read())
                {

                    td.IdDepartamento = Convert.ToInt32(idr["idDepartamento"]);
                    td.Nombre = idr["nombre"].ToString();
                    td.CantEmpleado = Convert.ToInt32(idr["cantEmpleado"]);
                    td.JefeDepartamento = idr["jefeDepartamento"].ToString();
                    td.Ext = idr["ext"].ToString();
                    td.Gmail = idr["gmail"].ToString();

                }
                return td;
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

        public Int32 eliminarDepto(tbl_Departamento tbe)
        {
            int eliminado;
            sb.Clear();
            sb.Append("DELETE FROM BDAyatoLovers.Departamento WHERE idDepartamento= " + tbe.IdDepartamento);

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

        public DT_tbl_Departamento()
        {

        }
    }
}
