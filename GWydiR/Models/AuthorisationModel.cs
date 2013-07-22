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
            this.authorisationView = authorisationView;
            authorisationView.RegisterNewSubscription(NewSubscriptionHandler);

            ITabNavigation genericPanel = CastITabNavigation(authorisationView);
            genericPanel.RegisterNext(NextHandler);

            // set up display on the view if there are SID's to show
            if (wizard.SIDList != null)
                authorisationView.DisplaySubsriptions(wizard.SIDList);
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
                authorisationView.DisplaySubsriptions(wizard.SIDList);
            }
            catch (InvalidSIDException exc)
            {
                ((IViewError)authorisationView).NotifyOfError(exc);
            }
        }
    }
}
