namespace AoC
{
    class Day07
    {
        public static string currentFolder = "/";
        public static void Solution()
        {
            var contents = Utils.ReadContents(7, false);

            var sumInventories = new List<int>();

            var currentFolderPath = "/";
            var root = new AoCFolder("/");
            var currentFolder = root;

            var folderRegister = new List<AoCFolder>() { root };

            var data = contents.Split("\n");
            foreach (var line in data)
            {
                switch (line.Split(" "))
                {
                    case ["$", "cd", var targetFolder]:
                        {
                            if (targetFolder == "/")
                            {
                                currentFolderPath = "/";
                                currentFolder = root;
                            }
                            else if (targetFolder == "..")
                            {
                                var parts = currentFolderPath.Split("/");
                                currentFolderPath = string.Join("/", parts.Take(parts.Length - 1));
                                currentFolder = currentFolder.parent ?? root;
                            }
                            else
                            {
                                currentFolderPath += $"{targetFolder}/";
                                currentFolder = currentFolder.CreateChild(targetFolder);

                                // This could lead to duplicate folders potentially
                                folderRegister.Add(currentFolder);
                            }
                            break;
                        }
                    case ["$", "ls"]: break;
                    case ["dir", var dirName]:
                        {
                            currentFolder.CreateChild(dirName);
                            break;
                        }
                    case [var fileSize, var fileName]:
                        {
                            currentFolder.CreateFile(fileName, fileSize);
                            break;
                        };
                    default: throw new InvalidDataException();
                };
            }


            // Part 1
            var sumSizeSmallFolders = folderRegister.Where(f => f.Size <= 100000).Sum(f => f.Size);
            Console.WriteLine($"Total size of folders with at most 100000 is {sumSizeSmallFolders}.");

            // Part 2
            var neededSpace = 30000000 - (70000000 - root.Size);
            var bestFolder = folderRegister.Where(f => f.Size >= neededSpace).OrderBy(f => f.Size).First();
            Console.WriteLine($"Best folder to delete is {bestFolder.fullPath} with a size of {bestFolder.Size}.");
        }
    }

    class AoCFolder
    {
        public string name;
        public AoCFolder? parent = null;
        public Dictionary<string, AoCFolder> children = new Dictionary<string, AoCFolder>();
        public Dictionary<string, int> files = new Dictionary<string, int>();

        public AoCFolder(string name, AoCFolder? parent = null)
        {
            this.name = name;
            this.parent = parent;
        }

        public AoCFolder CreateChild(string name)
        {
            if (this.children.ContainsKey(name))
            {
                return this.children[name];
            }

            var folder = new AoCFolder(name, this);
            this.children.Add(name, folder);
            return folder;
        }

        public void CreateFile(string name, string size)
        {
            var fileSize = int.Parse(size);
            this.files.Add(name, fileSize);
        }

        public int Size
        {
            get
            {
                var size = 0;
                foreach (var file in files)
                {
                    size += file.Value;
                }
                foreach (var child in children)
                {
                    size += child.Value.Size;
                }
                return size;
            }
        }

        public string fullPath
        {
            get
            {
                var path = "";
                var pointer = this;
                while (pointer.parent != null)
                {
                    path = $"{pointer.name}/{path}";
                    pointer = pointer.parent;
                }
                return $"/{path}";
            }
        }
    }
}