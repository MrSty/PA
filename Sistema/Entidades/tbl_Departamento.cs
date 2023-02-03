using System;
namespace Sistema.Entidades
{
    public class tbl_Departamento
    {
        private int idDepartamento;
        private string nombre;
        private int cantEmpleado;
        private string jefeDepartamento;
        private string ext;
        private string gmail;

        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int CantEmpleado { get => cantEmpleado; set => cantEmpleado = value; }
        public string JefeDepartamento { get => jefeDepartamento; set => jefeDepartamento = value; }
        public string Ext { get => ext; set => ext = value; }
        public string Gmail { get => gmail; set => gmail = value; }

        public tbl_Departamento()
        {
        }
    }
}
