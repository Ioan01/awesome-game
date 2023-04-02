using Godot;
using System;
using Awesomegame;

public partial class size : item
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Price = 30;
		this.Name = "Projectile size";
		base._Ready();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public override void OnBuy(Node player)
	{
		QueueFree();
		GlobalState.SizeModifier += 0.2f;
	}
}
