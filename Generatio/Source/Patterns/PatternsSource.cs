using System;
using static System.Console;


using static Generatio.GlobalSettings;



namespace Generatio
{
    internal class PatternSource
    {
        public abstract class Pattern 
        {
            protected Byte type;
            private readonly UInt16 _sizeX, _sizeY;
            protected readonly ConsoleColor[] colors;
            private readonly bool _useMargin, _showInfo;
            private readonly string _patternName;

            protected Pattern(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
            {
                _sizeX = sizeX;
                _sizeY = sizeY;
                this.colors = colors;
                _patternName = patternName;
                _useMargin = useMargin;
                _showInfo = showInfo;
            }

            public abstract void Draw();
            protected void NormalizeSize(out UInt16 nX, out UInt16 nY)
            {
                //  Shrink the pattern size if it is too big
                if (gShrinkPatterns && _sizeX > WindowWidth / 2) nX = (UInt16)(WindowWidth / 2);
                else nX = _sizeX;
                if (gShrinkPatterns && _sizeY > WindowHeight) nY = (UInt16)WindowHeight;
                else nY = _sizeY;
            }
            protected string CalculateMargin(UInt16 X, Int32 windowX)
            {
                string margin = "";
                if (_useMargin) for (Int32 curX = 0; curX < windowX / 2 - X; curX++) margin += " ";
                return margin;
            }
            public void Save(UInt32 compactUnixDateTime)
            {
                Byte[] packedColors = Gallery.GalleryManager.PackColors(CustomFunctions.ConvertColorsToBytes(colors));
                GalleryEncodings.EncodeV3("", type, _sizeX, _sizeY, (UInt16)colors.Length, packedColors, compactUnixDateTime, _patternName);
                //  Temporary bullshit
            }
            public void Info()
            {
                string info = $"Узор #{type} - {PatternName}";
                string margin = CalculateMargin((UInt16)(info.Length >> 1), WindowWidth);
                Write($"\n{margin}{info}\n");
            }

            public Byte   Type => type;
            public UInt16 Width => _sizeX;
            public UInt16 Height => _sizeY;
            public ConsoleColor[] Colors => colors;
            public string PatternName => _patternName;
            public bool   Margin => _useMargin;
            public bool   ShowInfo => _showInfo;
        }

        public class PatternType1  : Pattern
        {
            public PatternType1(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 1; }

            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < (Y + 1) / 2; curY++)
                {
                    Write(margin);
                    for (Int32 curX = curY; curX < (X + 1) / 2 + curY; curX++)
                    {
                        curColor = curX % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = X / 2 + curY; curX > curY; curX--)
                    {
                        curColor = (curX - 1) % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
                for (Int32 curY = Y / 2; curY > 0; curY--)
                {
                    Write(margin);
                    for (Int32 curX = curY; curX < (X + 1) / 2 + curY; curX++)
                    {
                        curColor = (curX - 1) % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = X / 2 + curY; curX > curY; curX--)
                    {
                        curColor = (curX - 2) % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType2  : Pattern
        {
            public PatternType2(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 2; }

            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < Y; curY++)
                {
                    Write(margin);
                    for (Int32 curX = curY; curX < X + curY; curX++)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType3  : Pattern
        {
            public PatternType3(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 3; }

            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < Y / 2; curY++)
                {
                    Write(margin);
                    for (Int32 curX = 0; curX < curY && curX < X / 2; curX++)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = curY; curX < X - curY; curX++)
                    {
                        curColor = curY % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = Math.Max(X - curY, X / 2); curX < X; curX++)
                    {
                        curColor = (X - curX - 1) % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
                for (Int32 curY = Y / 2; curY < Y; curY++)
                {
                    Write(margin);
                    for (Int32 curX = 0; curX < Y - curY && curX < (X + 1) / 2; curX++)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = Y - curY; curX <= X + curY - Y && curX < X; curX++)
                    {
                        curColor = (Y - curY - 1) % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = Math.Max(X + curY - Y, (X + 1) / 2 - 1) + 1; curX < X; curX++)
                    {
                        curColor = (X - curX - 1) % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType4  : Pattern
        {
            public PatternType4(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 4; }

            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (UInt16 curY = 0; curY < Y; curY++)
                {
                    Write(margin);
                    for (UInt16 curX = 0; curX < X; curX++)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - 2 - curColor % colAmount;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType5  : Pattern
        {
            public PatternType5(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 5; }

            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < Y; curY++)
                {
                    Write(margin);
                    for (Int32 curX = 0; curX < X; curX++)
                    {
                        curColor = curY % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - 2 - curColor % colAmount;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType6  : Pattern
        {
            public PatternType6(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 6; }

            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < (Y + 1) / 2; curY++)
                {
                    Write(margin);
                    for (Int32 curX = (X + 1) / 2 + curY; curX > curY; curX--)
                    {
                        curColor = (curX - 1) % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = curY + X % 2; curX < (X + 1) / 2 + curY; curX++)
                    {
                        curColor = curX % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
                for (Int32 curY = Y / 2; curY > 0; curY--)
                {
                    Write(margin);
                    for (Int32 curX = (X + 1) / 2 + curY; curX > curY; curX--)
                    {
                        curColor = (curX - 2) % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = curY + X % 2; curX < (X + 1) / 2 + curY; curX++)
                    {
                        curColor = (curX - 1) % (colAmount * 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType7  : Pattern
        {
            public PatternType7(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 7; }
            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < Y / 2; curY++)
                {
                    Write(margin);
                    for (Int32 j = 0; j < curY && j < X / 2; j++)
                    {
                        curColor = colAmount - (j % (colAmount * 2 - 2)) - 1;
                        curColor = Math.Abs(curColor);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 j = curY; j < X - curY; j++)
                    {
                        curColor = curY % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 j = Math.Max(X - curY, X / 2); j < X; j++)
                    {
                        curColor = colAmount - ((X - j - 1) % (colAmount * 2 - 2)) - 1;
                        curColor = Math.Abs(curColor);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
                for (Int32 curY = Y / 2; curY < Y; curY++)
                {
                    Write(margin);
                    for (Int32 curX = 0; curX < Y - curY - 1 && curX < (X + 1) / 2; curX++)
                    {
                        curColor = colAmount - (curX % (colAmount * 2 - 2)) - 1;
                        curColor = Math.Abs(curColor);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = Y - curY - 1; curX <= X + curY - Y && curX < X; curX++)
                    {
                        curColor = (Y - curY - 1) % (colAmount * 2 - 2);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    for (Int32 curX = Math.Max(X + curY - Y, (X - 1) / 2) + 1; curX < X; curX++)
                    {
                        curColor = colAmount - ((X - curX - 1) % (colAmount * 2 - 2)) - 1;
                        curColor = Math.Abs(curColor);
                        if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType8  : Pattern
        {
            public PatternType8(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 8; }
            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < (Y + 1) / 2; curY++)
                {
                    Write(margin);
                    for (Int32 curX = curY; curX < X + curY; curX++)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
                for (Int32 curY = Y / 2; curY > 0; curY--)
                {
                    Write(margin);
                    for (Int32 curX = 2 * colAmount - 1 - curY; curX > 2 * colAmount - 1 - curY - X; curX--)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor < 0) curColor = Math.Abs(curColor) % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType9  : Pattern
        {
            public PatternType9(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 9; }
            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < Y; curY++)
                {
                    Write(margin);
                    for (Int32 curX = 2 * curY; curX > 2 * curY - X; curX--)
                    {
                        curColor = curX % (colAmount * 2 - 2);
                        if (curColor < 0) curColor = Math.Abs(curColor) % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            }
        }
        public class PatternType10 : Pattern
        {
            public PatternType10(UInt16 sizeX, UInt16 sizeY,
                ConsoleColor[] colors, string patternName,
                bool useMargin, bool showInfo)
                : base(sizeX, sizeY, colors, patternName, useMargin, showInfo)
            { type = 10; }
            public override void Draw()
            {
                Int32 curColor, colAmount = colors.Length;
                NormalizeSize(out UInt16 X, out UInt16 Y);
                string margin = CalculateMargin(X, WindowWidth);

                for (Int32 curY = 0; curY < Y; curY++)
                {
                    Write(margin);
                    for (Int32 curX = 2 * curY; curX > 2 * curY - X; curX--)
                    {
                        curColor = (curX - 1 - curY * 5) % (colAmount * 2 - 2);
                        if (curColor < 0) curColor = Math.Abs(curColor) % (colAmount * 2 - 2);
                        if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                        BackgroundColor = colors[curColor];
                        Write("  ");
                    }
                    BackgroundColor = ConsoleColor.Black;
                    Write("\n");
                }
            } 
        }
    }
}