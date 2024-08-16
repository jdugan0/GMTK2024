using Godot;
using System;
using System.Collections.Generic;

public partial class VirusGenerator : Node
{
	[Export] private int virusAmount;
	[Export] private PackedScene virusScene;
	List<VirusBoid> boids = new List<VirusBoid>();
	[Export] float coherence;
	[Export] float seperation;
	[Export] float seperationDistance;
	[Export] float alignment;
	[Export] float mouseForce;
	[Export] float velocityRange;

	[Export] float maxVelocity;

	[Export] float viewRange;

	[Export] float maxAccel;

	[Export] float size;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < virusAmount; i++){
			//generate virus
			VirusBoid virus = (VirusBoid)virusScene.Instantiate();
			virus.Position = new Vector2((float)GD.RandRange(-size/2, size/2),(float)GD.RandRange(-size/2, size/2));
			boids.Add(virus);
			virus.velocity = new Vector2();
			
			Vector2 velocity = new Vector2((float)GD.RandRange(-1f,1f),(float)GD.RandRange(-1f,1f));
			velocity = velocity.Normalized();
			velocity = velocity * (float)GD.RandRange(-velocityRange/2, velocityRange/2);
			virus.velocity = velocity;
			AddChild(virus);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (VirusBoid boid in boids){
			Vector2 accel = new Vector2();
			Vector2 com = new Vector2();
			Vector2 com_v = new Vector2();
			Vector2 sep = new Vector2();
			int c = 0;
			foreach (VirusBoid b in boids){
				if (b != boid && b.Position.DistanceSquaredTo(boid.Position) <= viewRange){
					com += b.Position;
					com_v += b.velocity;
					c++;
				}
				if (b != boid && b.Position.DistanceSquaredTo(boid.Position) <= seperationDistance){
					sep += (b.Position - boid.Position);
				}
			}	
			if (c > 0){
				com /= c;
				com_v /= c;
			}
			//calc coherence
			accel += (com - boid.Position) * coherence;

			// calc seperation
			accel -= sep * seperation;

			//calc alignment
			accel += (com_v - boid.velocity) * alignment;

			//calc mouse force
			accel += (GetViewport().GetCamera2D().GetGlobalMousePosition() - boid.Position) * mouseForce;

			// clamp acceleration
			if (Math.Abs(accel.Length()) > maxAccel){
				accel = accel.Normalized() * maxAccel;
			}

			// set velocity
			boid.velocity += accel;

			// clamp velocity
			if (Math.Abs(boid.velocity.Length()) > maxVelocity){
				boid.velocity = boid.velocity.Normalized() * maxVelocity;
			}
			
			
			// boid.LookAt(boid.GlobalPosition + boid.velocity, Vector3.Up);

			boid.Position += boid.velocity * (float)delta;
		}
	}
}
