using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace StudyXmlSerialization
{
    [XmlInclude(typeof(Director))]
    public class Director : Person, ILeaderInterface
    {
        public Director(int maxTeamSize)
        {
            MaxTeamSize = 3;
        }

        public Director()
        {
            MaxTeamSize = 3;

        }

        public static int MaxTeamSize { get; set; }
        int ILeaderInterface.ActualTeamSize
        {
            get { return ActualTeamSize; }
            set { ActualTeamSize = value; }
        }

        int ILeaderInterface.MaxTeamSize
        {
            get { return MaxTeamSize; }
            set { MaxTeamSize = value; }
        }

        public int ActualTeamSize { get; set; }

        
    }
    [XmlInclude(typeof(Manager))]
    public class Manager : Person, ILeaderInterface
    {
        public Manager(int maxTeamSize)
        {
            MaxTeamSize = 2;
        }

        public Manager()
        {
            MaxTeamSize = 2;

        }

        public static int MaxTeamSize { get; set; }
        int ILeaderInterface.ActualTeamSize
        {
            get { return ActualTeamSize; }
            set { ActualTeamSize = value; }
        }

        int ILeaderInterface.MaxTeamSize
        {
            get { return MaxTeamSize; }
            set { MaxTeamSize = value; }
        }

        public int ActualTeamSize { get; set; }

        
    }
    [XmlInclude(typeof(TeamLeader))]
    public class TeamLeader : Person, ILeaderInterface
    {
        public TeamLeader(int maxTeamSize)
        {
            MaxTeamSize = 3;
        }

        public TeamLeader()
        {
            MaxTeamSize = 3;
        }

        public static int MaxTeamSize { get; set; }
        int ILeaderInterface.ActualTeamSize
        {
            get { return ActualTeamSize; }
            set { ActualTeamSize = value; }
        }

        int ILeaderInterface.MaxTeamSize
        {
            get { return MaxTeamSize; }
            set { MaxTeamSize = value; }
        }

        public int ActualTeamSize { get; set; }

        
    }
    [XmlInclude(typeof(Engineer))]
    public class Engineer : Person
    {

    }

}
