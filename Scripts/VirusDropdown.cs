using Godot;
using System;

public partial class VirusDropdown : CanvasLayer
{
	public VirusDropdown()
	{
		// Visible = false;
		// foreach (VirusItem virus in Inventory.instance.GetViruses())
		// {
		// 	AddChild(new Syringe(virus, this));
		// }
	}

	public void Toggle()
	{
		Visible = !Visible;
	}
}
