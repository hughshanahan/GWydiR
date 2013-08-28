using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ModelInterfaces
{
    interface IGWydiRModel
    {
        void SetConfiguration(bool doUpload, string appName, 
            string appSrviceURL, string appStorageKey, 
            string dataStorageKey, string scriptFileName, 
            string userZipFilename, string listOfJobsFileName, 
            string rootFileOutputForLogs);
    }
}
