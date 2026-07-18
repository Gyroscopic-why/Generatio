using System;
using System.Text;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using static System.Console;


using AVcontrol;
using GyroscopicDataLibrary;



namespace Generatio
{
    internal static partial class Settings
    {
        static public void Reset()
        {
            alwaysDrawAll = true;
            autoSaveAll   = true;
            autoNameBlankPatterns = true;

            showBasicInfo = false;
            showDevInfo   = false;

            shrinkOversized  = true;
            ignoreFullScreen = false;
            ignoreSizeLimit  = false;

            pathToGallery  = BetterDataIO.GetPath(false, true, "\\Gyroscopic\\Generatio\\Gallery", false);
            pathToSettings = BetterDataIO.GetPath(false, true, "\\Gyroscopic\\Generatio\\Settings", false);
        }



        static public void Load()
        {
            try
            {
                List<Byte> binData = BetterDataIO.ReadBinaryData(pathToSettings, "Generatio.settings", showBasicInfo, false, "\t\t", "\n");

                alwaysDrawAll = (binData[0] & 0b00000001) != 0;
                autoSaveAll   = (binData[0] & 0b00000010) != 0;
                autoNameBlankPatterns = (binData[0] & 0b00000100) != 0;

                showBasicInfo = (binData[0] & 0b00001000) != 0;
                showDevInfo   = (binData[0] & 0b00010000) != 0;

                shrinkOversized  = (binData[0] & 0b00100000) != 0;
                ignoreFullScreen = (binData[0] & 0b01000000) != 0;
                ignoreSizeLimit  = (binData[0] & 0b10000000) != 0;

                pathToGallery = FromBinary.Utf8(binData[1..]);

                if (showBasicInfo) Write("\t\tСохранение настроек успешно загружено!\n\n");
            }
            catch (Exception e)
            {
                if (showBasicInfo)
                {
                    Write("\n\t\tНе удалось загрузить сохранённые настройки");
                    Write("\n\t\tКод ошибки: " + e);
                }
            }
        }

        static public void Save()
        {
            Byte encodedSettings = (Byte)(
                (Unsafe.As<bool, Byte>(ref alwaysDrawAll)    << 0) |
                (Unsafe.As<bool, Byte>(ref autoSaveAll)      << 1) |
                (Unsafe.As<bool, Byte>(ref autoNameBlankPatterns) << 2) |
                (Unsafe.As<bool, Byte>(ref showBasicInfo)    << 3) |
                (Unsafe.As<bool, Byte>(ref showDevInfo)      << 4) |
                (Unsafe.As<bool, Byte>(ref shrinkOversized)  << 5) |
                (Unsafe.As<bool, Byte>(ref ignoreFullScreen) << 6) |
                (Unsafe.As<bool, Byte>(ref ignoreSizeLimit)  << 7));

            List<Byte> settingsData =
            [
                encodedSettings,
                .. Encoding.UTF8.GetBytes(pathToGallery)
            ];

            BetterDataIO.SaveBinaryData(pathToSettings, "Generatio.settings", settingsData, false, "", showBasicInfo, false, "\t\t", "\n");
        }
    }
}