/*
 *
 * Condition.cs
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

using System;
using System.Collections.Generic;

public class Condition {

    private IList<RowCondition> rowConditions;

    private Match match;

    public IList<Match> Get { get; }

    public Condition() {
        rowConditions = new List<RowCondition>();
        Get = new List<Match>();
        match = new Match();
    }

    public void Add(string pattern, bool negative) {
        rowConditions.Add(new RowCondition(pattern, negative));
    }

    public void Test(string arg, int rix) {
        if (0 > rix) {
            throw new ArgumentException("Argument 'rix' must be bigger than zero. 'rix' is Row IndeX of file that testing.");
        }
        int testStart = 0;
        if (!match.Started && rowConditions[0].Test(arg)) {
            match.Start(rix);
            testStart += rowConditions[0].Left;
        }
        if (match.Started) {
            match.Add(arg);
            int rcc = rowConditions.Count;
            for (int i = 1; rcc > i; ++i) {
                if (rowConditions[i].IsHit) {
                    continue;
                }
                if (rowConditions[i].Test(arg.Substring(testStart))) {
                    testStart += rowConditions[i].Left;
                }
            }
        }
        if (!ConsistencyInspection()) {
            InitConditions();
            match.Init();
        }
        if (AllHit()) {
            InitConditions();
            Get.Add(match);
            match = new Match();
        }
    }

    private bool ConsistencyInspection() {
        IList<RowCondition> p = new List<RowCondition>();
        foreach (RowCondition item in rowConditions) {
            if (item.Negative) {
                continue;
            }
            p.Add(item);
        }
        int prcc = p.Count;
        for (int i = 1; prcc > i; ++i) {
            if (!p[i - 1].IsHit && p[i].IsHit) {
                return false;
            }
        }
        IList<RowCondition> n = new List<RowCondition>();
        foreach (RowCondition item in rowConditions) {
            if (item.Negative) {
                n.Add(item);
            }
        }
        int nrcc = n.Count;
        for (int j = 0; nrcc > j; ++j) {
            if (n[j].IsHit) {
                return false;
            }
        }
        return true;
    }

    private bool AllHit() {
        foreach (RowCondition item in rowConditions) {
            if (!item.Negative && !item.IsHit) {
                return false;
            }
        }
        return true;
    }

    public void Init() {
        InitConditions();
        Get.Clear();
        match.Init();
    }

    private void InitConditions() {
        foreach (RowCondition c in rowConditions) {
            c.Init();
        }
    }

    public override string ToString() {
        string ret = @"";
        for (int i = 0; rowConditions.Count > i; ++i) {
            if (0 < i) {
                ret += @" ## ";
            }
            if (rowConditions[i].Negative) {
                ret += @"[N]" + rowConditions[i].ToString();
            }
            else {
                ret += rowConditions[i].ToString();
            }
        }
        return ret;
    }

    private class RowCondition {

        private string p;

        public bool Negative { get; private set; }

        public bool IsHit { get; private set; }

        public int Left { get; private set; }

        public RowCondition(string pattern, bool negative) {
            p = pattern;
            Negative = negative;
            IsHit = false;
            Left = 0;
        }

        public void Init() {
            IsHit = false;
            Left = 0;
        }

        public bool Test(string arg) {
            if (System.Text.RegularExpressions.Regex.IsMatch(arg, p)) {
                IsHit = true;
                string capture = System.Text.RegularExpressions.Regex.Match(arg, p).Captures[0].Value;
                Left = arg.IndexOf(capture) + capture.Length;
                return true;
            }
            return false;
        }

        public override string ToString() {
            return p;
        }
    }
}