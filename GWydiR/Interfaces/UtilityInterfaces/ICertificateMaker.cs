using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using Pluralsight.Crypto;

namespace GWydiR.Interfaces.UtilityInterfaces
{
    public interface ICertificateMaker
    {
        X509Certificate2 MakeCertificate(bool isPrivateKeyExportable,int keyBitLength, X500DistinguishedName name, DateTime validFrom, DateTime validTill); //to be added too.
    }
}
