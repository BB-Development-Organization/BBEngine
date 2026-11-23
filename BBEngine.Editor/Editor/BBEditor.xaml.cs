using BBEngine.Editor.Proj;
using System.Windows;

namespace BBEngine.Editor.Editor
{
    /// <summary>
    /// Interaction logic for BBEditor.xaml
    /// </summary>
    public partial class BBEditor : Window
    {
        public Project EditTarget { get; set; }

        public BBEditor(Project p)
        {
            InitializeComponent();

            // Events registering operation

            EditTarget = p;
            Title = $"BB Engine - Editor        ({p.ProjectName})";
        }

        // Events
        private void QuitToLauncher(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void QuitApp(object sender, RoutedEventArgs e) =>
            App.Current.Shutdown();
    }
}
