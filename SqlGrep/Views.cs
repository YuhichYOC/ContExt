/*
 *
 * Views.cs
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

    public class Views {

        private IList<View> views;

        public Views() {
            views = new List<View>();
        }

        public PatternPicker PatternPicker { private get; set; }

        public NamePicker NamePicker { private get; set; }

        public void scan(string path) {
            ContExt c = new ContExt();
            PatternPicker.FillViews(NamePicker);
            c.Init(PatternPicker.Views, true);
            c.Scan(path);
        }

        private class View {

            private const string Label = @"VIEW";

            public View() {
                Selects = new List<string>();
            }

            public string Name { get; set; }

            public IList<string> Selects { get; set; }

            public override string ToString() {
                string ret = @"";
                foreach (string s in Selects) {
                    ret += Label + "\t" + Name + "\t" + @"SELECTS" + "\t" + s + "\r\n";
                }
                return ret;
            }
        }
    }
}