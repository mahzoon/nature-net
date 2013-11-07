using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nature_net
{
    public class program : System.Windows.Application
    {
        [STAThread]
        public static void Main()
        {
            program p = new program();
            p.StartupUri = new System.Uri("main_window.xaml", System.UriKind.Relative);
            p.Run();
        }
    }
}
