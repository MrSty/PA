using System;
namespace Sistema.Entidades
{
    public class tbl_Empleado
    {
        private int idEmpleado;
        private string correoPersonal;
        private string correoLaboral;
        private string telefono;
        private string nombre;
        private string ciudad;
        private string apellido;
        private string cedula;
        private string direccion;
        private int idCargo;
        private int idHorario;

        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public string CorreoPersonal { get => correoPersonal; set => correoPersonal = value; }
        public string CorreoLaboral { get => correoLaboral; set => correoLaboral = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Ciudad { get => ciudad; set => ciudad = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Cedula { get => cedula; set => cedula = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public int IdHorario { get => idHorario; set => idHorario = value; }

        public tbl_Empleado()
        {
        }
    }
}
