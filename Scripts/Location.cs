using Godot;
using System;
using System.Collections.Generic;

public partial class Location : Node2D
{
	// Called when the node enters the scene tree for the first time.
	bool updated = false;
	public enum LocationType{
		Leaf,
		Stem,
		Root,
		Flower
	}
	public static Dictionary<LocationType, float> valueDict = new Dictionary<LocationType, float>(){
		{LocationType.Leaf, 0.75f},
		{LocationType.Stem, 1.5f},
		{LocationType.Root, 2f},
		{LocationType.Flower, 3f}
	};
	[Export] public LocationType type;
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!updated && VirusGenerator.instance!=null){
			VirusGenerator.instance.locations.Add(this);
			VirusGenerator.instance.locationQualities.Add(type, 0);
			updated = true;
		}
	}
}
