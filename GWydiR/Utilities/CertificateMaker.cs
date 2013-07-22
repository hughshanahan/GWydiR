using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.UtilityInterfaces;
using System.Security.Cryptography.X509Certificates;
using Pluralsight.Crypto;

namespace GWydiR.Utilities
{
    public class CertificateMaker : ICertificateMaker
    {



        /// <summary>
        /// Factory method to allow for injection of code for testing.
        /// </summary>
        /// <returns></returns>
        protected virtual CryptContext makeContext()
        {
            return new CryptContext();
        }

        public X509Certificate2 MakeCertificate(bool isPrivateKeyExportable, int keyBitLength, X500DistinguishedName name, DateTime validFrom, DateTime validTill)
        {
            X509Certificate2 returnCertificate = new X509Certificate2();

            CryptContext context = makeContext();
            context.Open();
            returnCertificate = context.CreateSelfSignedCertificate(
                new SelfSignedCertProperties()
                {
                    IsPrivateKeyExportable = isPrivateKeyExportable,
                    KeyBitLength = keyBitLength,
                    Name = name,
                    ValidFrom = validFrom,
                    ValidTo = validTill,
                });
            context.Dispose();
            return returnCertificate;
        }
    }
}
