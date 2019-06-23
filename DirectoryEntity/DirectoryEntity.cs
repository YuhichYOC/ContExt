/*
 *
 * DirectoryEntity.cs
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

public class DirectoryEntity {

    private readonly string enc;

    public string Path { get; private set; }

    public string Name => System.IO.Path.GetFileName(Path);

    public IList<DirectoryEntity> SubDirectories { get; private set; }

    public IList<FileEntity> Files { get; private set; }

    public DirectoryEntity(string arg1path, string arg2enc) {
        Path = arg1path;
        enc = arg2enc;
        SubDirectories = new List<DirectoryEntity>();
        Files = new List<FileEntity>();
    }

    public void Describe() {
        System.IO.DirectoryInfo self = new System.IO.DirectoryInfo(Path);
        foreach (System.IO.FileInfo f in self.GetFiles()) {
            FileEntity add = new FileEntity(f.FullName, enc);
            AddFile(add);
        }
        foreach (System.IO.DirectoryInfo d in self.GetDirectories()) {
            AddSubDirectory(Describe(d));
        }
    }

    private DirectoryEntity Describe(System.IO.DirectoryInfo arg) {
        DirectoryEntity ret = new DirectoryEntity(arg.FullName, enc);
        foreach (System.IO.FileInfo f in arg.GetFiles()) {
            FileEntity add = new FileEntity(f.FullName, enc);
            ret.AddFile(add);
        }
        foreach (System.IO.DirectoryInfo d in arg.GetDirectories()) {
            ret.AddSubDirectory(Describe(d));
        }
        return ret;
    }

    private void AddSubDirectory(DirectoryEntity arg) {
        SubDirectories.Add(arg);
    }

    private void AddFile(FileEntity arg) {
        Files.Add(arg);
    }
}