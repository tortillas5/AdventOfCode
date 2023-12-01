using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllText(@"input.txt").Split('\n').Where(s => !string.IsNullOrEmpty(s)).ToList();

            int i = 2;
            Directory directory = NextDirectory(ref i, input, "/");

            PrintArbo(directory, 0);

            Console.WriteLine();

            List<Directory> Dir100k = MostSizeIs(directory, 100000);

            foreach (var item in Dir100k)
            {
                Console.WriteLine(item.Name);
            }

            Console.WriteLine();

            // Part 1
            Console.WriteLine($"Total size : {Dir100k.Sum(d => d.TotalSize())}");

            // Part 2
            long maxSize = 70000000;
            long neededSpace = 30000000;

            long freeSpace = maxSize - directory.TotalSize();

            long sizeToDelete = neededSpace - freeSpace;

            long min = GetDirectories(directory).Where(d => d.TotalSize() >= sizeToDelete).Min(d => d.TotalSize());

            Console.WriteLine($"Size of the smollest directory to delete to get enought space : {min}");
        }

        public static List<Directory> GetDirectories(Directory directory)
        {
            List<Directory> res = new List<Directory>
            {
                directory
            };

            foreach (var dir in directory.Directories)
            {
                res.AddRange(GetDirectories(dir));
            }

            return res;
        }

        public static List<Directory> MostSizeIs(Directory directory, long size)
        {
            List<Directory> res = new List<Directory>();

            if (directory.TotalSize() <= size)
            {
                res.Add(directory);
            }

            foreach (var dir in directory.Directories)
            {
                res.AddRange(MostSizeIs(dir, size));
            }

            return res;
        }

        public static void PrintArbo(Directory directory, int spaces)
        {
            Console.WriteLine($"{new string(' ', spaces)}- {directory.Name} (dir) (size : {directory.TotalSize()})");

            spaces += 2;

            foreach (var file in directory.Files)
            {
                Console.WriteLine($"{new string(' ', spaces)}- {file.Name}, {file.Size}");
            }

            foreach (var dir in directory.Directories)
            {
                PrintArbo(dir, spaces);
            }
        }

        public static Directory NextDirectory(ref int i, List<string> input, string directoryName)
        {
            Directory directory = new Directory(directoryName);

            while (i < input.Count)
            {
                if (input[i].StartsWith("$ cd .."))
                {
                    i++;
                    return directory;
                }
                else if (input[i].StartsWith("$ cd"))
                {
                    i++;
                    string dirName = input[i - 1].Split(' ')[2];

                    var dir = directory.Directories.FirstOrDefault(d => d.Name == dirName);

                    if (dir != null)
                    {
                        directory.Directories.Remove(dir);
                    }

                    Directory newDirectory = NextDirectory(ref i, input, dirName);
                    directory.Directories.Add(newDirectory);

                    if (i > input.Count)
                    {
                        return directory;
                    }

                    i--;
                }

                if (input[i].StartsWith("dir"))
                {
                    directory.Directories.Add(new Directory(input[i].Split(' ')[1]));
                }

                if (input[i].First() >= '0' && input[i].First() <= '9')
                {
                    directory.Files.Add(new File(input[i].Split(' ')[1], long.Parse(input[i].Split(' ')[0])));
                }

                i++;
            }

            i++;
            return directory;
        }
    }

    public class Directory
    {
        public Directory()
        {
            Directories = new List<Directory>();
            Files = new List<File>();
        }

        public Directory(string name)
        {
            Directories = new List<Directory>();
            Files = new List<File>();

            this.Name = name;
        }

        public string Name { get; set; }

        public List<Directory> Directories { get; set; }

        public List<File> Files { get; set; }

        public long TotalSize()
        {
            return Files.Sum(f => f.Size) + Directories.Sum(d => d.TotalSize());
        }
    }

    public class File
    {
        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; set; }

        public long Size { get; set; }
    }
}
