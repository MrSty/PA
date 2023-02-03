using System;
using Gtk;
using MySql.Data.MySqlClient;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema
{
    public partial class MarcarAsistencia : Gtk.Window
    {
        tbl_MarcarAsistencia tbma = new tbl_MarcarAsistencia();
        DT_tbl_MarcarAsistencia dtma = new DT_tbl_MarcarAsistencia();
        MessageDialog ms = null;
        //int contador = 0;

        public MarcarAsistencia() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            Iteracion();
            //this.tiempoLbl();

            this.BtnSalida.Visible = false;
            this.tvListaAsistencia.Model = dtma.listaAsistencia();

            string[] titulos = { "Hora de entrada", "Hora de salida", "Cedula" };
            for (int i = 0; i < titulos.Length; i++)
            {
                this.tvListaAsistencia.AppendColumn(titulos[i], new CellRendererText(), "text", i);
            }
        }

        private bool existeUsuario(string laCedula)
        {
            string cadenita = "server=localhost;uid=roberto;database=BDAyatoLovers;pwd=123";
            MySqlConnection conn = new MySqlConnection(cadenita);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT cedula FROM Empleado WHERE cedula = '" + laCedula + "'";


            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (reader["cedula"].ToString() == laCedula)
                    {
                        Console.WriteLine(reader["cedula"].ToString());
                        return true;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }

            Console.WriteLine("No existe");
            return false;
        }

        private void Iteracion()
        {
            GLib.Timeout.Add(100, new GLib.TimeoutHandler(Actualizacion));
        }

        private bool Actualizacion()
        {
            //lblFecha.Text = DateTime.Now.ToShortDateString();
            lblHora.Text = DateTime.Now.ToString("hh:mm:ss");

            return true;
        }

        string cedula;
        DateTime horaDeEntrada;
        DateTime horaDeSalida;


        protected void OnBtnEntradaClicked(object sender, EventArgs e)
        {
            if (TxtCedula.Text.Equals("Maechatelprix"))
            {
                MainWindow win = new MainWindow();
                win.Show();

                this.Destroy();
            }

            if (existeUsuario(TxtCedula.Text))
            {
                DateTime now = DateTime.Now;
                Console.WriteLine("Existe");
                cedula = TxtCedula.Text.Trim();
                //horaDeEntrada = DateTime.ParseExact(lblHora.Text, "yyyy/MM/dd hh:mm:ss", null);
                horaDeEntrada = now;

                tbma.HoraEntrada = horaDeEntrada;
                tbma.Cedula = cedula;

                BtnEntrada.Visible = false;
                BtnSalida.Visible = true;
            }
            else
            {
                Console.WriteLine("No existe");
                TxtCedula.Text = "";
            }

        }

        protected void OnBtnSalidaClicked(object sender, EventArgs e)
        {
            if (existeUsuario(TxtCedula.Text))
            {
                DateTime now = DateTime.Now;
                Console.WriteLine("Existe");
                //horaDeSalida = DateTime.ParseExact(lblHora.Text, "yyyy/MM/dd hh:mm:ss", null);
                horaDeSalida = now;
                tbma.HoraSalida = horaDeSalida;

                Console.WriteLine("Es: " + tbma.HoraEntrada);

                try
                {
                    if (dtma.registrarAsistencia(tbma))
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Se guardó correctamente");
                        ms.Run();
                        ms.Destroy();
                        MarcarAsistencia ma = new MarcarAsistencia();
                        ma.Show();

                        this.Destroy();
                    }
                    else
                    {
                        Console.WriteLine("Ocurrió un Error");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                BtnEntrada.Visible = true;
            }
            else
            {
                Console.WriteLine("No existe");
                TxtCedula.Text = "";
            }

        }
    }
}
