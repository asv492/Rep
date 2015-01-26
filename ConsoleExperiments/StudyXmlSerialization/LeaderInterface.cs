using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    interface ILeaderInterface
    {
        int MaxTeamSize { get;  set; }
        int ActualTeamSize { get; set; }

    }
}
