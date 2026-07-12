
using System.Numerics;
using Debugger.Core.Components;

namespace Debugger.Core.Entities
{
    public enum Direction { Up, Down, Left, Right }

    public class Player : Entity
    {
        public Direction Facing { get; private set; } = Direction.Down;
        public Player(Vector2 startPosition) : base(startPosition)
        {
            Hitbox = new CircleHitbox(10.0f, new(0,20.0f));
            TextureKey = "player_walk";
            Width = 32;
            Height = 32;
            Scale = 4.0f;

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
        
        public void HandleAnimationState(Vector2 movementInput)
        {
            if (Animator == null) return;

            bool isMoving = movementInput != Vector2.Zero;

            if (isMoving)
            {
                if (Math.Abs(movementInput.X) >= Math.Abs(movementInput.Y))
                {
                    Facing = (movementInput.X > 0) ? Direction.Right : Direction.Left;
                }
                else
                {
                    Facing = (movementInput.Y > 0) ? Direction.Down : Direction.Up;
                }
            }

            string animationToPlay = Facing switch
            {
                Direction.Up    => isMoving ? "walk_up"    : "idle_up",
                Direction.Down  => isMoving ? "walk_down"  : "idle_down",
                Direction.Left  => isMoving ? "walk_left"  : "idle_left",
                Direction.Right => isMoving ? "walk_right" : "idle_right",
                _ => "idle_down"
            };

            Animator.Play(animationToPlay);
        }
    }
}