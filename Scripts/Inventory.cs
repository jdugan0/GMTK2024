using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Inventory instance;
	private List<VirusItem> viruses = new List<VirusItem>();
	private static List<PlantInfo> plantInfos = new List<PlantInfo>();
	[Export] VirusItem[] testItems;
	

    public override void _Ready()
    {
		instance = this;
		for (int i = 0; i < testItems.Length; i++){
			Inventory.instance.AddVirus(testItems[i]);
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

	public void AddPlantInfo(PlantInfo plantInfo, PlantLayer from)
	{
		if (plantInfos.Count < 15)
		{
			plantInfos.Add(plantInfo);
		}
	}

	public void RefreshVisuals(PlantLayer from){
		foreach (PlantInfo info in plantInfos){
			from.AddPlant(info);
			GD.Print(PlantLayer.GetTableOccupied());
			if (info.onTable){
				
				PlantLayer.SetTableOccupied(info.plant);
			}
		}
	}

	public void AddVirus(VirusItem virus)
	{
		viruses.Add(virus);
	}
	public void RemoveVirus(VirusItem virus){
		viruses.Remove(virus);
	}


	public void TestPlants(int number, PlantLayer from)
	{
		for (int i = 0; i < number; i++)
		{
			PlantInfo info = new PlantInfo();
			info.species = new Species();
			info.species.species = Species.PlantSpecies.JuniperBerries;
			info.species.texture = (Texture2D) GD.Load("res://Art/juniper berries _test.png");
			AddPlantInfo(info, from);
		}
	}
}
