using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    /// <summary>
    /// Interface to define the visible methods of a configuration view
    /// </summary>
    public interface IConfigurationView
    {
        string GetCloudServiceName();
        string GetAppStoreName();
        string GetAppStoreKey();
        string GetDataStoreName();
        string GetDataStoreKey();
    }
}
