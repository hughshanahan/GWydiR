using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Pluralsight.Crypto;

namespace GWydiR.Interfaces.UtilityInterfaces
{
    public interface ICertificateMaker
    {
        X509Certificate2 MakeCertificate(string principalName, X509Certificate2 cert); //to be added too.
    }
}
