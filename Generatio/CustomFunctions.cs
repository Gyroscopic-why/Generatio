using System;
using System.Collections.Generic;

using static System.Console;


using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;
using static Generatio.CustomProcedures;



namespace Generatio
{
    internal class CustomFunctions
    {
        //-----------------------------  Returns bool  ---------------------------------------------------------//

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

        static public bool ChooseColorType(UInt16 _width, UInt16 _height)
        {
            //  User input temporary buffer
            string _userInput = "";

            //  Optimised error flag
            bool _firstTry = true;

            //  While parsing was unsuccessfull
            while (_userInput != "☺")
            {
				//  Reset UI, dont wait for additional input, print info
				ResetUI(!gIgnoreFullScreen, true);
				Write("\n\t\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");
				Write("\t\t[=]  - Выбранные размеры: " + _width + " x " + _height + "\n");


                //  Ask for color type
                Write("\n\t\t[?]  - Вы желаете выбрать свои цвета для узоров?");
                ForegroundColor = ConsoleColor.Gray;
                Write("\n\t\t        > ДА/YES <   - Свои цвета");
                Write("\n\t\t        > НЕТ/NO <   - Цвета из базы");
                ForegroundColor = ConsoleColor.White;



				//  Return error message if the user input was invalid and try again
				//  Interesting error detection - if we havent exited the loop:
				//   > Either its our first try, or the input was invalid
				if (!_firstTry)
				{
					// Print error message
					ForegroundColor = ConsoleColor.Red;
					Write("\n\n\t\t[!]  - Не удалось распознать предыдущий выбор, повторите ввод: ");
					ForegroundColor = ConsoleColor.White;
				}
				else Write("\n\n\t\t[->] - Ваш выбор: ");


				//  Parse the user input
				_userInput = ReadLine().Trim().ToLower();



                //  Check for custom colors choice, if so, return true = custom choice
                if (_userInput == "да" || _userInput == "д" || 
                    _userInput == "yes" || _userInput == "ye" || _userInput == "y") return true;

                //  Check for asset colors choice, if so, exit the loop and return false = asset choice
                else if (_userInput == "нет" || _userInput == "не" || _userInput == "н" ||
                    _userInput == "no" || _userInput == "n") _userInput = "☺";

				// Reset the input, just in case the user inputs "☺" (Alt+1)
				else if (_userInput == "☺") _userInput = ""; 



                //  Reset error flag
                _firstTry = false;
			}

            // Only possible to exit the loop if we got the asset colors choice, so we return false = asset choice
            return false;
        }
             // Get the users color type choice (custom or from assets)



        //---------------------------  Returns numbers  -------------------------------------------------------//


        static public byte GetColorAssetID(UInt16 _width, uint _height)
        {
			string _userInput;
			byte _choice = 0;


			//  Reset UI, dont wait for additional input, print info
			ResetUI(!gIgnoreFullScreen, true);
			Write("\n\t\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");
			Write("\t\t[=]  - Выбранные размеры: " + _width + " x " + _height + "\n");


            //  Print all the options
			Write("\n\n\t\tВсего есть " + StoredColors.Length + " коллекций цветов:");
			for (int i = 0; i < StoredColors.Length; i++)
			{
                //  Write the info about the current collection
				Write("\n\n\t\t  > " + (i + 1) + " <    ");


				for (int j = 0; j < StoredColors[i].Length; j++)
				{
					BackgroundColor = StoredColors[i][j];

                    //  If the color isnt black - output it
					if (BackgroundColor != ConsoleColor.Black) Write("      ");

                    //  Else write "black" on black background
					else
					{
						ForegroundColor = ConsoleColor.DarkGray;
						Write("Чёрный");
						ForegroundColor = ConsoleColor.White;
					}
				}

				//  Write the margin
				BackgroundColor = ConsoleColor.Black;
				for (int j = 0; j < 12 - StoredColors[i].Length; j++) Write("      ");

                //  Write the color amount in this collection
				Write("\t- " + StoredColors[i].Length + " цвета(ов)");
			}


            //  Get user choice
			while (_choice > StoredColors.Length || _choice < 1)
			{
				Write("\n\n\n\n\t\tПожалуйста, введите цифру от 1 до " + StoredColors.Length + " - ваш выбор цветовой палитры: ");
				_userInput = ReadLine();
				_userInput = _userInput.Trim();
				if (byte.TryParse(_userInput, out _choice))
				{
					_choice = byte.Parse(_userInput);
					if (_choice < 1 || _choice > StoredColors.Length) Write("\t\tВведённое число не попадает в диапозон. Повторите ввод.\n");
				}
				else Write("\t\tНе удалось расспознать выбор. Повторите ввод.\n");
			}
			_choice--;
			return _choice;
		}
		// Let the user choose the colors from the assets

		static public UInt16 GetColorsAmount(UInt16 _maxAmount)
		{
			UInt16 _amount = 0;             // We will be storing the final parsed result here
			const int _minAmount = 2;    // Min amount is a constant
			string _userInput;           // For user input parsing


			// Primary asking for the user choice for the amount of colors
			Write("\t\tВведите количество цветов (число, от " + _minAmount + " и до " + _maxAmount + ", или СЛУЧ/RND): ");


			while (_amount < _minAmount || _amount > _maxAmount)  // While we dont have a valid amount of colors
			{
				_userInput = ReadLine().Trim().ToLower();    // Parsing the user input

				if (UInt16.TryParse(_userInput, out _amount))   // Try converting it to a number
				{
					// Succesful conversion to a number

					// Check if the number is bigger than the max valid result, if so, return error
					if (_amount < _minAmount) Write("\t\t[!]  - Число меньше допустимого значения, повторите ввод: ");

					// Check if the number is lower  that the min valid result, if so, return error
					else if (_amount > _maxAmount) Write("\t\t[!]  - Число больше допустимого значения, повторите ввод: ");
				}
				else if (_userInput == "случайно" || _userInput == "случ" || _userInput == "random" || _userInput == "rand" || _userInput == "rnd")
				{
					//  Choose a random amount if the user wants so
					_amount = (UInt16)gRandom.Next(Math.Max(5, _maxAmount / 4), _maxAmount + 1);
					

                    //  Print the randomly chosen number
					Write("\t\t[i]  - Выбрано: " + _amount + "\n");                       
				}

				// Write error message
				else Write("\t\t[!]  - Не удалось распознать число, повторите ввод: ");
			}


			// return the chosen amount
			return _amount; 
		}
		     // Get the amount of colors (for custom clors choice)



		static public UInt16 GetSize(string _sizeType, UInt16 _last = 0)
        {
            UInt16 _pictureSize = 1;            //  Storing the final parsed result
			UInt16 _maxSize, _realMax;      //  Max allowed size, and maxWindowSize
            const int _minSize = 5;     // Min size is a constant
            string _userInput;      // For user input parsing


			//  Forcing fullscreen to capture max window size (optional)
			if (!gIgnoreFullScreen) ForceFullScreen();  


            //  Set the limit for the max pattern size
            if (_sizeType == "высот") _maxSize = (UInt16)Math.Min(65535, (WindowHeight - 1));
            else _maxSize = (UInt16)Math.Min(65535, (WindowWidth - 1) / 2);

            //  Save the window max for info, and rnd generator
			_realMax = _maxSize;

			//  Set the size to the absolute max if a cheatcode is enabled
			if (_maxSize < 65535 && gNoSizeLimit) _maxSize = 65535;



			//  Reset the window UI
			ResetSizeUI(_sizeType, _maxSize, _realMax, _last);
			Write("\n\t\t[->] - Ваш выбор: ");



			//  While we dont have a valid size
			while (_pictureSize != 0 && _pictureSize < _minSize || _pictureSize > _maxSize)
            {
				//  Parsing the user input
				_userInput = ReadLine().Trim().ToLower();


				//  Try converting it to a number
				if (UInt16.TryParse(_userInput, out _pictureSize))
                {
                    //  Check if the number is bigger than the max valid result, if so, return error
                    if (_pictureSize < _minSize && _pictureSize != 0)
                    {
						//  Reset the window UI
						ResetSizeUI(_sizeType, _maxSize, _realMax, _last);

						ForegroundColor = ConsoleColor.Red;
                        Write("\n\t\t[!]  - Число меньше допустимого значения, повторите ввод: ");
                        ForegroundColor = ConsoleColor.White;
                    }


                    //  Check if the number is lower  that the min valid result, if so, return error
                    else if (_pictureSize > _maxSize)
                    {
						//  Reset the window UI
						ResetSizeUI(_sizeType, _maxSize, _realMax, _last);

						ForegroundColor = ConsoleColor.Red;
                        Write("\n\t\t[!]  - Число больше допустимого значения, повторите ввод: ");
                        ForegroundColor = ConsoleColor.White;
                    }
                }


                //  Random number choice
                else if (_userInput == "случайно" || _userInput == "случ" || _userInput == "с" ||
                    _userInput == "random" || _userInput == "rand" || _userInput == "rnd" || _userInput == "r")
                {
                    //  Choose a "random" size
                    _pictureSize = (UInt16)gRandom.Next(Math.Max(5, _realMax / 4), _realMax + 1);

                    //  Output the randomly chosen number
                    Write("\t\t[i]  - Выбрано: " + _pictureSize + "\n");
                }


                //  Invalid input - Write error message
                else
                {
					//  Reset the window UI
					ResetSizeUI(_sizeType, _maxSize, _realMax, _last);

					//  Write error message
					ForegroundColor = ConsoleColor.Red;
                    Write("\n\t\t[!]  - Не удалось распознать число, повторите ввод: ");
                    ForegroundColor = ConsoleColor.White;


					//  Reset loop logic
					_pictureSize = 1;
				}
            }
            return _pictureSize; // return chosen picture size
        }
             // Get for the size for the patterns (both X, and Y independently)

        
        

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


		//-----------------------------  Returns strings     -------------------------------------------------//


		static public string GetUserTask()
		{
			bool _validTask = false;
			string _userInput = "";

			Write("\n\n\n\t\t[?]  - Что вы хотите сделать?");
			Write("\n\t\t   > 1 <    - О программе");
			Write("\n\t\t   > 2 <    - Создать узоры\n");

			Write("\n\t\t   > 3 <    - Галерея узоров");
			Write("\n\t\t   > 4 <    - Обновить последнее сохранение галереи\n");

			Write("\n\t\t   > 5 <    - Настройки");
			Write("\n\t\t   > 6 <    - Обновить сохранение настроек\n");

			Write("\n\t\t   > 0 <    - Выход\n");


			while (!_validTask)
			{
				Write("\n\t\tВаш выбор: ");
				_userInput = ReadLine().Trim();
				if (_userInput == "0" || _userInput == "1" || _userInput == "2" || _userInput == "3"
									  || _userInput == "4" || _userInput == "5" || _userInput == "6")
					_validTask = true;
				else Write("\t\t[!]  - Не удалось распознать выбор. Пожалуйста, повторите ввод\n");
			}
			return _userInput;
		}
		     // Get the users wanted task



		//------------------------  Returns arrays (mostly)  -------------------------------------------------//


		static public byte[] GetCustomColors(UInt16 _colAmount)
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
                //if (i > 5) Write(gColorNames[i] + " - " + gColorKeycodes[9 + (i - 5) * 3] + "/" + gColorKeycodes[9 + (i - 5) * 3 + 1] + "/" + gColorKeycodes[9 + (i - 5) * 3 + 2] + "/" + i);
                //else Write(gColorNames[i] + " - " + gColorKeycodes[i * 2] + "/" + gColorKeycodes[i * 2 + 1] + "/" + i);

                Write(gColorNames[i] + " - " + gColorKeycodes[i * 3] + "/" + gColorKeycodes[i * 3 + 1] + "/" + gColorKeycodes[i * 3 + 2] + "/" + i);
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
                            for (byte j = 0; j < gColorKeycodes.Length && !_validColor; j++)
                            {
                                //  If a keycode matches (user choice is a valid color)
                                if (_userInput == gColorKeycodes[j])
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


		static public byte[] ChooseBestPatterns(int AmountOfColors)
		{
			byte[] _bestPatterns = new byte[5];

			if (AmountOfColors < 3) _bestPatterns[0] = 2;
			else _bestPatterns[0] = 9;

			if (gRandom.Next(0, 100) < 50) _bestPatterns[1] = 1;
			else _bestPatterns[1] = 6;

			if (gRandom.Next(0, 100) > 40) _bestPatterns[2] = 3;
			else _bestPatterns[2] = 7;

			_bestPatterns[3] = 8;
			_bestPatterns[4] = 10;

			return _bestPatterns;
		}
		     // Choosing the best patterns
		static public List<byte> SelectPatternsForSaving(out List<string> _patternNames)
        {
            //  Reset choices of patterns
            List<byte> _chosen = new List<byte>();
            _patternNames = new List<string>();

            //  User input split by the special char ","
            string[] _choices = ReadLine().Split(',');

            for (int i = 0; i < _choices.Length; i++)
            {
                //  Id of the splitter between the pattern type and name
                int _helper = _choices[i].IndexOf('=');

                //  If the pattern is unnamed
                if (_helper == -1)
                {
                    if (byte.TryParse(_choices[i].Replace(" ", ""), out byte _validChoice))
                    {
                        if (_validChoice > 0 && _validChoice < 11)
                        {
                            //  Save pattern number
                            _chosen.Add(_validChoice);

                            //  Mark the pattern unnamed
                            _patternNames.Add("Unnamed");
                        }
						else if (gAdvInfo) Write("\n\t\tВыбранный номер не соответствует ни одному узору:" + _validChoice);
					}
                    else if (gAdvInfo) Write("\n\t\tНе удалось распознать номер узора: " + _choices[i].Replace(" ", ""));
				}

                //  If a pattern name might still be present
                else
                {
                    if (_choices[i].Length > 1)
                    {
                        //  Transform user input into a pattern number choice
                        if (byte.TryParse(_choices[i].Substring(0, _helper).Replace(" ", ""), out byte _validChoice))
                        {
                            if (_validChoice > 0 && _validChoice < 11)
                            {
                                //  Save pattern number
                                _chosen.Add(_validChoice);

                                //  Save pattern name
                                if (_helper + 1 < _choices[i].Length)
                                    _patternNames.Add(_choices[i].Substring(_helper + 1));
                                else _patternNames.Add("Unnamed");
                            }
                            else if (gAdvInfo) Write("\n\t\tВыбранный номер не соответствует ни одному узору:" + _validChoice);
                        }
                        else if (gAdvInfo) Write("\n\t\tНе удалось распознать номер узора: " + _choices[i].Substring(0, _helper).Replace(" ", ""));
                    }
                    else if (gAdvInfo) Write("\n\t\tНе верный формат данных: " + _choices[i]);
                }
            }

            //  Returning the chosen patterns, and their names
            return _chosen;
        }
             //  Let the user select the patterns for saving



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



        //-------------------  Array and List conversion  ------------------------------------------------------//

        static public List<byte> ToByteList(byte[] _array, int _startIndex = 0, int _endIndex = -1)
        {
            if (_array == null) return null;

            //  Set the parameters for a substring of the data
            if (_endIndex == -1 || _endIndex > _array.Length) _endIndex = _array.Length;

            //  Store list
            List<byte> _list = new List<byte>();

            //  Convert array to list
            for (int i = _startIndex; i < _endIndex; i++) _list.Add(_array[i]);

            //  Return the list
            return _list;
        }
             //  Converts a List(byte) to a Array(byte)
        static public byte[] ToByteArray(List<byte> _list, int _startIndex = 0, int _endIndex = -1)
        {
            if (_list == null) return null;

            //  Set the parameters for a substring of the data
            if (_endIndex == -1 || _endIndex > _list.Count) _endIndex = _list.Count;

            //  Store list
            byte[] _array = new byte[_list.Count];

            //  Convert list to array
            for (int i = _startIndex; i < _endIndex; i++) _array[i] = _list[i];

            //  Return the array
            return _array;
        }
             //  Converts a Array(byte) to a List(byte)


        static public List<string> ToStringList(string[] _array, int _startIndex = 0, int _endIndex = -1)
        {
            if (_array == null) return null;

            //  Set the parameters for a substring of the data
            if (_endIndex == -1 || _endIndex > _array.Length) _endIndex = _array.Length;

            //  Store list
            List<string> _list = new List<string>();

            //  Convert array to list
            for (int i = _startIndex; i < _endIndex; i++) _list.Add(_array[i]);

            //  Return the list
            return _list;
        }
             //  Converts a List(string) to a Array(string)
        static public string[] ToStringArray(List<string> _list, int _startIndex = 0, int _endIndex = -1)
        {
            if (_list == null) return null;

            //  Set the parameters for a substring of the data
            if (_endIndex == -1 || _endIndex > _list.Count) _endIndex = _list.Count;

            //  Store list
            string[] _array = new string[_list.Count];

            //  Convert list to array
            for (int i = _startIndex; i < _endIndex; i++) _array[i] = _list[i];

            //  Return the array
            return _array;
        }
             //  Converts a Array(string) to a List(string)
    }
}