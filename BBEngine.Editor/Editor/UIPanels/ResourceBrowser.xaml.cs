using BBEngine.Editor.Proj;
using BBEngine.Level.Serialization;
using System.IO;
using System.Windows.Controls;

namespace BBEngine.Editor.Editor.UIPanels
{
    /// <summary>
    /// Interaction logic for ResourceBrowser.xaml
    /// </summary>
    public partial class ResourceBrowser : UserControl
    {
        public required Project targetProject { get; set; }

        private ResourcesLayer targetLayer;

        public string CurrentPath;

        public ResourceBrowser()
        {
            InitializeComponent();

            targetLayer = serializer.from_file<ResourcesLayer>(Path.Combine(targetProject.ResourcesDir.FullName, "res layer.bbrl"));

        }

        public void RefreshView()
        {
            // Directory layer button container refreshing
            foreach (var name in CurrentPath.Split("\\"))
            {
                Button b = new()
                {
                    Content = name,
                    Tag =
                };

                DirectoryLayerButtonsContainer.Children.Add(b);
            }
        }
    }
}
