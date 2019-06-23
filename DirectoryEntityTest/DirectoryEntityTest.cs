using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DirectoryEntityTest {

    [TestClass]
    public class DirectoryEntityTest {

        [TestMethod]
        public void TestMethod1() {
            DirectoryEntity d = new DirectoryEntity(@"./a", @"UTF-8");
            d.Describe();
            Assert.AreEqual(@"a1.txt", d.Files[0].Name);
            Assert.AreEqual(@"ab", d.SubDirectories[0].Name);
            Assert.AreEqual(@"ac", d.SubDirectories[1].Name);
            Assert.AreEqual(@"ab1.txt", d.SubDirectories[0].Files[0].Name);
            Assert.AreEqual(@"ac1.txt", d.SubDirectories[1].Files[0].Name);
        }
    }
}