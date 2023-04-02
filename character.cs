using Godot;

namespace Awesomegame;

public abstract partial class character : CharacterBody2D
{
    public bool IsDead { get; set; } = false;
    protected int _hp = 5;

    public virtual int Hp
    {
        get => _hp;
        set
        {
            _hp = value;
            TookDamage();
        }
    }

    private Vector2 knockBackVelocity = Vector2.Zero;
    private float sinceKnockback;

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
            
            collision?.SetDeferred("disabled", true);

            IsDead = true;
            
            RemoveFromGroup("enemies");
            RemoveFromGroup("npcs");
            RemoveFromGroup("players");
        }
    }

    public void KnockBack(Vector2 direction)
    {
        knockBackVelocity = direction * 3;
        sinceKnockback = 0;
    }
    
    protected void Move(Vector2 direction, float delta)
    {
        if (IsDead) return;
        Vector2 velocity = Velocity;
		
        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
		
        if (direction != Vector2.Zero)
        {
            velocity = ((knockBackVelocity != Vector2.Zero) ? knockBackVelocity : direction) * Speed;

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
            velocity.X = (knockBackVelocity != Vector2.Zero) ? knockBackVelocity.X * Speed : Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = (knockBackVelocity != Vector2.Zero) ? knockBackVelocity.Y * Speed : Mathf.MoveToward(Velocity.Y, 0, Speed);
			
            sprite2D?.Play("idle");
        }

        sinceKnockback += delta;

        if (sinceKnockback > 0.25f)
        {
            knockBackVelocity = Vector2.Zero;
            sinceKnockback = 0;
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}