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

        const int TabNumber = 0;

        IAuthorisationView authorisationView;

        IWizard wizard;

        public string SID { get; set; }

        public string Cert { get; set; }

        public string Password { get; set; }

        public X509Certificate2 certificateSTS { get; set; }
        public X509Certificate2 certificateManagement { get; set; }

        public AuthorisationModel()
        {
        }

        public AuthorisationModel(IAuthorisationView authorisationView)
        {
            this.authorisationView = authorisationView;
            // register eventhandlers with UI
            ITabNavigation genericTabPanel = CastITabNavigation(authorisationView);
            genericTabPanel.RegisterNext(NextHandler,TabNumber);
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
            //register for changes to selection of the certificates on the ui.
            authorisationView.RegisterChangedCertificateSelection(ChangedCertificateSelectionHandler);
            //register for create button click
            authorisationView.RegisterCreate(CreateButtonHandler);
            //register password changed handler
            authorisationView.RegisterCertificatePasswordChanged(CertficatePasswordChanged);

            ITabNavigation genericPanel = CastITabNavigation(authorisationView);
            genericPanel.RegisterNext(NextHandler,TabNumber);

            // set up display on the view if there are SID's to show
            if (wizard.GetSIDList() != null && wizard.GetCertList() != null)
            {
                authorisationView.DisplaySubsriptions(wizard.GetSIDList());
                authorisationView.DisplayCertificates(wizard.GetCertList());
                authorisationView.DisableCreate();
                authorisationView.EnableNext();
            }

                
        }

        // similar to a factory method, this allows a mocked tabNavigationObject to be injected while testing.
        public virtual ITabNavigation CastITabNavigation(IAuthorisationView authorisationView)
        {
            return (ITabNavigation)authorisationView;
        }

        /// <summary>
        /// This method should deal with navigating forward from the Authentication tab
        /// This should involve, generating a give certificate if it didn't previously exists, 
        /// getting access to the certificate object if it did. Saving a .cer file of the
        /// certificate on the desktop if it is newly created, providing instructions for uploading.
        /// Setting updata for configure ation step and destoying unused data from authentication.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NextHandler(object sender, EventArgs e)
        {
            SID = authorisationView.GetSelectedSubscription();
            Cert = authorisationView.GetSelectedCertificate();
            if (wizard.HasSubscription(SID, Cert))
            {
                CertificateManager certManager = makeCertificateManager();
                //get the thumb print
                string thumbprint = wizard.GetSTSThumbPrint(SID, Cert);
                //if the valid subscription's certificate exists, load it from the local store
                if (certManager.CertificateExistsLocally(thumbprint))
                {
                    certificateSTS = certManager.GetLocalCertificate(thumbprint);
                    ITabNavigation navView = CastITabNavigation(authorisationView);
                    navView.ChangeTab(1);
                }
                else //if not then tell the user there has been an error
                // will also need to deal with this (make new certificate if user agrees etc)
                {
                    (castToIViewError(authorisationView)).NotifyOfError(new Exception("Certificate does not exist Locally"));
                }

            }
            else
            {
                try
                {
                    wizard.AddSubscription(SID, Cert, certificateSTS.Thumbprint,certificateManagement.Thumbprint);
                    wizard.SaveSubscriptions();
                }
                catch (NullReferenceException exc)
                {
                    // what should I do with a null refference?
                }
            }
        }

        /// <summary>
        /// This method exists to allow injection of sata during testing
        /// </summary>
        /// <param name="authorisationView"></param>
        /// <returns></returns>
        protected virtual IViewError castToIViewError(IAuthorisationView authorisationView)
        {
            return (IViewError)authorisationView;
        }

        protected virtual CertificateManager makeCertificateManager()
        {
            return new CertificateManager();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void NewSubscriptionHandler(object data)
        {
            try
            {
                wizard.addSID((string)data);
                SID = (string)data;
                authorisationView.DisplaySubsriptions(wizard.GetSIDList());
            }
            catch (InvalidSIDException exc)
            {
                (castToIViewError(authorisationView)).NotifyOfError(exc);
            }
        }

        /// <summary>
        /// Handler for new certificate event, a string array is passed the first element of which is the certificate name
        /// and the second is the password to be used to secure the certificate.
        /// </summary>
        /// <param name="data">Data will be an array of strings</param>
        public void NewCertificateHandler(object data)
        {
            string[] certData = (string[])data;
            Cert = certData[0];
            Password = certData[1];
            wizard.AddCertificate(Cert);
            authorisationView.DisplayCertificates(wizard.GetCertList());

            // UI actions
            authorisationView.EnableCreate();
            authorisationView.DisableNext();
        }

        /// <summary>
        /// This method retrieves the index of a given SID and checks that a certificate exists at that index in
        /// the list of certificate names. If it does, then the index is returned, otherwise a -1 is returned.
        /// </summary>
        /// <param name="SID"></param>
        /// <returns></returns>
        public int ChangedSIDSelectionHandler(int index)
        {
            //set selected value in model
            SID = wizard.GetSIDList()[index];
            wizard.ChosenSID = SID;

            int returnIndex = -1;
            if ((index  < wizard.GetSubscriptions().Count))
                returnIndex = index;
            return returnIndex;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int ChangedCertificateSelectionHandler(int index)
        {
            //set selected value in model
            Cert = wizard.GetCertList()[index];
            wizard.ChosenCertificate = Cert;

            //if the current SID and cert selected are a valid subscription
            if (wizard.HasSubscription(SID, Cert))
            {
                authorisationView.EnableNext();
                authorisationView.DisableCreate();
            }
            else if (SID != null) // if there is an valid SID that isn't part of a subscription
            {
                authorisationView.EnableCreate();
                authorisationView.DisableNext();
            }
            else // else nothing can be created and we can't move forward so prevent it from occuring
            {
                authorisationView.DisableCreate();
                authorisationView.DisableNext();
            }


            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void CreateButtonHandler(object Sender, EventArgs e)
        {
            //this method will write certificate data and SID's into
            //the subscriptions file.
            //before this we need to generate the certificate object.
            //save it locally
            //provide access to a .cer file on the desktop

            CertificateMaker certMaker = makeCertificateMaker();
            certificateSTS = new X509Certificate2();
            // if the subscription already exists
            
            //Write Certificate as file to desktop
            FileWriter writer = makeFileWriter();

            //needs to be rethought out and re designed
            #region certificate creation 

            // make STS certificate, save and export private key
            PkcsCertificate stsCert = certMaker.MakeCertificate(Cert, Password);
            certificateSTS = stsCert.GetX509Certificate2(X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
            // wrte sts certificate to desktop
            writer.Write(makeCertificatePath(Cert + "STS", ".pfx"), stsCert.GetCertificateStream().ToArray());

            PkcsCertificate managementCert = certMaker.MakeCertificate(Cert, Password);
            certificateManagement = managementCert.GetX509Certificate2(X509KeyStorageFlags.DefaultKeySet);
            writer.Write(makeCertificatePath(Cert + "Management", ".cer"), certificateManagement.GetRawCertData());
            certificateManagement = managementCert.GetX509Certificate2(X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
            

            // store both private key certificates locally in both user and loca machine stores
            // to thats general worker can have access to them
            //X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            //store.Open(OpenFlags.ReadOnly);
            //store.Add(certificateSTS);
            //store.Add(certificateManagement);
            //store.Close();

            //store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            //store.Open(OpenFlags.ReadOnly);
            //store.Add(certificateSTS);
            //store.Add(certificateManagement);
            //store.Close();

            #endregion

            // make management certificate save private key, export public cert

            
            // indicate certificate is being made to user

            authorisationView.DisableCreate();
            authorisationView.EnableNext();           

        }

        private string makeCertificatePath(string name,string extension)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            return path + "\\" + name + extension;
        }

        protected virtual CertificateMaker makeCertificateMaker()
        {
            return new CertificateMaker();
        }

        protected virtual FileWriter makeFileWriter()
        {
            return new FileWriter();
        }

        public void CertficatePasswordChanged(object sender, EventArgs args)
        {
            Password = authorisationView.GetPassword();
        }



    }
}
