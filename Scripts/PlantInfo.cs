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
    [Export] public float value;
}