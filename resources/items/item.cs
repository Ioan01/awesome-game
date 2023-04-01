using Godot;
using System;
using Awesomegame;

public partial class item : Area2D	
{
	// Called when the node enters the scene tree for the first time.
	protected GlobalState state;
	
	public override void _Ready()
	{
		state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
		
		var sprite2D = FindChild("animation") as AnimatedSprite2D;
		
		
		sprite2D.Play("default");

		BodyEntered += OnEnter;
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public virtual void OnEnter(Node node)
	{
		
	}
}
