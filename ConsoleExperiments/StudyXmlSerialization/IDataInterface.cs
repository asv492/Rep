using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public interface IDataInterface
    {
        int Add(Person person);
        Person Retrieve(int personId);
        bool Update(Person person);
        bool MarkAsNotEmployed(Person person);

    }
}
