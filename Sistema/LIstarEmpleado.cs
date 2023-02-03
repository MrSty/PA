using System;
using Gtk;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema
{
    public partial class LIstarEmpleado : Gtk.Window
    {
        tbl_Empleado tbe = new tbl_Empleado();
        DT_tbl_Empleado dte = new DT_tbl_Empleado();
        MessageDialog ms = null;

        public LIstarEmpleado() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.DeleteEvent += new DeleteEventHandler(Cerrar);

            this.tvListaEmpleado.Model = dte.listaEmpleado();

            string[] titulos = { "ID", "NOMBRE", "APELLIDO", "CIUDAD", "CEDULA" };
            for (int i = 0; i < titulos.Length; i++)
            {
                this.tvListaEmpleado.AppendColumn(titulos[i], new CellRendererText(), "text", i);
            }

        }

        protected void Cerrar(object sender, DeleteEventArgs e)
        {
            Application.Quit();
        }


        protected void OnTvListaEmpleadoCursorChanged(object sender, EventArgs e)
        {
            TreeSelection seleccion = (sender as TreeView).Selection;
            TreeIter iter;
            TreeModel model;
            if (seleccion.GetSelected(out model, out iter))
            {
                tbe = dte.listById(Convert.ToInt32(model.GetValue(iter, 0).ToString()));
                this.txtId.Text = tbe.IdEmpleado.ToString();
                this.txtNombre.Text = tbe.Nombre.ToString();
                this.txtApellido.Text = tbe.Apellido.ToString();
                this.txtTelefono.Text = tbe.Telefono.ToString();
                this.txtCiudad.Text = tbe.Ciudad.ToString();
                this.txtDireccion.Text = tbe.Direccion.ToString();
                this.txtCedula.Text = tbe.Cedula.ToString();
                this.txtPersonal.Text = tbe.CorreoPersonal.ToString();
                this.txtLaboral.Text = tbe.CorreoLaboral.ToString();
            }
        }

        protected void OnBtnRegresarClicked(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            Hide();
        }

        protected void OnBtnGuardarClicked(object sender, EventArgs e)
        {

            tbe.Nombre = this.txtNombre.Text.Trim();
            tbe.Apellido = this.txtApellido.Text.Trim();
            tbe.Ciudad = this.txtCiudad.Text.Trim();
            tbe.Telefono = this.txtTelefono.Text.Trim();
            tbe.Direccion = this.txtDireccion.Text.Trim();
            tbe.Cedula = this.txtCedula.Text.Trim();
            tbe.CorreoLaboral = this.txtLaboral.Text.Trim();
            tbe.CorreoPersonal = this.txtPersonal.Text.Trim();


            try
            {
                if (dte.guardarUsuario(tbe))
                {
                    ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Se guardó correctamente");
                    ms.Run();
                    ms.Destroy();
                    Sistema.LIstarEmpleado ls = new LIstarEmpleado();
                    ls.Show();

                    this.Destroy();
                }
                else
                {
                    Console.WriteLine("Ocurrió un Error");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void OnBtnModificarClicked(object sender, EventArgs e)
        {
            if(txtNombre.Text.Equals("") || txtApellido.Text.Equals("") || txtTelefono.Text.Equals("") ||
            txtCiudad.Text.Equals("") || txtCedula.Text.Equals("") || txtDireccion.Text.Equals("") ||
                txtPersonal.Text.Equals("") || txtLaboral.Text.Equals(""))
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, 
                    ButtonsType.Ok, "Se requiere todos los campos");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                tbe.IdEmpleado = Convert.ToInt32(this.txtId.Text);
                tbe.Nombre = this.txtNombre.Text.Trim();
                tbe.Apellido = this.txtApellido.Text.Trim();
                tbe.Ciudad = this.txtCiudad.Text.Trim();
                tbe.Telefono = this.txtTelefono.Text.Trim();
                tbe.Direccion = this.txtDireccion.Text.Trim();
                tbe.Cedula = this.txtCedula.Text.Trim();
                tbe.CorreoLaboral = this.txtLaboral.Text.Trim();
                tbe.CorreoPersonal = this.txtPersonal.Text.Trim();

                try
                {
                    if (dte.editarEmpleado(tbe))
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal,
                            MessageType.Info, ButtonsType.Ok, "Datos actualizados");
                        ms.Run();
                        ms.Destroy();

                        Sistema.LIstarEmpleado ls = new LIstarEmpleado();
                        ls.Show();
                        this.Destroy();
                    }
                    else
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal,
                           MessageType.Error, ButtonsType.Ok,
                           "Error al editar datos");
                        ms.Run();
                        ms.Destroy();
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void OnBtnEliminarClicked(object sender, EventArgs e)
        {
            if (this.txtId.Text.Equals(""))
            {
                ms = new MessageDialog(null, DialogFlags.Modal,
                MessageType.Warning, ButtonsType.Ok, "Debe seleccionar el usuario a Eliminar");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error,
                    ButtonsType.YesNo, "¿Desea eliminar al usuario?");
                ResponseType response = (ResponseType)ms.Run();
                ms.Show();

                if(response == ResponseType.Yes)
                {
                    ms.Destroy();
                    tbe.IdEmpleado = Convert.ToInt32(this.txtId.Text);
                    if(dte.eliminarEmpleado(tbe) > 0)
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info,
                           ButtonsType.Ok, "El usuario ha sido eliminado");
                        ms.Run();
                        ms.Destroy();
                        Sistema.LIstarEmpleado ls = new LIstarEmpleado();
                        ls.Show();
                        this.Destroy();

                    }
                    else
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal,
                            MessageType.Warning, ButtonsType.Ok, "Error: Verifique los datos del usuario");
                        ms.Run();
                        ms.Destroy();
                    }
                }
            }
        }

        /*
        protected void OnBtnGuardarClicked(object sender, EventArgs e)
        {
            tbe.Nombre = this.txtNombre.Text.Trim();
            tbe.Apellido = this.txtApellido.Text.Trim();
            tbe.Ciudad = this.txtCiudad.Text.Trim();
            tbe.Telefono = this.txtTelefono.Text.Trim();
            tbe.Direccion = this.txtDireccion.Text.Trim();
            tbe.Cedula = this.txtCedula.Text.Trim();
            tbe.CorreoLaboral = this.txtLaboral.Text.Trim();
            tbe.CorreoPersonal = this.txtPersonal.Text.Trim();


            if (dte.guardarUsuario(tbe))
            {
                Console.WriteLine("Se guardaron los datos sin problemas!");
                this.Destroy();
            }
            else
            {
                Console.WriteLine("Ocurrió un Error");
            }
        }
        */
    }
}
