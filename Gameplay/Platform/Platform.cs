using System.Collections.Generic;
using System.Linq;
using GameJamQC2023.Color;
using GameJamQC2023.Utils;
using Godot;

namespace GameJamQC2023.Platform
{
	public partial class Platform : StaticBody2D, IColorable
	{
		[Export]
		private Godot.Color baseColor = new(0f, 0f, 0f);

		public Queue<Godot.Color> HeldColors { get; private set; }
		
		private Sprite2D spriteTexture;

		public override void _Ready()
		{
			spriteTexture = GetNode<Sprite2D>("Sprite2D");
			
			HeldColors = new Queue<Godot.Color>();
			for (var i = 0; i < 2; i++)
				HeldColors.Enqueue(Colors.Black);
			EnqueueColor(baseColor);
			
			spriteTexture.SelfModulate = GetBlendedColor();
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
	}
}
