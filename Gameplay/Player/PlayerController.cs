using System.Collections.Generic;
using System.Linq;
using GameJamQC2023.Color;
using GameJamQC2023.Utils;
using Godot;

namespace GameJamQC2023.Player
{
	public partial class PlayerController : Node2D, IColorable
	{
		private Sprite2D spriteTexture;
		public Queue<Godot.Color> HeldColors { get; private set; }

		private IColorable currentColorable;

		public override void _Ready()
		{
			HeldColors = new Queue<Godot.Color>();
			for (var i = 0; i < 2; i++)
				HeldColors.Enqueue(Colors.Black);
			
			spriteTexture = GetNode<Sprite2D>("PlayerMovementComponent/PlayerSprite");
		}

		public override void _Process(double delta)
		{
			if(currentColorable != null && Input.IsActionJustPressed("DropColor"))
			{
				currentColorable.EnqueueColor(GetBlendedColor());
				ResetColor();
				currentColorable = null;
			}
		}

		public Godot.Color GetBlendedColor()
		{
			var colorArray = HeldColors.ToArray();
			return colorArray[0].RgbBlend(colorArray[1]);
		}

		public Godot.Color EnqueueColor(Godot.Color newColor)
		{
			if (newColor == Colors.Black)
				return Colors.Transparent;
			
			var discard = HeldColors.TryDequeue(out var result) ? result : Colors.Transparent;
			if(discard != Colors.Transparent)
			
			if (HeldColors.Count != 1)
			{
				GD.Print($"Held Colors count {HeldColors.Count}, expected 1");
				return Colors.Transparent;
			}

			HeldColors.Enqueue(newColor);
			spriteTexture.SelfModulate = GetBlendedColor();

			return discard;
		}

		public void ResetColor()
		{
			HeldColors = new Queue<Godot.Color>();
			for (var i = 0; i < 2; i++)
				HeldColors.Enqueue(Colors.Black);
			spriteTexture.SelfModulate = GetBlendedColor();
		}


		private void OnPlayerBodyEntered(Node2D body)
		{
			if (body is not IColorable colorable) 
				return;
			currentColorable = colorable;

			EnqueueColor(colorable.GetBlendedColor());
			colorable.ResetColor();
		}
	}
}
