using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using static ProgramLauncherMod.KeyHandling;
using static ProgramLauncherMod.VirtualKeys;

namespace ProgramLauncherMod
{
    public class Settings
    {
        public Settings() { }

        public Settings(ProgramMacro[] programMacros)
        {
            ProgramMacros = programMacros;
        }

        public ProgramMacro[] ProgramMacros { get; /*private*/ set; }


        public static readonly Settings Default = new Settings(
            new ProgramMacro[]
            {
                new ProgramMacro(Mod.Tunnel.DefaultAlias, VKeys.APPS, new KeyModifierData[] { new KeyModifierData("", KeyModifiers.None, KeyboardFlags.None) }, "cmd.exe", "/k \"Welcome to the ProgramLauncher Mod!")
            });

        public static Settings Current = Default;
    }

    public class KeyModifierData
    {
        public KeyModifierData() { }

        public KeyModifierData(string keyboardAlias, KeyModifiers keyModifiers, KeyFlags keyFlags)
        {
            KeyboardAlias = keyboardAlias;
            KeyModifiers = keyModifiers;
        }

        public string KeyboardAlias;
        public KeyModifiers KeyModifiers;
        KeyFlags KeyFlags;
    }

    public class KeyData
    {
        public KeyData() { }

        public KeyData(string keyboardAlias, VKeys vKey)
        {
            KeyboardAlias = keyboardAlias;
            VKey = vKey;
        }

        public string KeyboardAlias;
        public VKeys VKey;
    }

    class Config
    {
        public static readonly string ConfigDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs");
        public static readonly string AppConfigDir = Path.Combine(ConfigDir, "Application");
        public static readonly string ModConfigDir = Path.Combine(ConfigDir, "Mods");
        public static readonly string ProgramLauncherModConfigDir = Path.Combine(ModConfigDir, "ProgramLauncherMod");
        public static readonly string ProgramLauncherModDefaultConfig = Path.Combine(ProgramLauncherModConfigDir, "ProgramLauncherMod.cfg");

        public static bool Exists => File.Exists(ProgramLauncherModDefaultConfig);

        public static void Load()
        {

            if (Exists)
            {
                Settings.Current = ReadConfig();
            }
            else
            {
                Directory.CreateDirectory(ProgramLauncherModConfigDir);
                SaveConfig(Settings.Current);
            }
        }

        public static Settings ReadConfig()
        {
            XmlSerializer xs = new XmlSerializer(typeof(Settings));

            try
            {

                using (TextReader tw = new StreamReader(ProgramLauncherModDefaultConfig))
                {
                    return (Settings)xs.Deserialize(tw);
                }
            }
            catch (Exception ex)
            {
                new Thread(() =>
                {
                    MessageBox.Show($"Type: {ex.GetType()}\r\nMessage: {ex.Message}\r\n\r\nPlease check the config file for any invalid/misplaced characters and reload the mods before continuing!",
                      "ProgramLauncher Mod - Execution Error @ ReadConfig (ConfigDeserialize)");
                }).Start();
            }
            return null;
        }

        public static void SaveConfig(Settings settings, Stream saveStream)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Settings));

            xs.Serialize(saveStream, settings);
        }

        public static void SaveConfig(Settings settings)
        {
            using (var f = File.Create(ProgramLauncherModDefaultConfig))
            {
                SaveConfig(settings, f);
                //return dc;
            }
        }
    }

    public class ProgramMacro
    {
        public ProgramMacro() { }
        // This code might not conform to the MacroBoard latest macro format standards, so it may not work with the latest build of MacroBoard
        public ProgramMacro(string keyboardAlias, VKeys vKey, KeyModifierData[] keyModifiers, string programLocation, string programArguments)
        {
            KeyboardAlias = keyboardAlias;
            VKey = vKey;
            KeyModifiers = keyModifiers;
            ProgramLocation = programLocation;
            ProgramArguments = programArguments;
        }

        public string KeyboardAlias;
        public VKeys VKey;
        public KeyModifierData[] KeyModifiers;
        public string ProgramLocation;
        public string ProgramArguments;

    }
}
