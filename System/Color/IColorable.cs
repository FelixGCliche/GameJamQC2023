namespace GameJamQC2023.Color
{
    public interface IColorable
    {
        public ColorData ColorData { get; }
    
        public void ReceiveColorData(IColorable sender);
        public void BlendColorData(IColorable sender);
        public void DropColorData();
    }
}
