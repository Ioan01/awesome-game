using Godot;
using Awesomegame;

public partial class map : Node2D
{
	private GlobalState state;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_area_2d_body_entered(Node2D body)
	{
		if (state.IsDoneFighting)
		{
			// GetTree().ChangeSceneToFile
			var nextLevel = ResourceLoader.Load<PackedScene>($"res://resources/scenes/scene_{1}.tscn").Instantiate();
			AddChild(nextLevel);
		}
	}
}


