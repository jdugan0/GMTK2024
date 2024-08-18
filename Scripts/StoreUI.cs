using Godot;
using System;
using System.Collections;

public partial class StoreUI : Control
{
	[Export] private VirusItem[] virusPresets;
    [Export] private PlantInfo[] plantPresets;
	[Export] private VBoxContainer buttons;
    [Export] private Texture2D[] textures;
    [Export] private PackedScene purchaseVirusButton;
    [Export] private PackedScene purchasePlantButton;

    public override void _Ready()
    {
        StoreConfiguration config = RandomizeStore(5, 3);
        int amount = 0;
		foreach (VirusItem virus in config.GetVirusItems())
		{
			PurchaseVirusButton button = (PurchaseVirusButton)(purchaseVirusButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
			button.SetVirusItem(virus);
			buttons.AddChild(button);
            amount++;
		}
		foreach (PlantInfo plant in config.GetPlantInfos())
		{
			PurchasePlantButton button = (PurchasePlantButton)(purchasePlantButton.Instantiate());
            button.TextureNormal = textures[amount % 2];
            button.TextureDisabled = textures[amount % 2];
            button.TextureHover = textures[amount % 2];
            button.TextureFocused = textures[amount % 2];
            button.TexturePressed = textures[amount % 2];
			button.SetPlantInfo(plant);
			buttons.AddChild(button);
            amount++;
		}
    }

	public void Toggle()
	{
		Visible = !Visible;
	}

	private StoreConfiguration RandomizeStore(int numViruses, int numPlants)
    {
        VirusItem[] viruses = new VirusItem[numViruses];
        PlantInfo[] plants = new PlantInfo[numPlants];
        for (int i = 0; i < numViruses; i++)
        {
            VirusItem v = virusPresets[GD.RandRange(0, virusPresets.Length - 1)];
            viruses[i] = new VirusItem(v);
        }
        for (int i = 0; i < numPlants; i++)
        {
            PlantInfo p = plantPresets[GD.RandRange(0, plantPresets.Length - 1)];
            plants[i] = new PlantInfo(p);
        }
        return new StoreConfiguration(viruses, plants);
    }
}
