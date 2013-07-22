using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;
using NUnit.Framework;
using Rhino.Mocks;

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

            public OverRiddenAuthorisationModel_1(IAuthorisationView view) : base(view) { }
            public OverRiddenAuthorisationModel_1(IAuthorisationView view, IWizard w) : base(view, w) { }

            public override ITabNavigation CastITabNavigation(IAuthorisationView view)
            {
                ITabNavigation mock = MockRepository.GenerateStub<ITabNavigation>();
                mock.Stub(x => x.RegisterNext(Arg<EventHandler>.Is.Anything));
                return mock;
            }
        }

        /// <summary>
        /// Test to make sure that the UI's get subscription method is called
        /// </summary>
        [Test]
        public void NextHandlerCallsSubscriptionTest()
        {
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.GetSelectedSubscription()).Return(string.Empty);
            model = new OverRiddenAuthorisationModel_1(mockView);
            model.NextHandler(new object(), new EventArgs());
            mockView.AssertWasCalled(x => x.GetSelectedSubscription());
        }

        [Test]
        public void NextHandlerCallsCertificateTest()
        {
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.GetSelectedSubscription()).Return(string.Empty);
            mockView.Expect(x => x.GetSelectedCertificate()).Return(string.Empty);
            model = new OverRiddenAuthorisationModel_1(mockView);
            model.NextHandler(new object(), new EventArgs());
            mockView.AssertWasCalled(x => x.GetSelectedCertificate());
        }

        /// <summary>
        /// Test to see that the New Subscription Method works as expected, to gather the new data and to update the comboBox of subscriptions.
        /// </summary>
        [Test]
        public void NewSubscriptionHandlerTest()
        {
            IAuthorisationView mockView = MockRepository.GenerateStub<IAuthorisationView>();
            mockView.Expect(x => x.DisplaySubsriptions(Arg<List<string>>.Is.Anything));
            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();
            mockWizard.Expect(x => x.addSID(Arg<string>.Is.Anything));
            mockWizard.SIDList = new List<string>();
            model = new OverRiddenAuthorisationModel_1(mockView, mockWizard);
            model.NewSubscriptionHandler(string.Empty);
            mockView.VerifyAllExpectations();
        }





        

    }
}
