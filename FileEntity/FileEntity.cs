/*
 *
 * FileEntity.cs
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

public class FileEntity {

    private readonly string enc;

    public string Path { get; private set; }

    public string Name => System.IO.Path.GetFileName(Path);

    public IList<string> Get { get; private set; }

    public int RowCount { get; private set; }

    public FileEntity(string arg1path, string arg2enc) {
        Path = arg1path;
        enc = arg2enc;
        Get = new List<string>();
        RowCount = 0;
    }

    public void Read() {
        using (System.IO.StreamReader r = new System.IO.StreamReader(Path, System.Text.Encoding.GetEncoding(enc))) {
            while (!r.EndOfStream) {
                Get.Add(r.ReadLine());
                ++RowCount;
            }
        }
    }

    public void Clear() {
        Get.Clear();
        RowCount = 0;
    }
}