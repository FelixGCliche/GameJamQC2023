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
		private float dashSpeed = 2000;

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

		[Export]
		private float dashTime = 0.1f;

		[Export]
		private float floatTime = 3f;

		private int maxNbJump = 2;
		private int nbJump = 1;
		private bool doubleJumpEnabled;

		private bool dashEnabled;
		private int nbDash = 1;
		private float dashTimer;

		private bool floatEnabled;
		private int nbFloat = 1;
		private float floatTimer;

		private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

		public override void _Ready()
		{
			if (doubleJumpEnabled)
				nbJump = maxNbJump;
		}

		public override void _PhysicsProcess(double delta)
		{
			var movementVelocity = Velocity;

			if (!IsOnFloor())
				movementVelocity.Y += gravity * (float) delta;

			var direction = Input.GetAxis("MoveLeft", "MoveRight");
			movementVelocity = Move(movementVelocity, direction);

			if (Input.IsActionJustReleased("Jump"))
				nbJump--;
			movementVelocity = Jump(movementVelocity);

			if (dashEnabled && Input.IsActionJustPressed("Dash") && !IsOnFloor() && nbDash > 0)
			{
				dashTimer = dashTime;
				nbDash--;
			}

			movementVelocity = Dash(movementVelocity, direction, (float) delta);

			if (floatEnabled && Input.IsActionPressed("Float") && !IsOnFloor() && nbFloat > 0)
			{
				floatTimer = floatTime;
				nbFloat--;
			}

			movementVelocity = Float(movementVelocity, (float) delta);

			Velocity = movementVelocity;
			MoveAndSlide();

			if (IsOnFloor())
				nbJump = doubleJumpEnabled ? maxNbJump : 1;
			ResetDash();
			ResetFloat();
		}

		private Vector2 Move(Vector2 velocity, float direction)
		{
			if (direction != 0)
				velocity.X += direction * acceleration;
			else
				velocity.X = Mathf.MoveToward(Velocity.X, 0, deceleration);

			if (dashEnabled && dashTimer > 0)
				return velocity;

			velocity.X = IsOnFloor()
				? Mathf.Clamp(velocity.X, -maxGroundSpeed, maxGroundSpeed)
				: Mathf.Clamp(velocity.X, -maxAirSpeed, maxAirSpeed);
			return velocity;
		}

		private Vector2 Jump(Vector2 velocity)
		{
			AudioStreamPlayer2D soundPlayer = GetParent().GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
			AudioStream jumpSound = (AudioStream)GD.Load("res://Music/Sounds/Jump.mp3");
            

            var jumped = Input.IsActionJustPressed("Jump");
			var jumping = Input.IsActionPressed("Jump");
			if (jumped && nbJump > 0)
			{
                velocity.Y = nbJump < maxNbJump ? doubleJumpSpeed : jumpSpeed;
				soundPlayer.Stream = jumpSound;
                soundPlayer.Play();
            }

			if (velocity.Y > jumpVelocityFalloff)
				velocity += Vector2.Up * gravity * fallMultiplier;
			else if (velocity.Y < 0 && !jumping)
				velocity += Vector2.Up * gravity * shortJumpMultiplier;

			return velocity;
		}

		private Vector2 Dash(Vector2 velocity, float direction, float delta)
		{
            AudioStreamPlayer2D soundPlayer = GetParent().GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
            AudioStream dashSound = (AudioStream)GD.Load("res://Music/Sounds/Dash.mp3");
            

            if (dashTimer <= 0)
				return velocity;

			dashTimer -= delta;

			if (direction != 0)
				velocity.X += direction * acceleration;
			velocity.Y = 0;
			velocity.X = Mathf.Clamp(velocity.X, -dashSpeed, dashSpeed);

            soundPlayer.Stream = dashSound;
            soundPlayer.Play();

            return velocity;
		}

		private Vector2 Float(Vector2 velocity, float delta)
		{
			AudioStreamPlayer2D soundPlayer = GetParent().GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
			AudioStream floatSound = (AudioStream)GD.Load("res://Music/Sounds/Float.mp3");

            if (floatTimer > 0 && !soundPlayer.Playing)
            {
                soundPlayer.Stream = floatSound;
                soundPlayer.Play();
            }
			//else if (floatTimer <= 0 && soundPlayer.Playing)
			//{
                
   //         }

            if (floatTimer <= 0)
			{
				return velocity;
            }
			
			if (Input.IsActionJustReleased("Float"))
			{
                soundPlayer.Stop();
                floatTimer = 0;
            }
				

			floatTimer -= delta;
			velocity.Y *= 0.1f;

            return velocity;
		}

		private void ResetDash()
		{
			if (dashEnabled && IsOnFloor() && dashTimer <= 0)
				nbDash = 1;
		}

		private void ResetFloat()
		{
			if (floatEnabled && IsOnFloor() && floatTimer <= 0)
				nbFloat = 1;
		}

		private void OnRedPowerUpdated(bool enabled)
		{
			doubleJumpEnabled = enabled;
		}

		private void OnGreenPowerUpdated(bool enabled)
		{
			dashEnabled = enabled;
		}

		private void OnBluePowerEnabled(bool enabled)
		{
			floatEnabled = enabled;
		}
	}
}
