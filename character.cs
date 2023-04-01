using Godot;
using System;
using System.Collections.Generic;
using Awesomegame;

public partial class character : CharacterBody2D
{
	public const float Speed = 300.0f;

	protected List<player> players = new List<player>();
	// Get the gravity from the project settings to be synced with RigidBody nodes.

	public void OnPlayerAdded(player player)
	{
		players.Add(player);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Zero;
		
		
		Vector2 velocity = Velocity;

		
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed * GlobalState.SpeedModifier;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * GlobalState.SpeedModifier);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
