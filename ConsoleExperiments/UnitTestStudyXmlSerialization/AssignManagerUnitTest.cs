using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]



    public class AssignManagerUnitTest
    {

 

        [TestMethod]
        public void NewEmployeeIsAssignedToTheFirstTeamLederWithLessThan3Employees()
        {
            Company.FillEmployeeRoster();
            var engineer1 = new Engineer
            {
                Id = 88,
                FirstName = "Mark",
                LastName = "Doe",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1975, 12, 31), 
                SocialSecurityNo = 3323443
            };
            Company.AddNewEmployee(engineer1);
            Assert.AreEqual(5, Company.EmployeeRoster[10].ManagerId);

        }


        [TestMethod]
        public void TeamLeaderHasAlready3Employees_NewEmployeeAssignedToNextTeamLeader()
        {
            Company.FillEmployeeRoster();
            var engineer1 = new Engineer
            {
                Id = 33,
                FirstName = "Mark",
                LastName = "Doe",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1975, 12, 31),
                SocialSecurityNo = 3377743
            };

            var engineer2 = new Engineer
            {
                Id = 77,
                FirstName = "Bob",
                LastName = "Huskins",
                ManagerId = 1,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1975, 12, 31),
                SocialSecurityNo = 3322243
            };
            Company.AddNewEmployee(engineer1);
            Company.AddNewEmployee(engineer2);

            Assert.AreEqual(6, Company.EmployeeRoster[11].ManagerId);

        }
        [TestMethod]
        public void ManagerHasAlready2TeamLeader_NewTeamLeaderAssignedToNextManager()
        {
            Company.FillEmployeeRoster();
            var teamLeader1 = new TeamLeader
            {
                Id = 100,
                FirstName = "Mark",
                LastName = "Doe",
                ManagerId = 0,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1975, 12, 31),
                SocialSecurityNo = 3376743
            };

            var teamLeader2 = new TeamLeader
            {
                Id = 200,
                FirstName = "Bob",
                LastName = "Huskins",
                ManagerId = 0,
                IsEmployed = true,
                Salary = 10000,
                Address = "Boschdijk",
                DateOfBirth = new DateTime(1975, 12, 31),
                SocialSecurityNo = 3322113
            };
            Company.AddNewEmployee(teamLeader1);
            Company.AddNewEmployee(teamLeader2);

            Assert.AreEqual(4, Company.EmployeeRoster[11].ManagerId);

        }

    }
}
