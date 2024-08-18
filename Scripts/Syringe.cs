using Godot;
using System;

public partial class Syringe : TextureButton
{
	private VirusItem virusItem;
	private VirusDropdown container;
	private bool follow = false;
	[Export] private Texture2D texture; // TODO

	public Syringe(VirusItem virusItem, VirusDropdown container)
	{
		TextureNormal = (Texture2D) GD.Load("res://Art/injections icon.png");
		this.virusItem = virusItem;
		this.container = container;
	}

    public override void _Process(double delta)
    {
        if (follow == true)
		{
			
		}
    }

    public Syringe FreeAndDrag()
	{
		container.RemoveChild(this);
		follow = true;
		return this;
	}
}
