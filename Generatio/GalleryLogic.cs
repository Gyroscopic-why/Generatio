using System;
using System.Collections.Generic;

using static System.Console;


using static Generatio.PatternSource;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
using static Generatio.CustomProcedures;
using static Generatio.DataManipulation;


namespace Generatio
{
    internal class GalleryLogic
    {
        public class GalleryPattern
        {
            //-----------------------   Variables  ----------------------------------//


            //  Pattern number index or type 
            private readonly byte patternType;

            //  Dimensions for the pattern
            private readonly UInt16 width;
            private readonly UInt16 height;

            //  Amount of colors
            static private UInt16 colAmount;

            //  Array of colors (in bytes))
            private readonly byte[] colors = new byte[colAmount];

            //  Name of the current pattern (origin/date/time/author/etc)
            private readonly string name;

            //-----------------------  Functions  ----------------------------------//



            public GalleryPattern(byte iPatternType,
                UInt16 iWidth, UInt16 iHeight,
                UInt16 iColAmount, byte[] iColors,
                string iName = "Unnamed")
            {
                // i - input

                patternType = iPatternType;
                width = iWidth;
                height = iHeight;
                colAmount = iColAmount;
                colors = iColors;
                name = iName;
            }
                 //  Constructor

            public void Generate()
            {
                //  All posible pattern types dictionary
                var patterns = new Dictionary<byte, Action>()  {
                    { 1,  () => Pattern1 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 2,  () => Pattern2 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 3,  () => Pattern3 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 4,  () => Pattern4 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 5,  () => Pattern5 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 6,  () => Pattern6 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 7,  () => Pattern7 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 8,  () => Pattern8 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 9,  () => Pattern9 (width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                    { 10, () => Pattern10(width, height, ConvertColorsToConsole(colors), true, name, false, gShowInfo) },
                };

                //  Generete the chosen pattern from the dictionary
                patterns[patternType]();
            }
            //  Universal pattern generation by code

            public byte GetPatternType() { return patternType; }
                 //  Returns the pattern type

            public UInt16 GetWidth() { return width; }
                 //  Returns the pattern width

            public UInt16 GetHeight() { return height; }
                 //  Returns the pattern height

            public UInt16 GetColAmount() { return colAmount; }
                 //  Returns the amount of colors in the pattern 

            public byte[] GetColorsBytes() { return colors; }
                 //  Returns the colors in bytes[]

            public ConsoleColor[] GetColorsConsole() { return ConvertColorsToConsole(colors); }
                 //  Returns the colors in ConsoleColor[]

        }


        static public List<GalleryPattern> StockGallery  = new List<GalleryPattern>();
        static public List<GalleryPattern> UserGallery   = new List<GalleryPattern>();
        static public List<GalleryPattern> GalleryBuffer = new List<GalleryPattern>();

        
        static public List<GalleryPattern> ConvertToGalleryPattern(List<string> _data, bool _showInfo = false)
        {
            if (_data != null && _data.Count > 1)
            {
                //  Storing here the successfully converted gallery patterns
                List<GalleryPattern> _converted = new List<GalleryPattern>();

                string[] _parserHelper;
                List<byte> _colors = new List<byte>();

                string _name = "Unnamed";
                byte _patternType;
                UInt16 _width;
                UInt16 _height;
                UInt16 _expectedColAmount;

                for (int i = 0; i < _data.Count; i++)
                {
                    //  If the line is expected to have the pattern name
                    if (_data[i].IndexOf(",") == -1)
                    {
                        _name = _data[i].Trim();

                        if (_showInfo)
                        {
                            //  Show the name of the pattern
                            Write("\n\t\tРаспознанно название узора: " + _name + "\n");
                        }
                    }

                    //  If the line is expected to have pattern data
                    else
                    {
                        //  Transform the data into more easily usable
                        _parserHelper = SplitForParsing(_data[i], ",");

                        //  If the data is at lest a full pattern with 2+ colors
                        if (_parserHelper.Length > 5)
                        {
                            //  Try parse the pattern type
                            if (byte.TryParse(_parserHelper[0], out _patternType)
                                && _patternType > 0 && _patternType < 11)
                            {
                                //  Try parse the pattern size
                                if (UInt16.TryParse(_parserHelper[1].Trim(), out _width)
                                    && UInt16.TryParse(_parserHelper[2].Trim(), out _height)
                                    && _width > 0 && _height > 0)
                                {
                                    //  Try parse the amount of colors (2+ not 0 and not 1)
                                    if (UInt16.TryParse(_parserHelper[3].Trim(), out _expectedColAmount)
                                        && _expectedColAmount > 1)
                                    {
                                        //  Show the parsed base parameters
                                        if (_showInfo)
                                        {
                                            Write("\n\t\tУспешно распознанны базовые параметры!");
                                            Write("\n\t\tТип узора: " + _patternType);
                                            Write("\n\t\tРазмеры узора: " + _width + "x" + _height);
                                            Write("\n\t\tКоличество цветов: " + _expectedColAmount + "\n");
                                        }

                                        //  Try parse the colors
                                        for (int j = 4; j < 4 + _expectedColAmount; j++)
                                        {
                                            //  If the color is out of bounds - break from loop
                                            if (j >= _parserHelper.Length) j += _expectedColAmount;
                                            else
                                            {
                                                //  Try parse the color
                                                if (byte.TryParse(_parserHelper[j], out byte _holdColor))
                                                {
                                                    //  If the color is valid - add it to the list
                                                    _colors.Add(_holdColor);
                                                }
                                                else if (_showInfo)
                                                {
                                                    //  If the color is invalid - output error
                                                    Write("\n\t\tНе удалось распознать цвет: " + _parserHelper[j] + "\n");
                                                }
                                            }
                                        }

                                        //  Print the parsed colors
                                        if (_showInfo)
                                        {
                                            if (_colors.Count > 0)
                                            {
                                                Write("\n\t\tУспешно распознанны цвета: ");
                                                for (int j = 0; j < _colors.Count; j++)
                                                {
                                                    //  Convert colors to a byte array
                                                    //  For the special display feature
                                                    byte[] _colorBuffer = new byte[_colors.Count];
                                                    for (int k = 0; k < _colors.Count; k++) _colorBuffer[k] = _colors[k];


                                                    //  Show the successfully parsed color
                                                    ForegroundColor = ConvertColorsToConsole(_colorBuffer)[j];
                                                    if (_colorBuffer[j] == 15) BackgroundColor = ConsoleColor.White;
                                                    Write(_colors[j]);


                                                    // Reset text highlighting
                                                    BackgroundColor = ConsoleColor.Black;
                                                    ForegroundColor = ConsoleColor.White;
                                                    Write("  ");
                                                }
                                            }
                                            else
                                            {
                                                //  If the color parsing failed - output error
                                                Write("\n\t\tНе удалось распознать цвета\n");
                                            }
                                        }


                                        //  After data parsing and conversion
                                        //  We check if we have enough data to check for its validation
                                        if (_patternType > 0 && _width > 0 && _height > 0 && _expectedColAmount > 0 && _colors.Count > 0)
                                        {
                                            //  If the real (parsed) colors amount is equal to the expected one
                                            if (_colors.Count == _expectedColAmount)
                                            {
                                                //  Add the successfully converted pattern to the list
                                                _converted.Add(new GalleryPattern(_patternType, _width, _height, _expectedColAmount, _colors.ToArray(), _name));

                                                if (_showInfo)
                                                {
                                                    //  Highlight the success of a pattern conversion
                                                    ForegroundColor = ConsoleColor.Green;
                                                    Write("\n\t\tУспешно распознан узор!\n");
                                                    ForegroundColor = ConsoleColor.White;
                                                }
                                            }
                                            else if (_showInfo)
                                            {
                                                //  Highlight pattern conversion error
                                                ForegroundColor = ConsoleColor.Red;
                                                Write("\n\t\tОшибка данных: настоящее количество цветов не совпадает с указанным");
                                                ForegroundColor = ConsoleColor.White;
                                                Write("\n\t\tУказано: " + _expectedColAmount + ", обнаружено: " + _colors.Count + "\n");
                                            }

                                            //  Reset the colors and name to avoid data mixing
                                            _name = "Unnamed";
                                            _colors.Clear();
                                        }
                                    }
                                    else if (_showInfo)
                                        //  Try parse the last parameter (amount of colors)
                                        //  If failes - output error
                                        Write("\n\t\tНе удалось распознать количество цветов: " + _parserHelper[3] + "\n");
                                }
                                //  Show error parsing the size if needed
                                else if (_showInfo) Write("\n\t\tНе удалось распознать размеры узора: " + _parserHelper[1] + "x" + _parserHelper[2] + "\n");
                            }
                            //  Show error parsing the type if needed
                            else if (_showInfo) Write("\n\t\tНе удалось распознать тип узора: " + _parserHelper[0] + "\n");
                        }
                    }
                }

                //  Show the amount of successfully converted patterns or an error
                if (_showInfo)
                {
                    if (_converted.Count > 0) Write("\n\t\tУспешно обнаружено: " + _converted.Count + " узоров" + "\n");
                    else Write("\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
                }

                //  Return the successfully converted patterns
                return _converted;
            }

            //  return null if the data is invalid
            if (_showInfo) Write("\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
            return null;
        }


        static public List<int> ParseGalleryChoice(int _gallerySize, out bool _choiceIsExit)
        {
            string _userInput;

            //  Final list of all parsed patterns
            List<int> _parsedId = new List<int>();
            
            //  Temporary buffer for the help of parsing the user input
            int[] _helperId = new int[2];


            //  Get the user input
            Write("\n\t\t[->] - Ваш выбор: ");
            _userInput = ReadLine().Trim();


            //  Gallery exit logic
            if (_userInput == "0")
            {
                _choiceIsExit = true;
                return null;
            }


            //  If not exit - get the chosen patterns
            //
            //  Parse single pattern choice
            if (int.TryParse(_userInput, out _helperId[0]))
            {
                if (_helperId[0] > 0 && _helperId[0] < _gallerySize + 1)
                {
                    _parsedId.Add(_helperId[0] - 1);
                    Write("\n\t\tРаспознанный узор: " + _helperId[0] + "\n\n");
                }
                else if (_helperId[0] != 0) Write("\n\t\tИндекс вне границ базы данных. Повторите ввод");
            }

            //  Parse multiple patterns choice
            else
            {
                string[] _multiId = SplitForParsing(_userInput, "-/, ");
                string _splitters = GetSplitters(_userInput, "-/, ");

                if (_multiId.Length > 1)
                {
                    for (int i = 0; i < _multiId.Length; i += 2)
                    {
                        if (i < _multiId.Length - 1 && _multiId[i] != null && _multiId[i + 1] != null)
                        {
                            if (int.TryParse(_multiId[i].Trim(), out _helperId[0])
                                && int.TryParse(_multiId[i + 1].Trim(), out _helperId[1]))
                            {

                                if (_splitters[i / 2] == '-')
                                {

                                    //-----------  Limit the parsed intervals  -----------//
                                    //                                                    //
                                    _helperId[0] = Math.Min(_helperId[0], _gallerySize);  //
                                    _helperId[0] = Math.Max(_helperId[0], 1);             //
                                    //                                                    //
                                    _helperId[1] = Math.Min(_helperId[1], _gallerySize);  //
                                    _helperId[1] = Math.Max(_helperId[1], 1);             //
                                    //                                                    //                                                    //
                                    //-----------  Limit the parsed intervals  -----------//


                                    //  Print success message
                                    if (gAdvInfo) Write("\n\t\tРаспознан интервал с " + _helperId[0] + " по " + _helperId[1]);


                                    //  Check for a normal interval (from lower to bigger)
                                    for (int j = 0; j <= _helperId[1] - _helperId[0]; j++)
                                    {
                                        //  Add normal interval
                                        _parsedId.Add(_helperId[0] + j - 1);
                                    }


                                    //  Check for an inverted interval (from bigger to lower)
                                    for (int j = 0; j <= _helperId[0] - _helperId[1]; j++)
                                    {
                                        //  Add inverted interval
                                        _parsedId.Add(_helperId[0] - j - 1);
                                    }
                                }
                                else if (_splitters[i / 2] == '/' || _splitters[i / 2] == ',' || _splitters[i / 2] == ' ')
                                {
                                    if (int.TryParse(_multiId[i], out _helperId[0]))
                                    {
                                        if (_helperId[0] > 0 && _helperId[0] < _gallerySize + 1)
                                        {
                                            _parsedId.Add(_helperId[0] - 1);
                                            if (gAdvInfo) Write("\n\t\tРаспознанный узор: " + _helperId[0] + "\n");
                                        }
                                    }
                                    if (int.TryParse(_multiId[i + 1], out _helperId[1]))
                                    {
                                        if (_helperId[1] > 0 && _helperId[1] < _gallerySize + 1)
                                        {
                                            _parsedId.Add(_helperId[1] - 1);
                                            if (gAdvInfo) Write("\n\t\tРаспознанный узор: " + _helperId[1] + "\n");
                                        }
                                    }
                                }
                            }
                        }
                        else if (int.TryParse(_multiId[i].Trim(), out _helperId[1]))
                        {
                            if (_splitters.Length > 0 && _splitters[i - 1] == '-' && _multiId.Length > 1)
                            {
                                if (int.TryParse(_multiId[i - 1].Trim(), out _helperId[0]))
                                {
                                    //-----------  Limit the parsed intervals  -----------//
                                    //                                                    //
                                    _helperId[0] = Math.Min(_helperId[0], _gallerySize);  //
                                    _helperId[0] = Math.Max(_helperId[0], 1);             //
                                    //                                                    //
                                    _helperId[1] = Math.Min(_helperId[1], _gallerySize);  //
                                    _helperId[1] = Math.Max(_helperId[1], 1);             //
                                    //                                                    //                                                    //
                                    //-----------  Limit the parsed intervals  -----------//


                                    //  Print success message
                                    if (gAdvInfo) Write("\n\t\tРаспознан интервал с " + _helperId[0] + " по " + _helperId[1]);


                                    //  Check for a normal interval (from lower to bigger)
                                    for (int j = 1; j <= _helperId[1] - _helperId[0]; j++)
                                    {
                                        //  Add normal interval
                                        _parsedId.Add(_helperId[0] + j - 1);
                                    }


                                    //  Check for an inverted interval (from bigger to lower)
                                    for (int j = 0; j <= _helperId[0] - _helperId[1] - 1; j++)
                                    {
                                        //  Add inverted interval
                                        _parsedId.Add(_helperId[0] - j - 1);
                                    }
                                }
                            }
                            else if (_helperId[1] > 0 && _helperId[1] < _gallerySize + 1)
                            {
                                _parsedId.Add(_helperId[1] - 1);
                                if (gAdvInfo) Write("\n\t\tРаспознанный узор: " + _helperId[1] + "\n\n");
                            }
                        }
                    }
                }
                else Write("\n\t\tНе удалось распознать значения диапозона. Повторите ввод");
            }

            _choiceIsExit = false;
            return _parsedId;
        }


        static public void UpdateStockGallery()
        {
            //  Read the stock data for the gallery
            List<string> _parsedData = ReadData(gGalleryPath, "1stock.patterns", gShowInfo, false, "\t\t", "\n");
            
            //  Parse the data into more easily usable
            _parsedData = ParseData(_parsedData, true, true, "*", "", "*", gShowInfo, false, "\t\t");

            //  Convert the data into patterns
            GalleryBuffer = ConvertToGalleryPattern(_parsedData, gAdvInfo);

            if (GalleryBuffer != null)
            {
                //  Update the gallery
                StockGallery = GalleryBuffer;
            }
        }

        static public void UpdateUserGallery()
        {
            List<string> _patternFiles = ToStringList(GetFiles(gGalleryPath, true, gShowInfo, false, "\t\t"));

            if (_patternFiles != null)
            {
                //  Remove the unrelated files from the search
                for (int i = 0; i < _patternFiles.Count; i++)
                {
                    //  Check if the file doesnt have the .patterns extention
                    if (_patternFiles[i].Length < 9 ||
                        _patternFiles[i].IndexOf(".patterns", _patternFiles[i].Length - 9) == -1 ||
                        _patternFiles[i].IndexOf("stock.") != -1)
                    {
                        //  Remove the unrelated file, including:
                        //  - Not .patterns files
                        //  - Stock patterns
                        _patternFiles.RemoveAt(i);
                        i--;
                    }
                }

                //  Reset the previous gallery save
                UserGallery.Clear();

                //  Loop through all the pattern files
                foreach (string _file in _patternFiles)
                {
                    //  Read the stock data for the gallery
                    List<string> _parsedData = ReadData(gGalleryPath, _file, gShowInfo, false, "\t\t");

                    //  Parse the data into more easily usable
                    _parsedData = ParseData(_parsedData, true, true, "*", "", "*", gShowInfo, false, "\t\t");

                    //  Convert the data into patterns
                    GalleryBuffer = ConvertToGalleryPattern(_parsedData, gAdvInfo);

                    //  Update the gallery
                    foreach (GalleryPattern _pattern in GalleryBuffer)
                    {
                        //  Add the pattern to the gallery
                        UserGallery.Add(_pattern);
                    }
                }
            }
        }


        /*static public void GenerateGallery()
        {
            gGeneratedGpatterns = true;

            byte[] Colors0 = { 4, 8, 10, 4, 0, 6, 0, 4, 2, 8, 2, 5, 4, 10, 6 };
            GalleryPattern item0 = new GalleryPattern(3, 60, 30, 15, Colors0);
            Gallery.Add(item0);

            byte[] Colors1 = { 10, 4, 0, 6, 8, 2, 11, 5 };
            GalleryPattern item1 = new GalleryPattern(3, 60, 30, 8, Colors1);
            Gallery.Add(item1);

            byte[] Colors2 = { 11, 8, 7, 1, 2, 5, 11, 5, 2, 1, 7, 8, 11, 4, 10 };
            StoredPatterns[2] = new GalleryPattern(3, 40, 40, 15, Colors2);

            byte[] Colors3 = { 11, 8, 10, 4, 10, 8, 11, 5 };
            StoredPatterns[3] = new GalleryPattern(7, 60, 30, 8, Colors3);

            byte[] Colors4 = { 9, 3, 1, 7, 1, 7, 1, 3, 9 };
            StoredPatterns[4] = new GalleryPattern(4, 60, 30, 9, Colors4);

            byte[] Colors5 = { 3, 9, 0, 6, 10, 4, 10, 6, 0, 9, 3 };
            StoredPatterns[5] = new GalleryPattern(1, 60, 30, 11, Colors5);

            byte[] Colors6 = { 12, 13, 15, 14, 2, 13, 14, 12, 15, 14, 13, 2, 8, 2, 13, 12 };
            StoredPatterns[6] = new GalleryPattern(9, 70, 40, 16, Colors6);

            byte[] Colors7 = { 3, 9, 0, 6, 0, 6, 0, 9, 3 };
            StoredPatterns[7] = new GalleryPattern(3, 60, 30, 9, Colors7);

            byte[] Colors8 = { 11, 8, 7, 1, 2, 5, 11, 5, 2, 1, 7, 8, 11, 4, 10 };
            StoredPatterns[8] = new GalleryPattern(3, 40, 40, 15, Colors8);

            byte[] Colors9 = { 5, 11, 2, 8, 10, 4, 6, 0, 9, 3, 1, 7 };
            StoredPatterns[9] = new GalleryPattern(2, 40, 20, 12, Colors9);

            byte[] Colors10 = { 10, 4, 8, 2, 11, 5, 7, 1, 3, 9, 0, 6, 0, 6, 0, 9, 3, 1, 7, 5, 11, 2, 8, 4, 10, 4, 10, 4 };
            StoredPatterns[10] = new GalleryPattern(7, 60, 30, 28, Colors10);

            byte[] Colors11 = { 5, 11, 2, 8, 10, 4, 6, 0, 9, 3, 1, 7 };
            StoredPatterns[11] = new GalleryPattern(1, 40, 20, 12, Colors11);

            byte[] Colors12 = { 10, 4, 8, 2, 11, 5, 7, 1, 3, 9, 0, 6, 0, 6, 0, 9, 3, 1, 7, 5, 11, 2, 8, 4, 10, 4, 10, 4 };
            StoredPatterns[12] = new GalleryPattern(1, 60, 30, 28, Colors12);

            byte[] Colors13 = { 4, 10, 6, 0, 6, 8, 2, 1, 7, 1 };
            StoredPatterns[13] = new GalleryPattern(6, 64, 32, 10, Colors13);

            byte[] Colors14 = { 1, 7, 2, 8, 4, 10, 4, 6, 0, 6, 4, 10, 4, 8, 2 };
            StoredPatterns[14] = new GalleryPattern(3, 60, 30, 15, Colors14);

            byte[] Colors15 = { 11, 8, 10, 4, 10, 8, 11, 0, 6, 0, 11, 8, 10, 4, 10, 8, 11, 5 };
            StoredPatterns[15] = new GalleryPattern(6, 30, 18, 18, Colors15);

            byte[] Colors16 = { 0, 6, 8, 2 };
            StoredPatterns[16] = new GalleryPattern(3, 46, 23, 4, Colors16);

            byte[] Colors17 = { 5, 11, 2, 8, 0, 6, 4, 10, 4, 6, 0, 8, 2, 11, 5, 11 };
            StoredPatterns[17] = new GalleryPattern(1, 38, 25, 16, Colors17);

            byte[] Colors18 = { 2, 1, 7, 1, 2, 8, 11, 8, 2, 1, 7, 1 };
            StoredPatterns[18] = new GalleryPattern(3, 46, 23, 13, Colors18);

            byte[] Colors19 = { 15, 14, 13, 12, 13, 12, 13, 14, 15, 14 };
            StoredPatterns[19] = new GalleryPattern(1, 46, 23, 10, Colors19);

            byte[] Colors20 = { 4, 10, 4, 6, 0, 2, 8, 7, 1, 11 };
            StoredPatterns[20] = new GalleryPattern(6, 72, 36, 10, Colors20);

            byte[] Colors21 = { 0, 1, 0, 1, 0, 1, 7, 6, 7, 6, 7, 6, 0, 1, 0, 1 };
            StoredPatterns[21] = new GalleryPattern(9, 70, 40, 16, Colors21);
        }*/
             // Set some basic gallery patterns so the gallery isnt empty

        static public void NavigateGallery()
        {
            //  Logic for exiting the gallery
            bool _exitFlag = false;

            //  Choice of all the patterns the user wants to see
            List<int> _choice;

            //  Amount of different patterns in the gallery
            int _stockAmount = StockGallery.Count;
            int _userAmount = UserGallery.Count;

            //  Amount of patterns present in the gallery
            int _gallerySize = _stockAmount + _userAmount;

            WriteGalleryInfo(_stockAmount, _userAmount);

            //  While the user doesnt want to exit, show him the gallery
            while (!_exitFlag)
            {
                //  Get the user pattern choice
                _choice = ParseGalleryChoice(_gallerySize, out _exitFlag);

                //  If the input is not (invalid or exit)
                if (_choice != null)
                {
                    //  Print every chosen pattern
                    for (int i = 0; i < _choice.Count; i++)
                    {
                        Write("\n\n");
                        if (_choice[i] < _stockAmount) StockGallery[_choice[i]].Generate();
                        else UserGallery[_choice[i] - _stockAmount].Generate();
                    }
                }
            }
        }
        // For navigating the stored patterns (gallery)


        static public void WriteGalleryInfo(int _stockGallerySize, int _userGallerySize)
        {
            //  Full gallery size
            int _gallerySize = _stockGallerySize + _userGallerySize;

            ResetUI(!gIgnoreFullScreen, true);
            Write("\n\t\t\t\t\t\tВыбрано: --- === Просмотр галереи === ---\n\n\n");

            ForegroundColor = ConsoleColor.DarkCyan;
            Write("\n\t\tДобро пожаловать в галерею!\n");
            ForegroundColor = ConsoleColor.White;



            Write("\n\t\tЗдесь хранятся ");
            ForegroundColor = ConsoleColor.Yellow;
            Write("стоковые и пользовательские сохранённые узоры, ");
            ForegroundColor = ConsoleColor.White;
            Write("а так же их параметры.");


            Write("\n\t\tНа данный момент узоров в галереи: ");
            if (_gallerySize > 100) ForegroundColor = ConsoleColor.Green;
            else ForegroundColor = ConsoleColor.Red;
            Write(_gallerySize + "\n");
            ForegroundColor = ConsoleColor.White;

            if (_gallerySize > 0)
            {
                Write("\n\t\tДля просмотра узоров введите:");
                Write("\n\t\t   > Одно число - для просмотра одного узора (под этим номером)");
                Write("\n\t\t   > Два числа через '-' - для просмотра узоров под номерами от первого до второго числа");
                Write("\n\t\t   > Несколько чисел через '/' - для просмотра нескольких узоров (только под этими номерами)\n");

                Write("\n\n\n\t\t[i]  - Доступный функционал на данный момент:");
            }
            else
            {
                Write("\n\n\t\t[!]  - На данный момент галерея пуста   :(\n");
                Write("\n\t\t       Не забывайте сохранять свои узоры - чтобы они появлялись здесь");
                Write("\n\t\t[i]  - Совет: проверьте правильность указанного пути к сохранённым узорам\n\n");
            }
            if (_stockGallerySize > 0)
            {
                
                Write("\n\t\t         > ");
                Write(1 + "-" + _stockGallerySize);

                //  Calculate margin for the gallery output
                string _margin = "         ";
                int _tempBuffer = _stockGallerySize;
                while(_tempBuffer > 0)
                {
                    if(_margin.Length > 0) _margin = _margin.Remove(0, 1);
                    _tempBuffer /= 10;
                }

                Write(" < " + _margin + " - Стоковые узоры");
            }
            if (_userGallerySize > 0)
            {
                Write("\n\t\t         > ");
                Write(_stockGallerySize + 1 + "-" + _gallerySize);

                //  Calculate margin for the gallery output
                string _margin = "          ";
                int _tempBuffer = _stockGallerySize;
                while (_tempBuffer > 0)
                {
                    if (_margin.Length > 0) _margin = _margin.Remove(0, 1);
                    _tempBuffer /= 10;
                }
                _tempBuffer = _stockGallerySize + _userGallerySize;
                while (_tempBuffer > 0)
                {
                    if (_margin.Length > 0) _margin = _margin.Remove(0, 1);
                    _tempBuffer /= 10;
                }

                Write(" < " + _margin + " - Пользовательские узоры");
            }
            Write("\n\t\t         > 0 <            - Выход из галереи\n");
        }
    }
}