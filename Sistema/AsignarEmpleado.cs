using System;
using System.Collections.Generic;
using Sistema.Entidades;
using Sistema.Datos;
using Sistema.Views;
using Gtk;

namespace Sistema
{
    public partial class AsignarEmpleado : Gtk.Window
    {
        DT_tbl_Cargo dtc = new DT_tbl_Cargo();
        DT_tbl_CargoEmpleado dtce = new DT_tbl_CargoEmpleado();
        MessageDialog ms = null;
        tbl_CargoEmpleado tbce = new tbl_CargoEmpleado();
        VWEmpleadoCargo vwec = new VWEmpleadoCargo();
        DT_VW_EmpleadoCargo dvwec = new DT_VW_EmpleadoCargo();
        public AsignarEmpleado() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            llenarComboCargo();
            this.tvEmpleadoCargo.Model = dvwec.listaCargo();

            string[] titulos = { "ID Empleado", "ID Cargo", "Nombre Empleado", "Nombre Cargo" };
            for (int i = 0; i < titulos.Length; i++)
            {
                this.tvEmpleadoCargo.AppendColumn(titulos[i], new CellRendererText(), "text", i);
            }
        }

        protected void OnBtnRegresarClicked(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();
            Hide();
        }

        protected void llenarComboCargo()
        {
            List<tbl_Cargo> listaCargo = new List<tbl_Cargo>();
            listaCargo = dtc.llenarcbxRol();

            this.cbCargo.InsertText(0, "Seleccione...");
            foreach(tbl_Cargo tbc  in listaCargo)
            {
                this.cbCargo.InsertText(tbc.IdCargo, tbc.Nombre);
            }


        }

        protected void OnBntGuardarClicked(object sender, EventArgs e)
        {
            int primero = 0;
            int ultimo = 0;
            int idEmpleado;
            string idCargo;
            if (this.cbCargo.ActiveText.Equals("Seleccione..."))
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Warning, ButtonsType.Ok, "Todos los campos son requeridos");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                idEmpleado = Convert.ToInt32(this.txtIdEmpleado.Text.Trim());
                idCargo = this.cbCargo.ActiveText.Trim().ToString();

                tbce.IdEmpleado = idEmpleado;
                tbce.IdCargo = dtce.getIdCargo(idCargo);

                if (dtce.guardarCargoEmpleado(tbce))
                {
                    ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Datos Guardados con Éxito");
                    ms.Run();
                    ms.Destroy();
                    Sistema.AsignarEmpleado asi = new AsignarEmpleado();
                    asi.Show();
                    this.Destroy();

                }
                else
                {
                    ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Ocurrió un Error");
                    ms.Run();
                    ms.Destroy();
                }

            }
        }

        protected void OnTvEmpleadoCargoCursorChanged(object sender, EventArgs e)
        {
            TreeSelection seleccion = (sender as TreeView).Selection;
            TreeIter iter;
            TreeModel model;
            if (seleccion.GetSelected(out model, out iter))
            {
                vwec = dvwec.listById(Convert.ToInt32(model.GetValue(iter, 0).ToString()), Convert.ToInt32(model.GetValue(iter, 1).ToString()));
                this.txtIdEmpleado.Text = vwec.IdEmpleado.ToString();
                this.txtIdCargo.Text = vwec.IdCargo.ToString();
               
            }
        }

        protected void OnBtnEliminarClicked(object sender, EventArgs e)
        {
            if (txtIdCargo.Text.Equals("") || txtIdEmpleado.Text.Equals(""))
            {
                ms = new MessageDialog(null, DialogFlags.Modal,
                MessageType.Warning, ButtonsType.Ok, "Debe seleccionar el Cargo a Eliminar");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                //Pregunta para confirmar eliminación de usuario
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error,
                    ButtonsType.YesNo, "¿Desea eliminar al rol?");
                ResponseType response = (ResponseType)ms.Run();
                ms.Show();

                if (response == ResponseType.Yes)
                {
                    ms.Destroy();
                    //Se obtiene el valor del id
                    tbce.IdEmpleado = Convert.ToInt32(this.txtIdEmpleado.Text);
                    tbce.IdCargo = Convert.ToInt32(this.txtIdCargo.Text);
                    try
                    {
                        if (dtce.eliminarCargo(tbce) > 0)
                        {
                            ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info,
                                ButtonsType.Ok, "El Cargo ha sido eliminado");
                            ms.Run();
                            ms.Destroy();

                           /*Sistema.AsignarEmpleado ase = new AsignarEmpleado();
                            ase.Show();
                            this.Destroy();*/
                        }
                        else
                        {
                            ms = new MessageDialog(null, DialogFlags.Modal,
                                MessageType.Warning, ButtonsType.Ok, "Error: Verifique los datos del Cargo");
                            ms.Run();
                            ms.Destroy();
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("F");
                    ms.Destroy();
                }


            }
        }
    }
}
