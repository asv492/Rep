using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyXmlSerialization;

namespace UnitTestStudyXmlSerialization
{
    class Company
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
                EmployeeRoster.Add(dir);

            }
        }

        private static bool CheckRules(Director dir)
        {
            var rule1 = RuleEngine.EmployedAndAgeGreaterThan(dir);

            var rule2 = RuleEngine.EmployedAndSalaryGreaterThan(dir);

            return (rule1 && rule2);
        }



    }
}
