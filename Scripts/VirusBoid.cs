using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class VirusBoid : RigidBody2D	
{
	public Vector2 velocity;
	[Export] public VirusParameter[] coherence;
	[Export] public VirusParameter[] seperation;
	[Export] public VirusParameter[] seperationDistance;
	[Export] public VirusParameter[] alignment;
	public Dictionary<String,float> coherenceDict = new Dictionary<string, float>();
	public Dictionary<String,float> seperationDict = new Dictionary<string, float>();
	public Dictionary<String,float> seperationDistanceDict = new Dictionary<string, float>();
	public Dictionary<String,float> alignmentDict = new Dictionary<string, float>();
	[Export] public String[] validDamageTypes;
	[Export] public float mouseForce;
	[Export] public float velocityRange;

	[Export] public float maxVelocity;

	[Export] public float viewRange;

	[Export] public float maxAccel;

	[Export] public float damageTime = 3f;
	private float time = 0f;
	[Export] public float health;
	[Export] public float damage;
	public VirusGenerator generator;
	[Export] public String name;
	VirusBoid col;
	[Export] public bool player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (VirusParameter p in coherence){
			coherenceDict.Add(p.name, p.value);
		}
		foreach (VirusParameter p in alignment){
			alignmentDict.Add(p.name, p.value);
		}
		foreach (VirusParameter p in seperation){
			seperationDict.Add(p.name, p.value);
		}
		foreach (VirusParameter p in seperationDistance){
			seperationDistanceDict.Add(p.name, p.value);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (time > 0){
			time -= (float)delta;
		}
		LinearVelocity = velocity;
		if (health <= 0){
			
			generator.boids.Remove(this);
			QueueFree();
		}
		if (col != null && time <= 0){
			if (validDamageTypes.Contains(col.name)){
				time = damageTime;
				col.health -= damage;
			}
		}
	}

	public void DealDamage(Node node){
		var n = node as VirusBoid;
		if (n != null){
			col = n;
		}
	}
	public void Exited(Node node){
		if (node as VirusBoid == col){
			col = null;
		}
	}
	
}
