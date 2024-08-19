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
	public List<PlantInfo> plantInfos = new List<PlantInfo>();
	public int plantNumber = 0;
	private static CanvasLayer plantLayer;
	[Export] public PlantInfo starterPlant;
	[Export] public VirusItem starterVirusItem;
	// TODO plants on a canvaslayer
	public bool start = false;

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
    }

    public void AddPlant(PlantInfo info, bool reset)
	{
		int plantIndex = GetFreePlantIndex();
		if (plantIndex != -1)
		{
			Plant plant = (Plant) plantScene.Instantiate();
			info.plant = plant;
			info.inventoryIndex = plantIndex;
			plant.SetInfo(info);
			GD.Print(plantPositions[plantIndex]);
			plant.SetPositionPreset(plantPositions[plantIndex]);
			plants[plantIndex] = plant;
			plantLayer.AddChild(plant);
			plantNumber++;
			if (!reset){
				plantInfos.Add(new PlantInfo(info));
			}
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

	public void UpdateVisuals(){
		int x = plantInfos.Count;
		for (int i = 0; i < x; i++){
			AddPlant(new PlantInfo(plantInfos[i]), true);
		}
	}

	public void RemovePlant(int index)
	{
		foreach (Plant plant in GetPlantsPresent())
		{
			if (plant.GetPlantInfo().inventoryIndex == index)
			{
				if (plants[15] != null)
				{
					foreach (PlantInfo i in plantInfos){
						if (i.value == plant.info.value && i.species == plant.info.species){
							plantInfos.Remove(i);
							break;
						}
					}
					plants[index].QueueFree();
					if (plants[15] == plants[index])
					{
						plants[15].QueueFree();
						plants[15] = null;
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
