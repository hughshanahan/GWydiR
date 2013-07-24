using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GWydiR.Interfaces.ViewInterfaces
{
    /// <summary>
    /// An interface to define the visible reposncibilities of the authorisation tab of the GWydiR wizard.
    /// </summary>
    public interface IAuthorisationView
    {
        /// <summary>
        /// Method to populate a combo box with a list of subscription id's
        /// </summary>
        void DisplaySubsriptions(List<string> subscriptions);

        /// <summary>
        /// Method to populate a combo box with a list of certificate names
        /// </summary>
        void DisplayCertificates(List<string> certificates);

        /// <summary>
        /// Mthod to return a subscription Id selected by a user
        /// </summary>
        string GetSelectedSubscription();

        /// <summary>
        /// Method to return a certificate selected by a user
        /// </summary>
        string GetSelectedCertificate();

        /// <summary>
        /// Method to allow a user to interact with a button
        /// </summary>
        void EnableNext();

        /// <summary>
        /// method to prevent a user interating with a button
        /// </summary>
        void DisableNext();

        /// <summary>
        /// method to allow a user to interact with a button
        /// </summary>
        void EnableCreate();

        /// <summary>
        /// method to rpevent a user interacting with a button
        /// </summary>
        void DisableCreate();

        /// <summary>
        /// method to register an event handling delegate with a new subscritions event
        /// </summary>
        /// <param name="handler"></param>
        void RegisterNewSubscription(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler);

        /// <summary>
        /// Method to remove a delegate from the list of delegates to call when the event occurs
        /// </summary>
        /// <param name="handler"></param>
        void DeRegisterNewSubscription(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler);

        /// <summary>
        /// Method to register an event handler with the new certifiacte event
        /// </summary>
        /// <param name="handler"></param>
        void RegisterNewCertificate(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler);

        /// <summary>
        /// Method to remove the delegate from the new certificate event.
        /// </summary>
        /// <param name="handler"></param>
        void DeRegisterNewCerticate(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler);

        /// <summary>
        /// Method to add delegate to respond to change in selected SID event
        /// </summary>
        /// <param name="handler"></param>
        void RegisterChangedSIDSelection(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler);

        /// <summary>
        /// Method to remove delegate that responds to changed selected SID event
        /// </summary>
        /// <param name="handler"></param>
        void DeRegisterChangedSIDSelected(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler);

        /// <summary>
        /// Method to add delegate
        /// </summary>
        /// <param name="handler"></param>
        void RegisterChangedCertificateSelection(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler);

        /// <summary>
        /// method to remove delegate
        /// </summary>
        /// <param name="handler"></param>
        void DeRegisterChangedCertificateSelection(GWydiR.Handlers.GWydiRHandlers.ChangedSelectionHandler handler);

        /// <summary>
        /// Method to add delegate that responds to changes to the selection made by  user
        /// of the Certificat combo box
        /// </summary>
        /// <param name="handler"></param>
        void RegisterCreate(EventHandler handler);

        /// <summary>
        /// Method to remove delegates that respond to changes to the selected value in the
        /// certificate combo box
        /// </summary>
        /// <param name="handler"></param>
        void DeRegisterCreate(EventHandler handler);

    }
}
