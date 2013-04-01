﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using Ionic.Zip;
using Microsoft.EMIC.Cloud.ApplicationRepository;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System.Web;
using System.Runtime.Serialization;
using Microsoft.EMIC.Cloud.GenericWorker;
using Microsoft.EMIC.Cloud.DataManagement;
using Microsoft.EMIC.Cloud.Storage.Azure;
using System.ServiceModel;
using Microsoft.EMIC.Cloud.Security;
using System.Security.Cryptography.X509Certificates;

namespace GWydiR
{
    class Program
    {
        private static string UserDataStoreConnectionString;

        static void Main(string[] args)
        {
            #region select input data

            Console.WriteLine("Location of parameter file is " + Properties.Settings.Default.setupParams);
            Console.WriteLine("Enter new location or press <Enter> if you want to keep it");

            var newParamsFile = Console.ReadLine();
            if ( newParamsFile != String.Empty ){
                 Properties.Settings.Default.setupParams = newParamsFile;
                 Properties.Settings.Default.Save();
            }

            var paramsData = readParams();
            string doUpload = "";
            string dataKeyFile = "";

            if ( paramsData.ContainsKey("doUpload")){
                doUpload = paramsData["doUpload"];
                if ( paramsData.ContainsKey("dataKeyFile")){
                    dataKeyFile = paramsData["dataKeyFile"];
                }
                else{
                    Console.Write("If you wish to upload the R environment, you must set the access to mass store via the dataKeyFile");
                    Console.Write("Press Return to Exit");
                    var leaving = Console.ReadLine();
                    Environment.Exit(-1);
                }
            }

            
            string RFileName = getParamVariable(paramsData,"RFileName",@"C:\Users\hugh\Dropbox\ForAnne\Script1.R");

            string userZipFileName = getParamVariable(paramsData,"userZipFileName",@"C:\Users\hugh\Dropbox\ForAnne\RExtraFiles.zip");

            string csvFileName = getParamVariable(paramsData,"csvFileName",@"C:\Users\hugh\list.txt");

            string outputRoot = getParamVariable(paramsData,"outputRoot",@"test");

            string appKeyFile = getParamVariable(paramsData,"appKeyFile",@"C:\Users\hugh\appKeyFile.txt");

            string myApplicationName = getParamVariable(paramsData,"ApplicationURL",@"http://www.shanahanlab.org/GWydiR");

			string serviceURL = getParamVariable(paramsData,"serviceURL",@"http://ctqmgwdemo.cloudapp.net/");

			UserDataStoreConnectionString = getConnectionString(appKeyFile);

            #endregion

            #region Upload application zip
            if ( doUpload.Equals("y") ){
       
                Console.Write("Uploading application ZIP.. ");

                Reference appReference = null;
                Reference descReference = null;

            // upload and install application - creates application package and description
                UploadApplication(UserDataStoreConnectionString, myApplicationName, dataKeyFile, out appReference, out descReference);

            }

            #endregion

            #region Set up containers for input, output etc.

            var account = CloudStorageAccount.Parse(UserDataStoreConnectionString);
            var blobClient = account.CreateCloudBlobClient();

            // Initial set up of container
            // Create a new container (folder) for storing the application
            CloudBlobContainer appDataContainer = blobClient.GetContainerReference("applicationcontainer");
            appDataContainer.CreateIfNotExist();

            // Create a second container for storing the input and output files

            var blobContainer = blobClient.GetContainerReference("testcontainer");
            blobContainer.CreateIfNotExist();

            var items = determineJobs(csvFileName);

            foreach ( string item in items ){

             // The job description tells the GenericWorker values for arguments and which files need to be downloaded before and uploaded after execution
                VENUSJobDescription mySimpleJobDescription = new VENUSJobDescription();
                mySimpleJobDescription.ApplicationIdentificationURI = myApplicationName;    // This needs to be the same URI as used when creating the application description
                mySimpleJobDescription.CustomerJobID = outputRoot + " Job " + item + DateTime.Now.ToLocalTime().ToString();
                mySimpleJobDescription.JobName = outputRoot + " Job " + item;

            // The GenericWorker needs to know where to find the application and description
                mySimpleJobDescription.AppPkgReference = new Reference(new AzureBlobReference(appDataContainer.GetBlobReference(HttpUtility.UrlEncode(mySimpleJobDescription.ApplicationIdentificationURI) + "_App"), UserDataStoreConnectionString));
                mySimpleJobDescription.AppDescReference = new Reference(new AzureBlobReference(appDataContainer.GetBlobReference(HttpUtility.UrlEncode(mySimpleJobDescription.ApplicationIdentificationURI) + "_Desc"), UserDataStoreConnectionString));

   
            // The application description says, that a several parameters are needed to execute the application.
            // So let's fill them with values.

            // First set up the R script for upload
                createArgument(RFileName, "RFile", mySimpleJobDescription, blobContainer);


            // Then the list of GSE's
                string jobFileName = item + @".csv";
                
                writeJobToFile(item, jobFileName);

                createArgument(jobFileName, "GSEs", mySimpleJobDescription, blobContainer);

            // Now the output file - we generate a unique file name for this.
                var nameOut = outputRoot + item + DateTime.Now.ToBinary() + ".log";
                createArgument(jobFileName, "OutputFile", mySimpleJobDescription, blobContainer,nameOut: nameOut);

            // Finally an optional zip file of additional material
                createArgument(userZipFileName, "userZip", mySimpleJobDescription, blobContainer);
        
                Console.WriteLine("Done");


            #region Submit job


                Console.WriteLine("Submitting job for item " + item);

                Func<GenericWorkerJobManagementClient> CreateUnprotectedClient = () =>
                {
                    var svcUrl = serviceURL + "JobSubmission/Service.svc";
                //ConfigurationManager.AppSettings["Microsoft.EMIC.Cloud.GenericWorker.URL"];
                    Console.WriteLine(string.Format("Submitting all jobs to {0}", svcUrl));

                    return GenericWorkerJobManagementClient.CreateUnprotectedClient(svcUrl);
                };

                var submissionPortal = CreateUnprotectedClient();
               


            // The order of job submissions does not matter, because the jobs are data driven
            //

                submissionPortal.SubmitVENUSJob(mySimpleJobDescription);

               

            }
            #endregion

            #endregion
 
            Console.WriteLine("Done");

           // var allJobs = submissionPortal.GetAllJobs();

            Console.WriteLine();
            Console.WriteLine("Press enter to quit");
            Console.ReadLine();
        }

        private static string getParamVariable(Dictionary<string,string> paramsData, string query, string otherwise)
        {
            string result = "";
            if ( paramsData.ContainsKey(query)) {
                result = paramsData[query];
            }
            else{
                result = otherwise; 
            }
            return result;
        }

        // Method for creating 
        private static void createArgument(string FileName, string commandName, VENUSJobDescription mySimpleJobDescription, CloudBlobContainer blobContainer, string nameOut="")
        {
   
            var commandBlob = blobContainer.GetBlobReference(FileName);
            commandBlob.UploadFile(FileName);

            var commandFile = new AzureArgumentSingleReference();
            commandFile.Name = commandName;               // This has to be the same name as in the application description
            if ( nameOut == "" ){
                commandFile.DataAddress = commandBlob.Uri.AbsoluteUri;
            }
            else{
                commandFile.DataAddress = blobContainer.GetBlobReference(nameOut).Uri.AbsoluteUri;
            }
            commandFile.ConnectionString = UserDataStoreConnectionString;
            mySimpleJobDescription.JobArgs.Add(commandFile);
        }

        private static void UploadApplication(string UserDataStoreConnectionString, string applicationIdentificationURI, string dataKeyFile, out Reference appReference, out Reference descReference)
        {
            var account = CloudStorageAccount.Parse(UserDataStoreConnectionString);
            var blobClient = account.CreateCloudBlobClient();
            CloudBlobContainer appDataContainer = blobClient.GetContainerReference("applicationcontainer");
            appDataContainer.CreateIfNotExist();

            #region Upload application
            String sourceFile = @".\RGW.zip";
            String destFile = @".\app.zip";
            String destKeyFile = @".\data.key";
            
            System.IO.File.Copy(sourceFile, destFile, true);
            // standard name for key file when uploaded
            System.IO.File.Copy(dataKeyFile, destKeyFile, true);

            addFileToZip(destFile, destKeyFile);
            System.IO.File.Delete(destKeyFile);

            #region Zip application
            FileStream instream = File.OpenRead(destFile);
            MemoryStream ConsoleRZipBytes = new MemoryStream();

            ConsoleRZipBytes.SetLength(instream.Length);
            instream.Read(ConsoleRZipBytes.GetBuffer(), 0, (int)instream.Length);

            ConsoleRZipBytes.Flush();
            instream.Close();

            ConsoleRZipBytes.Seek(0L, SeekOrigin.Begin);
            #endregion

            #region upload and register application

            VENUSApplicationDescription appDesc = new VENUSApplicationDescription()
            {
                ApplicationIdentificationURI = applicationIdentificationURI,
                CommandTemplate = new VENUSCommandTemplate()
                {
                    Path = string.Empty,
                    Executable = "RunInAzure.bat",
                    Args = new List<CommandLineArgument>() 
                    {
                        new CommandLineArgument() { 
                            Name = "RFile", 
                            FormatString = "{0}", 
                            Required = Required.Mandatory,
                            CommandLineArgType = CommandLineArgType.SingleReferenceInputArgument
                        },
                        new CommandLineArgument() { 
                            Name = "GSEs", 
                            FormatString = "{0}", 
                            Required = Required.Mandatory,
                            CommandLineArgType = CommandLineArgType.SingleReferenceInputArgument
                        },
                        new CommandLineArgument() { 
                            Name = "OutputFile", 
                            FormatString = "{0}", 
                            Required = Required.Mandatory,
                            CommandLineArgType = CommandLineArgType.SingleReferenceOutputArgument
                        },
                        new CommandLineArgument() { 
                            Name = "userZip", 
                            FormatString = "{0}", 
                            Required = Required.Optional,
                            CommandLineArgType = CommandLineArgType.SingleReferenceInputArgument
                        }
                    }
                }
            };

            Func<VENUSApplicationDescription, CloudBlob> uploadAppDesc = ((appDescription) =>
            {
                var blobName = HttpUtility.UrlEncode(appDescription.ApplicationIdentificationURI) + "_Desc";
                DataContractSerializer dcs = new DataContractSerializer(typeof(VENUSApplicationDescription));
                MemoryStream msxml = new MemoryStream();
                dcs.WriteObject(msxml, appDescription);
                CloudBlob xmlBlob = appDataContainer.GetBlobReference(blobName);
                xmlBlob.Properties.ContentType = "text/xml";
                xmlBlob.UploadByteArray(msxml.ToArray());
                return xmlBlob;
            });

            Func<string, MemoryStream, CloudBlob> uploadApp = (appURI, zipBytes) =>
            {
                var blobName = HttpUtility.UrlEncode(appURI) + "_App";
                CloudBlob applicationBlob = appDataContainer.GetBlobReference(blobName);
                applicationBlob.UploadByteArray(zipBytes.ToArray());

                return applicationBlob;
            };



            CloudBlob appDescBlob = uploadAppDesc(appDesc);
            CloudBlob appBlob = uploadApp(appDesc.ApplicationIdentificationURI, ConsoleRZipBytes);
           

            Console.WriteLine(string.Format("Uploaded {0} bytes", ConsoleRZipBytes.Length));
            #endregion


            appReference = new Reference(new AzureBlobReference(appBlob, UserDataStoreConnectionString));
            descReference = new Reference(new AzureBlobReference(appDescBlob, UserDataStoreConnectionString));

             System.IO.File.Delete(destFile);
            
            #endregion
        }

        #region IO methods
        private static void addFileToZip(string zipFile, string filename)
        {
            try
            {
                using (ZipFile zip = new ZipFile(zipFile))
                {
                    zip.AddFile(filename, "");
                    zip.Save();
                }
            }
            catch (System.Exception ex1)
            {
                System.Console.Error.WriteLine("exception: " + ex1);
            }
        }

        private static Dictionary<string, string> readParams()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            try
            {
                using (StreamReader sr = new StreamReader(Properties.Settings.Default.setupParams))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        if (!line.Equals(""))
                        {
                            String[] words = line.Split(' ');
                            dictionary.Add(words[0], words[1]);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("readParams The file " + Properties.Settings.Default.setupParams + " could not be read:");
                Console.WriteLine(e.Message);
            }

            return dictionary;
        }

		private static string getConnectionString(string fileName)
        {

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    String AccountName = sr.ReadLine();
                    String AccountKey = sr.ReadLine();
					string connectionString = "DefaultEndpointsProtocol=https;AccountName="+AccountName+";AccountKey="+AccountKey;
					return connectionString;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("getConnectionString The file " +  fileName + "could not be read:");
                Console.WriteLine(e.Message);
                return "";
            }

        }

        private static List<string> determineJobs(string jobFileName)
        {
            List<string> list = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(jobFileName))
                {
                    while (!sr.EndOfStream)
                    {

                        String job = sr.ReadLine();
                        if (!job.Equals(""))
                        {
                            list.Add(job);
                        }
                    }    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("determineJobs The file " + jobFileName + "could not be read:");
                Console.WriteLine(e.Message);
               
            }
            return list;

        }

        private static void writeJobToFile( string job, string jobFileName )
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(jobFileName))
                {
                    sw.WriteLine(job);
                    sw.Close();
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("write The file " + jobFileName + "could not be written to:");
                Console.WriteLine(e.Message);
               
            }
           
        }   

        #endregion

    }

    
}


