using System;
namespace Sistema.Entidades
{
    public class tbl_CargoEmpleado
    {
        private int idEmpleadoCargo;
        private int idCargo;
        private int idEmpleado;

        public int IdEmpleadoCargo { get => idEmpleadoCargo; set => idEmpleadoCargo = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public tbl_CargoEmpleado()
        {
        }


    }
}
