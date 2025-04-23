using System;

using static System.Console;


using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;
using static Generatio.CustomProcedures;
using System.Collections.Generic;


namespace Generatio
{
    internal class CustomFunctions
    {
        //-------------------  FUNCTIONS  ---------------------------------------------------------------------//
        
        static public bool Continue()
        {
            bool Generating = false;
            string UserInput;

            if (!gIgnoreFullScreen) ForceFullScreen();

            Write("\n\n\t\tВы хотите сгенерировать оставшиеся узоры? (ДА/НЕТ | YES/NO): ");
            UserInput = ReadLine().Trim().ToLower();
            if (UserInput == "да" || UserInput == "yes") Generating = true;
            Write("\n");

            return Generating;
        }           
             // Ask for generating the rest of the patterns
        
        static public bool ChooseColorType()
        {
            string _userInput = "";

            Write("\n\t\tВы желаете выбрать свои цвета для узоров? (ДА/YES - свои, НЕТ/NO - из базы): ");

            while (_userInput != "☺")
            {
                _userInput = ReadLine().Trim().ToLower(); // Parse the user input

                // Check for custom colors choice, if so, return true = custom choice
                if (_userInput == "да" || _userInput == "д" || _userInput == "yes" || _userInput == "y") return true;

                // Check for asset colors choice, if so, exit the loop and return false = asset choice
                else if (_userInput != "нет" || _userInput != "н" || _userInput != "no" || _userInput != "n") _userInput = "☺";

                // Return error message if the user input was invalid and try again
                else
                {
                    _userInput = ""; // Reset the input, just in case the user inputs "☺" (Alt+1)
                    Write("\t\t[!]  - Не удалось распознать выбор, повторите ввод: "); // Print error message
                }
            }

            // Only possible to exit the loop if we got the asset colors choice, so we return false = asset choice
            return false;
        }    
             // Get the users color type choice (custom or from assets)



        //---------------------------  Returns numbers  -------------------------------------------------------//
        
        static public byte GetAssetColorsID()
        {
            string UserInput;
            byte UserChoice = 0;

            Write("\n\n\t\tВсего есть " + StoredColors.Length + " вариантов сочетания цветов:");
            for (int i = 0; i < StoredColors.Length; i++)
            {
                Write("\n\n\t\t[" + (i + 1) + "] - ");
                for (int j = 0; j < StoredColors[i].Length; j++)
                {
                    BackgroundColor = StoredColors[i][j];
                    if (BackgroundColor != ConsoleColor.Black) Write("      ");
                    else
                    {
                        ForegroundColor = ConsoleColor.DarkGray;
                        Write("Чёрный");
                        ForegroundColor = ConsoleColor.White;
                    }
                }
                BackgroundColor = ConsoleColor.Black;
                Write("\t - " + StoredColors[i].Length + " цвета(ов)");
            }
            //////////////////////////////////////////////////////////////////////////////////////

            while (UserChoice > StoredColors.Length || UserChoice < 1)
            {
                Write("\n\n\n\n\t\tПожалуйста, введите цифру от 1 до " + StoredColors.Length + " - ваш выбор цветовой палитры: ");
                UserInput = ReadLine();
                UserInput = UserInput.Trim();
                if (byte.TryParse(UserInput, out UserChoice))
                {
                    UserChoice = byte.Parse(UserInput);
                    if (UserChoice < 1 || UserChoice > StoredColors.Length) Write("\t\tВведённое число не попадает в диапозон. Повторите ввод.\n");
                }
                else Write("\t\tНе удалось расспознать выбор. Повторите ввод.\n");
            }
            UserChoice--;
            return UserChoice;
        }   
             // Let the user choose the colors from the assets
        
        static public string GetUserTask()
        {
            bool _validTask = false;
            string _userInput = "";

            Write("\n\n\n\t\t[?]  - Что вы хотите сделать?\n");
            Write("\t\t   > 1 <    - О программе\n");
            Write("\t\t   > 2 <    - Сгенерировать узор\n");
            Write("\t\t   > 3 <    - Галерея узоров\n");
            Write("\t\t   > 4 <    - Обновить последнее сохранение галереи\n");
            Write("\t\t   > 5 <    - Настройки\n");

            ForegroundColor = ConsoleColor.DarkGray;
            Write("\t\t   > 6 <    - Сгенерировать узор по коду\n");
            ForegroundColor = ConsoleColor.White;

            Write("\t\t   > 0 <    - Выход\n");


            while (!_validTask)
            {
                Write("\n\t\tВаш выбор: ");
                _userInput = ReadLine().Trim();
                if (_userInput == "ВыдайМем") Write("\t\t:)   - А почему не 9?\n");
                else if (_userInput == "0" || _userInput == "1" || _userInput == "2" ||
                         _userInput == "3" || _userInput == "4" || _userInput == "5")
                    _validTask = true;
                else Write("\t\t[!]  - Не удалось распознать выбор. Пожалуйста, повторите ввод\n");
            }
            return _userInput;
        }
             // Get the users wanted task




        static public int GetSize(string _sizeType)
        {
            int _pictureSize = 0;     // We will be storing the final parsed result
            int _maxSize;             // We are setting the max size either to the current width or height of the window
            const int _minSize = 5;   // Min size is a constant
            string _userInput;        // For user input parsing



            if (!gIgnoreFullScreen) ForceFullScreen();  // Forcing full screen to get the max window size

            if (_sizeType == "высот") _maxSize = WindowHeight - 1;             // Setting the max window size
            else _maxSize = (WindowWidth - 1) / 2;                             //



            // Primary asking for the user choice for the size
            Write("\n\t\t[i]  - Чем больше " + _sizeType + "а узора, тем он красивее!");
            Write("\n\t\t[->] - Введите желаемую " + _sizeType + "у узора (число от 5 до " + _maxSize + ", или СЛУЧ/RND): ");



            while (_pictureSize < _minSize || _pictureSize > _maxSize)  // While we dont have a valid size
            {
                _userInput = ReadLine().Trim().ToLower();          // Parsing the user input

                if (int.TryParse(_userInput, out _pictureSize))    // Try converting it to a number
                {
                    // Succesful conversion to a number

                    // Check if the number is bigger than the max valid result, if so, return error
                    if (_pictureSize < _minSize) Write("\t\t[!]  - Число меньше допустимого значения, повторите ввод: ");

                    // Check if the number is lower  that the min valid result, if so, return error
                    else if (_pictureSize > _maxSize) Write("\t\t[!]  - Число больше допустимого значения, повторите ввод: ");
                }
                else if (_userInput == "случайно" || _userInput == "случ" || _userInput == "random" || _userInput == "rand" || _userInput == "rnd")
                {
                    _pictureSize = gRandom.Next(Math.Max(5, _maxSize / 4), _maxSize + 1);  // Choose a random size if the user wants so
                    Write("\t\t[i]  - Выбрано: " + _pictureSize + "\n");                  // Write the randomly chosen number
                }
                else Write("\t\t[!]  - Не удалось распознать число, повторите ввод: ");    // Write error message
            }
            return _pictureSize; // return chosen picture size
        }
             // Get for the size for the patterns (both X, and Y independently)

        static public int GetColorsAmount(int _maxAmount)
        {
            int _amount = 0;             // We will be storing the final parsed result here
            const int _minAmount = 2;    // Min amount is a constant
            string _userInput;           // For user input parsing


            // Primary asking for the user choice for the amount of colors
            Write("\t\tВведите количество цветов (число, от " + _minAmount + " и до " + _maxAmount + ", или СЛУЧ/RND): ");


            while (_amount < _minAmount || _amount > _maxAmount)  // While we dont have a valid amount of colors
            {
                _userInput = ReadLine().Trim().ToLower();    // Parsing the user input

                if (int.TryParse(_userInput, out _amount))   // Try converting it to a number
                {
                    // Succesful conversion to a number

                    // Check if the number is bigger than the max valid result, if so, return error
                    if (_amount < _minAmount) Write("\t\t[!]  - Число меньше допустимого значения, повторите ввод: ");

                    // Check if the number is lower  that the min valid result, if so, return error
                    else if (_amount > _maxAmount) Write("\t\t[!]  - Число больше допустимого значения, повторите ввод: ");
                }
                else if (_userInput == "случайно" || _userInput == "случ" || _userInput == "random" || _userInput == "rand" || _userInput == "rnd")
                {
                    _amount = gRandom.Next(Math.Max(5, _maxAmount / 4), _maxAmount + 1);   // Choose a random amount if the user wants so
                    Write ("\t\t[i]  - Выбрано: " + _amount + "\n");                       // Write the randomly chosen number
                }
                else Write("\t\t[!]  - Не удалось распознать число, повторите ввод: ");    // Write error message
            }
            return _amount; // return the chosen amount
        }  
             // Get the amount of colors (for custom clors choice)



        /*static public long Factorial(int Number)
        {
            long FactorialOfNumber = 1;
            while (Number > 1)
            {
                FactorialOfNumber *= Number;
                Number--;
            }
            return FactorialOfNumber;
        }*/        
             // Legacy factorial function
        

        //------------------------  Returns arrays  -----------------------------------------------------------//

        static public byte[] GetBestPatterns(int AmountOfColors)
        {
            byte[] BestPatterns = new byte[4];

            if (AmountOfColors < 3) BestPatterns[0] = 2; //CHOOSING 1 THE BEST PatternS
            else BestPatterns[0] = 9;

            if (gRandom.Next(0, 100) < 50) BestPatterns[1] = 1;
            else BestPatterns[1] = 6; //CHOOSING 2 THE BEST PatternS

            if (gRandom.Next(0, 100) > 40) BestPatterns[2] = 3;
            else BestPatterns[2] = 7; //CHOOSING 3 THE BEST PatternS

            BestPatterns[3] = 8; //CHOOSING 4 THE BEST PatternS

            return BestPatterns;
        }
        // Choose the best patterns

        static public byte[] GetCustomColors(int _colAmount)
        {
            //  Final color storing
            byte[] _colors = new byte[_colAmount];

            //  Temporary buffer for currently selected color
            byte _selectedColor = 0;

            //  Temporary buffer for the user input
            string _userInput = "";

            //  Amount of the different chosen colors (to get at least 2 different colors from the bunch)
            bool _differentColors = false;
            byte _lastColor = 0; 

            //  Flag for parsing the currently selected color
            bool _validColor;

            //-------------  WRITING INFO  --------------------//

            Write("\n\n\n\t\t\t\t\tЦвета на выбор:\n\n");
            for (int i = 0; i < 16; i++)
            {
                ForegroundColor = gAllColors[i];
                Write("\t\t\t\t");
                BackgroundColor = gAllColors[i];
                if (i == 15)
                {
                    ForegroundColor = ConsoleColor.White;
                    Write("|чёрный|    -    ");
                    BackgroundColor = ConsoleColor.White;
                    ForegroundColor = ConsoleColor.Black;
                }
                else
                {
                    Write("        ");
                    BackgroundColor = ConsoleColor.Black;
                    Write("    -    ");
                }
                //if (i > 5) Write(gColorNames[i] + " - " + gColorKeySigns[9 + (i - 5) * 3] + "/" + gColorKeySigns[9 + (i - 5) * 3 + 1] + "/" + gColorKeySigns[9 + (i - 5) * 3 + 2] + "/" + i);
                //else Write(gColorNames[i] + " - " + gColorKeySigns[i * 2] + "/" + gColorKeySigns[i * 2 + 1] + "/" + i);

                Write(gColorNames[i] + " - " + gColorKeySigns[i * 3] + "/" + gColorKeySigns[i * 3 + 1] + "/" + gColorKeySigns[i * 3 + 2] + "/" + i);
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }

            ForegroundColor = ConsoleColor.White;
            BackgroundColor = ConsoleColor.Black;

            //---------------  USEFUL INFORMATION  ------------------------//

            Write("\n\t\t\t\t[i]  - Вводите цвета которые хотите использовать");
            Write("\n\t\t\t\tВ том же порядке, который должен присутствовать в финальном узоре");
            Write("\n\t\t\t\tЦвета МОГУТ повторяться если вам это необходимо");
            Write("\n\t\t\t\tДолжно быть выбрано хотя бы 2 различных цвета\n");

            Write("\n\t\t[i]  - Способы ввода цветов:");
            Write("\n\t\t         > Введите кодовое слово отвечающее за цвет <");
            Write("\n\t\t         > Слово СЛУЧАЙНО для выбора 1 цвета компьютеров <");
            Write("\n\t\t         > Слова ВСЕ СЛУЧАЙНО/ВСЕСЛУЧ для выбора всех цветов компьютером <\n");


            //------------------  GETTING COLORS  ------------------------//
            for (int i = 0; i < _colAmount; i++)
            {
                _validColor = false;
                while (!_validColor)
                {
                    //  If we arent generating every color
                    //  then ask the user for a color choice
                    if (_userInput != "всеслучайно" && _userInput != "всеслуч")
                    {
                        Write("\n\t\t" + (i + 1) + "-ый цвет) ");

                        //  Get user input
                        _userInput = ReadLine();

                        //  Parse user input
                        _userInput = _userInput.ToLower();
                        _userInput = _userInput.Replace("-", "");
                        _userInput = _userInput.Replace(" ", "");
                        _userInput = _userInput.Replace("ё", "е");
                    }

                    //  Check for a rnd color
                    if (_userInput == "случайно" || _userInput == "случ" ||
                        _userInput == "всеслуч" || _userInput == "всеслучайно")
                    {
                        //  Choose a random color
                        _selectedColor = (byte)gRandom.Next(0, 16);

                        //  Check if it it unique 
                        if (i != 0 && _selectedColor != _lastColor) _differentColors = true;
                        // Update the last color for the next unique check
                        _lastColor = _selectedColor; 
                        
                        //  If we are already at the last choosable color
                        if (i == _colAmount - 1)
                        {
                            while (!_differentColors)
                            {
                                //  Choose a new random color (15/16 chance of being valid)
                                _selectedColor = (byte)gRandom.Next(0, 16);

                                //  Check if it is unique
                                //  If so - exit the loop
                                if (_selectedColor != _lastColor) _differentColors = true;
                            }
                        }

                        //  Set the color flag to valid
                        _validColor = true;
                    }

                    //  If not a rnd color - parse the user color choice
                    else
                    {
                        //  Try parse the colors by the number
                        for (byte j = 0; j < 16; j++)
                        {
                            //  If the user choice is a valid color number
                            if (_userInput == j.ToString())
                            {
                                //  Save the color 
                                _selectedColor = j;

                                // exit the loop
                                _validColor = true;
                                j += 16;
                            }
                        }

                        //  If number parsing failed
                        if (!_validColor)
                        {
                            //  Try parse by the color keycodes
                            for (byte j = 0; j < gColorKeySigns.Length && !_validColor; j++)
                            {
                                //  If a keycode matches (user choice is a valid color)
                                if (_userInput == gColorKeySigns[j])
                                {
                                    //  Save the color
                                    _selectedColor = (byte)(j / 3);

                                    //  Exit the loop
                                    _validColor = true;
                                }
                            }
                        }


                        //  If the color parsing failed (unknown choice) - write error message
                        if (!_validColor) Write("\t\tНе удалось распознать цвет. Пожалуйста, повторите ввод\n");

                        //  Else if the parsed color is valid
                        //  
                        //  But it is the last possible choice for a color
                        //  And there arent any different colors
                        else if (i == (_colAmount - 1))
                        {
                            //  Unique color check logic
                            if (i != 0 && _selectedColor != _lastColor) _differentColors = true;

                            if (!_differentColors)
                            {
                                //  Invalidate this color
                                _validColor = false;

                                //  Write error message
                                Write("\t\tДолжно быть хотя бы 2 различных цвета\n");
                            }
                        }
                    }
                }

                //  Save the valid color
                _colors[i] = _selectedColor;


                //  Unique color check logic
                if (i != 0 && _selectedColor != _lastColor) _differentColors = true;

                //  Update the last color for the next unique check
                _lastColor = _selectedColor;


                //  Write success message
                Write("\t\tЦвет распознан! ");

                ForegroundColor = gAllColors[_selectedColor];
                if (_selectedColor == 15) BackgroundColor = ConsoleColor.White;

                Write((i + 1) + "-ый цвет: " + gColorNames[_selectedColor]);

                //  Reset output formatting
                BackgroundColor = ConsoleColor.Black;
                ForegroundColor = ConsoleColor.White;
                Write("\n");
            }

            //  Return the selected colors
            return _colors;
        }                         
             // Get the colors for the custom user choice


        static public string GetSplitters(string _input, string _criteria)
        {
            string _splitters = "";

            for(int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _criteria.Length; j++) // For every splitter in the criterias string
                {
                    if (_input[i] == _criteria[j])
                    {
                        // Save the found splitter
                        _splitters += _input[i];

                        // Exit the loop - find the next splitter
                        j += _criteria.Length;
                    }
                }
            }

            return _splitters;
        }
             // Returns a string (array of chars): Get the splitters in the input strings that match the criteria


        static public string[] SplitForParsing(string _input, string _criterias)
        {
            // Future length for the string array
            //
            // Consists of how many splitters from the criterias string we have found in our input string
            int _arrayLength = 1;

            // Save the position for the last found splitter
            // Needed to save the new section between the splitters
            int _lastSplitCharPos = 0;

            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _criterias.Length; j++)  // For every splitter in the criterias string
                {
                    if (_input[i] == _criterias[j])  // If the splitter is found
                    {
                        _arrayLength++;             // Increase the future array length
                        j += _criterias.Length;     // Exit the loop - go find the next splitter
                    }
                }
            }

            // Initialisse the string array with the calculated length
            string[] _result = new string[_arrayLength];

            for (int i = 0; i < _arrayLength; i++)          // For every string in the string array   //
            {

                for (int j = _lastSplitCharPos; j < _input.Length; j++)
                {                                           // For every interval in the input string //

                    for (int k = 0; k < _criterias.Length; k++)
                    {       // Distinquish the intervals from each others by the criteria characters  //

                        if (_input[j] == _criterias[k])
                        {
                            // If the splitter is found                                               //

                            // Extract the found interval from the last splitter to the current splitter
                            _result[i] += _input.Substring(_lastSplitCharPos, j - _lastSplitCharPos);

                            // Save the new last found splitter position
                            _lastSplitCharPos = j + 1;

                            // Exit the loops to increase the saving index
                            j += _input.Length;
                            k += _criterias.Length;
                        }
                        else if (j == _input.Length - 1)
                        {
                            // If we got to the end of the input string                               //
                            // So we need to save the last remaining interval                         //


                            // Extract the last interval from the last splitter to the end of the string
                            _result[i] += _input.Substring(_lastSplitCharPos, j - _lastSplitCharPos + 1);

                            // Exit the loops for good
                            j += _input.Length;
                            k += _criterias.Length;
                        }
                    }
                }
            }

            // return new splitted string array
            return _result;  
        }
             // Split the input string into a string array by the criteria chars


        static public ConsoleColor[] ConvertColorsToConsole(byte[] _colorIdx)
        {
            ConsoleColor[] _colors = new ConsoleColor[_colorIdx.Length];
            for (int i = 0; i < _colors.Length; i++) _colors[i] = gAllColors[_colorIdx[i]];
            return _colors;
        }
             // Convert colors from bytes to console colors

        static public byte[] ConvertColorsToBytes(ConsoleColor[] _colors)
        {
            byte[] _byteColors = new byte[_colors.Length];
            for (int i = 0; i < _byteColors.Length; i++)
            {
                for (byte colorId = 0; colorId < 16; colorId++)
                {
                    if (gAllColors[colorId] == _colors[i]) _byteColors[i] = colorId;
                }
            }
            return _byteColors;
        }
             // Convert colors from console colors to bytes


        //------------------------  Return list  --------------------------------------------------//

        static public List<string> ToStringList(string[] _array)
        {
            //  Store list
            List<string> _list = new List<string>();

            //  Convert array to list
            for (int i = 0; i < _array.Length; i++) _list.Add(_array[i]);

            //  Return the list
            return _list;
        }
    }
}