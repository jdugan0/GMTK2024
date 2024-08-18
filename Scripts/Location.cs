using Godot;
using System;

public partial class Location : Node2D
{
	// Called when the node enters the scene tree for the first time.
	bool updated = false;
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!updated && VirusGenerator.instance!=null){
			VirusGenerator.instance.locations.Add(this);
			VirusGenerator.instance.locationQualities.Add(this, 0);
			updated = true;
		}
	}
}
