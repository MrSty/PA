using System;
using Sistema.Datos;
using Sistema.Entidades;
using Gtk;
namespace Sistema
{
    public partial class AdministrarDepartamentos : Gtk.Window
    {
        tbl_Departamento tbD = new tbl_Departamento();
        DT_tbl_Departamento dtD = new DT_tbl_Departamento();
        MessageDialog ms = null;

        public AdministrarDepartamentos() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
            this.DeleteEvent += new DeleteEventHandler(Cerrar);

            this.tvListaDepartamentos.Model = dtD.listaDepartamento();

            string[] titulos = { "ID", "NOMBRE DEPTO", "CANT EMPLEADO", "JEFE DEPTO", "EMAIL"};
            for (int i = 0; i < titulos.Length; i++)
            {
                this.tvListaDepartamentos.AppendColumn(titulos[i], new CellRendererText(), "text", i);
            }
        }
        protected void Cerrar(object sender, DeleteEventArgs e)
        {
            Application.Quit();
        }

        protected void OnBtnRegresarClicked(object sender, EventArgs e)
        {
            Sistema.Menu mn = new Menu();
            mn.Show();
            this.Destroy();
        }

        protected void OnBtnGuardarClicked(object sender, EventArgs e)
        {
            tbD.Nombre = this.txtNombreDept.Text.Trim();
            tbD.CantEmpleado = Convert.ToInt32(this.txtCantEmp.Text.Trim());
            tbD.JefeDepartamento = this.txtJefeDept.Text.Trim();
            tbD.Ext = this.txtExtDept.Text.Trim();
            tbD.Gmail = this.txtEmailDept.Text.Trim();

            try
            {
                if (dtD.guardarDepartamento(tbD))
                {
                    ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Se guardó correctamente");
                    ms.Run();
                    ms.Destroy();
                    Sistema.AdministrarDepartamentos ad = new AdministrarDepartamentos();
                    ad.Show();

                    this.Destroy();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void OnBtnModificarClicked(object sender, EventArgs e)
        {
            if (txtNombreDept.Text.Equals("") || txtJefeDept.Text.Equals("") || txtCantEmp.Text.Equals("") ||
            txtExtDept.Text.Equals("") || txtEmailDept.Text.Equals(""))
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error,
                   ButtonsType.Ok, "Se requiere todos los campos");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                tbD.IdDepartamento = Convert.ToInt32(this.txtIdDepto.Text.Trim());
                tbD.Nombre = this.txtNombreDept.Text.Trim();
                tbD.CantEmpleado = Convert.ToInt32(this.txtCantEmp.Text.Trim());
                tbD.JefeDepartamento = this.txtJefeDept.Text.Trim();
                tbD.Ext = this.txtExtDept.Text.Trim();
                tbD.Gmail = this.txtEmailDept.Text.Trim();

                try
                {
                    if (dtD.editarDepartamento(tbD))
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal,
                            MessageType.Info, ButtonsType.Ok, "Datos actualizados");
                        ms.Run();
                        ms.Destroy();

                        Sistema.AdministrarDepartamentos ls = new AdministrarDepartamentos();
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        protected void OnTvListaDepartamentosCursorChanged(object sender, EventArgs e)
        {
            TreeSelection seleccion = (sender as TreeView).Selection;
            TreeIter iter;
            TreeModel model;

            if(seleccion.GetSelected(out model, out iter))
            {
                tbD = dtD.listById(Convert.ToInt32(model.GetValue(iter, 0).ToString()));
                this.txtIdDepto.Text = tbD.IdDepartamento.ToString();
                this.txtNombreDept.Text = tbD.Nombre.ToString();
                this.txtCantEmp.Text = tbD.CantEmpleado.ToString();
                this.txtJefeDept.Text = tbD.JefeDepartamento.ToString();
                this.txtExtDept.Text = tbD.Ext.ToString();
                this.txtEmailDept.Text = tbD.Gmail.ToString();


            }
        }

        protected void OnBtnEliminarClicked(object sender, EventArgs e)
        {
            if (this.txtIdDepto.Text.Equals(""))
            {
                ms = new MessageDialog(null, DialogFlags.Modal,
                MessageType.Warning, ButtonsType.Ok, "Debe seleccionar el Departamento a Eliminar");
                ms.Run();
                ms.Destroy();
            }
            else
            {
                ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Error,
                    ButtonsType.YesNo, "¿Desea eliminar al Departamento?");
                ResponseType response = (ResponseType)ms.Run();
                ms.Show();

                if (response == ResponseType.Yes)
                {
                    ms.Destroy();
                    tbD.IdDepartamento = Convert.ToInt32(this.txtIdDepto.Text);
                    if (dtD.eliminarDepto(tbD) > 0)
                    {
                        ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info,
                           ButtonsType.Ok, "El Departamento ha sido eliminado");
                        ms.Run();
                        ms.Destroy();
                        Sistema.AdministrarDepartamentos ls = new AdministrarDepartamentos();
                        ls.Show();
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
    }
}
