using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;
using GWydiR.Exceptions;
using GWydiR.Utilities;
using GWydiR.Containers;
using System.Security.Cryptography.X509Certificates;

namespace GWydiR
{
    /// <summary>
    /// Class who responcibility it is to mirror the data avaliable to the user in the Authorisation Tab of the GWydiR Wizard.
    /// </summary>
    public class AuthorisationModel
    {

        IAuthorisationView authorisationView;

        IWizard wizard;

        string SID;

        string Cert;

        public AuthorisationModel()
        {
        }

        public AuthorisationModel(IAuthorisationView authorisationView)
        {
            this.authorisationView = authorisationView;
            // register eventhandlers with UI
            ITabNavigation genericTabPanel = CastITabNavigation(authorisationView);
            genericTabPanel.RegisterNext(NextHandler);
        }

        public AuthorisationModel(IAuthorisationView authorisationView, IWizard wizard)
        {
            this.wizard = wizard;
            // register events and handlers
            this.authorisationView = authorisationView;
            //register new subscription event handler with event
            authorisationView.RegisterNewSubscription(NewSubscriptionHandler);
            //register new certificate handler with new event
            authorisationView.RegisterNewCertificate(NewCertificateHandler);
            //register new changed SID selection handler
            authorisationView.RegisterChangedSIDSelection(ChangedSIDSelectionHandler);

            ITabNavigation genericPanel = CastITabNavigation(authorisationView);
            genericPanel.RegisterNext(NextHandler);

            // set up display on the view if there are SID's to show
            if (wizard.GetSIDList() != null)
                authorisationView.DisplaySubsriptions(wizard.GetSIDList());
            // set up display on the view if there are certificates to show
            if (wizard.GetCertList() != null)
                authorisationView.DisplayCertificates(wizard.GetCertList());
        }

        // similar to a factory method, this allows a mocked tabNavigationObject to be injected while testing.
        public virtual ITabNavigation CastITabNavigation(IAuthorisationView authorisationView)
        {
            return (ITabNavigation)authorisationView;
        }

        /// <summary>
        /// This method should deal with navgating forward from the Authentication tab
        /// This should involve, generating a give certificate if it didn't previously exists, 
        /// getting access to the certificate object if it did. Saving a .cer file of the
        /// certificate on the desktop if it is newly created, providing instructions for uploading.
        /// Setting updata for configure ation stepand destoying unused data from authentication.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextHandler(object sender, EventArgs e)
        {
            SID = authorisationView.GetSelectedSubscription();
            Cert = authorisationView.GetSelectedCertificate();
            // if there is a refference t0 the wizard
            if (wizard != null)
            {
                //this method will write certificate data and SID's into
                //the subscriptions file.
                //before this we need to generate the certificate object.
                //save it locally
                //provide access to a .cer file on the desktop
                SID = authorisationView.GetSelectedSubscription();
                Cert = authorisationView.GetSelectedCertificate();
                
                //needs moving out of this class i think
                CertificateManager certManager = new CertificateManager();
                CertificateMaker certMaker = new CertificateMaker();
                X509Certificate2 certificate = new X509Certificate2();
                // if the subscription already exists
                if (wizard.HasSubscription(SID, Cert))
                {
                    //get the thumb print
                    List<Subscription> subscriptions = wizard.GetSubscriptions();
                    string thumbprint = GetThumbPrint(SID, Cert, subscriptions);
                    if (certManager.CertificateExistsLocally(thumbprint))
                        certificate = certManager.GetLocalCertificate(thumbprint);
                }
                else
                {
                    certificate = certMaker.MakeCertificate(Cert, certificate);
                    // indicate certificate is being made to user
                }

                wizard.AddSubscription(SID, Cert, certificate.Thumbprint);

                ((IViewError)authorisationView).NotifyOfError(new Exception(certificate.Thumbprint));
            }
            // need to deal with certificates aswell;
        }

        // not the responcibility of this class, should be moved somewhere that deals with subscriptions
        private string GetThumbPrint(string SID, string Cert, List<Subscription> subscriptions)
        {
            string returnString = "";
            foreach (Subscription subscription in subscriptions)
            {
                if (subscription.SID == SID && subscription.CertName == Cert)
                {
                    returnString = subscription.ThumbPrint;
                }
            }
            return returnString;
        }

        public void NewSubscriptionHandler(object data)
        {
            try
            {
                wizard.addSID((string)data);
                authorisationView.DisplaySubsriptions(wizard.GetSIDList());
            }
            catch (InvalidSIDException exc)
            {
                ((IViewError)authorisationView).NotifyOfError(exc);
            }
        }

        public void NewCertificateHandler(object data)
        {
            wizard.AddCertificate((String)data);
            authorisationView.DisplayCertificates(wizard.GetCertList());
        }

        /// <summary>
        /// This method retrieves the index of a given SID and checks that a certificate exists at that index in
        /// the list of certificate names. If it does, then the index is returned, otherwise a -1 is returned.
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public int ChangedSIDSelectionHandler(int index)
        {
            int returnIndex = -1;
            if ((index  < wizard.GetSubscriptions().Count))
                returnIndex = index;
            return returnIndex;
        }

    }
}
