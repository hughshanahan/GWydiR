using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using System.IO;
using GWydiR.Utilities;

namespace GWydiR.Test
{
    [TestFixture]
    class FileReaderTest
    {
        FileReader fileReader;

        [SetUp]
        public void setUp()
        {
            fileReader = new FileReader();
        }


        // inner class becuase c# doesn't do anonymous inline classes
        class MockReader_1 : FileReader
        {
            object returnObject;

            public MockReader_1(object returnObject) : base()
            {
                this.returnObject = returnObject;
            }

            public override StreamReader makeStream(string FileName)
            {
                // cast return object to stream reader
                return (StreamReader) returnObject;
            }
        }


        /// <summary>
        /// Test to see if given a responce from a stream reader the filereader can 
        /// Create a list of lines from it.
        /// </summary>
        [Test]
        public void readFileTest()
        {

            StreamReader stubReader =  MockRepository.GenerateStub<StreamReader>();
            string testFileContents = "123456-1234-1234-1234-123456789101\r";
            string testPath = "\\a\\test\\path.txt";
            stubReader.Stub(X => X.ReadToEnd()).Return(testFileContents);
            fileReader = new MockReader_1(stubReader);
            List<string> formattedData = fileReader.Read(testPath);
            Assert.IsTrue(formattedData.Count == 1);
        }
        
        
   }

}
