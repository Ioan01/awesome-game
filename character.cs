using Godot;

namespace Awesomegame;

public abstract partial class character : CharacterBody2D
{
    private bool _isDead = false;
    private int _hp = 5;

    public virtual int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            TookDamage();
        }
    }

    [Export]
    protected abstract float Speed { get; set; }
    
    protected virtual AnimatedSprite2D sprite2D { get; set; } = null!;

    protected virtual  CollisionShape2D collision { get; set; } = null!;

    protected virtual bool isFlipped { get; set; }

    protected virtual bool wasFlipped { get; set; }

    protected virtual bool isFacingRight { get; set; } = true;
    protected void TookDamage()
    {
        if (Hp <= 0)
        {
            foreach (var child in GetChildren())
            {
                if (child.Name != "skull")
                {
                    if (child is AnimatedSprite2D sprite)
                        sprite.Visible = false;
                    else if (child is Sprite2D spr)
                        spr.Visible = false;
                }

                else if (child is Sprite2D s) {s.Visible = true; GD.Print(s.Name);}
            }
            
            collision.SetDeferred("disabled", true);

            _isDead = true;
        }
    }
    protected void Move(Vector2 direction)
    {
        if (_isDead) return;
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