using System;
namespace Sistema
{
    public partial class Menu : Gtk.Window
    {
        public Menu() :
                base(Gtk.WindowType.Toplevel)
        {
            this.Build();
        }

        protected void OnListarEmpleadosAction1Activated(object sender, EventArgs e)
        {
            LIstarEmpleado lIstarEmpleado = new LIstarEmpleado();
            lIstarEmpleado.Show();
            Hide();
        }


        protected void OnBtnCerrarClicked(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        protected void OnAdministrarDepartamentosActionActivated(object sender, EventArgs e)
        {
            AdministrarDepartamentos adDep = new AdministrarDepartamentos();
            adDep.Show();
            Hide();
        }

        protected void OnAsignarCargosActionActivated(object sender, EventArgs e)
        {
            AsignarEmpleado asignarEmpleado = new AsignarEmpleado();
            asignarEmpleado.Show();
            Hide();
        }

        protected void OnAdministracionDeCargosActionActivated(object sender, EventArgs e)
        {
            AdministrarCargo ac = new AdministrarCargo();
            ac.Show();
            Hide();
        }
    }
}
