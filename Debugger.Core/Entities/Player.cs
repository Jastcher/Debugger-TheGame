
using System.Numerics;
using Debugger.Core.Components;

namespace Debugger.Core.Entities
{

    public class Player : Entity
    {
        public Vector2 MovementVector { get; set; }
        // direction of shooting
        public Vector2 FacingVector { get; set; }
        // direction for animations
        public Vector2 FacingDirection { get; set; }

        private string? _lastDirection;
        public Player()
        {
            // fix magic numbers
            Hitbox = new CircleHitbox(10.0f, new(64, 64 + 20));
            TextureKey = "player_walk";
            Width = 32;
            Height = 32;
            Scale = 4.0f;

            Position = new(50, 50);

            Animator = new();
            Animator.AddAnimation(new("walk_down", "player_walk", 0, 4, 0.1f, true));
            Animator.AddAnimation(new("walk_right", "player_walk", 4, 4, 0.1f, true));
            Animator.AddAnimation(new("walk_left", "player_walk", 4, 4, 0.1f, true, flipHorizontal: true));
            Animator.AddAnimation(new("walk_up", "player_walk", 8, 4, 0.1f, true));

            Animator.AddAnimation(new("idle_down", "player_idle", 0, 2, 0.5f, true));
            Animator.AddAnimation(new("idle_right", "player_idle", 2, 2, 0.5f, true));
            Animator.AddAnimation(new("idle_left", "player_idle", 2, 2, 0.5f, true, flipHorizontal: true));
            Animator.AddAnimation(new("idle_up", "player_idle", 4, 2, 0.5f, true));

            Animator.Play("idle_down");

        }


        public override void Update(float dt)
        {

            base.Update(dt);

            HandleAnimationState();
        }

        public void HandleAnimationState()
        {
            if (Animator == null) return;

            bool isMoving = MovementVector != Vector2.Zero;
            
            if (FacingVector != Vector2.Zero || isMoving)
            {
                Vector2 targetVector = FacingVector != Vector2.Zero ? FacingVector : MovementVector;

                _lastDirection = Math.Abs(targetVector.X) >= Math.Abs(targetVector.Y)
                    ? (targetVector.X > 0 ? "right" : "left")
                    : (targetVector.Y > 0 ? "down" : "up");
            }

            string action = isMoving ? "walk" : "idle";
            Animator.Play($"{action}_{_lastDirection}");
        }
    }
}