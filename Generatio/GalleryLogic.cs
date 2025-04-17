using System;
using System.Linq;
using System.Collections.Generic;
using static System.Console;


using static Generatio.PatternSource;
using static Generatio.GlobalSettings;
using static Generatio.CustomFunctions;
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
            private readonly ushort width;
            private readonly ushort height;

            //  Amount of colors
            static private ushort colAmount;

            //  Array of colors (in bytes))
            private readonly byte[] colors = new byte[colAmount];

            //  Name of the current pattern (origin/date/time/author/etc)
            private readonly string name;

            //-----------------------  Functions  ----------------------------------//



            public GalleryPattern(byte iPatternType,
                ushort iWidth, ushort iHeight,
                ushort iColAmount, byte[] iColors,
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
                switch (patternType)
                {
                    case 1:
                        Pattern1(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 2:
                        Pattern2(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 3:
                        Pattern3(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 4:
                        Pattern4(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 5:
                        Pattern5(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 6:
                        Pattern6(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 7:
                        Pattern7(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 8:
                        Pattern8(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    case 9:
                        Pattern9(width, height, ConvertColorsToConsole(colors), true);
                        break;
                    default:
                        break;
                }
            }
                 //  Universal pattern generation by code

            public byte GetPatternType() { return patternType; }
                 //  Returns the pattern type

            public ushort GetWidth() { return width; }
                 //  Returns the pattern width

            public ushort GetHeight() { return height; }
                 //  Returns the pattern height

            public ushort GetColAmount() { return colAmount; }
                 //  Returns the amount of colors in the pattern 

            public byte[] GetColorsBytes() { return colors; }
                 //  Returns the colors in bytes[]

            public ConsoleColor[] GetColorsConsole() { return ConvertColorsToConsole(colors); }
                 //  Returns the colors in ConsoleColor[]

        }


        static public List<GalleryPattern> GalleryStock  = new List<GalleryPattern>();
        static public List<GalleryPattern> GalleryUser   = new List<GalleryPattern>();
        static public List<GalleryPattern> GalleryBuffer = new List<GalleryPattern>();

        
        static public List<GalleryPattern> ConvertToGalleryPattern(List<string> _data, bool _showInfo = false)
        {
            if (_data != null && _data.Count > 2)
            {
                //  Storing here the successfully converted gallery patterns
                List<GalleryPattern> _converted = new List<GalleryPattern>();

                string[] _parserHelper;
                List<byte> _colors = new List<byte>();

                string _name = "Unnamed";
                byte _patternType = 0;
                ushort _width = 0;
                ushort _height = 0;
                ushort _colAmount = 0;

                for (int i = 0; i < _data.Count; i++)
                {
                    //  If the line is expected to not have pattern data
                    //  But the name - we parse it
                    if (_data[i].IndexOf(",") == -1)
                    {
                        _name = _data[i].Trim();

                        if (_showInfo)
                        {
                            //  Show the name of the pattern
                            Write("\n\t\tУспешно распознанно название узора: " + _name + "\n");
                        }
                    }

                    //  If the line is expected to have pattern data
                    else
                    {
                        //  Try get the special character indicating base pattern data
                        _parserHelper = SplitForParsing(_data[i], ",");

                        //  If found the special char - try parse base pattern data
                        if (_parserHelper[0][0] == '!')
                        {

                            //  Try parse the pattern Type
                            if (byte.TryParse(_parserHelper[0].Replace("!", "").Trim(), out _patternType))
                            {

                                //  Try parse the pattern size
                                if (ushort.TryParse(_parserHelper[1].Trim(), out _width)
                                    && ushort.TryParse(_parserHelper[2].Trim(), out _height))
                                {

                                    //  Try parse the amount of colors
                                    if (ushort.TryParse(_parserHelper[3].Trim(), out _colAmount))
                                    {
                                        if (_showInfo)
                                        {
                                            //  Show the amount of colors
                                            Write("\n\t\tУспешно распознанны базовые параметры!");
                                            Write("\n\t\tТип узора: " + _patternType);
                                            Write("\n\t\tРазмеры узора: " + _width + "x" + _height);
                                            Write("\n\t\tКоличество цветов: " + _colAmount + "\n");
                                        }
                                    }
                                    else if (_showInfo)
                                        //  Try parse the last parameter (amount of colors)
                                        //  If failes - output error
                                        Write("\n\t\tНе удалось распознать количество цветов: " + _parserHelper[3] + "\n");
                                }
                                //  Show error parsing the size if needed
                                else if (_showInfo) Write("\n\t\tНе удалось распознать размеры узора: " + _parserHelper[1] + " " + _parserHelper[2] + "\n");
                            }
                            //  Show error parsing the type if needed
                            else if (_showInfo) Write("\n\t\tНе удалось распознать тип узора: " + _parserHelper[0] + "\n");


                            /*  
                             *  Trying to parse the base parameters of a pattern:
                             *  Type, Width, Height, Amount of colors
                             *  
                             *  Saving them (if succeded)
                             *  Else outputing an error
                             *  And moving to the next line
                             *  (Where we expect the colors to be)
                             */
                        }


                        /*
                         *  If the line doesnt indicate of the storing of:
                         *  Pattern name, or base parameters
                         *  =>  Then we look for colors in it
                        */

                        else
                        {
                            for (int j = 0; j < _parserHelper.Length; j++)
                            {
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
                        }
                    }

                    //  After data parsing and conversion
                    //  We check if we have enough data to check for its validation
                    if (_patternType > 0 && _width > 0 && _height > 0 && _colAmount > 0 && _colors.Count > 0)
                    {
                        //  If the real parsed colors amount is equal to the expected one
                        if (_colors.Count == _colAmount)
                        {
                            //  Add the successfully converted pattern to the list
                            _converted.Add(new GalleryPattern(_patternType, _width, _height, _colAmount, _colors.ToArray(), _name));

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
                            Write("\n\t\tУказано: " + _colAmount + ", обнаружено: " + _colors.Count + "\n");
                        }

                        //  Reset the parameters to avoid data mixing
                        _name = "Unnamed";
                        _patternType = 0;
                        _width = 0;
                        _height = 0;
                        _colAmount = 0;
                        _colors.Clear();

                        if (_showInfo) Write("\n\t\tПараметры были сброшены\n");
                    }

                }


                //  Show the amount of successfully converted patterns
                if (_showInfo) Write("\n\t\tУспешно обнаружено: " + _converted.Count + " узоров" + "\n");

                //  Return the successfully converted patterns
                return _converted;
            }

            //  return null if the data is invalid
            if (_showInfo) Write("\n\t\tНе удалось распознать сохранённые узоры: Некорректный формат данных\n");
            return null;
        }


        static public void UpdateStockGallery()
        {
            //  Read the stock data for the gallery
            List<string> _parsedData = ReadData(gGalleryPath, "Stock_pack1.db", true);

            //  Parse the data into more easily usable
            _parsedData = ParseData(_parsedData, true, true, "*", "", "*", true);

            //  Convert the data into patterns
            GalleryBuffer = ConvertToGalleryPattern(_parsedData, true);

            //  Update the gallery
            if (GalleryBuffer.Count > GalleryStock.Count)
            {
                //Gallery.Clear();
                GalleryStock = GalleryBuffer;
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
            UpdateStockGallery();

            //  Amount of patterns present in the gallery
            int gallerySize = GalleryStock.Count();

            //if (!gGeneratedGpatterns) GenerateGallery();
            string _userInput = "";

            //  Temporary buffer for the help of parsing the user input
            int[] _helperId = new int[2];

            //  List of Gallrey ids that the user wants to see
            List<int> _parsedId = new List<int>();

            for (int hide = 0; hide < 1; hide++)
            {
                Write("\n\n");

                ForegroundColor = ConsoleColor.DarkCyan;
                Write("\n\t\t\t\t\tДобро пожаловать в галерею!\n");
                ForegroundColor = ConsoleColor.White;
                Write("\n\t\tЗдесь хранятся стоковые и ваши сохранённые узоры, а так же их параметры.");
                Write("\n\t\tНа данный момент узоров: 20 (dev build version)\n");


                Write("\n\t\tДля просмотра узоров введите:");
                Write("\n\t\t  Одно число - для просмотра одного узора (под этим номером)");
                Write("\n\t\t  Два числа через '-' - для просмотра узоров под номерами от первого до второго числа");
                Write("\n\t\t  Несколько чисел через '/' - для просмотра нескольких узоров (только под этими номерами)\n\n");


                ForegroundColor = ConsoleColor.DarkGray;
                Write("\n\t\tПримеры ввода:");
                Write("\n\t\t  '12'       - будет показан только один узор (под этим номером в базе данных)");
                Write("\n\t\t  '3-8'      - будут показаны узоры под номерами 3, 4, 5, 6, 7 и 8 (от первого числа до второго)");
                Write("\n\t\t  '15/2/7/1' - будут показаны узоры под номерами 15, 2, 7 и 1 в таком же порядке как они были введены\n\n");
                ForegroundColor = ConsoleColor.White;


                Write("\n\t\tПри вводе числа, будет выведен узор под этим номером, а также его параметры.");
                Write("\n\t\tОни включают в себя тип, размеры узоров, количество цветов и сами цвета.\n");

                Write("\n\t\tПараметры будут выглядеть как набор чисел с разделениями:");
                ForegroundColor = ConsoleColor.Green;
                Write("\n\t\t   Тип узора/Ширина/Высота/Количество цветов/Цвет1-Цвет2-Цвет3...\n");
                ForegroundColor = ConsoleColor.White;

                Write("\n\n\n\t\t\t\tДоступный функционал:");
                Write("\n\t\t\t[" + Math.Min(1, gallerySize) + "-" + gallerySize + "]\t- Посмотреть узор/узоры");
                Write("\n\t\t\t[0]\t- Выход из галереи\n");
            }

            while (_userInput != "0")
            {
                Write("\n\t\tВаш выбор: ");
                _userInput = ReadLine().Trim();

                if (int.TryParse(_userInput, out _helperId[0]))
                {
                    if (_helperId[0] > 0 && _helperId[0] < gallerySize + 1)
                    {
                        _parsedId.Add(_helperId[0] - 1);
                        Write("\n\t\tРаспознанный узор: " + _helperId[0] + "\n\n");
                    }
                    else if (_helperId[0] != 0) Write("\n\t\tИндекс вне границ базы данных. Повторите ввод");
                }
                else
                {
                    string[] _multiId = SplitForParsing(_userInput, "-/, ");
                    string _splitters = GetSplitters(_userInput, "-/, ");

                    if (_multiId.Length > 1)
                    {
                        for (int i = 0; i < _multiId.Length - 1; i += 2)
                        {
                            //Write(_splitters);
                            if (_multiId[i] != null && _multiId[i + 1] != null)
                            {
                                if (int.TryParse(_multiId[i].Trim(), out _helperId[0])
                                    && int.TryParse(_multiId[i + 1].Trim(), out _helperId[1]))
                                {

                                    if (_splitters[i / 2] == '-')
                                    {

                                        //-----------  Limit the parsed intervals  -----------//
                                        //                                                    //
                                        _helperId[0] = Math.Min(_helperId[0], gallerySize);   //
                                        _helperId[0] = Math.Max(_helperId[0], 1);             //
                                                                                              //
                                        _helperId[1] = Math.Min(_helperId[1], gallerySize);   //
                                        _helperId[1] = Math.Max(_helperId[1], 1);             //
                                        //                                                    //
                                        //-----------  Limit the parsed intervals  -----------//


                                        // Print success message
                                        Write("\n\t\tРаспознан интервал с " + _helperId[0] + " по " + _helperId[1]);


                                        // Check for a normal interval (from lower to bigger)
                                        for (int j = 0; j <= _helperId[1] - _helperId[0]; j++)
                                        {
                                            // Add normal interval
                                            _parsedId.Add(_helperId[0] + j - 1);
                                        }


                                        // Check for an inverted interval (from bigger to lower)
                                        for (int j = 0; j <= _helperId[0] - _helperId[1]; j++)
                                        {
                                            // Add inverted interval
                                            _parsedId.Add(_helperId[0] - j - 1);
                                        }
                                    }
                                    else if (_splitters[i / 2] == '/' || _splitters[i / 2] == ',' || _splitters[i / 2] == ' ')
                                    {
                                        if (int.TryParse(_multiId[i], out _helperId[0]))
                                        {
                                            if (_helperId[0] > 0 && _helperId[0] < gallerySize + 1)
                                            {
                                                _parsedId.Add(_helperId[0] - 1);
                                                Write("\n\t\tРаспознанный узор: " + _helperId[0] + "\n\n");
                                            }
                                        }
                                        if (int.TryParse(_multiId[i + 1], out _helperId[1]))
                                        {
                                            if (_helperId[1] > 0 && _helperId[1] < gallerySize + 1)
                                            {
                                                _parsedId.Add(_helperId[1] - 1);
                                                Write("\n\t\tРаспознанный узор: " + _helperId[1] + "\n\n");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else Write("\n\t\tНе удалось распознать значения диапозона. Повторите ввод");
                }
                for (int i = 0; i < _parsedId.Count; i++)
                {
                    Write("\n\n");
                    GalleryStock[_parsedId[i]].Generate();
                }
                _parsedId.Clear();
            }

        }
             // For navigating the stored patterns (gallery)
    }
}