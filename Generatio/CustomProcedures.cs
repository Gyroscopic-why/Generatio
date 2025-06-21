using System;
using System.Collections.Generic;

using static System.Console;


using static Generatio.GalleryLogic;
using static Generatio.PatternSource;
using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;
using static Generatio.CustomFunctions;


using GyroscopicDataLibrary;



namespace Generatio
{
    internal class CustomProcedures
    {
        //-----------------------  Action procedures  ----------------------------------------------------------//


        static public void ForceFullScreen(int X = 0, int Y = 0)
        {
            UInt16 Counter = 0;
            if (X == 0) X = LargestWindowWidth / 13;
            if (Y == 0) Y = LargestWindowHeight / 26;
            if (!gNoSizeLimit)
            {
                if (WindowWidth < X || WindowHeight < Y)
                {
                    Write("\t\tДля продолжения работы программы требуется увеличить разрешение экрана\n");
                    Write("\t\tнастоятельно рекомендуется перейти в полноэкранный режим.\n");
                    Write("\t\tДля этого нажмите fullscreen/РАЗВЕРНУТЬ или клавишу F11/(Fn+F11)\n");
                    Write("\t\tПосле увеличения размера окна, нажмите любую кнопку для продолжения\n");
                }
                while (WindowWidth <= X || WindowHeight <= Y)
                {
                    Counter++;
                    if (Counter > 65001) Counter -= 65000;
                    ForegroundColor = ConsoleColor.White;
                    Write("\n\n\t\tНеобходимый размер окна: не менее чем " + X + "x" + Y + "\n\t\tТекущий размер: ");
                    ForegroundColor = ConsoleColor.Red;
                    Write(WindowWidth + "x" + WindowHeight);
                    ForegroundColor = ConsoleColor.Black;
                    ReadKey();
                }
                if (Counter > 1) Write("\n\n");
                ForegroundColor = ConsoleColor.White;
            }
        }
             //  Force the user to enlarge the window to prevent limitations of the size of the patterns


        static public void ResetAll()
        {
            //  Reset settings do default (fail save)
            ResetSettings();

            //  Load previous save of the settings
            LoadSettings();


            //  Update the gallery with patterns
            UpdateStockGallery();
            UpdateUserGallery();
        }
        //  Reset all the settings and patterns and load the previous save




        //----------------  Procedures exclusively meant for printing info  -----------------------------------//


        static public void ResetUI(bool forceFullScreen, bool autoContinue)
        {
            //  Wait for the user (optional)
            if (!autoContinue)
            {
                Write("\n\t\tГотово! Нажмите любую кнопку чтобы продолжить: ");
                ReadKey();
            }

            //  Force FullScreen (optional)
            if (forceFullScreen) ForceFullScreen(70, 30);


            //  Clear the console without losing information
            for (int i = 0; i < WindowHeight; i++) Write("\n");
            Clear();

            //  Print the logo
            Write("\n\n\n\n\n\n");
            PrintLogo();
        }
             //  Simple UI formatting (resetting the formatting)


        static public void ResetSizeUI(string sizeType, UInt16 maxSize, uint realMax, UInt16 last = 0)
        {
            //  Reset UI, dont wait for additional input, print info
            ResetUI(!gIgnoreFullScreen, true);
            Write("\n\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");

            if(last != 0) Write("\t\t[=]  - Выбранная ширина: " + last + "\n\n");


            //  Calculate margin for the gallery output
            string margin = "       ";
            UInt16 tempBuffer = maxSize;
            while (tempBuffer > 0)
            {
                if (margin.Length > 0) margin = margin.Remove(0, 1);
                tempBuffer /= 10;
            }



            //  Print earlier output UI
            Write("\n\t\t[?]  - Выберете " + sizeType + "у узора:");

            Write("\n\t\t         > " + 5 + " - " + maxSize);
            if (maxSize == 65535)
            {
                Write(" (рекоменд: " + realMax + ") <    - Выбор в интервале");
                Write("\n\t\t         > СЛУЧ/RAND <                   - Случайное значение");
                Write("\n\t\t         > 0 <                           - Назад");
            }
            else
            {
                Write(" <" + margin + " - Выбор в интервале");
                Write("\n\t\t         > СЛУЧ/RAND <   - Случайное значение");
                Write("\n\t\t         > 0 <           - Назад");
            }


            Write("\n\t\t[i]  - Чем больше " + sizeType + "а узора, тем он красивее!\n");
        }
             //  UI reset for the GetSize() function


        static public void WriteProgramInfo()
        {
            ResetUI(!gIgnoreFullScreen, true);
            Write("\n\t\t\t\t\t\tВыбрано: --- === Информация о программе === ---\n\n\n");

            //-----------------------  WRITTING INFO ABOUT THE PROGRAM  -----------------------------//


            Write("\n\t\tЭта программа была создана в учебных целях учеником Гунько Егором, для облегчения работы художникам.");
            Write("\n\t\tТехническая информация:  v2.1.2 patch\n");

            Write("\n\t\t[i]  - Функционал:");
            Write("\n\t\t         > Создание узоров (выбор размеров, цветов, названия для созданных узоров");
            Write("\n\t\t         > Просмотр  сгенерированных узоров");
            Write("\n\t\t         > Изменение настроек для более удобной работы с программой\n");

            Write("\n\t\t[i]  - Инструкция по сохранению узоров в галерею:");
            Write("\n\t\t         > Вводите номера узоров, которые желаете сохранить через ','");
            Write("\n\t\t       Если хотите добавить узору название напишите:");
            Write("\n\t\t         > номерузора = название\n");

            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\t\t       Примеры ввода:");
            Write("\n\t\t         > 1, 3, 10, 9");
            Write("\n\t\t         > 1=Солнце,4=Море");
            Write("\n\t\t         > 9, 8=20.2.2020 Февраль, 7, 2 = мойшедевр\n");
            ForegroundColor = ConsoleColor.White;



            Write("\n\t\t[i]  - Инструкция по работе с галереей:");
            Write("\n\t\t         > Одно число - для просмотра одного узора (под этим номером)");
            Write("\n\t\t         > Два числа через '-' - для просмотра узоров под номерами от первого до второго числа");
            Write("\n\t\t         > Несколько чисел через '/' - для просмотра нескольких узоров (только под этими номерами)\n");


            ForegroundColor = ConsoleColor.DarkGray;
            Write("\n\t\t       Примеры ввода:");
            Write("\n\t\t         > 12 <        - будет показан только один узор под номером 12");
            Write("\n\t\t         > 3-8 <       - будут показаны узоры под номерами 3, 4, 5, 6, 7 и 8");
            Write("\n\t\t         > 15/2/7/1 <  - будут показаны узоры под номерами 15, 2, 7 и 1 (в таком же порядке)\n");
            ForegroundColor = ConsoleColor.White;


            Write("\n\t\tНадеюсь вам поможет моя программа, удачи! ");
            ReadKey();
        }
             //  Write the info about the program


        static public void EncodePattern(short type, int X, int Y, int colAmount,
            ConsoleColor[] colors, string patternName = "Unnamed",
            bool savePattern = false, string fileName = "", string path = "",
            bool showInfo = false)
        {
            //  Convert parameters into short string codes
            //string patternCode = type + "/" + X + "/" + Y + "/" + colAmount + "/";
            string formatedCode = type + "," + X + "," + Y + "," + colAmount + ",";


            //  Get a name for an unnamed pattern
            if (patternName == "Unnamed" || patternName == "")
            {
                //  Get current date and time
                //
                //  Overwrite the pattern name
                //  Save the date and time to the pattern name
                patternName = $"{DateTime.Now:dd.MM.yyyy HH:mm}";


                //  Save the computer name, and the user name (currently logged in) to the pattern name
                patternName += " " + Environment.MachineName + " " + Environment.UserName;


                //  Replace banned character with a similar non-banned version
                //  ( the , symbol is used in the parsing of the file data )
                patternName = patternName.Replace(",", ".");
            }


            //  Get a file name for the patterns
            if (fileName == "") fileName = Environment.UserName + ".patterns";


            //  Convert the colors to a byte array for a string code transformation
            byte[] colorBytes = ConvertColorsToBytes(colors);


            //  Convert the colors to string codes
            //patternCode += string.Join("-", colorBytes);
            formatedCode += string.Join(",", colorBytes);


            //  If dev mode is on - show the pattern info
            if (showInfo)
            {
                Write("\t\t[i]  - Закодированная информация об узоре:\n");
                Write("\n\t\t          " + patternName);
                Write("\n\t\t          " + formatedCode + "\n");

                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t          > [1] <  -  Название узора");
                Write("\n\t\t          > [2] <  -  Тип узора, Размеры (Ширина, Высота), Кол-во цветов");
                Write("\n\t\t          > [3] <  -  Используемые цвета (в байтах)\n\n\n");
                ForegroundColor = ConsoleColor.White;

                //Write("\n\t\t       Быстрая команда: " + patternCode + "\n\n");
            }

            if (savePattern)
            { 
                List<string> patternData = new List<string>
                {
                    //  Add a special character for the ParseData function
                    //  So it doesn't remove spaces from the name string
                    "*" + patternName,

                    //  Add parameters + colors
                    formatedCode + "\n",
                };

                if (path == "") path = gGalleryPath;
                BetterDataIO.SaveData(path, fileName, patternData, true, "\n", showInfo, false, "\t\t");
                Write("\n\n");
            }
        }
             //  Writing dev info and/or saving the pattern
             //  Write info for developers (usefull for changing the gallery parameters)


        static public void PrintLogo()
        {
            Write("\t\t");
            BackgroundColor = ConsoleColor.DarkBlue;
            for (int i = 0; i < 124; i++) Write(" ");
            BackgroundColor = ConsoleColor.Black;
            Write("\n"); //1 Empty

            /////////////////////////////////2
            for (int j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("       ");//G
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("  ");//N
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("      ");//R
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("       ");//A
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("          ");//T
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("      ");//I
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkMagenta;
                Write("      ");//O
                BackgroundColor = ConsoleColor.DarkBlue;
                Write("   ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////3
            for (int j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//G
                BackgroundColor = ConsoleColor.Blue;
                Write("             ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("   ");//N
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//R
                BackgroundColor = ConsoleColor.Blue;
                Write("   ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");


                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("   ");//A
                BackgroundColor = ConsoleColor.Blue;
                Write("   ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("   ");
                BackgroundColor = ConsoleColor.Blue;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//T
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//I
                BackgroundColor = ConsoleColor.Blue;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//O
                BackgroundColor = ConsoleColor.Blue;
                Write("    ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////4
            for (int j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//G
                BackgroundColor = ConsoleColor.Blue;
                Write("   ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("    ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("     ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("         ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("    ");//N
                BackgroundColor = ConsoleColor.Blue;
                Write(" ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("     ");//E
                BackgroundColor = ConsoleColor.Blue;
                Write("         ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("      ");//R
                BackgroundColor = ConsoleColor.Blue;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//A
                BackgroundColor = ConsoleColor.Blue;
                Write("     ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");
                BackgroundColor = ConsoleColor.Blue;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//T
                BackgroundColor = ConsoleColor.Blue;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//I
                BackgroundColor = ConsoleColor.Blue;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//O
                BackgroundColor = ConsoleColor.Blue;
                Write("    ");
                BackgroundColor = ConsoleColor.Magenta;
                Write("  ");//O
                BackgroundColor = ConsoleColor.Blue;
                Write("  ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////5
            for (int j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//G
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("     ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//N
                BackgroundColor = ConsoleColor.DarkCyan;
                Write(" ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("    ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//R
                BackgroundColor = ConsoleColor.DarkCyan;
                Write(" ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("         ");//A
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//T
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("            ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//I
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("        ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//O
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("    ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//O
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            /////////////////////////////////6
            for (int j = 0; j < 1; j++)
            {
                BackgroundColor = ConsoleColor.Black;
                Write("\t\t");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("       ");//G
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//N
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("        ");//E
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//R
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("   ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("      ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//A
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("     ");
                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("  ");//T
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("          ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("      ");//I
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("       ");

                //////////////////

                BackgroundColor = ConsoleColor.DarkRed;
                Write("      ");//O
                BackgroundColor = ConsoleColor.DarkCyan;
                Write("   ");
                BackgroundColor = ConsoleColor.Black;
                Write("\n");
            }

            BackgroundColor = ConsoleColor.Black;
            Write("\t\t");
            BackgroundColor = ConsoleColor.Cyan;
            for (int i = 0; i < 124; i++) Write(" ");
            BackgroundColor = ConsoleColor.Black;
            Write("\n\n\n"); //7 Empty
        }
             //  Logo is stored here, consists of: 6 layers + 2 empty (8 total)



        //------------------  Pattern related procedures  ----------------------------------------//


        static public bool CreatePatternsLogic()
        {
            byte[] randomBestPatterns;
            UInt16 colAmount;
            ConsoleColor[] colors;
            UInt16 width = 0, height = 0;



            while (height == 0)
            {
                //  Reset UI, dont wait for additional input
                ResetUI(!gIgnoreFullScreen, true);
                Write("\n\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");


                //  Get the pattern width
                width = GetSize("ширин");

                //  Option: leave - Exit the pattern creation
                if (width == 0) return true;



                //  Reset UI, dont wait for additional input
                ResetUI(!gIgnoreFullScreen, true);

                //  Get the pattern height
                height = GetSize("высот", width);
            }


            //  User color choice
            if (ChooseColorType(width, height))
            {
                //  Get the amount for the user colors
                //  65'535 is the max possible amount for a UInt16 type (16 bit unsigned int)
                colAmount = GetColorsAmount(Math.Min(Math.Min(width, height), (UInt16)65535));

                //  Get the custom colors
                //  And convert them from bytes to ConsoleColors
                colors = ConvertColorsToConsole(GetCustomColors(colAmount));
            }

            //  Asset colors choice
            else
            {
                //  Choose asset colors
                colors = StoredColors[GetColorAssetID(width, height)];

                //  Extract the amount of colors from the chosen asset pack
                colAmount = (UInt16)colors.Length;
            }

            //  Randomly chosing
            randomBestPatterns = ChooseBestPatterns(colAmount);

            //  Printing them
            PrintPatterns(randomBestPatterns, width, height, colors);

            if (!gAutoSave)
            {
                Write("\n\t\t[?]  - Какие узоры вы хотели бы сохранить в галерею?");
                Write("\n\t\t[i]  - Введите номера узоров через ','");
                Write("\n\t\t       Если хотите добавить узору название напишите: номерузора = название\n");
                Write("\n\t\t> 0 <   - Вернуться в меню\n");

                Write("\n\t\t[->]  - Ваш выбор: ");
                List<byte> selected = SelectPatternsForSaving(out List<string> names);
                while (selected.Count > 0)
                {
                    for (int i = 0; i < selected.Count; i++)
                    {
                        //  Encode pattern
                        //  Save it
                        //  Write info about it and the process (optional)
                        EncodePattern(selected[i], width, height, colAmount, colors, names[i],
                            true, "", gGalleryPath, gShowInfo);
                    }

                    //  Continue selection
                    Write("\n\t\t[->]  - Ваш выбор: ");
                    selected = SelectPatternsForSaving(out names);
                }
            }

            //  Write success message
            Write("\n\n\n\t\tГенерация узоров завершена.\n\n\n\n\n");


            //  Return auto continue = false (wait for user input)
            return false;
        }
             //  All the logic for generating a user-prompted pattern

        static public void PrintPatterns(byte[] BestPatterns, int X, int Y, ConsoleColor[] Colors)
        {
            //  Pattern are stored in the dictionary for easy access
            var Patterns = new Dictionary<byte, Action>()  {
                { 1,  () => Pattern1 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 2,  () => Pattern2 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 3,  () => Pattern3 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 4,  () => Pattern4 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 5,  () => Pattern5 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 6,  () => Pattern6 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 7,  () => Pattern7 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 8,  () => Pattern8 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 9,  () => Pattern9 (X, Y, Colors, true, "", gAutoSave, gShowInfo) },
                { 10, () => Pattern10(X, Y, Colors, true, "", gAutoSave, gShowInfo) },
            };

            byte randomId = 255;
            byte[] usedIds = new byte[BestPatterns.Length];

            for (int i = 0; i < usedIds.Length; i++) usedIds[i] = 255;
            if (!gIgnoreFullScreen) ForceFullScreen(2 * X, Y);
            Write("\n\n\n\t\t\t\t\tВот лучшие сгенерированые узоры:\n\n");

            for (byte i = 0; i < 2 * BestPatterns.Length; i++)
            {
                while (i % 2 == 0)
                {
                    i++;
                    randomId = BestPatterns[gRandom.Next(0, BestPatterns.Length)];
                    for (byte j = 0; j < usedIds.Length; j++) if (randomId == usedIds[j]) i--;
                }
                usedIds[i / 2] = randomId;
                Write("\n\n");
                Patterns[randomId](); // Printing the good ones
            }

            Write("\n\n\t\tUsed id: ");
            for(int i = 0; i < usedIds.Length; i++)
            {
                Write(usedIds[i] + " ");
            }


            if (gAlwaysGenerate || Continue())
            {   // Asking if the user wants to generate more patters

                if (!gIgnoreFullScreen) ForceFullScreen(2 * X, Y);
                for (byte i = 1; i < Patterns.Count + 1; i++)
                {
                    for (byte j = 0; j < usedIds.Length; j++)
                    {
                        if (i == usedIds[j])
                        {
                            j = 0;
                            i++;
                        }
                    }
                    Write("\n\n");
                    if (i < Patterns.Count + 1) Patterns[i](); //Printing the rest
                }
            }
        }
             //  Printing all the patterns that we constructed through the standart process
    }
}