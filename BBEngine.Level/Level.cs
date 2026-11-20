using BBEngine.Level.Objs;
using BBEngine.Math;
using BBEngine.Runtime;
using System.Runtime.Serialization;

namespace BBEngine.Level
{
	public class Levels : Dictionary<int, Level>
	{

	}

	public class LevelObjects : Dictionary<string, LevelObject>
	{
		public void add(LevelObject o)
		{
			Add(Path.Combine(o.path ?? "", o.name ?? o.GetType().Name), o);
		}
	}

	[DataContract]
	[KnownType(typeof(LevelObjects))]
    [KnownType(typeof(LevelObject))]
    public class Level
	{
		[DataMember]
		public int id { get; set; }

        [DataMember]
        public LevelObjects objects { get; set; } = new();
	}

	[DataContract]
    [KnownType(typeof(transform))]
    [KnownType(typeof(vec2))]
    [KnownType(typeof(Sprite))]
    [KnownType(typeof(string))]
    public class LevelObject
	{
        public LevelObject(string name) =>
			this.name = name;

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string path { get; set; }


        [DataMember]
        public string parent_path { get; set; }

        [DataMember]
        public List<string> children_path { get; set; } = new();

        [DataMember]
        public transform transform { get; set; }

        [DataMember]
        public transform global_transform { get; set; }

        public void add_child(LevelObject o)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));

            if (ReferenceEquals(this, o))
                throw new InvalidOperationException("An object cannot be its own child.");

            var fullPath = Path.Combine(path ?? "", name ?? GetType().Name);

            var childPath = Path.Combine(fullPath, o.name ?? o.GetType().Name);

            o.parent_path = fullPath;
            o.path = fullPath;

            if (!children_path.Contains(childPath))
                children_path.Add(childPath);

            if (LevelManager.current != null) LevelManager.current.objects.add(o);
        }

        public LevelObject? get_parent() => find_object_by_path(parent_path) ?? null;
		public List<LevelObject>? get_children()
		{
			var r = new List<LevelObject>();

            foreach (var child in children_path)
            {
				r.Add(find_object_by_path(child) ?? null);
            }

			if (r.Count == 0)
				return null;
            return r;
		}

        public static LevelObject? find_object_by_path(string? path)
        {
            if (string.IsNullOrEmpty(path))
                return null;

            if (LevelManager.current == null)
                return null;

            LevelManager.current.objects.TryGetValue(path, out var obj);

            return obj;
        }

        public virtual void OnStart()
        {
            if (!string.IsNullOrEmpty(OnStartFunction))
                LifecycleFunctionRegistry.invoke_start(OnStartFunction, this);
        }

        public virtual void OnUpdate(float delta)
        {
            var parent = get_parent();

            if (parent != null)
                global_transform = parent.global_transform >>> transform;
            else
                global_transform = transform;

            if (!string.IsNullOrEmpty(OnUpdateFunction))
                LifecycleFunctionRegistry.invoke_update(
                    OnUpdateFunction,
                    this,
                    delta
                );
        }

        public virtual void OnDestroy()
        {
            if (!string.IsNullOrEmpty(OnDestroyFunction))
                LifecycleFunctionRegistry.invoke_destroy(OnDestroyFunction, this);
        }

        public static implicit operator bool(LevelObject target) => target != null;

        [DataMember]
        public string OnStartFunction { get; set; } = "";

        [DataMember]
        public string OnUpdateFunction { get; set; } = "";

        [DataMember]
        public string OnDestroyFunction { get; set; } = "";
    }
}