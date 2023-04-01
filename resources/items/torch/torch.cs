using Godot;
using System;

public partial class torch : AnimatedSprite2D
{

	private float baseScale = 0.2f;
	private PointLight2D light2D;

	private double delay = 0;

	private double accumulator = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		light2D = FindChild("light") as PointLight2D;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		delay += delta;
		accumulator += delta;
		if (delay > 0.05)
		{
			light2D.TextureScale = baseScale + 0.1f * (float)Mathf.Cos(accumulator*2);
			delay = 0;
		}

	}
}
