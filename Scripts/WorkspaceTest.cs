using Godot;
using System;

public partial class WorkspaceTest : Node2D
{
	[Export] private int testNumberPlants;
	[Export] private Inventory inventory;
	[Export] VirusItem[] testItems;

	public override void _Ready()
	{
		inventory.TestPlants(testNumberPlants);
		for (int i = 0; i <testItems.Length; i++){
			inventory.AddVirus(testItems[i]);
		}
		// Inventory.TestViruses(dropdown);
	}
}
