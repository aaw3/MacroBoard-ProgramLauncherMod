using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramLauncherMod
{
    public class KeyboardData
    {
        //Passed data from "RawInputKeyboardData"

        public readonly uint ExtraInformation;
        public readonly int Flags;
        public readonly int ScanCode;
        public readonly int VirtualKey;
        public readonly uint WindowMessage;
        
        //Custom made data
        
        //The string passed when the key is pressed to tell the mod what keyboard it was sent from.
        public readonly string KeyboardAlias;
        
        //Determine if any key modifiers are presed.
        public readonly int KeyModifiers;

        public KeyboardData(string keyboardAlias, int keyModifiers, uint extraInformation, int flags, int scanCode, int virtualKey, uint windowMessage)
        {
            KeyboardAlias = keyboardAlias;
            KeyModifiers = keyModifiers;
            ExtraInformation = extraInformation;
            Flags = flags;
            ScanCode = scanCode;
            VirtualKey = virtualKey;
            WindowMessage = windowMessage;
        }

        [Flags]
        public enum RawKeyboardFlags : ushort
        {
            None = 0,
            Up = 1 << 0,
            KeyE0 = 1 << 1,
            KeyE1 = 1 << 2,
        }
    }
}
