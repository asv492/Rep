using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
    public class XmlEmployeeData : IDataInterface
    {
        private static string _path = @"C:\Users\q\Documents\CountWords\PersonList.xml";
        
        public int Add(Person person)
        {
            var personList = new List<Person>(ReadPersonList());
            int highestId = 0;
            // int highestId = personList.Select(personEntry => personEntry.Id).Concat(new[] { 0 }).Max();
            foreach (var personEntry in personList)
            {
                if (highestId < personEntry.Id)
                {
                    highestId = personEntry.Id;
                }
            }
            person.Id = highestId + 1;
            personList.Add(person);
            WritePersonList(personList);
            return person.Id;

        }

        public Person Retrieve(int personId)
        {
            var personList = new List<Person>(ReadPersonList());
            foreach (var personEntry in personList)
            {
                if (personId == personEntry.Id)
                {
                    return personEntry;
                }
            }
            return null;
        }

        public bool Update(Person person)
        {
            var personId = person.Id;
            var personList = new List<Person>(ReadPersonList());
            foreach (var personEntry in personList)
            {
                if (personId == personEntry.Id)
                {
                    var placeInList = personList.IndexOf(personEntry);
                    personList.RemoveAt(placeInList);
                    personList.Add(person);
                    WritePersonList(personList);

                    return true;
                }
            }
            return false;
        }

        public bool MarkAsNotEmployed(Person person)
        {
            person.IsEmployed = false;
            return Update(person);
        }

        private void WritePersonList(List<Person> personList)
        {
            var ser = new XmlSerializer(personList.GetType());
            using (var fs = new System.IO.FileStream(_path, System.IO.FileMode.Create))
            {
                ser.Serialize(fs, personList);
            }
        }

        private List<Person> ReadPersonList()
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

    }
}
