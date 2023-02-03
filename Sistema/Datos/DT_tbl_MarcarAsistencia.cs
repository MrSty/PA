using System;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Sistema.Datos
{
    public class DT_tbl_MarcarAsistencia
    {
        Conexion con = new Conexion();
        IDataReader idr = null;
        StringBuilder sb = new StringBuilder();
        MessageDialog ms = null;
        MySqlCommand cmd = new MySqlCommand();



        public ListStore listaAsistencia()
        {
            ListStore asistencia_Datos = new ListStore(typeof(string), typeof(string), typeof(string));
            sb.Clear();
            sb.Append("USE BDAyatoLovers;");
            sb.Append("SELECT * FROM BDAyatoLovers.RegistroEntradaSalida;");

            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                while (idr.Read())
                {
                    /*
                     * 4 = Cedula
                     * 2 = Hora de entrada
                     * 3 = Hora de salida
                     */

                    asistencia_Datos.AppendValues(idr[1].ToString(), idr[2].ToString(), idr[3]);
                }

                return asistencia_Datos;
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

            return asistencia_Datos;
        }

        public bool registrarAsistencia(tbl_MarcarAsistencia ma)
        {
            bool guardado = false;
            int x = 0;
            sb.Clear();

            sb.Append("INSERT INTO BDAyatoLovers.RegistroEntradaSalida");
            sb.Append("(horaEntrada, horaSalida, cedula)");
            sb.Append("VALUES('" + ma.HoraEntrada.ToString("yyyy-MM-dd hh:mm:ss") + "','" + ma.HoraSalida.ToString("yyyy-MM-dd hh:mm:ss") + "','" + ma.Cedula + "');");

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

        public tbl_MarcarAsistencia listById(int idAsistencia)
        {
            tbl_MarcarAsistencia tma = new tbl_MarcarAsistencia();
            sb.Clear();
            sb.Append("Use BDAyatoLovers;");
            sb.Append("SELECT * FROM RegistroEntradaSalida where idAsistencia = " + idAsistencia);
            try
            {
                con.AbrirConexion();
                idr = con.Leer(CommandType.Text, sb.ToString());
                if (idr.Read())
                {
                    tma.IdAsistencia = Convert.ToInt32(idr["idAsistencia"]);
                    tma.HoraEntrada = Convert.ToDateTime(idr["horaEntrada"]);
                    tma.HoraSalida = Convert.ToDateTime(idr["horaSalida"]);
                    tma.Cedula = idr["cedula"].ToString();
                }
                return tma;
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
        public DT_tbl_MarcarAsistencia()
        {

        }
    }
}
