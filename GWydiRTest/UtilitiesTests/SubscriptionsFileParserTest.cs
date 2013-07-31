using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Utilities;
using NUnit.Framework;
using Rhino.Mocks;
using GWydiR.Containers;

namespace GWydiRTest.UtilitiesTests
{
    class SubscriptionsFileParserTest
    {
        SubscriptionFileParser parser;

        [SetUp]
        public void setup()
        {
            parser = new SubscriptionFileParser();
        }

        [Test]
        public void ParseFileDataTest()
        {
            //Arrange
            string test1 = "test1";
            string test2 = "test2";
            string test3 = "test3";
            List<string> testFileData = new List<string>() { test1+","+test2+","+test3 };

            //Act
            List<Subscription> output = parser.ParseSubscriptions(testFileData);

            //Assert
            Assert.IsTrue(output[0].SID == test1);
            Assert.IsTrue(output[0].CertName == test2);
            Assert.IsTrue(output[0].STSThumbPrint == test3);
        }

        [Test]
        public void ParseSubscriptionsDataIntoSIDsTest()
        {
            //Arrange
            string test1 = "test1";
            string test2 = "test2";
            string test3 = "test3";

            Subscription list1 = new Subscription();
            list1.SID = test1;
            Subscription list2 = new Subscription();
            list2.SID = test2;
            Subscription list3 = new Subscription();
            list3.SID = test3;


            List<Subscription> testList = new List<Subscription>();
            testList.Add(list1);
            testList.Add(list2);
            testList.Add(list3);

            //Act
            List<string> output = parser.ParseSids(testList);

            //Assert
            Assert.IsTrue(output[0] == test1);
            Assert.IsTrue(output[1] == test2);
            Assert.IsTrue(output[2] == test3);
        }

        [Test]
        public void ParseCertificatesDataIntoListTest()
        {
            //Arrange
            string testSID1 = "anSID";
            string testCert1 = "aCert";

            Subscription list1 = new Subscription();
            list1.SID = testSID1;
            list1.CertName = testCert1;

            List<Subscription> testList = new List<Subscription>();
            testList.Add(list1);

            //Act
            List<string> output = parser.ParseCertificateNames(testList);

            //Assert
            Assert.IsTrue(output[0] == testCert1);

        }

        [Test]
        public void ParserMultipleCertificatesIntoListTest()
        {
            //Arange
            string testSid1 = "sid1";
            string testSid2 = "sid2";
            string testCert1 = "cert1";
            string testCert2 = "cert2";

            Subscription list1 = new Subscription();
            list1.SID = testSid1;
            list1.CertName = testCert1;
            Subscription list2 = new Subscription();
            list2.SID = testSid2;
            list2.CertName = testCert2;

            List<Subscription> testList = new List<Subscription>();
            testList.Add(list1);
            testList.Add(list2);

            //Act
            List<string> output = parser.ParseCertificateNames(testList);

            //Assert
            Assert.IsTrue(output[0] == testCert1);
            Assert.IsTrue(output[1] == testCert2);
        }
    }
}
