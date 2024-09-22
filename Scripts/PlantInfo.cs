using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class PlantInfo : Resource
{
    [Export] public Species species;
    public List<VirusItem> viruses = new List<VirusItem>();
    [Export] public float price;
    [Export] public float value;
    public bool onTable;
    public Vector2 slot;
    public bool mutated = false;

    public PlantInfo(){

    }
    public PlantInfo(PlantInfo item){
        species = item.species;
        viruses = item.viruses;
        price = item.price;
        value = item.value;
        onTable = item.onTable;
        slot = item.slot;
        mutated = item.mutated;
    }

}