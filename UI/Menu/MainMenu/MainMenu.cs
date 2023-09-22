using Godot;
using System;

namespace GameJamQC.UI
{
	public partial class MainMenu : Control
	{
		private void OnPlayButtonPressed()
		{
			GetTree().ChangeSceneToFile("Scenes/scn_Game.tscn");
		}
		
		private void OnExitButtonPressed()
		{
			GetTree().Quit();
		}
	}
}
