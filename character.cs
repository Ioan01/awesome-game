using Godot;

namespace Awesomegame;

public abstract partial class character : CharacterBody2D
{
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
            RemoveFromGroup("enemies");
            RemoveFromGroup("npcs");
            RemoveFromGroup("players");
            QueueFree();
            var coin = GD.Load<PackedScene>("res://resources/items/coin/coin.tscn").Instantiate() as coin;
            coin.GlobalPosition = GlobalPosition;
            GetTree().CurrentScene.FindChild("map").AddChild(coin);

            if (this is enemy)
                GlobalState.SpeedModifier += 0.05f;
        }
    }

    public void KnockBack(Vector2 direction)
    {
        knockBackVelocity = direction * 3;
        sinceKnockback = 0;
    }
    
    protected void Move(Vector2 direction, float delta)
    {
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
			
        }
        else
        {
            velocity.X = (knockBackVelocity != Vector2.Zero) ? knockBackVelocity.X * Speed : Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = (knockBackVelocity != Vector2.Zero) ? knockBackVelocity.Y * Speed : Mathf.MoveToward(Velocity.Y, 0, Speed);
			
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