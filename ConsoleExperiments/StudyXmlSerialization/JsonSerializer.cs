using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class JsonSerializer : IDocumentSerializer
    {

        public bool Serialize(Document document)
        {
            //MemoryStream stream = new MemoryStream();
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));

            var stream = new MemoryStream();

            var serJson = new DataContractJsonSerializer(typeof(Person));

            foreach (var listEmployee in Company.EmployeeRoster)
            {
                serJson.WriteObject(stream, listEmployee);
            }
            string json = Encoding.UTF8.GetString(stream.ToArray());
            File.WriteAllText(@"C:\Users\q\Documents\CountWords\serializareJSON.json", json);

            stream.Position = 0;

            throw new System.NotImplementedException();

        }

        public string Deserialize(Document document)
        {
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            //string jsonText = File.ReadAllText(@"C:\Users\q\Documents\CountWords\serializareJSON.json");
            //MemoryStream stream = new MemoryStream();

            //StreamWriter writer = new StreamWriter(stream);
            //writer.Write(jsonText);
            //writer.Flush();
            //stream.Position = 0;
            //var personList = new List<Person>();
            //using (stream)
            //{
            //    personList.Add((Person)ser.ReadObject(stream));
            //}





            throw new System.NotImplementedException();
        }


    }
}
