using BBEngine.Editor.Editor;
using BBEngine.Editor.Proj;
using BBEngine.Level.Serialization;
using Microsoft.Win32;
using System.Windows;

namespace BBEngine.Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Project proj;
            SaveFileDialog dlg = new()
            {
                DefaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "Select a directory for new project"
            };
            if (dlg.ShowDialog() == true)
                proj = new(dlg.FileName);
            else return;

            proj.ProjectVersion = App._Version;

            // Open Editor and close self
            new BBEditor(proj).Show();
            Close();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    Project p = serializer.from_file<Project>(ofd.FileName);
                    if (p != null)
                    {
                        // Open Editor
                        new BBEditor(p).Show();
                        Close();
                    }
                    else
                        MessageBox.Show("Project file is Invalid!", "Unable to load", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error when loading project\n\n{ex.Message}", "Unable to load", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();
    }
}