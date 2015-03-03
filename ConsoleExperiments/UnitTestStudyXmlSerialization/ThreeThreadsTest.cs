using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class ThreeThreadsTest
    {

/*
Thread1: add an employee to a brand new list (queue) one by one 
        with a gap of 1sec (by reading from a file? or auto generating)
               
Thread2: remove an employee from that "brand new list", 
        and insert it into "our list based db and rules"
         
Thread3: whenever a change is detected in our "list based db", serialize it
*/



        //[TestMethod]
        //public void TestQueueIsFilledWithPersons()
        //{

        //ThreeThreads.StartThreads();
        //    Console.WriteLine(ThreeThreads.PersonQueue);
        //}
        //[TestMethod]
        //public void TestQueueIsFilledWithOnePersonEachSecond()
        //{

        //    ThreeThreads.StartThreads();
        //    Console.WriteLine(ThreeThreads.PersonQueue);
        //}
        //[TestMethod]
        //public void ThreadsAreStarting()
        //{
        //    Console.WriteLine("Start of test");

        //    ThreeThreads.StartThreads();
        //    Console.ReadLine();
        //}
        [TestMethod]
        public void TestRemoveStatic()
        {
            var tt = new ThreeThreads();
            tt.StartThreads();

        }
    }


    public class ThreadsExperiments
    {
        
    }
}
