// file used from https://dusted.codes/dotenv-in-dotnet
// minor modifications
namespace Dev_Blog
{
    using System;
    using System.IO;

    public static class DotEnv
    {
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    2,
                    StringSplitOptions.RemoveEmptyEntries);

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }
}