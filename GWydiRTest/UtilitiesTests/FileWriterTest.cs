using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;
using GWydiR;

namespace GWydiRTest
{
    [TestFixture]
    class FileWriterTest
    {
        FileWriter fileWriter;

        [SetUp]
        public void setUp()
        {
            fileWriter = new FileWriter();
        }

        class OverRiddenWriter_1 : FileWriter
        {
            object mockObject;
            public OverRiddenWriter_1(object mockObject)
            {
                this.mockObject = mockObject;
            }

            protected override StreamWriter makeStream(string filePath)
            {
                return (StreamWriter) mockObject;
            }

            protected override StreamWriter makeStream(string filePath, FileMode mode)
            {
                return (StreamWriter)mockObject;
            }
        }

        /// <summary>
        /// Test to check that the write method of a built streamwriter object is called from the FileWriter Object
        /// </summary>
        [Test]
        public void writeListToFileTest()
        {
            StreamWriter stubWriter = MockRepository.GenerateStub<StreamWriter>();
            stubWriter.Stub(x => x.Write(Arg<string>.Is.Anything,Arg<FileMode>.Is.Anything));
            fileWriter = new OverRiddenWriter_1(stubWriter);
            List<string> testData = new List<string>() { "111111-1111-1111-1111-111111111111", "222222-2222-2222-2222-22222222222" };
            string testPath = "\\not\\a\\file\\path.txt";
            fileWriter.Write(testPath,testData);
            stubWriter.AssertWasCalled(x => x.WriteLine(Arg<string>.Is.Anything));
        }

        [Test]
        public void writeStringToFileTest()
        {
            StreamWriter stubWriter = MockRepository.GenerateStub<StreamWriter>();
            stubWriter.Stub(x => x.Write(Arg<string>.Is.Anything));
            fileWriter = new OverRiddenWriter_1(stubWriter);
            string testData = "111111-1111-1111-1111-111111111111";
            string testPath = "\\this\\is\\not\\a\\path.dat";
            fileWriter.Write(testPath, testData);
            stubWriter.AssertWasCalled(x => x.WriteLine(Arg<string>.Is.Anything));
        }

        // class to allow ocing of filestream object
        class OverRiddenWriter_2 : FileWriter
        {
            private object mockFile;
            private bool called = false;
            public OverRiddenWriter_2(object mockFile) : base()
            {
                this.mockFile = mockFile;
            }

            protected override FileStream makeFile(string filePath)
            {
                called = true;
                return (FileStream) mockFile;
            }

            public bool wasCalled()
            {
                return called;
            }
        }

        /// <summary>
        /// Test to certify that a FileStream Object is being created via the create method.
        /// </summary>
        [Test]
        public void createFileTest()
        {
            //Arrange
            FileStream stubFile = MockRepository.GenerateStub<FileStream>();
            fileWriter = new OverRiddenWriter_2(stubFile);
            string testPath = "\\this\\is\\not\\a\\path.dat";

            //Act
            fileWriter.Create(testPath);

            //Assert
            bool passed = ((OverRiddenWriter_2)fileWriter).wasCalled();
            Assert.IsTrue(passed);
        }

        [Test]
        public void WriteFileFromBytesTest()
        {
            //Arrange
            FileStream stubFile = MockRepository.GenerateMock<FileStream>();
            stubFile.Expect(x => x.Write(Arg<byte[]>.Is.Anything, Arg<int>.Is.Anything, Arg<int>.Is.Equal(1)));
            stubFile.Expect(x => x.Close());

            fileWriter = new OverRiddenWriter_2(stubFile);

            string testPath ="\\this\\is\\a\\path\\not.dat";
            byte[] aByteArrray = new byte[1];

            //Act
            fileWriter.Write(testPath,aByteArrray);

            //Assert
            stubFile.VerifyAllExpectations();
        }

    }
}
