using System;
using System.Diagnostics;
using static ProgramLauncherMod.KeyHandling;
using static ProgramLauncherMod.VirtualKeys;

namespace ProgramLauncherMod
{
    public class Mod
    {
        public void Call(long wParam, long lParam, params dynamic[] args)
        {
            HandleKey(wParam, lParam, args);
        }


        private dynamic HostWindow;

        //Static may cause an exception.
        public static dynamic Tunnel;
        public void Init(dynamic tunnelInstance)
        {
            Tunnel = tunnelInstance;
            HostWindow = Tunnel.HostWindow;

            Config.Load();
        }

        public void Reload()
        {
            Config.Load();
        }

        public void Closing()
        {
            Config.SaveConfig(Settings.Current);
        }


        // This code might not conform to the MacroBoard latest HandkeKey format standards, so it may not work with the latest build of MacroBoard
        public void HandleKey(long wParam, long lParam, params dynamic[] args)
        {
            // Note: Working on new KeyModifiers, and adding KeyboardAliases

            //try
            //{

            KeyboardData keybdData = (KeyboardData)args[0];


            //Tunnel.WriteDebugLine("KeyModifiers (int): " + (int)keyModifiers + " - KeyModifiers Flags: - LShift: " + keyModifiers.HasFlag(KeyModifiers.LShift) + " RShift: " + keyModifiers.HasFlag(KeyModifiers.RShift) + " RControl: " + keyModifiers.HasFlag(KeyModifiers.RControl) + " LControl: " + keyModifiers.HasFlag(KeyModifiers.LControl) + " RAlt: " + keyModifiers.HasFlag(KeyModifiers.RAlt) + " LAlt: " + keyModifiers.HasFlag(KeyModifiers.LAlt));
            //}
            //catch (Exception e)
            //{
            //    Tunnel.ShowMessageBox(e.Message, "ProgramLauncherMod - Error", 0, 64);
            //}



            bool keyPressed = ((ulong)lParam & 0x80000000) == 0 ? true : false;

            if (!keyPressed)
                return;

            foreach (var keyCommands in Settings.Current.ProgramMacros)
            {
                if (keyCommands.VKey == (VKeys)wParam && keyCommands.KeyModifiers == keybdData.KeyModifiers)
                {
                    Process.Start(new ProcessStartInfo() { FileName = keyCommands.ProgramLocation, Arguments = keyCommands.ProgramArguments, UseShellExecute = true });
                }
            }
        }
    }
}
