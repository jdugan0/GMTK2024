using Godot;
using System;

public partial class WorkspaceTest : Node2D
{
	[Export] private VirusDropdown dropdown;
	[Export] private PlantLayer layer;
	[Export] private int testNumberPlants;

	public override void _Ready()
	{
		Inventory.TestPlants(layer, testNumberPlants);
		// Inventory.TestViruses(dropdown);
	}
}
