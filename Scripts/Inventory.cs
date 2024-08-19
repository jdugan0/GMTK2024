using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Inventory instance;
	private List<VirusItem> viruses = new List<VirusItem>();
	[Export] VirusItem[] testItems;
	[Export] public int money;

	public static Node2D[] plantPositions = new Node2D[15];
	[Export] public PackedScene plantScene;
	private static Plant[] plants = new Plant[16];
	private int plantNumber;
	private static CanvasLayer plantLayer;
	[Export] PlantInfo starterPlant;
	[Export] VirusItem starterVirusItem;
	// TODO plants on a canvaslayer
	bool start = false;

	public static void ConfigurePlantData(Node2D[] plantPositions, CanvasLayer layer)
	{
		Inventory.plantPositions = plantPositions;
		plantLayer = layer;
	}

    public override void _Ready()
    {
		instance = this;
		for (int i = 0; i < testItems.Length; i++){
			Inventory.instance.AddVirus(testItems[i]);
		}
    }

    public override void _Process(double delta)
    {
        if (!start){
			start = true;
			AddPlant(starterPlant);
			AddVirus(starterVirusItem);
		}
    }

    public void AddPlant(PlantInfo info)
	{
		int plantIndex = GetFreePlantIndex();
		if (plantIndex != -1)
		{
			Plant plant = (Plant) plantScene.Instantiate();
			info.plant = plant;
			info.inventoryIndex = plantIndex;
			plant.SetInfo(info);
			plant.SetPositionPreset(plantPositions[plantIndex]);
			plants[plantIndex] = plant;
			plantLayer.AddChild(plant);
		}
		
	}

	private int GetFreePlantIndex()
	{
		for (int i = 0; i < 15; i++)
		{
			if (plants[i] == null)
			{
				return i;
			}
		}
		return -1;
	}

	public static Vector2 GetTablePosition()
	{
		return new Vector2(800, 310);
	}

	public static void SetTableOccupied(Plant occuplant)
	{
		plants[15] = occuplant;
	}

	public static void SetTableFree()
	{
		plants[15] = null;
	}

	public static bool GetTableOccupied()
	{
		return plants[15] != null;
	}

	public static Plant GetTableOccuplant()
	{
		return plants[15];
	}

	// public void RefreshVisuals()
	// {
	// 	foreach (PlantInfo info in plantInfos){
	// 		AddPlant(info);
	// 		if (info.onTable){
	// 			PlantLayer.SetTableOccupied(info.plant);
	// 		}
	// 	}
	// }

	public List<PlantInfo> GetPlantInfos()
	{
		List<PlantInfo> infos = new List<PlantInfo>();
		for (int i = 0; i < 15; i++)
		{
			if (plants[i] != null)
			{
				infos.Add(plants[i].GetPlantInfo());
			}
		}
		return infos;
	}

	public void RemovePlant(int index)
	{
		foreach (Plant plant in plants)
		{
			if (plant.GetPlantInfo().inventoryIndex == index && plants[index] != null)
			{
				plants[index] = null;
			}
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
