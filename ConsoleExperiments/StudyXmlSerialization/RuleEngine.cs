using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class RuleEngine
    {
 


        public static bool EmployedAndAgeGreaterThan(Person pers)
        {
            var age = CalculateAge(pers);

            if (pers.IsEmployed && age > 16)
            {
                return true;
            }
            else
            {
                throw new EmployeeRulesExceptions.AnyEmployedPersonMustBeOver16YearsOldException();
                //return false;
            }
        }

        public static bool EmployedAndSalaryGreaterThan(Person pers)
        {
            if (pers.IsEmployed && pers.Salary > 100)
            {
                return true;
            }
            else
            {
                throw new EmployeeRulesExceptions.EmployedAndSalaryGreaterThanException();
                //return false;
            }
        }

        public static bool IsSocialSecNumberUnique(Person pers)
        {
            int ssn = pers.SocialSecurityNo;
            if (Company.EmployeeRoster.Any(person => ssn == person.SocialSecurityNo))
            {
                throw new EmployeeRulesExceptions.SocialSecNumberAlreadyExistsException();
            }

            return true;
        }

        public static bool IsUnder80(Person pers)
        {
            var age = CalculateAge(pers);

            if (pers.IsEmployed && age < 80)
            {
                return true;
            }
            else
            {
                throw new EmployeeRulesExceptions.AnyEmployedPersonMustBeUnder80();
                //return false;
            }
        }
        private static int CalculateAge(Person pers)
        {
            var birthday = pers.DateOfBirth;
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;
            return age;
        }
    }
}
