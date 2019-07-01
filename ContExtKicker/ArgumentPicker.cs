using System;
using System.Collections.Generic;

namespace ContExtKicker {

    public class ArgumentPicker {

        public string Delimiter { get; private set; }

        public string Encoding { get; private set; }

        public string PatternFile { get; private set; }

        public string DirectoryPath { get; private set; }

        public string OutputPath { get; private set; }

        private List<string> argList;

        public ArgumentPicker(string[] arg) {
            argList = new List<string>();
            for (int i = 0; arg.Length > i; ++i) {
                argList.Add(arg[i]);
            }
            run();
        }

        private void run() {
            PairInspection();
            PickDelimiter();
            PickEncoding();
            PickPatternFile();
            PickDirectoryPath();
            PickOutputPath();
        }

        private void PairInspection() {
            for (int i = 1; argList.Count > i; ++i) {
                if (argList[i - 1].StartsWith(@"-") && argList[i].StartsWith(@"-")) {
                    throw new ArgumentException(@"引数 " + argList[i - 1] + @" が指定されていません");
                }
                if (argList.Count - 1 == i && argList[i].StartsWith(@"-")) {
                    throw new ArgumentException(@"引数 " + argList[i] + @" が指定されていません");
                }
            }
        }

        private void PickDelimiter() {
            for (int i = 0; argList.Count > i; ++i) {
                if (argList[i].StartsWith(@"--Delim")) {
                    if (@"\t".Equals(argList[i + 1])) {
                        Delimiter = "\t";
                    }
                    else {
                        Delimiter = argList[i + 1];
                    }
                    return;
                }
            }
        }

        private void PickEncoding() {
            for (int i = 0; argList.Count > i; ++i) {
                if (argList[i].StartsWith(@"--Encod")) {
                    _ = System.Text.Encoding.GetEncoding(argList[i + 1]);
                    Encoding = argList[i + 1];
                    return;
                }
            }
        }

        private void PickPatternFile() {
            for (int i = 0; argList.Count > i; ++i) {
                if (argList[i].StartsWith(@"--Patt")) {
                    if (!System.IO.File.Exists(argList[i + 1])) {
                        throw new ArgumentException(@"パターンファイル " + argList[i + 1] + @" はファイルシステム上に存在しません");
                    }
                    PatternFile = argList[i + 1];
                    return;
                }
            }
        }

        private void PickDirectoryPath() {
            for (int i = 0; argList.Count > i; ++i) {
                if (argList[i].StartsWith(@"--Dir")) {
                    if (!System.IO.Directory.Exists(argList[i + 1])) {
                        throw new ArgumentException(@"ディレクトリ " + argList[i + 1] + @" はファイルシステム上に存在しません");
                    }
                    DirectoryPath = argList[i + 1];
                    return;
                }
            }
        }

        private void PickOutputPath() {
            for (int i = 0; argList.Count > i; ++i) {
                if (argList[i].StartsWith(@"--Out")) {
                    if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(argList[i + 1]))) {
                        throw new ArgumentException(@"出力ディレクトリ " + argList[i + 1] + @" はファイルシステム上に存在しません");
                    }
                    if (System.IO.File.Exists(argList[i + 1])) {
                        throw new ArgumentException(@"出力先ファイル " + argList[i + 1] + @" が既に存在します");
                    }
                    OutputPath = argList[i + 1];
                    return;
                }
            }
        }
    }
}