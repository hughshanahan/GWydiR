using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Utilities;
using NUnit.Framework;
using Rhino.Mocks;
using System.Security.Cryptography.X509Certificates;
using GWydiR.Wrappers;

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

        class OverridenManager_1 : CertificateManager
        {
            private X509Certificate2Collection mockCollection;

            public OverridenManager_1(X509Certificate2Collection mockCollection)
            {
                this.mockCollection = mockCollection;
            }

            protected override X509Certificate2Collection getCollection(CertificateStore store)
            {
                return mockCollection;
            }
        }

        [Test]
        public void FindCertExistTest()
        {
            //Arrange
            X509Certificate2 testCert = MockRepository.GenerateStub<X509Certificate2>();
            testCert.Stub(x => x.Thumbprint).Return("athumbprint");
            X509Certificate2 testCert2 = MockRepository.GenerateStub<X509Certificate2>();
            testCert2.Stub(x => x.Thumbprint).Return("wrongThumbPrint");
            X509Certificate2 testCert3 = MockRepository.GenerateStub<X509Certificate2>();
            testCert3.Stub(x => x.Thumbprint).Return("WrongThumbPrint");

            X509Certificate2Collection mockCollection = new X509Certificate2Collection() {testCert,testCert2,testCert3}; 
            certManager = new OverridenManager_1(mockCollection);

            //Act
            bool exists = certManager.CertificateExistsLocally(testCert.Thumbprint);

            //Assert
            Assert.IsTrue(exists);
        }

        class OverRidenManager_2 : CertificateManager
        {
            private CertificateStore mockStore;

            public OverRidenManager_2(CertificateStore mockStore)
            {
                this.mockStore = mockStore;
            }

            protected override CertificateStore makeStore()
            {
                return mockStore;
            }
            
        }

        [Test]
        public void AddCertToStoreTest()
        {
            //Arrange
            CertificateStore mockStore = MockRepository.GenerateStub<CertificateStore>();
            mockStore.Expect(x => x.Add(Arg<X509Certificate2>.Is.Anything));

            certManager = new OverRidenManager_2(mockStore);

            //Act
            certManager.SaveCertificateLocally(new X509Certificate2());

            //Assert
            mockStore.VerifyAllExpectations();
        }

        [Test]
        public void RemoveCertFromStoreTest()
        {
            //Arrange
            CertificateStore mockStore = MockRepository.GenerateStub<CertificateStore>();
            mockStore.Expect(x => x.Remove(Arg<X509Certificate2>.Is.Anything));

            certManager = new OverRidenManager_2(mockStore);

            //Act
            certManager.RemoveCertificateLocally(new X509Certificate2());

            //Assert
            mockStore.VerifyAllExpectations();
        }

        [Test]
        public void GetCertificateFromStoreTest()
        {
            //Arrange
            string testPrint = "aprint";
            CertificateStore mockStore = MockRepository.GenerateStub<CertificateStore>();
            X509Certificate2 testCert = MockRepository.GenerateStub<X509Certificate2>();
            mockStore.Expect(x => x.GetCertificates()).Return(new X509Certificate2Collection() {testCert});
            testCert.Stub(X => X.Thumbprint).Return(testPrint);

            certManager = new OverRidenManager_2(mockStore);

            //Act
            X509Certificate2 returnCert = certManager.GetLocalCertificate(testPrint);

            //Assert
            mockStore.VerifyAllExpectations();
            //Testing That the objects are infact the same object, not simply equal.
            Assert.IsTrue(testCert == returnCert);
            
        }
    }
}
