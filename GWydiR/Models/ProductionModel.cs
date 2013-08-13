using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;

namespace GWydiR.Models
{
    public class ProductionModel
    {
        private const int TabNumber = 2;
        private int NumberOfInstances;
        private IWizard wizard;
        private IProductionView view;

        public ProductionModel()
        { }

        public ProductionModel(IProductionView view, IWizard wizard)
        {
            this.view = view;
            ITabNavigation navView = castNavigationView(view);
            navView.RegisterNext(NextHandler, TabNumber);
            navView.RegisterPrevious(PreviousHandler, TabNumber);

            this.wizard = wizard;
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
            ITabNavigation navView = castNavigationView(view);
            //navView.ChangeTab(TabNumber + 1); //Doesn't exist yet
        }

        public void PreviousHandler(object sender, EventArgs args)
        {
            ITabNavigation navView = castNavigationView(view);
            navView.ChangeTab(TabNumber - 1);
        }

    }
}
