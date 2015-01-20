using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class RuleEngine
    {
 
        //public Director DirectorObject { get; set; }

        //public static bool CheckRules(Director dir)
        //{
        //    return (EmployedAndSalaryGreaterThan(dir) && EmployedAndAgeGreaterThan(dir));
        //}

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
                throw new CustomExceptions.AnyEmployedPersonMustBeOver16YearsOldException();
                //return false;
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
                throw new CustomExceptions.EmployedAndSalaryGreaterThanException();
                //return false;
            }
        }

        public static bool IsSocialSecNumberUnique(Director dir)
        {
            int ssn = dir.SocialSecurityNo;
            //if (Company.EmployeeRoster.Any(person => ssn == person.SocialSecurityNo))
            foreach (Person person in Company.EmployeeRoster)
            {
                if (ssn == person.SocialSecurityNo)
                {
                    throw new CustomExceptions.SocialSecNumberAlreadyExistsException();
                }
            }

            return true;
        }

        public static bool IsUnder80(Director dir)
        {
            var birthday = dir.DateOfBirth;
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;

            if (dir.IsEmployed && age < 80)
            {
                return true;
            }
            else
            {
                throw new CustomExceptions.AnyEmployedPersonMustBeUnder80();
                //return false;
            }
        }
    }
}
