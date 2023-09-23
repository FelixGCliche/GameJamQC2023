using Godot;
using System;
	public partial class PauseMenu : Control
	{
		private void OnResumeButtonPressed()
		{
			GetTree().Paused = false;
			Visible = false;
		}

		private void OnExitButtonPressed()
		{
			GetTree().Paused = false;
			Visible = false;
			GetTree().ChangeSceneToFile("Scenes/scn_MainMenu.tscn");
		}
}
