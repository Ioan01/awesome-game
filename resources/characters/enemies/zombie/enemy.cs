using System.Linq;
using Awesomegame;
using Godot;

public partial class enemy : npc
{
	[Export] protected override float Speed { get; set; } = 600;

	public override void _Ready()
	{
		AddToGroup("enemies");
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		var targetPosition = GetTarget().Position;

		Move((targetPosition - Position).Normalized());
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
			return (p.Position.X - Position.X) * (p.Position.X - Position.X) -
			       (p.Position.Y - Position.Y) * (p.Position.Y - Position.Y);
		}) as CharacterBody2D;
	}
}
