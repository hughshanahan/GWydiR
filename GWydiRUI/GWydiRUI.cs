using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GWydiR.Interfaces.ViewInterfaces;

namespace GWydiRUI
{
    public partial class GWydiRUI : Form, IGWydiRConfigView, IViewError
    {
        public GWydiRUI()
        {
            InitializeComponent();
        }

        public bool GetDoUpload()
        {
            return DoUploadCheckBox.Checked;
        }

        public void SetDoUpload(bool doUpload)
        {
            DoUploadCheckBox.Checked = doUpload;
        }

        public string GetAppName()
        {
            return AppNameTextBox.Text;
        }

        public void SetAppName(string appName)
        {
            AppNameTextBox.Text = appName;
        }

        public string GetServiceURL()
        {
            return ServiceURLTextBox.Text;
        }

        public void SetServiceURL(string serviceURL)
        {
            ServiceURLTextBox.Text = serviceURL;
        }

        public string GetAppStorageKey()
        {
            return AppStorageKeyTextBox.Text;
        }

        public void SetAppStorageKey(string appStorageKey)
        {
            AppStorageKeyTextBox.Text = appStorageKey;
        }

        public string GetDataStorageKey()
        {
            return DataStorageKeyTextBox.Text;
        }

        public void SetDataStorageKey(string dataStorageKey)
        {
            DataStorageKeyTextBox.Text = dataStorageKey;
        }

        public string GetFullScriptFileName()
        {
            return RFileNameTextBox.Text;
        }

        public void SetFullScriptFileName(string scriptFileName)
        {
            RFileNameTextBox.Text = scriptFileName;
        }

        public string GetFullUserZipFileName()
        {
            return UserZipFileTextBox.Text;
        }

        public void SetFullUserZipFileName(string userZipFileName)
        {
            UserZipFileTextBox.Text = userZipFileName;
        }

        public string GetFullListOfJobsCSVFileName()
        {
            return ListOfWorkItemsTextBox.Text;
        }

        public void SetFullListOfJobsCSVFileName(string listOfJobsFileName)
        {
            ListOfWorkItemsTextBox.Text = listOfJobsFileName;
        }

        public string GetRootFileOutForLogs()
        {
            return LogFileRootTextBox.Text;
        }

        public void SetRootFileOutForLogs(string rootFileOutForLogs)
        {
            LogFileRootTextBox.Text = rootFileOutForLogs;
        }


        public void RegisterRunButton(EventHandler handler)
        {
            UploadRunButton.Click += handler;
        }

        public void DeRegisterRunButton(EventHandler handler)
        {
            UploadRunButton.Click -= handler;
        }

        public void NotifyOfError(Exception e)
        {
            MessageBox.Show(e.Message);
        }


        public string GetAppStorageContainerName()
        {
            return AppStorageContainerNameTextBox.Text;
        }

        public void SetAppStorageContainerName(string appStorageContainerName)
        {
            AppStorageContainerNameTextBox.Text = appStorageContainerName;
        }

        public string GetDataStorageContainerName()
        {
            return DataStorageContainerNameTextBox.Text;
        }

        public void SetDataStorageContainerName(string dataStorageContainerName)
        {
            DataStorageContainerNameTextBox.Text = dataStorageContainerName;
        }
    }
}
