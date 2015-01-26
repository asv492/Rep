using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyXmlSerialization;

namespace StudyXmlSerialization
{
    public class Company

    {
        public Person PersonObject { get; set; }

        public Company(Person personObject)
        {
            PersonObject = personObject;    
        }

        public static List<Person> EmployeeRoster = new List<Person>();
        public static Dictionary<int,int> TeamSizes = new Dictionary<int, int>();

        public static void FillEmployeeRoster()

        {
            EmployeeRoster.Add(new Director { Id = 1,  FirstName = "John", LastName = "Smith", ManagerId = 1, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 1235343 });
            EmployeeRoster.Add(new Director { Id = 2, FirstName = "Alice", LastName = "Doe", ManagerId = 1, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 2235343 });
            EmployeeRoster.Add(new Manager { Id = 3, FirstName = "Bob", LastName = "Doe", ManagerId = 2, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 3235343 });
            EmployeeRoster.Add(new Manager { Id = 4, FirstName = "Mike", LastName = "Doe", ManagerId = 1, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 4235343 });
            EmployeeRoster.Add(new TeamLeader { Id = 5, FirstName = "Joe", LastName = "Doe", ManagerId = 3, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            EmployeeRoster.Add(new TeamLeader { Id = 6, FirstName = "Stan", LastName = "Doe", ManagerId = 4, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 6235343 });
            EmployeeRoster.Add(new Engineer { Id = 7, FirstName = "Ike", LastName = "Doe", ManagerId = 5, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 7235343 });
            EmployeeRoster.Add(new Engineer { Id = 8, FirstName = "Luke", LastName = "Doe", ManagerId = 5, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 8235343 });
            EmployeeRoster.Add(new Engineer { Id = 9, FirstName = "Bee", LastName = "Doe", ManagerId = 6, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 9235343 });
            EmployeeRoster.Add(new Engineer { Id = 10, FirstName = "Sue", LastName = "Doe", ManagerId = 6, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 1035343 });
        }

        //public static void SetMaxTeamSize(Director director , int maxTeamSize)
        //{
        //    director.MaxTeamSize = maxTeamSize;
        //}
        //public static void SetMaxTeamSize(Person   person)
        //{
        //    if (person.GetType() == typeof (Director))
        //    {
        //        person.
        //    }
        //}

        //public static void SetCurrentTeamSizes()
        //{
        //    foreach (Director director in EmployeeRoster)
        //    {
        //        //var managedTeamSize = EmployeeRoster.Count(currentPerson => director.Id == currentPerson.ManagerId);
        //        var managedTeamSize = 0;
        //        foreach (var currentPerson in EmployeeRoster)
        //        {
        //            if (director.Id == currentPerson.ManagerId)
        //            {
        //                managedTeamSize++;
        //            }

        //        }
        //        director.ActualTeamSize = managedTeamSize;
        //    }
        //    foreach (Manager manager in EmployeeRoster)
        //    {
        //        var managedTeamSize = 0;
        //        foreach (var currentPerson in EmployeeRoster)
        //        {
        //            if (manager.Id == currentPerson.ManagerId)
        //            {
        //                managedTeamSize++;
        //            }

        //        }
        //        manager.ActualTeamSize = managedTeamSize;
        //    }
        //    foreach (TeamLeader teamLider in EmployeeRoster)
        //    {
        //        var managedTeamSize = 0;
        //        foreach (var currentPerson in EmployeeRoster)
        //        {
        //            if (teamLider.Id == currentPerson.ManagerId)
        //            {
        //                managedTeamSize++;
        //            }

        //        }
        //        teamLider.ActualTeamSize = managedTeamSize;
        //    }
            
        //}

        public static void SetCurrentTeamSizes()
        {
            for (int i = 0; i < EmployeeRoster.Count; i++)
            {
                if (EmployeeRoster[i] is ILeaderInterface)
                {
                    int currentTeamSize = 0;
                    for (int j = 0; j < EmployeeRoster.Count; j++)
                    {
                        if (EmployeeRoster[j].ManagerId == EmployeeRoster[i].Id)
                        {
                            currentTeamSize++;
                        }
                    }
                    //(ILeaderInterface) EmployeeRoster[i].ActualTeamSize = currentTeamSize;
                    //SetTeamSize((ILeaderInterface) EmployeeRoster[i], currentTeamSize);
                    TeamSizes.Add(EmployeeRoster[i].Id, currentTeamSize);
                }
            }
        }

        private static void SetTeamSize(ILeaderInterface leader, int currentTeamSize)
        {
            leader.ActualTeamSize = currentTeamSize;
        }

        public static void AddNewEmployee(Person pers)

        {

            if (CheckRules(pers))
            {
                GenerateEmailUsername(pers);
                TeamSizes.Clear();
                SetCurrentTeamSizes();
                pers.ManagerId = GetAvailableManagerId(pers);
                EmployeeRoster.Add(pers);

            }
        }

        private static void GenerateEmailUsername(Person pers)
        {
            string emailString = pers.FirstName + "." + pers.LastName;
            string emailStringIfNotUnique = emailString;
            int emailStringSuffix = 1;

            while (!UsernameIsUnique(emailStringIfNotUnique))
            {
                emailStringIfNotUnique = emailString + emailStringSuffix;
                emailStringSuffix++;
            }
            // i'm using .Address to store email username 
            pers.Address = emailStringIfNotUnique;

        }

        private static bool UsernameIsUnique(string emailString)
        {
            //return EmployeeRoster.All(pers => emailString != pers.Address);
            foreach (Person pers in EmployeeRoster)
            {
                if (emailString == pers.Address)
                {
                    return false;
                }

            }
            return true;

        }

        private static bool CheckRules(Person pers)
        {
            var rule1 = RuleEngine.EmployedAndAgeGreaterThan(pers);

            var rule2 = RuleEngine.EmployedAndSalaryGreaterThan(pers);

            var rule3 = RuleEngine.IsSocialSecNumberUnique(pers);

            var rule4 = RuleEngine.IsUnder80(pers);


            return (rule1 && rule2 && rule3 && rule4);
        }

        public static void FireEmployee(Person pers)
        {
            pers.IsEmployed = false;
            pers.Address = "";
            pers.Salary = 0;
        }
        public static int GetAvailableManagerId(Person newlyHiredPerson)
        {
            if (newlyHiredPerson is Engineer)
            {
                foreach (var existingEmployee  in EmployeeRoster)
                {
                    if (existingEmployee is TeamLeader)
                    {
                        if (TeamSizes[existingEmployee.Id] < TeamLeader.MaxTeamSize)
                        {
                            return existingEmployee.Id;
                        }
                    }
                }
                throw new CustomExceptions.AllManagersHaveMaximumEmployeesInTheirTeams();
            }
            else if (newlyHiredPerson is TeamLeader)
            {
                foreach (var existingEmployee in EmployeeRoster)
                {
                    if (existingEmployee is Manager)
                    {
                        if (TeamSizes[existingEmployee.Id] < Manager.MaxTeamSize)
                        {
                            return existingEmployee.Id;
                        }
                    }
                }
                throw new CustomExceptions.AllManagersHaveMaximumEmployeesInTheirTeams();
            }
            else if (newlyHiredPerson is Manager)
            {
                foreach (var existingEmployee in EmployeeRoster)
                {
                    if (existingEmployee is Director)
                    {
                        if (TeamSizes[existingEmployee.Id] < Director.MaxTeamSize)
                        {
                            return existingEmployee.Id;
                        }
                    }
                }
            }
            throw new CustomExceptions.AllManagersHaveMaximumEmployeesInTheirTeams();
        }

    }
}
