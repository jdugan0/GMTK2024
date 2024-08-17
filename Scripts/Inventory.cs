using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Inventory instance;
	private static List<VirusItem> viruses;
	private static List<Plant> plants;

	public Inventory()
	{
		instance = this;
		viruses = new List<VirusItem>();
		plants = new List<Plant>();
	}

	public List<VirusItem> GetViruses()
	{
		return viruses;
	}
}
