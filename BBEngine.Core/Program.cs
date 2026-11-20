using BBEngine.Core;
using BBEngine.Level;
using BBEngine.Level.Objs;
using BBEngine.Level.Serialization;
using BBEngine.Math;
using BBEngine.Runtime;

using Raylib_cs;
using System.Reflection;

using r = Raylib_cs.Raylib;

create_sample_scene();

// ===================================
// Window info loading
// ===================================

DirectoryInfo dataDir = new(@".\dat");

string[] wnd = File.ReadAllLines(
    dataDir.GetFiles("window")[0].FullName
);

intnum width = int.Parse(wnd[0]);
intnum height = int.Parse(wnd[1]);

string title = wnd[2];

// ===================================
// Levels info loading
// ===================================

string levelsManagerPath = dataDir.GetFiles("levels")[0].FullName;

string[] lvs = File.ReadAllLines(levelsManagerPath);

Console.WriteLine(
    $"window: {title}\n\t{width.value} x {height.value}"
);

Console.WriteLine(
    $"\n\nwindow background color:\n\t" +
    $"{Color.FromHSV(float.Parse(lvs[0]), float.Parse(lvs[1]), float.Parse(lvs[2]))}"
);

// ===================================
// Window initialization
// ===================================

DisplayResolution.SetResolution(width, height);

r.InitWindow(width, height, title);

if (wnd.Length > 3 && wnd[3] == "fullscreen_window(true)")
    r.ToggleFullscreen();

// ===================================
// Resources loading
// ===================================

DirectoryInfo resourcesDir = new(@".\dat\rss");

// Textures
var textures = resourcesDir.GetFiles("*.png");

Dictionary<string, Texture2D> gameTextures = new();

foreach (FileInfo texture in textures)
{
    gameTextures.Add(
        Path.GetFileNameWithoutExtension(texture.Name),
        r.LoadTexture(texture.FullName)
    );
}

// Audio
var audios = resourcesDir.GetFiles("*.wav");

Dictionary<string, Sound> gameSounds = new();

foreach (FileInfo audio in audios)
{
    gameSounds.Add(
        Path.GetFileNameWithoutExtension(audio.Name),
        r.LoadSound(audio.FullName)
    );
}

// ===================================
// Assembly loading and registration
// ===================================

DirectoryInfo assemblyDir = new(
    Path.Combine(dataDir.FullName, "asb")
);

// Ensure assembly directory exists.
if (!assemblyDir.Exists)
    assemblyDir.Create();

// Keep module instances alive.
List<IGameModule> gameModules = new();

foreach (FileInfo assemblyFile in assemblyDir.GetFiles("*.dll"))
{
    string assemblyName = Path.GetFileNameWithoutExtension(
        assemblyFile.Name
    );

    Console.WriteLine(
        $"[Assembly] Loading: {assemblyName}"
    );

    // Load DLL.
    Assembly assembly = Assembly.LoadFrom(
        assemblyFile.FullName
    );

    // Fixed entry point:
    // Namespace = Assembly name
    // Class = GameModule
    Type? moduleType = assembly.GetType(
        $"{assemblyName}.GameModule"
    );

    if (moduleType == null)
    {
        Console.WriteLine(
            $"[Assembly] GameModule not found: {assemblyName}"
        );

        continue;
    }

    // Check IGameModule.
    if (!typeof(IGameModule).IsAssignableFrom(moduleType))
    {
        throw new InvalidOperationException(
            $"{assemblyName}.GameModule must implement IGameModule."
        );
    }

    // Create module instance.
    IGameModule module = (IGameModule)Activator.CreateInstance(
        moduleType
    )!;

    // Register DLL functions.
    module.Initialize();

    gameModules.Add(module);

    Console.WriteLine(
        $"[Assembly] Initialized: {assemblyName}"
    );
}

// ===================================
// Level loading
// ===================================

// DLL functions must be registered BEFORE loading Levels.
LevelManager.setDirectory(
    Path.Combine(dataDir.FullName, "lvs")
);

if (LevelManager.gameLevels.Count > 0)
{
    LevelManager.setCurrent(0);

    Console.WriteLine(
        $"[LevelManager] Current Level: {LevelManager.current?.id}"
    );
}

// ===================================
// Game loop
// ===================================

float splashTimer = float.Parse(lvs[3]);

bool isInit = true;
bool hasStarted = false;

while (!r.WindowShouldClose())
{
    float delta = r.GetFrameTime();

    r.BeginDrawing();

    r.ClearBackground(
        Color.FromHSV(
            float.Parse(lvs[0]),
            float.Parse(lvs[1]),
            float.Parse(lvs[2])
        )
    );

    if (splashTimer > 0)
    {
        if (gameTextures.TryGetValue("splash", out var splash))
            r.DrawTexture(splash, 0, 0, Color.White);

        splashTimer -= delta;
    }
    else
    {
        renderLevel(delta);
    }

    r.EndDrawing();
}

// ===================================
// Cleanup
// ===================================

// Call OnDestroy only if objects have started.
if (hasStarted && LevelManager.current != null)
{
    foreach (var obj in LevelManager.current.objects.Values)
        obj.OnDestroy();
}

// Unload textures.
foreach (var texture in gameTextures.Values)
    r.UnloadTexture(texture);

// Unload sounds.
foreach (var sound in gameSounds.Values)
    r.UnloadSound(sound);

r.CloseWindow();

// ===================================
// Functions
// ===================================

void renderLevel(float delta)
{
    if (LevelManager.current == null)
        return;

    var current = LevelManager.current;

    // Initialize objects once.
    if (isInit)
    {
        foreach (var obj in current.objects.Values)
            obj.OnStart();

        isInit = false;
        hasStarted = true;
    }

    // Render root objects only.
    // Children are rendered recursively.
    foreach (var obj in current.objects.Values)
    {
        if (string.IsNullOrEmpty(obj.parent_path))
            renderObject(obj, delta);
    }
}

void renderObject(LevelObject obj, float delta)
{
    if (obj == null)
        return;

    // Update object.
    obj.OnUpdate(delta);

    var transf = obj.global_transform;

    // Render children.
    var children = obj.get_children();

    if (children != null)
        foreach (var child in children)
            if (child != obj)
                renderObject(child, delta);

    // Rendering
    if (obj is Sprite obj_s)
    {
        if(!gameTextures[obj_s.image_key].Equals(null))
            r.DrawTextureEx
            (
                // get the texture by key, not by path!
                gameTextures[obj_s.image_key],

                // drawing position caculation
                new(
                    (int)(obj_s.global_transform.position.x.value + obj_s.sprite_offset.x.value),
                    (int)(obj_s.global_transform.position.y.value + obj_s.sprite_offset.y.value)
                ),
                
                // drawing angle setting
                obj_s.global_transform.angle,

                // height alignment
                (float)obj_s.global_transform.scale.y.value,

                // color tint
                obj_s.tint
            );
    }
}

void create_sample_scene()
{
    var sampleL = new Level()
    {
        id = 0,
        objects = new()
    };

    var obj1 = new LevelObject("chars")
    {
        transform = new(
            new(16d, 32d),
            new(1d, 1d),
            0
        )
    };

    var obj2 = new Sprite("player", "MRH1")
    {
        transform = new(
            new(64d, 300d),
            new(1d, 1d),
            0
        ),
        OnStartFunction = "player.mrh1.start",
        OnUpdateFunction = "player.mrh1.update"
    };

    obj1.add_child(obj2);

    sampleL.objects.add(obj1);
    sampleL.objects.add(obj2);

    serializer.to_file(sampleL, @"C:\level0");
}