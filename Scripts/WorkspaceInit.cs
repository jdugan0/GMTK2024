using Godot;
using System;

public partial class WorkspaceInit : Node2D
{
	[Export] CanvasLayer layer;
	[Export] private Node2D[] plantPositions;

    public override void _Ready()
    {
        ConfigureInventory();
    }

    public void ConfigureInventory()
	{
		Inventory.ConfigurePlantData(plantPositions, layer);
	}
}
