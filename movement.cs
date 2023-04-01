using Godot;

public partial class movement : CharacterBody2D
{
	private AnimatedSprite2D sprite2D = null!;

	private CollisionShape2D collision = null!;

	private bool isFlipped;

	private bool wasFlipped;

	private bool isFacingRight = true;
	
	
	[Export]
	public float Speed { get; set; }= 800.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public override void _Ready()
	{
		sprite2D = FindChild("animations") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		
		sprite2D.Play("idle");
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
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
