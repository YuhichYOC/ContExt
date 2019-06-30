using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SqlGrepTest {

    [TestClass]
    public class NamePickerTest {

        [TestMethod]
        public void TestMethod1() {
            SqlGrep.NamePicker np = new SqlGrep.NamePicker();
            np.Read(@"./Test/namepickertest.txt");

            Assert.AreEqual(8, np.Tables.Count);

            const string T1 = @"Account";
            const string T2 = @"Bugs";
            const string T3 = @"BugsProducts";
            const string T4 = @"BugStatus";
            const string T5 = @"Comments";
            const string T6 = @"Products";
            const string T7 = @"Screenshots";
            const string T8 = @"Tags";
            const string TR1 = @"Tr_AfterIns_Accounts";
            const string TR2 = @"Tr_BeforeIns_Bugs";

            Assert.AreEqual(T1, np.Tables[0]);
            Assert.AreEqual(T2, np.Tables[1]);
            Assert.AreEqual(T3, np.Tables[2]);
            Assert.AreEqual(T4, np.Tables[3]);
            Assert.AreEqual(T5, np.Tables[4]);
            Assert.AreEqual(T6, np.Tables[5]);
            Assert.AreEqual(T7, np.Tables[6]);
            Assert.AreEqual(T8, np.Tables[7]);
            Assert.AreEqual(2, np.Triggers.Count);
            Assert.AreEqual(TR1, np.Triggers[0]);
            Assert.AreEqual(TR2, np.Triggers[1]);
        }
    }
}