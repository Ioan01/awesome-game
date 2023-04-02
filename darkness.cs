using Godot;
using System;
using Awesomegame;

public partial class darkness : DirectionalLight2D
{
	private GlobalState state;

	private double elapsed = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		elapsed += delta;

		if (elapsed >= 2)
		{
			state.Darkness += 0.05f;
			elapsed = 0;
		}
		

	}
}
