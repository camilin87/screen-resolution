namespace RevertScreenResolution
{
    class Program
    {
        static void Main(string[] args)
        {
            var screenResolutionChanger = new ScreenResolutionChanger();
            var maxScreenResolution = screenResolutionChanger.GetMaximumSupportedScreenResolution();
            screenResolutionChanger.SetResolution(maxScreenResolution);
        }
    }
}
