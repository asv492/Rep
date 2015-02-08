using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
    public class XmlReadWrite : IDataInterfaceReadWrite
    {
        private static string _path = @"C:\Users\q\Documents\CountWords\PersonList.xml";
        public List<Person> ReadPersonList()
        {
            var myDeserializer = new XmlSerializer(typeof(List<Person>));
            List<Person> personList;
            using (
                var myFileStream = new FileStream(_path, FileMode.Open))
            {
                personList = (List<Person>)myDeserializer.Deserialize(myFileStream);
            }
            return personList;
        }

        public void WritePersonList(List<Person> personList)
        {
            var ser = new XmlSerializer(personList.GetType());
            using (var fs = new System.IO.FileStream(_path, System.IO.FileMode.Create))
            {
                ser.Serialize(fs, personList);
            }
        }
    }
}
