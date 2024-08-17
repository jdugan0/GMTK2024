using Godot;
using System;

public partial class ViewVirusButton : TextureButton
{
	[Export] private VirusDropdown menu;

	public void ToggleMenu()
	{
		menu.Toggle();
	}
}
