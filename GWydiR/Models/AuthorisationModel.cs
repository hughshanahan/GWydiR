using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;
using GWydiR.Exceptions;

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

        // this needs testing....
        public void NextHandler(object sender, EventArgs e)
        {
            SID = authorisationView.GetSelectedSubscription();
            Cert = authorisationView.GetSelectedCertificate();
            // if there is a refference t0 the wizard
            if (wizard != null)
            {
                try
                {
                    wizard.addSID(SID);
                }
                catch (InvalidSIDException exc)
                {
                    ((IViewError)authorisationView).NotifyOfError(exc);
                }
            }
            // need to deal with certificates aswell;
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
    }
}
