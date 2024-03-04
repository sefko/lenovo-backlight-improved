using System.Reflection;

namespace LenovoBacklightImproved
{
    internal class KeyboardBacklightStatusControl
    {
        private const string KeyboardControl = "Keyboard_Core.KeyboardControl";
        private const string GetKeyboardBacklightStatus = "GetKeyboardBackLightStatus";
        private const string SetKeyboardBackLightStatus = "SetKeyboardBackLightStatus";

        private string dllPath = "";

        private object keyboardControlInstance;
        private MethodInfo getKeyboardBacklightStatus;
        private MethodInfo setKeyboardBacklightStatus;

        public KeyboardBacklightStatusControl(string dllFilePath)
        {
            dllPath = dllFilePath;

            Type? keyboardControlType = Assembly.LoadFile(dllFilePath).GetType(KeyboardControl);

            if (keyboardControlType != null)
            {
                object? keyboardControlInstance = Activator.CreateInstance(keyboardControlType);

                if (keyboardControlInstance != null)
                {
                    this.keyboardControlInstance = keyboardControlInstance;

                    MethodInfo? getKeyboardBacklightStatus = Array.Find(keyboardControlType.GetMethods(), method => method.Name.Equals(GetKeyboardBacklightStatus));
                    MethodInfo? setKeyboardBacklightStatus = Array.Find(keyboardControlType.GetMethods(), method => method.Name.Equals(SetKeyboardBackLightStatus));

                    if (getKeyboardBacklightStatus != null && setKeyboardBacklightStatus != null)
                    {
                        this.getKeyboardBacklightStatus = getKeyboardBacklightStatus;
                        this.setKeyboardBacklightStatus = setKeyboardBacklightStatus;
                    } else
                    {
                        throw new DllNotSupportedException($"Some keyboard backlight status functions are not available in file '{dllFilePath}'!");
                    }
                } else
                {
                    throw new DllNotSupportedException($"Could not instantiate '{KeyboardControl} type from file '{dllFilePath}'!");
                }
            } else
            {
                throw new DllNotSupportedException($"Type '{KeyboardControl}' is not available in file '{dllFilePath}'!");
            }
        }

        public byte GetStatus()
        {
            object?[] args = [ null, null ];

            getKeyboardBacklightStatus.Invoke(keyboardControlInstance, args);

            if (args[0] == null || (int)args[0]! < 0 || (int)args[0]! > 2)
            {
                throw new InvalidDataException("Status from keyboard backlight is either 'null' or has invalid value!");
            }

            return byte.CreateChecked((int)args[0]!);
        }

        public void SetStatus(byte status)
        {
            if (status > 2)
            {
                throw new ArgumentOutOfRangeException("Keyboard backlight status could be a value between 0 and 2.");
            }

            object?[] args = [(int)status, null];
            setKeyboardBacklightStatus.Invoke(keyboardControlInstance, args);
        }

        public string getDll()
        {
            return dllPath;
        }
    }
}