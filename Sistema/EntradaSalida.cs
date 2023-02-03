using System;
using Gtk;
namespace Sistema
{
    public partial class EntradaSalida : Gtk.Window
    {
        MessageDialog ms = null;

        public EntradaSalida() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        public void usuarioEmpleado(string a)
        {
            lblEmpleado.LabelProp = a;
        }

        protected void OnBtnEntradaClicked(object sender, EventArgs e)
        {
            ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Entrada registrada");
            ms.Run();
            ms.Destroy();
        }

        protected void OnBtnSalidaClicked(object sender, EventArgs e)
        {
            ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Salida registrada");
            ms.Run();
            ms.Destroy();
        }

        protected void OnBtnSalirClicked(object sender, EventArgs e)
        {
            MainWindow inicio = new MainWindow();
            inicio.Show();
            Hide();
        }
    }
}
