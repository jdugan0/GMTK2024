using Godot;
using System;

public partial class WorkspaceTest : Node2D
{
	[Export] private int testNumberPlants;
	[Export] VirusItem[] testItems;
	[Export] PlantLayer layer;

	public override void _Ready()
	{
		if (Inventory.instance.GetPlants().Count <= 0){
			Inventory.instance.TestPlants(testNumberPlants, layer);
		}
		Inventory.instance.RefreshVisuals(layer);
		for (int i = 0; i < testItems.Length; i++){
			Inventory.instance.AddVirus(testItems[i]);
		}
		// Inventory.TestViruses(dropdown);
	}
}
