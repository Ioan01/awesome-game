using Godot;
using System;

public partial class death : CpuParticles2D
{

	private double count;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		count += delta;
		
		if (count >= Lifetime)
			QueueFree();
	}
}
