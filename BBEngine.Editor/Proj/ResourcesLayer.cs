using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace BBEngine.Editor.Proj
{
    [DataContract]
    public class ResourcesLayer
    {
        public string layer_path { get; set; }
        public ObservableCollection<ResourceModel> resources { get; set; } = new();
        public ObservableCollection<ResourcesLayer> sub_layers { get; set; } = new();

        public ResourcesLayer()
        {
            resources.Add(new() { source_key = "splash", full_path = "splash" });
        }
    }

    [DataContract]
    public class ResourceModel
    {
        public string source_key { get; set; }
        public string full_path { get; set; }
    }
}
