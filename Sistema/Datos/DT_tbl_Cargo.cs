using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public class DT_tbl_Cargo
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;

        public ListStore listaCargo()
        {
            ListStore cargo_Datos = new ListStore(typeof(int), typeof(string), typeof(string), typeof(int));
            sb.Clear();
            sb.Append("USE BDAyatoLovers;");
            sb.Append("SELECT * FROM BDAyatoLovers.Cargo;");

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

        public List<tbl_Cargo> llenarcbxRol()
        {
            List<tbl_Cargo> listaCargo = new List<tbl_Cargo>();

            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("select * from Cargo;");
            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                while (idr.Read())
                {
                    tbl_Cargo tr = new tbl_Cargo()
                    {
                        IdCargo = Convert.ToInt32(idr["idCargo"]),
                        Nombre = idr["nombre"].ToString(),
                        Descripcion = idr["descripcion"].ToString()
                    };
                    listaCargo.Add(tr);
                }
                return listaCargo;
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
            finally
            {
                idr.Close();
                con.CerrarConexion();
            }
        }



        public bool guardarCargo(tbl_Cargo cargo)
        {
            bool guardado = false;
            int x = 0;
            sb.Clear();

            sb.Append("INSERT INTO BDAyatoLovers.Cargo");
            sb.Append("(nombre, descripcion, idDepartamento)");
            sb.Append("VALUES('" + cargo.Nombre + "','" + cargo.Descripcion + "','" + cargo.IdDepartamento + "');");

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

        public bool editarCargo(tbl_Departamento dep)
        {
            bool editado = false;
            int x = 0;
            sb.Clear();
            sb.Append("UPDATE BDAyatoLovers.Cargo");

            return editado;
        }

        public tbl_Cargo listById(int idCargo)
        {
            tbl_Cargo tc = new tbl_Cargo();
            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("SELECT * FROM Cargo where idCargo = " + idCargo);
            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                if (idr.Read())
                {
                    tc.IdCargo = Convert.ToInt32(idr["idCargo"]);
                    tc.Nombre = idr["nombre"].ToString();
                    tc.Descripcion = idr["descripcion"].ToString();
                    tc.IdDepartamento = Convert.ToInt32(idr["idDepartamento"]);
                }
                return tc;
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

        public bool editarCargo(tbl_Cargo tbc)
        {
            bool editado = false;
            int x = 0;
            sb.Clear();
            sb.Append("UPDATE BDAyatoLovers.Cargo");
            sb.Append(" set nombre= '" + tbc.Nombre + "',");
            sb.Append(" descripcion= '" + tbc.Descripcion + "',");
            sb.Append(" idDepartamento= '" + tbc.IdDepartamento + "'");
            sb.Append(" WHERE idCargo= " + tbc.IdCargo);

            Console.WriteLine(tbc.ToString());

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

        public Int32 eliminarCargo(tbl_Cargo tbc)
        {
            int eliminado;
            sb.Clear();
            sb.Append("DELETE FROM BDAyatoLovers.Cargo WHERE idCargo= " + tbc.IdCargo);

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

        public DT_tbl_Cargo()
        {
        }
    }
}
