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

            var birthday = pers.DateOfBirth;
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;

            if (pers.IsEmployed && age > 16)
            {
                return true;
            }
            else
            {
                throw new CustomExceptions.AnyEmployedPersonMustBeOver16YearsOldException();
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
                throw new CustomExceptions.EmployedAndSalaryGreaterThanException();
                //return false;
            }
        }

        public static bool IsSocialSecNumberUnique(Person pers)
        {
            int ssn = pers.SocialSecurityNo;
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

        public static bool IsUnder80(Person pers)
        {
            var birthday = pers.DateOfBirth;
            DateTime today = DateTime.Today;
            int age = today.Year - birthday.Year;
            if (birthday > today.AddYears(-age)) age--;

            if (pers.IsEmployed && age < 80)
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
