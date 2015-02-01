using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace StudyXmlSerialization
{
    [TestClass]
    public class JsonSerializationTest
    {
        [TestMethod]
        public void TestSerializeListToFile()
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
        }
        [TestMethod]
        public void DeSerializeFileToList()
        {

        }

        [TestMethod]
        
        public void Test()

        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            Company.WriteJsonToFile();

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            string jsonText = File.ReadAllText(@"C:\Users\q\Documents\CountWords\serializareJSON.json");
            MemoryStream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream);
            writer.Write(jsonText);
            writer.Flush();
            stream.Position = 0;
            var personList = new List<Person>();
            using (stream)
            {
                personList.Add((Person)ser.ReadObject(stream));
            }
        }
    }
}
