using Godot;

namespace Awesomegame;

public partial class GlobalState : Node2D
{
	private int _gold = 0;
	
	public  bool Sane { get; set; }

	public  float SpeedModifier { get; set; } = 1f;

	public  int Gold
	{
		get => _gold;
		set
		{
			_gold = value;
			var label = GetTree().CurrentScene.FindChild("gold_l") as Label;
			label.Text = Gold.ToString();
		}
	}
}
