using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public interface IDataInterfaceReadWrite
    {
        List<Person> ReadPersonList();
        void WritePersonList(List<Person> personList);
    }
}
