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
	[Export] public VirusParameter[] targeting;
	public Dictionary<String,float> coherenceDict = new Dictionary<string, float>();
	public Dictionary<String,float> seperationDict = new Dictionary<string, float>();
	public Dictionary<String,float> seperationDistanceDict = new Dictionary<string, float>();
	public Dictionary<String,float> alignmentDict = new Dictionary<string, float>();
	public Dictionary<String,float> targetingDict = new Dictionary<string, float>();
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
	[Export] public bool selected;
	bool justEntered = false;
	[Export] Sprite2D sprite2D;
	[Export] float abilityTime;
	float abilityTimer = 0;
	[Export] Texture2D rootedTexture;
	bool abilityEnded = true;
	[Export] public float abilityCooldown;
	public float abilityCooldownTimer;

	public enum AbilityType{
		None,
		Dash,
		Explode,
		Sacrafice
	}
	[Export] public AbilityType ability = AbilityType.None;
	public Location rootable;
	bool rooted = false;
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
		foreach(VirusParameter p in targeting){
			targetingDict.Add(p.name, p.value);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (selected && Input.IsActionJustPressed("Ability") && abilityCooldownTimer <= 0){
			switch (ability){
				case AbilityType.None:
				 	break;
				case AbilityType.Dash:
					maxVelocity*=2;
					mouseForce *=2;
					abilityTimer = abilityTime;
					abilityEnded = false;
					abilityCooldownTimer = abilityCooldown;
				    break;
				case AbilityType.Explode:
					abilityCooldownTimer = abilityCooldown * 3;
					abilityEnded = false;
					List<VirusBoid> toRemove = new List<VirusBoid>();
					foreach (VirusBoid b in generator.boids){
						if (b.Position.DistanceTo(Position) <= 500 && b.name == "V2"){
							toRemove.Add(b);
						}
					}
					foreach (VirusBoid boid in toRemove){
						generator.boids.Remove(boid);
						boid.QueueFree();
					}
				break;
				case AbilityType.Sacrafice:
					abilityCooldownTimer = abilityCooldown * 3;
					abilityEnded = false;
					foreach (VirusBoid b in generator.boids){
						if (b.Position.DistanceTo(Position) <= 500 && b.name == "V1"){
							generator.boids.Remove(this);
							b.health += 3;
						}
					}
					QueueFree();
				break;
			}
		}
		if (abilityCooldownTimer > 0){
			abilityCooldownTimer -= (float)delta;
		}
		else{

		}
		if (abilityTimer > 0f){
			abilityTimer -= (float)delta;
		}
		else{
			if (!abilityEnded){
				abilityEnded = true;
				switch (ability){
					case AbilityType.None:
						break;
					case AbilityType.Dash:
						maxVelocity/=2;
						mouseForce/=2;
						break;
			}
			}
		}
		if (Input.IsActionPressed("RightClick") && justEntered && !rooted){
			selected = !selected;
			justEntered = false;
			if (selected){
				Modulate = Colors.Blue;
			}
			else{
				Modulate = Colors.White;
			}
		}
		rootable = null;
		foreach (Location l in VirusGenerator.instance.locations){
				if (l.Position.DistanceTo(Position) < 280.7){
					rootable = l;
					break;
				}
		}
		
		if (Input.IsActionJustPressed("Root") && selected && !rooted && rootable != null){
		
					AudioManager.instance.PlaySFX(this, "Root");
					rooted = true;
					selected = false;
					generator.locationQualities[rootable.type] += health;
					sprite2D.Texture = rootedTexture;
					Modulate = Colors.White;
					Freeze = true;
					generator.boids.Remove(this);
		}
		if (time > 0){
			time -= (float)delta;
		}
		if (!rooted){
			LinearVelocity = velocity;
		}
		else{
			LinearVelocity = new Vector2();
		}
		if (health <= 0){
			generator.boids.Remove(this);
			QueueFree();
		}
		if (col != null && time <= 0){
			if (validDamageTypes.Contains(col.name)){
				time = damageTime;
				col.health -= damage;
				AudioManager.instance.PlaySFX(this,"Damage");
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

	public void MouseEnteredLogic(){
		// GD.Print("qwefd");
		// generator.rightClickLabel.Visible = true;
		// generator.rightClickLabel.Position = generator.rightClickLabel.GetLocalMousePosition();
		if (player && !justEntered && !rooted){
			justEntered = true;
			Modulate = new Color(Colors.Blue.R, Colors.Blue.G, Colors.Blue.B, 0.5f);
			// GD.Print(selected);
			// selected = !selected;
		}
	}
	public void MouseExitedLogic(){
		// generator.rightClickLabel.Visible = false;
		if (player){
			justEntered = false;
			if (selected){
				Modulate = Colors.Blue;
			}
			else{
				Modulate = Colors.White;
			}
		}
	}
	
}
