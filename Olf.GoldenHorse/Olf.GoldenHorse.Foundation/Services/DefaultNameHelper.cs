using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Olf.GoldenHorse.Foundation.Services
{
    public static class DefaultNameHelper
    {
        public static string GetDefaultName(string locationToCheck, string prefix)
        {
            int[] projectNums = Directory.EnumerateDirectories(locationToCheck)
                .Select(
                    dir =>
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);

                        string pattern = @"^" + prefix + @"(\d+)$";

                        if (!Regex.IsMatch(dirInfo.Name, pattern))
                            return 0;

                        string value = Regex.Match(dirInfo.Name, pattern).Groups[1].Value;

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