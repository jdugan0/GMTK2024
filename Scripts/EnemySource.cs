using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySource : Node2D
{
	[Export] public float sourceTime;
	[Export] public float range;
	[Export] int amount;
	[Export] public PackedScene virusType;
	float time;

    public override void _Ready()
    {
        time = sourceTime;
    }
    public override void _Process(double delta)
    {
        if (time > 0){
			time -= (float)delta;
		}
		else{
			VirusGenerator.instance.CreateBoid(virusType, Position, amount);
			time = sourceTime + (float)GD.RandRange(-range/2, range/2);
		}
    }
}
