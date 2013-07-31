using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Containers;

namespace GWydiR.Interfaces.ModelInterfaces
{
    /// <summary>
    /// An interface to describe the public methods of the Wizard class, used mainly to allow mocking during testing.
    /// </summary>
    public interface IWizard
    {
        List<string> GetSIDList();
        List<string> GetCertList();
         FileReader makeReader();
         FileWriter makeWriter();
         bool hasSID(string SID);
         bool isValidSID(string SID);
         void addSID(string SID);
         void AddCertificate(string certName);
         void AddSubscription(string SID, string certificateName, string STSCertificateThumbprint, string ManagementCertificateThumbprint);
         List<Subscription> GetSubscriptions();
         bool HasSubscription(string SID, string certificateName);
         string GetSTSThumbPrint(string SID, string certificateName);
         void SaveSubscriptions();
    }
}
