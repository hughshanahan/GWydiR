using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GWydiR.Containers;
using NUnit.Framework;
using Rhino.Mocks;

namespace GWydiRTest.ContainerTests
{
    class ServiceConfigurationTests
    {
        ServiceConfiguration config;

        [SetUp]
        public void setup()
        {
            config = new ServiceConfiguration();
        }


        [Test]
        public void SerializationTest()
        {
            config.osFamily = 3;
            System.Xml.Serialization.XmlSerializer serializaer = new System.Xml.Serialization.XmlSerializer(config.GetType());
            serializaer.Serialize(Console.Out, config);
            Console.WriteLine();
        }
    }

}
