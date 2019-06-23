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

    public void Add(string pattern) {
        rowConditions.Add(new RowCondition(pattern));
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
                    if (!rowConditions[i - 1].IsHit) {
                        match.Init();
                        return;
                    }
                    testStart += rowConditions[i].Left;
                }
            }
            for (int j = 1; rcc > j; ++j) {
                if (!rowConditions[j].IsHit) {
                    return;
                }
            }
            Get.Add(match);
            match = new Match();
        }
    }

    public void Init() {
        foreach (RowCondition c in rowConditions) {
            c.Init();
        }
        Get.Clear();
        match.Init();
    }

    public override string ToString() {
        string ret = @"";
        for (int i = 0; rowConditions.Count > i; ++i) {
            if (0 < i) {
                ret += @" ## ";
            }
            ret += rowConditions[i].ToString();
        }
        return ret;
    }

    private class RowCondition {

        private string p;

        public bool IsHit { get; private set; }

        public int Left { get; private set; }

        public RowCondition(string pattern) {
            p = pattern;
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
                Left = System.Text.RegularExpressions.Regex.Match(arg, @".*" + p).Length;
                return true;
            }
            return false;
        }

        public override string ToString() {
            return p;
        }
    }
}