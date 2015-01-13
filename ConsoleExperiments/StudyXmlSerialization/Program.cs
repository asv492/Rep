using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
    class Program
    {
        static void Main(string[] args)
        {

            var listEmployees = new List<Person>();
            listEmployees.Add(new Director { Id = 1, FirstName = "John", LastName = "Smith", ManagerId = 1, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Director { Id = 2, FirstName = "Alice", LastName = "Doe", ManagerId = 1, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Manager { Id = 3, FirstName = "Bob", LastName = "Doe", ManagerId = 2, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Manager { Id = 4, FirstName = "Mike", LastName = "Doe", ManagerId = 1, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new TeamLeader { Id = 5, FirstName = "Joe", LastName = "Doe", ManagerId = 3, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new TeamLeader { Id = 6, FirstName = "Stan", LastName = "Doe", ManagerId = 4, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Engineer { Id = 7, FirstName = "Ike", LastName = "Doe", ManagerId = 5, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Engineer { Id = 8, FirstName = "Luke", LastName = "Doe", ManagerId = 5, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Engineer { Id = 9, FirstName = "Bee", LastName = "Doe", ManagerId = 6, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
            listEmployees.Add(new Engineer { Id = 10, FirstName = "Sue", LastName = "Doe", ManagerId = 6, IsEmployed = true, Salary = 1000, Address = "Boschdijk", DateOfBirth = new DateTime(1970, 12, 31), SocialSecurityNo = 5235343 });
             //

            //XML serialization
            var ser = new XmlSerializer(listEmployees.GetType());
            using (var fs = new System.IO.FileStream(@"C:\Users\q\Documents\CountWords\serializareXMLtest1.xml", System.IO.FileMode.Create))
            {
                ser.Serialize(fs, listEmployees);
            }
            //var serJson = new DataContractJsonSerializer(listEmployees.GetType());

            var stream = new MemoryStream();

            var serJson = new DataContractJsonSerializer(typeof(Person));

            foreach (var listEmployee in listEmployees)
            {
                serJson.WriteObject(stream, listEmployee);
            }
            string json = Encoding.UTF8.GetString(stream.ToArray());
            File.WriteAllText(@"C:\Users\q\Documents\CountWords\serializareJSON.json", json);

            stream.Position = 0;
            var sr = new StreamReader(stream);

            Console.WriteLine(sr.ReadToEnd());

            Console.WriteLine();
            Console.ReadLine();





            // serJson.WriteObject(stream, listEmployees);

            //using (var fs = new System.IO.FileStream(@"C:\Users\q\Documents\CountWords\serializareJSON.json", System.IO.FileMode.Create))
            //{
            //    serJson.WriteObject(fs, listEmployees);
            //}
            ////string json = Encoding.UTF8.GetString(stream.ToArray()); File.WriteAllText(filePath, json.IndentJSON());
            //var textToBeWritten = serJson.ToString();
            //File.WriteAllText(@"C:\Users\q\Documents\CountWords\serializareJSON.json", textToBeWritten);

            //   JavaScriptSerializer

            //   System.Web.Script.Serialization.JavaScriptSerializer oSerializer =
            //new System.Web.Script.Serialization.JavaScriptSerializer();
            //   string sJSON = oSerializer.Serialize(oList);
            //string json = JsonConvert.SerializeObject(user, Formatting.Indented); 
            //File.WriteAllText(@"c:\user.json", json);

        }
    }
}
