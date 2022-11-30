using System;
using System.Collections.Generic;
using System.Text;

namespace XAMLIslandTransparency
{
    public class Program
    {
        [System.STAThreadAttribute()]
        public static void Main()
        {
            using (new XAMLIslandUWP.App())
            {
                var app = new XAMLIslandTransparency.App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }
}
