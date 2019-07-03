using System;

namespace ContExtKicker {

    internal class Program {

        private static void Main(string[] args) {
            try {
                ArgumentPicker picker = new ArgumentPicker(args);
                if (string.IsNullOrEmpty(picker.Pattern)) {
                    return;
                }
                if (string.IsNullOrEmpty(picker.Directory)) {
                    return;
                }
                ContExt c = new ContExt();
                c.Init(picker.Pattern, false);
                SetDelimiter(c, picker);
                SetEncoding(c, picker);
                InitC(c, picker);
                c.Scan(picker.Directory);
                Write(c, picker);
            }
            catch (System.Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SetDelimiter(ContExt c, ArgumentPicker picker) {
            if (!string.IsNullOrEmpty(picker.Delimiter)) {
                c.Delimiter = picker.Delimiter;
            }
            else {
                c.Delimiter = "\t";
            }
        }

        private static void SetEncoding(ContExt c, ArgumentPicker picker) {
            if (!string.IsNullOrEmpty(picker.Encoding)) {
                c.Encoding = picker.Encoding;
            }
        }

        private static void InitC(ContExt c, ArgumentPicker picker) {
            c.Init(picker.Pattern, picker.UseTag);
        }

        private static void Write(ContExt c, ArgumentPicker picker) {
            Writer w = new Writer();
            w.WriteTag = picker.UseTag;
            w.WriteStdout = picker.WriteStdout;
            w.Path = picker.Out;
            w.Run(c.Hit);
        }
    }
}