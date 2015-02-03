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
                    //Id	LastName	FirstName	DateOfBirth	SocialSecurityNo	IsEmployed	ManagerId	Address	Salary
                    person.Id = Convert.ToInt32(valuesFromOneLine[0]);
                    person.LastName = valuesFromOneLine[1];
                    person.FirstName = valuesFromOneLine[2];
                    person.DateOfBirth = Convert.ToDateTime(valuesFromOneLine[3]);
                    person.SocialSecurityNo = Convert.ToInt32(valuesFromOneLine[4]);
                    person.IsEmployed = Convert.ToBoolean(valuesFromOneLine[5]);
                    person.ManagerId = Convert.ToInt32(valuesFromOneLine[6]);
                    person.Address = valuesFromOneLine[7];
                    person.Salary = Convert.ToInt32(valuesFromOneLine[8]);
                    valuesFromOneLine.Clear();
                    //TODO Converter<Person, Director>(person, person); ???
                    personList.Add(person);
                }
            }
            reader.Close();
            return personList;
        }
    }
}
