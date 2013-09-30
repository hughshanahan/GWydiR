using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace GWydiR.Interfaces.UtilityInterfaces
{
    /// <summary>
    /// Defines the public responicibilites of the Certificate Manager Class
    /// </summary>
    public interface ICertificateManager
    {
        /// <summary>
        /// This method should check to see if a certificate exists locally with a give thumb print
        /// </summary>
        /// <param name="ThumbPrint"></param>
        /// <returns></returns>
        bool CertificateExistsLocally(string ThumbPrint);

        /// <summary>
        /// This method should store a given certificate locally in a certificate store
        /// </summary>
        /// <param name="certificate"></param>
        void SaveCertificateLocally(X509Certificate2 certificate);

        /// <summary>
        /// This emthod should remove a certificate from a local store bases o a given thumbprint
        /// </summary>
        /// <param name="ThumbPrint"></param>
        void RemoveCertificateLocally(X509Certificate2 certificate);

        /// <summary>
        /// This method should return the object representation of a X509 certificate stored locally,
        /// given a thum print to find it.
        /// </summary>
        /// <param name="ThumbPrint"></param>
        /// <returns></returns>
        X509Certificate2 GetLocalCertificate(string ThumbPrint);


    }
}
