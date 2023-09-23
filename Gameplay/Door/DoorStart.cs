using Godot;
using System;

public partial class DoorStart : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if((bool)GetMeta("isStartDoor"))
			GetParent().GetNode<CharacterBody2D>("Player").Position = Position;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
    }

	private void OnCollisionEnter(Node2D body)
	{
		PrintTree();
	}

}
