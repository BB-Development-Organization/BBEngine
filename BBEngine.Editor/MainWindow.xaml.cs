using BBEngine.Editor.Proj;
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
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            if (ofd.ShowDialog() == true)
            {

            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();
    }
}