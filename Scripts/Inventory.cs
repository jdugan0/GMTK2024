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
	private int plantNumber = 0;
	private static CanvasLayer plantLayer;
	// TODO plants on a canvaslayer

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

	private List<Plant> GetPlantsPresent()
	{
		List<Plant> presentPlants = new List<Plant>();
		for (int i = 0; i < 15; i++)
		{
			if (plants[i] != null)
			{
				presentPlants.Add(plants[i]);
			}
		}
		foreach (Plant plant in presentPlants)
		{
			GD.Print(plant);
		}
		return presentPlants;
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
			plantNumber++;
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

	public int GetPlantNumber()
	{
		return plantNumber;
	}

	public void RemovePlant(int index)
	{
		foreach (Plant plant in GetPlantsPresent())
		{
			if (plant.GetPlantInfo().inventoryIndex == index)
			{
				if (plants[15] != null)
				{
					if (plants[15] == plants[index])
					{
						plants[index].QueueFree();
						plants[15].QueueFree();
						plants[15] = null;
					}
					else
					{
						plants[index].QueueFree();
					}
				}
				else
				{
					plants[index].QueueFree();
				}
				plants[index] = null;
				break;
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
