using System;


using AVcontrol;



namespace Generatio.Gallery
{
    public class GalleryPattern(Byte patternType,
        UInt16 width, UInt16 height,
        UInt16 colAmount, Byte[] colors,
        DateTime4b datetime,
        string patternName)
    {
        private readonly PatternSource.Pattern pattern = patternType switch
        {
            1 => new PatternSource.PatternType1 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            2 => new PatternSource.PatternType2 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            3 => new PatternSource.PatternType3 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            4 => new PatternSource.PatternType4 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            5 => new PatternSource.PatternType5 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            6 => new PatternSource.PatternType6 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            7 => new PatternSource.PatternType7 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            8 => new PatternSource.PatternType8 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            9 => new PatternSource.PatternType9 (width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
            _ => new PatternSource.PatternType10(width, height, CustomFunctions.ConvertColorsToConsole(colors), patternName, true, true),
        };
        private readonly Byte _patternType = patternType;
        private readonly UInt16 _width = width, _height = height;

        private readonly UInt16 _colAmount = colAmount;
        private readonly Byte[] _colors = colors;

        private readonly DateTime4b _datetime = datetime;  //  Date and time of the pattern creation
        private readonly string _patternName = patternName;

        public void Draw()
        {
            if (pattern.ShowInfo) pattern.Info();
            pattern.Draw();
        }



        public Byte[] PackedColorBytes => PackColors();
        private Byte[] PackColors()
        {
            Int32 packedLength = (_colors.Length + 1) >> 1;
            Byte[] packed = new Byte[packedLength];

            for (Int32 i = 0; i < _colors.Length; i++)
            {
                Int32 packedIndex = i >> 1;
                Byte color = (Byte)(_colors[i] & 0x0F); //  Gives 0-15 as a result

                if (i % 2 == 0) packed[packedIndex] = (Byte)(color << 4);  // Upper 4 bits
                else packed[packedIndex] |= color;      //  Lower 4 bits
            }

            return packed;
        }
        private static Byte[] UnpackColors(Byte[] packed, UInt16 unpackedLength)
        {
            Byte[] colors = new Byte[unpackedLength];

            for (int i = 0; i < packed.Length * 2 && i < unpackedLength; i += 2)
            {
                colors[i] = (Byte)((packed[i] >> 4) & 0x0F);  //  Upper 4 bits
                if (i + 1 < unpackedLength) colors[i + 1] = (Byte)(packed[i] & 0x0F);  //  Lower 4 bits
            }

            return colors;
        }




        public Byte PatternType => _patternType;
        public UInt16 Width => _width;
        public UInt16 Height => _height;



        public UInt16 ColAmount => _colAmount;
        public Byte[] ColorsBytes => _colors;
        public ConsoleColor[] ColorsConsole => CustomFunctions.ConvertColorsToConsole(_colors);



        public string StandardTimeString => _datetime.ToStringFull();
        public DateTime4b Datetime => _datetime;
        public UInt32 CompactUnixDatetime => _datetime.PassedTotalMinutes;



        public string PatternName => _patternName;
    }
}