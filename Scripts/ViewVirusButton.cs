using Godot;
using System;
using System.Collections.Generic;

public partial class ViewVirusButton : TextureButton
{
	[Export] private Control container;
	[Export] PackedScene syringeScene;
	[Export] private Label hoverLabel;
	public List<SyringeDragging> syringes = new List<SyringeDragging>();
	public static ViewVirusButton instance;
    public override void _Ready()
    {
        instance = this;
    }
    public void ToggleMenu()
	{
		container.Visible = !container.Visible;
		if (container.Visible)
		{
			foreach (VirusItem item in Inventory.instance.GetViruses())
			{
				SyringeDragging syringeDragging = (SyringeDragging) syringeScene.Instantiate();
				syringeDragging.virus = item;
				syringeDragging.SetTextures();
				syringes.Add(syringeDragging);
				container.AddChild(syringeDragging);
			}
		}

		else
		{
			if (Inventory.instance.GetTableOccuplant() != null){
				Inventory.instance.GetTableOccuplant().ClearViruses();
				Inventory.instance.GetTableOccuplant().syringe.Clear();
			}
			
			for (int i = syringes.Count - 1; i >= 0; i--){
				syringes[i].QueueFree();
				syringes.RemoveAt(i);
			}
		}
	}

	public void RemoveViruses(List<VirusItem> toRemove){
		
	}

	public void Hover()
	{
		hoverLabel.Text = "Click to view genomes";
		hoverLabel.Visible = true;
	}

	public void HoverExit()
	{
		hoverLabel.Visible = false;
	}

    public override void _Process(double delta)
    {
        if (hoverLabel.Visible)
		{
			hoverLabel.Position = GetLocalMousePosition() + new Vector2(30,0);
		}
    }
}
