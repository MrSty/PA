using System;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Entidades;

namespace Sistema.Datos
{
    public class DT_tbl_CargoDepartamento
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;


        public bool guardarCargo(tbl_CargoDepartamento cargo)
        {
            bool guardado = false;
            int x = 0;
            sb.Clear();

            sb.Append("INSERT INTO BDAyatoLovers.cargoDepartamento");
            sb.Append("(idCargo, idDepartamento, descripcion)");
            sb.Append("VALUES(" + cargo.IdCargo + "," + cargo.IdCargoDepartamento + ",'" + cargo.Descripcion + "');");

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
        public DT_tbl_CargoDepartamento()
        {
        }
    }
}
