using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileEntityTest {

    [TestClass]
    public class FileEntityTest {

        [TestMethod]
        public void TestMethod1() {
            FileEntity f = new FileEntity(@"./TextFile1.txt", @"UTF-8");
            f.Read();
            Assert.AreEqual(5, f.RowCount);
            Assert.AreEqual(@"This", f.Get[0]);
            Assert.AreEqual(@"is", f.Get[1]);
            Assert.AreEqual(@"file", f.Get[2]);
            Assert.AreEqual(@"for", f.Get[3]);
            Assert.AreEqual(@"TestMethod1", f.Get[4]);
            f.Clear();
            Assert.AreEqual(0, f.Get.Count);
            Assert.AreEqual(0, f.RowCount);
        }
    }
}