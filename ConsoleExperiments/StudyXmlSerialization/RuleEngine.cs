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
