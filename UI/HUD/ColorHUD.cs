using Godot;

public partial class ColorHUD : Control
{
	private TextureRect[] colorTextureRects;

	public override void _Ready()
	{
		colorTextureRects = new[]
		{
			GetNode<TextureRect>("MarginContainer/HBoxContainer/ColorTextureRect1"),
			GetNode<TextureRect>("MarginContainer/HBoxContainer/ColorTextureRect2")
		};

		foreach (var textureRect in colorTextureRects)
			textureRect.SelfModulate = Colors.Black;
	}


	private void OnPlayerHeldColorsUpdated(Color[] colors)
	{
		for (var i = 0; i < colors.Length; i++)
			colorTextureRects[i].SelfModulate = colors[i];
	}
}
