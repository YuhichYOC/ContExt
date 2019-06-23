/*
 *
 * ContExt.cs
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

public class ContExt {

    private IList<Condition> conditions;

    public string Delimiter { get; set; }

    public string Encoding { get; set; }

    public IList<Match> Get { get; private set; }

    public ContExt() {
        conditions = new List<Condition>();
        Delimiter = @"|";
        Encoding = @"UTF-8";
        Get = new List<Match>();
    }

    public void Init(string arg) {
        using (System.IO.StreamReader r = new System.IO.StreamReader(arg, System.Text.Encoding.GetEncoding(Encoding))) {
            while (!r.EndOfStream) {
                string l = r.ReadLine();
                Condition add = new Condition();
                string[] addc = l.Split(Delimiter.ToCharArray());
                for (int i = 0; addc.Length > i; ++i) {
                    add.Add(addc[i]);
                }
                conditions.Add(add);
            }
        }
    }

    public void Init(IList<IList<string>> arg) {
        foreach (IList<string> l in arg) {
            Condition add = new Condition();
            foreach (string item in l) {
                add.Add(item);
            }
            conditions.Add(add);
        }
    }

    public void Scan(string path) {
        DirectoryEntity d = new DirectoryEntity(path, Encoding);
        d.Describe();
        Scan(d);
    }

    private void Scan(DirectoryEntity d) {
        foreach (FileEntity f in d.Files) {
            f.Read();
            for (int i = 0; f.RowCount > i; ++i) {
                foreach (Condition c in conditions) {
                    c.Test(f.Get[i], i);
                }
            }
            foreach (Condition c in conditions) {
                if (0 < c.Get.Count) {
                    foreach (Match m in c.Get) {
                        m.Path = f.Path;
                        m.Pattern = c.ToString();
                        Get.Add(m);
                    }
                }
                c.Init();
            }
            f.Clear();
        }
        foreach (DirectoryEntity subD in d.SubDirectories) {
            Scan(subD);
        }
    }
}