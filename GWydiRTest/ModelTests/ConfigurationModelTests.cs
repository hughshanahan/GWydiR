using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using GWydiR.Models;
using GWydiR.Interfaces.ModelInterfaces;
using GWydiR.Interfaces.ViewInterfaces;

namespace GWydiRTest.ModelTests
{
    class ConfigurationModelTests
    {

        ConfigurationModel model;

        [SetUp]
        public void seetup()
        {
            model = new ConfigurationModel();
        }


        class OverRiddenConfigModel_1 : ConfigurationModel
        {

            public OverRiddenConfigModel_1(IWizard wizard, IConfigurationView view) : base (view,wizard)
            {
                  
            }

            protected override ITabNavigation castNavigationView(IConfigurationView view)
            {
                return MockRepository.GenerateStub<ITabNavigation>();
            }

        }

        [Test]
        public void GetConnectionStringProperlyFormattedAppStoreTest()
        {
            //Arrange

            string appName = "apptestname";
            string appKey = "atestkey-1234-1234-1234";

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();

            IConfigurationView mockConfView = MockRepository.GenerateStub<IConfigurationView>();
            mockConfView.Stub(x => x.GetAppStoreName()).Return(appName);
            mockConfView.Stub(x => x.GetAppStoreKey()).Return(appKey);

            model = new OverRiddenConfigModel_1(mockWizard, mockConfView);

            //Act
            string connectionString = model.GetConnectionString(GWydiR.Flags.AccountType.AppStorage);

            //Assert
            Assert.IsTrue(("DefaultEndpointsProtocol=https;AccountName="+appName+";AccountKey="+appKey).Equals(connectionString));
        }

        [Test]
        public void GetConnectionStringProperlyFormattedDataStoreTest()
        {
            //Arrange
            string appName = "apptestname";
            string appKey = "atestkey-1234-1234-1234";

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();

            IConfigurationView mockConfView = MockRepository.GenerateStub<IConfigurationView>();
            mockConfView.Stub(x => x.GetDataStoreName()).Return(appName);
            mockConfView.Stub(x => x.GetDataStoreKey()).Return(appKey);

            model = new OverRiddenConfigModel_1(mockWizard, mockConfView);

            //Act
            string connectionString = model.GetConnectionString(GWydiR.Flags.AccountType.DataStorage);

            //Assert
            Assert.IsTrue(("DefaultEndpointsProtocol=https;AccountName=" + appName + ";AccountKey=" + appKey).Equals(connectionString));
        }

        [Test]
        public void GetDNSNameFormattedTest()
        {
            //Arrange
            string dnsName = "AcloudService";

            IWizard mockWizard = MockRepository.GenerateStub<IWizard>();

            IConfigurationView mockConfView = MockRepository.GenerateStub<IConfigurationView>();
            mockConfView.Stub(x => x.GetCloudServiceName()).Return(dnsName);

            model = new OverRiddenConfigModel_1(mockWizard, mockConfView);

            //Act
            string cloudUrl = model.GetDNSName();

            //Assert
            Assert.IsTrue((dnsName + ".cloudapp.net").Equals(cloudUrl));
        }



    }
}
