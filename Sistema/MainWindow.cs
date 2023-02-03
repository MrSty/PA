using System;
using Gtk;
using Sistema;

public partial class MainWindow : Gtk.Window
{
    string idAdmin = "admin";
    string contraAdmin = "123";

    string idEmp = "emp";
    string contraEmp = "12345";

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnIniciarClicked(object sender, EventArgs e)
    {
        EvaluarSesion();
    }

    private void EvaluarSesion()
    {
        if (txtUsuario.Text.Equals(idAdmin) && txtClave.Text.Equals(contraAdmin))
        {
            Sistema.Menu menu = new Sistema.Menu();
            menu.Show();
            this.Hide();
        }

        else if (txtUsuario.Text.Equals(idEmp) && txtClave.Text.Equals(contraEmp))
        {
            EntradaSalida ventana = new Sistema.EntradaSalida();
            ventana.usuarioEmpleado(idEmp);
            ventana.Show();
            this.Hide();
        }
        else
        {
            MessageDialog ms = null;
            ms = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Credenciales equivocados");
            ms.Run();
            ms.Destroy();
            txtUsuario.Text = "";
            txtClave.Text = "";
        }
    }

    protected void OnBtnSalirClicked(object sender, EventArgs e)
    {
        Application.Quit();
    }
}
