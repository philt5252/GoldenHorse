using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class DefaultNameHelper
    {
        public enum CheckType { File, Directory };
        public static string GetDefaultName(string locationToCheck, string prefix, CheckType checkType = CheckType.Directory)
        {
            if (!Directory.Exists(locationToCheck))
                Directory.CreateDirectory(locationToCheck);


            IEnumerable<string> enumerable;

            if (checkType == CheckType.Directory)
                enumerable = Directory.EnumerateDirectories(locationToCheck);
            else
                enumerable = Directory.EnumerateFiles(locationToCheck);

            int[] projectNums = enumerable
                .Select(
                    dir =>
                    {
                        string name;

                        if (checkType == CheckType.Directory)
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo(dir);
                            name = dirInfo.Name;
                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo(dir);
                            name = fileInfo.Name.Substring(0, fileInfo.Name.Length - fileInfo.Extension.Length);
                        }
                        

                        string pattern = @"^" + prefix + @"(\d+)$";

                        if (!Regex.IsMatch(name, pattern))
                            return 0;

                        string value = Regex.Match(name, pattern).Groups[1].Value;

                        return int.Parse(value);
                    })
                .OrderBy(i => i).ToArray();

            int num = 1;
            foreach (int projectNum in projectNums)
            {
                if (projectNum == num)
                {
                    num++;
                }
                else
                {
                    break;
                }
            }
            string newName = prefix + num;
            return newName;
        }
    }
}