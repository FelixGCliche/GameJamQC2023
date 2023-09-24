using Godot;

namespace GameJamQC2023.Player
{
	public partial class PlayerPowerController : Node2D
	{
		[Signal]
		public delegate void RedPowerUpdatedEventHandler(bool enabled);

		[Signal]
		public delegate void GreenPowerUpdatedEventHandler(bool enabled);

		[Signal]
		public delegate void BluePowerUpdatedEventHandler(bool enabled);

		private void OnPlayerHeldColorsUpdated(Godot.Color currentColor)
		{
			EmitSignal(SignalName.RedPowerUpdated, currentColor.R > 0);
			EmitSignal(SignalName.GreenPowerUpdated, currentColor.G > 0);
			EmitSignal(SignalName.BluePowerUpdated, currentColor.B > 0);
		}
	}
}
