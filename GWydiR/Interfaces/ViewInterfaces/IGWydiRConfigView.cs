using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    public interface IGWydiRConfigView
    {
        bool GetDoUpload();
        void SetDoUpload(bool doUpload);

        string GetAppName();
        void SetAppName(string appName);

        string GetServiceURL();
        void SetServiceURL(string serviceURL);

        string GetAppStorageKey();
        void SetAppStorageKey(string appStorageKey);

        string GetDataStorageKey();
        void SetDataStorageKey(string dataStorageKey);

        string GetFullScriptFileName();
        void SetFullScriptFileName(string scriptFileName);

        string GetFullUserZipFileName();
        void SetFullUserZipFileName(string userZipFileName);

        string GetFullListOfJobsCSVFileName();
        void SetFullListOfJobsCSVFileName(string listOfJobsFileName);

        string GetRootFileOutForLogs();
        void SetRootFileOutForLogs(string rootFileOutForLogs);

        void RegisterRunButton(EventHandler handler);

        void DeRegisterRunButton(EventHandler handler);
    }
}
