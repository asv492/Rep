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
        //RosterTest: a list that stores all the employees
        public static List<Person> RosterTest = new List<Person>();
        //PersonQueue: a queue with new employees that have to be added in RosterTest
        public static Queue<Person> PersonQueue = new Queue<Person>();

        public delegate void RosterChangedEventHandler();
        //3 threads:
        public static Task AddToQueueTask = new Task(() => FillQueue(1));
        public static Task WatchQueueTask = new Task(() => QueueWatcher(2));
        public static Task SerializeRosterTask;
        public static DateTime NextStartTime = DateTime.Now + TimeSpan.FromMinutes(3);
        private static AutoResetEvent event_2 = new AutoResetEvent(false);

        public static void StartThreads()
        {
            //fill existing employees
            FillRoster();

            var roster = new Roster();
            var rosterWatcher = new RosterWatcher(roster);
            var action = new RosterChangedEventHandler(Roster.AddPerson);

            if (AddToQueueTask != null) AddToQueueTask.Start();

            System.Diagnostics.Debug.WriteLine("addToQueueTask started");
            WatchQueueTask.Start();
            System.Diagnostics.Debug.WriteLine("watchQueueTask started");

        }

        private static void QueueWatcher(int delayInSeconds)
        {
            while (NextStartTime > DateTime.Now)
            {
                if (PersonQueue.Count != 0)
                {
                    var ros = new Roster();
                    Roster.AddPerson();

                    Thread.Sleep(delayInSeconds * 1000);
                }
            }
        }

        public static void SerializeRoster()
        {
           // event_2.WaitOne();

            SerializeRosterTask = new Task(() =>
                    {
                        var typeOfExport = new XmlReadWrite();
                        var employeeData = new GenericEmployeeData(typeOfExport);
                        employeeData.WritePersonList(RosterTest);
                    });
            SerializeRosterTask.Start();
            System.Diagnostics.Debug.WriteLine("serializeTask started");

        }

        //public class SampleEventArgs
        //{
        //    public SampleEventArgs(string s) { Text = s; }
        //    public String Text { get; private set; } // readonly
        //}





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
            Thread.Sleep(delayInSeconds*1000);
        }
    }
    }
}
