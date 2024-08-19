using Godot;
using System;

public partial class WorkspaceInit : Node2D
{
	[Export] CanvasLayer layer;
	[Export] private Node2D[] plantPositions;
    bool run = false;
    [Export] public static Label hoverText;
    [Export] public Label i;

    public override void _Ready()
    {
        hoverText = i;
        ConfigureInventory();
    }

    public override void _Process(double delta)
    {
        if (Inventory.instance.plantInfos.Count > 0 && !run){
            
            Inventory.instance.UpdateVisuals();
            
        }
        run = true;
        if (!Inventory.instance.start){
                Inventory.instance.AddPlant(Inventory.instance.starterPlant, false);
			    Inventory.instance.AddVirus(Inventory.instance.starterVirusItem);
                Inventory.instance.start = true;
        }
    }

    public void ConfigureInventory()
	{
		Inventory.ConfigurePlantData(plantPositions, layer);
	}
}
