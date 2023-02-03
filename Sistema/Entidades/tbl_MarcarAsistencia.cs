using System;
namespace Sistema.Entidades
{
    public class tbl_MarcarAsistencia
    {
        private int idAsistencia;
        private DateTime horaEntrada;
        private DateTime horaSalida;
        private string cedula;

        public int IdAsistencia { get => idAsistencia; set => idAsistencia = value; }
        public DateTime HoraEntrada { get => horaEntrada; set => horaEntrada = value; }
        public DateTime HoraSalida { get => horaSalida; set => horaSalida = value; }
        public string Cedula { get => cedula; set => cedula = value; }


        public tbl_MarcarAsistencia()
        {
        }
    }
}
