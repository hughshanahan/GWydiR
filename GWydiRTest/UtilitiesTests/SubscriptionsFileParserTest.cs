using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Utilities;
using NUnit.Framework;
using Rhino.Mocks;

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
            List<List<string>> output = parser.ParseSubscriptions(testFileData);

            //Assert
            Assert.IsTrue(output[0][0] == test1);
            Assert.IsTrue(output[0][1] == test2);
            Assert.IsTrue(output[0][2] == test3);
        }

        [Test]
        public void ParseSubscriptionsDataIntoSIDsTest()
        {
            //Arrange
            string test1 = "test1";
            string test2 = "test2";
            string test3 = "test3";

            List<string> list1 = new List<string>() { test1 };
            List<string> list2 = new List<string>() { test2 };
            List<string> list3 = new List<string>() { test3 };

            List<List<string>> testList = new List<List<string>>() { list1, list2, list3 };

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

            List<string> list1 = new List<string>() { testSID1,testCert1};

            List<List<string>> testList = new List<List<string>>() { list1 };

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

            List<string> list1 = new List<string>() { testSid1, testCert1 };
            List<string> list2 = new List<string>() { testSid2, testCert2 };

            List<List<string>> testList = new List<List<string>>() { list1, list2 };

            //Act
            List<string> output = parser.ParseCertificateNames(testList);

            //Assert
            Assert.IsTrue(output[0] == testCert1);
            Assert.IsTrue(output[1] == testCert2);
        }
    }
}
