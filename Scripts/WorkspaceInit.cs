using Godot;
using System;
using System.Linq;

public partial class WorkspaceInit : Node2D
{
	[Export] Control layer;
	[Export] private Node2D[] plantPositions;
    bool run = false;
    [Export] public static Label hoverText;
    [Export] public Label i;

    public override void _Ready()
    {
        hoverText = i;
        ConfigureInventory();
        Inventory.instance.ResetVisuals();
    }

    public override void _Process(double delta)
    {
        
    }

    public void ConfigureInventory()
	{
		Inventory.instance.ConfigurePlantData(plantPositions, layer);
	}
}
