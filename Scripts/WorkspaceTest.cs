using Godot;
using System;

public partial class WorkspaceTest : Node2D
{
	[Export] private int testNumberPlants;
	
	[Export] PlantLayer layer;

	public override void _Ready()
	{
		// if (Inventory.instance.GetPlants().Count <= 0){
		// 	Inventory.instance.TestPlants(testNumberPlants, layer);
		// }
		// Inventory.instance.RefreshVisuals(layer);
		
		// Inventory.TestViruses(dropdown);
	}
}
