using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ModelInterfaces
{
    public interface IGWydiRModel
    {
        void SetConfiguration(bool doUpload, string appName, 
            string appServiceURL,string appStoreName, string appStorageKey, 
            string dataStoreName, string dataStorageKey, string scriptFileName, 
            string userZipFilename, string listOfJobsFileName, 
            string rootFileOutputForLogs);
        void Run();
    }
}
