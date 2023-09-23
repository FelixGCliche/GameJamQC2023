using Godot;


public partial class ColorData : Resource
{
    [Export]
    private string name;
    [Export]
    private Color color;

    public string Name => name;
    public Color Color => color;
    public string ColorCode => color.ToHtml(false);

    public ColorData()
    {
        name = "Black";
        color = new Color(0,0,0);
    }

    public ColorData(string name, Color color)
    {
        this.name = name;
        this.color = color;
    }
}
