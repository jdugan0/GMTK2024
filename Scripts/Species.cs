using Godot;
using System;

[GlobalClass]
public partial class Species : Resource
{
    public enum PlantSpecies
	{
		YellowTroutLily,
		JuniperBerries,
		CinnamonFern
	}

    [Export] public PlantSpecies species;
    [Export] public Texture2D texture;
}