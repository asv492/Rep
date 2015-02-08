using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class CsvReadWrite : IDataInterfaceReadWrite
    {
        private static string _path = @"C:\Users\q\Documents\CountWords\exportToCSV.csv";
        public List<Person> ReadPersonList()
        {
            List<Person> personList = CsvImport.GetPersonList(_path);
            return personList;
        }

        public void WritePersonList(List<Person> personList)
        {
            var csv = new CsvExport<Person>(personList);
            csv.ExportToFile(_path);
        }
    }
}
