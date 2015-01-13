using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
    [XmlInclude(typeof(Director))]
    public class Director : Person
    {

    }
    [XmlInclude(typeof(Manager))]
    public class Manager : Person
    {

    }
    [XmlInclude(typeof(TeamLeader))]
    public class TeamLeader : Person
    {
    }
    [XmlInclude(typeof(Engineer))]
    public class Engineer : Person
    {

    }

}
