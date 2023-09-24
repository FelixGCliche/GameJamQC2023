using Godot;

public partial class MainMenu : Control
{
	private void OnPlayButtonPressed()
	{
		GetTree().ChangeSceneToFile("Scenes/scn_Level1.tscn");
	}

	private void OnExitButtonPressed()
	{
		GetTree().Quit();
	}
}
