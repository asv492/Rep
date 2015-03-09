using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudyXmlSerialization
{
    public delegate void RosterChangedEventHandler(bool isChanged);

    class RosterWatcher
    {
        private bool _isChanged;
        public event RosterChangedEventHandler RosterChanged;

        public bool Status
        {
            set
            {
                _isChanged = value;
                RosterChanged(_isChanged);
            }
        }
    }

    public class ThreeThreads
    {
        //3 threads:
        private Task _watchQueueTask;
        private Task _serializeRosterTask;
        private Task _addToQueueTask;

        //RosterTest: a list that stores all the employees
        private List<Person> _rosterTest;

        //PersonQueue: a queue with new employees that have to be added in RosterTest
        private Queue<Person> _personQueue;

        //NextStartTime decides running and stopping the threads periodically
        private DateTime _nextStartTime;

        //AutoResetEvents for tasks syncronization
        private AutoResetEvent _writingToRosterEvent;
        private AutoResetEvent _serializingRosterEvent;

        private RosterWatcher _rosterChanged;


        public ThreeThreads()
        {
            _addToQueueTask = new Task(() => FillQueue(1));
            _watchQueueTask = new Task(() => QueueWatcher(2));

            _personQueue = new Queue<Person>();
            _rosterTest = new List<Person>();
            _nextStartTime = DateTime.Now + TimeSpan.FromMinutes(3);
            _writingToRosterEvent = new AutoResetEvent(false);
            _serializingRosterEvent = new AutoResetEvent(false);
            // _rosterChanged = new RosterWatcher();

            _rosterChanged = new RosterWatcher();
            _rosterChanged.RosterChanged += new RosterChangedEventHandler(SerializeRoster);
        }

        public void StartThreads()
        {

            //fill existing employees
            FillRoster();

            //var roster = new Roster();
            //var rosterWatcher = new RosterWatcher(roster);
           // SerializeRoster(true);


            if (_addToQueueTask != null) _addToQueueTask.Start();

            System.Diagnostics.Debug.WriteLine("addToQueueTask started");
            _watchQueueTask.Start();
            System.Diagnostics.Debug.WriteLine("watchQueueTask started");
           // var allTasks = new[] { _addToQueueTask, _watchQueueTask, _serializeRosterTask };
            //Task.Factory.ContinueWhenAll(allTasks, FinalWork);

        }

        private static void FinalWork(Task[] tasks)
        {
            if (tasks.All(t => t.Status == TaskStatus.RanToCompletion))
            {
                System.Diagnostics.Debug.WriteLine("All tasks ran to completion");
            }
        }

        private void QueueWatcher(int delayInSeconds)
        {
            while (_nextStartTime > DateTime.Now)
            {
                if (_personQueue.Count != 0)
                {
                    MovePersonFromQueueToRoster();

                    Thread.Sleep(delayInSeconds * 1000);
                }
            }
        }

        public void SerializeRoster(bool status)
        {
            

            _serializeRosterTask = new Task(() =>
            {
                var typeOfExport = new XmlReadWrite();
                var employeeData = new GenericEmployeeData(typeOfExport);
                employeeData.WritePersonList(_rosterTest);
            });
            System.Diagnostics.Debug.WriteLine("serializeTask status: " + _serializeRosterTask.Status);

            //_serializingRosterEvent.WaitOne();
            System.Diagnostics.Debug.WriteLine("_addToQueueTask status: " + _addToQueueTask.Status);
            System.Diagnostics.Debug.WriteLine("_watchQueueTask status: " + _watchQueueTask.Status);

            _serializeRosterTask.Start();

            //wait until this is finished - but how?
            _serializeRosterTask.Wait();
            System.Diagnostics.Debug.WriteLine("_addToQueueTask status: " + _addToQueueTask.Status);
            System.Diagnostics.Debug.WriteLine("_watchQueueTask status: " + _watchQueueTask.Status);
            _serializingRosterEvent.Set();
//
            //
            //
           // _serializingRosterEvent.Set();
            System.Diagnostics.Debug.WriteLine("_addToQueueTask status: " + _addToQueueTask.Status);
            System.Diagnostics.Debug.WriteLine("_watchQueueTask status: " + _watchQueueTask.Status);
            //_serializeRosterTask.ContinueWith(_serializingRosterEvent.Set())

            //_serializeRosterTask.Wait();
            System.Diagnostics.Debug.WriteLine("serializeTask status: " + _serializeRosterTask.Status);
            //while (true)//!_serializeRosterTask.IsCompleted)
            //{
            //    System.Diagnostics.Debug.WriteLine("serializeTask status: " + _serializeRosterTask.Status);
            //}
            //System.Diagnostics.Debug.WriteLine("serializeTask started");

        }

        private void FillRoster()
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            var personList = Company.EmployeeRoster;
            foreach (var person in personList)
            {
                _rosterTest.Add(person);
            }
        }

        public void FillQueue(int delayInSeconds)
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            var personList = Company.EmployeeRoster;
            foreach (var person in personList)
            {
                _personQueue.Enqueue(person);
                Thread.Sleep(delayInSeconds * 1000);
            }
        }


        public void MovePersonFromQueueToRoster()
        {
            var personToBeMoved = _personQueue.Dequeue();
            _rosterTest.Add(personToBeMoved);
            _serializingRosterEvent.WaitOne();
            _rosterChanged.Status = true;



        }



    }
}
