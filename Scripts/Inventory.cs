using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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
	[Export] PlantInfo starterPlant;
	[Export] VirusItem starterVirusItem;
	// TODO plants on a canvaslayer
	bool start = false;


	public static void TransferPlantInfo()
	{
		for (int i = 0; i < 15; i++)
		{
			if (plants[i] != null)
			{
				VirusDataTransfer.AddPlantInfo(plants[i].GetPlantInfo());
			}
		}
	}

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
			plantNumber++;
		}
	}

	public void RefreshPlant(PlantInfo info)
	{
		Plant plant = (Plant) plantScene.Instantiate();
		info.plant = plant;
		plant.SetInfo(info);
		plant.SetPositionPreset(plantPositions[info.inventoryIndex]);
		plants[info.inventoryIndex] = plant;
		plantLayer.AddChild(plant);
		plantNumber++;
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

	public static void RefreshVisuals()
	{
		for (int i = 0; i < 16; i++)
		{
			plants[i] = null;
		}
		foreach (PlantInfo info in VirusDataTransfer.GetPlantInfo()){
			instance.RefreshPlant(info);
			if (info.onTable){
				SetTableOccupied(info.plant);
			}
		}
	}

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
