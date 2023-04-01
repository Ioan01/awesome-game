using Godot;
using System;
using Awesomegame;

public partial class coin : item
{
	// Called when the node enters the scene tree for the first time.
	public override void OnEnter(Node node)
	{
		if (node.GetGroups().Contains("players"))
		{
			state.Gold += 1;
			QueueFree();
		}
	}
}
