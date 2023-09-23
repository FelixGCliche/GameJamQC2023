using Godot;

namespace GameJamQC2023.Player
{
    public partial class PlayerController : CharacterBody2D, IColorable
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
		private float jumpVelocityFalloff = -1500f;

		[Export]
		private float fallMultiplier = -0.18f;

		[Export]
		private float shortJumpMultiplier = -0.12f;

		private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

		[Export]
		private ColorData colorData;
        public ColorData ColorData => colorData;

        public override void _PhysicsProcess(double delta)
		{
			var movementVelocity = Velocity; 

			if (!IsOnFloor())
				movementVelocity.Y += gravity * (float) delta;

			movementVelocity = Move(movementVelocity);
			movementVelocity = Jump(movementVelocity, (float) delta);

			Velocity = movementVelocity;
			MoveAndSlide();


			var collisionInfo = MoveAndCollide(movementVelocity * (float)delta);
			HandleColorChange(collisionInfo);
		}

		private Vector2 Move(Vector2 velocity)
		{
			var direction = Input.GetAxis("MoveLeft", "MoveRight");

			if (direction != 0)
				velocity.X += direction * acceleration;
			else
				velocity.X = Mathf.MoveToward(Velocity.X, 0, deceleration);

			velocity.X = IsOnFloor() ? Mathf.Clamp(velocity.X, -maxGroundSpeed, maxGroundSpeed) : Mathf.Clamp(velocity.X, -maxAirSpeed, maxAirSpeed);
			return velocity;
		}

		private Vector2 Jump(Vector2 velocity, float delta)
		{
			var jumped = Input.IsActionJustPressed("Jump");
			var jumping = Input.IsActionPressed("Jump");

			if (IsOnFloor() && jumped)
				velocity.Y = jumpSpeed;
			
			if(velocity.Y > jumpVelocityFalloff)
				velocity += Vector2.Up * gravity * fallMultiplier;
			else if (velocity.Y < 0 && !jumping)
				velocity += Vector2.Up * gravity * shortJumpMultiplier;
			
			return velocity;
		}

		private void HandleColorChange(KinematicCollision2D collision)
		{
			if (collision == null)
				return;

            if (collision.GetCollider() is Platform platform)
            {
                GD.Print($"Platform {platform}");
                ReceiveColorData(platform.ColorData);
            }
        }

        public void SendColorData()
        {
            throw new System.NotImplementedException();
        }

        public void ReceiveColorData(ColorData colorData)
        {
            throw new System.NotImplementedException();
        }

        public void BlendColorData()
        {
            throw new System.NotImplementedException();
        }

        public void DropColorData()
        {
            throw new System.NotImplementedException();
        }
    }
}
