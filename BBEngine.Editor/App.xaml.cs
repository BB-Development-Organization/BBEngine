using System.Configuration;
using System.Data;
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
    }
}
