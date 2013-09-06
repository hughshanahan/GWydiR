using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;
using GWydiR.Utilities;
using System.Configuration;
using System.Diagnostics;

namespace GWydiR.Models
{
    public class ProductionModel
    {
        private const int TabNumber = 2;
        private int NumberOfInstances;
        private IWizard wizard;
        private IProductionView view;
        private string VMSize;

        public ProductionModel()
        { }

        public ProductionModel(IProductionView view, IWizard wizard)
        {
            this.view = view;
            // This reads the folder with VMSizes in, formatting the names into a list
            // and delivering the to the UI to be drawn in the combo box
            FileEnumerator FileEnum = makeFileEnumerator();
            view.SetVmSizes(FileEnum.Enumerate(ConfigurationManager.AppSettings["VMSizesFolder"]));

            ITabNavigation navView = castNavigationView(view);
            navView.RegisterNext(NextHandler, TabNumber);
            navView.RegisterPrevious(PreviousHandler, TabNumber);

            this.wizard = wizard;
        }

        protected virtual FileEnumerator makeFileEnumerator()
        {
            return new FileEnumerator();
        }

        protected virtual ITabNavigation castNavigationView(IProductionView view)
        {
            return (ITabNavigation)view;
        }

        //One next is slected from the production view, the .cscfg file should be written to the desktop.
        //Since the production model it's self does not hold all of the required data it must delagte this task to
        // an object that does hold refference to all the required data. The wizard would be an ideal such obect.
        public void NextHandler(object sender, EventArgs args)
        {
            NumberOfInstances = view.GetInstanceCount();
            wizard.InstanceCount = NumberOfInstances;
            wizard.WriteConfigurationFile();
            
            //Need to open GWyDirUI and Close this UI.
            
            //Need to copy file defining VM onto the desktop
            VMSize = view.GetVmSize();
            wizard.VMSize = VMSize;
            try
            {
                wizard.CopyVmFileToDesktop();
            } catch (Exception exc)
            {
                ((IViewError)view).NotifyOfError(exc);
            }

            // Start GWydiRUI window
            Process GwydirUI = new Process();
            GwydirUI.StartInfo.FileName = ConfigurationManager.AppSettings.Get("GWydiRUI");
            GwydirUI.Start();

        }

        public void PreviousHandler(object sender, EventArgs args)
        {
            ITabNavigation navView = castNavigationView(view);
            navView.ChangeTab(TabNumber - 1);
        }

    }
}
