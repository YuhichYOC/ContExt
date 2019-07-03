using System.Collections.Generic;

namespace ContExtKicker {

    public class Writer {

        public bool WriteTag { private get; set; }

        public bool WriteStdout { private get; set; }

        public string Path { private get; set; }

        public Writer() {
            WriteTag = false;
        }

        public void Run(IList<Match> arg) {
            IList<string> ret = new List<string>();
            for (int i = 0; arg.Count > i; ++i) {
                for (int j = 0; arg[i].Hit.Count > j; ++j) {
                    string add = @"";
                    if (WriteTag) {
                        add += arg[i].Tag;
                        add += "\t";
                        add += i.ToString();
                        add += "\t";
                        add += j.ToString();
                        add += "\t";
                    }
                    add += arg[i].Path;
                    add += "\t";
                    add += (arg[i].Start + j).ToString();
                    add += "\t";
                    add += arg[i].Hit[j];
                    ret.Add(add);
                }
            }
            Stdout(ret);
            Write(ret);
        }

        private void Stdout(IList<string> arg) {
            if (!WriteStdout) {
                return;
            }
            for (int i = 0; arg.Count > i; ++i) {
                System.Console.WriteLine(arg[i]);
            }
        }

        private void Write(IList<string> arg) {
            if (WriteStdout) {
                return;
            }
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Path)) {
                for (int i = 0; arg.Count > i; ++i) {
                    sw.WriteLine(arg[i]);
                }
            }
        }
    }
}