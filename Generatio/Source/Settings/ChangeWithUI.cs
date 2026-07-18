using System;

using static System.Console;
using GyroscopicDataLibrary;



namespace Generatio
{
    internal static partial class Settings
    {
        static public void Change()
        {
            string choice = "", previousValidPath;

            while (choice != "0")
            {
                UI.ResetUI(true);
                ShowOptions();
                choice = GetUserInput();

                for (var i = 0; i < choice.Length; i++)
                {
                    switch (choice[i])
                    {
                        case '1': alwaysDrawAll = !alwaysDrawAll; break;
                        case '2': autoSaveAll   = !autoSaveAll;   break;
                        case '3': autoNameBlankPatterns = !autoNameBlankPatterns; break;
                        case '4': showBasicInfo = !showBasicInfo; break;
                        case '5': showDevInfo   = !showDevInfo;   break;
                        case '6': shrinkOversized  = !shrinkOversized;  break;
                        case '7': ignoreFullScreen = !ignoreFullScreen; break;
                        case '8': ignoreSizeLimit  = !ignoreSizeLimit;  break;

                        case '9':
                            previousValidPath = pathToGallery;
                            pathToGallery = BetterDataIO.GetPath(true, false, "\\Gyroscopic\\Generatio\\Gallery", showBasicInfo, false, "\t\t");
                            pathToGallery ??= previousValidPath;
                            break;

                        case '=':
                            previousValidPath = pathToSettings;
                            pathToSettings = BetterDataIO.GetPath(true, false, "\\Gyroscopic\\Generatio\\Settings", showBasicInfo, false, "\t\t");
                            pathToSettings ??= previousValidPath;
                            break;

                        case '-':
                            Reset();
                            break;
                    }
                }
            }

            Save();
        }

        static private string GetUserInput()
        {
            Write("\n\n\t\t[->] - Ваш выбор: ");
            return ReadLine()?.Trim() ?? "";
        }
        static private void ShowOptions()
        {
            Write("\n\t\t\t\t\t\tВыбрано: --- === Изменение настроек === ---\n\n\n");
            Write("\t\t[i]  - Текущие настройки:");

            ShowSettingsAlwaysDrawAll();
            ShowSettingsAutoSaveAll();
            ShowSettingsAutoNameBlankPatterns();

            ShowSettingsShowBasicInfo();
            ShowSettingsShowDevInfo();

            ShowSettingsShrinkOversized();
            ShowSettingsIgnoreFullScreen();
            ShowSettingsIgnoreSizeLimit();

            ShowSettingsPathToGallery();
            ShowSettingsPathToSettings();

            ShowSettingResetSettings();
            ShowSettingExit();
        }

        static private void ShowSettingsAlwaysDrawAll()
        {
            if (alwaysDrawAll)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\t  > 1 <    - Всегда генерировать все узоры: ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t  > 1 <    - Всегда генерировать все узоры: ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }
        static private void ShowSettingsAutoSaveAll()
        {
            if (autoSaveAll)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\t  > 2 <    - Автосохранение всех сгенерированных узоров: ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t  > 2 <    - Автосохранение всех сгенерированных узоров: ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }
        static private void ShowSettingsAutoNameBlankPatterns()
        {
            if (autoNameBlankPatterns)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\t  > 3 <    - Автоматическая генерация имён для пустых узоров: ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t  > 3 <    - Автоматическая генерация имён для пустых узоров: ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }

        static private void ShowSettingsShowBasicInfo()
        {
            if (showBasicInfo)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\n\t\t  > 4 <    - Дополнительные уведомления:    ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\n\t\t  > 4 <    - Дополнительные уведомления:    ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }
        static private void ShowSettingsShowDevInfo()
        {
            if (showDevInfo)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\t  > 5 <    - Показать служебную информацию: ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t  > 5 <    - Показать служебную информацию: ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }

        static private void ShowSettingsShrinkOversized()
        {
            if (shrinkOversized)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\n\t\t  > 6 <    - Уменьшать слишком большие узоры:  ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\n\t\t  > 6 <    - Уменьшать слишком большие узоры:  ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }
        static private void ShowSettingsIgnoreFullScreen()
        {
            if (ignoreFullScreen)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\t  > 7 <    - Игнорировать полноэкранный режим: ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t  > 7 <    - Игнорировать полноэкранный режим: ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }
        static private void ShowSettingsIgnoreSizeLimit()
        {
            if (ignoreSizeLimit)
            {
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\t  > 8 <    - Игнорировать ограничение размера узоров: ");
                ForegroundColor = ConsoleColor.Green;
                Write("Да");
            }
            else
            {
                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t  > 8 <    - Игнорировать ограничение размера узоров: ");
                ForegroundColor = ConsoleColor.Red;
                Write("Нет");
            }
        }

        static private void ShowSettingsPathToGallery()
        {
            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\n\t\t  > 9 <    - Изменить путь сохранения узоров");
            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\t\t               Текущий: " + pathToGallery);
        }
        static private void ShowSettingsPathToSettings()
        {
            ForegroundColor = ConsoleColor.White;
            Write("\n\t\t  > = <    - Изменить путь к файлу настроек");
            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\t\t               Текущий: " + pathToSettings);
        }

        static private void ShowSettingResetSettings()
        {
            ForegroundColor = ConsoleColor.White;
            Write("\n\n\n\t\t  > - <    - Восстановить настройки по умолчанию");
        }
        static private void ShowSettingExit()
            => Write("\n\t\t  > 0 <    - Назад");
    }
}