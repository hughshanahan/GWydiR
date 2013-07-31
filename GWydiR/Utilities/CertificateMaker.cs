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
using Org.BouncyCastle.Pkcs;
using System.IO;
using GWydiR.Containers;


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
        public virtual PkcsCertificate MakeCertificate(string principalName, string password)
        {

            //build a key generator
            RsaKeyPairGenerator keyGen = makeRsaGenerator();

            //initialise the keygenerator to produce a large enough key for Azure
            //this may need breaking up for debugging
            keyGen.Init(makeKeyGenParameters());

            //must find out what var this is for type safety
            AsymmetricCipherKeyPair keyPair = keyGen.GenerateKeyPair();

            //instantiate the generator
            X509V3CertificateGenerator certGen = makeCertificateGenerator();

            //set the values required by the generator to generate a certificate
            //this may need adapting to make this more secure
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

            // make a .pfx from this certificate
            Pkcs12Store store = makePkcsStore();

            // In a pkcs12 (.pfx file) we associate the private key with a certificate via the friendly name
            string friendlyName = intermediateCertificate.SubjectDN.ToString();

            // add the certificate to the store with the friendly name as the entry label
            X509CertificateEntry certEntry = new X509CertificateEntry(intermediateCertificate);
            store.SetCertificateEntry(friendlyName, certEntry);

            // add the private key with the same label/alias and an association with our intermediate certificate
            // it takes an array of entries with which to associate as all of those entries could be certificates 
            // signed with that private key.
            store.SetKeyEntry(friendlyName, new AsymmetricKeyEntry(keyPair.Private), new[] { certEntry });

            
            //make new certificate object
            PkcsCertificate certificate = new PkcsCertificate(password, keyPair, intermediateCertificate, store);

            return certificate;
        }

        protected virtual Pkcs12Store makePkcsStore()
        {
            return new Pkcs12Store();
        }

        protected virtual MemoryStream makeMemeoryStream()
        {
            return new MemoryStream();
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
