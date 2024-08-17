using Godot;
using System;

public partial class SyringeContainer : VBoxContainer
{
	public SyringeContainer()
	{
		foreach (VirusItem virus in Inventory.instance.GetViruses())
		{
			AddChild(new Syringe(virus));
			Console.WriteLine("syringe added");
		}
	}
}
