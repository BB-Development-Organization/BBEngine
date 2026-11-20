using BBEngine.Level.Serialization;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;

namespace BBEngine.Editor.Proj
{
    // User edit project
    [DataContract]
    public class Project
    {
        public static readonly string _Extension = ".bbp";

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string ProjectFilePath { get; set; }

        [DataMember]
        public string ProjectFolder { get; set; }

        [DataMember]
        public Version ProjectVersion { get; set; } = App._Version;


        [IgnoreDataMember]
        public DirectoryInfo EngineDir { get; protected set; }

        [IgnoreDataMember]
        public DirectoryInfo ResourcesDir { get; protected set; }

        [IgnoreDataMember]
        public DirectoryInfo ConfigDir { get; protected set; }

        [IgnoreDataMember]
        public DirectoryInfo AssemblyDir { get; protected set; }


        [DataMember]
        public string EnginePath { get; set; } = ".Eng";

        [DataMember]
        public string ResourcesPath { get; set; } = ".Res";

        [DataMember]
        public string ConfigPath { get; set; } = ".Cfg";

        [DataMember]
        public string AssemblyPath { get; set; } = ".Asb";


        public Project(string filePath)
        {
            var paths = filePath.Split('\\');
            var projName = paths[paths.Length - 1];

            ProjectName = projName;
            ProjectFilePath = Path.Combine(filePath, projName + _Extension);
            ProjectFolder = Path.Combine(filePath);

            Directory.CreateDirectory(ProjectFolder);

            EngineDir = new DirectoryInfo(Path.Combine(ProjectFolder, ".Eng"));
            ResourcesDir = new DirectoryInfo(Path.Combine(ProjectFolder, ".Res"));
            ConfigDir = new DirectoryInfo(Path.Combine(ProjectFolder, ".Cfg"));
            AssemblyDir = new DirectoryInfo(Path.Combine(ProjectFolder, ".Asb"));

            EnginePath = EngineDir.FullName;
            ResourcesPath = ResourcesDir.FullName;
            ConfigPath = ConfigDir.FullName;
            AssemblyPath = AssemblyDir.FullName;

            EngineDir.Create();
            ResourcesDir.Create();
            ConfigDir.Create();
            AssemblyDir.Create();

            string[] engineDlls = {
                "Raylib-cs.dll",
                "BBEngine.Core.dll",
                "BBEngine.Input.dll",
                "BBEngine.Math.dll",
                "BBEngine.Level.dll"
            };

            foreach (var dll in engineDlls)
            {
                if (File.Exists(dll))
                {
                    File.Copy(dll, Path.Combine(AssemblyDir.FullName, dll), true);
                }
            }

            string gameObjectsProjDir = Path.Combine(AssemblyDir.FullName, "GameObjects");

            ProcessStartInfo dotnetCreate = new()
            {
                FileName = "dotnet",
                Arguments = $"new classlib -n GameObjects -f net9.0 -o \"{gameObjectsProjDir}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(dotnetCreate))
            {
                process?.WaitForExit();
            }

            string csprojPath = Path.Combine(gameObjectsProjDir, "GameObjects.csproj");
            if (File.Exists(csprojPath))
            {
                string csprojContent = File.ReadAllText(csprojPath);

                string references = @"
  <ItemGroup>
    <Reference Include=""Raylib-cs"">
      <HintPath>..\Raylib-cs.dll</HintPath>
    </Reference>
    <Reference Include=""BBEngine.Core"">
      <HintPath>..\BBEngine.Core.dll</HintPath>
    </Reference>
    <Reference Include=""BBEngine.Input"">
      <HintPath>..\BBEngine.Input.dll</HintPath>
    </Reference>
    <Reference Include=""BBEngine.Math"">
      <HintPath>..\BBEngine.Math.dll</HintPath>
    </Reference>
    <Reference Include=""BBEngine.Level"">
      <HintPath>..\BBEngine.Level.dll</HintPath>
    </Reference>
  </ItemGroup>
</Project>";

                csprojContent = csprojContent.Replace("</Project>", references);
                File.WriteAllText(csprojPath, csprojContent);
            }

            ProjectConfig cfg = new();

            serializer.to_file(this, ProjectFilePath);
            serializer.to_file(cfg, Path.Combine(ConfigDir.FullName, "project config.cfg"));
        }
    }

    public static class Copyer
    {
        public static void CopyDirectory(string sourceDir, string destinationDir)
        {
            var dir = new DirectoryInfo(sourceDir);
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Engine Installation Not Complete!\nFolder not found: {dir.FullName}");

            Directory.CreateDirectory(destinationDir);

            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath, true);
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                CopyDirectory(subDir.FullName, newDestinationDir);
            }
        }

        public static void CopyFile(string sourceFile, string destinationFile) => File.Copy(sourceFile, destinationFile);
    }
}