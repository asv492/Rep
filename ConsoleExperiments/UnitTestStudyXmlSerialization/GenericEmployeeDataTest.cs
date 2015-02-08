using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class GenericEmployeeDataTest
    {
        private static Person _person = new Person { Id = 999, FirstName = "Albert", LastName = "Bach", ManagerId = 1, IsEmployed = true, Salary = 2220, Address = "USA 500", DateOfBirth = new DateTime(1973, 12, 31), SocialSecurityNo = 9995343 };

        //Remove comments to restore xml to original

        [TestMethod]
        public void TestReturnXmlToOriginal()
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            var personList = Company.EmployeeRoster;
            var typeOfExport = new XmlReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            employeeData.WritePersonList(personList);

        }
        //with xml

        [TestMethod]
        public void WithXmlAddPersonReturnsHighestIdPlus1()
        {
            var typeOfExport = new XmlReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.Add(_person);
            Assert.AreEqual(result, 11);
        }
        [TestMethod]
        public void WithXmlRetrievePersonReturnsTheRightPerson()
        {
            var typeOfExport = new XmlReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.Retrieve(11);
            // put breakpoint here
            Assert.AreEqual(result, _person);
        }
        [TestMethod]
        public void WithXmlUpdatePersonReturnsTrue()
        {
            _person.Id = 3;
            var typeOfExport = new XmlReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.Update(_person);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WithXmlMarkAsNotEmployedReturnsTrue()
        {
            _person.Id = 4;
            var typeOfExport = new XmlReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.MarkAsNotEmployed(_person);
            Assert.AreEqual(result, true);
        }

        //with csv
        [TestMethod]
        public void WithCsvAddPersonReturnsHighestIdPlus1()
        {
            var typeOfExport = new CsvReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.Add(_person);
            Assert.AreEqual(result, 11);
        }
        [TestMethod]
        public void WithCsvRetrievePersonReturnsTheRightPerson()
        {
            var typeOfExport = new CsvReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.Retrieve(11);
            // put breakpoint here
            Assert.AreEqual(result, _person);
        }
        [TestMethod]
        public void WithCsvUpdatePersonReturnsTrue()
        {
            _person.Id = 3;
            var typeOfExport = new CsvReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.Update(_person);
            Assert.AreEqual(result, true);
        }
        [TestMethod]
        public void WithCsvMarkAsNotEmployedReturnsTrue()
        {
            _person.Id = 4;
            var typeOfExport = new CsvReadWrite();
            var employeeData = new GenericEmployeeData(typeOfExport);
            var result = employeeData.MarkAsNotEmployed(_person);
            Assert.AreEqual(result, true);
        }
    }
}
