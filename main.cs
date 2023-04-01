using Godot;
using System;

public partial class main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private NavigationRegion2D region2D;

	public override void _EnterTree()
	{
		
	}

	public override void _Ready()
	{
		foreach (var node in GetTree().GetNodesInGroup("npcs"))
		{
			(node as character).OnPlayerAdded(FindChild("player") as player);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
