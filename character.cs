using Godot;
using System.Collections.Generic;
using Awesomegame;

public partial class character : CharacterBody2D
{
	public const float Speed = 300.0f;

	protected List<player> players = new List<player>();

	public override void _Ready()
	{
		AddToGroup("characters");
	}

	public void OnPlayerAdded(player player)
	{
		players.Add(player);
	}

	public void OnPlayerRemoved(player player)
	{
		players.Remove(player);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Zero;
		
		
		Vector2 velocity = Velocity;

		
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed * GlobalState.SpeedModifier;
			velocity.Y = direction.Y * Speed * GlobalState.SpeedModifier;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed * GlobalState.SpeedModifier);
			velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed * GlobalState.SpeedModifier);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
