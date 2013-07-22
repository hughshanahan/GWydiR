﻿using System;
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
        /// method to register an event handling delegate with a new subscritions event
        /// </summary>
        /// <param name="handler"></param>
        void RegisterNewSubscription(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler);

        /// <summary>
        /// Method to remove a delegate from the list of delegates to call when the event occurs
        /// </summary>
        /// <param name="handler"></param>
        void DeRegisterNewSubscription(GWydiR.Handlers.GWydiRHandlers.NewDataEventHandler handler);

    }
}