using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyXmlSerialization;


namespace StudyXmlSerialization
{
    [TestClass]
    public class AddEmployeeUnitTest
    {

        [TestMethod]
        [ExpectedException(typeof(EmployeeRulesExceptions.AnyEmployedPersonMustBeOver16YearsOldException))]
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
        [ExpectedException(typeof(EmployeeRulesExceptions.EmployedAndSalaryGreaterThanException))]
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

        [TestMethod]
        public void NewEmailUsernamesAreUnique()
        {
            //create 15 users with identical names
            for (int i = 0; i < 15; i++)
            {
                var ssn = 5235349 + i;

                var directorTest = new Director
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    ManagerId = 1,
                    IsEmployed = true,
                    Salary = 10000, 
                    Address = "Boschdijk",
                    DateOfBirth = new DateTime(1970, 12, 31),
                    SocialSecurityNo = ssn,
                };
                Company.AddNewEmployee(directorTest);
            }
            foreach (var pers in Company.EmployeeRoster)
            {
                Console.WriteLine(pers.Address);
            }
            /*
             * output
             * John.Smith
               John.Smith1
               John.Smith2
               John.Smith3
               John.Smith4
               John.Smith5
               John.Smith6
               John.Smith7
               John.Smith8
               John.Smith9
               John.Smith10
               John.Smith11
               John.Smith12
               John.Smith13
               John.Smith14
             */
        }

        [TestMethod]
        [ExpectedException(typeof(EmployeeRulesExceptions.SocialSecNumberAlreadyExistsException))]
        public void NewSocialSecNumbersAreUnique()
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
                    DateOfBirth = new DateTime(1970, 12, 31),
                    SocialSecurityNo = 5235343
                };
                var directorTest2 = new Director
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    ManagerId = 1,
                    IsEmployed = true,
                    Salary = 10000,
                    Address = "Boschdijk",
                    DateOfBirth = new DateTime(1970, 12, 31),
                    SocialSecurityNo = 5235343 // the same SSN as above
                };
                Company.AddNewEmployee(directorTest1);
                Company.AddNewEmployee(directorTest2);
            

        }

        [TestMethod]
        [ExpectedException(typeof(EmployeeRulesExceptions.AnyEmployedPersonMustBeUnder80))]
        public void AnyEmployedPersonMustBeUnder80()
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
                DateOfBirth = new DateTime(1915, 12, 31), // older than 80 
                SocialSecurityNo = 5277743
            };
            Company.AddNewEmployee(directorTest1);
        }

    }
}
