using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Utilities;
using System.Diagnostics;
using System.Configuration;

namespace GWydiR.Models
{
    public class GWydiRModel : IGWydiRModel
    {
        public string paramFileName { get; set; }

        public string paramFilePath { get; set; }

        public string GWydiRAppExeName { get; set; }

        public GWydiRModel()
        {
            paramFilePath = ConfigurationManager.AppSettings.Get("ParamFolderPath");
            GWydiRAppExeName = ConfigurationManager.AppSettings.Get("GWydiRAppPath");
        }

        /// <summary>
        /// This is a method to set the configuration values so that GWydiR can run.
        /// </summary>
        /// <param name="doUpload">Bool value informs GWydiR whether to upload the application or not</param>
        /// <param name="appName">String value is the name of the cloud service that the application is to run on.</param>
        /// <param name="appServiceURL">String value this is the URL at which the clouse service can be accesses [appname.cloud.net]</param>
        /// <param name="appStorageKey">String value, this is the access key provide bby azure for a particular storage account</param>
        /// <param name="dataStorageKey">String value, this is the access key provide bby azure for a particular storage account</param>
        /// <param name="scriptFileName">String value, Full path to the script that the user wishes to run on azure</param>
        /// <param name="userZipFilename">String value, Full path of zip file containg any extra resources the user application requires</param>
        /// <param name="listOfJobsFileName">Sring value, full path of .csv containing a list of data to be processed</param>
        /// <param name="rootFileOutputForLogs">String value,</param>
        public void SetConfiguration(bool doUpload, string appName, string appServiceURL, string appStorageKey, string dataStorageKey, string scriptFileName, string userZipFilename, string listOfJobsFileName, string rootFileOutputForLogs)
        {

            paramFileName = appName + "_params.txt";

            // Here we should create a parameters file and write it to the file system somewhere
            // reachable in the future, e.g. C:\\Programs Files x86\GWydiR\ParameterFiles. The 
            // location of this file should be persitent over the lfit of this class so that 
            // another method may start the GWydiR Process passing it the location of the
            // parameters file.

            //First we are required to write two key files for GWydiR to read
            //One for the app store key
            string AppStoreKeyFile = paramFilePath + "";


            //One for the data store key

            // make a list of strings with the key and value pairs for the parameter file
            List<string> parameters = new List<string>();

            // if doUpload == tru then put y else n
            parameters.Add("doUpload " + ((doUpload == true) ? "y" : "n"));
            parameters.Add("dataKey " + dataStorageKey);
            parameters.Add("RFileName " + scriptFileName);
            parameters.Add("userZipFile " + userZipFilename);
            parameters.Add("csvFileName " + listOfJobsFileName);
            parameters.Add("outputRoot " + rootFileOutputForLogs);
            parameters.Add("appKey " + appStorageKey);
            parameters.Add("myApplicationName " + appName);
            parameters.Add("serviceURL " + appServiceURL);

            //write the parameter file to a location on disk
            FileWriter fileWriter = makeFileWriter();
            fileWriter.Write(paramFilePath + paramFileName, parameters);
        }

        protected virtual FileWriter makeFileWriter()
        {
            return new FileWriter();
        }


        /// <summary>
        /// This method should begin the process of uploading and running the application
        /// as defined in some parameters file.
        /// </summary>
        public void Run()
        {
            Process GWydiR = new Process();
            GWydiR.StartInfo.Arguments = paramFilePath.Replace("\\\\","\\").Replace(" ","\\ ") + paramFileName;
            GWydiR.StartInfo.FileName = GWydiRAppExeName;
            try
            {
                GWydiR.Start();
            }
            catch (Exception e)
            {
                //do something
            }
        }


        public void SetConfiguration(bool doUpload, string appName, string appServiceURL, string appStoreName, string appStorageKey, string dataStoreName, string dataStorageKey, string scriptFileName, string userZipFilename, string listOfJobsFileName, string rootFileOutputForLogs)
        {
            paramFileName = appName + "_params.txt";

            // Here we should create a parameters file and write it to the file system somewhere
            // reachable in the future, e.g. C:\\Programs Files x86\GWydiR\ParameterFiles. The 
            // location of this file should be persitent over the lfit of this class so that 
            // another method may start the GWydiR Process passing it the location of the
            // parameters file.

            //First we are required to write two key files for GWydiR to read
            //One for the app store key
            string appStoreKeyFile = paramFilePath + appStoreName + ".key";
            FileWriter fileWriter = makeFileWriter();
            fileWriter.Write(appStoreKeyFile, new List<string>() { appStoreName, appStorageKey });


            //One for the data store key
            string dataStoreKeyFile = paramFilePath + dataStoreName + ".key";
            fileWriter.Write(dataStoreKeyFile, new List<string>() { dataStoreName, dataStorageKey });

            // make a list of strings with the key and value pairs for the parameter file
            List<string> parameters = new List<string>();

            // if doUpload == tru then put y else n
            parameters.Add("doUpload " + ((doUpload == true) ? "y" : "n"));
            parameters.Add("dataKeyFile " + dataStoreKeyFile);
            parameters.Add("RFileName " + scriptFileName);
            parameters.Add("userZipFile " + userZipFilename);
            parameters.Add("csvFileName " + listOfJobsFileName);
            parameters.Add("outputRoot " + rootFileOutputForLogs);
            parameters.Add("appKeyFile " + appStoreKeyFile);
            parameters.Add("myApplicationName " + appName);
            parameters.Add("serviceURL " + appServiceURL);

            //write the parameter file to a location on disk
            fileWriter.Write(paramFilePath + paramFileName, parameters);
        }
    }
}
