using System.Linq;
using Godot;

public partial class enemy : npc
{
	[Export] protected override float Speed { get; set; } = 600;

	private CharacterBody2D oldTarget;
	private double elapsed = 0;

	public override void _Ready()
	{
		AddToGroup("enemies");
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		var targetPosition = GetTargetHelper(delta).Position;

		Move((targetPosition - Position).Normalized());
	}

	private CharacterBody2D GetTargetHelper(double delta)
	{
		if (oldTarget == null || elapsed >= 2)
		{
			oldTarget = GetTarget();
			elapsed = 0;
		}

		elapsed += delta;

		return oldTarget;
	}

	protected override CharacterBody2D GetTarget()
	{
		var targets = GetTree().GetNodesInGroup("players").ToHashSet();
		var npcs = GetTree().GetNodesInGroup("npcs").ToHashSet();
		npcs.ExceptWith(GetTree().GetNodesInGroup("enemies"));
		targets.UnionWith(npcs);
		
		return targets.MaxBy(x =>
		{
			var p = x as CharacterBody2D;
			return (p.Transform.X.X - Transform.X.X) * (p.Transform.X.X - Transform.X.X) -
			       (p.Transform.Y.Y - Transform.Y.Y) * (p.Transform.Y.Y - Transform.Y.Y);
		}) as CharacterBody2D;
	}
}
