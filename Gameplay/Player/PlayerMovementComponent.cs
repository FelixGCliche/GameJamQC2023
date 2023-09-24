using Godot;


namespace GameJamQC2023.Player
{
	public partial class PlayerMovementComponent : CharacterBody2D
	{
		[Export]
		private float acceleration = 250.0f;

		[Export]
		private float deceleration = 200.0f;

		[Export]
		private float maxGroundSpeed = 1250.0f;

		[Export]
		private float maxAirSpeed = 600.0f;

		[Export]
		private float jumpSpeed = -1650.0f;

		[Export]
		private float doubleJumpSpeed = -1600.0f;

		[Export]
		private float jumpVelocityFalloff = -1500f;

		[Export]
		private float fallMultiplier = -0.18f;

		[Export]
		private float shortJumpMultiplier = -0.12f;

		private int maxNbJump = 2;
		private int nbJump = 1;
		private bool doubleJumpEnabled;

		private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

		public override void _Ready()
		{
			if(doubleJumpEnabled)
				nbJump = maxNbJump;
		}

		public override void _PhysicsProcess(double delta)
		{
			var movementVelocity = Velocity;

			if (!IsOnFloor())
				movementVelocity.Y += gravity * (float) delta;

			movementVelocity = Move(movementVelocity);
			if (Input.IsActionJustReleased("Jump"))
				nbJump--;
			movementVelocity = Jump(movementVelocity);


			Velocity = movementVelocity;
			MoveAndSlide();

			if (IsOnFloor())
				nbJump = doubleJumpEnabled ? maxNbJump : 1;
		}

		private Vector2 Move(Vector2 velocity)
		{
			var direction = Input.GetAxis("MoveLeft", "MoveRight");

			if (direction != 0)
				velocity.X += direction * acceleration;
			else
				velocity.X = Mathf.MoveToward(Velocity.X, 0, deceleration);

			velocity.X = IsOnFloor()
				? Mathf.Clamp(velocity.X, -maxGroundSpeed, maxGroundSpeed)
				: Mathf.Clamp(velocity.X, -maxAirSpeed, maxAirSpeed);
			return velocity;
		}

		private Vector2 Jump(Vector2 velocity)
		{
			var jumped = Input.IsActionJustPressed("Jump");
			var jumping = Input.IsActionPressed("Jump");
			if (jumped && nbJump > 0)
				velocity.Y = nbJump < maxNbJump ? doubleJumpSpeed : jumpSpeed;

			if (velocity.Y > jumpVelocityFalloff)
				velocity += Vector2.Up * gravity * fallMultiplier;
			else if (velocity.Y < 0 && !jumping)
				velocity += Vector2.Up * gravity * shortJumpMultiplier;

			return velocity;
		}

		private void OnRedPowerUpdated(bool enabled)
		{
			doubleJumpEnabled = enabled;
		}
	}
}
