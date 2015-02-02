using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class CsvImport
    {

        //public string Path { get; set; }

        //public CsvImport(string path)
        //{
        //    Path = path;
        //}

        public static List<Person> GetPersonList(string path)
        {
            var personList = new List<Person>();
            var valuesFromOneLine = new List<string>();
            var reader = new StreamReader(File.OpenRead(path));
            var headerNames  = new List<string>();
            
               //read the first line which should contain headers
                     var headerString = reader.ReadLine();
                if (headerString != null)
                {
                    var headersArray = headerString.Split(',');
                    foreach (var header in headersArray)
                    {
                        headerNames.Add(header);
                    }
                }

            while (!reader.EndOfStream)
            {

                //read the rest of lines which contain values
                var line = reader.ReadLine();
                if (line != null)
                {
                    var values = line.Split(',');
                    foreach (var value in values)
                    {
                        valuesFromOneLine.Add(value);
                    }
                    var person = new Person();

                    person.FirstName = valuesFromOneLine[2];
                    person.SocialSecurityNo = Convert.ToInt32(valuesFromOneLine[4]);
                    personList.Add(person);
                }
            }
            return personList;
        }
    }
}
