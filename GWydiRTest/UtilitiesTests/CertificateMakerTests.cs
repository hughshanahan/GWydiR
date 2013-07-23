using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Utilities;
using NUnit.Framework;
using Rhino.Mocks;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Pluralsight.Crypto;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;

namespace GWydiRTest.UtilitiesTests
{
    class CertificateMakerTests
    {
        CertificateMaker certMaker;

        [SetUp]
        public void setup()
        {
            certMaker = new CertificateMaker();
        }

        /// <summary>
        /// Object to allow all virtual methods to be over riden so as to inject mock objects into the real code.
        /// </summary>
        public class OverRiddenCertificatemaker_1 : CertificateMaker
        {

            object mockX509Certificate2;
            object mockX509Certificate;
            object mockX509Generator;
            object mockKeyGenParams;
            object mockRsaKeyPairGen;

            public OverRiddenCertificatemaker_1(object mockCert2, object mockCert, object mockCertGen, object mockKeyParams, object mockKeyGen) : base() 
            {
                mockX509Certificate2 = mockCert2;
                mockX509Certificate = mockCert;
                mockX509Generator = mockCertGen;
                mockKeyGenParams = mockKeyParams;
                mockRsaKeyPairGen = mockKeyGen;
            }

            protected override X509Certificate2 
                makeDotNetX509Certificate2(
                System.Security.Cryptography.X509Certificates.X509Certificate 
                dotNetCertificate)
            {
                return (X509Certificate2)mockX509Certificate2;
            }

            protected override System.Security.Cryptography.
                X509Certificates.X509Certificate 
                makeDotNetX509Certificate(
                Org.BouncyCastle.X509.X509Certificate 
                intermediateCertificate)
            {
                return (System.Security.Cryptography.X509Certificates.X509Certificate)mockX509Certificate;
            }

            protected override X509V3CertificateGenerator makeCertificateGenerator()
            {
                return (X509V3CertificateGenerator)mockX509Generator;
            }

            protected override KeyGenerationParameters makeKeyGenParameters()
            {
                return (KeyGenerationParameters)mockKeyGenParams;
            }

            protected override RsaKeyPairGenerator makeRsaGenerator()
            {
                return (RsaKeyPairGenerator)mockRsaKeyPairGen;
            }
        }

        /// <summary>
        /// Currently must be resigned to intergration testing.
        /// </summary>
        [Test]
        //public void MakeCertificateTest()
        //{
        //    //Arrange
        //    string testNam = "name";
        //    X509Certificate2 mockCert = MockRepository.GenerateStub<X509Certificate2>();
        //    X509Certificate2 mockCert2 = MockRepository.GenerateStub<X509Certificate2>();

        //    System.Security.Cryptography.X509Certificates.X509Certificate 
        //        mockCert1 = MockRepository.GenerateStub<
        //        System.Security.Cryptography.X509Certificates.X509Certificate>();

        //    Org.BouncyCastle.X509.X509Certificate mockBouncyCert = 
        //        MockRepository.GenerateStub<Org.BouncyCastle.X509.X509Certificate>();

        //    X509V3CertificateGenerator mockGen = MockRepository.GenerateStub<X509V3CertificateGenerator>();
        //    KeyGenerationParameters mockParams = MockRepository.GenerateStub<KeyGenerationParameters>(new SecureRandom(new CryptoApiRandomGenerator()),2048);
        //    RsaKeyPairGenerator mockKeyGen = MockRepository.GenerateStub<RsaKeyPairGenerator>();

        //    mockKeyGen.Expect(x => x.Init(mockParams));

        //    AsymmetricCipherKeyPair mockKeyPair = MockRepository.GenerateStub<AsymmetricCipherKeyPair>(new AsymmetricKeyParameter(false), new AsymmetricKeyParameter(true));
        //    mockKeyPair.Stub(x => x.Public).Return(new AsymmetricKeyParameter(false));
        //    mockKeyPair.Stub(x => x.Private).Return(new AsymmetricKeyParameter(true));

        //    mockKeyGen.Expect(x => x.GenerateKeyPair()).Return(mockKeyPair);

        //    mockGen.Expect(x => x.SetSerialNumber(Arg<BigInteger>.Is.Anything));
        //    mockGen.Expect(x => x.SetIssuerDN(Arg<X509Name>.Is.Anything));
        //    mockGen.Expect(x => x.SetNotBefore(Arg<DateTime>.Is.Anything));
        //    mockGen.Expect(x => x.SetNotAfter(Arg<DateTime>.Is.Anything));
        //    mockGen.Expect(x => x.SetSubjectDN(new Org.BouncyCastle.Asn1.X509.X509Name("cn="+testNam)));
        //    mockGen.Expect(X => X.SetPublicKey(Arg<AsymmetricKeyParameter>.Is.Anything));
        //    mockGen.Expect(x => x.SetSignatureAlgorithm("SHA1WithRSA"));

        //    mockGen.Expect(x => x.Generate(Arg<AsymmetricKeyParameter>.Is.Anything)).Return(mockBouncyCert);

        //    certMaker = new OverRiddenCertificatemaker_1(mockCert2, mockCert1, mockGen, mockParams, mockKeyGen);


        //    //Act
        //    X509Certificate2 cert = certMaker.MakeCertificate(testNam,mockCert);

        //    //Assert
        //    mockKeyGen.VerifyAllExpectations();
        //    mockParams.VerifyAllExpectations();
        //    mockGen.VerifyAllExpectations();
        //    mockKeyPair.VerifyAllExpectations();
        //}


        /// <summary>
        /// BAD TEST, DOES NOT CONFORM TO UNIT STANDARDS
        /// </summary>
        [Test]
        public void MakeCertificateWithName()
        {
            string testName = "name";
            X509Certificate2 mockCert = MockRepository.GenerateStub<X509Certificate2>();

            X509Certificate2 cert = certMaker.MakeCertificate(testName, mockCert);
            string testResult = "CN=" + testName;
            Assert.IsTrue(cert.SubjectName.Name.Equals(testResult));
        }
        
    }
}
