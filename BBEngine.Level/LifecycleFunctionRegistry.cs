using BBEngine.Level;

namespace BBEngine.Runtime
{
    public static class LifecycleFunctionRegistry
    {
        private static readonly Dictionary<string, Action<LevelObject>> startFunctions = new();
        private static readonly Dictionary<string, Action<LevelObject, float>> updateFunctions = new();
        private static readonly Dictionary<string, Action<LevelObject>> destroyFunctions = new();

        // Register OnStart
        public static void register_start(
            string id,
            Action<LevelObject> function)
        {
            startFunctions.Add(id, function);
        }

        // Register OnUpdate
        public static void register_update(
            string id,
            Action<LevelObject, float> function)
        {
            updateFunctions.Add(id, function);
        }

        // Register OnDestroy
        public static void register_destroy(
            string id,
            Action<LevelObject> function)
        {
            destroyFunctions.Add(id, function);
        }

        // Invoke OnStart
        public static void invoke_start(string id, LevelObject obj)
        {
            if (startFunctions.TryGetValue(id, out var function))
                function(obj);
        }

        // Invoke OnUpdate
        public static void invoke_update(
            string id,
            LevelObject obj,
            float delta)
        {
            if (updateFunctions.TryGetValue(id, out var function))
                function(obj, delta);
        }

        // Invoke OnDestroy
        public static void invoke_destroy(string id, LevelObject obj)
        {
            if (destroyFunctions.TryGetValue(id, out var function))
                function(obj);
        }
    }
}