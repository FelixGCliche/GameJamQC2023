using Godot;

namespace GameJamQC2023.Player
{
    public partial class PlayerController : CharacterBody2D
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

        public override void _PhysicsProcess(double delta)
        {
            var movementVelocity = Velocity;

            if (!IsOnFloor())
                movementVelocity.Y += gravity * (float) delta;

            movementVelocity = Move(movementVelocity);
            movementVelocity = Jump(movementVelocity, (float) delta);

            Velocity = movementVelocity;
            MoveAndSlide();
            GD.Print(Velocity);
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
    }
}