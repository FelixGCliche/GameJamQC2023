using Godot;

namespace GameJamQC2023.Player
{
    public partial class PlayerController : CharacterBody2D
    {
        [Export]
        private float speed = 600.0f;

        [Export]
        private float jumpVelocity = -400.0f;

        [Export]
        private float fallMultiplier = -2.5f;

        [Export]
        private float shortJumpMultiplier = -2.0f;

        private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

        public override void _PhysicsProcess(double delta)
        {
            var movementVelocity = Velocity;

            if (!IsOnFloor())
                movementVelocity.Y += gravity * (float) delta;

            var direction = Input.GetAxis("MoveLeft", "MoveRight");
            movementVelocity = Move(movementVelocity, direction);
            movementVelocity = Jump(movementVelocity, Input.IsActionPressed("Jump"), (float) delta);

            Velocity = movementVelocity;
            MoveAndSlide();
        }

        private Vector2 Move(Vector2 velocity, float direction)
        {
            if (direction != 0)
                velocity.X = direction * speed;
            else
                velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
            return velocity;
        }

        private Vector2 Jump(Vector2 velocity, bool jumped, float delta)
        {
            if (IsOnFloor() && jumped)
                velocity.Y = jumpVelocity;

            switch (velocity.Y)
            {
                case > 0:
                    velocity += Vector2.Up * gravity * (fallMultiplier - 1f) * delta;
                    break;
                case < 0 when !jumped:
                    velocity += Vector2.Up * gravity * (shortJumpMultiplier - 1f) * delta;
                    break;
            }

            return velocity;
        }
    }
}