using Godot;
using System;
using System.Collections.Generic;

public partial class VirusGenerator : Node
{
	[Export] private int virusAmount;
	[Export] private PackedScene virusScene;
	[Export] private PackedScene virusScene2;
	public List<VirusBoid> boids = new List<VirusBoid>();
	[Export] float size;

	public static VirusGenerator instance;

	private Vector2 mousePos;
	public List<Location> locations = new List<Location>();

	public Dictionary<Location, float> locationQualities = new Dictionary<Location, float>();

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		instance = this;
		CreateBoid(virusScene, new Vector2(), virusAmount);
		// CreateBoid(virusScene2, new Vector2(), virusAmount/2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (Input.IsActionPressed("Click")){
			mousePos = GetViewport().GetCamera2D().GetGlobalMousePosition();
		}
		Vector2 comCamera = new Vector2();
		int cCam = 0;
		foreach (VirusBoid boid in boids){
			Vector2 accel = new Vector2();
			Dictionary<String, Vector2> com = new Dictionary<String, Vector2>();
			if (boid.player){
				comCamera += boid.Position;
				cCam++;
			}
			Dictionary<String, Vector2> com_v = new Dictionary<String, Vector2>();
			Dictionary<String, Vector2> sep = new Dictionary<String, Vector2>();
			Dictionary<String, int> count = new Dictionary<String, int>();
			foreach (VirusBoid b in boids){
				if (b != boid && b.Position.DistanceTo(boid.Position) <= boid.viewRange){
					if (!com.ContainsKey(b.name)){
						com.Add(b.name, b.Position);
					}
					else{
						com[b.name] += b.Position;
					}
					if (!com_v.ContainsKey(b.name)){
						com_v.Add(b.name, b.velocity);
					}
					else{
						com_v[b.name] += b.velocity;
					}
					
					if (!count.ContainsKey(b.name)){
						count.Add(b.name, 1);
					}
					else{
						count[b.name]++;
					}
				}
				if (b != boid && b.Position.DistanceTo(boid.Position) <= getValueFromParam(boid.seperationDistanceDict, b.name, boid.name)){
					if (!sep.ContainsKey(b.name)){
						sep.Add(b.name, (b.Position - boid.Position)* getValueFromParam(boid.seperationDict, b.name, boid.name));
					}
					else{
						sep[b.name] += (b.Position - boid.Position)* getValueFromParam(boid.seperationDict, b.name, boid.name);
					}
				}
			}	
			foreach (String v in com.Keys){
				if (count[v] > 0){
					accel += getValueFromParam(boid.coherenceDict, v, boid.name) * (com[v] - boid.Position) / count[v];
				}
			}
			
			//calc coherence
			
			// calc seperation
			foreach (String v in sep.Keys){
				accel -= sep[v];
			}

			//calc alignment
			foreach (String v in com_v.Keys){
				if (count[v] > 0){
					accel += getValueFromParam(boid.alignmentDict, v, boid.name) * (com_v[v] - boid.velocity) / count[v];
				}
			}
			

			//calc mouse force
			accel += (mousePos - boid.Position) * boid.mouseForce;

			// clamp acceleration
			// if (Math.Abs(accel.Length()) > boid.maxAccel){
			// 	accel = accel.Normalized() * boid.maxAccel;
			// }

			// set velocity
			boid.velocity += accel * (float)delta;

			// clamp velocity
			if (Math.Abs(boid.velocity.Length()) > boid.maxVelocity){
				boid.velocity = boid.velocity.Normalized() * boid.maxVelocity;
			}

			if ((boid.Position.DistanceTo(mousePos) < 22 || Input.IsActionPressed("RightClick")) && boid.mouseForce > 0){
				boid.velocity = new Vector2();
			}
			
			
			// boid.LookAt(boid.GlobalPosition + boid.velocity, Vector3.Up);

			// boid.Position += boid.velocity * (float)delta;


		}
		comCamera = comCamera / cCam;
		GetViewport().GetCamera2D().Position = comCamera;
	}
	public void CreateBoid(PackedScene boidType, Vector2 pos, int amount){
		for (int i = 0; i < amount; i++){
			//generate virus
			VirusBoid virus = (VirusBoid)boidType.Instantiate();
			virus.Position = new Vector2((float)GD.RandRange(-size/2, size/2),(float)GD.RandRange(-size/2, size/2)) + pos;
			boids.Add(virus);
			virus.velocity = new Vector2();
			
			Vector2 velocity = new Vector2((float)GD.RandRange(-1f,1f),(float)GD.RandRange(-1f,1f));
			velocity = velocity.Normalized();
			velocity = velocity * (float)GD.RandRange(-virus.velocityRange/2, virus.velocityRange/2);
			virus.velocity = velocity;
			virus.generator = this;
			AddChild(virus);
		}
	}
	public float getValueFromParam(Dictionary<String, float> dict, String key, String def){
		if (dict.ContainsKey(key)){
			return dict[key];
		}
		return dict[def];
	}
}
