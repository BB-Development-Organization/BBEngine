using BBEngine.Math;
using Raylib_cs;

namespace BBEngine.Input
{
    public static class Mouse
    {
        public static bool ButtonPressed(MouseButton button) => Raylib.IsMouseButtonPressed(button);

        public static bool ButtonPressing(MouseButton button) => Raylib.IsMouseButtonDown(button);

        public static bool ButtonReleased(MouseButton button) => Raylib.IsMouseButtonReleased(button);

        public static vec2 GetPosition() => new(Raylib.GetMousePosition().X, Raylib.GetMousePosition().Y);

        public static dotnum GetX() => Raylib.GetMouseX();

        public static dotnum GetY() => Raylib.GetMouseY();

        public static vec2 GetDelta() => new(Raylib.GetMouseDelta().X, Raylib.GetMouseDelta().Y);

        public static dotnum GetWheelMove() => Raylib.GetMouseWheelMove();

        public static void SetPosition(intnum x, intnum y) => Raylib.SetMousePosition(x, y);

        public static void SetVisible(bool visible)
        {
            if (visible)
                Raylib.ShowCursor();
            else
                Raylib.HideCursor();
        }

        public static void SetLocked(bool locked)
        {
            if (locked)
                Raylib.DisableCursor();
            else
                Raylib.EnableCursor();
        }

        public static bool IsOnScreen() => Raylib.IsCursorOnScreen();
    }
}