using Godot;
using System;
using System.Collections.Generic;
using Awesomegame;

public partial class trap : AnimatedSprite2D
{
	private Area2D area2D;

	private List<character> characters = new List<character>();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Play("default");

		FrameChanged += () =>
		{
			if (Frame == 3)
				characters.ForEach(character => character.Hp -= 2);
		};
		area2D = FindChild("area") as Area2D;
		area2D.BodyEntered += body =>
		{
				if (body is character)
				{
					if (!characters.Contains(body as character)) 
						characters.Add(body as character);
				}
		};
		
		area2D.BodyExited += body =>
		{
			if (body is character)
			{
				if (characters.Contains(body as character)) 
					characters.Remove(body as character);
			}
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
