using System.Linq;
using Awesomegame;
using Godot;

public partial class player : character
{
	[Export]
	private bool isPlayer1 = true;

	[Export]
	protected override float Speed { get; set; } = 800;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public override void _Ready()
	{
		sprite2D = FindChild("animations") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		
		sprite2D.Play("idle");
		
		AddToGroup("players");
	}

	public override void _PhysicsProcess(double delta)
	{

		Vector2 direction = isPlayer1
			? Input.GetVector("left", "right", "up", "down")
			: Input.GetVector("left_2", "right_2", "up_2", "down_2");
		Move(direction);
	}
}
