using GameJamQC2023.Color;
using Godot;

namespace GameJamQC2023.Player
{
	public partial class PlayerController : Node2D, IColorable
	{
		[Export]
		private ColorData colorData;

		public ColorData ColorData => colorData;

		private Sprite2D spriteTexture;

		public override void _Ready()
		{
			colorData ??= GD.Load<ColorData>("res://System/Color/ColorData/Data/Transparent.tres");
			spriteTexture = GetNode<Sprite2D>("PlayerMovementComponent/PlayerSprite");
		}

		public void ReceiveColorData(IColorable sender)
		{
			colorData = sender.ColorData;
			GD.Print(colorData);
			spriteTexture.SelfModulate = colorData.Color;
		}

		public void BlendColorData(IColorable sender)
		{
			colorData.Blend(sender.ColorData.Color);
			spriteTexture.SelfModulate = colorData.Color;
		}

		public void DropColorData()
		{
			// colorData.Reset();
			// spriteTexture.SelfModulate = colorData.Color;
		}
	}
}
