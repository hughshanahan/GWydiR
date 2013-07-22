using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Utilities;
using NUnit.Framework;
using Rhino.Mocks;
using System.Security.Cryptography.X509Certificates;
using Pluralsight.Crypto;

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

        public class OverRiddenCertificatemaker_1 : CertificateMaker
        {
            object mock;
            public OverRiddenCertificatemaker_1(object mock) : base() 
            {
                this.mock = mock;
            }

            protected override CryptContext makeContext()
            {
                return (CryptContext)mock;
            }
        }

        /// <summary>
        /// Needs more indepth testing
        /// </summary>
        [Test]
        public void MakeCertificateTest()
        {
            CryptContext context = MockRepository.GenerateStub<CryptContext>();
            context.Expect(x => x.Open());
            certMaker = new OverRiddenCertificatemaker_1(context);
            X509Certificate2 cert = certMaker.MakeCertificate(false, 4096, new X500DistinguishedName("cn=someName"), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));
            context.VerifyAllExpectations();
            Assert.IsTrue(cert is X509Certificate2);
        }
    }
}
