using BBEngine.Math;
using Raylib_cs;

namespace BBEngine.Input
{
    public static class Gamepad
    {
        public static bool IsConnected(intnum id) => Raylib.IsGamepadAvailable(id);

        public static string GetName(intnum id) => Raylib.GetGamepadName_(id);

        public static bool ButtonPressed(intnum id, GamepadButton but) => Raylib.IsGamepadButtonPressed(id, but);

        public static bool ButtonPressing(intnum id, GamepadButton but) => Raylib.IsGamepadButtonDown(id, but);

        public static bool ButtonReleased(intnum id, GamepadButton but) => Raylib.IsGamepadButtonReleased(id, but);

        public static void Vibration(intnum id, dotnum left, dotnum right, dotnum durationSeconds) =>
            Raylib.SetGamepadVibration(id, (float)left.value, (float)right.value, (float)durationSeconds.value);

        public static intnum GetGamepadCount()
        {
            intnum count = 0;

            for (intnum i = 0; i < 8; i++)
            {
                if (IsConnected(i))
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Get gamepad left stick axis
        /// Horizontal: 1 = right, Vertical: 1 = down
        /// </summary>
        /// <param name="id">Target gamepad ID</param>
        /// <returns>return a vector of gamepad left axis</returns>
        public static vec2 GetLeftAxis(intnum id) => new(Raylib.GetGamepadAxisMovement(id, GamepadAxis.LeftX), Raylib.GetGamepadAxisMovement(id, GamepadAxis.LeftY));

        /// <summary>
        /// Get gamepad right stick axis
        /// Horizontal: 1 = right, Vertical: 1 = down
        /// </summary>
        /// <param name="id">Target gamepad ID</param>
        /// <returns>return a vector of gamepad right axis</returns>
        public static vec2 GetRightAxis(intnum id) => new(Raylib.GetGamepadAxisMovement(id, GamepadAxis.RightX), Raylib.GetGamepadAxisMovement(id, GamepadAxis.RightY));
    }
}