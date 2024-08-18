using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Inventory instance;
	private static List<VirusItem> viruses;
	private static List<Plant> plants;

	public Inventory()
	{
		instance = this;
		viruses = new List<VirusItem>();
		plants = new List<Plant>();
	}

	public static List<VirusItem> GetViruses()
	{
		return viruses;
	}

	public static List<Plant> GetPlants()
	{
		return plants;
	}

	public static void AddPlant(Plant plant, PlantLayer from)
	{
		if (plants.Count < 15)
		{
			plants.Add(plant);
			from.AddPlant();
		}
	}

	public static void AddVirus(VirusItem virus, VirusDropdown from)
	{
		viruses.Add(virus);
		from.AddChild(new Syringe(virus, from));
	}


	// Test methods
	public static void TestViruses(VirusDropdown from)
	{
		for (int i = 0; i < 3; i++)
		{
			AddVirus(new VirusItem(), from);
		}
	}

	public static void TestPlants(PlantLayer layer, int number)
	{
		for (int i = 0; i < number; i++)
		{
			AddPlant(new Plant(), layer);
		}
	}
}
