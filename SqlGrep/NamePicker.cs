/*
 *
 * NamePicker.cs
 *
 * Copyright 2019 Yuichi Yoshii
 *     吉井雄一 @ 吉井産業  you.65535.kir@gmail.com
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System.Collections.Generic;

namespace SqlGrep {

    public class NamePicker {

        private const string DEFINE_FUNCTION = @"FUNCTION";

        private const string DEFINE_PACKAGEBODY = @"PACKAGEBODY";

        private const string DEFINE_PACKAGE = @"PACKAGE";

        private const string DEFINE_PROCEDURE = @"PROCEDURE";

        private const string DEFINE_TABLE = @"TABLE";

        private const string DEFINE_TRIGGER = @"TRIGGER";

        private const string DEFINE_VIEW = @"VIEW";

        public IList<string> Executables { get; private set; }

        public IList<string> Functions { get; private set; }

        public IList<string> PackageBodies { get; private set; }

        public IList<string> Packages { get; private set; }

        public IList<string> Procedures { get; private set; }

        public IList<string> Selectables { get; private set; }

        public IList<string> Tables { get; private set; }

        public IList<string> Triggers { get; private set; }

        public IList<string> Views { get; private set; }

        public NamePicker() {
            Executables = new List<string>();
            Functions = new List<string>();
            PackageBodies = new List<string>();
            Packages = new List<string>();
            Procedures = new List<string>();
            Selectables = new List<string>();
            Tables = new List<string>();
            Triggers = new List<string>();
            Views = new List<string>();
        }

        public void Read(string path) {
            FileEntity f = new FileEntity(path, "UTF-8");
            f.Read();
            foreach (string l in f.Get) {
                if (0 > l.IndexOf("\t")) {
                    continue;
                }
                string left = l.Split("\t".ToCharArray())[0];
                string right = l.Split("\t".ToCharArray())[1];
                switch (left) {
                    case DEFINE_FUNCTION:
                        Functions.Add(right);
                        break;

                    case DEFINE_PACKAGEBODY:
                        PackageBodies.Add(right);
                        break;

                    case DEFINE_PACKAGE:
                        Packages.Add(right);
                        break;

                    case DEFINE_PROCEDURE:
                        Procedures.Add(right);
                        break;

                    case DEFINE_TABLE:
                        Tables.Add(right);
                        break;

                    case DEFINE_TRIGGER:
                        Triggers.Add(right);
                        break;

                    case DEFINE_VIEW:
                        Views.Add(right);
                        break;

                    default:
                        break;
                }
            }
            foreach (string item in Functions) {
                Executables.Add(item);
            }
            foreach (string item in PackageBodies) {
                Executables.Add(item);
            }
            foreach (string item in Packages) {
                Executables.Add(item);
            }
            foreach (string item in Procedures) {
                Executables.Add(item);
            }
            foreach (string item in Tables) {
                Selectables.Add(item);
            }
            foreach (string item in Views) {
                Selectables.Add(item);
            }
        }

        public string PickExecutable(string arg) {
            for (int i = 0; Executables.Count > i; ++i) {
                if (-1 < arg.IndexOf(Executables[i])) {
                    return Executables[i];
                }
            }
            return string.Empty;
        }

        public string PickSelectable(string arg) {
            for (int i = 0; Selectables.Count > i; ++i) {
                if (-1 < arg.IndexOf(Selectables[i])) {
                    return Selectables[i];
                }
            }
            return string.Empty;
        }

        public string PickTrigger(string arg) {
            for (int i = 0; Triggers.Count > i; ++i) {
                if (-1 < arg.IndexOf(Triggers[i])) {
                    return Triggers[i];
                }
            }
            return string.Empty;
        }
    }
}