using BBEngine.Level.Serialization;
using System.IO;
using System.Windows;

namespace BBEngine.Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Get the BB Editor`s application version
        /// </summary>
        public static readonly Version _Version = new("1.0.0.0");

        /// <summary>
        /// Get the BB Editor`s application name
        /// </summary>
        public static readonly string _Name = "BB Editor";

        public static readonly string _DataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BB Corporation", "BB Engine");

        public static AppSettings _Settings
        {
            get
            {
                if (File.Exists(Path.Combine(_DataDir, "settings.ini")))
                    return serializer.from_file<AppSettings>(Path.Combine(_DataDir, "settings.ini"));
                else
                    return new();
            }
            set => serializer.to_file(value, Path.Combine(_DataDir, "settings.ini"));
        }
    }

    public class AppSettings
    {
        public string DotnetVersion { get; set; }
    }
}
