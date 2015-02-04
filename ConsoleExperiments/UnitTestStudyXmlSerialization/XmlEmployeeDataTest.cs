using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class XmlEmployeeDataTest
    {
        private static Person _person = new Person { Id = 999, FirstName = "Albert", LastName = "Bach", ManagerId = 1, IsEmployed = true, Salary = 2220, Address = "USA 500", DateOfBirth = new DateTime(1973, 12, 31), SocialSecurityNo = 9995343 };
       
        //Remove comments to restore xml to original
        //private static string _path = @"C:\Users\q\Documents\CountWords\PersonList.xml";

        //[TestMethod]
        //public void TestReturnXmlToOriginal()
        //{
        //    Company.EmployeeRoster.Clear();
        //    Company.FillEmployeeRoster();
        //    var personList = Company.EmployeeRoster;
        //    WritePersonList(personList);

        //}
        //private void WritePersonList(List<Person> personList)
        //{
        //    var ser = new XmlSerializer(personList.GetType());
        //    using (var fs = new System.IO.FileStream(_path, System.IO.FileMode.Create))
        //    {
        //        ser.Serialize(fs, personList);
        //    }
        //}

        [TestMethod]
        public void AddPersonReturnsHighestIdPlus1()
        {
            var dataInterfaceImplementation = new XmlEmployeeData();
            var result = dataInterfaceImplementation.Add(_person);
            Assert.AreEqual(result, 11);
        }
        [TestMethod]
        public void RetrievePersonReturnsTheRightPerson()
        {
            var dataInterfaceImplementation = new XmlEmployeeData();
            var result = dataInterfaceImplementation.Retrieve(11);
            // put breakpoint here
            Assert.AreEqual(result, _person);
        }
        [TestMethod]
        public void UpdatePersonReturnsTrue()
        {
            _person.Id = 3;
            var dataInterfaceImplementation = new XmlEmployeeData();
            var result = dataInterfaceImplementation.Update(_person);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void MarkAsNotEmployedReturnsTrue()
        {
            _person.Id = 4;
            var dataInterfaceImplementation = new XmlEmployeeData();
            var result = dataInterfaceImplementation.MarkAsNotEmployed(_person);
            Assert.AreEqual(result, true);
        }




    }



}

