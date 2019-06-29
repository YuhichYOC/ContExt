/*
 *
 * PatternPicker.cs
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

    public class PatternPicker {

        private const string PATTERN_LABEL = @"##";

        private const string PATTERN_LABEL_FUNCTIONS = @"##FUNCTIONS##";

        private const string PATTERN_LABEL_PACKAGEBODIES = @"##PACKAGEBODIES##";

        private const string PATTERN_LABEL_PACKAGES = @"##PACKAGES##";

        private const string PATTERN_LABEL_PROCEDURES = @"##PROCEDURES##";

        private const string PATTERN_LABEL_TRIGGERS = @"##TRIGGERS##";

        private const string PATTERN_LABEL_VIEWS = @"##VIEWS##";

        public IList<IList<string>> Functions { get; private set; }

        public IList<IList<string>> PackageBodies { get; private set; }

        public IList<IList<string>> Packages { get; private set; }

        public IList<IList<string>> Procedures { get; private set; }

        public IList<IList<string>> Triggers { get; private set; }

        public IList<IList<string>> Views { get; private set; }

        public PatternPicker() {
            Functions = new List<IList<string>>();
            PackageBodies = new List<IList<string>>();
            Packages = new List<IList<string>>();
            Procedures = new List<IList<string>>();
            Triggers = new List<IList<string>>();
            Views = new List<IList<string>>();
        }

        public void Read(string path) {
            FileEntity f = new FileEntity(path, "UTF-8");
            f.Read();
            Condition c_function = new Condition();
            c_function.Add(PATTERN_LABEL_FUNCTIONS, false);
            c_function.Add(PATTERN_LABEL, false);
            Condition c_packageBody = new Condition();
            c_packageBody.Add(PATTERN_LABEL_PACKAGEBODIES, false);
            c_packageBody.Add(PATTERN_LABEL, false);
            Condition c_package = new Condition();
            c_package.Add(PATTERN_LABEL_PACKAGES, false);
            c_package.Add(PATTERN_LABEL, false);
            Condition c_procedure = new Condition();
            c_procedure.Add(PATTERN_LABEL_PROCEDURES, false);
            c_procedure.Add(PATTERN_LABEL, false);
            Condition c_trigger = new Condition();
            c_trigger.Add(PATTERN_LABEL_TRIGGERS, false);
            c_trigger.Add(PATTERN_LABEL, false);
            Condition c_view = new Condition();
            c_view.Add(PATTERN_LABEL_VIEWS, false);
            c_view.Add(PATTERN_LABEL, false);
            for (int i = 0; f.RowCount > i; ++i) {
                c_function.Test(f.Get[i], i);
                c_packageBody.Test(f.Get[i], i);
                c_package.Test(f.Get[i], i);
                c_procedure.Test(f.Get[i], i);
                c_trigger.Test(f.Get[i], i);
                c_view.Test(f.Get[i], i);
            }
            Pick(Functions, c_function);
            Pick(PackageBodies, c_packageBody);
            Pick(Packages, c_package);
            Pick(Procedures, c_procedure);
            Pick(Triggers, c_trigger);
            Pick(Views, c_view);
        }

        private void Pick(IList<IList<string>> vs, Condition condition) {
            if (0 < condition.Get.Count && 2 < condition.Get[0].Get.Count) {
                for (int i = 1; condition.Get[0].Get.Count - 1 > i; ++i) {
                    vs.Add(new List<string>(condition.Get[0].Get[i].Split("\t".ToCharArray())));
                }
            }
        }

        public void FillFunctions(NamePicker np) => Functions = Fill(np, Functions);

        public void FillPackageBodies(NamePicker np) => PackageBodies = Fill(np, PackageBodies);

        public void FillPackages(NamePicker np) => Packages = Fill(np, Packages);

        public void FillProcedures(NamePicker np) => Procedures = Fill(np, Procedures);

        public void FillViews(NamePicker np) => Views = Fill(np, Views);

        private IList<IList<string>> Fill(NamePicker np, IList<IList<string>> arg) {
            IList<IList<string>> replace = new List<IList<string>>();
            foreach (IList<string> item in arg) {
                int i = item.IndexOf(@"[:TABLENAME:]");
                int j = item.IndexOf(@"[:EXECUTABLENAME:]");
                IList<IList<string>> c = new List<IList<string>>();
                c.Add(new List<string>());
                c = Cartesian(c, (0 > i) ? new List<string>() : np.Selectables, (0 > i));
                c = Cartesian(c, (0 > j) ? new List<string>() : np.Executables, false);
                int total = c.Count;
                IList<IList<string>> add = new List<IList<string>>();
                for (int k = 0; total > k; ++k) {
                    add.Add(new List<string>(item));
                }
                for (int l = 0; total > l; ++l) {
                    if (-1 < j) {
                        add[l][j] = c[l][1];
                    }
                    if (-1 < i) {
                        add[l][i] = c[l][0];
                    }
                }
                foreach (IList<string> addItem in add) {
                    replace.Add(addItem);
                }
            }
            arg = replace;
            return arg;
        }

        public void FillTriggers(NamePicker np) {
            IList<IList<string>> replace = new List<IList<string>>();
            foreach (IList<string> item in Triggers) {
                int i = item.IndexOf(@"[:TRIGGERNAME:]");
                int j = item.IndexOf(@"[:TABLENAME:]");
                int k = item.IndexOf(@"[:EXECUTABLENAME:]");
                IList<IList<string>> c = new List<IList<string>>();
                c.Add(new List<string>());
                c = Cartesian(c, (0 > i) ? new List<string>() : np.Triggers, (0 > i));
                c = Cartesian(c, (0 > j) ? new List<string>() : np.Selectables, false);
                c = Cartesian(c, (0 > k) ? new List<string>() : np.Executables, false);
                int total = c.Count;
                IList<IList<string>> add = new List<IList<string>>();
                for (int l = 0; total > l; ++l) {
                    add.Add(new List<string>(item));
                }
                for (int m = 0; total > m; ++m) {
                    if (-1 < k) {
                        add[m][k] = c[m][2];
                    }
                    if (-1 < j) {
                        add[m][j] = c[m][1];
                    }
                    if (-1 < i) {
                        add[m][i] = c[m][0];
                    }
                }
                foreach (IList<string> addItem in add) {
                    replace.Add(addItem);
                }
            }
            Triggers = replace;
        }

        private IList<IList<string>> Cartesian(IList<IList<string>> arg1, IList<string> arg2, bool shiftRight) {
            IList<IList<string>> ret = new List<IList<string>>();
            if (0 == arg1.Count) {
                for (int i = 0; arg2.Count > i; ++i) {
                    List<string> add = new List<string>();
                    add.Add(arg2[i]);
                    ret.Add(add);
                }
            }
            else if (0 == arg2.Count) {
                for (int i = 0; arg1.Count > i; ++i) {
                    List<string> add = new List<string>();
                    for (int j = 0; arg1[i].Count > j; ++j) {
                        add.Add(arg1[i][j]);
                    }
                    ret.Add(add);
                }
            }
            else {
                for (int i = 0; arg1.Count > i; ++i) {
                    for (int j = 0; arg2.Count > j; ++j) {
                        List<string> add = new List<string>();
                        for (int k = 0; arg1[i].Count > k; ++k) {
                            add.Add(arg1[i][k]);
                        }
                        add.Add(arg2[j]);
                        ret.Add(add);
                    }
                }
            }
            if (shiftRight) {
                foreach (IList<string> s in ret) {
                    s.Insert(0, @"");
                }
            }
            return ret;
        }
    }
}