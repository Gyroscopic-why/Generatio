using System;
using static System.Console;


using static Generatio.CustomProcedures;
using static Generatio.GlobalSettings;



namespace Generatio
{
    internal class PatternSource
    {
        static public void Pattern1(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight)    Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(1, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 1\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < (Y + 1) / 2; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = i; j < (X + 1) / 2 + i; j++)
                {
                    curColor = j % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = X / 2 + i; j > i; j--)
                {
                    curColor = (j - 1) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i > 0; i--)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = i; j < (X + 1) / 2 + i; j++)
                {
                    curColor = (j - 1) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = X / 2 + i; j > i; j--)
                {
                    curColor = (j - 2) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern2(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(2, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 2\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = i; j < X + i; j++)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
        }

        static public void Pattern3(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo) 
                EncodePattern(3, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 3\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y / 2; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 0; j < i && j < X / 2; j++)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = i; j < X - i; j++)
                {
                    curColor = i % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X - i, X / 2); j < X; j++)
                {
                    curColor = (X - j - 1) % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 0; j < Y - i && j < (X + 1) / 2; j++)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = Y - i; j <= X + i - Y && j < X; j++)
                {
                    curColor = (Y - i - 1) % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X + i - Y, (X + 1) / 2 - 1) + 1; j < X; j++)
                {
                    curColor = (X - j - 1) % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern4(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(4, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 4\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 0; j < X; j++)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - 2 - curColor % colAmount;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern5(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(5, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 5\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 0; j < X; j++)
                {
                    curColor = i % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - 2 - curColor % colAmount;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern6(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(6, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 6\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < (Y + 1) / 2; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = (X + 1) / 2 + i; j > i; j--)
                {
                    curColor = (j - 1) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = i + X % 2; j < (X + 1) / 2 + i; j++)
                {
                    curColor = j % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i > 0; i--)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = (X + 1) / 2 + i; j > i; j--)
                {
                    curColor = (j - 2) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = i + X % 2; j < (X + 1) / 2 + i; j++)
                {
                    curColor = (j - 1) % (colAmount * 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern7(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(7, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 7\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y / 2; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 0; j < i && j < X / 2; j++)
                {
                    curColor = colAmount - (j % (colAmount * 2 - 2)) - 1;
                    curColor = Math.Abs(curColor);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = i; j < X - i; j++)
                {
                    curColor = i % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X - i, X / 2); j < X; j++)
                {
                    curColor = colAmount - ((X - j - 1) % (colAmount * 2 - 2)) - 1;
                    curColor = Math.Abs(curColor);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 0; j < Y - i - 1 && j < (X + 1) / 2; j++)
                {
                    curColor = colAmount - (j % (colAmount * 2 - 2)) - 1;
                    curColor = Math.Abs(curColor);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = Y - i - 1; j <= X + i - Y && j < X; j++)
                {
                    curColor = (Y - i - 1) % (colAmount * 2 - 2);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                for (int j = Math.Max(X + i - Y, (X - 1) / 2) + 1; j < X; j++)
                {
                    curColor = colAmount - ((X - j - 1) % (colAmount * 2 - 2)) - 1;
                    curColor = Math.Abs(curColor);
                    if (curColor > colAmount - 1) curColor = colAmount - curColor % colAmount - 1;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern8(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(8, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 8\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < (Y + 1) / 2; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = i; j < X + i; j++)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            for (int i = Y / 2; i > 0; i--)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 2 * colAmount - 1 - i; j > 2 * colAmount - 1 - i - X; j--)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor < 0) curColor = Math.Abs(curColor) % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern9(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(9, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 9\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 2 * i; j > 2 * i - X; j--)
                {
                    curColor = j % (colAmount * 2 - 2);
                    if (curColor < 0) curColor = Math.Abs(curColor) % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }

        static public void Pattern10(int X, int Y, ConsoleColor[] colors, bool useMargin, string name = "", bool savePattern = false, bool showInfo = false)
        {
            int curColor, colAmount = colors.Length, edgeMargin = WindowWidth / 2 - X;

            //  Shrink the pattern if it is too big
            //  (optional)
            if (gShrinkPatterns && X > WindowWidth / 2) X = WindowWidth / 2;
            if (gShrinkPatterns && Y > WindowHeight) Y = WindowHeight;


            if (savePattern || showInfo)
                EncodePattern(10, X, Y, colAmount, colors, name, savePattern, "", gGalleryPath, showInfo);

            for (int j = 0; j < (WindowWidth - name.Length) / 2 - 2; j++) Write(" ");
            if (name == "") Write("No 10\n\n");
            else Write("  " + name + "\n\n");


            for (int i = 0; i < Y; i++)
            {
                if (useMargin) for (int j = 0; j < edgeMargin; j++) Write(" ");
                for (int j = 2 * i; j > 2 * i - X; j--)
                {
                    curColor = (j - 1 - i * 5) % (colAmount * 2 - 2);
                    if (curColor < 0) curColor = Math.Abs(curColor) % (colAmount * 2 - 2);
                    if (curColor >= colAmount) curColor = colAmount - curColor % colAmount - 2;
                    BackgroundColor = colors[curColor];
                    Write("  ");
                }
                BackgroundColor = ConsoleColor.Black;
                WriteLine();
            }
            BackgroundColor = ConsoleColor.Black;
        }
    }
}