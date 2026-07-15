namespace Generatio.Gallery
{
    internal class UserInfo(string deviceName = "Unknown", string userName = "Unknown")
    {
        public string DeviceName { get; set; } = deviceName;
        public string Username   { get; set; } = userName;
    }
}