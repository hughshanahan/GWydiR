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

        /// <summary>
        /// A method to be called when a new item is selected in the SID list
        /// </summary>
        public event GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler ChangeSelectedSID;

        /// <summary>
        /// A method to be called when a new item is selected from the cert list
        /// </summary>
        public event GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler ChangedSelectCert;

        public GWydiRWizardUI()
        {
            InitializeComponent();
            CreateSubscriptionBtn.Enabled = false;
            NavigateNextBtn.Enabled = false;
        }

        public void  DisplaySubsriptions(List<string> subscriptions)
        {
            SIDComboBx.DataSource = null;
            SIDComboBx.DataSource = subscriptions;
            SIDComboBx.SelectedIndex = subscriptions.Count - 1;
        }

        public void  DisplayCertificates(List<string> certificates)
        {
            CertComboBx.DataSource = null;
            CertComboBx.DataSource = certificates;
            CertComboBx.SelectedIndex = certificates.Count - 1;
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

        //This is a method that should throw an event to as to allow the mdoel to deal with it
        //fairly complex logic here, i should find a way to move this out and test it
        private void SIDComboBx_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int index = 0;
            if (ChangeSelectedSID != null)
                index = ChangeSelectedSID(SIDComboBx.SelectedIndex);

            if (index > -1)
            {
                CertComboBx.SelectedIndex = index;
                CertComboBx.Text = ((List<string>)CertComboBx.DataSource)[index];
            }
        }

        public void RegisterChangedSIDSelection(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler)
        {
            ChangeSelectedSID += handler;
        }

        public void DeRegisterChangedSIDSelected(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler)
        {
            ChangeSelectedSID -= handler;
        }


        public void EnableNext()
        {
            NavigateNextBtn.Enabled = true;
        }

        public void DisableNext()
        {
            NavigateNextBtn.Enabled = false;
        }

        public void EnableCreate()
        {
            CreateSubscriptionBtn.Enabled = true;
        }

        public void DisableCreate()
        {
            CreateSubscriptionBtn.Enabled = false;
        }

        public void RegisterChangedCertificateSelection(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler)
        {
            ChangedSelectCert += handler;
        }

        public void DeRegisterChangedCertificateSelection(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler)
        {
            ChangedSelectCert -= handler;
        }

        public void RegisterCreate(EventHandler handler)
        {
            CreateSubscriptionBtn.Click += handler;
        }

        public void DeRegisterCreate(EventHandler handler)
        {
            CreateSubscriptionBtn.Click -= handler;
        }

        private void CertComboBx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChangedSelectCert != null)
                ChangedSelectCert(CertComboBx.SelectedIndex);
        }
    }
}
