using System;
using System.Data;
using System.Text;
using Gtk;
using Sistema.Datos;

namespace Sistema.Views
{
    public class VWEmpleadoCargo
    {

        private int idEmpleado;
        private int idCargo;
        private string nombreEmp;
        private string nombrecargo;

        public VWEmpleadoCargo()
        {
        }

        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public string NombreEmp { get => nombreEmp; set => nombreEmp = value; }
        public string Nombrecargo { get => nombrecargo; set => nombrecargo = value; }
    }
}
