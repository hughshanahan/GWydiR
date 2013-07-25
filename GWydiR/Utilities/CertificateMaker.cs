using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Interfaces.UtilityInterfaces;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Crypto.Generators;
using System.Collections;
using Org.BouncyCastle.Math;


namespace GWydiR.Utilities
{
    public class CertificateMaker : ICertificateMaker
    {
        /// <summary>
        /// This method takes in a principal name and converts it into a distinguished name for applying to the certificate, also an RSA key pair which is
        /// used to encypt the certificate. This certificate is then returned to the caller.
        /// </summary>
        /// <param name="principalName"></param>
        /// <param name="rsaKeyPair"></param>
        /// <returns></returns>
        public virtual X509Certificate2 MakeCertificate(string principalName, X509Certificate2 cert)
        {
            //build a key generator
            RsaKeyPairGenerator keyGen = makeRsaGenerator();

            //initialise the keygenerator to produce a large enough key for Azure
            //this may need breaking up for debugging
            keyGen.Init(makeKeyGenParameters());

            //must find out what var this is for type safety
            var keyPair = keyGen.GenerateKeyPair();

            //instantiate the generator
            X509V3CertificateGenerator certGen = makeCertificateGenerator();

            //set the values required by the generator to generate a certificate
            certGen.SetSerialNumber(BigInteger.One);
            //magic value
            certGen.SetIssuerDN(new Org.BouncyCastle.Asn1.X509.X509Name("OU=GWydiR"));
            certGen.SetNotBefore(DateTime.Today.AddDays(-1));
            certGen.SetNotAfter(DateTime.Today.AddMonths(3));
            certGen.SetSubjectDN(new Org.BouncyCastle.Asn1.X509.X509Name("cn="+principalName));
            certGen.SetPublicKey(keyPair.Public);
            certGen.SetSignatureAlgorithm("SHA1WithRSA");

            //make a bouncy castle specific x509 certificate to transform into a .net certificate
            Org.BouncyCastle.X509.X509Certificate intermediateCertificate = certGen.Generate(keyPair.Private);
            
            //transform the bouncy castle certificate into a .net certificate
            System.Security.Cryptography.X509Certificates.X509Certificate dotNetCertificate = makeDotNetX509Certificate(intermediateCertificate);

            //transform .net certificate helper object into certificate2
            X509Certificate2 returnCertificate = makeDotNetX509Certificate2(dotNetCertificate);

            return returnCertificate;
            
        }

        /// <summary>
        /// Factory method to provide  means of data injection for testing
        /// </summary>
        /// <param name="dotNetCertificate"></param>
        /// <returns></returns>
        protected virtual X509Certificate2 makeDotNetX509Certificate2(System.Security.Cryptography.X509Certificates.X509Certificate dotNetCertificate)
        {
            return new X509Certificate2(dotNetCertificate);
        }

        /// <summary>
        /// Factory method to provide a means of data injection for testing
        /// </summary>
        /// <param name="intermediateCertificate"></param>
        /// <returns></returns>
        protected virtual System.Security.Cryptography.X509Certificates.X509Certificate makeDotNetX509Certificate(Org.BouncyCastle.X509.X509Certificate intermediateCertificate)
        {
            return DotNetUtilities.ToX509Certificate(intermediateCertificate.CertificateStructure);
        }

        /// <summary>
        /// Factory method to provide a means of data injection for testing
        /// </summary>
        /// <returns></returns>
        protected virtual X509V3CertificateGenerator makeCertificateGenerator()
        {
            return new X509V3CertificateGenerator();
        }

        /// <summary>
        /// Factory method to provide a means of data injection for testing
        /// </summary>
        /// <returns></returns>
        protected virtual KeyGenerationParameters makeKeyGenParameters()
        {
            return new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), 2048);
        }

        /// <summary>
        /// Factory method to provide a means of data injection for testing
        /// </summary>
        /// <returns></returns>
        protected virtual RsaKeyPairGenerator makeRsaGenerator()
        {
            return new RsaKeyPairGenerator();
        }

    }
}
