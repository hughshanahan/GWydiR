using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using GWydiR.Wrappers;
using GWydiR.Interfaces.UtilityInterfaces;

namespace GWydiR.Utilities
{
    /// <summary>
    /// 
    /// </summary>
    public class CertificateManager : ICertificateManager
    {

        /// <summary>
        /// Method to open the local certificate store and check to see if a 
        /// certificate with the appropriate thumb print exists
        /// </summary>
        /// <param name="testThumbPrint"></param>
        /// <returns></returns>
        public virtual bool CertificateExistsLocally(string testThumbPrint)
        {
            //open store
            //should use a wrapper class as the store object is sealed.
            CertificateStore store = makeCurrentUserStore();
            store.Open(OpenFlags.ReadOnly);

            // retrieve collection for certificates from store
            X509Certificate2Collection collection = getCollection(store);

            store.Close();

            bool returnValue = false;

            // for each certificate in the store
            foreach (X509Certificate2 cert in collection)
            {
                // return true if the thumb print given and the thumb print found match
                if (cert.Thumbprint.Equals(testThumbPrint))
                {
                    returnValue = true;
                }
            }

            return returnValue;
         }

        /// <summary>
        /// Method to allow data injection for testing
        /// </summary>
        /// <returns></returns>
        protected virtual CertificateStore makeCurrentUserStore()
        {
            return new CertificateStore(new X509Store(StoreName.My, StoreLocation.CurrentUser));
        }

        protected virtual CertificateStore makeLocalMachineStore()
        {
            return new CertificateStore(new X509Store(StoreName.My, StoreLocation.LocalMachine));
        }

        protected virtual X509Certificate2Collection getCollection(CertificateStore store)
        {
            return store.GetCertificates();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testCert"></param>
        public void SaveCertificateLocally(System.Security.Cryptography.X509Certificates.X509Certificate2 testCert)
        {
            CertificateStore store = makeCurrentUserStore();
            store.Open(OpenFlags.ReadWrite);
            store.Add(testCert);
            store.Close();
            store = makeLocalMachineStore();
            store.Open(OpenFlags.ReadWrite);
            store.Add(testCert);
            store.Close();
            
        }

        public virtual X509Certificate2 GetLocalCertificate(string ThumbPrint)
        {
            X509Certificate2 returnCert = null;
            CertificateStore store = makeCurrentUserStore();
            X509Certificate2Collection collection = store.GetCertificates();
            foreach (X509Certificate2 cert in collection)
            {
                if(cert.Thumbprint.Equals(ThumbPrint))
                {
                    returnCert = cert;
                }
            }

            return returnCert;

        }


        public void RemoveCertificateLocally(X509Certificate2 certificate)
        {
            CertificateStore store = makeCurrentUserStore();
            store.Open(OpenFlags.ReadOnly);
            store.Remove(certificate);
            store.Close();
            store = makeLocalMachineStore();
            store.Open(OpenFlags.ReadOnly);
            store.Remove(certificate);
            store.Close();
        }
    }
}
