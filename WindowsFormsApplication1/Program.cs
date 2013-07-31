using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GWydiR;
using GWydiR.Models;

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
            Wizard wizard = new Wizard(); // need to add an object to congiure the wizard from subscritions/config files
            AuthorisationModel authModel = new AuthorisationModel(ui, wizard);
            ConfigurationModel confModel = new ConfigurationModel(ui, wizard);

            Application.Run(ui);
        }
    }
}
