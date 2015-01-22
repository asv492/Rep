using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class FiringResignUnitTest
    {
        [TestMethod]
        public void FiredEmployeeActivePropertySetToFalse()
        {
            var directorTest1 = new Director
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Doe",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1966, 12, 31),
                SocialSecurityNo = 5235222
            };
            Company.AddNewEmployee(directorTest1);
            Company.FireEmployee(directorTest1);
            Assert.AreEqual(false, directorTest1.IsEmployed);
        }
        [TestMethod]
        public void FiredEmployeeSalarySetToZero()
        {
            var directorTest2 = new Director
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Doe",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1966, 12, 31),
                SocialSecurityNo = 5235333
            };
            Company.AddNewEmployee(directorTest2);
            Company.FireEmployee(directorTest2);
            Assert.AreEqual(0, directorTest2.Salary);
        }
        [TestMethod]
        public void FiredEmployeeEmailIdSetToEmptyString() //company recycles emails
        {
            var directorTest3 = new Director
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Doe",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1966, 12, 31),
                SocialSecurityNo = 5235444
            };
            Company.AddNewEmployee(directorTest3);
            Company.FireEmployee(directorTest3);
            Assert.AreEqual("", directorTest3.Address);
        }

        [TestMethod]
        public void FiredEmployeeAllChangesAreMade() //company recycles emails
        {
            var directorTest4 = new Director
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Doe",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1966, 12, 31),
                SocialSecurityNo = 5235555
            };
            Company.AddNewEmployee(directorTest4);
            Company.FireEmployee(directorTest4);


            Assert.AreEqual(false, directorTest4.IsEmployed);
            Assert.AreEqual(0, directorTest4.Salary);
            Assert.AreEqual("", directorTest4.Address);

        }
    }
}
