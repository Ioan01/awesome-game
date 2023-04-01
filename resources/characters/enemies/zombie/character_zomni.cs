using Godot;
using System;

public partial class character_zomni : CharacterBody2D
{

	private NavigationAgent2D navigationAgent2D;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		navigationAgent2D = FindChild("navigation") as NavigationAgent2D;


		var map = NavigationServer2D.GetMaps()[0];
		navigationAgent2D.SetNavigationMap(map);


		navigationAgent2D.TargetPosition = new Vector2(100, 100);


	}

	public override void _PhysicsProcess(double delta)
	{
			
		
		
		Vector2 velocity = Velocity;

		Vector2 direction = Vector2.Zero;

		if (navigationAgent2D.GetCurrentNavigationPath().Length > 0)
			direction = navigationAgent2D.GetCurrentNavigationPath()[0];	


		GD.Print($"{direction.X} | {direction.Y}");

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Y = direction.Y * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.X = Mathf.MoveToward(Velocity.Y, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
