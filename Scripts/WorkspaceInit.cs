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
    [Export] TextureButton beginButton;
    [Export] TextureButton sellButton;

    public override void _Ready()
    {
        hoverText = i;
        ConfigureInventory();
        Inventory.instance.ResetVisuals();
    }

    public override void _Process(double delta)
    {
        if (Inventory.instance.quotaCountCurrent > Inventory.instance.quotaCount){
            beginButton.Visible = false;
            beginButton.Disabled = true;

            sellButton.Visible = true;
            sellButton.Disabled = false;
        }
        else{
            beginButton.Visible = true;
            beginButton.Disabled = false;

            sellButton.Visible = false;
            sellButton.Disabled = true;
        }
    }

    public void ConfigureInventory()
	{
		Inventory.instance.ConfigurePlantData(plantPositions, layer);
	}
}
