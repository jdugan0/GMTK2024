using Godot;
using System;
using System.Collections.Generic;

public class StoreConfiguration
{
    private VirusItem[] viruses;
    private PlantInfo[] plants;
    public StoreConfiguration(VirusItem[] viruses, PlantInfo[] plants)
    {
        this.viruses = viruses;
        this.plants = plants;
    }

    public VirusItem[] GetVirusItems()
    {
        return viruses;
    }

    public PlantInfo[] GetPlantInfos()
    {
        return plants;
    }
}