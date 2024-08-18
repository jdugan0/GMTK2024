using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	private List<VirusItem> viruses = new List<VirusItem>();
	private static List<Plant> plants = new List<Plant>();
	[Export] private PlantLayer layer;

	public List<VirusItem> GetViruses()
	{
		return viruses;
	}

	public List<Plant> GetPlants()
	{
		return plants;
	}

	public void AddPlant(Plant plant)
	{
		if (plants.Count < 15)
		{
			plants.Add(plant);
			layer.AddPlant();
		}
	}

	public void AddVirus(VirusItem virus)
	{
		viruses.Add(virus);
	}
	public void RemoveVirus(VirusItem virus){
		viruses.Remove(virus);
	}


	public void TestPlants(int number)
	{
		for (int i = 0; i < number; i++)
		{
			AddPlant(new Plant());
		}
	}
}
