using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class VirusDataTransfer : Node
{
	public static VirusDataTransfer instance;
	private static List<VirusItem> viruses;
	public static float score;

	private static List<PlantInfo> inventoryPlantInfo;

	public VirusDataTransfer()
	{
		instance = this;
		viruses = new List<VirusItem>();
		inventoryPlantInfo = new List<PlantInfo>();
	}

	public static void AddViruses(VirusItem virus)
	{
		viruses.Add(virus);
	}

	public static void ClearViruses()
	{
		viruses.Clear();
	}

	public static List<VirusItem> GetViruses()
	{
		return viruses;
	}

	public static void AddPlantInfo(PlantInfo info)
	{
		inventoryPlantInfo.Add(info);
	}

	public static List<PlantInfo> GetPlantInfo()
	{
		return inventoryPlantInfo;
	}
}
