using Godot;
using System;

public partial class WorkspaceTest : Node2D
{
	[Export] private int testNumberPlants;
	[Export] VirusItem[] testItems;

	public override void _Ready()
	{
		Inventory.instance.TestPlants(testNumberPlants);
		for (int i = 0; i <testItems.Length; i++){
			Inventory.instance.AddVirus(testItems[i]);
		}
		// Inventory.TestViruses(dropdown);
	}
}
