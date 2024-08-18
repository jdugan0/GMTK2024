using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Inventory instance;
	private List<VirusItem> viruses = new List<VirusItem>();
	private static List<PlantInfo> plantInfos = new List<PlantInfo>();
	[Export] private PlantLayer layer;

    public override void _Ready()
    {
		instance = this;
		foreach (PlantInfo info in plantInfos)
		{
			layer.AddPlant(info);
		}
    }

    public List<VirusItem> GetViruses()
	{
		return viruses;
	}

	public List<PlantInfo> GetPlants()
	{
		return plantInfos;
	}

	public void AddPlantInfo(PlantInfo plantInfo)
	{
		if (plantInfos.Count < 15)
		{
			plantInfos.Add(plantInfo);
			layer.AddPlant(plantInfo);
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
			PlantInfo info = new PlantInfo();
			info.species = new Species();
			info.species.species = Species.PlantSpecies.JuniperBerries;
			info.species.texture = (Texture2D) GD.Load("res://Art/juniper berries _test.png");
			AddPlantInfo(info);
		}
	}
}
