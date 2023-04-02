using Godot;
using Awesomegame;
using System;
using System.Linq;

public partial class map : Node2D
{
	
	
	private static string[] enemies = new[]
	{
		"res://resources/characters/enemies/demon/demon.tscn",
		"res://resources/characters/enemies/imp/imp.tscn",
		"res://resources/characters/enemies/zombie/character_zomni.tscn",
		"res://resources/characters/enemies/necromancers/necromancer.tscn"
	};

	private static string[] items = new[]
	{
		"res://resources/items/potions/health.tscn",
		"res://resources/items/potions/size.tscn",
		"res://resources/items/candle.tscn",
		"res://resources/items/coin/coin.tscn"
	};
	
	
	private GlobalState state;

	private Label wave;
	private int numberOfMaps = 2;
	private double elapsed;

	private void spawnItems()
	{
		var spawn = FindChild("items");

		if (spawn != null)
		{
			var positions = spawn.GetChildren().Select(node => (node as Node2D).GlobalPosition).ToList();
			for (int i = 0; i < Random.Shared.Next(5); i++)
			{
				var enemy = GD.Load<PackedScene>(items[Random.Shared.Next(items.Length)]).Instantiate() as item;
				enemy.GlobalPosition = positions[Random.Shared.Next(positions.Count)];
				AddChild(enemy);
				
			}
		}
	}
	
	private void spawn()
	{
		var spawn = FindChild("spawner");

		if (spawn != null)
		{
			var positions = spawn.GetChildren().Select(node => (node as Node2D).GlobalPosition).ToList();
			for (int i = 0; i < state.Enemeies; i++)
			{
				var enemy = GD.Load<PackedScene>(enemies[Random.Shared.Next(enemies.Length)]).Instantiate() as enemy;
				enemy.GlobalPosition = positions[Random.Shared.Next(positions.Count)];
				enemy.AddToGroup("enemies");
				AddChild(enemy);
				
			}
		}
	}
	// Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
        state.Enemeies = 5;

        GD.Print("Loaded");


		

		wave = GetTree().CurrentScene.FindChild("wave") as Label;
		spawn();
		spawnItems();
		
		
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
	    elapsed += delta;

	    if (elapsed >= 20)
	    {
		    state.Wave += 1;
		    wave.Text = $"Wave {state.Wave}";
		    elapsed = 0;
		    state.Enemeies = (int)(state.Enemeies * 1.25);
		    spawn();
		    spawnItems();
	    }
    }

	private void _on_area_2d_body_entered(Node2D body)
	{
		// if (GetTree().GetNodesInGroup("enemies").Count == 0)
		// 	if (body is player)
		// 	{
		// 		var level = getRandomLevel();
		// 		var nextLevel = ResourceLoader.Load<PackedScene>($"res://resources/scenes/scene_{level}.tscn").Instantiate();
		// 		GetParent().AddChild(nextLevel);
		// 		QueueFree();
		// 		
		// 		foreach (var node in GetTree().GetNodesInGroup("players"))
		// 		{
		// 			node.RemoveFromGroup("players");
		// 		}
		// 		
		// 		foreach (var node in GetTree().GetNodesInGroup("enemies"))
		// 		{
		// 			node.RemoveFromGroup("enemies");
		// 			node.RemoveFromGroup("npcs");
		// 		}
		//
		// 		state.Enemeies += 5;
		// 	}
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




