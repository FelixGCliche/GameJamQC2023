using Godot;
using System;
using GameJamQC2023.Player;

public partial class Door : Area2D
{
	Sprite2D lockSprite;
	private Color lockColor = Colors.Black;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		lockSprite = GetNode<Sprite2D>("LockSprite2D");

		if ((bool) GetMeta("isStartDoor"))
		{
			lockSprite.Visible = false;
			GetParent().GetNode<PlayerController>("Player").Position = Position;
		}
		else
		{
			lockColor = (Color) GetMeta("colorToUnlock");
			lockSprite.SelfModulate = lockColor;
			lockSprite.Visible = true;
		}
	}

	private void OnCollisionEnter(Node2D body)
	{
		if ((bool) GetMeta("isStartDoor"))
			return;

		if (body.GetParent() is not PlayerController player)
			return;

		if (player.GetBlendedColor() == lockColor)
			GetTree().ChangeSceneToFile((string) GetMeta("newScenePath"));
		else
			GD.Print($"Lock color is {lockColor} and you are {player.GetBlendedColor()}");
	}
}
