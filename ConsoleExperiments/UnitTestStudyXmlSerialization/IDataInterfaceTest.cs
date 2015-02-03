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






        public class Helper
        {
            public int AddPerson(IDataInterface iDataInterface)
            {
                return iDataInterface.Add(_person);
            }

        }
    }
}
