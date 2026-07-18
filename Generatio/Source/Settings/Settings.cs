using GyroscopicDataLibrary;



namespace Generatio
{
    internal static partial class Settings
    {
        //  Default: \Users\thisUser\Documents\Gyroscopic\Generatio\Gallery
        static public string pathToGallery  = BetterDataIO.GetPath(false, true, "\\Gyroscopic\\Generatio\\Gallery", false);

        //  Default: \Users\thisUser\Documents\Gyroscopic\Generatio\Settings
        static public string pathToSettings = BetterDataIO.GetPath(false, true, "\\Gyroscopic\\Generatio\\Settings", false);


        static public bool ignoreFullScreen;
        static public bool ignoreSizeLimit;

        static public bool shrinkOversized;

        static public bool showBasicInfo;
        static public bool showDevInfo;

        static public bool alwaysDrawAll;

        static public bool autoSaveAll;
        static public bool autoNameBlankPatterns;
    }
}