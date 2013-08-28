using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GWydiR.Models;

namespace GWydiRUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GWydiRUI ui = new GWydiRUI();
            GWydiRModel model = new GWydiRModel();

            GWydiRConfigModel viewModel = new GWydiRConfigModel(ui,model);

            Application.Run(ui);
        }
    }
}
