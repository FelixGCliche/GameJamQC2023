using Godot;

public partial class GameController : Node2D
{
	private Control pauseMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		pauseMenu = GetNode<Control>("CanvasLayer/PauseMenu");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Pause();
		ResetLevel();
	}

	private void  Pause ()
	{
        if (!Input.IsActionPressed("Pause"))
            return;
        pauseMenu.Visible = true;
        GetTree().Paused = true;
    }

    private void ResetLevel()
    {
        if (Input.IsActionPressed("Reset"))
			GetTree().ReloadCurrentScene();
    }
}
