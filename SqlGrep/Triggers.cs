/*
 *
 * Triggers.cs
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

    public class Triggers {

        private IList<Trigger> triggers;

        public Triggers() {
            triggers = new List<Trigger>();
        }

        public PatternPicker PatternPicker { private get; set; }

        public NamePicker NamePicker { private get; set; }

        public void scan(string path) {
            ContExt c = new ContExt();
            PatternPicker.FillTriggers(NamePicker);
            c.Init(PatternPicker.Triggers, true);
            c.Scan(path);
            IList<Match> ret = c.Get;
            foreach (Match m in ret) {
            }
        }

        private class Trigger {

            private const string Label = @"TRIGGER";

            public Trigger() {
                Selects = new List<string>();
                Updates = new List<string>();
                Inserts = new List<string>();
                Deletes = new List<string>();
            }

            public string Name { get; set; }

            public string TriggerTable { get; set; }

            public IList<string> Selects { get; set; }

            public IList<string> Updates { get; set; }

            public IList<string> Inserts { get; set; }

            public IList<string> Deletes { get; set; }

            public override string ToString() {
                string ret = Label + "\t" + Name + "\t" + @"WATCHES" + "\t" + TriggerTable + "\r\n";
                foreach (string s in Selects) {
                    ret += Label + "\t" + Name + "\t" + @"SELECTS" + "\t" + s + "\r\n";
                }
                foreach (string u in Updates) {
                    ret += Label + "\t" + Name + "\t" + @"UPDATES" + "\t" + u + "\r\n";
                }
                foreach (string i in Inserts) {
                    ret += Label + "\t" + Name + "\t" + @"INSERTS" + "\t" + i + "\r\n";
                }
                foreach (string d in Deletes) {
                    ret += Label + "\t" + Name + "\t" + @"DELETES" + "\t" + d + "\r\n";
                }
                return ret;
            }
        }
    }
}