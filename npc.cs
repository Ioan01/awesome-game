using Godot;
using System.Collections.Generic;
using Awesomegame;

public partial class npc : character
{
	[Export] protected override float Speed { get; set; } = 800;

	public override void _Ready()
	{
		AddToGroup("npcs");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Move(GetTarget().Position);
	}

	protected virtual CharacterBody2D GetTarget()
	{
		return null!;
	}
}
