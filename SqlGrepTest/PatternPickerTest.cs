using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Reflection;

namespace SqlGrepTest {

    [TestClass]
    public class PatternPickerTest {

        [TestMethod]
        public void TestMethod0() {
            SqlGrep.PatternPicker pp = new SqlGrep.PatternPicker();
            MethodInfo cartesian = pp.GetType().GetMethod("Cartesian", BindingFlags.NonPublic | BindingFlags.InvokeMethod | BindingFlags.Instance);

            IList<IList<string>> arg1;
            IList<string> arg2;
            bool arg3;
            object[] args;
            IList<IList<string>> ret;

            // Pattern 1.
            // { A, B } * { 1, 2 }
            //   =
            //     { A, B, 1 }
            //     { A, B, 2 }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>(new string[] { @"A", @"B" }));
            arg2 = new List<string>(new string[] { @"1", @"2" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(3, ret[0].Count);
            Assert.AreEqual(3, ret[1].Count);
            Assert.AreEqual(@"A", ret[0][0]);
            Assert.AreEqual(@"B", ret[0][1]);
            Assert.AreEqual(@"1", ret[0][2]);
            Assert.AreEqual(@"A", ret[1][0]);
            Assert.AreEqual(@"B", ret[1][1]);
            Assert.AreEqual(@"2", ret[1][2]);

            // Pattern 2.
            // { A, B }
            // { C, D } * { 1, 2 }
            //   =
            //     { A, B, 1 }
            //     { A, B, 2 }
            //     { C, D, 1 }
            //     { C, D, 2 }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>(new string[] { @"A", @"B" }));
            arg1.Add(new List<string>(new string[] { @"C", @"D" }));
            arg2 = new List<string>(new string[] { @"1", @"2" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(4, ret.Count);
            Assert.AreEqual(3, ret[0].Count);
            Assert.AreEqual(3, ret[1].Count);
            Assert.AreEqual(3, ret[2].Count);
            Assert.AreEqual(3, ret[3].Count);
            Assert.AreEqual(@"A", ret[0][0]);
            Assert.AreEqual(@"B", ret[0][1]);
            Assert.AreEqual(@"1", ret[0][2]);
            Assert.AreEqual(@"A", ret[1][0]);
            Assert.AreEqual(@"B", ret[1][1]);
            Assert.AreEqual(@"2", ret[1][2]);
            Assert.AreEqual(@"C", ret[2][0]);
            Assert.AreEqual(@"D", ret[2][1]);
            Assert.AreEqual(@"1", ret[2][2]);
            Assert.AreEqual(@"C", ret[3][0]);
            Assert.AreEqual(@"D", ret[3][1]);
            Assert.AreEqual(@"2", ret[3][2]);

            // Pattern 3.
            // { } * { 1, 2 }
            //   =
            //     { 1 }
            //     { 2 }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>());
            arg2 = new List<string>(new string[] { @"1", @"2" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(1, ret[0].Count);
            Assert.AreEqual(1, ret[1].Count);
            Assert.AreEqual(@"1", ret[0][0]);
            Assert.AreEqual(@"2", ret[1][0]);

            // Pattern 4.
            // { A, B } * { }
            //   =
            //     { A, B }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>(new string[] { @"A", @"B" }));
            arg2 = new List<string>();
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(2, ret[0].Count);
            Assert.AreEqual(@"A", ret[0][0]);
            Assert.AreEqual(@"B", ret[0][1]);

            // Pattern 5-1. ( shift )
            // { } * { }
            //   =
            //     { "" }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>());
            arg2 = new List<string>();
            arg3 = true;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(1, ret[0].Count);
            Assert.AreEqual(@"", ret[0][0]);

            // Pattern 5-2.
            // { "" } * { A, B }
            //   =
            //     { "", A }
            //     { "", B }
            arg1 = ret;
            arg2 = new List<string>(new string[] { @"A", @"B" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(2, ret[0].Count);
            Assert.AreEqual(2, ret[1].Count);
            Assert.AreEqual(@"", ret[0][0]);
            Assert.AreEqual(@"A", ret[0][1]);
            Assert.AreEqual(@"", ret[1][0]);
            Assert.AreEqual(@"B", ret[1][1]);

            // Pattern 5-3.
            // { "", A }
            // { "", B } * { 1, 2 }
            //   =
            //     { "", A, 1 }
            //     { "", A, 2 }
            //     { "", B, 1 }
            //     { "", B, 2 }
            arg1 = ret;
            arg2 = new List<string>(new string[] { @"1", @"2" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(4, ret.Count);
            Assert.AreEqual(3, ret[0].Count);
            Assert.AreEqual(3, ret[1].Count);
            Assert.AreEqual(3, ret[2].Count);
            Assert.AreEqual(3, ret[3].Count);
            Assert.AreEqual(@"", ret[0][0]);
            Assert.AreEqual(@"A", ret[0][1]);
            Assert.AreEqual(@"1", ret[0][2]);
            Assert.AreEqual(@"", ret[1][0]);
            Assert.AreEqual(@"A", ret[1][1]);
            Assert.AreEqual(@"2", ret[1][2]);
            Assert.AreEqual(@"", ret[2][0]);
            Assert.AreEqual(@"B", ret[2][1]);
            Assert.AreEqual(@"1", ret[2][2]);
            Assert.AreEqual(@"", ret[3][0]);
            Assert.AreEqual(@"B", ret[3][1]);
            Assert.AreEqual(@"2", ret[3][2]);

            // Pattern 5-4.
            // { "", A, 1 }
            // { "", A, 2 }
            // { "", B, 1 }
            // { "", B, 2 } * { "" }
            //   =
            //     { "", A, 1, "" }
            //     { "", A, 2, "" }
            //     { "", B, 1, "" }
            //     { "", B, 2, "" }
            arg1 = ret;
            arg2 = new List<string>(new string[] { @"" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(4, ret.Count);
            Assert.AreEqual(4, ret[0].Count);
            Assert.AreEqual(4, ret[1].Count);
            Assert.AreEqual(4, ret[2].Count);
            Assert.AreEqual(4, ret[3].Count);
            Assert.AreEqual(@"", ret[0][0]);
            Assert.AreEqual(@"A", ret[0][1]);
            Assert.AreEqual(@"1", ret[0][2]);
            Assert.AreEqual(@"", ret[0][3]);
            Assert.AreEqual(@"", ret[1][0]);
            Assert.AreEqual(@"A", ret[1][1]);
            Assert.AreEqual(@"2", ret[1][2]);
            Assert.AreEqual(@"", ret[1][3]);
            Assert.AreEqual(@"", ret[2][0]);
            Assert.AreEqual(@"B", ret[2][1]);
            Assert.AreEqual(@"1", ret[2][2]);
            Assert.AreEqual(@"", ret[2][3]);
            Assert.AreEqual(@"", ret[3][0]);
            Assert.AreEqual(@"B", ret[3][1]);
            Assert.AreEqual(@"2", ret[3][2]);
            Assert.AreEqual(@"", ret[3][3]);

            // Pattern 6-1. ( shift )
            // { } * { 1, 2 }
            //   =
            //     { "", 1 }
            //     { "", 2 }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>());
            arg2 = new List<string>(new string[] { @"1", @"2" });
            arg3 = true;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(2, ret[0].Count);
            Assert.AreEqual(2, ret[1].Count);
            Assert.AreEqual(@"", ret[0][0]);
            Assert.AreEqual(@"1", ret[0][1]);
            Assert.AreEqual(@"", ret[1][0]);
            Assert.AreEqual(@"2", ret[1][1]);

            // Pattern 6-2.
            // { } * { 1, 2 }
            //   =
            //     { 1 }
            //     { 2 }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>());
            arg2 = new List<string>(new string[] { @"1", @"2" });
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(1, ret[0].Count);
            Assert.AreEqual(1, ret[1].Count);
            Assert.AreEqual(@"1", ret[0][0]);
            Assert.AreEqual(@"2", ret[1][0]);

            // Pattern 7.
            // { } * { }
            //   =
            //     { }
            arg1 = new List<IList<string>>();
            arg1.Add(new List<string>());
            arg2 = new List<string>();
            arg3 = false;
            args = new object[3] { arg1, arg2, arg3 };

            ret = (IList<IList<string>>)(cartesian.Invoke(pp, args));
            Assert.AreEqual(1, ret.Count);
            Assert.AreEqual(0, ret[0].Count);
        }

        [TestMethod]
        public void TestMethod1() {
            SqlGrep.PatternPicker p = new SqlGrep.PatternPicker();
            p.Read(@"./patterns.txt");

            Assert.AreEqual(3, p.Triggers.Count);

            const string L1T1 = @"CREATE";
            const string L1T2 = @"(OR REPLACE)*";
            const string L1T3 = @"TRIGGER";
            const string L1T4 = @"[:TRIGGERNAME:]";
            const string L1T5 = @"(BEFORE|AFTER)";
            const string L1T6 = @"(UPDATE|INSERT|DELETE)";
            const string L1T7 = @"ON";
            const string L1T8 = @"[N](SELECT|UPDATE|INSERT|DELETE)";
            const string L1T9 = @"[:TABLENAME:]";
            const string L2T1 = @"(UPDATE|INSERT|DELETE)";
            const string L2T2 = @"[N]ON";
            const string L2T3 = @"[:TABLENAME:]";
            const string L3T1 = @"(UPDATE|INSERT|DELETE)";
            const string L3T2 = @"[N]ON";
            const string L3T3 = @"[:TABLENAME:]";
            const string L3T4 = @"IMMEDIATE";

            Assert.AreEqual(L1T1, p.Triggers[0][0]);
            Assert.AreEqual(L1T2, p.Triggers[0][1]);
            Assert.AreEqual(L1T3, p.Triggers[0][2]);
            Assert.AreEqual(L1T4, p.Triggers[0][3]);
            Assert.AreEqual(L1T5, p.Triggers[0][4]);
            Assert.AreEqual(L1T6, p.Triggers[0][5]);
            Assert.AreEqual(L1T7, p.Triggers[0][6]);
            Assert.AreEqual(L1T8, p.Triggers[0][7]);
            Assert.AreEqual(L1T9, p.Triggers[0][8]);
            Assert.AreEqual(L2T1, p.Triggers[1][0]);
            Assert.AreEqual(L2T2, p.Triggers[1][1]);
            Assert.AreEqual(L2T3, p.Triggers[1][2]);
            Assert.AreEqual(L3T1, p.Triggers[2][0]);
            Assert.AreEqual(L3T2, p.Triggers[2][1]);
            Assert.AreEqual(L3T3, p.Triggers[2][2]);
            Assert.AreEqual(L3T4, p.Triggers[2][3]);
        }
    }
}