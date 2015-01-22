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
        public Company(Director directorObject)
        {
            DirectorObject = directorObject;    
        }
        public static List<Person> EmployeeRoster = new List<Person>();

        public Director DirectorObject { get; set; }

        public static void AddNewEmployee(Director dir)

        {

            if (CheckRules(dir))
            {
                GenerateEmailUsername(dir);
                EmployeeRoster.Add(dir);

            }
        }

        private static void GenerateEmailUsername(Director dir)
        {
            string emailString = dir.FirstName + "." + dir.LastName;
            string emailStringIfNotUnique = emailString;
            int emailStringSuffix = 1;

            while (!UsernameIsUnique(emailStringIfNotUnique))
            {
                emailStringIfNotUnique = emailString + emailStringSuffix;
                emailStringSuffix++;
            }
            // i'm using .Address to store email username 
            dir.Address = emailStringIfNotUnique;

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

        private static bool CheckRules(Director dir)
        {
            var rule1 = RuleEngine.EmployedAndAgeGreaterThan(dir);

            var rule2 = RuleEngine.EmployedAndSalaryGreaterThan(dir);

            var rule3 = RuleEngine.IsSocialSecNumberUnique(dir);

            var rule4 = RuleEngine.IsUnder80(dir);


            return (rule1 && rule2 && rule3 && rule4);
        }

        public static void FireEmployee(Director dir)
        {
            dir.IsEmployed = false;
            dir.Address = "";
            dir.Salary = 0;
        }


    }
}
