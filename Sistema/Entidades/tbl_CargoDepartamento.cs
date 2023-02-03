using System;
namespace Sistema.Entidades
{
    public class tbl_CargoDepartamento
    {
        private int idCargoDepartamento;
        private int idCargo;
        private int idDepartamento;
        private string descripcion;

        public int IdCargoDepartamento { get => idCargoDepartamento; set => idCargoDepartamento = value; }
        public int IdCargo { get => idCargo; set => idCargo = value; }
        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public tbl_CargoDepartamento()
        {
        }

       
    }
}
