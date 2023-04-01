using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Godot;

public partial class player : CharacterBody2D
{
	private AnimatedSprite2D sprite2D = null!;

	private CollisionShape2D collision = null!;

	private bool isFlipped;

	private bool wasFlipped;

	private bool isFacingRight = true;

	[Export]
	private bool isPlayer1 = true;
	
	
	[Export]
	public float Speed { get; set; }= 800.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public override void _Ready()
	{
		sprite2D = FindChild("animations") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		
		sprite2D.Play("idle");
		
		
		AddToGroup("players");


		var enemies = GetTree().GetNodesInGroup("npcs").ToList();
		
		enemies.ForEach(e =>
		{
			(e as character).OnPlayerAdded(this);
		});

	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = isPlayer1 ? Input.GetVector("left", "right", "up", "down") : Vector2.Zero;
		
		
		
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
			
			sprite2D.Play("run");
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			
			sprite2D.Play("idle");

		}
		
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
