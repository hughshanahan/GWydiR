using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ViewInterfaces;
using GWydiR.Interfaces.ModelInterfaces;

namespace GWydiR.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GWydiRConfigModel
    {

        /// <summary>
        /// Refference to the view object
        /// </summary>
        IGWydiRConfigView View;

        IGWydiRModel Model;

        public GWydiRConfigModel() { }

        /// <summary>
        /// Constructor to intialize the internal refference to the 
        /// view and to register a handler for the run button.
        /// </summary>
        /// <param name="view"></param>
        public GWydiRConfigModel(IGWydiRConfigView view, IGWydiRModel model)
        {
            this.View = view;
            this.Model = model;
            View.RegisterRunButton(UploadAndRunHandler);
        }

        /// <summary>
        /// Handler that is called when the run button click event is fired.
        /// It will collect all the configuration data from the view and 
        /// pass it to a model to handle processing that configuration data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void UploadAndRunHandler(object sender, EventArgs args)
        {
            Model.SetConfiguration(View.GetDoUpload(), 
                View.GetAppName(), 
                View.GetServiceURL(), 
                View.GetAppStorageKey(), 
                View.GetDataStorageKey(), 
                View.GetFullScriptFileName(), 
                View.GetFullUserZipFileName(), 
                View.GetFullListOfJobsCSVFileName(), 
                View.GetRootFileOutForLogs());
        }
    }
}
