using System.IO;
using System.Linq;
using Godot;

namespace Awesomegame;

public abstract partial class character : CharacterBody2D
{
    private static AudioStream enemyHit;
    private static AudioStream playerHit;
    
    
    
    public override void _Ready()
    {
        player = new AudioStreamPlayer();
        enemyHit = GD.Load<AudioStream>("res://sounds/Hurt1.wav");
        playerHit = GD.Load<AudioStream>("res://sounds/Hurt4.wav");
        
        AddChild(player);


    }

    protected AudioStreamPlayer player;
    
    
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
        if (player != null)
        {
            if (this is enemy)
                player.Stream = character.enemyHit;
            else player.Stream = character.playerHit;
        }
        
        if (Hp <= 0)
        {

            
            RemoveFromGroup("enemies");
            RemoveFromGroup("npcs");
            RemoveFromGroup("players");
            QueueFree();
            
            GlobalState state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
            if (this is player && GetTree().GetNodesInGroup("players").Count == 0)
                state.reset();
            
            var coin = GD.Load<PackedScene>("res://resources/items/coin/coin.tscn").Instantiate() as coin;
            coin.Position = Position;
            GetTree().CurrentScene.FindChild("map").AddChild(coin);
            
            var death = GD.Load<PackedScene>("res://death.tscn").Instantiate() as death;
            death.Position = Position;
            GetTree().CurrentScene.FindChild("map").AddChild(death);

            if (this is enemy)
            {
                
                
                GlobalState.SpeedModifier += 0.05f;
                state.Darkness -= 0.04f;
                
                var closeBy = GetTree().GetNodesInGroup("enemies").Where(x =>
                {
                    var e = x as enemy;
                    if ((e.Transform.X.X - Transform.X.X) * (e.Transform.X.X - Transform.X.X) -
                        (e.Transform.Y.Y - Transform.Y.Y) * (e.Transform.Y.Y - Transform.Y.Y) <= 4)
                        return true;

                    return false;
                }).Take(3);
            
                foreach (var n in closeBy)
                {
                    var x = n as enemy;
                    x.Hp--;
                }
            }

            if (GetTree().GetNodesInGroup("players").Count == 0)
            {
                
                
                
                foreach (character node in GetTree().GetNodesInGroup("players"))
                {
                    node.Position = Vector2.Zero;
                    
                }
                
                foreach (character node in GetTree().GetNodesInGroup("enemies"))
                {
                    node.QueueFree();
                    
                }
                var player = GD.Load<PackedScene>("res://resources/characters/player/player.tscn").Instantiate() as player;
                player.Position = Vector2.Zero;
                player.AddToGroup("players");
                var map = GetTree().CurrentScene.FindChild("map");
                map.AddChild(player);
                player.Owner = map;
                
                var player2 = GD.Load<PackedScene>("res://resources/characters/player/player.tscn").Instantiate() as player;
                player2.Position = new Vector2(100, 100);
                player2.isPlayer1 = false;
                player2.AddToGroup("players");
                map.AddChild(player2);
                player2.Owner = map;

                
                
            }
        }
        
        if (player != null)
            player.Play();

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