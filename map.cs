using Godot;
using Awesomegame;
using System;

public partial class map : Node2D
{
	private GlobalState state;
	private int numberOfMaps = 2;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
		GD.Print("Loaded");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (body.GetClass() != "TileMap")
		{
			var level = getRandomLevel();
			var nextLevel = ResourceLoader.Load<PackedScene>($"res://resources/scenes/scene_{level}.tscn").Instantiate();
			GetParent().AddChild(nextLevel);
			QueueFree();
		}
	}

	private int getRandomLevel()
	{
		Random rnd = new Random();
		int randomLevel = rnd.Next(1, numberOfMaps + 1);
		while (state.CurrentLevel == randomLevel)
		{
			randomLevel = rnd.Next(1, numberOfMaps + 1);
		}
		state.CurrentLevel = randomLevel;
		return randomLevel;
	}

}




