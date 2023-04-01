using Godot;
using System;
using Awesomegame;

public partial class item : Area2D
{
	[Export] public int Price { get; set; } = 0;
	[Export] public string Name { get; set; }
	
	// Called when the node enters the scene tree for the first time.
	protected GlobalState state;
	
	public override void _Ready()
	{
		state = GetTree().CurrentScene.FindChild("globals") as GlobalState;
		
		var sprite2D = FindChild("animation") as AnimatedSprite2D;
		
		sprite2D.Play("default");

		BodyEntered += body =>
		{
			if (Price > 0)
			{
				(GetTree().CurrentScene.FindChild("price") as Label).Visible = true;
				(GetTree().CurrentScene.FindChild("price") as Label).Text = $"{Name} ${Price}";

				
				state.hoveredItem = this;
			}
			OnEnter(body);
		};

		BodyExited += body =>
		{
			if (Price > 0)
			{
				(GetTree().CurrentScene.FindChild("price") as Label).Visible = false;
			}
		};


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public virtual void OnEnter(Node node)
	{
		
	}

	public virtual void OnBuy()
	{
		
	}
}
