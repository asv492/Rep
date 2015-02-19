using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class Roster
    {
        public static event ThreeThreads.RosterChangedEventHandler RosterChanged;
        public static void AddPerson()
        {
            var personToBeMoved = ThreeThreads.PersonQueue.Dequeue();
            ThreeThreads.RosterTest.Add(personToBeMoved);

            if (RosterChanged != null)
            {
                RosterChanged();
            }

        }
    }
}
