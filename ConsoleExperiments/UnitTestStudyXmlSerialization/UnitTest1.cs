using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyXmlSerialization;


namespace UnitTestStudyXmlSerialization
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AnyEmployedPersonMustHaveASalaryGreaterThan100()
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

            Assert.IsFalse(RuleEngine.EmployedAndSalaryGreaterThan(directorTest));
        }
        [TestMethod]
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
                DateOfBirth = new DateTime(2000, 12, 31),
                SocialSecurityNo = 5235343
            };

            Assert.IsFalse(RuleEngine.EmployedAndAgeGreaterThan(directorTest));
        }

    }
}
