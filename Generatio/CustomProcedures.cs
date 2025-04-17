using System;
using System.Collections.Generic;
using static System.Console;


using static Generatio.PatternSource;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.DataManipulation;
using System.Linq;


namespace Generatio
{
    internal class CustomProcedures
    {
        //-----------------------  Action procedures  ----------------------------------------------------------//


        static public void ForceFullScreen(int X = 0, int Y = 0)
        {
            ushort Counter = 0;
            if (X == 0) X = LargestWindowWidth / 13;
            if (Y == 0) Y = LargestWindowHeight / 26;
            if (!gNoLimitMode)
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
                    if (gGeneratedPatterns == false) Clear();
                }
                if (Counter > 1) Write("\n\n");
                ForegroundColor = ConsoleColor.White;
            }
        }
             //  Force the user to enlarge the window to prevent limitations of the size of the patterns



        //----------------  Procedures exclusively meant for printing info  -----------------------------------//

        static public void WriteInfo()
        {   //-----------------------  WRITTING INFO ABOUT THE PROGRAM  -----------------------------//
            if (!gGeneratedPatterns) Clear();
            Write("\n\n\n\n\n\n");
            PrintLogo();

            Write("\t\t\t\t\t\t\tВыбрано: --- === О программе === ---\n\n\n");

            Write("\t\tЭта программа была создана в учебных целях учеником Гунько Егором, для облегчения работы художникам.\n");
            Write("\t\tЗдесь представлена не полная версия программы. Данная версия имеет ограниченный функционал, и будет дорабатываться в будующем.\n");
            Write("\t\tЕё предназначение: помогать художникам, любителям и простым людям с созданием узоров.\n");
            Write("\t\tПока что функционал не сильно велик, но программа уже может генерировать 9 типов узоров любых размеров по параметрам пользователя.\n\n");

            Write("\t\tИнструкция по работе с программой:\n");
            Write("\t\t  1. Выберите нужный вам пункт в меню\n");
            Write("\t\t  2. Вводите параметры узоров, следуя указаниям на экране\n");
            Write("\t\t  3. Выберете, генерировать или нет дополнительные узоры\n");
            Write("\t\t  4. Сохраните параметры для генерации узоров, и сделайте снимок экрана для сохранения понравившихся узоров\n");
            Write("\t\t  5. После сохранения, узоры в любой момент можно будет посмотреть в пункте 'галерея'\n");

            Write("\n\n\t\tНадеюсь вам поможет моя программа, удачи!\n\n\n\n");
        }
        //  Write the info about the program


        static public void EncodePattern(short _type, int X, int Y, int _colAmount,
            ConsoleColor[] _colors, string _patternName = "",
            bool _savePattern = false, string _fileName = "", string _path = "",
            bool _showInfo = false)
        {
            //  Convert parameters into short string codes
            string _patternCode = _type + "/" + X + "/" + Y + "/" + _colAmount + "/";
            string _formatedCode1 = _type + ", " + X + ", " + Y + ", " + _colAmount;
            string _formatedCode2;


            //  Get a name for an unnamed pattern
            if (_patternName == "")
            {
                //  Get current date and time
                //
                //  Save the date and time to the pattern name
                _patternName += $"{DateTime.Now:dd.MM.yyyy HH:mm}";


                //  Save the computer name, and the user name (currently logged in) to the pattern name
                _patternName += " " + Environment.MachineName + " " + Environment.UserName;


                //  Replace banned character with a similar non-banned version
                //  ( the , symbol is used in the parsing of the file data )
                _patternName = _patternName.Replace(",", ".");
            }


            //  Convert the colors to a byte array for a string code transformation
            byte[] _colorBytes = ConvertColorsToBytes(_colors);


            //  Convert the colors to string codes
            _patternCode += string.Join("-", _colorBytes);
            _formatedCode2 = string.Join(", ", _colorBytes);


            //  If dev mode is on - show the pattern info
            if (_showInfo)
            {
                Write("\t\t[i]  - Закодированная информация об узоре:\n");
                Write("\n\t\t          " + _patternName);
                Write("\n\t\t          " + _formatedCode1);
                Write("\n\t\t          " + _formatedCode2 + "\n");

                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\t          > [1] <  -  Название узора");
                Write("\n\t\t          > [2] <  -  Тип узора, Размеры (Ширина, Высота), Кол-во цветов");
                Write("\n\t\t          > [3] <  -  Используемые цвета (в байтах)\n");
                ForegroundColor = ConsoleColor.White;

                Write("\n\t\t       Быстрая команда: " + _patternCode + "\n\n");
            }

            if (_savePattern)
            { 
                List<string> _patternData = new List<string>
                {
                    //  Add a special character for the ParseData function
                    //  So it doesn't remove spaces from the name string
                    "*" + _patternName,


                    //  Add base parameters
                    "!" + _formatedCode1,


                    //  Add colors
                    _formatedCode2,


                    //  Add new line for better visual separation
                    //  (will be skipped by the parser anyway)
                    "\n"
                };

                if (_path == "") _path = gGalleryPath;
                SaveData(_path, _fileName, _patternData, true, _showInfo);
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


        static public void PrintPatterns(byte[] BestPatterns, int X, int Y, ConsoleColor[] Colors)
        {
            //  Pattern are stored in the dictionary for easy access
            var Patterns = new Dictionary<byte, Action>()  {
                { 1, () => Pattern1(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 2, () => Pattern2(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 3, () => Pattern3(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 4, () => Pattern4(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 5, () => Pattern5(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 6, () => Pattern6(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 7, () => Pattern7(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 8, () => Pattern8(X, Y, Colors, true, gAutoSave, gDevMode) },
                { 9, () => Pattern9(X, Y, Colors, true, gAutoSave, gDevMode) }
            };

            byte _randomId = 255;
            byte[] _usedIds = new byte[BestPatterns.Length];

            for (int i = 0; i < BestPatterns.Length; i++) _usedIds[i] = 255;
            if (!gIgnoreFullScreenMode) ForceFullScreen(2 * X, Y);
            Write("\n\n\n\t\t\t\t\tВот лучшие сгенерированые узоры:\n\n");

            for (byte i = 0; i < 2 * BestPatterns.Length; i++)
            {
                while (i % 2 == 0)
                {
                    i++;
                    _randomId = (byte)gRandom.Next(0, BestPatterns.Length);
                    for (byte j = 0; j < BestPatterns.Length; j++) if (_randomId == _usedIds[j]) i--;
                }
                _usedIds[i / 2] = _randomId;
                Write("\n\n");
                Patterns[BestPatterns[_randomId]](); // Printing the good ones
            }
            if (gAlwaysGenerate || Continue())
            {   // Asking if the user wants to generate more patters

                if (!gIgnoreFullScreenMode) ForceFullScreen(2 * X, Y);
                for (byte i = 1; i < Patterns.Count; i++)
                {
                    for (byte j = 0; j < BestPatterns.Length; j++)
                    {
                        if (i == _usedIds[j])
                        {
                            j = 0;
                            i++;
                        }
                    }
                    Write("\n\n");
                    Patterns[i](); //Printing the rest
                }
            }
        }
             //  Printing all the patterns that we constructed through the standart process
    }
}