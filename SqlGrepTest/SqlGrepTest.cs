using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SqlGrepTest {

    [TestClass]
    public class SqlGrepTest {

        [TestMethod]
        public void TestMethod1() {
            SqlGrep.NamePicker np = new SqlGrep.NamePicker();
            np.Read(@"./names.txt");
            SqlGrep.PatternPicker pp = new SqlGrep.PatternPicker();
            pp.Read(@"./patterns.txt");
            SqlGrep.Triggers trs = new SqlGrep.Triggers();
            trs.PatternPicker = pp;
            trs.NamePicker = np;
            trs.scan(@"./Test");
            Console.WriteLine(@"test");
        }
    }
}