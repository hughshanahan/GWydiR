using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;
using NUnit.Framework;
using Rhino.Mocks;
using GWydiR.Utilities;
using System.Security.Cryptography.X509Certificates;
using GWydiR.Exceptions;
using GWydiR.Containers;

namespace GWydiRTest.ModelTests
{
    class AuthorisationModelTest
    {

        AuthorisationModel model;

        [SetUp]
        public void Setup()
        {
            model = new AuthorisationModel();
        }

        class OverRiddenAuthorisationModel_1 : AuthorisationModel
        {
            private CertificateManager mockManager;
            private IViewError mockErrorView;

            public OverRiddenAuthorisationModel_1(IAuthorisationView view) : base(view) { }
            public OverRiddenAuthorisationModel_1(IAuthorisationView view, IWizard w) : base(view, w) { }
            public OverRiddenAuthorisationModel_1(IAuthorisationView view, IWizard w, CertificateManager mockManager)
                : base(view, w)
            {
                this.mockManager = mockManager;
            }
            public OverRiddenAuthorisationModel_1(IAuthorisationView view, IWizard w, CertificateManager mockManager,IViewError mockErrorView)
                : base(view, w)
            {
                this.mockManager = mockManager;
                this.mockErrorView = mockErrorView;
            }
            public OverRiddenAuthorisationModel_1(IAuthorisationView view, IWizard w, IViewError mockErrorView)
                : base(view, w)
            {
                this.mockErrorView = mockErrorView;
            }

            public override ITabNavigation CastITabNavigation(IAuthorisationView view)
            {
                ITabNavigation mock = MockRepository.GenerateStub<ITabNavigation>();
                mock.Stub(x => x.RegisterNext(Arg<EventHandler>.Is.Anything));
                return mock;
            }

            protected override GWydiR.Utilities.CertificateManager makeCertificateManager()
            {
                return mockManager;
            }

            protected override IViewError castToIViewError(IAuthorisationView authorisationView)
            {
                return mockErrorView;
            }
        }

        /// <summary>
        /// Test to make sure that the UI's get subscription method is called
        /// </summary>
        [Test]
        public void NextHandlerInvalidSubscriptionTest()
        {
            //Arrange
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.GetSelectedSubscription()).Return(string.Empty);
            mockView.Expect(x => x.GetSelectedCertificate()).Return(string.Empty);

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.AddSubscription(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mockWizard.Expect(x => x.HasSubscription(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(false);

            X509Certificate2 mockCertificate = MockRepository.GenerateStub<X509Certificate2>();
            mockCertificate.Stub(x => x.Thumbprint).Return(string.Empty);

            model = new OverRiddenAuthorisationModel_1(mockView,mockWizard);
            model.certificate = mockCertificate;

            //Act
            model.NextHandler(new object(), new EventArgs());

            //Assert
            mockView.VerifyAllExpectations();
            mockWizard.VerifyAllExpectations();
        }


        [Test]
        public void NextHandlerValidSubscriptionNoLocalCertificateTest()
        {
            //Arrange
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.GetSelectedSubscription()).Return(string.Empty);
            mockView.Expect(x => x.GetSelectedCertificate()).Return(string.Empty);

            IViewError mockErrorView = MockRepository.GenerateStrictMock<IViewError>();
            mockErrorView.Expect(x => x.NotifyOfError(Arg<Exception>.Is.Anything));

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.AddSubscription(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mockWizard.Expect(x => x.HasSubscription(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(true);
            mockWizard.Expect(x => x.GetThumbPrint(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(String.Empty);

            CertificateManager mockManager = MockRepository.GenerateStub<CertificateManager>();
            mockManager.Expect(x => x.CertificateExistsLocally(Arg<string>.Is.Anything)).Return(false);

            X509Certificate2 mockCertificate = MockRepository.GenerateStub<X509Certificate2>();
            mockCertificate.Stub(x => x.Thumbprint).Return(string.Empty);

            model = new OverRiddenAuthorisationModel_1(mockView,mockWizard,mockManager,mockErrorView);

            model.certificate = mockCertificate;

            //Act
            model.NextHandler(new object(), new EventArgs());

            //Assert
            mockView.VerifyAllExpectations();
            mockWizard.VerifyAllExpectations();
            mockErrorView.VerifyAllExpectations();
            mockManager.VerifyAllExpectations();
        }

        [Test]
        public void NextHandlerValidSubscriptionAndFoundLocallyTest()
        {
            //Arrange
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.GetSelectedSubscription()).Return(string.Empty);
            mockView.Expect(x => x.GetSelectedCertificate()).Return(string.Empty);

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.AddSubscription(Arg<string>.Is.Anything, Arg<string>.Is.Anything, Arg<string>.Is.Anything));
            mockWizard.Expect(x => x.HasSubscription(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(true);
            mockWizard.Expect(x => x.GetThumbPrint(Arg<string>.Is.Anything, Arg<string>.Is.Anything)).Return(String.Empty);

            X509Certificate2 mockCertificate = MockRepository.GenerateStub<X509Certificate2>();
            mockCertificate.Stub(x => x.Thumbprint).Return(string.Empty);

            CertificateManager mockManager = MockRepository.GenerateStub<CertificateManager>();
            mockManager.Expect(x => x.CertificateExistsLocally(Arg<string>.Is.Anything)).Return(true);
            mockManager.Expect(x => x.GetLocalCertificate(Arg<string>.Is.Anything)).Return(mockCertificate);

            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard, mockManager);

            model.certificate = mockCertificate;

            //Act
            model.NextHandler(new object(), new EventArgs());

            //Assert
            mockView.VerifyAllExpectations();
            mockWizard.VerifyAllExpectations();
            mockManager.VerifyAllExpectations();
        }

        /// <summary>
        /// Test to see that the New Subscription Method works as expected, to gather the new data and to update the comboBox of subscriptions.
        /// </summary>
        [Test]
        public void NewSubscriptionHandlerNoExceptionTest()
        {
            //Arrange
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.DisplaySubsriptions(Arg<List<string>>.Is.Anything));

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.addSID(Arg<string>.Is.Anything));

            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard);

            //Act
            model.NewSubscriptionHandler(string.Empty);

            //Assert
            mockView.VerifyAllExpectations();
            mockWizard.VerifyAllExpectations();
        }

        [Test]
        public void NewSubscriptionHandlerWithException()
        {
            //Arrange
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.DisplaySubsriptions(Arg<List<string>>.Is.Anything));

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.addSID(Arg<string>.Is.Anything)).Throw(new InvalidSIDException());

            IViewError mockErrorView = MockRepository.GenerateStub<IViewError>();
            mockErrorView.Expect(x => x.NotifyOfError(Arg<Exception>.Is.Anything));

            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard, mockErrorView);

            //Act
            model.NewSubscriptionHandler(string.Empty);

            //Assert
            mockView.VerifyAllExpectations();
            mockWizard.VerifyAllExpectations();
            mockErrorView.VerifyAllExpectations();
        }

        [Test]
        public void NewCertificateHandlerTest()
        {
            //Arrange
            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.AddCertificate(Arg<string>.Is.Anything));

            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.DisplayCertificates(Arg<List<string>>.Is.Anything));

            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard);

            //Act
            model.NewCertificateHandler(string.Empty);

            //Asssert
            mockWizard.VerifyAllExpectations();
            mockView.VerifyAllExpectations();
        }

        [Test]
        public void ChangedSIDSelectionHandlerHasNoCertificatesTest()
        {
            //Arrange
            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.GetSIDList()).Return(new List<string>() { string.Empty });
            mockWizard.Expect(x => x.GetSubscriptions()).Return(new List<Subscription>());

            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();

            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard);

            //Act
            int index = model.ChangedSIDSelectionHandler(0);

            //Assert
            mockWizard.VerifyAllExpectations();
            Assert.IsTrue(index == -1);
        }

        [Test]
        public void ChangedSIDSelectionHasCertificatesTest()
        {
            //Arrange
            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.GetSIDList()).Return(new List<string>() { string.Empty });
            Subscription mocksub = new Subscription();
            mockWizard.Expect(x => x.GetSubscriptions()).Return(new List<Subscription>() {mocksub});

            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();

            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard);

            //Act
            int index = model.ChangedSIDSelectionHandler(0);

            //Assert
            mockWizard.VerifyAllExpectations();
            Assert.IsTrue(index == 0);
        }

        

    }
}
