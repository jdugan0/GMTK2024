using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class Inventory : Node
{
	public static Inventory instance;
	private List<VirusItem> viruses = new List<VirusItem>();
	[Export] VirusItem[] testItems;
	[Export] public float money;

	[Export] public PackedScene plantScene;
	public Dictionary<Vector2, PlantInfo> plants = new Dictionary<Vector2, PlantInfo>();
	private Control plantLayer;
	[Export] public PlantInfo starterPlant;
	[Export] public VirusItem starterVirusItem;
	public List<Plant> plantObj = new List<Plant>();
	Plant tableOccuplant;
	List<Vector2> positions = new List<Vector2>();
	[Export] public int quotaCount;
	public float torwardsQuota;
	public int quotaCountCurrent;
	[Export] public float quotaCap;

	// TODO plants on a canvaslayer
	public bool start = false;

	public void ConfigurePlantData(Node2D[] plantPositions, Control layer)
	{
		plantLayer = layer;
		foreach (Node2D n in plantPositions){
			positions.Add(n.Position - new Vector2(87, 131));
		}
		
	}

    public override void _Ready()
    {
		instance = this;
		
    }

    public override void _Process(double delta)
    {
		if (!start){
			AddPlant(starterPlant);
			AddVirus(starterVirusItem);
			start = true;
		}
    }
	public void SetTableOccupied(Plant plant){
		tableOccuplant = plant;
	}
	public int GetPlantNumber(){
		return plants.Values.Count;
	}
	public Vector2 GetTablePosition()
	{
		return new Vector2(800, 310);
	}
	public Plant GetTableOccuplant(){
		return tableOccuplant;
	}
	public void AddPlant(PlantInfo info){
		foreach (Vector2 p in positions){
			if (!plants.ContainsKey(p)){
				plants.Add(p, info);
				ResetVisuals();
				return;
			}
		}
		GD.PushError("PLANTS FULL");
	}
	public void SellPlants(){
		plants.Clear();
		ResetVisuals();
	}
	public void ResetVisuals(){
		for (int i = plantObj.Count-1; i >= 0; i--){
			if (IsInstanceValid(plantObj[i])){
				plantObj[i].QueueFree();
			}
			plantObj.RemoveAt(i);
		}
		foreach (Vector2 p in plants.Keys){
			Plant p1 = (Plant)plantScene.Instantiate();
			p1.info = plants[p];
			if (plants[p].onTable){
				p1.Position = GetTablePosition();
				p1.Scale = new Vector2(1.5f, 1.5f);
			}
			else{
				p1.Position = p;
				p1.Scale = new Vector2(.5f, .5f);
			}
			plantObj.Add(p1);
			plantLayer.AddChild(p1);
		}
	}
    public List<VirusItem> GetViruses()
	{
		return viruses;
	}

	public void AddVirus(VirusItem virus)
	{
		viruses.Add(virus);
	}
	public void RemoveVirus(VirusItem virus){
		viruses.Remove(virus);
	}
}
