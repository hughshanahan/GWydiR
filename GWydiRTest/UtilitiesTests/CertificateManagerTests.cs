using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Utilities;
using NUnit.Framework;
using Rhino.Mocks;
using System.Security.Cryptography.X509Certificates;

namespace GWydiRTest.UtilitiesTests
{
    class CertificateManagerTests
    {
        CertificateManager certManager;

        [SetUp]
        public void setup()
        {
            certManager = new CertificateManager();
        }

        [Test]
        public void DoesCertificateExistTest()
        {
            string testThumbPrint = "";
            bool exists = certManager.CertificateExistsLocally(testThumbPrint);
            Assert.IsFalse(exists);
        }

        [Test]
        public void AddCertExistTest()
        {
            X509Certificate2 testCert = (new CertificateMaker()).MakeCertificate(false, 128, new X500DistinguishedName("cn=testName"), DateTime.Today, DateTime.Today.AddDays(1));
            certManager.SaveCertificateLocally(testCert);
            bool exists = certManager.CertificateExistsLocally(testCert.Thumbprint);
            Assert.IsTrue(exists);

        }

    }
}
