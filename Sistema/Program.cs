using System;
using Gtk;

namespace Sistema
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Application.Init();
            //MainWindow win = new MainWindow();
            //win.Show();
            MarcarAsistencia mc = new MarcarAsistencia();
            mc.Show();
            Application.Run();
        }
    }
}
