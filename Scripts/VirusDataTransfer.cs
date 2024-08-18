using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class VirusDataTransfer : Node
{
	public static VirusDataTransfer instance;
	private static List<VirusItem> viruses;
	public static float score;

	public VirusDataTransfer()
	{
		instance = this;
		viruses = new List<VirusItem>();
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
}
