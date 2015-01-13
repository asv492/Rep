using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Exercise1
{
    [KnownType(typeof(Director))]
    [KnownType(typeof(Manager))]
    [KnownType(typeof(TeamLeader))]
    [KnownType(typeof(Engineer))]
    [DataContract]
    [XmlInclude(typeof(Director)), XmlInclude(typeof(Manager)),
        XmlInclude(typeof(TeamLeader)), XmlInclude(typeof(Engineer))]
    public class Person //: ISerializable
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public int SocialSecurityNo { get; set; }

        [DataMember]
        public bool IsEmployed { get; set; }

        [DataMember]
        public int ManagerId { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public int Salary { get; set; }


        //public int CalculateAge()
        //{
        //    //throw new System.NotImplementedException();
        //}

        //public int CalculateBonus()
        //{
        //   // throw new System.NotImplementedException();
        //}







        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", this.Id);
            info.AddValue("LastName", this.LastName);
            info.AddValue("FirstName", this.FirstName);
            info.AddValue("DateOfBirth", this.DateOfBirth);
            info.AddValue("SocialSecurityNo", this.SocialSecurityNo);
            info.AddValue("IsEmployed", this.IsEmployed);
            info.AddValue("ManagerId", this.ManagerId);
            info.AddValue("Address", this.Address);
            info.AddValue("Salary", this.Salary);

        }
    }
}
