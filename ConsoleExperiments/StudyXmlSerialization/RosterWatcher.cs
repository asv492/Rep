using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class RosterWatcher
    {
        private Roster _roster;

        public RosterWatcher(Roster roster)
        {
            _roster = roster;
            Roster.RosterChanged += ThreeThreads.SerializeRoster;
        }


    }
}
