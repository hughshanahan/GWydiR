using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using GWydiR;
using GWydiR.Interfaces.ViewInterfaces;

namespace WindowsFormsApplication1
{
    public partial class GWydiRWizardUI : Form, IAuthorisationView, ITabNavigation, IViewError
    {

        /// <summary>
        /// Event to be called when the AddNewSidBtn is clicked
        /// </summary>
        public event GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler NewSubscription;

        /// <summary>
        /// Event to be called when the AddNewCertBtn is cliked
        /// </summary>
        public event GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler NewCertificate;

        public GWydiRWizardUI()
        {
            InitializeComponent();
        }

        public void  DisplaySubsriptions(List<string> subscriptions)
        {
            SIDComboBx.DataSource = null;
            SIDComboBx.DataSource = subscriptions;
        }

        public void  DisplayCertificates(List<string> certificates)
        {
            CertComboBx.DataSource = certificates;
        }

        public string  GetSelectedSubscription()
        {
            return SIDComboBx.SelectedText;
        }

        public string  GetSelectedCertificate()
        {
            return CertComboBx.SelectedText;
        }

        public void RegisterNext(EventHandler nextHandler)
        {
            NavigateNextBtn.Click += nextHandler;
        }

        public void RegisterPrevious(EventHandler PreviousHandler)
        {
            // No previous Button yet
        }


        public void DeRegisterNext(EventHandler nextHandler)
        {
            NavigateNextBtn.Click -= nextHandler;
        }

        public void DeRegisterPrevious(EventHandler previousHandler)
        {
            // no previous button
        }


        public void RegisterNewSubscription(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler)
        {
            NewSubscription += handler;
        }

        public void DeRegisterNewSubscription(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler)
        {
            NewSubscription -= handler;
        }

        private void AddNewSIDBtn_Click(object sender, EventArgs e)
        {
            // open new gui
            string newSID = Interaction.InputBox("Please enter a new Subscription ID", "New Subscription ID");
            // raise the event
            if(NewSubscription != null)
                NewSubscription(newSID);
        }

        public void NotifyOfError(Exception e)
        {
            MessageBox.Show(e.Message);
        }


        public void RegisterNewCertificate(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler)
        {
            NewCertificate += handler;
        }

        public void DeRegisterNewCerticate(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler)
        {
            NewCertificate -= handler;
        }

        private void AddNewCertBtn_Click(object sender, EventArgs e)
        {
            // open mesage box
            String newCertificate = Interaction.InputBox("Please eventer to whom the certificate is being issued", "Subject Name");
            // raise event if not empty
            if (NewCertificate != null)
                NewCertificate(newCertificate);
        }
    }
}
