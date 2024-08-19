using Godot;
using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

public partial class ArrowPoint : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = GetViewport().GetCamera2D().Position + new Vector2(0, 200);
		Node2D distanceObj = null;
		float distance = float.MaxValue;
		foreach(Node2D n in VirusGenerator.instance.locations){
			if (n.Position.DistanceSquaredTo(Position) < distance){
				distance = n.Position.DistanceSquaredTo(Position);
				distanceObj = n;
			}
		}
		LookAt(distanceObj.Position);
		Rotate(Mathf.Pi);
	}
}
