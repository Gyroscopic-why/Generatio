using System;
using static System.Console;


using static Generatio.CustomProcedures;
using static Generatio.GlobalSettings;



namespace Generatio
{
    internal class PatternSource
    {
        static public void Pattern1(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight)    Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(1, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 1\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < (Y + 1) / 2; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = i; j < (X + 1) / 2 + i; j++)
                {
                    _curColor = j % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = X / 2 + i; j > i; j--)
                {
                    _curColor = (j - 1) % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i > 0; i--)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = i; j < (X + 1) / 2 + i; j++)
                {
                    _curColor = (j - 1) % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = X / 2 + i; j > i; j--)
                {
                    _curColor = (j - 2) % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern2(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(2, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 2\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = i; j < X + i; j++)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
        }

        static public void Pattern3(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo) 
                EncodePattern(3, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 3\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y / 2; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 0; j < i && j < X / 2; j++)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = i; j < X - i; j++)
                {
                    _curColor = i % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X - i, X / 2); j < X; j++)
                {
                    _curColor = (X - j - 1) % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 0; j < Y - i && j < (X + 1) / 2; j++)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = Y - i; j <= X + i - Y && j < X; j++)
                {
                    _curColor = (Y - i - 1) % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X + i - Y, (X + 1) / 2 - 1) + 1; j < X; j++)
                {
                    _curColor = (X - j - 1) % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern4(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(4, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 4\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 0; j < X; j++)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - 2 - _curColor % _colAmount;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern5(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(5, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 5\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 0; j < X; j++)
                {
                    _curColor = i % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - 2 - _curColor % _colAmount;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern6(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(6, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 6\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < (Y + 1) / 2; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = (X + 1) / 2 + i; j > i; j--)
                {
                    _curColor = (j - 1) % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = i + X % 2; j < (X + 1) / 2 + i; j++)
                {
                    _curColor = j % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i > 0; i--)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = (X + 1) / 2 + i; j > i; j--)
                {
                    _curColor = (j - 2) % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = i + X % 2; j < (X + 1) / 2 + i; j++)
                {
                    _curColor = (j - 1) % (_colAmount * 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern7(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(7, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 7\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y / 2; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 0; j < i && j < X / 2; j++)
                {
                    _curColor = _colAmount - (j % (_colAmount * 2 - 2)) - 1;
                    _curColor = Math.Abs(_curColor);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = i; j < X - i; j++)
                {
                    _curColor = i % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X - i, X / 2); j < X; j++)
                {
                    _curColor = _colAmount - ((X - j - 1) % (_colAmount * 2 - 2)) - 1;
                    _curColor = Math.Abs(_curColor);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 0; j < Y - i - 1 && j < (X + 1) / 2; j++)
                {
                    _curColor = _colAmount - (j % (_colAmount * 2 - 2)) - 1;
                    _curColor = Math.Abs(_curColor);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = Y - i - 1; j <= X + i - Y && j < X; j++)
                {
                    _curColor = (Y - i - 1) % (_colAmount * 2 - 2);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X + i - Y, (X - 1) / 2) + 1; j < X; j++)
                {
                    _curColor = _colAmount - ((X - j - 1) % (_colAmount * 2 - 2)) - 1;
                    _curColor = Math.Abs(_curColor);
                    if (_curColor > _colAmount - 1) _curColor = _colAmount - _curColor % _colAmount - 1;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern8(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(8, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 8\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < (Y + 1) / 2; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = i; j < X + i; j++)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i > 0; i--)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 2 * _colAmount - 1 - i; j > 2 * _colAmount - 1 - i - X; j--)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor < 0) _curColor = Math.Abs(_curColor) % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern9(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(9, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 9\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 2 * i; j > 2 * i - X; j--)
                {
                    _curColor = j % (_colAmount * 2 - 2);
                    if (_curColor < 0) _curColor = Math.Abs(_curColor) % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern10(int X, int Y, ConsoleColor[] _colors, bool _useMargin, string _name = "", bool _savePattern = false, bool _showInfo = false)
        {
            int _curColor, _colAmount = _colors.Length, _edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (_savePattern || _showInfo)
                EncodePattern(10, X, Y, _colAmount, _colors, _name, _savePattern, "", gGalleryPath, _showInfo);

            for (int j = 0; j < (WindowWidth - _name.Length) / 2 - 2; j++) Write(" ");
            if (_name == "") Write("No 10\n\n");
            else Write("  " + _name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (_useMargin) for (int j = 0; j < _edgeMargin; j++) Write(" ");
                for (int j = 2 * i; j > 2 * i - X; j--)
                {
                    _curColor = (j - 1 - i * 5) % (_colAmount * 2 - 2);
                    if (_curColor < 0) _curColor = Math.Abs(_curColor) % (_colAmount * 2 - 2);
                    if (_curColor >= _colAmount) _curColor = _colAmount - _curColor % _colAmount - 2;
                    BackgroundColor = _colors[_curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }
    }
}