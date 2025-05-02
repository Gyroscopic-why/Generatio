using System;
using System.Text;
using System.Collections.Generic;

using static System.Console;


using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;
using static Generatio.DataManipulation;



namespace Generatio
{
    internal class GlobalSettings
    {

            //  Get the path to the \users\user_name_here\documents folder
            //  And then create/navigate to: Gyroscopic\Generatio\
        static public string gGalleryPath = GetPath(false, true, "\\Gyroscopic\\Generatio\\Gallery", false);


            //  Get the path to the \users\user_name_here\documents folder
            //  And then create/navigate to: Gyroscopic\Generatio\
        static public string gSettingsPath = GetPath(false, true, "\\Gyroscopic\\Generatio\\Settings", false);



        //------------------------- Stored settings variables -------------------------------------------------//


            //  Makes the size function ignore the upper limit (Max = from MaxWindowSize to int.MaxValue)
        static public bool gNoSizeLimit;
            //  Makes the program ignore your window size 
        static public bool gIgnoreFullScreen;
            //  Shrinks the patterns if a critical error occurs to the size of the screen
        static public bool gShrinkPatterns;


            //  Writes some info about the programs processes
        static public bool gShowInfo;
            //  Writes additional debug dev info
        static public bool gAdvInfo;



            //  Always generates all patterns without asking for it in the final generation stage
        static public bool gAlwaysGenerate;



            //  Automatically saves the all generated patterns to the file
            //  (if you want to save them, but not to generate them again)
        static public bool gAutoSave;
            //  Automatically assigns new names to unnamed patterns
        static public bool gAutonameUnnamedPatterns;




        static public void ResetSettings()
        {
            gAlwaysGenerate = true;
            gAutoSave = true;
            gAutonameUnnamedPatterns = true;

            gShowInfo = false;
            gAdvInfo = false;

            gShrinkPatterns = true;
            gIgnoreFullScreen = false;
            gNoSizeLimit = false;



            gGalleryPath  = GetPath(false, true, "\\Gyroscopic\\Generatio\\Gallery",  false);
            gSettingsPath = GetPath(false, true, "\\Gyroscopic\\Generatio\\Settings", false);
        }
             //  Reset all settings to default values

        static public void ChangeSettings()
        {
            string UserInput = "";

            while (UserInput != "0")
            {
                ResetUI(!gIgnoreFullScreen, true);
                Write("\n\t\t\t\t\t\tВыбрано: --- === Изменение настроек === ---\n\n\n");

                Write("\t\t[i]  - Текущие настройки:");


                ForegroundColor = ConsoleColor.DarkGray;   //  [1] Always generate option
                if (gAlwaysGenerate)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 1 <    - Всегда генерировать все узоры: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 1 <    - Всегда генерировать все узоры: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [2] Autosave option
                if (gAutoSave)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 2 <    - Автосохранение всех сгенерированных узоров: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 2 <    - Автосохранение всех сгенерированных узоров: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [3] Automatically assign names to unnamed patterns
                if (gAutonameUnnamedPatterns)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 3 <    - Автоматически называть безымянные сохранённые узоры: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 3 <    - Автоматически называть безымянные сохранённые узоры: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//





                ForegroundColor = ConsoleColor.DarkGray;   //  [4] Basic info output option
                Write("\n");                               //
                if (gShowInfo)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 4 <    - Выводить дополнительную информацию о процессах: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 4 <    - Выводить дополнительную информацию о процессах: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [5] Advanced info output option
                if (gAdvInfo)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 5 <    - Выводить служебную информацию: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 5 <    - Выводить служебную информацию: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [6] Shrink patterns larger than max size option
                Write("\n");                               //
                if (gShrinkPatterns)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 6 <    - Уменьшать узоры при превышении максимальных размеров: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 6 <    - Уменьшать узоры при превышении максимальных размеров: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [7] Ingore full screen option
                if (gIgnoreFullScreen)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 7 <    - Игнорировать режим полного экрана: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 7 <    - Игнорировать режим полного экрана: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [8] No size limit option
                if (gNoSizeLimit)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 8 <    - Режим без ограничений: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 8 <    - Режим без ограничений: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//



                ForegroundColor = ConsoleColor.White;                         //  [9] Gallery path
                Write("\n");                                                  //
                Write("\n\t\t  > 9 <    - Изменить путь сохранения узоров");  //
                ForegroundColor = ConsoleColor.DarkGray;                      //
                Write("\n\t\t               Текущий: " + gGalleryPath);       //
                //------------------------------------------------------------//


                ForegroundColor = ConsoleColor.White;                         //  [=] Settings path
                Write("\n\t\t  > = <    - Изменить путь к файлу настроек");   //
                ForegroundColor = ConsoleColor.DarkGray;                      //
                Write("\n\t\t               Текущий: " + gSettingsPath);      //
                //------------------------------------------------------------//


                ForegroundColor = ConsoleColor.White;                             //  [-] Reset settings to default
                Write("\n");                                                      //
                Write("\n\t\t  > - <    - Восстановить настройки по умолчанию");  //
                //----------------------------------------------------------------//


                Write("\n\t\t  > 0 <    - Назад");         //  Exit the settings menu
                //-----------------------------------------//


                //  Get the user input
                Write("\n\n\t\t[->] - Ваш выбор: ");
                UserInput = ReadLine().Trim();

                //  Parse choice
                for (int i = 0; i < UserInput.Length; i++)
                {
                    //  For the situation of a path change
                    //  Saving the previous state to not revert to the default state
                    string _previousValidPath;

                    switch (UserInput[i])
                    {
                        case '1':
                            gAlwaysGenerate = !gAlwaysGenerate;
                            break;
                        case '2':
                            gAutoSave = !gAutoSave;
                            break;
                        case '3':
                            gAutonameUnnamedPatterns = !gAutonameUnnamedPatterns;
                            break;
                        case '4':
                            gShowInfo = !gShowInfo;
                            break;
                        case '5':
                            gAdvInfo = !gAdvInfo;
                            break;
                        case '6':
                            gShrinkPatterns = !gShrinkPatterns;
                            break;
                        case '7':
                            gIgnoreFullScreen = !gIgnoreFullScreen;
                            break;
                        case '8':
                            gNoSizeLimit = !gNoSizeLimit;
                            break;
                        case '9':
                            _previousValidPath = gGalleryPath;
                            gGalleryPath = GetPath(true, false, "\\Gyroscopic\\Generatio\\Gallery", gShowInfo, false, "\t\t");
                            if (gGalleryPath == null) gGalleryPath = _previousValidPath;
                            break;
                        case '=':
                            _previousValidPath = gSettingsPath;
                            gSettingsPath = GetPath(true, false, "\\Gyroscopic\\Generatio\\Settings", gShowInfo, false, "\t\t");
                            if (gSettingsPath == null) gSettingsPath = _previousValidPath;
                            break;
                        case '-':
                            ResetSettings();
                            break;
                    }
                }
            }

            //  On exit, save the new settings
            SaveSettings();
        }
             //  Changing the settings according to the user input


        

        static public void LoadSettings()
        {
            try
            {
                //  Read the binary encoded settings data
                List<byte> _data = ReadBinaryData(gSettingsPath, "Generatio.settings", gShowInfo, false, "\t\t", "\n");

                //  Transform the first byte to our boolean settings
                gAlwaysGenerate           = (_data[0] & 0b00000001) != 0;
                gAutoSave                 = (_data[0] & 0b00000010) != 0;
                gAutonameUnnamedPatterns  = (_data[0] & 0b00000100) != 0;

                gShowInfo                 = (_data[0] & 0b00001000) != 0;
                gAdvInfo                  = (_data[0] & 0b00010000) != 0;

                gShrinkPatterns           = (_data[0] & 0b00100000) != 0;
                gIgnoreFullScreen         = (_data[0] & 0b01000000) != 0;
                gNoSizeLimit              = (_data[0] & 0b10000000) != 0;


                //  Transform the rest of the bytes to our gallery path
                byte[] _gPathBytes = ToByteArray(_data, 1);
                gGalleryPath = Encoding.UTF8.GetString(_gPathBytes).Substring(1);

                if (gShowInfo) Write("\t\tСохранение настроек успешно загружено!\n\n");
            }
            catch (Exception e)
            {
                if(gShowInfo)
                {
                    Write("\n\t\tНе удалось загрузить сохранённые настройки");
                    Write("\n\t\tКод ошибки: " + e);
                }
            }
        }
             //  Load the saved settings data, and decode it

        static public void SaveSettings()
        {
            //  8 bits (1 byte) for 8 settings (binary number)
            byte[] _encodedBoolSettings = new byte[] { 0b00000000 };

            
            if (gAlwaysGenerate)           _encodedBoolSettings[0] |= 0b00000001;   //  1 bit
            if (gAutoSave)                 _encodedBoolSettings[0] |= 0b00000010;   //  2 bit
            if (gAutonameUnnamedPatterns)  _encodedBoolSettings[0] |= 0b00000100;   //  3 bit

            if (gShowInfo)                 _encodedBoolSettings[0] |= 0b00001000;   //  4 bit
            if (gAdvInfo)                  _encodedBoolSettings[0] |= 0b00010000;   //  5 bit

            if (gShrinkPatterns)           _encodedBoolSettings[0] |= 0b00100000;   //  6 bit
            if (gIgnoreFullScreen)         _encodedBoolSettings[0] |= 0b01000000;   //  7 bit
            if (gNoSizeLimit)              _encodedBoolSettings[0] |= 0b10000000;   //  8 bit


            //  Get the data into a more convinient format of a List of byte[] arrays
            List<byte[]> _settingsData = new List<byte[]>
            {
                //  a byte number representing our 8 binary settings
                _encodedBoolSettings,

                //  Transform the paths to byte[] arrays
                Encoding.UTF8.GetBytes(gGalleryPath)
            };

            //  Save the encoded settings to the special binary Generatio.settings file
            SaveBinaryData(gSettingsPath, "Generatio.settings", _settingsData, false, "", gShowInfo, false, "\t\t", "\n");
        }
             //  Encode current settings data, and save it
    }
}