using Godot;
using System;

public partial class SceneTransitionRect : ColorRect
{
	AnimationPlayer animPlayer;
	string scenePath;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		animPlayer = GetNode<AnimationPlayer>("FadePlayer");
		animPlayer.PlayBackwards("Fade");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void transitionTo(string newScenePath)
	{
		animPlayer.Play("Fade");
        scenePath = newScenePath;
    }

	private void OnFadeEnded(StringName anim_name)
	{
        GetTree().ChangeSceneToFile(scenePath);
    }
}
