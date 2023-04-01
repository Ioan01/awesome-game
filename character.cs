using Godot;

namespace Awesomegame;

public partial class character : CharacterBody2D
{
    [Export]
    protected virtual float Speed { get; set; } = 300.0f;
    
    protected virtual AnimatedSprite2D sprite2D { get; set; } = null!;

    protected virtual  CollisionShape2D collision { get; set; } = null!;

    protected virtual bool isFlipped { get; set; }

    protected virtual bool wasFlipped { get; set; }

    protected virtual bool isFacingRight { get; set; } = true;
    protected void Move(Vector2 direction)
    {
        Vector2 velocity = Velocity;
		
        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
		
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Y = direction.Y * Speed;

            if (direction.X < 0)
            {
                isFlipped = true;
            }
            else isFlipped = false;

            if (isFlipped != wasFlipped)
                Scale = new Vector2(-1 * Scale.X, Scale.Y);

			
            wasFlipped = isFlipped;
			
            sprite2D?.Play("run");
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			
            sprite2D?.Play("idle");
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}