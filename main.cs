using Godot;
using System;

public partial class main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private NavigationRegion2D region2D;

	public override void _EnterTree()
	{
		
	}

	public override void _Ready()
	{
		var rid = NavigationServer2D.MapCreate();
		region2D = FindChild("nav") as NavigationRegion2D;
		
		NavigationServer2D.RegionSetMap(region2D.GetRegionRid(),rid);
		


	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
