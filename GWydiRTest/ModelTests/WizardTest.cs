using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using GWydiR;
using GWydiR.Utilities;
using GWydiR.Containers;

namespace GWydiR.Test
{
    [TestFixture]
    class WizardTest
    {
        GWydiR.Wizard wizard;
        string testSID;

        // need to mock the reader and writer, this will mean adding factory methods for each, meaning that 
        // i wil have to instantiate each object when in reality this is inconvenient.
        class OverRiddenWizard_1 : Wizard
        {


            public OverRiddenWizard_1()
            {

            }

            public override FileReader makeReader()
            {
                FileReader mockReader = MockRepository.GenerateStub<FileReader>();
                mockReader.Stub(x => x.Read(Arg<string>.Is.Anything)).Return(new List<string>());
                return (FileReader) mockReader;
            }

            public override FileWriter makeWriter()
            {
                FileWriter mockWriter = MockRepository.GenerateStub<FileWriter>();
                mockWriter.Stub(x => x.Write(Arg<string>.Is.Anything, Arg<string>.Is.Anything));
                mockWriter.Stub(x => x.Write(Arg<string>.Is.Anything, Arg<List<string>>.Is.Anything));
                mockWriter.Stub(x => x.Create(Arg<string>.Is.Anything));
                return (FileWriter) mockWriter;
            }
        }

        [SetUp]
        public void setUp()
        {
            
            
            wizard = new OverRiddenWizard_1();
            // mockWriter.Stub(x => x.Create(Arg<string>.Is.Anything));
            testSID = "1ab23c-23de-d3d2-4f2f-2f2c2d1a4f3d";
        }

        /// <summary>
        /// A Test to see if the wizard object can identify an SID given to it
        /// in a list of SID's that it holds.
        /// </summary>
        [Test]
        public void findSIDFromListTest()
        {
            List<string> testList = new List<string>() {testSID};
            wizard.SIDList = testList;
            bool found = false;
            try
            {
                found = wizard.hasSID(testSID);
            }
            finally
            {
                Assert.IsTrue(found);
            }
        }

        /// <summary>
        /// This test will see if when givn an SID that is not in it's current list, whether it can add that
        /// SID to the list.
        /// </summary>
        [Test]
        public void notFindSIDTest()
        {
            bool found = false;
            try
            {
                found = wizard.hasSID(testSID);
            }
            finally
            {
                Assert.IsFalse(found, wizard.SIDList.ToString());
            }
        }

        [Test]
        public void addSIDTest()
        {
            try
            {
                wizard.addSID(testSID);
            }
            finally 
            {
                Assert.Contains(testSID, wizard.SIDList);
            }

        }

        [Test]
        public void addSIDDuplicatesTest()
        {
            try
            {
                wizard.addSID(testSID);
                wizard.addSID(testSID);
            }
            finally
            {
                Assert.IsTrue(wizard.SIDList.Count == 1);
            }
        }

        /// <summary>
        /// Test to deal with an Empty SID value
        /// </summary>
        [Test]
        public void emptySIDTest()
        {
            try
            {
                wizard.addSID(string.Empty);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is GWydiR.Exceptions.InvalidSIDException);
            }
        }

        /// <summary>
        /// Test to check a regex validator of SID's
        /// </summary>
        [Test]
        public void validSIDTest()
        {
            string testID = "111111-1111-1111-1111-111111111111";
            bool valid = false;
            try
            {
                valid = wizard.isValidSID(testID);
            }
            finally
            {
                Assert.IsTrue(valid);
            }
        }

        /// <summary>
        /// Test method to make sure sid's that do not match are not accepted
        /// </summary>
        [Test]
        public void notValidSIDTest()
        {
            string testID = "this is not a subscription is";
            bool valid = false;
            try
            {
                valid = wizard.isValidSID(testID);
            }
            catch (Exception e)
            {
            }
            Assert.IsFalse(valid);
        }

        /// <summary>
        /// Test to check that when given an invalid SID an appropriate exception is thrown
        /// </summary>
        [Test]
        public void checkForInvalidSIDTest()
        {
            string testID = "12345-123-123-123-1234689";
            bool thrown = false;
            try
            {
                wizard.addSID(testID);
            }
            catch (Exception e)
            {
                if (e is Exceptions.InvalidSIDException)
                {
                    thrown = true;
                }
            }
            Assert.IsTrue(thrown);
        }

        [Test]
        public void emptySIDValidTest()
        {
            try
            {
                wizard.isValidSID(string.Empty);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is GWydiR.Exceptions.InvalidSIDException);
            }
        }

        /// <summary>
        /// Test to see if duplicates are prvented from occuring
        /// </summary>
        [Test]
        public void checkForDuplicatesTest()
        {
            string testID = "123456-1234-1234-1234-123456789101";
            try
            {
                wizard.addSID(testID);
                wizard.addSID(testID);
                wizard.addSID(testID);
            }
            finally
            {

                List<string> results = wizard.SIDList;
                bool passed = true;
                for (int i = 0; i < results.Count - 2; i++)
                {
                    for (int j = i + 1; j < results.Count - 1; j++)
                    {
                        if (results[i] == results[j])
                        {
                            passed = false;
                        }
                    }
                }
                Assert.IsTrue(passed);
            }
        }

        [Test]
        public void PreventEmtpyLastValueTest()
        {
            try
            {
                wizard.addSID(testSID);
            }
            catch (Exception e)
            {

            }



            Assert.IsTrue(wizard.SIDList[wizard.SIDList.Count-1] != "");
        }

        class OverRiddenWizard_2 : Wizard
        {

            private FileWriter mockWriter;

            public OverRiddenWizard_2()
            {
                
            }
            public OverRiddenWizard_2(FileWriter mockWriter)
            {
                this.mockWriter = mockWriter;
            }

            protected override SubscriptionFileParser makeSubscriptionsParser()
            {
                SubscriptionFileParser mockParser = MockRepository.GenerateStub<SubscriptionFileParser>();
                mockParser.Expect(x => x.ParseCertificateNames(Arg<List<Subscription>>.Is.Anything)).Return(new List<string>());
                mockParser.Stub(x => x.ParseSubscriptions(Arg<List<string>>.Is.Anything)).Return(new List<Subscription>());
                mockParser.Stub(x => x.ParseSids(Arg<List<Subscription>>.Is.Anything)).Return(new List<string>());
                return mockParser;
            }

            public override FileWriter makeWriter()
            {
                return mockWriter;
            }

            
        }

        [Test]
        public void HasCertTest()
        {
            //Arrange
            string testName1 = "testname1";
            wizard = new OverRiddenWizard_2();

            //Act
            bool pass = wizard.HasCert(testName1);

            //Assert
            Assert.IsFalse(pass);
        }

        [Test]
        public void AddCertTest()
        {
            //Arrange
            string testName = "testName";
            wizard = new OverRiddenWizard_2();

            //Act
            wizard.AddCertificate(testName);
            List<string> output = wizard.CertList;

            //Assert
            Assert.IsTrue(output[0] == testName);
        }

        [Test]
        public void AddMultipleCertsAddNoDuplicatesTest()
        {
            //Arrange
            string testName = "testName";
            wizard = new OverRiddenWizard_2();

            //Act
            wizard.AddCertificate(testName);
            wizard.AddCertificate(testName);
            List<string> output = wizard.CertList;

            //Assert
            Assert.IsTrue(output.Count == 1);
        }

        /// <summary>
        /// This test will be testing if adding the data required to make a new subscription
        /// updates the subsciption list within the wizard object.
        /// </summary>
        [Test]
        public void AddSubscriptionTest()
        {
            //Arrange
            string testSID = "anSID";
            string testCertName = "aCertName";
            string testCertThumb = "aCertThumb";

            //Act
            wizard.AddSubscription(testSID, testCertName, testCertThumb,testCertThumb);

            //Assert
            Assert.IsTrue(wizard.GetSubscriptions().Count == 1);
        }

        [Test]
        public void HasSubscriptionFalseTest()
        {
            //Arrange
            string testSID = "anSID";
            string testCertName = "aCertName";
            string testCertThumb = "aCertThumb";

            //Act
            bool pass = wizard.HasSubscription(testSID, testCertName);
            
            //Assert
            Assert.IsFalse(pass);
        }

        [Test]
        public void HasSubscriptionTrueTest()
        {
            //Arrange
            string testSID = "anSID";
            string testCertName = "aCertName";
            string testCertThumb = "aCertThumb";

            //Act
            wizard.AddSubscription(testSID, testCertName, testCertThumb,testCertThumb);
            bool pass = wizard.HasSubscription(testSID, testCertName);

            //Assert
            Assert.IsTrue(pass);
        }

        [Test]
        public void AddMultipleSubscriptionsTest()
        {
            //Arrange
            string testSID = "anSID";
            string testCertName = "aCertName";
            string testCertThumb = "aCertThumb";
            
            //Act
            wizard.AddSubscription(testSID, testCertName, testCertThumb, testCertThumb); 
            wizard.AddSubscription(testSID, testCertName, testCertThumb, testCertThumb);

            //Assert
            Assert.IsTrue(wizard.GetSubscriptions().Count == 1);
        }

        [Test]
        public void GetThumbPrintTest()
        {
            //Arrange
            string testSID = "anSID";
            string testCertName = "aCertname";
            string testCertPrint = "aprint";

            wizard.AddSubscription(testSID, testCertName, testCertPrint, testCertPrint);

            //Act
            string output = wizard.GetSTSThumbPrint(testSID,testCertName);

            //Assert
            Assert.IsTrue(output == testCertPrint);
        }

        [Test]
        public void SaveSubscriptionsTest()
        {
            //Arrange
            FileWriter mockWriter = MockRepository.GenerateMock<FileWriter>();
            mockWriter.Expect(x => x.Write(Arg<string>.Is.Anything, Arg<List<string>>.Is.Anything));

            wizard = new OverRiddenWizard_2(mockWriter);

            //Act
            wizard.SaveSubscriptions();

            //Assert
            mockWriter.VerifyAllExpectations();
        }


    }
}
