using Godot;

namespace GameJamQC2023.Player
{
	public partial class PlayerController : Node2D, IColorable
	{
		[Export]
		public ColorData ColorData { get; private set; } = GD.Load<ColorData>("res://System/Color/ColorData/Data/Black.tres");

		public void SendColorData()
		{
			throw new System.NotImplementedException();
		}

		public void ReceiveColorData(ColorData colorData)
		{
			throw new System.NotImplementedException();
		}

		public void BlendColorData()
		{
			throw new System.NotImplementedException();
		}

		public void DropColorData()
		{
			throw new System.NotImplementedException();
		}
	}
}
