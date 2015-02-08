using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
   public class GenericEmployeeData
    {
        private IDataInterfaceReadWrite _typeOfExport;
        public GenericEmployeeData(IDataInterfaceReadWrite typeOfExport)
        {
            this._typeOfExport = typeOfExport;
        }

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

        public List<Person> ReadPersonList()
        {
            var personList = _typeOfExport.ReadPersonList();
            return personList;
        }

        public void WritePersonList(List<Person> personList)
        {
            _typeOfExport.WritePersonList(personList);
        }
    }
}
