using Godot;
using System;
using System.Collections.Generic;

public partial class ViewVirusButton : TextureButton
{
	[Export] private Control container;
	[Export] PackedScene syringeScene;
	public List<Control> syringes = new List<Control>();
	public void ToggleMenu()
	{
		container.Visible = !container.Visible;
		if (container.Visible)
		{
			foreach (VirusItem item in Inventory.instance.GetViruses())
			{
				SyringeDragging syringeDragging = (SyringeDragging)syringeScene.Instantiate();
				syringeDragging.virus = item;
				syringes.Add(syringeDragging);
				container.AddChild(syringeDragging);
			}
		}

		else
		{
			for (int i = syringes.Count - 1; i >= 0; i--){
				syringes[i].QueueFree();
				syringes.RemoveAt(i);
			}
		}
	}

}
