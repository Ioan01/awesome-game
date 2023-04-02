using System.Linq;
using Awesomegame;
using Godot;

public partial class enemy : npc
{
	[Export] protected override float Speed { get; set; } = 600;

	private Vector2 direction;

	private CharacterBody2D oldTarget;
	private double elapsed = 0;

	public override void _Ready()
	{
		_hp = 10;
		sprite2D = FindChild("animation") as AnimatedSprite2D;
		collision = FindChild("collision") as CollisionShape2D;
		AddToGroup("enemies");
		base._Ready();
		
		sprite2D.Play("run");



	}

	public override void _PhysicsProcess(double delta)
	{
		var targetPosition = GetTargetHelper(delta)?.Position;
		if (!targetPosition.HasValue) return;
		
		direction = (targetPosition!.Value - Position).Normalized();

		Move(direction, (float)delta);
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
		
		return targets.MaxBy(x =>
		{
			var p = x as CharacterBody2D;
			return (p.Transform.X.X - Transform.X.X) * (p.Transform.X.X - Transform.X.X) -
			       (p.Transform.Y.Y - Transform.Y.Y) * (p.Transform.Y.Y - Transform.Y.Y);
		}) as CharacterBody2D;
	}
	
	public void _on_body_entered(Node2D node)
	{
		if (node == this) return;
		if (node is not character c) return;
		
		var targets = GetTree().GetNodesInGroup("players").ToHashSet();
		var npcs = GetTree().GetNodesInGroup("npcs").ToHashSet();
		npcs.ExceptWith(GetTree().GetNodesInGroup("enemies"));
		targets.UnionWith(npcs);

		if (!targets.Contains(c)) return;
		
		c.Hp--;
		c.KnockBack(direction);
	}
}
