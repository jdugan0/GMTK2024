using Godot;
using System;
using System.Collections.Generic;

public class StoreConfiguration
{
    private List<VirusItem> viruses;
    private List<PlantInfo> plants;
    public StoreConfiguration(List<VirusItem>  viruses, List<PlantInfo> plants)
    {
        this.viruses = viruses;
        this.plants = plants;
    }

    public List<VirusItem> GetVirusItems()
    {
        return viruses;
    }

    public List<PlantInfo> GetPlantInfos()
    {
        return plants;
    }
}