using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	private float speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		var direction = Input.GetVector("MoveLeft", "MoveRight", "", "");
		Move(direction);
		MoveAndSlide();
	}

	private void Move(Vector2 direction)
	{
		var velocity = Velocity;
		
		if (direction != Vector2.Zero)
			velocity.X = direction.X * speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);

		Velocity = velocity;
	}
}
