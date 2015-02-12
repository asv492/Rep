using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public class ThreeThreads
    {
        public static Queue<Person> PersonQueue = new Queue<Person>();
        public static List<Person> RosterTest = new List<Person>();
        public delegate void RosterChangedEventHandler();


        public static void StartThreads()
        {
            //fill existing employees
            FillRoster();

            var roster = new Roster();
            var rosterWatcher = new RosterWatcher(roster);
            var action = new RosterChangedEventHandler(Roster.AddPerson);
            //3 threads
            var addToQueueTask = new Task(() => FillQueue(1));

            addToQueueTask.Start();
            System.Diagnostics.Debug.WriteLine("addToQueueTask started");
            var watchQueueTask = new Task(() => QueueWatcher(2));
            watchQueueTask.Start();
            System.Diagnostics.Debug.WriteLine("watchQueueTask started");

        }

        private static void QueueWatcher(int delayInSeconds)
        {
            while (true)
            {
                if (PersonQueue.Count != 0)
                {
                    var ros = new Roster();
                    Roster.AddPerson();

                    Thread.Sleep(delayInSeconds * 1000);
                }
            }
        }

        private static void SerializeRoster()
        {
            var serializeTask = new Task(() =>
                    {
                        var typeOfExport = new XmlReadWrite();
                        var employeeData = new GenericEmployeeData(typeOfExport);
                        employeeData.WritePersonList(RosterTest);
                    });
            serializeTask.Start();
            System.Diagnostics.Debug.WriteLine("serializeTask started");

        }
        public class RosterWatcher
        {
            private Roster _roster;

            public RosterWatcher(Roster roster)
            {
                _roster = roster;
                Roster.RosterChanged += SerializeRoster; 
            }


        }
        public class SampleEventArgs
        {
            public SampleEventArgs(string s) { Text = s; }
            public String Text { get; private set; } // readonly
        }

        public class Roster
        {
            
            public static event RosterChangedEventHandler RosterChanged;



            public static void AddPerson()
            {
                var personToBeMoved = PersonQueue.Dequeue();
                RosterTest.Add(personToBeMoved);

                if (RosterChanged != null)
                {
                    RosterChanged();
                }
                
            }


        }



        private static void FillRoster()
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            var personList = Company.EmployeeRoster;
            foreach (var person in personList)
            {
                RosterTest.Add(person);
            }
        }

        public static void FillQueue(int delayInSeconds)
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            var personList = Company.EmployeeRoster;
            foreach (var person in personList)
            {
                PersonQueue.Enqueue(person);
                Thread.Sleep(delayInSeconds * 1000);
            }
        }
    }
}
