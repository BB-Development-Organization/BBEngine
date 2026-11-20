using Raylib_cs;
using System.Runtime.Serialization;

namespace BBEngine.Editor.Proj
{
    [DataContract]
    public class ProjectConfig
    {
        [DataMember]
        public WindowConfig WindowPart { get; set; }

        [DataMember]
        public StartupConfig StartupPart { get; set; }

        public ProjectConfig()
        {
            WindowPart = new(1280, 720, "Game Window", false);
            StartupPart = new(Color.SkyBlue, 2.2f);
        }

        public ProjectConfig(WindowConfig windowPart, StartupConfig startupPart)
        {
            WindowPart = windowPart;
            StartupPart = startupPart;
        }
    }

    [DataContract]
    public class WindowConfig
    {
        [DataMember]
        public int Height, Width;

        [DataMember]
        public string Title = "Game Window";

        [DataMember]
        public bool FullScreen = false;

        public WindowConfig(int width, int height, string title, bool fullScreen)
        {
            Height = height;
            Width = width;
            Title = title;
            FullScreen = fullScreen;
        }
    }

    [DataContract]
    public class StartupConfig
    {
        [DataMember]
        public Color Background = Color.SkyBlue;

        [DataMember]
        public float SplashSeconds = 2.2f;

        public StartupConfig(Color background, float splashSeconds)
        {
            Background = background;
            SplashSeconds = splashSeconds;
        }

        public (float, float, float) GetBackgroundHSV()
        {
            float h, s, v;
            Background.GetHSV(out h, out s, out v);
            return (h, s, v);
        }
    }
}
