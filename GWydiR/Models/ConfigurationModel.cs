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
            ITabNavigation navView = castNavigationView(view);
            navView.RegisterNext(NextHandler,TabNumber);
            navView.RegisterPrevious(PreviousHandler,TabNumber);
            this.wizard = wizard;
        }

        protected virtual ITabNavigation castNavigationView(IConfigurationView view)
        {
            return (ITabNavigation)view;
        }

        public void NextHandler(object sender, EventArgs args)
        {
            CloudCerviceName = view.GetCloudServiceName();
            AppStoreName = view.GetAppStoreName();
            AppStoreKey = view.GetAppStoreKey();
            DataStoreName = view.GetDataStoreName();
            DataStoreKey = view.GetDataStoreKey();

            wizard.AppUrl = GetDNSName();
            wizard.AppStorageAccountConnectionString = GetConnectionString(Flags.AccountType.AppStorage);
            wizard.DataStorageAccountConnectionString = GetConnectionString(Flags.AccountType.DataStorage);

            ITabNavigation navView = castNavigationView(view);
            navView.ChangeTab(TabNumber + 1);
        }

        public void PreviousHandler(object sender, EventArgs args)
        {
            ITabNavigation navView = castNavigationView(view);
            navView.ChangeTab(TabNumber - 1);
        }

        public string GetConnectionString(GWydiR.Flags.AccountType flag)
        {
            string returnString = "";
            switch (flag)
            {
                case Flags.AccountType.AppStorage:
                    returnString = buildConnectionString(view.GetAppStoreName(), view.GetAppStoreKey());
                    break;
                case Flags.AccountType.DataStorage:
                    returnString = buildConnectionString(view.GetDataStoreName(), view.GetDataStoreKey());
                    break;
                case Flags.AccountType.None:
                default:
                    break;
            }
            return returnString;
        }

        private string buildConnectionString(string name, string key)
        {
            return "DefaultEndpointsProtocol=https;AccountName="+name+";AccountKey=" + key;
        }

        public string GetDNSName()
        {
            return buildCloudAppUrl(view.GetCloudServiceName());
        }

        private string buildCloudAppUrl(string serviceName)
        {
            return serviceName + ".cloudapp.net";
        }
    }
}
