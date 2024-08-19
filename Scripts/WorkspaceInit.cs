using Godot;
using System;

public partial class WorkspaceInit : Node2D
{
	[Export] CanvasLayer layer;
	[Export] private Node2D[] plantPositions;
    bool run = false;

    public override void _Ready()
    {
        
        ConfigureInventory();
    }

    public override void _Process(double delta)
    {
        if (Inventory.instance.plantInfos.Count > 0 && !run){
            
            Inventory.instance.UpdateVisuals();
            
        }
        run = true;
        if (!Inventory.instance.start){
                Inventory.instance.AddPlant(Inventory.instance.starterPlant);
			    Inventory.instance.AddVirus(Inventory.instance.starterVirusItem);
                Inventory.instance.start = true;
        }
    }

    public void ConfigureInventory()
	{
		Inventory.ConfigurePlantData(plantPositions, layer);
	}
}
