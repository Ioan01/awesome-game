using System;
using System.Linq;
using Awesomegame;
using Godot;

public partial class player : character
{
	private static AudioStream attack;

	private static AudioStream dash;
	
	private GlobalState state;
	
	private int _hp = 5;
	public override int Hp
	{
		get => _hp;
		set
		{
			_hp = value;
			if (!isPlayer1) state.Player1Hearts = value;
			else state.Player2Hearts = value;
			TookDamage();
		}
	}

	private PointLight2D light2D;
	[Export]
	public bool isPlayer1 = true;

	private float elapsedDash = 0;

	[Export]
	protected override float Speed { get; set; } = 800;

	private float elapsed = 0;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public override void _Ready()
	{
		attack = GD.Load<AudioStream>("res://sounds/Laser1.wav");
		dash = GD.Load<AudioStream>("res://sounds/Ping1.wav");

		
		base._Ready();
		state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
		sprite2D = FindChild("animations") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		light2D = FindChild("light") as PointLight2D;
		
		sprite2D.Play("idle");

		if (!isPlayer1)
		{
			_hp = state.Player2Hearts;
			sprite2D.Modulate = new Color("e68d00");

		}
		else _hp = state.Player1Hearts;
		
		AddToGroup("players");
	}

	public void setIntensity(float intensity)
	{
		light2D.Energy = intensity;
	}

	public override void _PhysicsProcess(double delta)
	{
		elapsed += (float)delta;
		
		Vector2 direction = isPlayer1
			? Input.GetVector("left", "right", "up", "down")
			: Input.GetVector("left_2", "right_2", "up_2", "down_2");
		Move(direction, (float)delta);

		elapsedDash += (float)delta;
		if (Input.IsActionPressed("dash") && elapsedDash >= 1)
		{
			player.Stream = dash;
			player.Play();
			KnockBack(direction);
			elapsedDash = 0;
		}

		if (Input.IsActionPressed("shoot") && isPlayer1 && elapsed >= 0.75f / GlobalState.AttackSpeedModifier / GlobalState.SpeedModifier)
		{
			var b = GD.Load<PackedScene>("res://projectile.tscn").Instantiate() as projectile;
			Owner.AddChild(b);
			(b.FindChild("sprite") as AnimatedSprite2D).Scale *= GlobalState.SizeModifier;
			(b.FindChild("CollisionShape2D") as CollisionShape2D).Scale *= GlobalState.SizeModifier;	
			b.GlobalTransform = GlobalTransform;
			b.direction = (GetGlobalMousePosition() - GlobalPosition).Normalized();

			elapsed = 0;


			player.Stream = attack;
			player.Play();
		}

		var ctrlrAimDir = Input.GetVector("shoot_left", "shoot_right", "shoot_up", "shoot_down");

		if (ctrlrAimDir != Vector2.Zero && !isPlayer1 &&
			elapsed >= 0.75f / GlobalState.AttackSpeedModifier / GlobalState.SpeedModifier)
		{
			player.Stream = attack;
			player.Play();
			var b = GD.Load<PackedScene>("res://projectile.tscn").Instantiate() as projectile;
			if (b != null)
			{
				Owner.AddChild(b);
				(b.FindChild("sprite") as AnimatedSprite2D).Scale *= GlobalState.SizeModifier;
				(b.FindChild("CollisionShape2D") as CollisionShape2D).Scale *= GlobalState.SizeModifier;
				b.GlobalTransform = GlobalTransform;
				b.direction = ctrlrAimDir.Normalized();
			}

			elapsed = 0;
			
		}
		
	}

	public override void _Process(double delta)
	{
		if (isPlayer1)
			if (Input.IsActionPressed("pickup"))
			{
				if (state.hoveredItem != null)
				{
					if (state.Gold >= state.hoveredItem.Price)
					{
						state.hoveredItem.OnBuy(this);
						state.Gold -= state.hoveredItem.Price;
					}
				}
			}
		else 
			if (Input.IsActionPressed("pickup_2"))
			{
				if (state.hoveredItem != null)
				{
					if (state.Gold >= state.hoveredItem.Price)
					{
						state.hoveredItem.OnBuy(this);
						state.Gold -= state.hoveredItem.Price;
					}
				}
			}
	}
	
}
