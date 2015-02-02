using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class CsvExportTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Company.EmployeeRoster.Clear();
            Company.FillEmployeeRoster();
            var csv = new CsvExport<Person>(Company.EmployeeRoster);
           csv.ExportToFile(@"C:\Users\q\Documents\CountWords\exportToCSV.csv");
        }
    }
}
