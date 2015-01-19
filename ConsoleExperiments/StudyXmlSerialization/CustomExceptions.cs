using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class CustomExceptions
    {
        public class EmployedAndSalaryGreaterThanException : Exception
        {
            public EmployedAndSalaryGreaterThanException()
                : base("Any Employed Person Must Have A Salary Greater Than 100")
            {
                //throw new NotImplementedException();
            }
        }

        public class AnyEmployedPersonMustBeOver16YearsOldException : Exception
        {
            public AnyEmployedPersonMustBeOver16YearsOldException()
                : base("Any Employed Person Must Be Over 16 Years Old Exception")
            {
                
            }
        }
    }
}
