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

    public void Init(string arg, bool startsWithTag) {
        using (System.IO.StreamReader r = new System.IO.StreamReader(arg, System.Text.Encoding.GetEncoding(Encoding))) {
            while (!r.EndOfStream) {
                conditions.Add(FetchPatterns(Split(r.ReadLine()), startsWithTag));
            }
        }
    }

    private IList<string> Split(string arg) {
        IList<string> ret = new List<string>();
        string[] items = arg.Split(Delimiter.ToCharArray());
        for (int i = 0; items.Length > i; ++i) {
            ret.Add(items[i]);
        }
        return ret;
    }

    public void Init(IList<IList<string>> arg, bool startsWithTag) {
        for (int i = 0; arg.Count > i; ++i) {
            conditions.Add(FetchPatterns(arg[i], startsWithTag));
        }
    }

    private Condition FetchPatterns(IList<string> patterns, bool startsWithTag) {
        Condition ret = new Condition();
        for (int i = 0; patterns.Count > i; ++i) {
            if (0 == i && startsWithTag) {
                ret.Tag = patterns[i];
                continue;
            }
            bool negative = false;
            if (patterns[i].StartsWith(@"[N]")) {
                negative = true;
                patterns[i] = patterns[i].Substring(3);
            }
            if (patterns[i].StartsWith(@"(-)")) {
                negative = true;
                patterns[i] = patterns[i].Substring(3);
            }
            ret.Add(patterns[i], negative);
        }
        return ret;
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