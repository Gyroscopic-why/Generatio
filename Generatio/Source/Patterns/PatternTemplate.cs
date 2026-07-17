using static System.Console;



namespace Generatio.Source.Patterns
{
    public abstract class IPattern(Byte type, UInt16 sizeX, UInt16 sizeY,
        ConsoleColor[] colors, string patternName,
        bool useMargin, bool showInfo)
    {
        public Byte Type { get; private set; } = type;
        public ConsoleColor[] Colors { get; private set; } = colors;

        public UInt16 SizeX { get; private set; } = sizeX;
        public UInt16 SizeY { get; private set; } = sizeY;


        public string PatternName = patternName;


        public bool UseMargin { get; private set; } = useMargin;
        public bool ShowInfo  { get; private set; } = showInfo;

        

        public abstract void DrawFull();
        public abstract ConsoleColor[] DrawLine(UInt16 targetY);
        public abstract ConsoleColor[] DrawLine(UInt16 targetY, UInt16 startX, UInt16 length, UInt16 nX, UInt16 nY);

        static public void DrawPreloadStatic(ConsoleColor[] preload, Int32 count = -1, bool resetLine = true)
        {
            if (count < 0) count = preload.Length;
            for (var i = 0; i < count; i++)
            {
                BackgroundColor = preload[i % preload.Length];
                Write("  ");
            }
            if (resetLine)
            {
                BackgroundColor = ConsoleColor.Black;
                Write('\n');
            }
        }

        #pragma warning disable CA1822
        public void DrawPreload(ConsoleColor[] preload, Int32 count = -1, bool resetLine = true)
            => DrawPreloadStatic(preload, count, resetLine);
        #pragma warning restore CA1822



        // protected
        public (UInt16 nX, UInt16 nY) NormalizeSize()
        {
            UInt16 nwWidth  = (UInt16)(WindowWidth  >> 1);
            UInt16 nwHeight = (UInt16)(WindowHeight >> 1);

            return
            (
                Settings.CurSettings.shrinkOversized && SizeX > nwWidth  ? nwWidth  : SizeX,
                Settings.CurSettings.shrinkOversized && SizeY > nwHeight ? nwHeight : SizeY
            );
        }


        static public string CalculateMargin(UInt16 sizeX, Int32 offsetX, Int32 windowX)
        {
            Int32  length  = windowX / 2 - sizeX - offsetX;
            return length <= 0 ? "" : new string(' ', length);
        }



        public void Save(UInt32 compactUnixDateTime)
        {
            Byte[] packedColors = Gallery.GalleryManager.PackColors(CustomFunctions.ConvertColorsToBytes(Colors));

            GalleryEncodings.EncodeV3("", Type, SizeX, SizeY, (UInt16)Colors.Length, packedColors, compactUnixDateTime, PatternName);
            //  Temporary bullshit
        }

        public void Info()
        {
            string info = $"Узор #{Type} - {PatternName}";
            string margin = CalculateMargin((UInt16)(info.Length >> 1), 0, WindowWidth);
            Write($"\n{margin}{info}\n");
        }
    }
}