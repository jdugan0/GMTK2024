using Godot;
using System;

public partial class StoreUI : Control
{
	[Export] private VirusItem[] virusPresets;

    public override void _Ready()
    {
        
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
            viruses[i] = virusPresets[GD.RandRange(0, virusPresets.Length - 1)];
        }
        for (int i = 0; i < numPlants; i++)
        {
            PlantInfo plant = new PlantInfo();
            int speciesIndex = GD.RandRange(0, 2);
            if (speciesIndex == 0)
            {
                plant.species.species = Species.PlantSpecies.YellowTroutLily;
                plant.species.texture = (Texture2D) GD.Load("res://Art/yellow trout lily _test.png");
            } else if (speciesIndex == 1)
            {
                plant.species.species = Species.PlantSpecies.JuniperBerries;
                plant.species.texture = (Texture2D) GD.Load("res://Art/juniper berries _test.png");
            } else if (speciesIndex == 2)
            {
                plant.species.species = Species.PlantSpecies.CinnamonFern;
                plant.species.texture = (Texture2D) GD.Load("res://Art/cinnamon fern _test.png");
            }
            plants[i] = plant;
        }
        return new StoreConfiguration(viruses, plants);
    }
}
