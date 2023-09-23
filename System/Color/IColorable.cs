public interface IColorable
{
    public ColorData ColorData { get; }
    public void SendColorData();
    public void ReceiveColorData(ColorData colorData);
    public void BlendColorData();
    public void DropColorData();
}
