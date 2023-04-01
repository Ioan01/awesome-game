using Godot;

namespace Awesomegame;

public partial class GlobalState : Node2D
{
	private int _gold = 0;
	private float _darkness;

	public  bool Sane { get; set; }


	public static float SpeedModifier { get; set; } = 1f;

	public static float AttackSpeedModifier { get; set; } = 1f;
	
	public int Gold
	{
		get => _gold;
		set
		{
			_gold = value;
			var label = GetTree().CurrentScene.FindChild("gold_l") as Label;
			label.Text = Gold.ToString();
		}
	}

	public float Darkness
	{
		get => _darkness;
		set
		{
			_darkness = value;
			if (value > 0.9f)
				_darkness = 0.9f;
			
			foreach (player node in GetTree().GetNodesInGroup("players"))
			{
				node.setIntensity(_darkness);
			}
			
			
			
			var light = GetTree().CurrentScene.FindChild("darkness") as DirectionalLight2D;
			light.Energy = _darkness;
		}
	}
}
