using System;
using System.Collections.Generic;

using static System.Console;


using static Generatio.UI;
using static Generatio.GlobalSettings;
using static Generatio.GlobalVariables;



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

        static public bool ChooseColorType(UInt16 width, UInt16 height)
        {
            //  User input temporary buffer
            string userInput = "";

            //  Optimised error flag
            bool firstTry = true;

            //  While parsing was unsuccessfull
            while (userInput != "☺")
            {
				//  Reset UI, dont wait for additional input, print info
				ResetUI(!gIgnoreFullScreen, true);
				Write("\n\t\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");
				Write("\t\t[=]  - Выбранные размеры: " + width + " x " + height + "\n");


                //  Ask for color type
                Write("\n\t\t[?]  - Вы желаете выбрать свои цвета для узоров?");
                ForegroundColor = ConsoleColor.Gray;
                Write("\n\t\t        > ДА/YES <   - Свои цвета");
                Write("\n\t\t        > НЕТ/NO <   - Цвета из базы");
                ForegroundColor = ConsoleColor.White;



				//  Return error message if the user input was invalid and try again
				//  Interesting error detection - if we havent exited the loop:
				//   > Either its our first try, or the input was invalid
				if (!firstTry)
				{
					// Print error message
					ForegroundColor = ConsoleColor.Red;
					Write("\n\n\t\t[!]  - Не удалось распознать предыдущий выбор, повторите ввод: ");
					ForegroundColor = ConsoleColor.White;
				}
				else Write("\n\n\t\t[->] - Ваш выбор: ");


				//  Parse the user input
				userInput = ReadLine().Trim().ToLower();



                //  Check for custom colors choice, if so, return true = custom choice
                if (userInput == "да" || userInput == "д" || 
                    userInput == "yes" || userInput == "ye" || userInput == "y") return true;

                //  Check for asset colors choice, if so, exit the loop and return false = asset choice
                else if (userInput == "нет" || userInput == "не" || userInput == "н" ||
                    userInput == "no" || userInput == "n") userInput = "☺";

				// Reset the input, just in case the user inputs "☺" (Alt+1)
				else if (userInput == "☺") userInput = ""; 



                //  Reset error flag
                firstTry = false;
			}

            // Only possible to exit the loop if we got the asset colors choice, so we return false = asset choice
            return false;
        }
             // Get the users color type choice (custom or from assets)



        //---------------------------  Returns numbers  -------------------------------------------------------//


        static public byte GetColorAssetID(UInt16 width, UInt32 height)
        {
			string userInput;
			byte choice = 0;


			//  Reset UI, dont wait for additional input, print info
			ResetUI(!gIgnoreFullScreen, true);
			Write("\n\t\t\t\t\t\t\tВыбрано: --- === Создание узоров === ---\n\n\n");
			Write("\t\t[=]  - Выбранные размеры: " + width + " x " + height + "\n");


            //  Print all the options
			Write("\n\n\t\tВсего есть " + StoredColors.Length + " коллекций цветов:");
			for (Int32 i = 0; i < StoredColors.Length; i++)
			{
                //  Write the info about the current collection
				Write("\n\n\t\t  > " + (i + 1) + " <    ");


				for (Int32 j = 0; j < StoredColors[i].Length; j++)
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
				for (Int32 j = 0; j < 12 - StoredColors[i].Length; j++) Write("      ");

                //  Write the color amount in this collection
				Write("\t- " + StoredColors[i].Length + " цвета(ов)");
			}


            //  Get user choice
			while (choice > StoredColors.Length || choice < 1)
			{
				Write("\n\n\n\n\t\tПожалуйста, введите цифру от 1 до " + StoredColors.Length + " - ваш выбор цветовой палитры: ");
				userInput = ReadLine();
				userInput = userInput.Trim();
				if (byte.TryParse(userInput, out choice))
				{
					choice = byte.Parse(userInput);
					if (choice < 1 || choice > StoredColors.Length) Write("\t\tВведённое число не попадает в диапозон. Повторите ввод.\n");
				}
				else Write("\t\tНе удалось расспознать выбор. Повторите ввод.\n");
			}
			choice--;
			return choice;
		}
		// Let the user choose the colors from the assets

		static public UInt16 GetColorsAmount(UInt16 maxAmount)
		{
			UInt16 amount = 0;             // We will be storing the final parsed result here
			const Int32 minAmount = 2;    // Min amount is a constant
			string userInput;           // For user input parsing


			// Primary asking for the user choice for the amount of colors
			Write("\t\tВведите количество цветов (число, от " + minAmount + " и до " + maxAmount + ", или СЛУЧ/RND): ");


			while (amount < minAmount || amount > maxAmount)  // While we dont have a valid amount of colors
			{
				userInput = ReadLine().Trim().ToLower();    // Parsing the user input

				if (UInt16.TryParse(userInput, out amount))   // Try converting it to a number
				{
					// Succesful conversion to a number

					// Check if the number is bigger than the max valid result, if so, return error
					if (amount < minAmount) Write("\t\t[!]  - Число меньше допустимого значения, повторите ввод: ");

					// Check if the number is lower  that the min valid result, if so, return error
					else if (amount > maxAmount) Write("\t\t[!]  - Число больше допустимого значения, повторите ввод: ");
				}
				else if (userInput == "случайно" || userInput == "случ" || userInput == "random" || userInput == "rand" || userInput == "rnd")
				{
					//  Choose a random amount if the user wants so
					amount = (UInt16)gRandom.Next(Math.Max(5, maxAmount / 4), maxAmount + 1);
					

                    //  Print the randomly chosen number
					Write("\t\t[i]  - Выбрано: " + amount + "\n");                       
				}

				// Write error message
				else Write("\t\t[!]  - Не удалось распознать число, повторите ввод: ");
			}


			// return the chosen amount
			return amount; 
		}
		     // Get the amount of colors (for custom clors choice)



		static public UInt16 GetSize(string sizeType, UInt16 last = 0)
        {
            UInt16 pictureSize = 1;            //  Storing the final parsed result
			UInt16 maxSize, realMax;      //  Max allowed size, and maxWindowSize
            const Int32 minSize = 5;     // Min size is a constant
            string userInput;      // For user input parsing


			//  Forcing fullscreen to capture max window size (optional)
			if (!gIgnoreFullScreen) ForceFullScreen();  


            //  Set the limit for the max pattern size
            if (sizeType == "высот") maxSize = (UInt16)Math.Min(65535, (WindowHeight - 1));
            else maxSize = (UInt16)Math.Min(65535, (WindowWidth - 1) / 2);

            //  Save the window max for info, and rnd generator
			realMax = maxSize;

			//  Set the size to the absolute max if a cheatcode is enabled
			if (maxSize < 65535 && gNoSizeLimit) maxSize = 65535;



			//  Reset the window UI
			ResetSizeUI(sizeType, maxSize, realMax, last);
			Write("\n\t\t[->] - Ваш выбор: ");



			//  While we dont have a valid size
			while (pictureSize != 0 && pictureSize < minSize || pictureSize > maxSize)
            {
				//  Parsing the user input
				userInput = ReadLine().Trim().ToLower();


				//  Try converting it to a number
				if (UInt16.TryParse(userInput, out pictureSize))
                {
                    //  Check if the number is bigger than the max valid result, if so, return error
                    if (pictureSize < minSize && pictureSize != 0)
                    {
						//  Reset the window UI
						ResetSizeUI(sizeType, maxSize, realMax, last);

						ForegroundColor = ConsoleColor.Red;
                        Write("\n\t\t[!]  - Число меньше допустимого значения, повторите ввод: ");
                        ForegroundColor = ConsoleColor.White;
                    }


                    //  Check if the number is lower  that the min valid result, if so, return error
                    else if (pictureSize > maxSize)
                    {
						//  Reset the window UI
						ResetSizeUI(sizeType, maxSize, realMax, last);

						ForegroundColor = ConsoleColor.Red;
                        Write("\n\t\t[!]  - Число больше допустимого значения, повторите ввод: ");
                        ForegroundColor = ConsoleColor.White;
                    }
                }


                //  Random number choice
                else if (userInput == "случайно" || userInput == "случ" || userInput == "с" ||
                    userInput == "random" || userInput == "rand" || userInput == "rnd" || userInput == "r")
                {
                    //  Choose a "random" size
                    pictureSize = (UInt16)gRandom.Next(Math.Max(5, realMax / 4), realMax + 1);

                    //  Output the randomly chosen number
                    Write("\t\t[i]  - Выбрано: " + pictureSize + "\n");
                }


                //  Invalid input - Write error message
                else
                {
					//  Reset the window UI
					ResetSizeUI(sizeType, maxSize, realMax, last);

					//  Write error message
					ForegroundColor = ConsoleColor.Red;
                    Write("\n\t\t[!]  - Не удалось распознать число, повторите ввод: ");
                    ForegroundColor = ConsoleColor.White;


					//  Reset loop logic
					pictureSize = 1;
				}
            }
            return pictureSize; // return chosen picture size
        }
             // Get for the size for the patterns (both X, and Y independently)

        
        

		/*static public long Factorial(Int32 Number)
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
			bool validTask = false;
			string userInput = "";

			Write("\n\n\n\t\t[?]  - Что вы хотите сделать?");
			Write("\n\t\t   > 1 <    - О программе");
			Write("\n\t\t   > 2 <    - Создать узоры\n");

			Write("\n\t\t   > 3 <    - Галерея узоров");
			Write("\n\t\t   > 4 <    - Обновить последнее сохранение галереи\n");

			Write("\n\t\t   > 5 <    - Настройки");
			Write("\n\t\t   > 6 <    - Обновить сохранение настроек\n");

			Write("\n\t\t   > 0 <    - Выход\n");


			while (!validTask)
			{
				Write("\n\t\tВаш выбор: ");
				userInput = ReadLine().Trim();
				if (userInput == "0" || userInput == "1" || userInput == "2" || userInput == "3"
									  || userInput == "4" || userInput == "5" || userInput == "6")
					validTask = true;
				else Write("\t\t[!]  - Не удалось распознать выбор. Пожалуйста, повторите ввод\n");
			}
			return userInput;
		}
		     // Get the users wanted task



		//------------------------  Returns arrays (mostly)  -------------------------------------------------//


		static public byte[] GetCustomColors(UInt16 colAmount)
        {
            //  Final color storing
            byte[] colors = new byte[colAmount];

            //  Temporary buffer for currently selected color
            byte selectedColor = 0;

            //  Temporary buffer for the user input
            string userInput = "";

            //  Amount of the different chosen colors (to get at least 2 different colors from the bunch)
            bool differentColors = false;
            byte lastColor = 0;

            //  Flag for parsing the currently selected color
            bool validColor;

            //-------------  WRITING INFO  --------------------//

            Write("\n\n\n\t\t\t\t\tЦвета на выбор:\n\n");
            for (Int32 i = 0; i < 16; i++)
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
            for (Int32 i = 0; i < colAmount; i++)
            {
                validColor = false;
                while (!validColor)
                {
                    //  If we arent generating every color
                    //  then ask the user for a color choice
                    if (userInput != "всеслучайно" && userInput != "всеслуч")
                    {
                        Write("\n\t\t" + (i + 1) + "-ый цвет) ");

                        //  Get user input
                        userInput = ReadLine();

                        //  Parse user input
                        userInput = userInput.ToLower();
                        userInput = userInput.Replace("-", "");
                        userInput = userInput.Replace(" ", "");
                        userInput = userInput.Replace("ё", "е");
                    }

                    //  Check for a rnd color
                    if (userInput == "случайно" || userInput == "случ" ||
                        userInput == "всеслуч" || userInput == "всеслучайно")
                    {
                        //  Choose a random color
                        selectedColor = (byte)gRandom.Next(0, 16);

                        //  Check if it it unique 
                        if (i != 0 && selectedColor != lastColor) differentColors = true;
                        // Update the last color for the next unique check
                        lastColor = selectedColor;

                        //  If we are already at the last choosable color
                        if (i == colAmount - 1)
                        {
                            while (!differentColors)
                            {
                                //  Choose a new random color (15/16 chance of being valid)
                                selectedColor = (byte)gRandom.Next(0, 16);

                                //  Check if it is unique
                                //  If so - exit the loop
                                if (selectedColor != lastColor) differentColors = true;
                            }
                        }

                        //  Set the color flag to valid
                        validColor = true;
                    }

                    //  If not a rnd color - parse the user color choice
                    else
                    {
                        //  Try parse the colors by the number
                        for (byte j = 0; j < 16; j++)
                        {
                            //  If the user choice is a valid color number
                            if (userInput == j.ToString())
                            {
                                //  Save the color 
                                selectedColor = j;

                                // exit the loop
                                validColor = true;
                                j += 16;
                            }
                        }

                        //  If number parsing failed
                        if (!validColor)
                        {
                            //  Try parse by the color keycodes
                            for (byte j = 0; j < gColorKeycodes.Length && !validColor; j++)
                            {
                                //  If a keycode matches (user choice is a valid color)
                                if (userInput == gColorKeycodes[j])
                                {
                                    //  Save the color
                                    selectedColor = (byte)(j / 3);

                                    //  Exit the loop
                                    validColor = true;
                                }
                            }
                        }


                        //  If the color parsing failed (unknown choice) - write error message
                        if (!validColor) Write("\t\tНе удалось распознать цвет. Пожалуйста, повторите ввод\n");

                        //  Else if the parsed color is valid
                        //  
                        //  But it is the last possible choice for a color
                        //  And there arent any different colors
                        else if (i == (colAmount - 1))
                        {
                            //  Unique color check logic
                            if (i != 0 && selectedColor != lastColor) differentColors = true;

                            if (!differentColors)
                            {
                                //  Invalidate this color
                                validColor = false;

                                //  Write error message
                                Write("\t\tДолжно быть хотя бы 2 различных цвета\n");
                            }
                        }
                    }
                }

                //  Save the valid color
                colors[i] = selectedColor;


                //  Unique color check logic
                if (i != 0 && selectedColor != lastColor) differentColors = true;

                //  Update the last color for the next unique check
                lastColor = selectedColor;


                //  Write success message
                Write("\t\tЦвет распознан! ");

                ForegroundColor = gAllColors[selectedColor];
                if (selectedColor == 15) BackgroundColor = ConsoleColor.White;

                Write((i + 1) + "-ый цвет: " + gColorNames[selectedColor]);

                //  Reset output formatting
                BackgroundColor = ConsoleColor.Black;
                ForegroundColor = ConsoleColor.White;
                Write("\n");
            }

            //  Return the selected colors
            return colors;
        }
		     // Get the colors for the custom user choice


		static public void RankPatterns(Int32 colAmount, out List<Byte> best, out List<Byte> remaining)
		{
            best = new List<byte>();
            remaining = new List<byte>();

            if (colAmount < 3)
            {
                best.Add(2);
                remaining.Add(9);
            }
            else
            {
                best.Add(9);
                remaining.Add(2);
            }

            if (gRandom.Next(0, 100) < 50)
            {
                best.Add(1);
                remaining.Add(6);
            }
            else
            {
                best.Add(6);
                remaining.Add(1);
            }

            if (gRandom.Next(0, 100) > 40)
            {
                best.Add(3);
                remaining.Add(7);
            }
            else
            {
                best.Add(7);
                remaining.Add(3);
            }

            best.Add(8);
            best.Add(10);
            remaining.Add(4);
            remaining.Add(5);
		}
		     // Choosing the best patterns
		static public List<byte> SelectPatternsForSaving(out List<string> patternNames)
        {
            //  Reset choices of patterns
            List<byte> chosen = new List<byte>();
            patternNames = new List<string>();

            //  User input split by the special char ","
            string[] choices = ReadLine().Split(',');

            for (Int32 i = 0; i < choices.Length; i++)
            {
                //  Id of the splitter between the pattern type and name
                Int32 helper = choices[i].IndexOf('=');

                //  If the pattern is unnamed
                if (helper == -1)
                {
                    if (byte.TryParse(choices[i].Replace(" ", ""), out byte validChoice))
                    {
                        if (validChoice > 0 && validChoice < 11)
                        {
                            //  Save pattern number
                            chosen.Add(validChoice);

                            //  Mark the pattern unnamed
                            patternNames.Add("Unnamed");
                        }
						else if (gAdvInfo) Write("\n\t\tВыбранный номер не соответствует ни одному узору:" + validChoice);
					}
                    else if (gAdvInfo) Write("\n\t\tНе удалось распознать номер узора: " + choices[i].Replace(" ", ""));
				}

                //  If a pattern name might still be present
                else
                {
                    if (choices[i].Length > 1)
                    {
                        //  Transform user input into a pattern number choice
                        if (byte.TryParse(choices[i].Substring(0, helper).Replace(" ", ""), out byte validChoice))
                        {
                            if (validChoice > 0 && validChoice < 11)
                            {
                                //  Save pattern number
                                chosen.Add(validChoice);

                                //  Save pattern name
                                if (helper + 1 < choices[i].Length)
                                    patternNames.Add(choices[i].Substring(helper + 1));
                                else patternNames.Add("Unnamed");
                            }
                            else if (gAdvInfo) Write("\n\t\tВыбранный номер не соответствует ни одному узору:" + validChoice);
                        }
                        else if (gAdvInfo) Write("\n\t\tНе удалось распознать номер узора: " + choices[i].Substring(0, helper).Replace(" ", ""));
                    }
                    else if (gAdvInfo) Write("\n\t\tНе верный формат данных: " + choices[i]);
                }
            }

            //  Returning the chosen patterns, and their names
            return chosen;
        }
             //  Let the user select the patterns for saving



		static public string GetSplitters(string input, string criteria)
        {
            string splitters = "";

            for(Int32 i = 0; i < input.Length; i++)
            {
                for (Int32 j = 0; j < criteria.Length; j++) // For every splitter in the criterias string
                {
                    if (input[i] == criteria[j])
                    {
                        // Save the found splitter
                        splitters += input[i];

                        // Exit the loop - find the next splitter
                        j += criteria.Length;
                    }
                }
            }

            return splitters;
        }
             // Returns a string (array of chars): Get the splitters in the input strings that match the criteria
        static public string[] SplitForParsing(string input, string criterias)
        {
            //  Future length for the string array
            //
            //  Consists of how many splitters from the criterias string we have found in our input string (+1)
            Int32 arrayLength = 1;

            //  Save the position for the last found splitter
            //  Needed to save the new section between the splitters
            Int32 lastSplitCharPos = 0;

            for (Int32 i = 0; i < input.Length; i++)
            {
                for (Int32 j = 0; j < criterias.Length; j++)  //  For every splitter in the criterias string
                {
                    if (input[i] == criterias[j])  //  If the splitter is found
                    {
                        arrayLength++;             //  Increase the future array length
                        j += criterias.Length;     //  Exit the loop - go find the next splitter
                    }
                }
            }

            //  Initialize the string array with the calculated length
            string[] result = new string[arrayLength];

            for (Int32 i = 0; i < arrayLength; i++)          //  For every string in the string array   //
            {

                for (Int32 j = lastSplitCharPos; j < input.Length; j++)
                {                                           //  For every interval in the input string //

                    for (Int32 k = 0; k < criterias.Length; k++)
                    {       //  Distinquish the intervals from each others by the criteria characters  //

                        if (input[j] == criterias[k])
                        {
                            //  If the splitter is found                                               //

                            //  Extract the found interval from the last splitter to the current splitter
                            result[i] += input.Substring(lastSplitCharPos, j - lastSplitCharPos);

                            //  Save the new last found splitter position
                            lastSplitCharPos = j + 1;

                            //  Exit the loops to increase the saving index
                            j += input.Length;
                            k += criterias.Length;
                        }
                        else if (j == input.Length - 1)
                        {
                            //  If we got to the end of the input string                               //
                            //  So we need to save the last remaining interval                         //


                            //  Extract the last interval from the last splitter to the end of the string
                            //
                            //  If the last char of or input is a splitter - save all but it
                            for (Int32 l = 0; l < criterias.Length; l++)
                            {
                                if(input[input.Length - 1] == criterias[l])
                                {
                                    //  Save the last input substring without the splitter (last char)
                                    result[i] += input.Substring(lastSplitCharPos, j - lastSplitCharPos);

                                    //  Exit the criteria splitters search loop
                                    l += criterias.Length;

                                    //  Logic flag for singular save
                                    lastSplitCharPos = input.Length;
                                }
                            }
                            if (lastSplitCharPos != input.Length)
                            {
                                //  Save all the last characters from the input string
                                result[i] += input.Substring(lastSplitCharPos, j - lastSplitCharPos + 1);

                                //  Exit the loops for good
                                lastSplitCharPos = input.Length;
                            }


                            //  Exit the loops for good
                            j += input.Length;
                            k += criterias.Length;
                        }
                    }
                }
            }

            // return new splitted string array
            return result;  
        }
             // Split the input string into a string array by the criteria chars



        static public ConsoleColor[] ConvertColorsToConsole(byte[] colorIdx)
        {
            ConsoleColor[] colors = new ConsoleColor[colorIdx.Length];
            for (Int32 i = 0; i < colors.Length; i++) colors[i] = gAllColors[colorIdx[i]];
            return colors;
        }
             // Convert colors from bytes to console colors

        static public byte[] ConvertColorsToBytes(ConsoleColor[] colors)
        {
            byte[] byteColors = new byte[colors.Length];
            for (Int32 i = 0; i < byteColors.Length; i++)
            {
                for (byte colorId = 0; colorId < 16; colorId++)
                {
                    if (gAllColors[colorId] == colors[i]) byteColors[i] = colorId;
                }
            }
            return byteColors;
        }
             // Convert colors from console colors to bytes



        //-------------------  Array and List conversion  ------------------------------------------------------//

        static public List<byte> ToByteList(byte[] array, Int32 startIndex = 0, Int32 endIndex = -1)
        {
            if (array == null) return null;

            //  Set the parameters for a substring of the data
            if (endIndex == -1 || endIndex > array.Length) endIndex = array.Length;

            //  Store list
            List<byte> list = new List<byte>();

            //  Convert array to list
            for (Int32 i = startIndex; i < endIndex; i++) list.Add(array[i]);

            //  Return the list
            return list;
        }
             //  Converts a List(byte) to a Array(byte)
        static public byte[] ToByteArray(List<byte> list, Int32 startIndex = 0, Int32 endIndex = -1)
        {
            if (list == null) return null;

            //  Set the parameters for a substring of the data
            if (endIndex == -1 || endIndex > list.Count) endIndex = list.Count;

            //  Store list
            byte[] array = new byte[list.Count];

            //  Convert list to array
            for (Int32 i = startIndex; i < endIndex; i++) array[i] = list[i];

            //  Return the array
            return array;
        }
             //  Converts a Array(byte) to a List(byte)


        static public List<string> ToStringList(string[] array, Int32 startIndex = 0, Int32 endIndex = -1)
        {
            if (array == null) return null;

            //  Set the parameters for a substring of the data
            if (endIndex == -1 || endIndex > array.Length) endIndex = array.Length;

            //  Store list
            List<string> list = new List<string>();

            //  Convert array to list
            for (Int32 i = startIndex; i < endIndex; i++) list.Add(array[i]);

            //  Return the list
            return list;
        }
             //  Converts a List(string) to a Array(string)
        static public string[] ToStringArray(List<string> list, Int32 startIndex = 0, Int32 endIndex = -1)
        {
            if (list == null) return null;

            //  Set the parameters for a substring of the data
            if (endIndex == -1 || endIndex > list.Count) endIndex = list.Count;

            //  Store list
            string[] array = new string[list.Count];

            //  Convert list to array
            for (Int32 i = startIndex; i < endIndex; i++) array[i] = list[i];

            //  Return the array
            return array;
        }
             //  Converts a Array(string) to a List(string)
    }
}