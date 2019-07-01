using System;
using System.Collections.Generic;

namespace ContExtKicker {

    internal class Program {

        private static void Main(string[] args) {
            try {
                ArgumentPicker picker = new ArgumentPicker(args);
                ContExt c = new ContExt();
                if (!string.IsNullOrEmpty(picker.Delimiter)) {
                    c.Delimiter = picker.Delimiter;
                }
                if (!string.IsNullOrEmpty(picker.Encoding)) {
                    c.Encoding = picker.Encoding;
                }
                c.Init(picker.PatternFile, false);
                c.Scan(picker.DirectoryPath);
                Write(c.Get, picker.OutputPath);
            }
            catch (System.Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Write(IList<Match> arg, string path) {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path)) {
                for (int i = 0; arg.Count > i; ++i) {
                    for (int j = 0; arg[i].Get.Count > j; ++j) {
                        sw.Write(arg[i].Path);
                        sw.Write("\t");
                        sw.Write((arg[i].StartAt + j).ToString());
                        sw.Write("\t");
                        sw.Write(arg[i].Get[j]);
                        sw.Write("\r\n");
                    }
                }
            }
        }
    }
}