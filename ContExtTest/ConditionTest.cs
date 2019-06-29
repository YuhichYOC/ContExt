using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ContExtTest {

    [TestClass]
    public class ConditionTest {

        [TestMethod]
        public void TestMethod1() {
            Condition c = new Condition();
            c.Add(@"a1\.txt", false);
            c.Add(@"contains", false);
            using (System.IO.StreamReader r = new System.IO.StreamReader(@"./Test1/a1.txt")) {
                int rc = 0;
                while (!r.EndOfStream) {
                    c.Test(r.ReadLine(), rc);
                    ++rc;
                }
            }
            IList<Match> ret = c.Get;
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(4, ret[0].Get.Count);
            Assert.AreEqual(1, ret[0].StartAt);

            const string L1 = @"a1.txt";
            const string L2 = @"elit, sed do eiusmod tempor incididunt ut labore et";
            const string L3 = @"dolore magna aliqua. Ut enim ad minim veniam, quis";
            const string L4 = @"contains";
            Assert.AreEqual(L1, ret[0].Get[0]);
            Assert.AreEqual(L2, ret[0].Get[1]);
            Assert.AreEqual(L3, ret[0].Get[2]);
            Assert.AreEqual(L4, ret[0].Get[3]);
        }

        [TestMethod]
        public void TestMethod2() {
            Condition c = new Condition();
            c.Add(@"ac1\.txt", false);
            c.Add(@"contains", false);
            using (System.IO.StreamReader r = new System.IO.StreamReader(@"./Test1/ac/ac1.txt")) {
                int rc = 0;
                while (!r.EndOfStream) {
                    c.Test(r.ReadLine(), rc);
                    ++rc;
                }
            }
            IList<Match> ret = c.Get;
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(5, ret[0].Get.Count);
            Assert.AreEqual(2, ret[0].StartAt);

            const string L1 = @"dolore magna ac1.txt aliqua. Ut enim ad minim veniam, quis";
            const string L2 = @"nostrud exercitation ullamco laboris nisi ut aliquip";
            const string L3 = @"ex ea commodo klm consequat. Duis aute irure dolor in";
            const string L4 = @"reprehenderit in voluptate velit esse cillum dolore";
            const string L5 = @"eu fugiat contains nulla pariatur. Excepteur sint occaecat";
            Assert.AreEqual(L1, ret[0].Get[0]);
            Assert.AreEqual(L2, ret[0].Get[1]);
            Assert.AreEqual(L3, ret[0].Get[2]);
            Assert.AreEqual(L4, ret[0].Get[3]);
            Assert.AreEqual(L5, ret[0].Get[4]);
        }

        [TestMethod]
        public void TestMethod3() {
            Condition c = new Condition();
            c.Add(@"ConditionTest::TestMethod3 start", false);
            c.Add(@"contains", false);
            c.Add(@"ConditionTest::TestMethod3 end", false);
            using (System.IO.StreamReader r = new System.IO.StreamReader(@"./Test1/a2.txt")) {
                int rc = 0;
                while (!r.EndOfStream) {
                    c.Test(r.ReadLine(), rc);
                    ++rc;
                }
            }
            IList<Match> ret = c.Get;
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(7, ret[0].Get.Count);
            Assert.AreEqual(1, ret[0].StartAt);

            const string L1 = @"ConditionTest::TestMethod3 start";
            const string L2 = @"elit, sed do eiusmod tempor incididunt ut labore et";
            const string L3 = @"dolore magna aliqua. Ut enim ad minim veniam, quis";
            const string L4 = @"contains";
            const string L5 = @"nostrud exercitation ullamco laboris nisi ut aliquip";
            const string L6 = @"ex ea commodo consequat. Duis aute irure dolor in";
            const string L7 = @"ConditionTest::TestMethod3 end";
            Assert.AreEqual(L1, ret[0].Get[0]);
            Assert.AreEqual(L2, ret[0].Get[1]);
            Assert.AreEqual(L3, ret[0].Get[2]);
            Assert.AreEqual(L4, ret[0].Get[3]);
            Assert.AreEqual(L5, ret[0].Get[4]);
            Assert.AreEqual(L6, ret[0].Get[5]);
            Assert.AreEqual(L7, ret[0].Get[6]);
        }

        [TestMethod]
        public void TestMethod4() {
            Condition c = new Condition();
            c.Add(@"ConditionTest::TestMethod4 start", false);
            c.Add(@"contains", false);
            c.Add(@"ConditionTest::TestMethod4 end", false);
            using (System.IO.StreamReader r = new System.IO.StreamReader(@"./Test1/a3.txt")) {
                int rc = 0;
                while (!r.EndOfStream) {
                    c.Test(r.ReadLine(), rc);
                    ++rc;
                }
            }
            IList<Match> ret = c.Get;
            Assert.AreEqual(0, ret.Count);
        }

        [TestMethod]
        public void TestMethod5() {
            const string P1 = @"##TABLES##";
            const string P2 = @"##";
            const string P3 = @"##";

            const string TEST1 = @"TEST ROW1";
            const string TEST2 = @"TEST ##TABLES## ROW2";
            const string TEST3 = @"TEST ROW3";
            const string TEST4 = @"## TEST ROW4";
            const string TEST5 = @"TEST ROW 5##";

            Condition c = new Condition();
            c.Add(P1, false);
            c.Add(P2, false);
            c.Add(P3, false);
            c.Test(TEST1, 0);
            c.Test(TEST2, 1);
            c.Test(TEST3, 2);
            c.Test(TEST4, 3);
            c.Test(TEST5, 4);

            IList<Match> ret = c.Get;
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(4, ret[0].Get.Count);
        }

        [TestMethod]
        public void TestMethod6() {
            const string P1 = @"CREATE";
            const string P2 = @"(OR REPLACE)*";
            const string P3 = @"TRIGGER";
            const string P4 = @"Tr_AfterIns_Accounts";
            const string P5 = @"(BEFORE|AFTER)";
            const string P6 = @"(UPDATE|INSERT|DELETE)";
            const string P7 = @"ON";
            const string P8 = @"(SELECT|UPDATE|INSERT|DELETE)";
            const string P9 = @"Accounts";

            const string TEST1 = @"CREATE OR REPLACE TRIGGER Tr_AfterIns_Accounts ";
            const string TEST2 = @"AFTER INSERT ON Accounts ";

            Condition c = new Condition();
            c.Add(P1, false);
            c.Add(P2, false);
            c.Add(P3, false);
            c.Add(P4, false);
            c.Add(P5, false);
            c.Add(P6, false);
            c.Add(P7, false);
            c.Add(P8, true);
            c.Add(P9, false);
            c.Test(TEST1, 0);
            c.Test(TEST2, 1);

            IList<Match> ret = c.Get;
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(2, ret[0].Get.Count);
        }
    }
}