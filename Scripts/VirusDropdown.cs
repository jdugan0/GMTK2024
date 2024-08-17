using Godot;
using System;

public partial class VirusDropdown : CanvasLayer
{
	public VirusDropdown()
	{
		Visible = false;
	}

	public void Toggle()
	{
		Visible = !Visible;
	}
}
