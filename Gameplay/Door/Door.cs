using Godot;
using System;
using GameJamQC2023.Player;

public partial class Door : Area2D
{
	Sprite2D lockSprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lockSprite = GetNode<Sprite2D>("LockSprite2D");

        if ((bool)GetMeta("isStartDoor"))
		{
            lockSprite.Visible = false;
            GetParent().GetNode<PlayerController>("Player").Position = Position;
		}
		else
		{
            lockSprite.Modulate = (Color)GetMeta("colorToUnlock");
            lockSprite.Visible = true;
		}
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
    }

	private void OnCollisionEnter(Node2D body)
	{

        if (!(bool)GetMeta("isStartDoor") && body.GetNode<Sprite2D>("PlayerSprite").SelfModulate == (Color)GetMeta("colorToUnlock"))
		{
			body.PrintTree();
			GetTree().ChangeSceneToFile((string)GetMeta("newScenePath"));
        }
            
    }

}
