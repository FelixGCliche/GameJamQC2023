using Godot;

public partial class PlayerController : CharacterBody2D
{
	[Export]
	private float speed = 300.0f;

	[Export]
	private float fallMultiplier = 2.5f;

	[Export] 
	private float shortJumpMultiplier = 2f;
	
	private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		var velocity = Velocity;
		
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		
		var direction = Input.GetAxis("MoveLeft", "MoveRight");
		Move(velocity, direction);
		MoveAndSlide();
	}

	private void Move(Vector2 velocity, float direction)
	{
		
		if (direction != 0)
			velocity.X = direction * speed;
		else
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);

		Velocity = velocity;
	}

	private void Jump(Vector2 velocity)
	{
	}
}
