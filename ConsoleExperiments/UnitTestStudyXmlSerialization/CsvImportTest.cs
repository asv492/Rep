using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudyXmlSerialization
{
    [TestClass]
    public class CsvImportTest
    {
        [TestMethod]
        public void CsvImportTestMethod1()
        {
            CsvImport.GetPersonList(@"C:\Users\q\Documents\CountWords\exportToCSV.csv");
            // CollectionAssert.AreEqual(expected, actual);
        }
    }
}
