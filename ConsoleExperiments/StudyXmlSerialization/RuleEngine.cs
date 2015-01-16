using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class RuleEngine
    {
        public Director DirectorObject { get; set; }

        public static bool CheckRules(Director dir)
        {
            return (EmployedAndSalaryGreaterThan(dir) && EmployedAndAgeGreaterThan(dir));
        }

        public static bool EmployedAndAgeGreaterThan(Director dir)
        {

            var birthday = dir.DateOfBirth;
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;

            if (dir.IsEmployed && age > 16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool EmployedAndSalaryGreaterThan(Director dir)
        {
            if (dir.IsEmployed && dir.Salary > 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
