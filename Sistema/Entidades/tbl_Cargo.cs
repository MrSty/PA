using System;
namespace Sistema.Entidades
{
    public class tbl_Cargo
    {
        private int idCargo;
        private string nombre;
        private string descripcion;
        private int idDepartamento;

        public int IdCargo { get => idCargo; set => idCargo = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }

        public tbl_Cargo()
        {
        }
    }
}
