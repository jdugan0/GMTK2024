using Godot;
using System;

public partial class ViewVirusButton : TextureButton
{
	[Export] private Control container;
	public void ToggleMenu()
	{
		container.Visible = !container.Visible;
	}
}
