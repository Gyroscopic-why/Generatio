using System;

using static System.Console;

using static Generatio.GlobalVariables;
using static Generatio.CustomProcedures;
using static Generatio.DataManipulation;

namespace Generatio
{
    internal class GlobalSettings
    {

            //  Get the path to the \users\user_name_here\documents folder
            //  And then create/navigate to: Gyroscopic\Generatio\
        static public string gGalleryPath = GetPath(false, "\\Gyroscopic\\Generatio\\Gallery", false);


            //  Get the path to the \users\user_name_here\documents folder
            //  And then create/navigate to: Gyroscopic\Generatio\
        static public string gSettingsPath = GetPath(false, "\\Gyroscopic\\Generatio\\Settings", false);


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



            //  Always generates all patterns without asking for it over and over again in the final generation stage
        static public bool gAlwaysGenerate;



            //  Automatically saves the all generated patterns to the file
            //  (if you want to save them, but not to generate them again)
        static public bool gAutoSave;
            //  Enable shortcut parsing from the menu
        static public bool gUseShortcuts;




        static public void ResetSettings()
        {
            gNoSizeLimit = false;
            gIgnoreFullScreen = false;
            gShrinkPatterns = true;

            gShowInfo = true;
            gAdvInfo = false;
            
            gAlwaysGenerate = true;
            gAutoSave = true;
            gUseShortcuts = true;

            gGalleryPath  = GetPath(false, "\\Gyroscopic\\Generatio\\Gallery",  false);
            gSettingsPath = GetPath(false, "\\Gyroscopic\\Generatio\\Settings", false);
        }
        //  Reset all settings to default values
        static public void ChangeSettings()
        {
            string UserInput = "";

            while (UserInput != "0")
            {
                if (!gGeneratedPatterns) Clear();
                Write("\n\n\n\n\n\n");
                PrintLogo();

                Write("\t\t\t\t\t\t\tВыбрано: --- === Изменение настроек === ---\n\n\n");

                Write("\t\t[i]  - Текущие настройки:");

                ForegroundColor = ConsoleColor.DarkGray;   //  [1] Autosave option
                Write("\n");                               //
                if (gAutoSave)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 1 <    - Автосохранение всех сгенерированных узоров: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 1 <    - Автосохранение всех сгенерированных узоров: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [2] Always generate option
                if (gAlwaysGenerate)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 2 <    - Всегда генерировать все узоры: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 2 <    - Всегда генерировать все узоры: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [3] Basic info output option
                Write("\n");                               //
                if (gShowInfo)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 3 <    - Выводить дополнительную информацию о процессах: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 3 <    - Выводить дополнительную информацию о процессах: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [4] Advanced info output option
                if (gAdvInfo)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 4 <    - Выводить служебную информацию: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 4 <    - Выводить служебную информацию: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [5] Shrink patterns larger than max size option
                Write("\n");                               //
                if (gShrinkPatterns)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 5 <    - Уменьшать узоры при превышении максимальных размеров: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 5 <    - Уменьшать узоры при превышении максимальных размеров: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [6] Ingore full screen option
                if (gIgnoreFullScreen)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 6 <    - Игнорировать режим полного экрана: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 6 <    - Игнорировать режим полного экрана: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//


                ForegroundColor = ConsoleColor.DarkGray;   //  [7] No size limit option
                if (gNoSizeLimit)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\t\t  > 7 <    - Режим без ограничений: ");
                    ForegroundColor = ConsoleColor.Green;
                    Write("Да");
                }
                else
                {
                    Write("\n\t\t  > 7 <    - Режим без ограничений: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write("Нет");
                }
                //-----------------------------------------//



                ForegroundColor = ConsoleColor.White;                         //  [8] Gallery path
                Write("\n");                                                  //
                Write("\n\t\t  > 8 <    - Изменить путь сохранения узоров");  //
                ForegroundColor = ConsoleColor.DarkGray;                      //
                Write("\n\t\t               Текущий: " + gGalleryPath);       //
                //------------------------------------------------------------//


                ForegroundColor = ConsoleColor.White;                         //  [9] Settings path
                Write("\n\t\t  > 9 <    - Изменить путь к файлу настроек");   //
                ForegroundColor = ConsoleColor.DarkGray;                      //
                Write("\n\t\t               Текущий: " + gSettingsPath);      //
                //------------------------------------------------------------//


                ForegroundColor = ConsoleColor.White;                             //  [-] Reset settings to default
                Write("\n");                                                      //
                Write("\n\t\t  > - <    - Восстановить настройки по умолчанию");  //
                //----------------------------------------------------------------//


                Write("\n\t\t  > 0 <    - Назад");         //  Exit the settings menu
                //-----------------------------------------//


                Write("\n\n\t\t[->] - Ваш выбор: ");
                UserInput = ReadLine().Trim();

                for (int i = 0; i < UserInput.Length; i++)
                {
                    switch (UserInput[i])
                    {
                        case '1':
                            gAutoSave = !gAutoSave;
                            break;
                        case '2':
                            gAlwaysGenerate = !gAlwaysGenerate;
                            break;
                        case '3':
                            gShowInfo = !gShowInfo;
                            break;
                        case '4':
                            gAdvInfo = !gAdvInfo;
                            break;
                        case '5':
                            gShrinkPatterns = !gShrinkPatterns;
                            break;
                        case '6':
                            gIgnoreFullScreen = !gIgnoreFullScreen;
                            break;
                        case '7':
                            gNoSizeLimit = !gNoSizeLimit;
                            break;
                        case '8':
                            gGalleryPath = GetPath(true, "\\Gyroscopic\\Generatio\\Gallery", gShowInfo, false, "\t\t");
                            break;
                        case '9':
                            gSettingsPath = GetPath(true, "\\Gyroscopic\\Generatio\\Settings", gShowInfo, false, "\t\t");
                            break;
                        case '-':
                            ResetSettings();
                            break;
                    }
                }
            }
        }
        //  Changing the function as the user wants



        static public void LoadSettings()
        {
            
        }
        static public void SaveSettings()
        {
            
        }

    }
}