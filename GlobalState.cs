using Godot;

namespace Awesomegame;

public partial class GlobalState : Node2D
{
	private static int maxHp = 5;

	public item hoveredItem { get; set; }= null;

	public int Enemeies { get; set; } = 10;
	
	private int _gold = 20;
	private float _darkness;
	private int _player1hp = maxHp;
	private int _player2hp = maxHp;

	public bool Sane { get; set; }

	public bool IsDoneFighting = false;

	public int CurrentLevel = 0;

	public int Player1Hearts
	{
		get => _player1hp;
		set
		{
			_player1hp = value;
			if (_player1hp <= 0)
			{
				_player1hp = 0;
			}

			(GetTree().CurrentScene.FindChild("hp1_l") as Label).Text = _player1hp.ToString();

		}
	}
	public int Player2Hearts
	{
		get => _player2hp;
		set
		{
			_player2hp = value;
			if (_player2hp <= 0)
			{
				_player2hp = 0;
			}

			(GetTree().CurrentScene.FindChild("hp2_l") as Label).Text = _player2hp.ToString();

		}
	}

	public static float SizeModifier { get; set; } = 1f;

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
			else if (value < 0)
				_darkness = 0;
			
			foreach (player node in GetTree().GetNodesInGroup("players"))
			{
				node.setIntensity(_darkness);
			}



			var light = GetTree().CurrentScene.FindChild("darkness") as DirectionalLight2D;
			light.Energy = _darkness;
		}
	}

	public int Wave { get; set; }
}
