using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Pluralsight.Crypto;
using GWydiR.Containers;

namespace GWydiR.Interfaces.UtilityInterfaces
{
    public interface ICertificateMaker
    {
        PkcsCertificate MakeCertificate(string principalName, string password); //to be added too.
    }
}
