using System.Linq;
using Awesomegame;
using Godot;

public partial class enemy : npc
{
	[Export] protected override float Speed { get; set; } = 600;

	private CharacterBody2D oldTarget;
	private double elapsed = 0;

	public override void _Ready()
	{
		sprite2D = FindChild("animations") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		AddToGroup("enemies");
		base._Ready();

		var nodes = GetTree().GetNodesInGroup("players");

	}

	public override void _PhysicsProcess(double delta)
	{
		var targetPosition = GetTargetHelper(delta)?.Position;

		if (targetPosition.HasValue)
			Move((targetPosition - Position).Value.Normalized(), (float)delta);
	}

	private CharacterBody2D GetTargetHelper(double delta)
	{
		elapsed += delta;
		
		if (oldTarget == null || elapsed >= 2)
		{
			oldTarget = GetTarget();
			elapsed = 0;
		}

		return oldTarget;
	}

	protected override CharacterBody2D GetTarget()
	{
		var targets = GetTree().GetNodesInGroup("players").ToHashSet();
		var npcs = GetTree().GetNodesInGroup("npcs").ToHashSet();
		npcs.ExceptWith(GetTree().GetNodesInGroup("enemies"));
		targets.UnionWith(npcs);
		
		return targets.Where(x =>
		{
			var p = x as character;
			return !p.IsDead;
		}).MaxBy(x =>
		{
			var p = x as CharacterBody2D;
			return (p.Transform.X.X - Transform.X.X) * (p.Transform.X.X - Transform.X.X) -
			       (p.Transform.Y.Y - Transform.Y.Y) * (p.Transform.Y.Y - Transform.Y.Y);
		}) as CharacterBody2D;
	}
}
