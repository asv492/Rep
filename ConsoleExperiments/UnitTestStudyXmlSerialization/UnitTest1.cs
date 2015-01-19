using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyXmlSerialization;


namespace UnitTestStudyXmlSerialization
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        [ExpectedException(typeof(CustomExceptions.AnyEmployedPersonMustBeOver16YearsOldException))]
        public void AnyEmployedPersonMustBeOver16YearsOld()
        {
            var directorTest = new Director
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 1000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(2000, 12, 31), // less than 16 years old
                SocialSecurityNo = 5235343
            };
            Company.AddNewEmployee(directorTest);
        }

        [TestMethod]
        [ExpectedException(typeof(CustomExceptions.EmployedAndSalaryGreaterThanException))]
        public void AnyEmployedPersonMustHaveASalaryGreaterThan100WithException()
        {
            var directorTest = new Director
            {
                Id = 1,
                FirstName = "John",
                LastName = "Smith",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10, // < 100
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1970, 12, 31),
                SocialSecurityNo = 5235343
            };
            Company.AddNewEmployee(directorTest);


        }

    }
}
