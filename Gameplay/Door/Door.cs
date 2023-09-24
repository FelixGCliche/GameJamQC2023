using Godot;
using System;
using GameJamQC2023.Player;

public partial class Door : Area2D
{
	Sprite2D lockSprite;
	private Color lockColor = Colors.Black;

	private SceneTransitionRect transitionRect;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		lockSprite = GetNode<Sprite2D>("LockSprite2D");
			
        transitionRect = GetParent().GetNode<CanvasLayer>("CanvasLayer").GetNode<ColorRect>("SceneTransitionRect") as SceneTransitionRect;

        if ((bool)GetMeta("isStartDoor"))
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
        AudioStreamPlayer2D soundPlayer = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		AudioStream lockedSound = (AudioStream)GD.Load("res://Music/Sounds/Locked_Door.mp3");
        AudioStream OpenSound = (AudioStream)GD.Load("res://Music/Sounds/Open_Door.mp3");

        if ((bool) GetMeta("isStartDoor"))
			return;
			

		if (body.GetParent() is not PlayerController player)
			return;

		if (player.GetBlendedColor() == lockColor)
        {
            soundPlayer.Stream = OpenSound;
            soundPlayer.Play();
            transitionRect.transitionTo((string)GetMeta("newScenePath"));
        }
		else
        {
            soundPlayer.Stream = lockedSound;
            soundPlayer.Play();
        }
    }
}
