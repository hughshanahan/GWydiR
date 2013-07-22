using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GWydiR;

namespace WindowsFormsApplication1
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

            GWydiRWizardUI ui = new GWydiRWizardUI();
            Wizard wizard = new Wizard();
            AuthorisationModel authModel = new AuthorisationModel(ui, wizard);

            Application.Run(ui);
        }
    }
}
