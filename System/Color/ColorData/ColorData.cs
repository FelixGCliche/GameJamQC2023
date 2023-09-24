using Godot;

namespace GameJamQC2023.Color
{
	public partial class ColorData : Resource
	{
		[Export]
		private Godot.Color color;
		
		[Export]
		private string name;

		public string Name => name;

		public Godot.Color Color => color;

		public override string ToString() => $"{Name} ({Color.ToHtml(false)})";

		public void Reset()
		{
			color = new Godot.Color(1f,1f,1f,1f);
			name = "Transparent";
		}
		
		public void Blend(Godot.Color other)
		{
			var blendedColor = Color.Blend(other);
			GD.Print($"Blend result {blendedColor.ToHtml(false)}");
			color = blendedColor;
		}
	}
}
