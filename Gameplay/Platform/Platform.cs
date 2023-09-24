using GameJamQC2023.Color;
using Godot;

namespace GameJamQC2023.Platform
{
	public partial class Platform : StaticBody2D, IColorable
	{
		[Export]
		private ColorData colorData;

		ColorData IColorable.ColorData => colorData;

		private Sprite2D spriteTexture;

		public override void _Ready()
		{
			colorData ??= GD.Load<ColorData>("res://System/Color/ColorData/Data/Transparent.tres");
			
			spriteTexture = GetNode<Sprite2D>("Sprite2D");
			spriteTexture.SelfModulate = colorData.Color;
		}

		public void ReceiveColorData(IColorable sender)
		{
			colorData = sender.ColorData;
			spriteTexture.SelfModulate = colorData.Color;
		}

		public void BlendColorData(IColorable sender)
		{
			colorData.Blend(sender.ColorData.Color);
			spriteTexture.SelfModulate = colorData.Color;
		}

		public void DropColorData()
		{
			colorData.Reset();
			spriteTexture.SelfModulate = colorData.Color;
		}


		private void OnPlatformBodyEntered(Node2D body)
		{
			var colorable = body.GetParent<IColorable>();
			if (colorable == null)
				return;
			
			GD.Print($"Colorable is {colorable.ColorData} \t {Name} is {colorData}");
			
			if (colorable.ColorData.Name != "Transparent" && colorData.Name == "Transparent")
			{
				ReceiveColorData(colorable);
				colorable.DropColorData();
			}
			else if (colorable.ColorData.Name == "Transparent" && colorData.Name != "Transparent")
			{
				colorable.ReceiveColorData(this);
				DropColorData();
			}
			else if (colorable.ColorData.Name != "Transparent" && colorData.Name != "Transparent")
			{
				colorable.BlendColorData(this);
				DropColorData();
			}
		}
	}
}
