using BBEngine.Level.Serialization;

namespace BBEngine.Level
{
    public static class LevelManager
    {
        private static string _gameDir;

        public static string gameDirectory
        {
            get => _gameDir;
            private set
            {
                _gameDir = value;
                loadLevels();
            }
        }

        public static Level current { get; private set; }

        public static Levels gameLevels { get; private set; } = new();

        public static Dictionary<int, FileInfo> levelFiles = new();

        private static void loadLevels()
        {
            gameLevels.Clear();
            levelFiles.Clear();

            try
            {
                foreach (FileInfo levelFile in new DirectoryInfo(_gameDir).GetFiles())
                {
                    if (!levelFile.Name.StartsWith("level"))
                        continue;

                    if (!int.TryParse(levelFile.Name.Substring(5), out int id))
                        continue;

                    try
                    {
                        var level = serializer.from_file<Level>(levelFile.FullName);

                        if (level == null)
                            continue;

                        levelFiles.Add(id, levelFile);
                        gameLevels.Add(id, level);

                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine(
                            $"[LevelManager] Loaded Level: {id}\n" +
                            $"\tObjects: {level.objects.Count}\n" +
                            $"\tFile: {levelFile.FullName}"
                        );

                        Console.ResetColor();
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"[LevelManager] Failed to load Level {id}: {ex.Message}");
                        Console.ResetColor();
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n[LevelManager] Total Levels: {gameLevels.Count}\n");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[LevelManager] Error: {ex.Message}");
                Console.ResetColor();
            }
        }

        public static void setCurrent(int id)
        {
            if (gameLevels.TryGetValue(id, out var level))
                current = level;
        }

        public static void setDirectory(string d) => gameDirectory = d;
    }
}