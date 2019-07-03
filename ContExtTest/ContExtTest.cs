using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ContExtTest {

    [TestClass]
    public class ContExtTest {

        [TestMethod]
        public void TestMethod1() {
            ContExt c = new ContExt();
            c.Delimiter = "\t";
            c.Encoding = "Shift-JIS";
            c.Init(@"./patterns.txt", false);
            c.Scan(@"./Test1");

            IList<Match> ret = c.Hit;

            Assert.AreEqual(6, ret.Count);

            const string F1 = @"a1.txt";
            const string F2 = @"ab1.txt";
            const string F3 = @"ac1.txt";

            const string F1L1 = @"a1.txt";
            const string F1L2 = @"elit, sed do eiusmod tempor incididunt ut labore et";
            const string F1L3 = @"dolore magna aliqua. Ut enim ad minim veniam, quis";
            const string F1L4 = @"contains";
            const string F1L5 = @"abc";
            const string F1L6 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F1L7 = @"eu fugiat nulla pariatur. Excepteur sint occaecat";
            const string F1L8 = @"def";

            Assert.AreEqual(F1, System.IO.Path.GetFileName(ret[0].Path));
            Assert.AreEqual(F1, System.IO.Path.GetFileName(ret[1].Path));

            Assert.AreEqual(1, ret[0].Start);
            Assert.AreEqual(7, ret[1].Start);

            Assert.AreEqual(F1L1, ret[0].Hit[0]);
            Assert.AreEqual(F1L2, ret[0].Hit[1]);
            Assert.AreEqual(F1L3, ret[0].Hit[2]);
            Assert.AreEqual(F1L4, ret[0].Hit[3]);
            Assert.AreEqual(F1L5, ret[1].Hit[0]);
            Assert.AreEqual(F1L6, ret[1].Hit[1]);
            Assert.AreEqual(F1L7, ret[1].Hit[2]);
            Assert.AreEqual(F1L8, ret[1].Hit[3]);

            const string F2L1 = @"ab1.txt contains";
            const string F2L2 = @"fgh";
            const string F2L3 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F2L4 = @"ijk";

            Assert.AreEqual(F2, System.IO.Path.GetFileName(ret[2].Path));
            Assert.AreEqual(F2, System.IO.Path.GetFileName(ret[3].Path));

            Assert.AreEqual(3, ret[2].Start);
            Assert.AreEqual(6, ret[3].Start);

            Assert.AreEqual(F2L1, ret[2].Hit[0]);
            Assert.AreEqual(F2L2, ret[3].Hit[0]);
            Assert.AreEqual(F2L3, ret[3].Hit[1]);
            Assert.AreEqual(F2L4, ret[3].Hit[2]);

            const string F3L1 = @"dolore magna ac1.txt aliqua. Ut enim ad minim veniam, quis";
            const string F3L2 = @"nostrud exercitation ullamco laboris nisi ut aliquip";
            const string F3L3 = @"ex ea commodo klm consequat. Duis aute irure dolor in";
            const string F3L4 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F3L5 = @"eu fugiat contains nulla pariatur. Excepteur sint occaecat";
            const string F3L6 = @"ex ea commodo klm consequat. Duis aute irure dolor in";
            const string F3L7 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F3L8 = @"eu fugiat contains nulla pariatur. Excepteur sint occaecat";
            const string F3L9 = @"cupidatat non opq proident, sunt in culpa qui officia";

            Assert.AreEqual(F3, System.IO.Path.GetFileName(ret[4].Path));
            Assert.AreEqual(F3, System.IO.Path.GetFileName(ret[5].Path));

            Assert.AreEqual(2, ret[4].Start);
            Assert.AreEqual(4, ret[5].Start);

            Assert.AreEqual(F3L1, ret[4].Hit[0]);
            Assert.AreEqual(F3L2, ret[4].Hit[1]);
            Assert.AreEqual(F3L3, ret[4].Hit[2]);
            Assert.AreEqual(F3L4, ret[4].Hit[3]);
            Assert.AreEqual(F3L5, ret[4].Hit[4]);
            Assert.AreEqual(F3L6, ret[5].Hit[0]);
            Assert.AreEqual(F3L7, ret[5].Hit[1]);
            Assert.AreEqual(F3L8, ret[5].Hit[2]);
            Assert.AreEqual(F3L9, ret[5].Hit[3]);
        }

        [TestMethod]
        public void TestMethod2() {
            ContExt c = new ContExt();
            c.Delimiter = "\t";
            c.Encoding = "Shift-JIS";
            c.Init(@"./patterns.txt", false);
            c.Scan(@"./Test2");

            IList<Match> ret = c.Hit;

            Assert.AreEqual(6, ret.Count);

            const string F1 = @"a1.txt";
            const string F2 = @"ab1.txt";
            const string F3 = @"ac1.txt";

            const string F1L1 = @"a1.txt";
            const string F1L2 = @"elit, sed do eiusmod tempor incididunt ut labore et";
            const string F1L3 = @"dolore magna aliqua. Ut enim ad minim veniam, quis";
            const string F1L4 = @"contains";
            const string F1L5 = @"abc";
            const string F1L6 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F1L7 = @"eu fugiat nulla pariatur. Excepteur sint occaecat";
            const string F1L8 = @"def";

            Assert.AreEqual(F1, System.IO.Path.GetFileName(ret[0].Path));
            Assert.AreEqual(F1, System.IO.Path.GetFileName(ret[1].Path));

            Assert.AreEqual(1, ret[0].Start);
            Assert.AreEqual(7, ret[1].Start);

            Assert.AreEqual(F1L1, ret[0].Hit[0]);
            Assert.AreEqual(F1L2, ret[0].Hit[1]);
            Assert.AreEqual(F1L3, ret[0].Hit[2]);
            Assert.AreEqual(F1L4, ret[0].Hit[3]);
            Assert.AreEqual(F1L5, ret[1].Hit[0]);
            Assert.AreEqual(F1L6, ret[1].Hit[1]);
            Assert.AreEqual(F1L7, ret[1].Hit[2]);
            Assert.AreEqual(F1L8, ret[1].Hit[3]);

            const string F2L1 = @"ab1.txt contains";
            const string F2L2 = @"fgh";
            const string F2L3 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F2L4 = @"ijk";

            Assert.AreEqual(F2, System.IO.Path.GetFileName(ret[2].Path));
            Assert.AreEqual(F2, System.IO.Path.GetFileName(ret[3].Path));

            Assert.AreEqual(3, ret[2].Start);
            Assert.AreEqual(6, ret[3].Start);

            Assert.AreEqual(F2L1, ret[2].Hit[0]);
            Assert.AreEqual(F2L2, ret[3].Hit[0]);
            Assert.AreEqual(F2L3, ret[3].Hit[1]);
            Assert.AreEqual(F2L4, ret[3].Hit[2]);

            const string F3L1 = @"dolore magna ac1.txt aliqua. Ut enim ad minim veniam, quis";
            const string F3L2 = @"nostrud exercitation ullamco laboris nisi ut aliquip";
            const string F3L3 = @"ex ea commodo klm consequat. Duis aute irure dolor in";
            const string F3L4 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F3L5 = @"eu fugiat contains nulla pariatur. Excepteur sint occaecat";
            const string F3L6 = @"ex ea commodo klm consequat. Duis aute irure dolor in";
            const string F3L7 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string F3L8 = @"eu fugiat contains nulla pariatur. Excepteur sint occaecat";
            const string F3L9 = @"cupidatat non opq proident, sunt in culpa qui officia";

            Assert.AreEqual(F3, System.IO.Path.GetFileName(ret[4].Path));
            Assert.AreEqual(F3, System.IO.Path.GetFileName(ret[5].Path));

            Assert.AreEqual(2, ret[4].Start);
            Assert.AreEqual(4, ret[5].Start);

            Assert.AreEqual(F3L1, ret[4].Hit[0]);
            Assert.AreEqual(F3L2, ret[4].Hit[1]);
            Assert.AreEqual(F3L3, ret[4].Hit[2]);
            Assert.AreEqual(F3L4, ret[4].Hit[3]);
            Assert.AreEqual(F3L5, ret[4].Hit[4]);
            Assert.AreEqual(F3L6, ret[5].Hit[0]);
            Assert.AreEqual(F3L7, ret[5].Hit[1]);
            Assert.AreEqual(F3L8, ret[5].Hit[2]);
            Assert.AreEqual(F3L9, ret[5].Hit[3]);
        }
    }
}