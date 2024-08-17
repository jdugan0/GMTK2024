using Godot;
using System;
using System.Collections.Generic;

public partial class EnemySource : Node2D
{
	[Export] public float sourceTime;
	[Export] public float range;
	[Export] public float difScaleTime;
	[Export] public float difScaleDist;
	[Export(PropertyHint.Range, "-1.0,1.0,0.000000001")] public float difScaleAmount;
	float totalTime = 0f;
	[Export] int amount;
	[Export] public PackedScene virusType;
	float time;

    public override void _Ready()
    {
        time = sourceTime;
    }
    public override void _Process(double delta)
    {
		totalTime += (float)delta;
        if (time > 0){
			time -= (float)delta;
		}
		else if (GetViewport().GetCamera2D().Position.DistanceTo(Position) < (1000 + totalTime * difScaleDist)){
			VirusGenerator.instance.CreateBoid(virusType, Position, amount + (int)(totalTime*difScaleAmount));
			time = sourceTime + (float)GD.RandRange(-range/2, range/2) - totalTime * difScaleTime;
		}
    }
}
