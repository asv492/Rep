using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class DataInterfaceTest
    {
        private static Person _person = new Person { Id = 999, FirstName = "Albert", LastName = "Bach", ManagerId = 1, IsEmployed = true, Salary = 2220, Address = "USA 500", DateOfBirth = new DateTime(1973, 12, 31), SocialSecurityNo = 9995343 };
        private static string _path = @"C:\Users\q\Documents\CountWords\exportToCSV.csv";
        [TestMethod]
        public void DataInterfaceImplementation_Implements_IDataInterface()
        {
            var dataInterfaceImplementation = new DataInterfaceImplementation();
            var helper = new Helper();
            _person.Id = 7;
            var result = helper.AddPerson(dataInterfaceImplementation);
            Assert.AreEqual(result, 7);
        }
        [TestMethod]
        public void AddPersonReturnsHighestIdPlus1()
        {
            var dataInterfaceImplementation = new DataInterfaceImplementation();
            var result = dataInterfaceImplementation.Add(_person);
            Assert.AreEqual(result, 11);
        }
        [TestMethod]
        public void RetrievePersonReturnsTheRightPerson()
        {
            var dataInterfaceImplementation = new DataInterfaceImplementation();
            var result = dataInterfaceImplementation.Retrieve(11);
            Assert.AreEqual(result, _person);
        }
        [TestMethod]
        public void UpdatePersonReturnsTrue()
        {
            _person.Id = 3;
            var dataInterfaceImplementation = new DataInterfaceImplementation();
            var result = dataInterfaceImplementation.Update(_person);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void MarkAsNotEmployedReturnsTrue()
        {
            _person.Id = 4;
            var dataInterfaceImplementation = new DataInterfaceImplementation();
            var result = dataInterfaceImplementation.MarkAsNotEmployed(_person);
            Assert.AreEqual(result, true);
        }


        public interface IDataInterface
        {
            int Add(Person person);
            Person Retrieve(int personId);
            bool Update(Person person);
            bool MarkAsNotEmployed(Person person);

        }

        public class DataInterfaceImplementation : IDataInterface
        {
            public int Add(Person person)
            {
                var personList = new List<Person>(ReadPersonList());
                int highestId = 0;
               // int highestId = personList.Select(personEntry => personEntry.Id).Concat(new[] { 0 }).Max();
                foreach (var personEntry in personList)
                {
                    if (highestId<personEntry.Id)
                    {
                        highestId = personEntry.Id;
                    }
                }
                person.Id = highestId + 1;
                personList.Add(person);
                WritePersonList(personList);
                return person.Id;

            }
            private List<Person>  ReadPersonList()
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

        public class Helper
        {
            public int AddPerson(IDataInterface iDataInterface)
            {
                return iDataInterface.Add(_person);
            }

        }
    }
}
