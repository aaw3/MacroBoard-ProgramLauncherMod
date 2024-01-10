using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramLauncherMod
{
    public class KeyHandling
    {
        [Flags]
        public enum KeyModifiers
        {
            None = 0,
            LShift = 1 << 0,
            RShift = 1 << 1,
            LControl = 1 << 2,
            RControl = 1 << 3,
            LAlt = 1 << 4,
            RAlt = 1 << 5
        }

        [Flags]
        public enum KeyFlags
        {
            None = 0,
            Up = 1,
            KeyE0 = 2,
            KeyE1 = 4
        }
    }
}
