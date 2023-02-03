using System;
using Gtk;
using Sistema.Datos;
using Sistema.Entidades;

namespace Sistema
{
    public partial class AdministrarCargo : Gtk.Window
    {
        DT_tbl_Cargo dtc = new DT_tbl_Cargo();
        tbl_Cargo tbc = new tbl_Cargo();
       
        MessageDialog ms = null;
        VWCargoDepartamento vwcd = new VWCargoDepartamento();

        public AdministrarCargo() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();

            this.tvAdministrarCargo.Model = vwcd.listaCargo();

            string[] titulos = { "ID", "Nombre cargo", "Descripcion", "Departamento" };
            for (int i = 0; i < titulos.Length; i++)
            {
                this.tvAdministrarCargo.AppendColumn(titulos[i], new CellRendererText(), "text", i);
            }
        }

        protected void OnBtnGuardarClicked(object sender, EventArgs e)
        {
            tbc.Nombre = this.txtNombreCargo.Text.Trim();
            tbc.Descripcion = this.txtDescripcion.Text.Trim();
            tbc.IdDepartamento = Convert.ToInt32(this.txtIdDepartamento.Text.Trim());


            try
            {
                if (dtc.guardarCargo(tbc))
                {
                    ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Se guardo correctamente");
                    ms.Run();
                    ms.Destroy();

                    AdministrarCargo ac = new AdministrarCargo();
                    ac.Show();

                    this.Destroy();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected void OnTvAdministrarCargoCursorChanged(object sender, EventArgs e)
        {
            TreeSelection seleccion = (sender as TreeView).Selection;
            TreeIter iter;
            TreeModel model;
            if (seleccion.GetSelected(out model, out iter))
            {
                tbc = dtc.listById(Convert.ToInt32(model.GetValue(iter, 0).ToString()));
                this.txtId.Text = tbc.IdCargo.ToString();
                this.txtNombreCargo.Text = tbc.Nombre.ToString();
                this.txtDescripcion.Text = tbc.Descripcion.ToString();
                this.txtIdDepartamento.Text = tbc.IdDepartamento.ToString();
            }
        }

        protected void OnBtnModificarClicked(object sender, EventArgs e)
        {
            if (txtNombreCargo.Text.Equals("") || txtDescripcion.Text.Equals("") || txtId.Text.Equals("") ||
            txtIdDepartamento.Text.Equals(""))
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error,
                   ButtonsType.Ok, "Se requieren todos los campos");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                tbc.Nombre = this.txtNombreCargo.Text.Trim();
                tbc.Descripcion = this.txtDescripcion.Text.Trim();
                tbc.IdDepartamento = Convert.ToInt32(this.txtIdDepartamento.Text.Trim());

                try
                {
                    if (dtc.editarCargo(tbc))
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal,
                            MessageType.Info, ButtonsType.Ok, "Datos actualizados");
                        ms.Run();
                        ms.Destroy();
                        this.tvAdministrarCargo.Model = dtc.listaCargo();
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
                MessageType.Warning, ButtonsType.Ok, "Debe seleccionar el Cargo que desea eliminar");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error,
                    ButtonsType.YesNo, "¿Desea eliminar este cargo?");
                ResponseType response = (ResponseType)ms.Run();
                ms.Show();

                if (response == ResponseType.Yes)
                {
                    ms.Destroy();
                    tbc.IdCargo = Convert.ToInt32(this.txtId.Text);
                    if (dtc.eliminarCargo(tbc) > 0)
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info,
                           ButtonsType.Ok, "El cargo seleccionado ha sido eliminado");
                        ms.Run();
                        ms.Destroy();
                        Sistema.AdministrarCargo ac = new AdministrarCargo();
                        ac.Show();
                        this.Destroy();

                    }
                    else
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal,
                            MessageType.Warning, ButtonsType.Ok, "Error: Verifique los datos del Departamento");
                        ms.Run();
                        ms.Destroy();
                    }
                }
            }
        }

        protected void OnBtnRegresarClicked(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            Hide();
        }
    }
}
