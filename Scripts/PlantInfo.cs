using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class PlantInfo : Resource
{
    [Export] public Species species;
    public List<VirusItem> viruses = new List<VirusItem>();
    public Vector2 posSlot = new Vector2(0, 0);
    public bool onTable = false;
    public Plant plant;
    [Export] public float price;
    [Export] public float value;
    public PlantInfo(){

    }
    public PlantInfo(PlantInfo item){
        species = item.species;
        viruses = item.viruses;
        posSlot = item.posSlot;
        onTable = item.onTable;
        plant = item.plant;
        price = item.price;
        value = item.value;
    }

}