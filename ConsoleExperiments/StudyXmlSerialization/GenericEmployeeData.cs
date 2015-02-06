using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
    class GenericEmployeeData<T> : IDataInterface
    {
        private T _typeOfExport;
        public GenericEmployeeData(T typeOfExport)
        {
            this._typeOfExport = typeOfExport;
        }
        private static string _path = @"C:\Users\q\Documents\CountWords\exportToCSV.csv";

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

        private List<Person> ReadPersonList()
        {
            if (_typeOfExport.GetType() == typeof(CsvEmployeeData))
            {
                List<Person> personList = CsvImport.GetPersonList(_path);

                return personList;
            }
            else if (_typeOfExport.GetType() == typeof(XmlEmployeeData))
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
            throw new NotImplementedException("unknown type");
        }

        private void WritePersonList(List<Person> personList)
        {
            if (_typeOfExport.GetType() == typeof(CsvEmployeeData))
            {
                var csv = new CsvExport<Person>(personList);
                csv.ExportToFile(_path);
            }
            else if (_typeOfExport.GetType() == typeof(XmlEmployeeData))
            {
                var ser = new XmlSerializer(personList.GetType());
                using (var fs = new System.IO.FileStream(_path, System.IO.FileMode.Create))
                {
                    ser.Serialize(fs, personList);
                }
            }
            throw new NotImplementedException("unknown type");
        }
    }
}
