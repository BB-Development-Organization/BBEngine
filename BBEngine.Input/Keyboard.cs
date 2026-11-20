using BBEngine.Math;
using Raylib_cs;

namespace BBEngine.Input
{
    public static class Keyboard
    {
        public static bool KeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);

        public static bool KeyPressing(KeyboardKey key) => Raylib.IsKeyDown(key);

        public static bool KeyReleased(KeyboardKey key) => Raylib.IsKeyReleased(key);

        public static intnum GetPressedKeyCount() => Raylib.GetKeyPressed();

        public static intnum GetCharPressed() => Raylib.GetCharPressed();

        public static void Clear()
        {
            while (Raylib.GetKeyPressed() != 0) { }
            while (Raylib.GetCharPressed() != 0) { }
        }
    }
}