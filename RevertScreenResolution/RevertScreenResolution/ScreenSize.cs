namespace RevertScreenResolution
{
    public class ScreenSize
    {
        public uint Width { get; set; }
        public uint Height { get; set; }

        public uint Area { get { return Width*Height; } }
    }
}