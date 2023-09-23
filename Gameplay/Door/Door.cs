using Godot;
using System;
using GameJamQC2023.Player;

public partial class Door : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if((bool)GetMeta("isStartDoor"))
			GetParent().GetNode<PlayerController>("Player").Position = Position;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
    }

	private void OnCollisionEnter(Node2D body)
	{
		
        if (!(bool)GetMeta("isStartDoor"))
		{
            body.PrintTree();
			GetTree().ChangeSceneToFile((string)GetMeta("newScenePath"));
        }
            
    }

}
