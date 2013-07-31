using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ViewInterfaces;
using GWydiR.Interfaces.ModelInterfaces;

namespace GWydiR.Models
{
    public class ConfigurationModel
    {
        const int TabNumber = 1;
        IConfigurationView view;
        IWizard wizard;
        string CloudCerviceName;
        string AppStoreName;
        string AppStoreKey;
        string DataStoreName;
        string DataStoreKey;

        public ConfigurationModel()
        {

        }

        public ConfigurationModel(IConfigurationView view, IWizard wizard)
        {
            this.view = view;
            ITabNavigation navView = (ITabNavigation)view;
            navView.RegisterNext(NextHandler,TabNumber);
            navView.RegisterPrevious(PreviousHandler,TabNumber);


            this.wizard = wizard;
        }

        public void NextHandler(object sender, EventArgs args)
        {
            CloudCerviceName = view.GetCloudServiceName();
            AppStoreName = view.GetAppStoreName();
            AppStoreKey = view.GetAppStoreKey();
            DataStoreName = view.GetDataStoreName();
            DataStoreKey = view.GetDataStoreKey();
        }

        public void PreviousHandler(object sender, EventArgs args)
        {
            ITabNavigation navView = (ITabNavigation)view;
            navView.ChangeTab(0);
        }
    }
}
