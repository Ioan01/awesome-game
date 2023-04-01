using System;
using System.Linq;
using Awesomegame;
using Godot;

public partial class player : character
{

	private PointLight2D light2D;
	[Export]
	private bool isPlayer1 = true;

	[Export]
	protected override float Speed { get; set; } = 800;

	private float elapsed = 0;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public override void _Ready()
	{
		sprite2D = FindChild("animations") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		light2D = FindChild("light") as PointLight2D;
		
		sprite2D.Play("idle");

		if (!isPlayer1)
			sprite2D.Modulate = new Color("e68d00");
		
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
		Move(direction);
		
		if (Input.IsActionPressed("shoot") && isPlayer1 && elapsed >= 0.75f / GlobalState.AttackSpeedModifier / GlobalState.SpeedModifier)
		{
			var b = GD.Load<PackedScene>("res://projectile.tscn").Instantiate() as projectile;
			Owner.AddChild(b);
			b.Transform = GlobalTransform;
			b.direction = (GetGlobalMousePosition() - Position).Normalized();

			elapsed = 0;
		}

		var ctrlrAimDir = Input.GetVector("shoot_left", "shoot_right", "shoot_up", "shoot_down");

		if (ctrlrAimDir != Vector2.Zero && !isPlayer1 &&
		    elapsed >= 0.75f / GlobalState.AttackSpeedModifier / GlobalState.SpeedModifier)
		{
			var b = GD.Load<PackedScene>("res://projectile.tscn").Instantiate() as projectile;
			Owner.AddChild(b);
			b.Transform = GlobalTransform;
			b.direction = ctrlrAimDir.Normalized();

			elapsed = 0;
			
		}
	}
}
