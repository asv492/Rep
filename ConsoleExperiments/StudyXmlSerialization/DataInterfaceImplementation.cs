using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class DataInterfaceImplementation : IDataInterface
    {
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
        private List<Person> ReadPersonList()
        {
            List<Person> personList = CsvImport.GetPersonList(_path);

            return personList;
        }

        private void WritePersonList(List<Person> personList)
        {
            var csv = new CsvExport<Person>(personList);
            csv.ExportToFile(_path);
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
    }
}
